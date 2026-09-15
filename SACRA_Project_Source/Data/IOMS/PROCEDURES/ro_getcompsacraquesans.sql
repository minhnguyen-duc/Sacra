CREATE OR REPLACE PROCEDURE ro_getcompsacraquesans (
    pi_offender_a IN VARCHAR2,
    pi_offender_b IN VARCHAR2,
    po_cursor     IN OUT iomstypes.rs_out_ref_cursor
)
 AS
BEGIN
    OPEN po_cursor FOR 
        WITH off_data AS (
          /* NARA document questtion and answers */
            SELECT DISTINCT
                prique.prisoner_id,
                prique.nara_question_name_code AS ques_code,
                prique.nara_question_desc      AS ques_desc,
                docans.nara_answer_desc        AS answer_desc,
                prique.display_order           AS display_order
            FROM
                (
                    SELECT DISTINCT
                        ocss.prisoner_id,
                        ocss.cell_sharing_snapshot_id,
                        ques.nara_question_name_code,
                        ques.doc_nara_question_id,
                        ques.nara_question_desc,
                        ques.display_order
                    FROM
                        ioms.om_ref_doc_nara_question ques,
                        ioms.om_cell_sharing_snapshot ocss
                    WHERE
                        ques.nara_question_name_code IN ( '7274', '7275', '7279', '7282' )
                        AND ocss.prisoner_id IN ( pi_offender_a, pi_offender_b )
                ) prique
                LEFT JOIN (
                    SELECT DISTINCT
                        doc.cell_sharing_snapshot_id,
                        to_char(ans.created_date, 'dd/MM/yyyy')
                        || ' '
                        || DECODE(ans.nara_answer_flag, '0', 'No', 'Yes')
                        || DECODE(ans.nara_answer_desc, 'NA', '', ' - ' || ans.nara_answer_desc) AS nara_answer_desc,
                        ans.doc_nara_question_id
                    FROM
                             om_cell_sharing_nara_doc doc
                        INNER JOIN ioms.om_doc_nara_answer ans ON doc.doc_nara_answer_id = ans.doc_nara_answer_id
                ) docans ON prique.cell_sharing_snapshot_id = docans.cell_sharing_snapshot_id
                            AND prique.doc_nara_question_id = docans.doc_nara_question_id
            WHERE
                prique.prisoner_id NOT IN (
                    SELECT
                        css.prisoner_id
                    FROM
                             ioms.om_cell_sharing_snapshot css
                        INNER JOIN ioms.om_cell_sharing_at_risk  csar ON csar.cell_sharing_snapshot_id = css.cell_sharing_snapshot_id
                        INNER JOIN ioms.om_ar_at_risk_assessment recp ON css.prisoner_id = recp.prisoner_id
                                                                         AND recp.prisoner_id IN ( pi_offender_a, pi_offender_b )
                                                                         AND document_type_code IN ( 'CDNF.NARC', 'NARC' )
                )
            UNION
        
            /* AtRisk Reception Question Answers */
        
            SELECT DISTINCT
                css.prisoner_id     AS prisoner_id,
                look1.lookup_code   AS ques_code,
                look1.lookup_desc   AS ques_desc,
                to_char(qa.action_time, 'dd/MM/yyyy')
                || ' '
                || DECODE(qa.answer_code, '1000', 'Yes', 'No')
                || ' - '
                || qa.answer_desc   AS answer_desc,
                look1.display_order AS display_order
            FROM
                     ioms.om_cell_sharing_snapshot css
                INNER JOIN ioms.om_cell_sharing_at_risk    csar ON csar.cell_sharing_snapshot_id = css.cell_sharing_snapshot_id
                LEFT OUTER JOIN ioms.om_ar_question_answer      qa ON qa.ar_question_answer_id = csar.ar_question_answer_id
                LEFT OUTER JOIN ioms.om_ar_section              sec ON sec.ar_section_id = qa.ar_section_id
                LEFT OUTER JOIN ioms.om_ref_lookup_relationship rel1 ON rel1.parent_lookup_code = '1010'
                                                                        AND rel1.parent_lookup_class = 'ARSECTION'
                                                                        AND rel1.child_lookup_class = 'ARPRISONERQUESTION'
                                                                        AND rel1.child_lookup_code IN ( '1000', '1070' )
                                                                        AND rel1.parent_lookup_code = '1010'
                INNER JOIN ioms.om_ref_lookup              look1 ON qa.question_code = look1.lookup_code
                                                       AND qa.question_class_name = look1.lookup_class
                                                       AND look1.lookup_class = 'ARPRISONERQUESTION'
                                                       AND look1.lookup_code = rel1.child_lookup_code
                INNER JOIN ioms.om_ar_at_risk_assessment   recp ON sec.ar_at_risk_assessment_id = recp.ar_at_risk_assessment_id
                                                                 AND sec.section_code = '1010'
                                                                 AND recp.prisoner_id IN ( pi_offender_a, pi_offender_b )
                                                                 AND document_type_code IN ( 'CDNF.NARC', 'NARC' )
            ORDER BY
                display_order
        )
        SELECT DISTINCT
            d1.prisoner_id   prisoner_id_a,
            d1.ques_code     ques_code_a,
            d1.ques_desc     ques_desc_a,
            d1.answer_desc   answer_desc_a,
            d1.display_order display_order_a,
            d2.prisoner_id   prisoner_id_b,
            d2.ques_code     ques_code_b,
            d2.ques_desc     ques_desc_b,
            d2.answer_desc   answer_desc_b,
            d2.display_order display_order_b
        FROM
            off_data d1  
			LEFT JOIN off_data d2
            ON d2.prisoner_id = pi_offender_b
            AND d2.ques_code = d1.ques_code
            WHERE d1.prisoner_id = pi_offender_a
            ORDER BY d1.display_order;

END ro_getcompsacraquesans;
/