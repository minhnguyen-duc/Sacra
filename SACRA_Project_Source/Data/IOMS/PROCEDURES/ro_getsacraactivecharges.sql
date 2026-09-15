create or replace PROCEDURE ro_getsacraactivecharges( pi_request_ids IN VARCHAR2,
													  pi_prn         IN VARCHAR2,
													  po_cursor      IN OUT iomstypes.rs_out_ref_cursor)


 AS
BEGIN
IF pi_request_ids IS NULL
   OR TRIM(pi_request_ids) IS NULL
   OR pi_prn IS NULL
   OR TRIM(pi_prn) IS NULL THEN

    OPEN po_cursor FOR
        SELECT
            CAST(NULL AS VARCHAR2(50))   AS prisoner_id,
            CAST(NULL AS VARCHAR2(50))   AS offence_code,
            CAST(NULL AS VARCHAR2(4000)) AS offence_desc,
            CAST(NULL AS VARCHAR2(100))  AS crn,
            CAST(NULL AS NUMBER)         AS charge_count
        FROM
            dual
        WHERE
            1 = 0;

    RETURN;
END IF;

OPEN po_cursor FOR
    SELECT
        qac.identifier AS prisoner_id,
        TRIM(qac.code_offence) AS offence_code,
        DECODE(
            qac.charge_description,
            NULL,
            epic.ef_lookup_text('OFFENCE CODE', qac.code_offence, 'IC'),
            qac.charge_description
        ) AS offence_desc,
        MAX(qac.crn) AS crn,
        COUNT(*) AS charge_count
    FROM
        epic.eh_ii_qac_active_charge qac
    WHERE
        qac.request_id IN (
            SELECT
                TO_NUMBER(TRIM(column_value))
            FROM
                TABLE(ioms.fn_split(pi_request_ids, ','))
            WHERE
                REGEXP_LIKE(TRIM(column_value), '^[0-9]+$')
        )
        AND TRIM(qac.identifier) = TRIM(pi_prn)
    GROUP BY
        qac.identifier,
        TRIM(qac.code_offence),
        DECODE(
            qac.charge_description,
            NULL,
            epic.ef_lookup_text('OFFENCE CODE', qac.code_offence, 'IC'),
            qac.charge_description
        )
    ORDER BY
        MAX(qac.crn) ASC;
END;
/ 