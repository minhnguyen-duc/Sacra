#region Header

#endregion Header

#region Using Namespace

using Corrections.Ioms.Bpl.Entities;
using Corrections.Ioms.Bpl.Movement.SharedAccomAssmnt.Entities;
using Corrections.Ioms.Common.Core.Helpers;
using Corrections.Ioms.Services.Proxies.Service040410;
using Corrections.Ioms.Services.ServiceProxy;
using Corrections.Ioms.UI.Helpers;
using Corrections.Ioms.UI.Helpers.BreadCrumb;
using Corrections.Ioms.UI.Helpers.Constants;
using Corrections.Ioms.UI.Helpers.Security;
using Corrections.Ioms.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

#endregion

namespace Corrections.Ioms.UI.Controllers.RiskAssessment
{
    /// <summary>
    /// The RiskAssessmentSACRAResultsController class handle the Create, Update, Search Sacra Result Details Operations
    /// </summary>
    public class RiskAssessmentSACRAResultsController : BaseUIMController
    {
        #region Fields

        private const string SACRARESULTSRESOURCECLASSNAME = "Corrections.Ioms.UI.IOMSResources.RiskAssessment.SacraResult";
        private const string NO = "N";
        private const string INDIVIDUAL_SACRA_EXIST = "INDIVIDUAL_SACRA_EXIST";
        private const string SACRA_RESULT_LOOKUP = "7320";
        private const string NO_DATA_FOUND = "Z";

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Constructor For Unit Testing
        /// </summary>
        /// <param name="lHelper">Used to mock the lookup helper interface</param>
        /// <param name="sessionMgr">Used to mock the session manager interface</param>
        /// <param name="masterMgr">Used to mock the master manager interface</param>
        /// <param name="proxyHelp">Used to mock the client proxy interface</param>
        /// <param name="securityInfo">Used to mock the security interface</param>
        public RiskAssessmentSACRAResultsController(ILookupHelper lHelper, ISessionManager sessionMgr, IMasterManager masterMgr, IIOMSClientProxy proxyHelp, ISecurityInfo securityInfo)
            : base(lHelper, sessionMgr, masterMgr, proxyHelp, securityInfo)
        {
        }

        /// <summary>
        ///  Zero Parameter Constructor 
        /// </summary>
        public RiskAssessmentSACRAResultsController()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Fetch Sacra Result Details
        /// </summary>
        /// <param name="sacraResultSearchParams">a SacraResultSearchParams entity to fetch SACRA result details</param>
        /// <returns>methodResponseData contains SACRA result details</returns>
        [HttpPost]
        public JsonResult GetResult(SACRAResultSearchParams sacraResultSearchParams)
        {
            string serviceErrorMsg = string.Empty;

            if (sacraResultSearchParams != null)
            {
                IOMSSessionManager.SetCacheValueMultiTab(SessionConstants.SACRARESULT_FILTER, sacraResultSearchParams);
            }
            else
            {
                IOMSSessionManager.RemoveCacheEntryMultiTab(SessionConstants.SACRARESULT_NAVIAGTE);
            }

            using (IClientProxy<ISharedAccomAssmnt> sacraResultClient
                = IOMSProxyHelper.GetService<ISharedAccomAssmnt>(ServiceConstants.SERVICE_SACRA_RESULT_040410, CommonConstants.PS_SERVICE))
            {
                List<SACRAResultDetails> sacraResultDetails = sacraResultClient.Service.FetchSACRAResultDetails(sacraResultSearchParams);

                if (CheckValidationErrors(sacraResultClient.IomsMessages, out serviceErrorMsg))
                {
                    return Json(new
                    {
                        ErrorsList = serviceErrorMsg,
                        IsError = true
                    });
                }
                else
                {
                    return Json(new
                    {
                        SacraResults = sacraResultDetails,
                        IsError = false
                    });
                }
            }
        }

        /// <summary>
        /// Method to store filter values in session for breadcrumb navigation
        /// </summary>
        /// <returns>SACRA Result search parameters</returns>
        [HttpGet]
        public JsonResult GetSacraSessionData()
        {
            var sessionData = new
            {
                sacraFilterCriteria = IOMSSessionManager?.GetCacheValueMultiTab<SACRAResultSearchParams>(SessionConstants.SACRARESULT_FILTER),
                sacraGridData = IOMSSessionManager?.GetCacheValueMultiTab<List<SACRAResultDetails>>(SessionConstants.SACRARESULT_NAVIAGTE)
            };

            return Json(sessionData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Default Action Method - Render The View
        /// </summary>
        /// <returns>return default view</returns>
        [UIMAuthorizationAttribute(SecurityWebForm.RISK_ASSESSMENT_SACRA_RESULT)]
        public ActionResult SACRAResults()
        {
            ViewBag.SACRAResultsResourceSet = GetDynamicResources(SACRARESULTSRESOURCECLASSNAME);
            PrepareSecurityPermission();
            ViewBag.AssessmentCtrSource = IOMSLookupHelper.GetCurrentLookupByParent(LookupConstants.SACRA_RESULT_ASSESSMENTRESULT, SACRA_RESULT_LOOKUP);
            ViewBag.UserActionForPrisoner = Enum.GetValues(typeof(UserActionForPrisoner)).Cast<UserActionForPrisoner>().ToDictionary(t => t.ToString(), t => (int)t);
            ViewBag.UserActionForOtherDetails = Enum.GetValues(typeof(UserActionForOtherDetails)).Cast<UserActionForOtherDetails>().ToDictionary(t => t.ToString(), t => (int)t);

            return View();
        }

        /// <summary>
        /// To save SACRA Results
        /// </summary>
        /// <param name="mode">a int, indicate mode is new or edit</param>
        /// <param name="resultDetails">contain SACRAResultDetails for adding</param>
        /// <param name="assessmentResultText">a string, contains assessment result text</param>
        /// <returns>methodResponseData contains newly/updated SACRAResultDetails</returns>
        [HttpPost]
        [SecurityObjectFilterAttribute(false, AccessPermissionLevel.Read, SecurityOperation.RISK_ASSESSMENT_SACRA_RESULT_ADD, SecurityOperation.RISK_ASSESSMENT_SACRA_RESULT_EDIT)]
        [ViewStateFilterAttribute(CommonConstants.SACRAResultGridId, CommonConstants.SACRAResultGridColumn, CommonConstants.SACRAResultId)]
        public JsonResult Save(int mode, SACRAResultDetails resultDetails, string assessmentResultText)
        {
            string serviceErrorMsg = string.Empty;
            string assessmentResultId = resultDetails.AssessmentResult;

            resultDetails.AssessmentResult = assessmentResultText;

            using (IClientProxy<ISharedAccomAssmnt> sacraResultClient
                 = IOMSProxyHelper.GetService<ISharedAccomAssmnt>(ServiceConstants.SERVICE_SACRA_RESULT_040410, CommonConstants.PS_SERVICE))
            {
                if (resultDetails.IsAutoPopulateSacraReport)
                {
                    resultDetails.DocumentPDF = IOMSSessionManager?.GetCacheValueMultiTab<string>(SessionConstants.SACRADocumentData) ?? string.Empty;
                }
                if (mode == 2 || string.IsNullOrEmpty(resultDetails.ApplicationId))
                {
                    resultDetails.CreatedById = GetUserIntId();
                    resultDetails.CreatedByName = GetCreatedByName();
                    resultDetails.CreatedTime = DateTime.Now;
                    resultDetails = sacraResultClient.Service.AddSACRAResultDetails(resultDetails);
                }
                else
                {
                    resultDetails.UpdatedById = GetUserIntId();
                    resultDetails.UpdatedByName = GetCreatedByName();
                    resultDetails.UpdatedTime = DateTime.Now;
                    resultDetails = sacraResultClient.Service.UpdateSACRAResultDetails(resultDetails);
                }

                if (CheckValidationErrors(sacraResultClient.IomsMessages, out serviceErrorMsg))
                {
                    return Json(new
                    {
                        ErrorsList = serviceErrorMsg,
                        IsError = true
                    });
                }
                else
                {
                    if (resultDetails != null)
                    {
                        resultDetails.AssessmentResult = assessmentResultId;
                        return Json(new
                        {
                            SacraResults = resultDetails,
                            IsError = false,
                            CurrentGridName = CurrentGridId,
                            UniqueKeyName = GridUniqueKeyName,
                            UniqueKeyValue = GridUniqueKeyValue,
                           
                        });
                    }
                }
            }

            return Json(true);
        }

        /// <summary>
        /// Method to set the grid data in session on navigation
        /// </summary>
        /// <param name="sacraGridData">sacraGridData dataset contains grid data details</param>
        /// <returns>isNavSessionSet variable to identify the lists are added in sessions or not </returns>
        [HttpPost]
        public JsonResult SessionOnNaviagte(List<SACRAResultDetails> sacraGridData)
        {
            bool isNavSessionSet = false;

            if (sacraGridData != null && sacraGridData.Count > 0)
            {
                IOMSSessionManager.SetCacheValueMultiTab(SessionConstants.SACRARESULT_NAVIAGTE, sacraGridData);
                isNavSessionSet = true;
            }
            else
            {
                IOMSSessionManager.RemoveCacheEntryMultiTab(SessionConstants.SACRARESULT_NAVIAGTE);
            }

            return Json(isNavSessionSet, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to set current offender selected from grid
        /// </summary>
        /// <param name="prisonerId">prisoner id of selected offender to set the current offender</param>
        /// <returns>returns true if current offender is set successfully</returns>
        [HttpPost]
        public JsonResult SetCurrentOffender(string prisonerId)
        {
            OffenderContext globalOffender = new OffenderContext();

            globalOffender.OffenderID = prisonerId;
            IOMSSessionManager.SetCacheValueMultiTab(SessionConstants.CURRENT_OFFENDER, globalOffender);

            return Json(true);
        }

        /// <summary>
        /// Method to get offender details
        /// </summary>
        /// <param name="offenderId">offender id to whom the details need to be fetched</param>
        /// <returns>JSON object which holds the value of offender details</returns>
        [HttpGet]
        public JsonResult GetOffenderDetail(string offenderId)
        {
            string offenderName = string.Empty;
            Corrections.Ioms.Bpl.Collective.Entities.Offender offenderDetails = null;

            offenderDetails = GetOffenderData(offenderId);

            if (offenderDetails != null)
            {
                offenderName = string.Concat(offenderDetails.NameFamily.ToUpper(), CommonConstants.COMMA_WITH_SPACE, offenderDetails.NameFirst, CommonConstants.EMPTY_SPACE,
                                             offenderDetails.NameOther, CommonConstants.EMPTY_SPACE, offenderDetails.NameSuffix);
            }

            return Json(offenderName.Trim(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Validate Individual SACRA
        /// </summary>
        /// <param name="offenderId">OffenderId</param>
        /// <returns>returns Json result</returns>
        [HttpPost]
        public JsonResult ValidateIndividualSACRA(string offenderId)
        {
            string errorMsgs = string.Empty;
            bool isError = false;
            bool isIndSACRAExist = false;
            bool isIndividualSACRARecorded = false;

            RecordRowCollection isIndividualSACRAExist = new RecordRowCollection();

            using (IClientProxy<ISharedAccomAssmnt> clientProxy
                     = IOMSProxyHelper.GetService<ISharedAccomAssmnt>(ServiceConstants.SERVICE_SACRA_RESULT_040410, CommonConstants.PS_SERVICE))
            {
                isIndividualSACRAExist = clientProxy.Service.ValidateIndividualSACRA(offenderId, CommonConstants.INDIVIDUAL_SACRA_TYPE, CommonConstants.INDIVIDUAL_SACRA_RESULT);

                isError = CheckValidationErrors(clientProxy.IomsMessages, out errorMsgs);

                if (isIndividualSACRAExist != null && isIndividualSACRAExist.Count > 0)
                {
                    if (isIndividualSACRAExist[0].Props.Contains(INDIVIDUAL_SACRA_EXIST))
                    {
                        if (!string.IsNullOrEmpty(isIndividualSACRAExist[0].Props[INDIVIDUAL_SACRA_EXIST].Value) &&
                            isIndividualSACRAExist[0].Props[INDIVIDUAL_SACRA_EXIST].Value != NO_DATA_FOUND)
                        {
                            isIndividualSACRARecorded = true;
                            if (isIndividualSACRAExist[0].Props[INDIVIDUAL_SACRA_EXIST].Value == NO)
                            {
                                isIndSACRAExist = false;
                            }
                            else
                            {
                                isIndSACRAExist = true;
                            }
                        }
                    }
                }
            }
            var result = new
            {
                isIndividualSACRARecorded = isIndividualSACRARecorded,
                isIndSACRAExist = isIndSACRAExist,
                IsError = isError,
                ErrorList = errorMsgs
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Permission Set
        /// </summary>
        private void PrepareSecurityPermission()
        {
            bool isCUDAllowed = IsCUDAllowed(SecurityWebForm.RISK_ASSESSMENT_SACRA_RESULT);
            bool isCreateAllowed = IsOperationAllowed(SecurityOperation.RISK_ASSESSMENT_SACRA_RESULT_ADD) && isCUDAllowed;
            bool isEditAllowed = IsOperationAllowed(SecurityOperation.RISK_ASSESSMENT_SACRA_RESULT_EDIT) && isCUDAllowed;
            var permissionSet = new
            {
                isCUDAllowed = isCUDAllowed,
                isCreateAllowed = isCreateAllowed,
                isEditAllowed = isEditAllowed
            };
            ViewBag.CUDAllowed = permissionSet;
        }

        /// <summary>
        /// To Set session Values for SACRA Report
        /// </summary>
        /// <param name="cellSharingAssessId">Cell sharing risk assessment id for group report.</param>
        /// <param name="prisonerId">Offender id for single-prisoner SACRA report.</param>
        /// <param name="prisonerIds">Selected prisoner ids for a CombinedSACRA report run before the record has been saved (no cellSharingAssessId yet).</param>
        /// <param name="isAutoPopulate">True when the generated report PDF should be cached for document save.</param>
        /// <returns>JSON Result returns True if session set successfully</returns>
        [HttpPost]
        public JsonResult SetReportParameter(string cellSharingAssessId, string prisonerId, List<string> prisonerIds, bool isAutoPopulate)
        {
            SetSACRAReportParameter(cellSharingAssessId, prisonerId, prisonerIds, isAutoPopulate);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Centralises SACRA report session setup so manual report click and save-success auto-run use the same parameters.
        /// </summary>
        private void SetSACRAReportParameter(string cellSharingAssessId, string prisonerId, List<string> prisonerIds, bool isAutoPopulate)
        {
            IOMSSessionManager.SetCacheValueMultiTab(SSRSReportConstants.SACRA_UNITCHK, CommonConstants.STRONE);
            IOMSSessionManager.RemoveCacheEntryMultiTab(SSRSReportConstants.SACRA_GRID);

            if (!string.IsNullOrEmpty(prisonerId))
            {
                IOMSSessionManager.SetCacheValueMultiTab(SSRSReportConstants.SACRA_OFFENDERIDS, prisonerId);
                IOMSSessionManager.RemoveCacheEntryMultiTab(SSRSReportConstants.CELL_SHARING_RISK_ASSESSMENT_ID);
            }
            else
            {
                IOMSSessionManager.RemoveCacheEntryMultiTab(SSRSReportConstants.SACRA_OFFENDERIDS);
            }

            // The CombinedSACRA report has two prisoner sources: an already-saved record (cellSharingAssessId),
            // or prisoner ids selected in the UI before the record has ever been saved (prisonerIds). Both feed
            // ro_getcompsacradetails/ro_getrequestid, which accept either parameter (mutually exclusive).
            string prisonerIdsCsv = prisonerIds != null && prisonerIds.Count > 0 ? string.Join(",", prisonerIds) : string.Empty;

            if (string.IsNullOrEmpty(prisonerId) && !string.IsNullOrEmpty(cellSharingAssessId))
            {
                IOMSSessionManager.SetCacheValueMultiTab(SSRSReportConstants.CELL_SHARING_RISK_ASSESSMENT_ID, cellSharingAssessId);
                IOMSSessionManager.RemoveCacheEntryMultiTab(SSRSReportConstants.SACRA_COMBINED_PRISONER_IDS);

                using (IClientProxy<ISharedAccomAssmnt> sacraResultClient = IOMSProxyHelper.GetService<ISharedAccomAssmnt>(ServiceConstants.SERVICE_SACRA_RESULT_040410, CommonConstants.PS_SERVICE))
                {
                    string qacRequestIds = sacraResultClient.Service.PrepareSACRAActiveChargeRequests(cellSharingAssessId, string.Empty);

                    if (!string.IsNullOrEmpty(qacRequestIds))
                    {
                        IOMSSessionManager.SetCacheValueMultiTab(SSRSReportConstants.SACRA_QAC_REQUEST_IDS, qacRequestIds);
                    }
                    else
                    {
                        IOMSSessionManager.RemoveCacheEntryMultiTab(SSRSReportConstants.SACRA_QAC_REQUEST_IDS);
                    }
                }
            }
            else if (string.IsNullOrEmpty(prisonerId) && !string.IsNullOrEmpty(prisonerIdsCsv))
            {
                IOMSSessionManager.RemoveCacheEntryMultiTab(SSRSReportConstants.CELL_SHARING_RISK_ASSESSMENT_ID);
                IOMSSessionManager.SetCacheValueMultiTab(SSRSReportConstants.SACRA_COMBINED_PRISONER_IDS, prisonerIdsCsv);

                using (IClientProxy<ISharedAccomAssmnt> sacraResultClient = IOMSProxyHelper.GetService<ISharedAccomAssmnt>(ServiceConstants.SERVICE_SACRA_RESULT_040410, CommonConstants.PS_SERVICE))
                {
                    string qacRequestIds = sacraResultClient.Service.PrepareSACRAActiveChargeRequests(string.Empty, prisonerIdsCsv);

                    if (!string.IsNullOrEmpty(qacRequestIds))
                    {
                        IOMSSessionManager.SetCacheValueMultiTab(SSRSReportConstants.SACRA_QAC_REQUEST_IDS, qacRequestIds);
                    }
                    else
                    {
                        IOMSSessionManager.RemoveCacheEntryMultiTab(SSRSReportConstants.SACRA_QAC_REQUEST_IDS);
                    }
                }
            }
            else
            {
                IOMSSessionManager.RemoveCacheEntryMultiTab(SSRSReportConstants.CELL_SHARING_RISK_ASSESSMENT_ID);
                IOMSSessionManager.RemoveCacheEntryMultiTab(SSRSReportConstants.SACRA_COMBINED_PRISONER_IDS);
                IOMSSessionManager.RemoveCacheEntryMultiTab(SSRSReportConstants.SACRA_QAC_REQUEST_IDS);
            }

            if (isAutoPopulate)
            {
                IOMSSessionManager.SetCacheValueMultiTab(SessionConstants.SACRAAutoPopulateReport, isAutoPopulate);
            }
            else
            {
                IOMSSessionManager.RemoveCacheEntryMultiTab(SessionConstants.SACRAAutoPopulateReport);
            }
        }

        /// <summary>
        /// Represent the method to Generate Combined Sacra Report
        /// </summary>
        /// <returns>combined sacra report content for the given offender id</returns>
        [HttpGet]
        public FileContentResult GetCombinedSACRAReport()
        {
            string cellSharingAssessId = IOMSSessionManager?.GetCacheValueMultiTab<string>(SSRSReportConstants.CELL_SHARING_RISK_ASSESSMENT_ID) ?? string.Empty;

            string prisonerIdsCsv = IOMSSessionManager?.GetCacheValueMultiTab<string>(SSRSReportConstants.SACRA_COMBINED_PRISONER_IDS) ?? string.Empty;

            string qacRequestIds = IOMSSessionManager?.GetCacheValueMultiTab<string>(SSRSReportConstants.SACRA_QAC_REQUEST_IDS) ?? string.Empty;

            var reportParams = new Dictionary<string, object>
            {
                { SSRSReportConstants.OP_REQUESTOR, GetReportUser() },

                { SSRSReportConstants.CELL_SHARING_RISK_ASSESSMENT_ID, cellSharingAssessId },

                { SSRSReportConstants.SACRA_COMBINED_PRISONER_IDS, prisonerIdsCsv },

                { SSRSReportConstants.PI_QAC_REQUEST_IDS, qacRequestIds }
            };

            var resultbyteArr = base.GenerateReport(SSRSReportConstants.COMBINED_SACRA, reportParams, true);

            if (resultbyteArr != null)
            {
                bool isAutopopulate = IOMSSessionManager?.GetCacheValueMultiTab<bool>(SessionConstants.SACRAAutoPopulateReport) ?? false;

                if (isAutopopulate && !string.IsNullOrWhiteSpace(cellSharingAssessId))
                {
                    // Existing/edited record: the ApplicationId already exists, so attach the document now.
                    string documentPdf = Convert.ToBase64String(resultbyteArr);

                    using (IClientProxy<ISharedAccomAssmnt> sacraResultClient =IOMSProxyHelper.GetService<ISharedAccomAssmnt>(ServiceConstants.SERVICE_SACRA_RESULT_040410,CommonConstants.PS_SERVICE))
                    {
                        SACRAResultDetails documentDetails = new SACRAResultDetails
                        {
                            ApplicationId = cellSharingAssessId,
                            DocumentPDF = documentPdf
                        };

                       sacraResultClient.Service.SaveSACRAReportDocument(documentDetails);
                    }
                }
                else if (isAutopopulate)
                {
                    // Not-yet-saved record (prisoner ids only, no ApplicationId yet): cache the PDF so Save()
                    // can pick it up via SACRADocumentData and attach it once the record is actually persisted.
                    IOMSSessionManager.SetCacheValueMultiTab(SessionConstants.SACRADocumentData, Convert.ToBase64String(resultbyteArr));
                }
                else
                {
                    IOMSSessionManager.RemoveCacheEntryMultiTab(SessionConstants.SACRADocumentData);
                }
            }

            return new FileContentResult(resultbyteArr, SSRSReportConstants.APPPDF);
        }
        /// <summary>
        /// To get details sacra report document
        /// </summary>
        /// <param name="cellSharingAssessId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetSACRAReportDocumentInfo(string cellSharingAssessId)
        {

            using (IClientProxy<ISharedAccomAssmnt> sacraResultClient =
                IOMSProxyHelper.GetService<ISharedAccomAssmnt>( ServiceConstants.SERVICE_SACRA_RESULT_040410,CommonConstants.PS_SERVICE))
            {
             SACRAResultDetails documentDetails = sacraResultClient.Service.GetSACRAReportDocumentInfo(cellSharingAssessId);

                return Json(new
                {
                    IsError = false,
                    DocumentId = documentDetails != null ? documentDetails.DocumentId : string.Empty,
                    DocumentVersion = documentDetails != null ? documentDetails.DocumentVersion : 0
                }, JsonRequestBehavior.AllowGet);
            }

        }


        #endregion Methods
    }
}