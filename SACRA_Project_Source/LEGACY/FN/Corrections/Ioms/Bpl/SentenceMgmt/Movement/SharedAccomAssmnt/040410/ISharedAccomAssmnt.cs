using System.ServiceModel;
using Corrections.Ioms.Bpl.Movement.SharedAccomAssmnt.Entities;
using Corrections.Ioms.Bpl.Entities;

namespace Corrections.Ioms.Bpl.Movement.SharedAccomAssmnt.Services.Web
{
    [ServiceContract(Namespace = "http://Corrections.Ioms.Bpl/", Name = "ISharedAccomAssmnt")]
    interface ISharedAccomAssmnt
    {
        [OperationContract(), TransactionFlow(TransactionFlowOption.Allowed)]
        EntityBaseCollection<SACRAResultDetails> FetchSACRAResultDetails(SACRAResultSearchParams searchParams);
        [OperationContract(), TransactionFlow(TransactionFlowOption.Allowed)]
        SACRAResultDetails AddSACRAResultDetails(SACRAResultDetails resultDetails);
        [OperationContract(), TransactionFlow(TransactionFlowOption.Allowed)]
        SACRAResultDetails UpdateSACRAResultDetails(SACRAResultDetails resultDetails);
        /* Individual SACRA NTDB changes #57727 - Starts*/
        [OperationContract(), TransactionFlow(TransactionFlowOption.Allowed)]
        SACRAResultDetails AddIndividualSACRA(SACRAResultDetails individualSacraDetails);
        [OperationContract(), TransactionFlow(TransactionFlowOption.Allowed)]
        EntityBaseCollection<SACRAResultDetails> GetIndividualSACRADetails(string offenderId);
        [OperationContract(), TransactionFlow(TransactionFlowOption.Allowed)]
        RecordRowCollection ValidateIndividualSACRA(string offenderId, string sacraTypeCode, string sacraResultCode);

        [OperationContract(), TransactionFlow(TransactionFlowOption.Allowed)]
        string PrepareSACRAActiveChargeRequests(string cellSharingAssessId);
        [OperationContract(), TransactionFlow(TransactionFlowOption.Allowed)]
        SACRAResultDetails SaveSACRAReportDocument(SACRAResultDetails resultDetails);
        [OperationContract(), TransactionFlow(TransactionFlowOption.Allowed)]
        SACRAResultDetails GetSACRAReportDocumentInfo(string cellSharingAssessId);
        /* Individual SACRA NTDB changes #57727 - Ends*/
    }
}
