CREATE OR REPLACE PACKAGE pkg_sacra_result IS
    PROCEDURE ro_getcellsharingdetails (
        pi_entityid           IN ioms.om_cell_sharing_prisoner.prisoner_id%TYPE,
        pi_assessmentdatefrom IN ioms.om_cell_sharing_risk_assmnt.assessment_date%TYPE,
        pi_assessmentdateto   IN ioms.om_cell_sharing_risk_assmnt.assessment_date%TYPE,
        po_cursor             OUT iomstypes.rs_out_ref_cursor
    );

    PROCEDURE rw_om_cell_sharing_risk_assmnt (
        pi_cell_sharing_risk_assmnt_id IN ioms.om_cell_sharing_risk_assmnt.cell_sharing_risk_assmnt_id%TYPE,
        pi_ntdb_alert_id               IN ioms.om_cell_sharing_risk_assmnt.ntdb_alert_id%TYPE,
        pi_assessment_date             IN ioms.om_cell_sharing_risk_assmnt.assessment_date%TYPE,
        pi_assessment_result_code      IN ioms.om_cell_sharing_risk_assmnt.assessment_result_code%TYPE,
        pi_assessment_comment          IN ioms.om_cell_sharing_risk_assmnt.assessment_comment%TYPE,
        pi_action_by                   IN ioms.om_cell_sharing_risk_assmnt.action_by%TYPE,
        po_cell_sharing_risk_assmnt_id OUT ioms.om_cell_sharing_risk_assmnt.cell_sharing_risk_assmnt_id%TYPE,
        po_result                      OUT NUMBER
    );

    PROCEDURE rw_om_cell_sharing_prisoner (
        pi_cell_sharing_prisoner_id    IN ioms.om_cell_sharing_prisoner.cell_sharing_prisoner_id%TYPE,
        pi_cell_sharing_risk_assmnt_id IN ioms.om_cell_sharing_prisoner.cell_sharing_risk_assmnt_id%TYPE,
        pi_prisoner_id                 IN ioms.om_cell_sharing_prisoner.prisoner_id%TYPE,
        pi_prisoner_order_num          IN ioms.om_cell_sharing_prisoner.prisoner_order_num%TYPE,
        pi_action_by                   IN ioms.om_cell_sharing_prisoner.action_by%TYPE,
        po_cell_sharing_prisoner_id    OUT ioms.om_cell_sharing_prisoner.cell_sharing_prisoner_id%TYPE,
        po_result                      OUT NUMBER
    );

    PROCEDURE rw_delete_om_cell_sharing_prisoner (
        pi_cell_sharing_prisoner_id IN ioms.om_cell_sharing_prisoner.cell_sharing_prisoner_id%TYPE,
        pi_action_by                IN ioms.om_cell_sharing_prisoner.action_by%TYPE,
        po_result                   OUT NUMBER
    );

    PROCEDURE rw_om_doc_function_rel (
        pi_document_id IN ioms.om_doc_function_rel.document_id%TYPE,
        pi_function_id IN ioms.om_doc_function_rel.function_id%TYPE,
        pi_action_by   IN ioms.om_doc_function_rel.action_by%TYPE,
        po_result      OUT NUMBER
    );
    PROCEDURE rw_delete_cell_sharing_prisoners_from_order (
    pi_cell_sharing_risk_assmnt_id IN ioms.om_cell_sharing_prisoner.cell_sharing_risk_assmnt_id%TYPE,
    pi_from_prisoner_order_num     IN ioms.om_cell_sharing_prisoner.prisoner_order_num%TYPE,
    pi_action_by                   IN ioms.om_cell_sharing_prisoner.action_by%TYPE,
    po_result                      OUT NUMBER
);
   PROCEDURE ro_get_sacra_report_document_info (
    pi_cell_sharing_risk_assmnt_id IN ioms.om_doc_function_rel.function_id%TYPE,
    po_cursor             OUT iomstypes.rs_out_ref_cursor
   );
   PROCEDURE ro_getrequestid(
    pi_cell_sharing_assmnt_id IN VARCHAR2,
    po_cursor                 IN OUT iomstypes.rs_out_ref_cursor
);
END pkg_sacra_result;
/
CREATE OR REPLACE PACKAGE BODY pkg_sacra_result IS

    PROCEDURE ro_getcellsharingdetails (
        pi_entityid           IN ioms.om_cell_sharing_prisoner.prisoner_id%TYPE,
        pi_assessmentdatefrom IN ioms.om_cell_sharing_risk_assmnt.assessment_date%TYPE,
        pi_assessmentdateto   IN ioms.om_cell_sharing_risk_assmnt.assessment_date%TYPE,
        po_cursor             OUT iomstypes.rs_out_ref_cursor
    ) AS
        l_assess_sacra_result_type CONSTANT ioms.om_cell_sharing_risk_assmnt.assessment_type_code%TYPE := '7320';
    BEGIN
        OPEN po_cursor FOR
            SELECT /*+pkg_sacra_result*/
                csp.prisoner_first_name,
                csp.prisoner_first_prn,
                csp.prisoner_first_id,
                csp.cell_sharing_first_prisoner_id,
                csra.created_by AS created_by_id,

                csp.prisoner_second_name,
                csp.prisoner_second_prn,
                csp.prisoner_second_id,
                csp.cell_sharing_sec_prisoner_id,

                csp.prisoner_third_name,
                csp.prisoner_third_prn,
                csp.prisoner_third_id,
                csp.cell_sharing_third_prisoner_id,

                csp.prisoner_fourth_name,
                csp.prisoner_fourth_prn,
                csp.prisoner_fourth_id,
                csp.cell_sharing_fourth_prisoner_id,

                csp.prisoner_fifth_name,
                csp.prisoner_fifth_prn,
                csp.prisoner_fifth_id,
                csp.cell_sharing_fifth_prisoner_id,

                csp.prisoner_sixth_name,
                csp.prisoner_sixth_prn,
                csp.prisoner_sixth_id,
                csp.cell_sharing_sixth_prisoner_id,

                csp.prisoner_seventh_name,
                csp.prisoner_seventh_prn,
                csp.prisoner_seventh_id,
                csp.cell_sharing_seventh_prisoner_id,

                csp.prisoner_eighth_name,
                csp.prisoner_eighth_prn,
                csp.prisoner_eighth_id,
                csp.cell_sharing_eighth_prisoner_id,

                csp.prisoner_ninth_name,
                csp.prisoner_ninth_prn,
                csp.prisoner_ninth_id,
                csp.cell_sharing_ninth_prisoner_id,

                csp.prisoner_tenth_name,
                csp.prisoner_tenth_prn,
                csp.prisoner_tenth_id,
                csp.cell_sharing_tenth_prisoner_id,

                csp.prisoner_count,
                csra.assessment_date,
                csra.assessment_result_code AS assessment_result,
                csra.assessment_comment     AS assessment_comments,
                csra.cell_sharing_risk_assmnt_id AS application_id,
                csra.cell_sharing_risk_assmnt_id AS cell_sharing_risk_assmnt_id,
                ioms.fn_get_staff_name(csra.created_by) AS created_by_name,
                csra.created_date AS created_time,
                CASE
                    WHEN csra.updated_date IS NOT NULL
                         AND (csp.latest_prisoner_action_time IS NULL
                              OR csra.updated_date >= csp.latest_prisoner_action_time) THEN
                        ioms.fn_get_staff_name(csra.updated_by)
                    WHEN csp.latest_prisoner_action_by IS NOT NULL THEN
                        ioms.fn_get_staff_name(csp.latest_prisoner_action_by)
                END AS updated_by_name,
                CASE
                    WHEN csra.updated_date IS NOT NULL
                         AND (csp.latest_prisoner_action_time IS NULL
                              OR csra.updated_date >= csp.latest_prisoner_action_time) THEN
                        csra.updated_date
                    ELSE
                        CAST(csp.latest_prisoner_action_time AS DATE)
                END AS updated_time,
                doc.document_id,
                doc.version_num
            FROM
                ioms.om_cell_sharing_risk_assmnt csra
                INNER JOIN (
                    SELECT
                        cell_sharing_risk_assmnt_id,

                        MAX(CASE WHEN prisoner_order_num = 0 THEN offender END) AS prisoner_first_name,
                        MAX(CASE WHEN prisoner_order_num = 0 THEN prn_dlicno END) AS prisoner_first_prn,
                        MAX(CASE WHEN prisoner_order_num = 0 THEN prisoner_id END) AS prisoner_first_id,
                        MAX(CASE WHEN prisoner_order_num = 0 THEN cell_sharing_prisoner_id END) AS cell_sharing_first_prisoner_id,

                        MAX(CASE WHEN prisoner_order_num = 1 THEN offender END) AS prisoner_second_name,
                        MAX(CASE WHEN prisoner_order_num = 1 THEN prn_dlicno END) AS prisoner_second_prn,
                        MAX(CASE WHEN prisoner_order_num = 1 THEN prisoner_id END) AS prisoner_second_id,
                        MAX(CASE WHEN prisoner_order_num = 1 THEN cell_sharing_prisoner_id END) AS cell_sharing_sec_prisoner_id,

                        MAX(CASE WHEN prisoner_order_num = 2 THEN offender END) AS prisoner_third_name,
                        MAX(CASE WHEN prisoner_order_num = 2 THEN prn_dlicno END) AS prisoner_third_prn,
                        MAX(CASE WHEN prisoner_order_num = 2 THEN prisoner_id END) AS prisoner_third_id,
                        MAX(CASE WHEN prisoner_order_num = 2 THEN cell_sharing_prisoner_id END) AS cell_sharing_third_prisoner_id,

                        MAX(CASE WHEN prisoner_order_num = 3 THEN offender END) AS prisoner_fourth_name,
                        MAX(CASE WHEN prisoner_order_num = 3 THEN prn_dlicno END) AS prisoner_fourth_prn,
                        MAX(CASE WHEN prisoner_order_num = 3 THEN prisoner_id END) AS prisoner_fourth_id,
                        MAX(CASE WHEN prisoner_order_num = 3 THEN cell_sharing_prisoner_id END) AS cell_sharing_fourth_prisoner_id,

                        MAX(CASE WHEN prisoner_order_num = 4 THEN offender END) AS prisoner_fifth_name,
                        MAX(CASE WHEN prisoner_order_num = 4 THEN prn_dlicno END) AS prisoner_fifth_prn,
                        MAX(CASE WHEN prisoner_order_num = 4 THEN prisoner_id END) AS prisoner_fifth_id,
                        MAX(CASE WHEN prisoner_order_num = 4 THEN cell_sharing_prisoner_id END) AS cell_sharing_fifth_prisoner_id,

                        MAX(CASE WHEN prisoner_order_num = 5 THEN offender END) AS prisoner_sixth_name,
                        MAX(CASE WHEN prisoner_order_num = 5 THEN prn_dlicno END) AS prisoner_sixth_prn,
                        MAX(CASE WHEN prisoner_order_num = 5 THEN prisoner_id END) AS prisoner_sixth_id,
                        MAX(CASE WHEN prisoner_order_num = 5 THEN cell_sharing_prisoner_id END) AS cell_sharing_sixth_prisoner_id,

                        MAX(CASE WHEN prisoner_order_num = 6 THEN offender END) AS prisoner_seventh_name,
                        MAX(CASE WHEN prisoner_order_num = 6 THEN prn_dlicno END) AS prisoner_seventh_prn,
                        MAX(CASE WHEN prisoner_order_num = 6 THEN prisoner_id END) AS prisoner_seventh_id,
                        MAX(CASE WHEN prisoner_order_num = 6 THEN cell_sharing_prisoner_id END) AS cell_sharing_seventh_prisoner_id,

                        MAX(CASE WHEN prisoner_order_num = 7 THEN offender END) AS prisoner_eighth_name,
                        MAX(CASE WHEN prisoner_order_num = 7 THEN prn_dlicno END) AS prisoner_eighth_prn,
                        MAX(CASE WHEN prisoner_order_num = 7 THEN prisoner_id END) AS prisoner_eighth_id,
                        MAX(CASE WHEN prisoner_order_num = 7 THEN cell_sharing_prisoner_id END) AS cell_sharing_eighth_prisoner_id,

                        MAX(CASE WHEN prisoner_order_num = 8 THEN offender END) AS prisoner_ninth_name,
                        MAX(CASE WHEN prisoner_order_num = 8 THEN prn_dlicno END) AS prisoner_ninth_prn,
                        MAX(CASE WHEN prisoner_order_num = 8 THEN prisoner_id END) AS prisoner_ninth_id,
                        MAX(CASE WHEN prisoner_order_num = 8 THEN cell_sharing_prisoner_id END) AS cell_sharing_ninth_prisoner_id,

                        MAX(CASE WHEN prisoner_order_num = 9 THEN offender END) AS prisoner_tenth_name,
                        MAX(CASE WHEN prisoner_order_num = 9 THEN prn_dlicno END) AS prisoner_tenth_prn,
                        MAX(CASE WHEN prisoner_order_num = 9 THEN prisoner_id END) AS prisoner_tenth_id,
                        MAX(CASE WHEN prisoner_order_num = 9 THEN cell_sharing_prisoner_id END) AS cell_sharing_tenth_prisoner_id,

                        COUNT(*) AS prisoner_count,
                        MAX(action_time) AS latest_prisoner_action_time,
                        MAX(action_by) KEEP (DENSE_RANK LAST ORDER BY action_time) AS latest_prisoner_action_by
                    FROM
                        (
                            SELECT
                                csp.cell_sharing_prisoner_id,
                                csp.cell_sharing_risk_assmnt_id,
                                csp.prisoner_id,
                                csp.prisoner_order_num,
                                csp.action_by,
                                csp.action_time,
                                ioms.pkg_da_offender.get_offender_name_by_service(csp.prisoner_id, 'PPS', 'LAST, First Middle') AS offender,
                                ioms.fn_getprnforsharedassmcom(csp.prisoner_id, 'PPS') AS prn_dlicno
                            FROM
                                ioms.om_cell_sharing_prisoner csp
                            WHERE
                                csp.prisoner_order_num BETWEEN 0 AND 9
                                AND csp.cell_sharing_risk_assmnt_id IN (
                                    SELECT
                                        csp_filter.cell_sharing_risk_assmnt_id
                                    FROM
                                        ioms.om_cell_sharing_prisoner csp_filter
                                    WHERE
                                        csp_filter.prisoner_id = pi_entityid
                                )
                        )
                    GROUP BY
                        cell_sharing_risk_assmnt_id
                ) csp ON csp.cell_sharing_risk_assmnt_id = csra.cell_sharing_risk_assmnt_id
                LEFT JOIN ioms.om_doc_function_rel docrel ON (
                    docrel.function_id = csra.cell_sharing_risk_assmnt_id
                    AND docrel.doc_rel_type_code = '5024'
                )
                LEFT JOIN ioms.om_document doc ON (
                    doc.document_id = docrel.document_id
                    AND doc.document_type_code = 'SACRA'
                    AND doc.version_num IN (
                        SELECT
                            MAX(doc2.version_num)
                        FROM
                            ioms.om_document doc2
                        WHERE
                            doc2.document_id = docrel.document_id
                    )
                )
            WHERE
                (pi_assessmentdatefrom IS NULL
                 OR trunc(csra.assessment_date) >= trunc(pi_assessmentdatefrom))
                AND (pi_assessmentdateto IS NULL
                     OR trunc(csra.assessment_date) <= trunc(pi_assessmentdateto))
                AND (csra.assessment_type_code = l_assess_sacra_result_type
                     OR csra.assessment_type_code IS NULL)
            ORDER BY
                csra.assessment_date DESC,
                nvl(csra.updated_date, csra.created_date) DESC;

    END ro_getcellsharingdetails;

    PROCEDURE rw_om_cell_sharing_risk_assmnt (
        pi_cell_sharing_risk_assmnt_id IN ioms.om_cell_sharing_risk_assmnt.cell_sharing_risk_assmnt_id%TYPE,
        pi_ntdb_alert_id               IN ioms.om_cell_sharing_risk_assmnt.ntdb_alert_id%TYPE,
        pi_assessment_date             IN ioms.om_cell_sharing_risk_assmnt.assessment_date%TYPE,
        pi_assessment_result_code      IN ioms.om_cell_sharing_risk_assmnt.assessment_result_code%TYPE,
        pi_assessment_comment          IN ioms.om_cell_sharing_risk_assmnt.assessment_comment%TYPE,
        pi_action_by                   IN ioms.om_cell_sharing_risk_assmnt.action_by%TYPE,
        po_cell_sharing_risk_assmnt_id OUT ioms.om_cell_sharing_risk_assmnt.cell_sharing_risk_assmnt_id%TYPE,
        po_result                      OUT NUMBER
    ) AS

        lc_cell_sharing_risk_assmnt_id               ioms.om_cell_sharing_risk_assmnt.cell_sharing_risk_assmnt_id%TYPE := epic.epic_ids.new_epic_id
        ;
        lc_assessment_type_code_prisoner_combination ioms.om_cell_sharing_risk_assmnt.assessment_type_code%TYPE := '7320';
    BEGIN
        po_result := 0;
        IF pi_cell_sharing_risk_assmnt_id IS NOT NULL THEN
            UPDATE  /*+pkg_sacra_result*/ ioms.om_cell_sharing_risk_assmnt
            SET
                cell_sharing_risk_assmnt_id = pi_cell_sharing_risk_assmnt_id,
                ntdb_alert_id = pi_ntdb_alert_id,
                assessment_date = pi_assessment_date,
                assessment_result_code = pi_assessment_result_code,
                assessment_comment = pi_assessment_comment,
                assessment_type_code = lc_assessment_type_code_prisoner_combination,
                updated_by = pi_action_by,
                updated_date = sysdate,
                action_by = pi_action_by,
                action_time = systimestamp,
                action_type = 'U'
            WHERE
                cell_sharing_risk_assmnt_id = pi_cell_sharing_risk_assmnt_id;

            po_result := po_result + SQL%rowcount;
        ELSE
            INSERT
					/*+pkg_sacra_result*/ INTO ioms.om_cell_sharing_risk_assmnt (
                cell_sharing_risk_assmnt_id,
                ntdb_alert_id,
                assessment_date,
                assessment_result_code,
                assessment_comment,
                assessment_type_code,
                created_by,
                created_date,
                action_by,
                action_time,
                action_type
            ) VALUES (
                lc_cell_sharing_risk_assmnt_id,
                pi_ntdb_alert_id,
                pi_assessment_date,
                pi_assessment_result_code,
                pi_assessment_comment,
                lc_assessment_type_code_prisoner_combination,
                pi_action_by,
                sysdate,
                pi_action_by,
                systimestamp,
                'I'
            ) RETURNING cell_sharing_risk_assmnt_id INTO po_cell_sharing_risk_assmnt_id;

            po_result := po_result + SQL%rowcount;
        END IF;

    END rw_om_cell_sharing_risk_assmnt;

    PROCEDURE rw_om_cell_sharing_prisoner (
        pi_cell_sharing_prisoner_id    IN ioms.om_cell_sharing_prisoner.cell_sharing_prisoner_id%TYPE,
        pi_cell_sharing_risk_assmnt_id IN ioms.om_cell_sharing_prisoner.cell_sharing_risk_assmnt_id%TYPE,
        pi_prisoner_id                 IN ioms.om_cell_sharing_prisoner.prisoner_id%TYPE,
        pi_prisoner_order_num          IN ioms.om_cell_sharing_prisoner.prisoner_order_num%TYPE,
        pi_action_by                   IN ioms.om_cell_sharing_prisoner.action_by%TYPE,
        po_cell_sharing_prisoner_id    OUT ioms.om_cell_sharing_prisoner.cell_sharing_prisoner_id%TYPE,
        po_result                      OUT NUMBER
    ) AS
        lc_cell_sharing_prisoner_id          ioms.om_cell_sharing_prisoner.cell_sharing_prisoner_id%TYPE := epic.epic_ids.new_epic_id;
        l_existing_cell_sharing_prisoner_id ioms.om_cell_sharing_prisoner.cell_sharing_prisoner_id%TYPE;
    BEGIN
        po_result := 0;
        po_cell_sharing_prisoner_id := pi_cell_sharing_prisoner_id;

        IF pi_cell_sharing_prisoner_id IS NOT NULL THEN
            UPDATE  /*+pkg_sacra_result*/ ioms.om_cell_sharing_prisoner
            SET
                cell_sharing_risk_assmnt_id = pi_cell_sharing_risk_assmnt_id,
                prisoner_id = pi_prisoner_id,
                prisoner_order_num = pi_prisoner_order_num,
                action_by = pi_action_by,
                action_time = systimestamp,
                action_type = 'U'
            WHERE
                cell_sharing_prisoner_id = pi_cell_sharing_prisoner_id;

            po_result := SQL%rowcount;
            po_cell_sharing_prisoner_id := pi_cell_sharing_prisoner_id;
        ELSE
            /*
              Edit mode can lose CELL_SHARING_PRISONER_ID on the browser model.
              To avoid inserting duplicate children, resolve the row by the natural slot key:
              CELL_SHARING_RISK_ASSMNT_ID + PRISONER_ORDER_NUM.
            */
            BEGIN
                SELECT
                    MIN(cell_sharing_prisoner_id)
                INTO l_existing_cell_sharing_prisoner_id
                FROM
                    ioms.om_cell_sharing_prisoner
                WHERE
                        cell_sharing_risk_assmnt_id = pi_cell_sharing_risk_assmnt_id
                    AND prisoner_order_num = pi_prisoner_order_num;
            EXCEPTION
                WHEN no_data_found THEN
                    l_existing_cell_sharing_prisoner_id := NULL;
            END;

            IF l_existing_cell_sharing_prisoner_id IS NOT NULL THEN
                UPDATE  /*+pkg_sacra_result*/ ioms.om_cell_sharing_prisoner
                SET
                    prisoner_id = pi_prisoner_id,
                    prisoner_order_num = pi_prisoner_order_num,
                    action_by = pi_action_by,
                    action_time = systimestamp,
                    action_type = 'U'
                WHERE
                    cell_sharing_prisoner_id = l_existing_cell_sharing_prisoner_id;

                po_result := SQL%rowcount;
                po_cell_sharing_prisoner_id := l_existing_cell_sharing_prisoner_id;
            ELSE
                INSERT
                        /*+pkg_sacra_result*/ INTO ioms.om_cell_sharing_prisoner (
                    cell_sharing_prisoner_id,
                    cell_sharing_risk_assmnt_id,
                    prisoner_id,
                    prisoner_order_num,
                    action_by,
                    action_time,
                    action_type
                ) VALUES (
                    lc_cell_sharing_prisoner_id,
                    pi_cell_sharing_risk_assmnt_id,
                    pi_prisoner_id,
                    pi_prisoner_order_num,
                    pi_action_by,
                    systimestamp,
                    'I'
                ) RETURNING cell_sharing_prisoner_id INTO po_cell_sharing_prisoner_id;

                po_result := SQL%rowcount;
            END IF;
        END IF;

    END rw_om_cell_sharing_prisoner;

    PROCEDURE rw_delete_om_cell_sharing_prisoner (
        pi_cell_sharing_prisoner_id IN ioms.om_cell_sharing_prisoner.cell_sharing_prisoner_id%TYPE,
        pi_action_by                IN ioms.om_cell_sharing_prisoner.action_by%TYPE,
        po_result                   OUT NUMBER
    ) AS
    BEGIN
        po_result := 0;
        UPDATE  /*+pkg_sacra_result*/ ioms.om_cell_sharing_prisoner
        SET
            action_by = pi_action_by,
            action_time = systimestamp,
            action_type = 'D'
        WHERE
            cell_sharing_prisoner_id = pi_cell_sharing_prisoner_id;

        DELETE /*+pkg_sacra_result*/ FROM ioms.om_cell_sharing_prisoner
        WHERE
            cell_sharing_prisoner_id = pi_cell_sharing_prisoner_id;

        po_result := po_result + SQL%rowcount;
    END rw_delete_om_cell_sharing_prisoner;

    PROCEDURE rw_delete_cell_sharing_prisoners_from_order (
        pi_cell_sharing_risk_assmnt_id IN ioms.om_cell_sharing_prisoner.cell_sharing_risk_assmnt_id%TYPE,
        pi_from_prisoner_order_num     IN ioms.om_cell_sharing_prisoner.prisoner_order_num%TYPE,
        pi_action_by                   IN ioms.om_cell_sharing_prisoner.action_by%TYPE,
        po_result                      OUT NUMBER
    ) AS
    BEGIN
        po_result := 0;

        UPDATE  /*+pkg_sacra_result*/ ioms.om_cell_sharing_prisoner
        SET
            action_by = pi_action_by,
            action_time = systimestamp,
            action_type = 'D'
        WHERE
                cell_sharing_risk_assmnt_id = pi_cell_sharing_risk_assmnt_id
            AND prisoner_order_num >= pi_from_prisoner_order_num;

        DELETE /*+pkg_sacra_result*/ FROM ioms.om_cell_sharing_prisoner
        WHERE
                cell_sharing_risk_assmnt_id = pi_cell_sharing_risk_assmnt_id
            AND prisoner_order_num >= pi_from_prisoner_order_num;

        po_result := SQL%rowcount;
    END rw_delete_cell_sharing_prisoners_from_order;

    PROCEDURE rw_om_doc_function_rel (
        pi_document_id IN ioms.om_doc_function_rel.document_id%TYPE,
        pi_function_id IN ioms.om_doc_function_rel.function_id%TYPE,
        pi_action_by   IN ioms.om_doc_function_rel.action_by%TYPE,
        po_result      OUT NUMBER
    ) AS

        lc_doc_function_rel_id     ioms.om_doc_function_rel.doc_function_rel_id%TYPE := epic.epic_ids.new_epic_id;
        lc_doc_rel_type_code_sacra ioms.om_doc_function_rel.doc_rel_type_code%TYPE := '5024';
    BEGIN
        po_result := 0;
        INSERT
					/*+pkg_sacra_result*/ INTO ioms.om_doc_function_rel (
            doc_function_rel_id,
            document_id,
            doc_rel_type_code,
            function_id,
            action_by,
            action_type,
            action_time
        ) VALUES (
            lc_doc_function_rel_id,
            pi_document_id,
            lc_doc_rel_type_code_sacra,
            pi_function_id,
            pi_action_by,
            'I',
            systimestamp
        );

        po_result := po_result + SQL%rowcount;
    END rw_om_doc_function_rel;
    
    PROCEDURE ro_get_sacra_report_document_info (
    pi_cell_sharing_risk_assmnt_id IN ioms.om_doc_function_rel.function_id%TYPE,
    po_cursor                      OUT iomstypes.rs_out_ref_cursor
) AS
BEGIN
    OPEN po_cursor FOR
        SELECT document_id,
               version_num
        FROM (
            SELECT /*+ pkg_sacra_result */
                   doc.document_id,
                   doc.version_num
            FROM ioms.om_doc_function_rel rel
            INNER JOIN ioms.om_document doc
                ON doc.document_id = rel.document_id
               AND doc.document_type_code = 'SACRA'
               AND doc.version_num = (
                    SELECT MAX(doc2.version_num)
                    FROM ioms.om_document doc2
                    WHERE doc2.document_id = rel.document_id
               )
            WHERE rel.function_id = pi_cell_sharing_risk_assmnt_id
              AND rel.doc_rel_type_code = '5024'
            ORDER BY doc.action_time DESC,
                     doc.version_num DESC
        )
        WHERE ROWNUM = 1;
END ro_get_sacra_report_document_info;

	PROCEDURE ro_getrequestid(
    pi_cell_sharing_assmnt_id IN VARCHAR2,
    po_cursor                 IN OUT iomstypes.rs_out_ref_cursor
)
AS
    l_request_id              NUMBER;
    l_request_ids             VARCHAR2(4000);
    l_roc_unavailable_reason  VARCHAR2(2000);
    l_request_status          CHAR(1);
BEGIN
    l_request_ids := NULL;

    FOR r IN (
        SELECT csp.prisoner_id
          FROM ioms.om_cell_sharing_prisoner csp
         WHERE csp.cell_sharing_risk_assmnt_id = pi_cell_sharing_assmnt_id
         ORDER BY csp.prisoner_order_num
    )
    LOOP
        l_request_id := NULL;

        IOMS.rw_qac_request_bulk(
            r.prisoner_id,
            0,
            l_request_id,
            l_roc_unavailable_reason,
            l_request_status
        );

        IF l_request_id IS NOT NULL THEN
            IF l_request_ids IS NULL THEN
                l_request_ids := TO_CHAR(l_request_id);
            ELSE
                l_request_ids := l_request_ids || ',' || TO_CHAR(l_request_id);
            END IF;
        END IF;
    END LOOP;

    OPEN po_cursor FOR
        SELECT l_request_ids AS REQUEST_IDS
          FROM dual;
END ro_getrequestid;

END pkg_sacra_result;
/