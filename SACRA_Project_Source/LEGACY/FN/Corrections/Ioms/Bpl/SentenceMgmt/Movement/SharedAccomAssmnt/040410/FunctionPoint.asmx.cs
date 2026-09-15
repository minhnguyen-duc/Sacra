
using System.ServiceModel;
using Corrections.Ioms.Bpl.Movement.SharedAccomAssmnt.Entities;
using Corrections.Ioms.Bpl.Movement.SharedAccomAssmnt.SharedAccomAssmntLogic.Logic;
using Corrections.Ioms.Bpl.Services.Web;
using Corrections.Ioms.Bpl.Entities;
using Corrections.Ioms.Bpl.Services.Extension;

namespace Corrections.Ioms.Bpl.Movement.SharedAccomAssmnt.Services.Web
{
    /// <summary>
    /// Summary description for FunctionPoint
    /// </summary>
    [ServiceBehavior(ConfigurationName = "FunctionPoint",
       InstanceContextMode = InstanceContextMode.PerCall,
       IncludeExceptionDetailInFaults = true)]
    [ServiceRequestBehaviour()]

    public class FunctionPoint : ComponentWebServiceBase, ISharedAccomAssmnt,IComponentWebServiceBase
    {
        #region IComponentWebServiceBase Members
        [OperationBehavior(TransactionScopeRequired = true, Impersonation = ImpersonationOption.NotAllowed)]
        ComponentResponse IComponentWebServiceBase.ExecuteAction(string processAction, Corrections.Ioms.Bpl.Entities.ParameterCollection Sysdata)
        {
            return base.ExecuteAction(processAction, Sysdata);
        }
        [OperationBehavior(TransactionScopeRequired = true, Impersonation = ImpersonationOption.NotAllowed)]
        ComponentResponse IComponentWebServiceBase.ExecuteParamAction(string processAction, Corrections.Ioms.Bpl.Entities.ParameterCollection Sysdata, Corrections.Ioms.Bpl.Entities.ParameterCollection parameters)
        {
            return base.ExecuteParamAction(processAction, Sysdata, parameters);
        }
        [OperationBehavior(TransactionScopeRequired = true, Impersonation = ImpersonationOption.NotAllowed)]
        ComponentResponse IComponentWebServiceBase.ExecuteRecordAction(string processAction, Corrections.Ioms.Bpl.Entities.ParameterCollection Sysdata, Corrections.Ioms.Bpl.Entities.RecordRowCollection records)
        {
            return base.ExecuteRecordAction(processAction, Sysdata, records);
        }

        #endregion

        public FunctionPoint()
        {
            //CODEGEN: This call is required by the ASP.NET Web Services Designer
            InitializeComponent();
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion

        #region ISharedAccomAssmnt Members

        /// <summary>
        /// Fetch SACRA Result details using the search criteria.
        /// </summary>
        /// <param name="searchParams">Search Criteria</param>
        /// <returns>Record Rows Satisfying the result.</returns>
        [FunctionPoint("Search.FetchSACRAResultDetails")]
        [OperationBehavior(Impersonation=ImpersonationOption.NotAllowed)]
        public EntityBaseCollection<SACRAResultDetails> FetchSACRAResultDetails(SACRAResultSearchParams searchParams)
        {
            SACRAResultLogic resultLogic = new SACRAResultLogic();
            return resultLogic.FetchSACRAResultDetails(searchParams);
        }
        /// <summary>
        ///  Get lists of request id
        /// </summary>
        /// <param name="cellSharingAssessId">Search Criteria</param>
        /// <returns>Record Rows Satisfying the result.</returns>
        [OperationBehavior(Impersonation = ImpersonationOption.NotAllowed)]
        public string PrepareSACRAActiveChargeRequests(string cellSharingAssessId)
        {
            SACRAResultLogic resultLogic = new SACRAResultLogic();
            return resultLogic.PrepareSACRAActiveChargeRequests(cellSharingAssessId);
        }

        /// <summary>
        /// Creates a new SACRA Result Record.
        /// </summary>
        /// <param name="resultDetails">Entity Having Record Information</param>
        /// <returns>Entity Having Saved Record Information</returns>
        [FunctionPoint("UPDATE.ADD.ADDSACRARESULTDETAILS")]
        [OperationBehavior(TransactionScopeRequired = true, Impersonation = ImpersonationOption.NotAllowed)]
        public SACRAResultDetails AddSACRAResultDetails(SACRAResultDetails resultDetails)
        {
            SACRAResultLogic resultLogic = new SACRAResultLogic();
            return resultLogic.AddSACRAResultDetails(resultDetails);
        }

        /// <summary>
        /// Updates the existing SACRA result record details.
        /// </summary>
        /// <param name="resultDetails">Entity Having Record Information</param>
        /// <returns>Entity Having Saved Record Information</returns>
        [FunctionPoint("UPDATE.EDIT.UPDATESACRARESULTDETAILS")]
        [OperationBehavior(TransactionScopeRequired = true, Impersonation = ImpersonationOption.NotAllowed)]
        public SACRAResultDetails UpdateSACRAResultDetails(SACRAResultDetails resultDetails)
        {
            SACRAResultLogic resultLogic = new SACRAResultLogic();
            return resultLogic.UpdateSACRAResultDetails(resultDetails);
        }

        #region Individual SACRA NTDB changes #57727

        /// <summary>
        /// Fetch Individual SACRA details.
        /// </summary>
        /// <param name="offenderId">offenderId</param>
        /// <returns>Return Individual SACRA list.</returns>
        [OperationBehavior(Impersonation = ImpersonationOption.NotAllowed)]
        public EntityBaseCollection<SACRAResultDetails> GetIndividualSACRADetails(string offenderId)
        {
            SACRAResultLogic resultLogic = new SACRAResultLogic();
            return resultLogic.GetIndividualSACRADetails(offenderId);
        }

        /// <summary>
        /// Validate Individual SACRA details.
        /// </summary>
        /// <param name="offenderId">offenderId</param>
        /// <returns>Return Y or N.</returns>
        [OperationBehavior(Impersonation = ImpersonationOption.NotAllowed)]
        public RecordRowCollection ValidateIndividualSACRA(string offenderId, string sacraTypeCode, string sacraResultCode)
        {
            SACRAResultLogic resultLogic = new SACRAResultLogic();
            return resultLogic.ValidateIndividualSACRA(offenderId, sacraTypeCode, sacraResultCode);
        }

        /// <summary>
        /// Creates a new Individual SACRA Record.
        /// </summary>
        /// <param name="individualSacraDetails">Entity Having Record Information</param>
        /// <returns>Entity Having Saved Record Information</returns>
        [FunctionPoint("UPDATE.SAVE.INDIVIDUALSACRA")]
        [OperationBehavior(TransactionScopeRequired = true, Impersonation = ImpersonationOption.NotAllowed)]
        public SACRAResultDetails AddIndividualSACRA(SACRAResultDetails individualSacraDetails)
        {
            SACRAResultLogic resultLogic = new SACRAResultLogic();
            return resultLogic.AddIndividualSACRA(individualSacraDetails);
        }
        [OperationBehavior(TransactionScopeRequired = true, Impersonation = ImpersonationOption.NotAllowed)]
        /// <summary>
        /// Creates a new document
        /// </summary>
        /// <param name="resultDetails">Entity Having Record Information</param>
        /// <returns>Entity Having Saved Record Information</returns>
        public SACRAResultDetails SaveSACRAReportDocument(SACRAResultDetails resultDetails)
        {
            SACRAResultLogic resultLogic = new SACRAResultLogic();
            return resultLogic.SaveSACRAReportDocument(resultDetails);
        }
        /// <summary>
        /// To get document infos
        /// </summary>
        /// <param name="cellSharingAssessId"></param>
        /// <returns></returns>
        public SACRAResultDetails GetSACRAReportDocumentInfo(string cellSharingAssessId)
        {
            SACRAResultLogic resultLogic = new SACRAResultLogic();
            return resultLogic.GetSACRAReportDocumentInfo(cellSharingAssessId);
        }


        #endregion

        #endregion
    }
}
