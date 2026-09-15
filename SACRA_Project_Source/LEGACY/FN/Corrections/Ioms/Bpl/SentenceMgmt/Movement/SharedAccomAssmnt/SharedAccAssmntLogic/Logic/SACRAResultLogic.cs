
using Corrections.Ioms.Bpl.Movement.SharedAccomAssmnt.DataAbstration;
using Corrections.Ioms.Bpl.Movement.SharedAccomAssmnt.Entities;
using Corrections.Ioms.Bpl.Movement.SharedAccomAssmnt.SharedAccomAssmntLogic.LogicRule;
using Corrections.Ioms.Bpl.Entities;


namespace Corrections.Ioms.Bpl.Movement.SharedAccomAssmnt.SharedAccomAssmntLogic.Logic
{
    public class SACRAResultLogic : ClassObjectBase
    {
        #region Constructor
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public SACRAResultLogic()
            : base()
        {

        }
        #endregion

        #region FetchSACRAResultDetails
        /// <summary>
        /// Gets the existing SACRA result details for the provided search criteria.
        /// </summary>
        /// <param name="resultSearchParams">Search Criteria</param>
        /// <returns>List of Rows satisfying Search Criteria</returns>
        public EntityBaseCollection<SACRAResultDetails> FetchSACRAResultDetails(SACRAResultSearchParams resultSearchParams)
        {
            if (ValidateEntities(typeof(SACRAResultLogicRule), new EntityBase[] { resultSearchParams }))
            {
                SACRAResultDAL resultDAL = new SACRAResultDAL();
                return SACRAResultDetails.GetSACRAResultList(resultDAL.FetchSACRAResultDetails(resultSearchParams));
            }
            return null;
        }
        /// <summary>
        /// Prepares QAC active charge request ids for Combined SACRA Report.
        /// </summary>
        /// <param name="cellSharingAssessId">Cell sharing risk assessment id.</param>
        /// <returns>Comma-separated QAC request ids.</returns>
        public string PrepareSACRAActiveChargeRequests(string cellSharingAssessId)
        {
            if (string.IsNullOrWhiteSpace(cellSharingAssessId))
            {
                return string.Empty;
            }

            SACRAResultDAL resultDAL = new SACRAResultDAL();
            RecordRowCollection requestIdRows = resultDAL.PrepareSACRAActiveChargeRequests(cellSharingAssessId);

            if (requestIdRows == null || requestIdRows.Count == 0)
            {
                return string.Empty;
            }

            EntityBaseCollection<SACRAQACRequestResult> requestResults = SACRAQACRequestResult.GetSACRAQACRequestResultList(requestIdRows);

            if (requestResults == null || requestResults.Count == 0)
            {
                return string.Empty;
            }
            return requestResults[0].RequestIds;
        }

        #endregion

        #region AddSACRAResultDetails
        /// <summary>
        /// Create the new SACRA result record.
        /// </summary>
        /// <param name="resultDetails">Entity Containing Data to Create a new record. </param>
        /// <returns>Entity having newly created record</returns>
        public SACRAResultDetails AddSACRAResultDetails(SACRAResultDetails resultDetails)
        {
            if (ValidateEntities(typeof(SACRAResultLogicRule), true, new EntityBase[] { resultDetails }))
            {
                SACRAResultDAL resultDAL = new SACRAResultDAL();
                return resultDAL.AddSACRAResultDetails(resultDetails);
            }
            return null;
        }
        #endregion

        #region UpdateSACRAResultDetails
        /// <summary>
        /// Create the new SACRA result record.
        /// </summary>
        /// <param name="resultDetails">Entity Containing Data to Create a new record. </param>
        /// <returns>Entity having newly created record</returns>
        public SACRAResultDetails UpdateSACRAResultDetails(SACRAResultDetails resultDetails)
        {
            if (ValidateEntities(typeof(SACRAResultLogicRule), true, new EntityBase[] { resultDetails }))
            {
                SACRAResultDAL resultDAL = new SACRAResultDAL();
                return resultDAL.UpdateSACRAResultDetails(resultDetails);
            }
            return null;
        }
        #endregion

        #region Individual SACRA NTB Changes #57727

        /// <summary>
        /// Fetch Individual SACRA details.
        /// </summary>
        /// <param name="offenderId">offenderId</param>
        /// <returns>Return Individual SACRA list.</returns>
        public EntityBaseCollection<SACRAResultDetails> GetIndividualSACRADetails(string offenderId)
        {
            SACRAResultDAL resultDAL = new SACRAResultDAL();
            return SACRAResultDetails.GetSACRAResultList(resultDAL.GetIndividualSACRADetails(offenderId));
        }

        /// <summary>
        /// Validate Individual SACRA details.
        /// </summary>
        /// <param name="offenderId">offenderId</param>
        /// <returns>Return Y or N.</returns>
        public RecordRowCollection ValidateIndividualSACRA(string offenderId, string sacraTypeCode, string sacraResultCode)
        {
            SACRAResultDAL resultDAL = new SACRAResultDAL();
            return resultDAL.ValidateIndividualSACRA(offenderId, sacraTypeCode, sacraResultCode);
        }

        /// <summary>
        /// Creates a new Individual SACRA Record.
        /// </summary>
        /// <param name="individualSacraDetails">Entity Having Record Information</param>
        /// <returns>Entity Having Saved Record Information</returns>
        public SACRAResultDetails AddIndividualSACRA(SACRAResultDetails individualSacraDetails)
        {
            SACRAResultDAL resultDAL = new SACRAResultDAL();
            return resultDAL.AddIndividualSACRA(individualSacraDetails);
        }
        /// <summary>
        /// Creates a new SACRA document
        /// </summary>
        /// <param name="resultDetails"></param>
        /// <returns></returns>
        public SACRAResultDetails SaveSACRAReportDocument(SACRAResultDetails resultDetails)
        {
            SACRAResultDAL resultDAL = new SACRAResultDAL();
            return resultDAL.SaveSACRAReportDocument(resultDetails);
        }
        /// <summary>
        /// To get SACRA document infos
        /// </summary>
        /// <param name="cellSharingAssessId"></param>
        /// <returns></returns>
        public SACRAResultDetails GetSACRAReportDocumentInfo(string cellSharingAssessId)
        {
            SACRAResultDAL resultDAL = new SACRAResultDAL();
            return resultDAL.GetSACRAReportDocumentInfo(cellSharingAssessId);
        }

        #endregion
    }
}