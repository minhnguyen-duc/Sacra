
using System;
using System.Collections;
using System.Collections.Generic;
using Corrections.Ioms.Bpl.DocumentEngine;
using Corrections.Ioms.Bpl.Entities;
using Corrections.Ioms.Bpl.Movement.SharedAccomAssmnt.Entities;
using Corrections.Ioms.Bpl.Services.Data.Oracle;
using Oracle.ManagedDataAccess.Client;

namespace Corrections.Ioms.Bpl.Movement.SharedAccomAssmnt.DataAbstration
{
    public class SACRAResultDAL : ClassObjectBase
    {
        private OracleHelper _oracleHelper = new OracleHelper();
        private const int FIRST_PRISONER_ORDER_NUMBER = 0;
        private const int SECOND_PRISONER_ORDER_NUMBER = 1;
        private const string PKG_INDIVIDUAL_SACRA_DETAIL = "ioms.pkg_individual_sacra.ro_get_individual_sacra_dtl";
        private const string PKG_INDIVIDUAL_SACRA_VALIDATE = "ioms.pkg_individual_sacra.ro_get_sacra_exists_by_type";
        private const string PI_OFFENDER_ID = "pi_offenderId";
        private const string PI_ASSESSMENT_TYPE_CODE = "pi_assessment_type_code";
        private const string PI_ASSESSMENT_RESULT_CODE = "pi_assessment_result_code";

        #region SACRAResultDAL
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public SACRAResultDAL()
            : base()
        { }
        #endregion

        #region Public Methods

        #region FetchSACRAResultDetails
        /// <summary>
        /// Method used to fetch the SACRA result records satisfying the search criteria.
        /// </summary>
        /// <param name="resultSearchParams">Search Criteria</param>
        /// <returns>Result Records Satisfying Search Criteria</returns>
        public RecordRowCollection FetchSACRAResultDetails(SACRAResultSearchParams resultSearchParams)
        {
            RecordRowCollection resultCollection = null;
            try
            {
                StoredProcCommand storedProcCommand = new StoredProcCommand("IOMS.pkg_sacra_result.ro_GetCellSharingDetails");
                storedProcCommand.AddParameter("pi_entityId", resultSearchParams.PrisonerId, Direction.Input);
                if (resultSearchParams.AssessmentDateFrom != DateTime.MinValue)
                {
                    storedProcCommand.AddParameter("pi_assessmentDateFrom", resultSearchParams.AssessmentDateFrom, Direction.Input);
                }
                else
                {
                    storedProcCommand.AddParameter("pi_assessmentDateFrom", DBNull.Value, Direction.Input);
                }
                if (resultSearchParams.AssessmentDateTo != DateTime.MinValue)
                {
                    storedProcCommand.AddParameter("pi_assessmentDateTo", resultSearchParams.AssessmentDateTo, Direction.Input);
                }
                else
                {
                    storedProcCommand.AddParameter("pi_assessmentDateTo", DBNull.Value, Direction.Input);
                }
                resultCollection = _oracleHelper.ExecuteSearchProcs(storedProcCommand);
            }
            catch (Exception ex)
            {
                RaiseErrorMessage(string.Format("Failed to execute routine FetchSACRAResultDetails. {0}", ex.Message));
                return null;
            }
            return resultCollection;
        }

        #endregion
        /// <summary>
        /// To get document info
        /// </summary>
        /// <param name="cellSharingAssessId"></param>
        /// <returns></returns>
        public SACRAResultDetails GetSACRAReportDocumentInfo(string cellSharingAssessId)
        {
            if (string.IsNullOrWhiteSpace(cellSharingAssessId))
            {
                return null;
            }

            try
            {
                StoredProcCommand storedProcCommand = new StoredProcCommand("IOMS.pkg_sacra_result.ro_get_sacra_report_document_info");

                storedProcCommand.AddParameter("pi_cell_sharing_risk_assmnt_id",cellSharingAssessId, Direction.Input);

                RecordRowCollection resultCollection =_oracleHelper.ExecuteSearchProcs(storedProcCommand);

                if (resultCollection != null && resultCollection.Count > 0)
                {
                    return new SACRAResultDetails(resultCollection[0]);
                }
            }
            catch (Exception ex)
            {
                RaiseErrorMessage(string.Format("Failed to execute routine GetSACRAReportDocumentInfo. {0}", ex.Message));
            }

            return null;
        }



        #region AddSACRAResultDetails
        /// <summary>
        /// Method used to create a new record for the SACRA result.
        /// </summary>
        /// <param name="resultDetails">Entity containing the data</param>
        /// <returns>Entity having details of created record</returns>
        public SACRAResultDetails AddSACRAResultDetails(SACRAResultDetails resultDetails)
        {
            if (resultDetails == null)
            {
                return null;
            }

            // One record is inserted into OM_CELL_SHARING_RISK_ASSMNT.
            if (CellSharingRiskAssmntCUD.AddSACRAResult(resultDetails) != null)
            {
                bool isCellSharingPrisonersSaved = SaveCellSharingPrisoners(resultDetails, true);
                if (!isCellSharingPrisonersSaved)
                {
                    return null;
                }
               
                resultDetails.UserActionForPrisoner = UserActionForPrisoner.None;

                if (resultDetails.IsAutoPopulateSacraReport)
                {
                    resultDetails = SaveSACRADocument(resultDetails);
                }
    
            }
            return resultDetails;
        }
        #endregion

        #region UpdateSACRAResultDetails
        /// <summary>
        /// Method used to update the existing SACRA result record.
        /// </summary>
        /// <param name="resultDetails">Entity containing the data</param>
        /// <returns>Entity having details of updated record</returns>
        public SACRAResultDetails UpdateSACRAResultDetails(SACRAResultDetails resultDetails)
        {
            if (resultDetails == null)
            {
                return null;
            }

            // A user can update prisoner group and assessment metadata in one save.
            if (resultDetails.UserActionForOtherDetails == UserActionForOtherDetails.OtherDetailsUpdated)
            {
                CellSharingRiskAssmntCUD.UpdateSACRAResult(resultDetails);
                resultDetails.UserActionForOtherDetails = UserActionForOtherDetails.OtherDetailsNotUpdated;
            }
            bool isCellSharingPrisonersSaved = SaveCellSharingPrisoners(resultDetails, false);
            if (!isCellSharingPrisonersSaved)
            {
                return null;
            }
            resultDetails.UserActionForPrisoner = UserActionForPrisoner.None;

            if (resultDetails.IsAutoPopulateSacraReport)
            {
                resultDetails = SaveSACRADocument(resultDetails);
            }

            return resultDetails;
        }
        #endregion


        #region Save SACRA Prisoners
        /// <summary>
        /// Saves the selected A..J prisoners as one compact, sequential prisoner group.
        /// </summary>
        /// <param name="resultDetails">SACRA result details.</param>
        /// <param name="isNewAssessment">True when the parent SACRA assessment has just been created.</param>
        /// <returns>True when all selected prisoners are saved successfully.</returns>
        private bool SaveCellSharingPrisoners(SACRAResultDetails resultDetails, bool isNewAssessment)
        {
            IList<SACRAPrisonerSlot> selectedPrisoners = resultDetails.GetSelectedPrisonerSlots();
            for (int index = 0; index < selectedPrisoners.Count; index++)
            {
                SACRAPrisonerSlot prisonerSlot = selectedPrisoners[index];
                short prisonerOrderNumber = Convert.ToInt16(index);
                prisonerSlot.CellSharingPrisonerId = CellSharingPrisonerCUD.SavePrisoner(resultDetails,string.Empty,prisonerSlot.PrisonerId,prisonerOrderNumber);

                if (string.IsNullOrWhiteSpace(prisonerSlot.CellSharingPrisonerId))
                {
                    return false;
                }
            }
            if (!isNewAssessment)
            {
                bool isExtraPrisonersDeleted = DeleteExtraPrisoners(resultDetails, selectedPrisoners.Count);

                if (!isExtraPrisonersDeleted)
                {
                    return false;
                }
            }
            resultDetails.ApplyPrisonerSlots(selectedPrisoners);
            return true;
        }

       

        /// <summary>
        /// Deletes old prisoner rows when an existing assessment is saved with fewer selected prisoners.
       
        /// </summary>
        private static bool DeleteExtraPrisoners(SACRAResultDetails resultDetails, int selectedPrisonerCount)
        {
            return CellSharingPrisonerCUD.DeletePrisonersFromOrder(resultDetails, Convert.ToInt16(selectedPrisonerCount));
        }
        #endregion
        #endregion

        #region Individual SACRA NTB Changes #57727

        /// <summary>
        /// Fetch Individual SACRA details.
        /// </summary>
        /// <param name="offenderId">offenderId</param>
        /// <returns>Return Individual SACRA list.</returns>
        public RecordRowCollection GetIndividualSACRADetails(string offenderId)
        {
            RecordRowCollection resultCollection = null;
            try
            {
                StoredProcCommand storedProcCommand = new StoredProcCommand(PKG_INDIVIDUAL_SACRA_DETAIL);
                storedProcCommand.AddParameter(PI_OFFENDER_ID, offenderId, Direction.Input);
                resultCollection = _oracleHelper.ExecuteSearchProcs(storedProcCommand);
            }
            catch (Exception ex)
            {
                RaiseErrorMessage(string.Format("Failed to execute routine GetIndividualSACRADetails. {0}", ex.Message));
                return null;
            }
            return resultCollection;
        }

        /// <summary>
        /// Validate Individual SACRA details.
        /// </summary>
        /// <param name="offenderId">offenderId</param>
        /// <returns>Return Y or N.</returns>
        public RecordRowCollection ValidateIndividualSACRA(string offenderId, string sacraTypeCode, string sacraResultCode)
        {
            RecordRowCollection rrc = null;
            try
            {
                StoredProcCommand storedProcCommand = new StoredProcCommand(PKG_INDIVIDUAL_SACRA_VALIDATE);
                storedProcCommand.AddParameter(PI_OFFENDER_ID, offenderId, Direction.Input);
                storedProcCommand.AddParameter(PI_ASSESSMENT_TYPE_CODE, sacraTypeCode, Direction.Input);
                storedProcCommand.AddParameter(PI_ASSESSMENT_RESULT_CODE, sacraResultCode, Direction.Input);
                rrc = _oracleHelper.ExecuteSearchProcs(storedProcCommand);
                return rrc;
            }
            catch (Exception ex)
            {
                RaiseErrorMessage(string.Format("Failed to execute routine ValidateIndividualSACRA. {0}", ex.Message));
                return null;
            }
        }

        /// <summary>
        /// Creates a new Individual SACRA Record.
        /// </summary>
        /// <param name="individualSacraDetails">Entity Having Record Information</param>
        /// <returns>Entity Having Saved Record Information</returns>
        public SACRAResultDetails AddIndividualSACRA(SACRAResultDetails individualSacraDetails)
        {
            return CellSharingRiskAssmntCUD.AddIndividualSACRA(individualSacraDetails);
        }

        #endregion

        #region Save Sacra Report

        /// <summary>
        /// Save Document and document relationship
        /// </summary>
        /// <param name="resultDetails">holds the details to save documents</param>
        private SACRAResultDetails SaveSACRADocument(SACRAResultDetails resultDetails)
        {
            DocumentHelper _docHelper = new DocumentHelper();
            IomsDocument document = new IomsDocument
            {
                DocumentType = "SACRA",
                DocumentPDF = resultDetails.DocumentPDF,
                DocumentId = resultDetails.DocumentId,
                Status = DocumentStatus.Final,
                CreatedBy = base.GetSysData(SysDataProps.UserIntId).ToString(),
                CreateDate = DateTime.Now,
                DocumentVersion = resultDetails.DocumentVersion
            };

            IomsDocument newDocument = _docHelper.SaveDocument(document);
            document.DocumentId = newDocument.DocumentId;
            document.DocumentVersion = newDocument.DocumentVersion;

            // tie the document to the request - this will be created the first time a document is created, 
            // so if document_id isn't null this record is already there
            if (string.IsNullOrWhiteSpace(resultDetails.DocumentId))
            {
                DocFunctionRelCUD.Add(document.DocumentId, resultDetails.ApplicationId, int.Parse(document.GetSysData(SysDataProps.UserIntId).ToString()));
            }
            resultDetails.DocumentId = !string.IsNullOrWhiteSpace(document.DocumentId) ? document.DocumentId : string.Empty;
            resultDetails.DocumentVersion = document.DocumentVersion;

            return resultDetails;
        }
        /// <summary>
        /// Prepares QAC active charge request ids for Combined SACRA Report.
        /// </summary>
        /// <param name="cellSharingAssessId">Cell sharing risk assessment id.</param>
        /// <returns>Comma-separated QAC request ids.</returns>
        public RecordRowCollection PrepareSACRAActiveChargeRequests(string cellSharingAssessId)
        {
            RecordRowCollection resultCollection = null;

            try
            {
                StoredProcCommand storedProcCommand = new StoredProcCommand("ioms.pkg_sacra_result.ro_getrequestid");

                storedProcCommand.AddParameter("pi_cell_sharing_assmnt_id", cellSharingAssessId, Direction.Input);

                resultCollection = _oracleHelper.ExecuteSearchProcs(storedProcCommand);
            }
            catch (Exception ex)
            {
                RaiseErrorMessage(string.Format("Failed to execute routine PrepareSACRAActiveChargeRequests. {0}", ex.Message));
                return null;
            }

            return resultCollection;
        }
        /// <summary>
        /// Create document
        /// </summary>
        /// <param name="resultDetails">Cell sharing risk assessment id.</param>
        /// <returns>Comma-separated QAC request ids.</returns>
        public SACRAResultDetails SaveSACRAReportDocument(SACRAResultDetails resultDetails)
        {
            if (string.IsNullOrWhiteSpace(resultDetails.ApplicationId) || string.IsNullOrWhiteSpace(resultDetails.DocumentPDF))
            {
                return null;
            }
            SACRAResultDetails existingDocument = GetSACRAReportDocumentInfo(resultDetails.ApplicationId);

            if (existingDocument != null && !string.IsNullOrWhiteSpace(existingDocument.DocumentId))
            {
                resultDetails.DocumentId = existingDocument.DocumentId;
                resultDetails.DocumentVersion = existingDocument.DocumentVersion;
            }
            return SaveSACRADocument(resultDetails);
        }
    }


    #endregion
   

}