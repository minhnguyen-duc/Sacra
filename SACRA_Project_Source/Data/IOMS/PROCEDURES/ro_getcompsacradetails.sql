CREATE OR REPLACE PROCEDURE ro_getcompsacradetails(
							   pi_cell_sharing_risk_assmnt_id IN VARCHAR2 DEFAULT NULL,
							   pi_prisoner_ids                 IN VARCHAR2 DEFAULT NULL,
po_cursor                      IN OUT iomstypes.rs_out_ref_cursor
)

AS
BEGIN
IF pi_cell_sharing_risk_assmnt_id IS NOT NULL THEN
    FOR r IN (
        SELECT
            csp.prisoner_id
        FROM
            ioms.om_cell_sharing_prisoner csp
        WHERE
            csp.cell_sharing_risk_assmnt_id = pi_cell_sharing_risk_assmnt_id
        ORDER BY
            csp.prisoner_order_num
    )
    LOOP
        ioms.pkg_cell_sharing_snapshot.refresh_cell_sharing_offender(r.prisoner_id);
    END LOOP;
ELSIF pi_prisoner_ids IS NOT NULL THEN
    FOR r IN (
        SELECT
            TRIM(column_value) AS prisoner_id
        FROM
            TABLE(ioms.fn_split(pi_prisoner_ids, ','))
        WHERE
            TRIM(column_value) IS NOT NULL
    )
    LOOP
        ioms.pkg_cell_sharing_snapshot.refresh_cell_sharing_offender(r.prisoner_id);
    END LOOP;
END IF;

COMMIT;

-- Two selectable prisoner sources, gated by which parameter is supplied:
--   1) an existing SACRA assessment (pi_cell_sharing_risk_assmnt_id) - the original, unchanged path.
--   2) prisoner ids selected in the UI but not yet saved to any assessment (pi_prisoner_ids) - used
--      by the Combined SACRA Report when run before the record is saved. Pairing/order is taken from
--      the position of each id in the comma-separated list (A first, then B), mirroring prisoner_order_num.
OPEN po_cursor FOR
    WITH ordered_prisoners AS (
        SELECT
            csp.cell_sharing_risk_assmnt_id,
            csp.prisoner_id,
            csp.prisoner_order_num,
            FLOOR(csp.prisoner_order_num / 2) + 1 AS pair_group_no,
            CASE
                WHEN MOD(csp.prisoner_order_num, 2) = 0 THEN 'A'
                ELSE 'B'
            END AS pair_slot
        FROM
            ioms.om_cell_sharing_prisoner csp
        WHERE
            pi_cell_sharing_risk_assmnt_id IS NOT NULL
            AND csp.cell_sharing_risk_assmnt_id = pi_cell_sharing_risk_assmnt_id

        UNION ALL

        SELECT
            pi_cell_sharing_risk_assmnt_id  AS cell_sharing_risk_assmnt_id,
            TRIM(sp.column_value)           AS prisoner_id,
            sp.rn - 1                       AS prisoner_order_num,
            FLOOR((sp.rn - 1) / 2) + 1       AS pair_group_no,
            CASE
                WHEN MOD(sp.rn - 1, 2) = 0 THEN 'A'
                ELSE 'B'
            END AS pair_slot
        FROM (
            SELECT
                column_value,
                ROWNUM AS rn
            FROM
                TABLE(ioms.fn_split(pi_prisoner_ids, ','))
        ) sp
        WHERE
            pi_cell_sharing_risk_assmnt_id IS NULL
            AND pi_prisoner_ids IS NOT NULL
            AND TRIM(sp.column_value) IS NOT NULL
    ),
    off_data AS (
        SELECT DISTINCT
            op.pair_group_no,
            op.pair_slot,
            op.prisoner_order_num,
            ocss.prisoner_id,
            epic.dob_age(epi.date_of_birth) AS offender_age,
            epic.ef_lookup_text(
                'OFFENDER STATUS',
                ees.code_entity_status,
                'IC'
            ) AS offender_status,
            ioms.pkg_da_offender.get_offender_name_by_service(
                epi.entity_id,
                'PPS',
                'First Middle LAST'
            ) AS offender_name,
            ioms.pkg_da_offender.get_prn_dlicno_by_service(
                epi.entity_id,
                'PPS'
            ) AS prn_dlic_no,
            TO_CHAR(seccls.initiated_date, 'dd/mm/yyyy') AS initiated_date,
            DECODE(
                ocss.security_classification_id,
                NULL,
                'Unclassified',
                reflk.lookup_desc
            ) AS security_class
        FROM
            ordered_prisoners op
            INNER JOIN epic.eh_entity_person eep
                ON eep.entity_id = op.prisoner_id
            INNER JOIN epic.eh_person_identity epi
                ON eep.primary_identity_id = epi.identity_id
            INNER JOIN epic.eh_entity_status ees
                ON eep.entity_id = ees.entity_id
            INNER JOIN ioms.om_cell_sharing_snapshot ocss
                ON eep.entity_id = ocss.prisoner_id
            LEFT JOIN ioms.om_cell_sharing_alert ocsa
                ON ocss.cell_sharing_snapshot_id = ocsa.cell_sharing_snapshot_id
            LEFT JOIN epic.eh_entity_alerts eea
                ON ocsa.alert_id = eea.alert_id
                AND eea.alert_active = 'Y'
            LEFT JOIN ioms.om_security_classification seccls
                ON ocss.security_classification_id = seccls.security_classification_id
            LEFT JOIN ioms.om_ref_lookup reflk
                ON reflk.lookup_class = 'SECURITY CLASSIFICATION'
                AND TRIM(seccls.sec_class_code) = TRIM(reflk.lookup_code)
        WHERE
            ioms.pkg_da_offender.get_pps_primary_identity_id(eep.entity_id) = epi.identity_id
    )
    SELECT
        pair_group_no,

        MAX(CASE WHEN pair_slot = 'A' THEN prisoner_id END) AS prisoner_id_a,
        MAX(CASE WHEN pair_slot = 'A' THEN offender_age END) AS offender_age_a,
        MAX(CASE WHEN pair_slot = 'A' THEN offender_status END) AS offender_status_a,
        MAX(CASE WHEN pair_slot = 'A' THEN offender_name END) AS offender_name_a,
        MAX(CASE WHEN pair_slot = 'A' THEN prn_dlic_no END) AS prn_dlic_no_a,
        MAX(CASE WHEN pair_slot = 'A' THEN initiated_date END) AS initiated_date_a,
        MAX(CASE WHEN pair_slot = 'A' THEN security_class END) AS security_class_a,

        MAX(CASE WHEN pair_slot = 'B' THEN prisoner_id END) AS prisoner_id_b,
        MAX(CASE WHEN pair_slot = 'B' THEN offender_age END) AS offender_age_b,
        MAX(CASE WHEN pair_slot = 'B' THEN offender_status END) AS offender_status_b,
        MAX(CASE WHEN pair_slot = 'B' THEN offender_name END) AS offender_name_b,
        MAX(CASE WHEN pair_slot = 'B' THEN prn_dlic_no END) AS prn_dlic_no_b,
        MAX(CASE WHEN pair_slot = 'B' THEN initiated_date END) AS initiated_date_b,
        MAX(CASE WHEN pair_slot = 'B' THEN security_class END) AS security_class_b
    FROM
        off_data
    GROUP BY
        pair_group_no
    ORDER BY
        pair_group_no;
END;
/
