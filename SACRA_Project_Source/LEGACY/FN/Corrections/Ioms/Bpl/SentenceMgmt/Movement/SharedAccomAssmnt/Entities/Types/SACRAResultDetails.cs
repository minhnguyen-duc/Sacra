
using System;
using System.Collections.Generic;
using Corrections.Ioms.Bpl.Entities;
using Corrections.Ioms.Bpl;

namespace Corrections.Ioms.Bpl.Movement.SharedAccomAssmnt.Entities
{
    [Serializable]
    public class SACRAResultDetails : EntityBase
    {
        #region Private Variables
        private string _prisonerFirstId = string.Empty;
        private string _prisonerFirstName = string.Empty;
        private string _prisonerFirstPRN = string.Empty;
        private string _prisonerSecondId = string.Empty;
        private string _prisonerSecondName = string.Empty;
        private string _prisonerSecondPRN = string.Empty;
        private string _prisonerThirdId = string.Empty;
        private string _prisonerThirdName = string.Empty;
        private string _prisonerThirdPRN = string.Empty;
        private string _prisonerFourthId = string.Empty;
        private string _prisonerFourthName = string.Empty;
        private string _prisonerFourthPRN = string.Empty;
        private string _prisonerFifthId = string.Empty;
        private string _prisonerFifthName = string.Empty;
        private string _prisonerFifthPRN = string.Empty;
        private string _prisonerSixthId = string.Empty;
        private string _prisonerSixthName = string.Empty;
        private string _prisonerSixthPRN = string.Empty;
        private string _prisonerSeventhId = string.Empty;
        private string _prisonerSeventhName = string.Empty;
        private string _prisonerSeventhPRN = string.Empty;
        private string _prisonerEighthId = string.Empty;
        private string _prisonerEighthName = string.Empty;
        private string _prisonerEighthPRN = string.Empty;
        private string _prisonerNinthId = string.Empty;
        private string _prisonerNinthName = string.Empty;
        private string _prisonerNinthPRN = string.Empty;
        private string _prisonerTenthId = string.Empty;
        private string _prisonerTenthName = string.Empty;
        private string _prisonerTenthPRN = string.Empty;
        private DateTime _assessmentDate = DateTime.MinValue;
        private string _assessmentResult = string.Empty;
        private string _additionalComments = string.Empty;
        private string _createdByName = string.Empty;
        private DateTime _createdTime = DateTime.MinValue;
        private string _updatedByName = string.Empty;
        private DateTime _updatedTime = DateTime.MinValue;
        private string _applicationId = string.Empty;
        private string _createdById = string.Empty;
        private string _updatedById = string.Empty;
        private int _rowCount = 0;
        private string _cellSharingFirstPrisonerId = string.Empty;
        private string _cellSharingSecondPrisonerId = string.Empty;
        private string _cellSharingThirdPrisonerId = string.Empty;
        private string _cellSharingFourthPrisonerId = string.Empty;
        private string _cellSharingFifthPrisonerId = string.Empty;
        private string _cellSharingSixthPrisonerId = string.Empty;
        private string _cellSharingSeventhPrisonerId = string.Empty;
        private string _cellSharingEighthPrisonerId = string.Empty;
        private string _cellSharingNinthPrisonerId = string.Empty;
        private string _cellSharingTenthPrisonerId = string.Empty;
        private UserActionForPrisoner _userActionForPrisoner = UserActionForPrisoner.None;
        private UserActionForOtherDetails _userActionForOtherDetails = UserActionForOtherDetails.OtherDetailsNotUpdated;
        private string _alertId = string.Empty;
        private short _prisonerOrderNum = 0;
        /* Individual SACRA NTDB changes #57727 - Starts*/
        private string _assesTypeCode = string.Empty;
        private string _cellSharingRiskAssId = string.Empty;
        private string _offenderId = string.Empty;
        /* Individual SACRA NTDB changes #57727 - Ends*/
        private string _documentPDF = string.Empty;
        private string _documentId = string.Empty;
        private bool _isAutoPopulateSacraReport = false;
        private int _documentVersion = 0;

        #endregion

        #region  Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public SACRAResultDetails()
            : base()
        {
        }

        /// <summary>
        /// Peramaterized Constructor
        /// </summary>
        /// <param name="recordRowCollection"></param>
        public SACRAResultDetails(RecordRowCollection recordRow)
            : base()
        {
            base.UnpackRequest(recordRow);
        }

        /// <summary>
        /// Peramaterized Constructor
        /// </summary>
        /// <param name="recordRowCollection"></param>
        public SACRAResultDetails(RecordRow recordRow)
            : base()
        {
            base.UnpackRequest(recordRow);
        }
        /// <summary>
        /// Peramaterized Constructor
        /// </summary>
        /// <param name="parameterCollection"></param>
        public SACRAResultDetails(ParameterCollection parameterCollection)
            : base()
        {
            base.UnpackRequest(parameterCollection);
        }
        #endregion

        #region Public Properties
        #region Prisoner A
        /// <summary>
        /// Holds the details of the Prisoner A.
        /// </summary>
        [DataFieldNameAttribute("PRISONER_FIRST_ID")]
        public string PrisonerFirstId
        {
            get { return _prisonerFirstId; }
            set { _prisonerFirstId = value; }
        }

        [DataFieldNameAttribute("PRISONER_FIRST_NAME")]
        public string PrisonerFirstName
        {
            get { return _prisonerFirstName; }
            set { _prisonerFirstName = value; }
        }
        [DataFieldNameAttribute("PRISONER_FIRST_PRN")]
        public string PrisonerFirstPRN
        {
            get { return _prisonerFirstPRN; }
            set { _prisonerFirstPRN = value; }
        }
        #endregion

        #region Prisoner B
        /// <summary>
        /// Holds the details of the Prisoner B.
        /// </summary>
        [DataFieldNameAttribute("PRISONER_SECOND_ID")]
        public string PrisonerSecondId
        {
            get { return _prisonerSecondId; }
            set { _prisonerSecondId = value; }
        }
        [DataFieldNameAttribute("PRISONER_SECOND_NAME")]
        public string PrisonerSecondName
        {
            get { return _prisonerSecondName; }
            set { _prisonerSecondName = value; }
        }
        [DataFieldNameAttribute("PRISONER_SECOND_PRN")]
        public string PrisonerSecondPRN
        {
            get { return _prisonerSecondPRN; }
            set { _prisonerSecondPRN = value; }
        }
        #endregion

        #region Prisoners C to J
        [DataFieldNameAttribute("PRISONER_THIRD_ID")]
        public string PrisonerThirdId
        {
            get { return _prisonerThirdId; }
            set { _prisonerThirdId = value; }
        }

        [DataFieldNameAttribute("PRISONER_THIRD_NAME")]
        public string PrisonerThirdName
        {
            get { return _prisonerThirdName; }
            set { _prisonerThirdName = value; }
        }

        [DataFieldNameAttribute("PRISONER_THIRD_PRN")]
        public string PrisonerThirdPRN
        {
            get { return _prisonerThirdPRN; }
            set { _prisonerThirdPRN = value; }
        }

        [DataFieldNameAttribute("PRISONER_FOURTH_ID")]
        public string PrisonerFourthId
        {
            get { return _prisonerFourthId; }
            set { _prisonerFourthId = value; }
        }

        [DataFieldNameAttribute("PRISONER_FOURTH_NAME")]
        public string PrisonerFourthName
        {
            get { return _prisonerFourthName; }
            set { _prisonerFourthName = value; }
        }

        [DataFieldNameAttribute("PRISONER_FOURTH_PRN")]
        public string PrisonerFourthPRN
        {
            get { return _prisonerFourthPRN; }
            set { _prisonerFourthPRN = value; }
        }

        [DataFieldNameAttribute("PRISONER_FIFTH_ID")]
        public string PrisonerFifthId
        {
            get { return _prisonerFifthId; }
            set { _prisonerFifthId = value; }
        }

        [DataFieldNameAttribute("PRISONER_FIFTH_NAME")]
        public string PrisonerFifthName
        {
            get { return _prisonerFifthName; }
            set { _prisonerFifthName = value; }
        }

        [DataFieldNameAttribute("PRISONER_FIFTH_PRN")]
        public string PrisonerFifthPRN
        {
            get { return _prisonerFifthPRN; }
            set { _prisonerFifthPRN = value; }
        }

        [DataFieldNameAttribute("PRISONER_SIXTH_ID")]
        public string PrisonerSixthId
        {
            get { return _prisonerSixthId; }
            set { _prisonerSixthId = value; }
        }

        [DataFieldNameAttribute("PRISONER_SIXTH_NAME")]
        public string PrisonerSixthName
        {
            get { return _prisonerSixthName; }
            set { _prisonerSixthName = value; }
        }

        [DataFieldNameAttribute("PRISONER_SIXTH_PRN")]
        public string PrisonerSixthPRN
        {
            get { return _prisonerSixthPRN; }
            set { _prisonerSixthPRN = value; }
        }

        [DataFieldNameAttribute("PRISONER_SEVENTH_ID")]
        public string PrisonerSeventhId
        {
            get { return _prisonerSeventhId; }
            set { _prisonerSeventhId = value; }
        }

        [DataFieldNameAttribute("PRISONER_SEVENTH_NAME")]
        public string PrisonerSeventhName
        {
            get { return _prisonerSeventhName; }
            set { _prisonerSeventhName = value; }
        }

        [DataFieldNameAttribute("PRISONER_SEVENTH_PRN")]
        public string PrisonerSeventhPRN
        {
            get { return _prisonerSeventhPRN; }
            set { _prisonerSeventhPRN = value; }
        }

        [DataFieldNameAttribute("PRISONER_EIGHTH_ID")]
        public string PrisonerEighthId
        {
            get { return _prisonerEighthId; }
            set { _prisonerEighthId = value; }
        }

        [DataFieldNameAttribute("PRISONER_EIGHTH_NAME")]
        public string PrisonerEighthName
        {
            get { return _prisonerEighthName; }
            set { _prisonerEighthName = value; }
        }

        [DataFieldNameAttribute("PRISONER_EIGHTH_PRN")]
        public string PrisonerEighthPRN
        {
            get { return _prisonerEighthPRN; }
            set { _prisonerEighthPRN = value; }
        }

        [DataFieldNameAttribute("PRISONER_NINTH_ID")]
        public string PrisonerNinthId
        {
            get { return _prisonerNinthId; }
            set { _prisonerNinthId = value; }
        }

        [DataFieldNameAttribute("PRISONER_NINTH_NAME")]
        public string PrisonerNinthName
        {
            get { return _prisonerNinthName; }
            set { _prisonerNinthName = value; }
        }

        [DataFieldNameAttribute("PRISONER_NINTH_PRN")]
        public string PrisonerNinthPRN
        {
            get { return _prisonerNinthPRN; }
            set { _prisonerNinthPRN = value; }
        }

        [DataFieldNameAttribute("PRISONER_TENTH_ID")]
        public string PrisonerTenthId
        {
            get { return _prisonerTenthId; }
            set { _prisonerTenthId = value; }
        }

        [DataFieldNameAttribute("PRISONER_TENTH_NAME")]
        public string PrisonerTenthName
        {
            get { return _prisonerTenthName; }
            set { _prisonerTenthName = value; }
        }

        [DataFieldNameAttribute("PRISONER_TENTH_PRN")]
        public string PrisonerTenthPRN
        {
            get { return _prisonerTenthPRN; }
            set { _prisonerTenthPRN = value; }
        }
        #endregion

        /// <summary>
        /// Holds the value of the Assessment date.
        /// </summary>
        [DataFieldNameAttribute("ASSESSMENT_DATE")]
        public DateTime AssessmentDate
        {
            get { return _assessmentDate; }
            set { _assessmentDate = value; }
        }

        /// <summary>
        /// Holds the value of the Assessment Result.
        /// </summary>
        [DataFieldNameAttribute("ASSESSMENT_RESULT")]
        public string AssessmentResult
        {
            get { return _assessmentResult; }
            set { _assessmentResult = value; }
        }

        /// <summary>
        /// Holds the value of the assessment comments.
        /// </summary>
        [DataFieldNameAttribute("ASSESSMENT_COMMENTS")]
        public string AdditionalComments
        {
            get { return _additionalComments; }
            set { _additionalComments = value; }
        }

        /// <summary>
        /// Holds the name of the person who created the record.
        /// </summary>
        [DataFieldNameAttribute("CREATED_BY_NAME")]
        public string CreatedByName
        {
            get { return _createdByName; }
            set { _createdByName = value; }
        }

        /// <summary>
        /// Holds the value when record was created.
        /// </summary>
        [DataFieldNameAttribute("CREATED_TIME")]
        public DateTime CreatedTime
        {
            get { return _createdTime; }
            set { _createdTime = value; }
        }

        /// <summary>
        /// Holds the name of the person who updated the record.
        /// </summary>
        [DataFieldNameAttribute("UPDATED_BY_NAME")]
        public string UpdatedByName
        {
            get { return _updatedByName; }
            set { _updatedByName = value; }
        }

        /// <summary>
        /// Holds the value when record was updated.
        /// </summary>
        [DataFieldNameAttribute("UPDATED_TIME")]
        public DateTime UpdatedTime
        {
            get { return _updatedTime; }
            set { _updatedTime = value; }
        }

        /// <summary>
        ///Holds the value of the record identifier (Primary Key).
        /// </summary>
        [DataFieldNameAttribute("APPLICATION_ID")]
        public string ApplicationId
        {
            get { return _applicationId; }
            set { _applicationId = value; }
        }

        /// <summary>
        /// Holds the name of the person who created the record.
        /// </summary>
        [DataFieldNameAttribute("CREATED_BY_ID")]
        public string CreatedById
        {
            get { return _createdById; }
            set { _createdById = value; }
        }

        /// <summary>
        /// Holds the entity id of the person who updated the record.
        /// </summary>
        [DataFieldNameAttribute("UPDATED_BY_ID")]
        public string UpdatedById
        {
            get { return _updatedById; }
            set { _updatedById = value; }
        }
        /// <summary>
        /// Holds the rowCount Value
        /// </summary>
        [DataFieldNameAttribute("ROW_COUNT")]
        public int RowCount
        {
            get { return _rowCount; }
            set
            {
                _rowCount = int.Parse(value.ToString());
            }
        }
        /// <summary>
        /// Holds the id of Cell sharing Prisoner Table for first table
        /// </summary>
        [DataFieldNameAttribute("CELL_SHARING_FIRST_PRISONER_ID")]
        public string CellSharingFirstPrisonerId
        {
            get { return _cellSharingFirstPrisonerId; }
            set { _cellSharingFirstPrisonerId = value; }
        }

        /// <summary>
        /// Holds the id of Cell sharing Prisoner Table for first table
        /// </summary>
        [DataFieldNameAttribute("CELL_SHARING_SEC_PRISONER_ID")]
        public string CellSharingSecondPrisonerId
        {
            get { return _cellSharingSecondPrisonerId; }
            set { _cellSharingSecondPrisonerId = value; }
        }

        /// <summary>
        /// Holds the id of Cell sharing Prisoner Table for prisoner C.
        /// </summary>
        [DataFieldNameAttribute("CELL_SHARING_THIRD_PRISONER_ID")]
        public string CellSharingThirdPrisonerId
        {
            get { return _cellSharingThirdPrisonerId; }
            set { _cellSharingThirdPrisonerId = value; }
        }

        /// <summary>
        /// Holds the id of Cell sharing Prisoner Table for prisoner D.
        /// </summary>
        [DataFieldNameAttribute("CELL_SHARING_FOURTH_PRISONER_ID")]
        public string CellSharingFourthPrisonerId
        {
            get { return _cellSharingFourthPrisonerId; }
            set { _cellSharingFourthPrisonerId = value; }
        }

        /// <summary>
        /// Holds the id of Cell sharing Prisoner Table for prisoner E.
        /// </summary>
        [DataFieldNameAttribute("CELL_SHARING_FIFTH_PRISONER_ID")]
        public string CellSharingFifthPrisonerId
        {
            get { return _cellSharingFifthPrisonerId; }
            set { _cellSharingFifthPrisonerId = value; }
        }

        /// <summary>
        /// Holds the id of Cell sharing Prisoner Table for prisoner F.
        /// </summary>
        [DataFieldNameAttribute("CELL_SHARING_SIXTH_PRISONER_ID")]
        public string CellSharingSixthPrisonerId
        {
            get { return _cellSharingSixthPrisonerId; }
            set { _cellSharingSixthPrisonerId = value; }
        }

        /// <summary>
        /// Holds the id of Cell sharing Prisoner Table for prisoner G.
        /// </summary>
        [DataFieldNameAttribute("CELL_SHARING_SEVENTH_PRISONER_ID")]
        public string CellSharingSeventhPrisonerId
        {
            get { return _cellSharingSeventhPrisonerId; }
            set { _cellSharingSeventhPrisonerId = value; }
        }

        /// <summary>
        /// Holds the id of Cell sharing Prisoner Table for prisoner H.
        /// </summary>
        [DataFieldNameAttribute("CELL_SHARING_EIGHTH_PRISONER_ID")]
        public string CellSharingEighthPrisonerId
        {
            get { return _cellSharingEighthPrisonerId; }
            set { _cellSharingEighthPrisonerId = value; }
        }

        /// <summary>
        /// Holds the id of Cell sharing Prisoner Table for prisoner I.
        /// </summary>
        [DataFieldNameAttribute("CELL_SHARING_NINTH_PRISONER_ID")]
        public string CellSharingNinthPrisonerId
        {
            get { return _cellSharingNinthPrisonerId; }
            set { _cellSharingNinthPrisonerId = value; }
        }

        /// <summary>
        /// Holds the id of Cell sharing Prisoner Table for prisoner J.
        /// </summary>
        [DataFieldNameAttribute("CELL_SHARING_TENTH_PRISONER_ID")]
        public string CellSharingTenthPrisonerId
        {
            get { return _cellSharingTenthPrisonerId; }
            set { _cellSharingTenthPrisonerId = value; }
        }


        /// <summary>
        /// User Action For the Prisoner
        /// </summary>
        [DataFieldNameAttribute("USER_ACTION_FOR_PRISONER")]
        public UserActionForPrisoner UserActionForPrisoner
        {
            get { return _userActionForPrisoner; }
            set { _userActionForPrisoner = value; }
        }

        /// <summary>
        /// User Action For other details
        /// </summary>
        [DataFieldNameAttribute("USER_ACTION_FOR_OTHER_DETAILS")]
        public UserActionForOtherDetails UserActionForOtherDetails
        {
            get { return _userActionForOtherDetails; }
            set { _userActionForOtherDetails = value; }
        }

        /// <summary>
        /// Prisoner Order 1 = Priosner A, 2 = Prisoner B
        /// </summary>
        [DataFieldNameAttribute("CELL_SHARING_PRISONER_ORDER")]
        public short PrisonerOrderNum
        {
            get { return _prisonerOrderNum; }
            set { _prisonerOrderNum = value; }
        }
        /// <summary>
        /// Prisoner Order 1 = Priosner A, 2 = Prisoner B
        /// </summary>
        [DataFieldNameAttribute("ALERT_ID")]
        public string AlertId
        {
            get { return _alertId; }
            set { _alertId = value; }
        }

        /// <summary>
        /// Holds Sacra document id
        /// </summary>
        [DataFieldNameAttribute("DOCUMENT_ID")]
        public string DocumentId
        {
            get { return _documentId; }
            set { _documentId = value; }
        }

        /// <summary>
        /// Holds Sacra document id
        /// </summary>
        [DataFieldNameAttribute("DOCUMENT_PDF")]
        public string DocumentPDF
        {
            get { return _documentPDF; }
            set { _documentPDF = value; }
        }

        /// <summary>
        /// Holds the combined sacra report generated on edit mode
        /// </summary>
        public bool IsAutoPopulateSacraReport
        {
            get { return _isAutoPopulateSacraReport; }
            set { _isAutoPopulateSacraReport = value; }
        }

        /// <summary>
        /// Document version
        /// </summary>
        [DataFieldNameAttribute("VERSION_NUM")]
        public int DocumentVersion
        {
            get { return _documentVersion; }
            set { _documentVersion = value; }
        }
        #endregion

        #region Public Mentod
        /// <summary>
        /// Method used to convert the Record Row Collection To List<SACRAResultDetails>
        /// </summary>
        /// <param name="recordRowCollection">Input RRC</param>
        /// <returns>List<SACRAResultDetails></returns>
        public static EntityBaseCollection<SACRAResultDetails> GetSACRAResultList(RecordRowCollection recordRowCollection)
        {
            EntityBaseCollection<SACRAResultDetails> resultList = new EntityBaseCollection<SACRAResultDetails>();
            for (int recordCount = 0; recordCount < recordRowCollection.Count; recordCount++)
            {
                SACRAResultDetails resultDetails = new SACRAResultDetails(recordRowCollection[recordCount]);
                resultDetails.RowCount = recordCount + 1;
                resultList.Add(resultDetails);
            }
            return resultList;
        }


        /// <summary>
        /// Returns selected prisoners in visual order A..J, skipping blank positions.
        /// Business rule: selected prisoners are persisted as a compact sequential group with order numbers 0..N-1.
        /// </summary>
        public IList<SACRAPrisonerSlot> GetSelectedPrisonerSlots()
        {
            List<SACRAPrisonerSlot> selectedPrisoners = new List<SACRAPrisonerSlot>();
            AddSelectedPrisonerSlot(selectedPrisoners, 0, PrisonerFirstId, PrisonerFirstName, PrisonerFirstPRN, CellSharingFirstPrisonerId);
            AddSelectedPrisonerSlot(selectedPrisoners, 1, PrisonerSecondId, PrisonerSecondName, PrisonerSecondPRN, CellSharingSecondPrisonerId);
            AddSelectedPrisonerSlot(selectedPrisoners, 2, PrisonerThirdId, PrisonerThirdName, PrisonerThirdPRN, CellSharingThirdPrisonerId);
            AddSelectedPrisonerSlot(selectedPrisoners, 3, PrisonerFourthId, PrisonerFourthName, PrisonerFourthPRN, CellSharingFourthPrisonerId);
            AddSelectedPrisonerSlot(selectedPrisoners, 4, PrisonerFifthId, PrisonerFifthName, PrisonerFifthPRN, CellSharingFifthPrisonerId);
            AddSelectedPrisonerSlot(selectedPrisoners, 5, PrisonerSixthId, PrisonerSixthName, PrisonerSixthPRN, CellSharingSixthPrisonerId);
            AddSelectedPrisonerSlot(selectedPrisoners, 6, PrisonerSeventhId, PrisonerSeventhName, PrisonerSeventhPRN, CellSharingSeventhPrisonerId);
            AddSelectedPrisonerSlot(selectedPrisoners, 7, PrisonerEighthId, PrisonerEighthName, PrisonerEighthPRN, CellSharingEighthPrisonerId);
            AddSelectedPrisonerSlot(selectedPrisoners, 8, PrisonerNinthId, PrisonerNinthName, PrisonerNinthPRN, CellSharingNinthPrisonerId);
            AddSelectedPrisonerSlot(selectedPrisoners, 9, PrisonerTenthId, PrisonerTenthName, PrisonerTenthPRN, CellSharingTenthPrisonerId);
            return selectedPrisoners;
        }

        /// <summary>
        /// Returns all persisted OM_CELL_SHARING_PRISONER ids currently known by the UI model.
        /// </summary>
        public IList<string> GetAllCellSharingPrisonerIds()
        {
            List<string> cellSharingPrisonerIds = new List<string>();
            AddCellSharingPrisonerId(cellSharingPrisonerIds, CellSharingFirstPrisonerId);
            AddCellSharingPrisonerId(cellSharingPrisonerIds, CellSharingSecondPrisonerId);
            AddCellSharingPrisonerId(cellSharingPrisonerIds, CellSharingThirdPrisonerId);
            AddCellSharingPrisonerId(cellSharingPrisonerIds, CellSharingFourthPrisonerId);
            AddCellSharingPrisonerId(cellSharingPrisonerIds, CellSharingFifthPrisonerId);
            AddCellSharingPrisonerId(cellSharingPrisonerIds, CellSharingSixthPrisonerId);
            AddCellSharingPrisonerId(cellSharingPrisonerIds, CellSharingSeventhPrisonerId);
            AddCellSharingPrisonerId(cellSharingPrisonerIds, CellSharingEighthPrisonerId);
            AddCellSharingPrisonerId(cellSharingPrisonerIds, CellSharingNinthPrisonerId);
            AddCellSharingPrisonerId(cellSharingPrisonerIds, CellSharingTenthPrisonerId);
            return cellSharingPrisonerIds;
        }

        /// <summary>
        /// Writes saved prisoners back into compact A..J positions after backend save.
        /// </summary>
        public void ApplyPrisonerSlots(IList<SACRAPrisonerSlot> prisonerSlots)
        {
            ClearPrisonerSlots();

            if (prisonerSlots == null)
            {
                return;
            }

            for (int index = 0; index < prisonerSlots.Count && index < 10; index++)
            {
                ApplyPrisonerSlot(index, prisonerSlots[index]);
            }
        }

        public int SelectedPrisonerCount
        {
            get { return GetSelectedPrisonerSlots().Count; }
        }

        private static void AddSelectedPrisonerSlot(IList<SACRAPrisonerSlot> selectedPrisoners, short originalOrderNumber, string prisonerId, string prisonerName, string prisonerPrn, string cellSharingPrisonerId)
        {
            if (!string.IsNullOrWhiteSpace(prisonerId))
            {
                selectedPrisoners.Add(new SACRAPrisonerSlot
                {
                    OriginalOrderNumber = originalOrderNumber,
                    PrisonerId = prisonerId,
                    PrisonerName = prisonerName,
                    PrisonerPRN = prisonerPrn,
                    CellSharingPrisonerId = cellSharingPrisonerId
                });
            }
        }

        private static void AddCellSharingPrisonerId(IList<string> cellSharingPrisonerIds, string cellSharingPrisonerId)
        {
            if (!string.IsNullOrWhiteSpace(cellSharingPrisonerId) && !cellSharingPrisonerIds.Contains(cellSharingPrisonerId))
            {
                cellSharingPrisonerIds.Add(cellSharingPrisonerId);
            }
        }

        private void ClearPrisonerSlots()
        {
            PrisonerFirstId = string.Empty;
            PrisonerFirstName = string.Empty;
            PrisonerFirstPRN = string.Empty;
            CellSharingFirstPrisonerId = string.Empty;
            PrisonerSecondId = string.Empty;
            PrisonerSecondName = string.Empty;
            PrisonerSecondPRN = string.Empty;
            CellSharingSecondPrisonerId = string.Empty;
            PrisonerThirdId = string.Empty;
            PrisonerThirdName = string.Empty;
            PrisonerThirdPRN = string.Empty;
            CellSharingThirdPrisonerId = string.Empty;
            PrisonerFourthId = string.Empty;
            PrisonerFourthName = string.Empty;
            PrisonerFourthPRN = string.Empty;
            CellSharingFourthPrisonerId = string.Empty;
            PrisonerFifthId = string.Empty;
            PrisonerFifthName = string.Empty;
            PrisonerFifthPRN = string.Empty;
            CellSharingFifthPrisonerId = string.Empty;
            PrisonerSixthId = string.Empty;
            PrisonerSixthName = string.Empty;
            PrisonerSixthPRN = string.Empty;
            CellSharingSixthPrisonerId = string.Empty;
            PrisonerSeventhId = string.Empty;
            PrisonerSeventhName = string.Empty;
            PrisonerSeventhPRN = string.Empty;
            CellSharingSeventhPrisonerId = string.Empty;
            PrisonerEighthId = string.Empty;
            PrisonerEighthName = string.Empty;
            PrisonerEighthPRN = string.Empty;
            CellSharingEighthPrisonerId = string.Empty;
            PrisonerNinthId = string.Empty;
            PrisonerNinthName = string.Empty;
            PrisonerNinthPRN = string.Empty;
            CellSharingNinthPrisonerId = string.Empty;
            PrisonerTenthId = string.Empty;
            PrisonerTenthName = string.Empty;
            PrisonerTenthPRN = string.Empty;
            CellSharingTenthPrisonerId = string.Empty;
        }

        private void ApplyPrisonerSlot(int slotIndex, SACRAPrisonerSlot prisonerSlot)
        {
            if (prisonerSlot == null)
            {
                return;
            }

            switch (slotIndex)
            {
                case 0:
                    PrisonerFirstId = prisonerSlot.PrisonerId;
                    PrisonerFirstName = prisonerSlot.PrisonerName;
                    PrisonerFirstPRN = prisonerSlot.PrisonerPRN;
                    CellSharingFirstPrisonerId = prisonerSlot.CellSharingPrisonerId;
                    break;
                case 1:
                    PrisonerSecondId = prisonerSlot.PrisonerId;
                    PrisonerSecondName = prisonerSlot.PrisonerName;
                    PrisonerSecondPRN = prisonerSlot.PrisonerPRN;
                    CellSharingSecondPrisonerId = prisonerSlot.CellSharingPrisonerId;
                    break;
                case 2:
                    PrisonerThirdId = prisonerSlot.PrisonerId;
                    PrisonerThirdName = prisonerSlot.PrisonerName;
                    PrisonerThirdPRN = prisonerSlot.PrisonerPRN;
                    CellSharingThirdPrisonerId = prisonerSlot.CellSharingPrisonerId;
                    break;
                case 3:
                    PrisonerFourthId = prisonerSlot.PrisonerId;
                    PrisonerFourthName = prisonerSlot.PrisonerName;
                    PrisonerFourthPRN = prisonerSlot.PrisonerPRN;
                    CellSharingFourthPrisonerId = prisonerSlot.CellSharingPrisonerId;
                    break;
                case 4:
                    PrisonerFifthId = prisonerSlot.PrisonerId;
                    PrisonerFifthName = prisonerSlot.PrisonerName;
                    PrisonerFifthPRN = prisonerSlot.PrisonerPRN;
                    CellSharingFifthPrisonerId = prisonerSlot.CellSharingPrisonerId;
                    break;
                case 5:
                    PrisonerSixthId = prisonerSlot.PrisonerId;
                    PrisonerSixthName = prisonerSlot.PrisonerName;
                    PrisonerSixthPRN = prisonerSlot.PrisonerPRN;
                    CellSharingSixthPrisonerId = prisonerSlot.CellSharingPrisonerId;
                    break;
                case 6:
                    PrisonerSeventhId = prisonerSlot.PrisonerId;
                    PrisonerSeventhName = prisonerSlot.PrisonerName;
                    PrisonerSeventhPRN = prisonerSlot.PrisonerPRN;
                    CellSharingSeventhPrisonerId = prisonerSlot.CellSharingPrisonerId;
                    break;
                case 7:
                    PrisonerEighthId = prisonerSlot.PrisonerId;
                    PrisonerEighthName = prisonerSlot.PrisonerName;
                    PrisonerEighthPRN = prisonerSlot.PrisonerPRN;
                    CellSharingEighthPrisonerId = prisonerSlot.CellSharingPrisonerId;
                    break;
                case 8:
                    PrisonerNinthId = prisonerSlot.PrisonerId;
                    PrisonerNinthName = prisonerSlot.PrisonerName;
                    PrisonerNinthPRN = prisonerSlot.PrisonerPRN;
                    CellSharingNinthPrisonerId = prisonerSlot.CellSharingPrisonerId;
                    break;
                case 9:
                    PrisonerTenthId = prisonerSlot.PrisonerId;
                    PrisonerTenthName = prisonerSlot.PrisonerName;
                    PrisonerTenthPRN = prisonerSlot.PrisonerPRN;
                    CellSharingTenthPrisonerId = prisonerSlot.CellSharingPrisonerId;
                    break;
            }
        }

        #endregion

        #region Individual SACRA NTDB changes #57727

        /// <summary>
        /// Assessment type code prisoner or Prisoner Combination
        /// </summary>
        [DataFieldNameAttribute("ASSESSMENT_TYPE_CODE")]
        public string AssessTypeCode
        {
            get { return _assesTypeCode; }
            set { _assesTypeCode = value; }
        }

        /// <summary>
        /// Cell Assessment Id.
        /// </summary>
        [DataFieldNameAttribute("CELL_SHARING_RISK_ASSMNT_ID")]
        public string CellSharingAssessId
        {
            get { return _cellSharingRiskAssId; }
            set { _cellSharingRiskAssId = value; }
        }

        /// <summary>
        /// Offender Id.
        /// </summary>
        [DataFieldNameAttribute("OFFENDERID")]
        public string OffenderId
        {
            get { return _offenderId; }
            set { _offenderId = value; }
        }

        #endregion
    }

  
}


