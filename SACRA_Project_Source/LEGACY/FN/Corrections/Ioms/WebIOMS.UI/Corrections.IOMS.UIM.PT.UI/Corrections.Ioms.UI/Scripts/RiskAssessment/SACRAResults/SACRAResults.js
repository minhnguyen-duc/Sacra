
/*jslint node:true*/

"use strict";

function SacraResult() { }
var sacraRst;
var sacraResultsResources;
var assessmentTypes;
var isCUDAllowed;
var sacraResultUserActionForPrisoner;
var sacraResultUserActionForOtherDetails;
var currenttAlertOffender;
var isNavSessionSave;
var isNavSessionFilter;
var NOT_SUITABLE = "Not Suitable";
var ASSESSMENT_RESULT_7271 = '7271';
var PRISONER_A_FIELD = "prisonerA";
var STRING_1_COLON = '1: ';
var STRING_2_COLON = '2: ';
var BREAK_LINE = '<br>';
var COMMA = ',';
var COMMA_SPACE = ", ";
var GREATER_THAN = "GREATERTHAN";
var MONTH = "M";
var FILTER_PRISONER_FIELD = "txtSacraRptFltPrisoner";
var SACRA_RESULT_GRID = "grdSACRresult";
var SACRA_REPORT = "SACRA Report";
var COMBINED_SACRA_REPORT = "Combined SACRA Report";
var SUCCESS_ALERT_AUTO_CLOSE_DELAY_MS = 3500;

var SACRA_PRISONERS = [
    { order: 0, letter: "A", ordinal: "First", searchCategory: "FirstPrisoner", disabledBinding: "IsFSectiondisabled", inputId: "prisonerA", inputName: "txtSacraRstPrisonerA", validId: "prisonerAValid", idField: "PrisonerFirstId", nameField: "PrisonerFirstName", prnField: "PrisonerFirstPRN", cellSharingField: "CellSharingFirstPrisonerId", tempField: "TempFirstPrisonerId" },
    { order: 1, letter: "B", ordinal: "Second", searchCategory: "SecondPrisoner", disabledBinding: "IsSecondPrisonerdisabled", inputId: "prisonerB", inputName: "txtSacraRstPrisonerB", validId: "prisonerBValid", idField: "PrisonerSecondId", nameField: "PrisonerSecondName", prnField: "PrisonerSecondPRN", cellSharingField: "CellSharingSecondPrisonerId", tempField: "TempSecondPrisonerId" },
    { order: 2, letter: "C", ordinal: "Third", searchCategory: "ThirdPrisoner", disabledBinding: "IsThirdPrisonerdisabled", inputId: "prisonerC", inputName: "txtSacraRstPrisonerC", validId: "prisonerCValid", idField: "PrisonerThirdId", nameField: "PrisonerThirdName", prnField: "PrisonerThirdPRN", cellSharingField: "CellSharingThirdPrisonerId", tempField: "TempThirdPrisonerId" },
    { order: 3, letter: "D", ordinal: "Fourth", searchCategory: "FourthPrisoner", disabledBinding: "IsFourthPrisonerdisabled", inputId: "prisonerD", inputName: "txtSacraRstPrisonerD", validId: "prisonerDValid", idField: "PrisonerFourthId", nameField: "PrisonerFourthName", prnField: "PrisonerFourthPRN", cellSharingField: "CellSharingFourthPrisonerId", tempField: "TempFourthPrisonerId" },
    { order: 4, letter: "E", ordinal: "Fifth", searchCategory: "FifthPrisoner", disabledBinding: "IsFifthPrisonerdisabled", inputId: "prisonerE", inputName: "txtSacraRstPrisonerE", validId: "prisonerEValid", idField: "PrisonerFifthId", nameField: "PrisonerFifthName", prnField: "PrisonerFifthPRN", cellSharingField: "CellSharingFifthPrisonerId", tempField: "TempFifthPrisonerId" },
    { order: 5, letter: "F", ordinal: "Sixth", searchCategory: "SixthPrisoner", disabledBinding: "IsSixthPrisonerdisabled", inputId: "prisonerF", inputName: "txtSacraRstPrisonerF", validId: "prisonerFValid", idField: "PrisonerSixthId", nameField: "PrisonerSixthName", prnField: "PrisonerSixthPRN", cellSharingField: "CellSharingSixthPrisonerId", tempField: "TempSixthPrisonerId" },
    { order: 6, letter: "G", ordinal: "Seventh", searchCategory: "SeventhPrisoner", disabledBinding: "IsSeventhPrisonerdisabled", inputId: "prisonerG", inputName: "txtSacraRstPrisonerG", validId: "prisonerGValid", idField: "PrisonerSeventhId", nameField: "PrisonerSeventhName", prnField: "PrisonerSeventhPRN", cellSharingField: "CellSharingSeventhPrisonerId", tempField: "TempSeventhPrisonerId" },
    { order: 7, letter: "H", ordinal: "Eighth", searchCategory: "EighthPrisoner", disabledBinding: "IsEighthPrisonerdisabled", inputId: "prisonerH", inputName: "txtSacraRstPrisonerH", validId: "prisonerHValid", idField: "PrisonerEighthId", nameField: "PrisonerEighthName", prnField: "PrisonerEighthPRN", cellSharingField: "CellSharingEighthPrisonerId", tempField: "TempEighthPrisonerId" },
    { order: 8, letter: "I", ordinal: "Ninth", searchCategory: "NinthPrisoner", disabledBinding: "IsNinthPrisonerdisabled", inputId: "prisonerI", inputName: "txtSacraRstPrisonerI", validId: "prisonerIValid", idField: "PrisonerNinthId", nameField: "PrisonerNinthName", prnField: "PrisonerNinthPRN", cellSharingField: "CellSharingNinthPrisonerId", tempField: "TempNinthPrisonerId" },
    { order: 9, letter: "J", ordinal: "Tenth", searchCategory: "TenthPrisoner", disabledBinding: "IsTenthPrisonerdisabled", inputId: "prisonerJ", inputName: "txtSacraRstPrisonerJ", validId: "prisonerJValid", idField: "PrisonerTenthId", nameField: "PrisonerTenthName", prnField: "PrisonerTenthPRN", cellSharingField: "CellSharingTenthPrisonerId", tempField: "TempTenthPrisonerId" }
];

window.SacraResult.prototype = {
    FilterRuleCollection: { rules: {} },
    FilterMessageCollection: { messages: {} },
    ResultRuleCollection: { rules: {} },
    ResultMessageCollection: { messages: {} },
    ASSESSMENT_RESULT_NOT_SUITABLE: NOT_SUITABLE,
    Prisoners: SACRA_PRISONERS,
    PanelMode: {
        Default: 0,
        Edit: 1,
        New: 2
    },
    TempFirstPrisonerId: '',
    TempSecondPrisonerId: '',
    TempThirdPrisonerId: '',
    TempFourthPrisonerId: '',
    TempFifthPrisonerId: '',
    TempSixthPrisonerId: '',
    TempSeventhPrisonerId: '',
    TempEighthPrisonerId: '',
    TempNinthPrisonerId: '',
    TempTenthPrisonerId: '',

    // Filter Entity
    sacraResultSearchParamsModel: { AssessmentDateFrom: '', AssessmentDateTo: '', PrisonerId: '', PrisonerName: '' },

    // Result Entity
    sacraResultDetailsModel: {
        AdditionalComments: '', AlertId: '', ApplicationId: '', AssessmentDate: '',
        AssessmentResult: '', AssessmentResultText: '', CellSharingAssessId: '',
        CellSharingFirstPrisonerId: '', CellSharingSecondPrisonerId: '', CellSharingThirdPrisonerId: '',
        CellSharingFourthPrisonerId: '', CellSharingFifthPrisonerId: '', CellSharingSixthPrisonerId: '',
        CellSharingSeventhPrisonerId: '', CellSharingEighthPrisonerId: '', CellSharingNinthPrisonerId: '',
        CellSharingTenthPrisonerId: '', CreatedById: '', CreatedByName: '', CreatedTime: '',
        PrisonerFirstId: '', PrisonerFirstName: '', PrisonerFirstPRN: '',
        PrisonerSecondId: '', PrisonerSecondName: '', PrisonerSecondPRN: '',
        PrisonerThirdId: '', PrisonerThirdName: '', PrisonerThirdPRN: '',
        PrisonerFourthId: '', PrisonerFourthName: '', PrisonerFourthPRN: '',
        PrisonerFifthId: '', PrisonerFifthName: '', PrisonerFifthPRN: '',
        PrisonerSixthId: '', PrisonerSixthName: '', PrisonerSixthPRN: '',
        PrisonerSeventhId: '', PrisonerSeventhName: '', PrisonerSeventhPRN: '',
        PrisonerEighthId: '', PrisonerEighthName: '', PrisonerEighthPRN: '',
        PrisonerNinthId: '', PrisonerNinthName: '', PrisonerNinthPRN: '',
        PrisonerTenthId: '', PrisonerTenthName: '', PrisonerTenthPRN: '',
        PrisonerOrderNum: '', RowCount: '', UpdatedById: '', UpdatedByName: '', UpdatedTime: '',
        UserActionForOtherDetails: '', UserActionForPrisoner: '', IsAutoPopulateSacraReport: false,
        DocumentId: '', DocumentPDF: '', DocumentVersion: 0
    },
    beforeEditResultDetails: null,
    LastDuplicatePrisonerMeta: null,
    // Tracks whether the CombinedSACRA report has already been generated for the current New/Edit session.
    HasGeneratedCombinedReportForSession: false,
    searchType: '',
    searchCategory: {
        Search: 0,
        FirstPrisoner: 1,
        SecondPrisoner: 2,
        ThirdPrisoner: 3,
        FourthPrisoner: 4,
        FifthPrisoner: 5,
        SixthPrisoner: 6,
        SeventhPrisoner: 7,
        EighthPrisoner: 8,
        NinthPrisoner: 9,
        TenthPrisoner: 10
    },
    sacraReportViewModel: '',

    CloneObject: function (obj) {
        return $.extend(true, {}, obj);
    },

    CreateEmptyResultDetailsModel: function () {
        return this.CloneObject(this.sacraResultDetailsModel);
    },

    GetPrisonerMetaByOrder: function (order) {
        var matches = window.JSLINQ(this.Prisoners).Where(function (item) {
            return item.order === order;
        }).Select(function (item) { return item; });
        return matches.items.length > 0 ? matches.items[0] : null;
    },

    GetPrisonerMetaBySearchType: function (searchType) {
        var numericSearchType = parseInt(searchType, 10);
        var matches = window.JSLINQ(this.Prisoners).Where(function (item) {
            return sacraRst.searchCategory[item.searchCategory] === numericSearchType;
        }).Select(function (item) { return item; });
        return matches.items.length > 0 ? matches.items[0] : null;
    },

    GetPrisonerFieldValue: function (order, fieldName) {
        var meta = this.GetPrisonerMetaByOrder(order);
        if (meta === null) {
            return '';
        }
        return $.trim(this.sacraReportViewModel.get('SacraResultDetails.' + meta[fieldName]));
    },

    GetPrisonerId: function (order) {
        return this.GetPrisonerFieldValue(order, 'idField');
    },

    GetPrisonerName: function (order) {
        return this.GetPrisonerFieldValue(order, 'nameField');
    },

    SetPrisonerFields: function (order, prisonerId, prisonerName, prisonerPRN) {
        var meta = this.GetPrisonerMetaByOrder(order);
        if (meta === null) {
            return;
        }

        this.sacraReportViewModel.set('SacraResultDetails.' + meta.idField, prisonerId || '');
        this.sacraReportViewModel.set('SacraResultDetails.' + meta.nameField, prisonerName || '');
        this.sacraReportViewModel.set('SacraResultDetails.' + meta.prnField, prisonerPRN || '');
        this.sacraReportViewModel.set('SacraResultDetails.' + meta.cellSharingField, prisonerId || '');
        this.RefreshPrisonerBindings();
    },

    ClearPrisoner: function (order) {
        this.SetPrisonerFields(order, '', '', '');
    },

    ClearAdditionalPrisoners: function () {
        var index;
        for (index = 1; index < this.Prisoners.length; index++) {
            this.ClearPrisoner(index);
        }
        this.RefreshPrisonerBindings();
    },

    ResetTempPrisonerIds: function () {
        var index;
        for (index = 0; index < this.Prisoners.length; index++) {
            this[this.Prisoners[index].tempField] = '';
        }
    },

    CaptureTempPrisonerIds: function (model) {
        var index;
        for (index = 0; index < this.Prisoners.length; index++) {
            this[this.Prisoners[index].tempField] = model[this.Prisoners[index].idField] || '';
        }
    },

    HasAnyPrisonerFromOrder: function (order) {
        var index;
        for (index = order; index < this.Prisoners.length; index++) {
            if (!uim.ValidateEmptyObject(this.GetPrisonerId(index))) {
                return true;
            }
        }
        return false;
    },

    IsAssessmentSuitableForSharing: function () {
        var combo = $('#cboSacraResultAssessRst').data('kendoComboBox');
        var assessmentResult = $.trim(this.sacraReportViewModel.get('SacraResultDetails.AssessmentResult'));
        if (uim.ValidateEmptyObject(assessmentResult) || combo === undefined || combo.selectedIndex === -1) {
            return false;
        }
        return combo.text() !== this.ASSESSMENT_RESULT_NOT_SUITABLE;
    },

    IsPrisonerSearchDisabled: function (order) {
        if (this.sacraReportViewModel.get('PanelCurrentMode') === this.PanelMode.Default) {
            return true;
        }

        if (order === 0) {
            return false;
        }

        return this.IsAdditionalPrisonerDisabled(order);
    },

    IsAdditionalPrisonerDisabled: function (order) {
        if (this.sacraReportViewModel.get('PanelCurrentMode') === this.PanelMode.Default) {
            return true;
        }

        // Prisoner B is always mandatory/enabled in New/Edit mode, regardless of Assessment Result.
        if (order === 1) {
            return false;
        }

        if (!this.IsAssessmentSuitableForSharing()) {
            return true;
        }

        // Normal sequential rule: previous prisoner selected -> current slot enabled.
        return !(this.GetPrisonerId(order - 1) !== '' || this.GetPrisonerId(order) !== '' || this.HasAnyPrisonerFromOrder(order + 1));
    },

    IsSecondPrisonerRequired: function () {
        return this.sacraReportViewModel.get('PanelCurrentMode') !== this.PanelMode.Default;
    },

    RefreshPrisonerBindings: function () {
        var viewModel = this.sacraReportViewModel;
        if (viewModel === null || viewModel === undefined || viewModel === '') {
            return;
        }

        var index;
        for (index = 0; index < this.Prisoners.length; index++) {
            viewModel.trigger('change', { field: this.Prisoners[index].disabledBinding });
        }
        viewModel.trigger('change', { field: 'IsSecondPrisonerRequired' });
        viewModel.trigger('change', { field: 'DisabledCombinedReportLnk' });
        viewModel.trigger('change', { field: 'DisabledAlertLnk' });
        viewModel.trigger('change', { field: 'IsPrintPreviewDisabled' });
    },

    GetSelectedPrisoners: function () {
        var prisoners = [];
        var index;
        var meta;
        var id;

        for (index = 0; index < this.Prisoners.length; index++) {
            meta = this.Prisoners[index];
            id = this.GetPrisonerId(index);
            if (!uim.ValidateEmptyObject(id)) {
                prisoners.push({
                    order: meta.order,
                    letter: meta.letter,
                    id: id,
                    name: this.GetPrisonerName(index),
                    prn: this.GetPrisonerFieldValue(index, 'prnField')
                });
            }
        }
        return prisoners;
    },

    NormalizeAdditionalPrisonersBeforeSave: function () {
        // Prisoner A and B are always mandatory now; only Prisoners C-J (order 2-9) are compacted/cleared.
        var index;

        if (!this.IsAssessmentSuitableForSharing()) {
            for (index = 2; index < this.Prisoners.length; index++) {
                this.ClearPrisoner(index);
            }
            this.RefreshPrisonerBindings();
            return;
        }

        var additionalPrisoners = [];
        var id;

        for (index = 2; index < this.Prisoners.length; index++) {
            id = this.GetPrisonerId(index);
            if (!uim.ValidateEmptyObject(id)) {
                additionalPrisoners.push({
                    id: id,
                    name: this.GetPrisonerName(index),
                    prn: this.GetPrisonerFieldValue(index, 'prnField')
                });
            }
        }

        for (index = 2; index < this.Prisoners.length; index++) {
            this.ClearPrisoner(index);
        }

        for (index = 0; index < additionalPrisoners.length && index < this.Prisoners.length - 2; index++) {
            this.SetPrisonerFields(index + 2, additionalPrisoners[index].id, additionalPrisoners[index].name, additionalPrisoners[index].prn);
        }
        this.RefreshPrisonerBindings();
    },

    IsAnyPrisonerChanged: function (beforeDetails, model) {
        var index;
        var meta;
        if (beforeDetails === null || beforeDetails === undefined) {
            return false;
        }
        for (index = 0; index < this.Prisoners.length; index++) {
            meta = this.Prisoners[index];
            if ((beforeDetails[meta.idField] || '') !== (model[meta.idField] || '')) {
                return true;
            }
        }
        return false;
    },

    SetUserActionForPrisoners: function (model, beforeEditDetails) {
        if (this.sacraReportViewModel.get('PanelCurrentMode') === this.PanelMode.Edit) {
            if (this.IsAnyPrisonerChanged(beforeEditDetails, model)) {
                model.UserActionForPrisoner = sacraResultUserActionForPrisoner.BothPrisonersUpdated;
            }
            if (beforeEditDetails !== null
                && !uim.ValidateEmptyObject(beforeEditDetails.PrisonerSecondId)
                && uim.ValidateEmptyObject(model.PrisonerSecondId)) {
                model.UserActionForPrisoner = sacraResultUserActionForPrisoner.SecondPrisonerDeleted;
            }
        }
        else if (this.GetSelectedPrisoners().length > 1) {
            model.UserActionForPrisoner = sacraResultUserActionForPrisoner.SecondPrisonerAdded;
        }
        else {
            model.UserActionForPrisoner = sacraResultUserActionForPrisoner.FirstPrisonerAdded;
        }
    },

    GetDuplicatePrisonerSelection: function (order, offenderId) {
        var index;
        var selectedPrisonerId;

        if (uim.ValidateEmptyObject(offenderId)) {
            return null;
        }

        for (index = 0; index < this.Prisoners.length; index++) {
            if (index !== order) {
                selectedPrisonerId = this.GetPrisonerId(index);
                if (!uim.ValidateEmptyObject(selectedPrisonerId) && selectedPrisonerId === offenderId) {
                    return this.Prisoners[index];
                }
            }
        }

        return null;
    },

    IsDuplicatePrisonerSelection: function (order, offenderId) {
        return this.GetDuplicatePrisonerSelection(order, offenderId) !== null;
    },

    GetDuplicatePrisonerMessage: function (duplicateMeta) {
        if (duplicateMeta !== null && duplicateMeta !== undefined) {
            return 'Prisoner ' + duplicateMeta.letter + ' cannot share a cell with himself';
        }

        return sacraResultsResources.DuplicatePrisonerMsg || 'The same prisoner cannot be selected more than once.';
    },

    SetSelectedPrisonerByOrder: function (order, prisoner) {
        var meta = this.GetPrisonerMetaByOrder(order);
        if (meta === null) {
            return;
        }

        if (!uim.ValidateEmptyObject(prisoner)) {
            var duplicateMeta = this.GetDuplicatePrisonerSelection(order, prisoner.OffenderID);
            if (duplicateMeta !== null) {
                uim.FailureAlertMessage(this.GetDuplicatePrisonerMessage(duplicateMeta));
                return false;
            }

            if (!uim.ValidateEmptyObject(prisoner.OffenderID) && this.ValidatePrisonerNTDBIndividualSACRA(prisoner.OffenderID)) {
                var offenderName = prisoner.OffenderName ? $.trim(prisoner.OffenderName) : $.trim(this.DisplayTitleCase(prisoner.DisplayName));
                this.SetPrisonerFields(order, prisoner.OffenderID, offenderName, $.trim(prisoner.PRN));

                if (this.sacraReportViewModel.PanelCurrentMode === this.PanelMode.Edit) {
                    if ((this[meta.tempField] || '') !== prisoner.OffenderID) {
                        this.sacraReportViewModel.set('SacraResultDetails.UserActionForPrisoner',
                            order === 0 ? sacraResultUserActionForPrisoner.FirstPrisonerUpdated : sacraResultUserActionForPrisoner.SecondPrisonerUpdated);
                    }
                }
                else if (this.sacraReportViewModel.PanelCurrentMode === this.PanelMode.New && order > 0) {
                    this.sacraReportViewModel.set('SacraResultDetails.UserActionForPrisoner', sacraResultUserActionForPrisoner.SecondPrisonerAdded);
                }

                var validator = $('#' + meta.validId).kendoValidator().data('kendoValidator');
                validator.hideMessages();

            }
        }
        else {
            this.ClearPrisoner(order);
        }
        this.RefreshPrisonerBindings();
    },

    SetSelectedPrisonerBySearchType: function (prisoner) {
        var meta = this.GetPrisonerMetaBySearchType(this.searchType);
        if (meta === null) {
            return;
        }
        this.SetSelectedPrisonerByOrder(meta.order, prisoner);
    },

    BindPrisonerEnterSearch: function (meta) {
        $('#' + meta.inputId).keydown(function (e) {
            if (currentOffender !== null && e.keyCode === 13) {
                if (sacraRst.IsPrisonerSearchDisabled(meta.order)) {
                    return false;
                }
                $.ajax({
                    type: "GET",
                    contentType: "application/json",
                    traditional: true,
                    cache: false,
                    url: uim.GetUrl() + "RiskAssessmentSACRAResults/GetOffenderDetail",
                    data: { offenderId: currentOffender.OffenderID },
                    success: function (offenderName) {
                        currentOffender.OffenderName = offenderName || currentOffender.DisplayName;
                        sacraRst.SetSelectedPrisonerByOrder(meta.order, currentOffender);
                    }
                });
            }
        }).bind("cut copy paste", function (e) {
            e.preventDefault();
        });
    },

    // on key enter of search
    InitSearchEnter: function () {
        $('#txtSacraRptFltPrisoner').keydown(function (e) {
            if (currentOffender !== null && e.keyCode === 13) {
                $.ajax({
                    type: "GET",
                    contentType: "application/json",
                    traditional: true,
                    cache: false,
                    url: uim.GetUrl() + "RiskAssessmentSACRAResults/GetOffenderDetail",
                    data: { offenderId: currentOffender.OffenderID },
                    success: function (offenderName) {
                        sacraRst.sacraReportViewModel.set("SACRAResultSearchParams.PrisonerName", offenderName || currentOffender.DisplayName);
                        sacraRst.sacraReportViewModel.set("SACRAResultSearchParams.PrisonerId", currentOffender.OffenderID);
                    }
                });
            }
        }).bind("cut copy paste", function (e) {
            e.preventDefault();
        });

        var index;
        for (index = 0; index < this.Prisoners.length; index++) {
            this.BindPrisonerEnterSearch(this.Prisoners[index]);
        }
    },

    // To initialize the page
    Init: function () {
      // Define View Model
        this.sacraReportViewModel = kendo.observable({
            Created: function () {
                var splitCreate;
                var CreatedTimeFromat = uim.ParseJsonDateTime(this.get('SacraResultDetails.CreatedTime'), uim.TimeFormathhmmtt);
                CreatedTimeFromat = CreatedTimeFromat === null ? this.get('SacraResultDetails.CreatedTime') : CreatedTimeFromat;
                var createdByName = this.SacraResultDetails.CreatedByName;
                if (!uim.ValidateEmptyObject(createdByName)) {
                    splitCreate = createdByName.split(",");
                    if (splitCreate.length > 1) {
                        createdByName = uim.NameCase(splitCreate);
                    }
                    return createdByName + ' ' + CreatedTimeFromat;
                }
            },

            Updated: function () {
                var splitUpdate;
                var UpdatedTimeFromat = uim.ParseJsonDateTime(this.get('SacraResultDetails.UpdatedTime'), uim.TimeFormathhmmtt);
                var updatedByName = this.SacraResultDetails.UpdatedByName;
                if (!uim.ValidateEmptyObject(updatedByName)) {
                    splitUpdate = updatedByName.split(",");
                    if (splitUpdate.length > 1) {
                        updatedByName = uim.NameCase(splitUpdate);
                    }
                    return updatedByName + ' ' + UpdatedTimeFromat;
                }
            },
            PanelCurrentMode: sacraRst.PanelMode.Default,
            AssessmentSource: assessmentTypes,
            SACRAResultSearchParams: sacraRst.CloneObject(sacraRst.sacraResultSearchParamsModel),
            SacraResultDetails: sacraRst.CreateEmptyResultDetailsModel(),
            SearchCategory: sacraRst.searchCategory,
            AssessmentTime: '',

            //Function to get the search prisoner pop-up
            SearchPrisoner: function (e) {
                sacraRst.searchType = parseInt($(e.target).data().searchtype, 10);
                if (sacraRst.searchType === sacraRst.searchCategory.Search) {
                    uim.PrisonerSearch(sacraRst.SelectedPrisonerCallBack);
                    return;
                }

                var meta = sacraRst.GetPrisonerMetaBySearchType(sacraRst.searchType);
                if (meta === null || sacraRst.IsPrisonerSearchDisabled(meta.order)) {
                    return false;
                }

                uim.PrisonerSearch(sacraRst.SelectedPrisonerCallBack);
            },

            //Filter button click event
            Filter: function () {
                var RuleName;
                var FilterRulesCollection = sacraRst.FilterRuleCollection;
                uim.ConvertModelJsonDateTime(sacraRst.sacraReportViewModel.get('SACRAResultSearchParams'));
                var dateFrom = $.trim(sacraRst.sacraReportViewModel.get('SACRAResultSearchParams.AssessmentDateFrom'));
                var dateTo = $.trim(sacraRst.sacraReportViewModel.get('SACRAResultSearchParams.AssessmentDateTo'));

                if (dateTo !== '' && dateFrom === '') {
                    RuleName = sacraResultsResources.FilterDateFromRequiredRule;
                    FilterRulesCollection.rules[RuleName] = sacraRst.Validation.FilterDateFromRequired;
                    sacraRst.FilterMessageCollection.messages[RuleName] = sacraResultsResources.FilterDateFromRequiredMsg;
                }
                else {
                    delete FilterRulesCollection.rules.FilterDateFromRequiredRule;
                }

                if (dateTo === '' && dateFrom !== '') {
                    RuleName = sacraResultsResources.FilterDateToRequiredRule;
                    FilterRulesCollection.rules[RuleName] = sacraRst.Validation.FilterDateToRequired;
                    sacraRst.FilterMessageCollection.messages[RuleName] = sacraResultsResources.FilterDateToRequiredMsg;
                }
                else {
                    delete FilterRulesCollection.rules.FilterDateToRequiredRule;
                }

                var container = $("#dvSacraResultFlt");
                container.kendoValidator({
                    rules: FilterRulesCollection.rules,
                    messages: sacraRst.FilterMessageCollection.messages
                });

                var validator = $("#dvSacraResultFlt").data("kendoValidator");
                if (validator.validate()) {
                    sacraRst.GetData(this.SACRAResultSearchParams);
                    isNavSessionFilter = true;
                }
            },
            //Clear button click event
            Clear: function () {
                sacraRst.PopulateGrid([]);
                this.set('SACRAResultSearchParams', sacraRst.CloneObject(sacraRst.sacraResultSearchParamsModel));
                sacraRst.Validation.HideFilterMsg();
            },
            //Assessment Result Combo box Change Event
            AssessmentResultChange: function () {
                // Prisoner B is always mandatory regardless of Assessment Result; only clear Prisoners C-J.
                if (this.get('SacraResultDetails.AssessmentResult').length === 0 || !sacraRst.IsAssessmentSuitableForSharing()) {
                    var index;
                    for (index = 2; index < sacraRst.Prisoners.length; index++) {
                        sacraRst.ClearPrisoner(index);
                    }
                }
                sacraRst.RefreshPrisonerBindings();
            },
            //Toggle report link disable state
            DisabledReportLnk: function () {
                return this.get('PanelCurrentMode') !== sacraRst.PanelMode.Default;
            },
            //disable Combined SACRA Report
            DisabledCombinedReportLnk: function () {
                return sacraRst.sacraReportViewModel.get('PanelCurrentMode') !== sacraRst.PanelMode.Default
                    || sacraRst.GetSelectedPrisoners().length === 0;
            },
            //Toggle alert link disable state
            DisabledAlertLnk: function () {
                var result = false;
                if (this.get('PanelCurrentMode') !== sacraRst.PanelMode.Default ||
                    this.get('SacraResultDetails.AssessmentResult') === ''
                    || this.get('SacraResultDetails.AssessmentResult') !== ASSESSMENT_RESULT_7271) {
                    result = true;
                }
                if (!result && this.get('SacraResultDetails') !== null) {
                    currenttAlertOffender = this.get('SacraResultDetails.PrisonerFirstId');
                }

                return result;
            },

            AlertClick: function () {
                if (currenttAlertOffender !== '') {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json",
                        traditional: true,
                        cache: false,
                        url: uim.GetUrl() + "RiskAssessmentSACRAResults/SetCurrentOffender",
                        data: JSON.stringify({ prisonerId: currenttAlertOffender }),
                        success: function () {
                            uim.ScreenRedirection("headerscreens/alerts");
                        }
                    });
                }
            },
            //Toggle task buttons disable state
            IsTaskDisabled: function () {
                return this.get('PanelCurrentMode') !== sacraRst.PanelMode.Default;
            },

            FirstSectionDisabled: true,
            IsFSectiondisabled: function () {
                this.FirstSectionDisabled = (this.get('PanelCurrentMode') === sacraRst.PanelMode.Default);
                return this.get('FirstSectionDisabled');
            },

            IsSecondPrisonerdisabled: function () { return sacraRst.IsAdditionalPrisonerDisabled(1); },
            IsThirdPrisonerdisabled: function () { return sacraRst.IsAdditionalPrisonerDisabled(2); },
            IsFourthPrisonerdisabled: function () { return sacraRst.IsAdditionalPrisonerDisabled(3); },
            IsFifthPrisonerdisabled: function () { return sacraRst.IsAdditionalPrisonerDisabled(4); },
            IsSixthPrisonerdisabled: function () { return sacraRst.IsAdditionalPrisonerDisabled(5); },
            IsSeventhPrisonerdisabled: function () { return sacraRst.IsAdditionalPrisonerDisabled(6); },
            IsEighthPrisonerdisabled: function () { return sacraRst.IsAdditionalPrisonerDisabled(7); },
            IsNinthPrisonerdisabled: function () { return sacraRst.IsAdditionalPrisonerDisabled(8); },
            IsTenthPrisonerdisabled: function () { return sacraRst.IsAdditionalPrisonerDisabled(9); },
            IsSecondPrisonerRequired: function () { return sacraRst.IsSecondPrisonerRequired(); },

            NewDisabled: false,
            EditDisabled: false,
            SaveDisabled: false,
            CancelDisabled: false,
            IsPrintPreviewDisabled: function () {
                if (this.get('PanelCurrentMode') !== sacraRst.PanelMode.Default) {
                    return true;
                }
                return uim.ValidateEmptyObject(this.get('SacraResultDetails.ApplicationId'));
            },
            //Function To enable / disable new icon
            IsNewDisabled: function () {
                this.set('NewDisabled', !(isCUDAllowed.isCUDAllowed && isCUDAllowed.isCreateAllowed
                    && this.get('PanelCurrentMode') === sacraRst.PanelMode.Default));
                return this.get('NewDisabled');
            },
            //Function To enable / disable edit icon
            IsEditDisabled: function () {
                this.set('EditDisabled', !(isCUDAllowed.isCUDAllowed && isCUDAllowed.isEditAllowed
                    && this.get('PanelCurrentMode') === sacraRst.PanelMode.Default
                    && this.get('SacraResultDetails.ApplicationId').length > 0));
                return this.get('EditDisabled');
            },
            //Function To enable / disable save icon
            IsSaveDisabled: function () {
                this.set('SaveDisabled', (this.get('PanelCurrentMode') === sacraRst.PanelMode.Default));
                return this.get('SaveDisabled');
            },

            //Function To enable / disable cancel icon

            IsCancelDisabled: function () {
                this.set('CancelDisabled', !(this.get('PanelCurrentMode') !== sacraRst.PanelMode.Default));
                return this.get('CancelDisabled');
            },
            // New icon click event
            New: function () {
                uim.GetCurrentInput = PRISONER_A_FIELD;
                uim.SetSaveChanges(sacraRst.sacraReportViewModel.Save, sacraRst.ActionCancel);

                if (!this.get('NewDisabled')) {
                    uim.DisableGrid('grdSACRresult');
                    this.set('PanelCurrentMode', sacraRst.PanelMode.New);
                    sacraRst.Validation.HideFilterMsg();
                    this.set('SacraResultDetails', sacraRst.CreateEmptyResultDetailsModel());
                    sacraRst.ResetTempPrisonerIds();
                    sacraRst.HasGeneratedCombinedReportForSession = false;
                    sacraRst.sacraReportViewModel.set('SacraResultDetails.AssessmentDate', new Date());
                    sacraRst.sacraReportViewModel.set('AssessmentTime', new Date());
                    sacraRst.RefreshPrisonerBindings();
                }
                else {
                    return false;
                }
                sacraRst.Validation.Init();
                uim.SetFocusToEditableSection('prisonerAValid', false);
                uim.SetSaveChanges(sacraRst.sacraReportViewModel.Save, sacraRst.ActionCancel);
            },
            //Edit icon click event
            Edit: function () {
                uim.GetCurrentInput = PRISONER_A_FIELD;

                if (!this.get('EditDisabled')) {
                    var model = this.get('SacraResultDetails');
                    sacraRst.beforeEditResultDetails = sacraRst.GetRequiredPropertyForModel(model, sacraRst.sacraResultDetailsModel);
                    var diffHour = (window.moment().diff(new Date(kendo.parseDate(model.AssessmentDate)), 'hour'));
                    var isEditable = ((diffHour < 4) ? true : false);
                    var isEditingUser = (parseInt(model.CreatedById, 10) === userDetails.UserId ? true : false);

                    if (isEditable && isEditingUser) {
                        uim.DisableGrid('grdSACRresult');
                        this.set('PanelCurrentMode', sacraRst.PanelMode.Edit);
                        sacraRst.Validation.HideFilterMsg();
                        sacraRst.CaptureTempPrisonerIds(model);
                        sacraRst.HasGeneratedCombinedReportForSession = false;
                        sacraRst.sacraReportViewModel.set('SacraResultDetails.AssessmentDate', new Date());
                        sacraRst.sacraReportViewModel.set('AssessmentTime', new Date());
                        sacraRst.Validation.Init();
                        sacraRst.RefreshPrisonerBindings();
                        uim.SetSaveChanges(sacraRst.sacraReportViewModel.Save, sacraRst.ActionCancel);
                    }
                    else {
                        var errorMsg = '';

                        if (!isEditable && !isEditingUser) {
                            errorMsg = STRING_1_COLON + sacraResultsResources.EditRuleType1Msg + BREAK_LINE + STRING_2_COLON + sacraResultsResources.EditRuleType2Msg;
                        }
                        if (!isEditable && isEditingUser) {
                            errorMsg = sacraResultsResources.EditRuleType1Msg;
                        }
                        if (!isEditingUser && isEditable) {
                            errorMsg = sacraResultsResources.EditRuleType2Msg;
                        }

                        uim.FailureAlertMessage(errorMsg);
                    }
                } else {
                    return false;
                }
                uim.SetFocusToEditableSection('dvSacraResult', false);
            },
            //To set the assessed date
            SetSacraDate: function (sacraDate, sacraTime) {
                var currentDate = sacraDate.getDate();
                var currentMonth = sacraDate.getMonth();
                var currentYear = sacraDate.getFullYear();
                var currentHour = sacraTime.getHours();
                var currentMinute = sacraTime.getMinutes();

                return new Date(currentYear, currentMonth, currentDate, currentHour, currentMinute, 0, 0);
            },
            //On Print Preview global icon click
            OnPrintPreviewClick: function () {
                var documentId = sacraRst.sacraReportViewModel.get('SacraResultDetails.DocumentId');
                if (!uim.ValidateEmptyObject(documentId)) {
                    UIMDocumentViewer.LoadIOMSDocument(documentId);
                    return;
                }
                var cellSharingAssessId = sacraRst.sacraReportViewModel.get('SacraResultDetails.ApplicationId');
                $.ajax({
                    url: uim.GetUrl() + 'riskassessmentsacraresults/GetSACRAReportDocumentInfo',
                    type: 'POST',
                    cache: false,
                    data: {
                        cellSharingAssessId: cellSharingAssessId
                    },
                    success: function (data) {
                        console.log(data);
                        if (data !== null && data.IsError === false) {
                            sacraRst.sacraReportViewModel.set('SacraResultDetails.DocumentId', data.DocumentId);

                            sacraRst.sacraReportViewModel.set('SacraResultDetails.DocumentVersion', data.DocumentVersion || 0);

                            UIMDocumentViewer.LoadIOMSDocument(sacraRst.sacraReportViewModel.get('SacraResultDetails.DocumentId'));
                           
                        }
                        else {
                            uim.InfoAlertMessage(sacraResultsResources.NoDocumentMsg);
                            return;

                        }
                    },
                    error: function () {
                        uim.FailureAlertMessage(Resources.FormErrors);
                    }
                });
            },
                
           
            //Save icon click event
            Save: function () {
                if (sacraRst.sacraReportViewModel.get('SaveDisabled')) {
                    return;
                }

                sacraRst.NormalizeAdditionalPrisonersBeforeSave();

                var mode = sacraRst.sacraReportViewModel.get('PanelCurrentMode');
                var model = sacraRst.sacraReportViewModel.get('SacraResultDetails');
                var beforeEditDetails = sacraRst.beforeEditResultDetails;
                var prisonersChanged = mode !== sacraRst.PanelMode.Edit || sacraRst.IsAnyPrisonerChanged(beforeEditDetails, model);

                if (prisonersChanged && !sacraRst.HasGeneratedCombinedReportForSession && sacraRst.GetSelectedPrisoners().length > 1) {
                    var confirmMsg = sacraResultsResources.CombinedSacraReportConfirmMsg
                        || 'You must run the Combined SACRA report before you save the Assessment Result. Do you want to run the Combined SACRA report?';
                    uim.WarningAlertMessage(confirmMsg, function () {
                        sacraRst.GenerateCombinedReportBeforeSave();
                    });
                    return;
                }

                sacraRst.PersistSacraResult();
            },
            //Cancel icon click event
            Cancel: function () {
                if (!this.get('CancelDisabled')) {
                    uim.WarningAlertMessage(Resources.CancelValidationMessage, sacraRst.ActionCancel);
                }
                else { return false; }
            },

            OnCombinedSACRAReportClick: function () {
                sacraRst.OnCombinedSACRAReportClick();
            }
        });
        kendo.bind($('#dvSacraResult'), this.sacraReportViewModel);
        kendo.bind($('#dvSacraResultPageHeader'), this.sacraReportViewModel);
        kendo.bind($('#dvSacraResultTaskSec'), this.sacraReportViewModel);
        // Initialize Grid
        sacraRst.PopulateGrid([]);
        this.Validation.Init();
        sacraRst.GetSacraSessionData();
        sacraRst.RefreshPrisonerBindings();
    },
    //Function to get the session values on breadcrumb navigation
    GetSacraSessionData: function () {
        $.ajax({
            type: "GET",
            contentType: "application/json",
            traditional: true,
            cache: false,
            url: uim.GetUrl() + "RiskAssessmentSACRAResults/GetSacraSessionData",
            success: function (result) {
                if (result !== undefined && result !== null && !uim.ValidateEmptyObject(result.sacraFilterCriteria)) {
                    if (uim.ParseJsonDate(result.sacraFilterCriteria.AssessmentDateFrom) != '') {
                        sacraRst.sacraReportViewModel.set('SACRAResultSearchParams.AssessmentDateFrom', kendo.parseDate(result.sacraFilterCriteria.AssessmentDateFrom));
                    }
                    if (uim.ParseJsonDate(result.sacraFilterCriteria.AssessmentDateTo) != '') {
                        sacraRst.sacraReportViewModel.set('SACRAResultSearchParams.AssessmentDateTo', kendo.parseDate(result.sacraFilterCriteria.AssessmentDateTo));
                    }

                    sacraRst.sacraReportViewModel.set("SACRAResultSearchParams.PrisonerName", result.sacraFilterCriteria.PrisonerName);
                    sacraRst.sacraReportViewModel.set("SACRAResultSearchParams.PrisonerId", result.sacraFilterCriteria.PrisonerId);

                    if (!uim.ValidateEmptyObject(result.sacraGridData)) {
                        sacraRst.PopulateGrid(result.sacraGridData);
                    }
                }
            }
        });
    },
    //For the cancel operation
    ActionCancel: function () {
        uim.EnableDisabledGrid('grdSACRresult');
        sacraRst.beforeEditResultDetails = null;
        sacraRst.HasGeneratedCombinedReportForSession = false;
        sacraRst.sacraReportViewModel.set('PanelCurrentMode', sacraRst.PanelMode.Default);
        var grid = $('#grdSACRresult').data('kendoGrid');
        $(window).scrollTop(0);

        if (grid.dataSource.data().length > 0) {
            if (grid.dataSource.page() !== 1) {
                grid.dataSource.page(1);
            }
            grid.select('tr:eq(1)');
            grid.tbody.find(">tr:first").trigger('click');
        }
        else {
            sacraRst.sacraReportViewModel.set('SacraResultDetails', sacraRst.CreateEmptyResultDetailsModel());
        }

        sacraRst.RefreshPrisonerBindings();
        sacraRst.Validation.HideSearchMsg();
        sacraRst.Validation.InitRules();
    },
    //Get sacra result details
    GetData: function (searchData) {
        $.ajax({
            url: uim.GetUrl() + 'riskassessmentsacraresults/getresult',
            type: "POST",
            contentType: "application/json",
            cache: false,
            data: JSON.stringify(searchData),
            success: function (data) {
                if (data !== null && sacraRst.ShowErrorList(data)) {
                    isNavSessionSave = false;
                    sacraRst.PopulateGrid(data.SacraResults);
                }
            },
            error: function () {
                uim.FailureAlertMessage(Resources.FormErrors);
            }
        });
    },
    //To show the Error list in pop-up
    ShowErrorList: function (data) {
        if (data.IsError) {
            uim.FailureAlertMessage(data.ErrorsList);
            return false;
        }
        return true;
    },
    //Get assessment text by id
    ParseAssessmentText: function (id) {
        var name = window.JSLINQ(assessmentTypes)
            .Where(function (item) { return item.Value === id; })
            .Select(function (item) { return item.DisplayText; });
        if (name.items.length > 0) {
            return name.items[0];
        }
        return '';
    },
    //To populate the sacra result grid
    PopulateGrid: function (dataSource) {
        var gridControls = uim.GetKendoGridControls('grdSACRresult', false);
        var gridModel = [];
        if (dataSource && dataSource.length > 0) {
            var i;
            for (i = 0; i < dataSource.length; i++) {
                gridModel[i] = dataSource[i];
                gridModel[i].AssessmentResultText = sacraRst.ParseAssessmentText(dataSource[i].AssessmentResult);
            }
            dataSource = gridModel;
        }

        var girdDataSource = new kendo.data.DataSource({
            pageSize: uim.DefaultKendoGridPageSize,
            data: dataSource,
            autoSync: true,
            schema: {
                model: {
                    fields: {
                        AssessmentDate: {
                            editable: false, format: uim.DateStringFormatddMMyyyyhhmmtt, type: 'date'
                        }
                    }
                }
            },
            sort: {
                field: "AssessmentDate",
                dir: 'desc'
            }
        });

        var columns = [];
        var index;
        for (index = 0; index < sacraRst.Prisoners.length; index++) {
            // Layout 2: only Prisoner A and B columns are visible, C-J remain hidden.
            columns.push({ field: sacraRst.Prisoners[index].nameField, title: "Prisoner " + sacraRst.Prisoners[index].letter, hidden: index > 1 });
        }
        columns.push({ field: "AssessmentResultText", title: "Result", sortable: true, filterable: true });
        columns.push({
            field: "AssessmentDate", title: "Date", width: "180px", type: 'date',
            template: "#= uim.ParseJsonDateTime(AssessmentDate, uim.TimeFormathhmmtt) #",
            filterable: {
                ui: uim.FilterDateTimePicker
            }
        });

        uim.BindkendoDataSourceGridWithEvents('grdSACRresult', girdDataSource, columns, sacraRst.GridRowClick);
        var grid = $('#grdSACRresult').data('kendoGrid');

        if (dataSource.length > 0) {
            if (isNavSessionSave || isNavSessionSave === undefined) {
                if (uim.ValidateEmptyObject(gridControls.Sort)) {
                    grid.dataSource.sort({ field: "AssessmentDate", dir: "desc" });
                }
            }
        }

        if (isNavSessionSave || isNavSessionFilter) {
            var sessionData = [];
            var j;
            for (j = 0; j < dataSource.length; j++) {
                sessionData.push(uim.ConvertModelJsonDateTime(dataSource[j]));
            }
            uim.AjaxPost(sessionData, 'RiskAssessmentSACRAResults/SessionOnNaviagte', sacraRst.SessionOnNaviagtion);
        }
    },
    //Sacra result grid data bound event
    OnDataBound: function () {
        sacraRst.GridRowClick();
        this.select(this.tbody.find(">tr:first"));
        this.tbody.find(">tr:first").trigger('click');
    },
    //To update the sacra result grid data source
    UpdateGridDataSource: function (data) {
        var gridModel = sacraRst.sacraResultDetailsModel;
        gridModel = data;
        gridModel.AssessmentResultText = sacraRst.ParseAssessmentText(data.AssessmentResult);
        data = gridModel;
        var grid = $('#grdSACRresult').data('kendoGrid');
        var dataSource = grid.dataSource.data();
        if (sacraRst.sacraReportViewModel.get('PanelCurrentMode') === sacraRst.PanelMode.Edit) {

            if (dataSource.length > 0) {
                var row = grid.select();
                var selectedData = grid.dataItem(row);
                data.AssessmentDate = new Date(kendo.toString(kendo.parseDate(data.AssessmentDate)));
                $.each(data, function (key, value) {
                    if (selectedData.hasOwnProperty(key)) {
                        selectedData[key] = value;
                    }
                });
            }
        } else {
            data.AssessmentDate = new Date(kendo.toString(kendo.parseDate(data.AssessmentDate)));
            dataSource.push(data);
        }

        var gridControls = uim.GetKendoGridControls('grdSACRresult', false);
        if (uim.ValidateEmptyObject(gridControls.Sort)) {
            grid.dataSource.sort({ field: "AssessmentDate", dir: "desc" });
        }

        var sessionData = [];
        var i;
        for (i = 0; i < dataSource.length; i++) {
            sessionData.push(uim.ConvertModelJsonDateTime(dataSource[i]));
        }
        uim.AjaxPost(sessionData, 'RiskAssessmentSACRAResults/SessionOnNaviagte', sacraRst.SessionOnNaviagtion);
    },
    //On Ajax Success of the value store in  session
    SessionOnNaviagtion: function (rdata) {
        if (rdata === true) {
            isNavSessionSave = true;
            return true;
        }
    },
    //Sacra result grid row click event
    GridRowClick: function () {
        var grid = $('#grdSACRresult').data("kendoGrid");

        if (grid.dataSource.data().length > 0) {
            var applicationGrid = $("#grdSACRresult").data("kendoGrid");
            var row = applicationGrid.dataItem(applicationGrid.select());
            var model = {};
            if (!uim.ValidateEmptyObject(row)) {
                model = sacraRst.GetRequiredPropertyForModel(row, sacraRst.sacraResultDetailsModel);
                sacraRst.CaptureTempPrisonerIds(model);
                sacraRst.sacraReportViewModel.set('AssessmentTime', uim.ParseJsonDateToTime(row.AssessmentDate, uim.TimeFormathhmmtt));
            }
            sacraRst.sacraReportViewModel.set('SacraResultDetails', model);
        }
        else {
            sacraRst.sacraReportViewModel.set('SacraResultDetails', sacraRst.CreateEmptyResultDetailsModel());
        }
        sacraRst.RefreshPrisonerBindings();
    },
    //To get the required property for the model
    GetRequiredPropertyForModel: function (fObj, tObj) {
        var model = this.CreateEmptyResultDetailsModel();
        $.each(fObj, function (key, value) {
            if (tObj.hasOwnProperty(key)) {
                model[key] = value;
            }
        });
        return model;
    },
    //Filter - prisoner text box key press event callback
    PrisonerKPressCBack: function (prisoner) {
        if (prisoner !== null) {
            sacraRst.sacraReportViewModel.set("SACRAResultSearchParams.PrisonerName", $.trim(prisoner.OffenderName || prisoner.DisplayName));
            sacraRst.sacraReportViewModel.set("SACRAResultSearchParams.PrisonerId", prisoner.OffenderID);
            var validator = $("#prisonerFilValid").kendoValidator().data("kendoValidator");
            validator.hideMessages();
        }
        else {
            sacraRst.sacraReportViewModel.set("SACRAResultSearchParams.PrisonerName", '');
            sacraRst.sacraReportViewModel.set("SACRAResultSearchParams.PrisonerId", '');
        }
    },
    //Prisoner A - J text box key press event callback


    FirstPrisonerKPressCBack: function (prisoner) { sacraRst.SetSelectedPrisonerByOrder(0, prisoner); },
    SecondPrisonerKPressCBack: function (prisoner) { sacraRst.SetSelectedPrisonerByOrder(1, prisoner); },
    ThirdPrisonerKPressCBack: function (prisoner) { sacraRst.SetSelectedPrisonerByOrder(2, prisoner); },
    FourthPrisonerKPressCBack: function (prisoner) { sacraRst.SetSelectedPrisonerByOrder(3, prisoner); },
    FifthPrisonerKPressCBack: function (prisoner) { sacraRst.SetSelectedPrisonerByOrder(4, prisoner); },
    SixthPrisonerKPressCBack: function (prisoner) { sacraRst.SetSelectedPrisonerByOrder(5, prisoner); },
    SeventhPrisonerKPressCBack: function (prisoner) { sacraRst.SetSelectedPrisonerByOrder(6, prisoner); },
    EighthPrisonerKPressCBack: function (prisoner) { sacraRst.SetSelectedPrisonerByOrder(7, prisoner); },
    NinthPrisonerKPressCBack: function (prisoner) { sacraRst.SetSelectedPrisonerByOrder(8, prisoner); },
    TenthPrisonerKPressCBack: function (prisoner) { sacraRst.SetSelectedPrisonerByOrder(9, prisoner); },

    GetSelectedPrisonersFromDetails: function (details) {
        var prisoners = [];
        var index;
        var meta;
        var id;

        details = details || {};

        for (index = 0; index < this.Prisoners.length; index++) {
            meta = this.Prisoners[index];
            id = $.trim(details[meta.idField] || '');
            if (!uim.ValidateEmptyObject(id)) {
                prisoners.push({
                    order: meta.order,
                    letter: meta.letter,
                    id: id,
                    name: details[meta.nameField] || '',
                    prn: details[meta.prnField] || ''
                });
            }
        }

        return prisoners;
    },

    OpenCombinedSACRAReportForDetails: function (details, isAutoPopulate) {
        var selectedPrisoners = this.GetSelectedPrisonersFromDetails(details);
        if (selectedPrisoners.length <= 1) {
            return false;
        }

        var cellSharingAssessId = details.ApplicationId;
        var prisonerIds = $.map(selectedPrisoners, function (item) { return item.id; });

        $.ajax({
            type: "POST",
            contentType: "application/json",
            traditional: true,
            cache: false,
            url: uim.GetUrl() + 'RiskAssessmentSACRAResults/SetReportParameter',
            data: JSON.stringify({
                cellSharingAssessId: cellSharingAssessId,
                prisonerId: '',
                prisonerIds: prisonerIds,
                isAutoPopulate: isAutoPopulate === true
            }),
            success: function () {
                uim.ReportViewer('RiskAssessmentSACRAResults/GetCombinedSACRAReport', COMBINED_SACRA_REPORT);
            }
        });

        return true;
    },
    //Auto populate combined sacra
    GenerateCombinedReportBeforeSave: function () {
        var model = this.sacraReportViewModel.get('SacraResultDetails');
        var opened = this.OpenCombinedSACRAReportForDetails(model, true);
        if (opened) {
            this.RunAfterSuccessPopupClosed(function () {
                sacraRst.HasGeneratedCombinedReportForSession = true;
            });
        }
    },

    //Persist the SACRA result record (called directly, or once the CombinedSACRA report confirmation has been satisfied)
    PersistSacraResult: function () {
        sacraRst.Validation.InitRules();
        var validator = $("#dvSacraResult").data("kendoValidator");
        if (!validator.validate()) {
            uim.SetFocusToErrorMsg("dvSacraResult", 350);
            return false;
        }

        if (sacraRst.sacraReportViewModel.get('SacraResultDetails.CreatedTime') !== "") {
            var CreatedDateTime = uim.ParseJsonDateTime(sacraRst.sacraReportViewModel.get('SacraResultDetails.CreatedTime'), 'hh:mm tt');
            sacraRst.sacraReportViewModel.set('SacraResultDetails.CreatedTime', CreatedDateTime);
        }

        sacraRst.sacraReportViewModel.set("SacraResultDetails.AssessmentDate",
            uim.ParseJsonDateTimeinAMPMFormat(sacraRst.sacraReportViewModel.SetSacraDate(sacraRst.sacraReportViewModel.get("SacraResultDetails.AssessmentDate"),
                sacraRst.sacraReportViewModel.get("AssessmentTime")), uim.TimeFormathhmm));

        var model = sacraRst.sacraReportViewModel.get('SacraResultDetails');
        var beforeEditDetails = sacraRst.beforeEditResultDetails;

        sacraRst.SetUserActionForPrisoners(model, beforeEditDetails);

        if (sacraRst.sacraReportViewModel.get('PanelCurrentMode') === sacraRst.PanelMode.Edit) {
            if (beforeEditDetails !== null &&
                (model.AssessmentResult !== beforeEditDetails.AssessmentResult ||
                    model.AdditionalComments !== beforeEditDetails.AdditionalComments ||
                    model.AssessmentDate !== beforeEditDetails.AssessmentDate)) {
                model.UserActionForOtherDetails = sacraResultUserActionForOtherDetails.OtherDetailsUpdated;
            }
        }

        $.ajax({
            url: uim.GetUrl() + 'riskassessmentsacraresults/save',
            type: "POST",
            contentType: "application/json",
            cache: false,
            data: JSON.stringify({
                mode: sacraRst.sacraReportViewModel.PanelCurrentMode, resultDetails: model,
                assessmentResultText: $('#cboSacraResultAssessRst').data('kendoComboBox').text()
            }),
            success: function (data) {
                uim.ExitRedirect();
                // To set the current grid settings
                uim.SetKendoGridControls(data.CurrentGridName, data.UniqueKeyName, data.UniqueKeyValue);
                if (data !== null && sacraRst.ShowErrorList(data)) {
                    var savedResultDetails = data.SacraResults || model;

                    sacraRst.sacraReportViewModel.set('SacraResultDetails.IsAutoPopulateSacraReport', false);
                    $(window).scrollTop(0);
                    uim.EnableDisabledGrid('grdSACRresult');
                    sacraRst.UpdateGridDataSource(savedResultDetails);
                    sacraRst.sacraReportViewModel.set('SacraResultDetails',
                        sacraRst.GetRequiredPropertyForModel(savedResultDetails, sacraRst.sacraResultDetailsModel));
                    sacraRst.sacraReportViewModel.set('PanelCurrentMode', sacraRst.PanelMode.Default);
                    sacraRst.HasGeneratedCombinedReportForSession = false;
                    sacraRst.RefreshPrisonerBindings();

                    uim.SuccessAlertMessage(Resources.SaveSuccessMsg);
                }
            }
        });
    },

    RunAfterSuccessPopupClosed: function (callback) {
        var isHandled = false;
        var closeSelectors = [
            '.k-window-action[aria-label="Close"]',
            '.k-window-titlebar .k-i-close',
            '.k-dialog-close',
            '.k-notification .k-i-close',
            '.k-notification-wrap .k-i-close',
            '.ui-dialog-titlebar-close',
            '.bootbox-close-button'
        ].join(',');

        var runCallback = function () {
            if (isHandled) {
                return;
            }
            isHandled = true;
            $(document).off('click.sacraSaveSuccessClose', closeSelectors);
            window.clearTimeout(fallbackTimer);
            callback();
        };

        $(document).one('click.sacraSaveSuccessClose', closeSelectors, function () {
            window.setTimeout(runCallback, 0);
        });

        var fallbackTimer = window.setTimeout(runCallback, SUCCESS_ALERT_AUTO_CLOSE_DELAY_MS);
    },
    //Validate Individual SACRA
    ValidatePrisonerNTDBIndividualSACRA: function (offenderId) {
        var isIndividualSACRAExist;
        var isActiveNTDBAlert = uim.ValidateNTDBAlertActive(offenderId);
        if (!uim.ValidateEmptyObject(isActiveNTDBAlert) && !isActiveNTDBAlert) {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                traditional: true,
                cache: true,
                async: false,
                data: JSON.stringify({ offenderId: offenderId }),
                url: uim.GetUrl() + 'RiskAssessmentSACRAResults/ValidateIndividualSACRA',
                success: function (result) {
                    if (!result.isError) {
                        if (result.isIndividualSACRARecorded) {
                            isIndividualSACRAExist = result.isIndSACRAExist;
                            if (!uim.ValidateEmptyObject(isIndividualSACRAExist) && !isIndividualSACRAExist) {
                                uim.InfoAlertMessage(sacraResultsResources.IndividualSACRANTDBMsg);
                            }
                        } else {
                            uim.InfoAlertMessage(sacraResultsResources.IndividualSACRARecordedMsg);
                            isIndividualSACRAExist = false;
                        }
                    }
                }
            });
            return isIndividualSACRAExist;
        } else {
            uim.InfoAlertMessage(sacraResultsResources.NTDBMsg);
            return false;
        }
    },
    //On Combined SACRA Report click
    OnCombinedSACRAReportClick: function () {
        var model = this.sacraReportViewModel.get('SacraResultDetails');
        var selectedPrisoners = this.GetSelectedPrisonersFromDetails(model);
        if (selectedPrisoners.length === 0) {
            return false;
        }

        if (selectedPrisoners.length > 1) {
            var opened = this.OpenCombinedSACRAReportForDetails(model, false);
            if (opened) {
                this.RunAfterSuccessPopupClosed(function () {
                    sacraRst.HasGeneratedCombinedReportForSession = true;
                });
            }
            return opened;
        }

        $.ajax({
            type: "POST",
            contentType: "application/json",
            traditional: true,
            cache: false,
            url: uim.GetUrl() + 'RiskAssessmentSACRAResults/SetReportParameter',
            data: JSON.stringify({
                cellSharingAssessId: model.CellSharingAssessId || model.ApplicationId,
                prisonerId: selectedPrisoners[0].id,
                prisonerIds: [selectedPrisoners[0].id],
                isAutoPopulate: false
            }),
            success: function () {
                uim.ReportViewer('RiskAssessmentSACRAReport/GetSACRAReport?id=' + Math.random(), SACRA_REPORT);
            }
        });
    },
    //function to display name in title case
    DisplayTitleCase: function (fullName) {
        var nameArray = fullName.split(COMMA);
        if (nameArray.length < 2) {
            return $.trim(uim.ToTitleCase(fullName));
        }
        var firstName = nameArray[0];
        var lastName = $.trim(uim.ToTitleCase(nameArray[1]));
        return firstName + COMMA_SPACE + lastName;
    },
    //Call back event after select prisoner
    SelectedPrisonerCallBack: function (prisoner) {
        $.ajax({
            type: "GET",
            contentType: "application/json",
            traditional: true,
            cache: false,
            url: uim.GetUrl() + "RiskAssessmentSACRAResults/GetOffenderDetail",
            data: { offenderId: prisoner.OffenderID },
            success: function (offenderName) {
                prisoner.OffenderName = offenderName;
                if (sacraRst.searchType === sacraRst.searchCategory.Search) {
                    sacraRst.PrisonerKPressCBack(prisoner);
                }
                else {
                    sacraRst.SetSelectedPrisonerBySearchType(prisoner);
                }
            }
        });
    },
    //Validation module
    Validation: {
        // Check prisoner A is empty
        PrisonerARequired: function (input) {
            return uim.EmptyTextValidation(input, '[name=txtSacraRstPrisonerA]');
        },
        // Check assessment result is empty
        AssessmentResultRequired: function (input) {
            return uim.EmptyTextValidation(input, '[name=cboSacraResultAssessRst]');
        },
        //Check additional comments is empty
        AdditionalCommentsRequired: function (input) {
            return uim.EmptyTextValidation(input, '[name=txtSacraResultComments]');
        },
        //Prisoner B is always mandatory in New/Edit mode
        PrisonerBRequired: function (input) {
            if (input.is('[name = txtSacraRstPrisonerB]')) {
                return !uim.ValidateEmptyObject($.trim(sacraRst.sacraReportViewModel.get('SacraResultDetails.PrisonerSecondName')));
            }
            return true;
        },
        //Check same prisoner selected
        SamePrisonerSelected: function (input) {
            var isPrisonerInput = false;
            var index;
            for (index = 0; index < sacraRst.Prisoners.length; index++) {
                if (input.is('[name = ' + sacraRst.Prisoners[index].inputName + ']')) {
                    isPrisonerInput = true;
                    break;
                }
            }

            if (isPrisonerInput) {
                var selectedIds = {};
                var id;
                sacraRst.LastDuplicatePrisonerMeta = null;
                for (index = 0; index < sacraRst.Prisoners.length; index++) {
                    id = sacraRst.GetPrisonerId(index);
                    if (!uim.ValidateEmptyObject(id)) {
                        if (selectedIds[id]) {
                            sacraRst.LastDuplicatePrisonerMeta = selectedIds[id];
                            return false;
                        }
                        selectedIds[id] = sacraRst.Prisoners[index];
                    }
                }
            }
            return true;
        },
        //Filter Panel
        //Check prisoner is empty
        FilterPrisonerRequired: function (input) {
            return uim.EmptyTextValidation(input, '[name=txtSacraRstFltPrisoner]');
        },
        //Check date from is empty
        FilterDateFromRequired: function (input) {
            return uim.EmptyTextValidation(input, '[name=dpkSacraRstFltDateFrom]');
        },
        //Check date to is empty
        FilterDateToRequired: function (input) {
            return uim.EmptyTextValidation(input, '[name=dpkSacraRstFltDateTo]');
        },
        //Compare date to with date from
        FilterDateTo: function (input) {
            return uim.CompareDateValidation(input, '[name=dpkSacraRstFltDateTo]', '#dpkSacraRstFltDateFrom');
        },
        //Check date from is future
        FilterDateFrom: function (input) {
            return uim.FutureDateValidation(input, "[name=dpkSacraRstFltDateFrom]");
        },

        //Check difference b/w date from & to greater than 30
        FilterDate: function (input) {
            if (input.is('[name = dpkSacraRstFltDateTo]')) {
                if ($.trim(sacraRst.sacraReportViewModel.get('SACRAResultSearchParams.AssessmentDateFrom')) !== ''
                    && $.trim(sacraRst.sacraReportViewModel.get('SACRAResultSearchParams.AssessmentDateTo')) !== '') {
                    var dateFrom = kendo.parseDate(sacraRst.sacraReportViewModel.get('SACRAResultSearchParams.AssessmentDateFrom'));
                    var dateTo = kendo.parseDate(sacraRst.sacraReportViewModel.get('SACRAResultSearchParams.AssessmentDateTo'));

                    return uim.CompareDateByType(uim.ParseJsonDate(uim.AddDate(dateFrom, 1, MONTH)), uim.ParseJsonDate(dateTo), GREATER_THAN);
                }
            }
            return true;
        },
        //To hide the filter panel validation messages
        HideFilterMsg: function () {
            var validator = $("#dvSacraResultFlt").kendoValidator().data("kendoValidator");
            validator.hideMessages();
        },
        //To hide the result panel validation messages
        HideSearchMsg: function () {
            var validator = $("#dvSacraResult").kendoValidator().data("kendoValidator");
            validator.hideMessages();
        },
        //Assign validation rules & messages to container
        InitRules: function () {
            var container = $("#dvSacraResult");
            container.kendoValidator({
                rules: sacraRst.ResultRuleCollection.rules,
                messages: sacraRst.ResultMessageCollection.messages
            });
            var valid = false;
            var validator = container.data("kendoValidator");
            if (validator.validate()) {
                valid = true;
            }
            else {
                uim.SetFocusToErrorMsg("dvSacraResult", 350);
                valid = false;
            }
            return valid;
        },
        //Initialize validation rules & messages
        Init: function () {
            // Filter Panel
            var RuleName = sacraResultsResources.FilterPrisonerRule;
            sacraRst.FilterRuleCollection.rules[RuleName] = sacraRst.Validation.FilterPrisonerRequired;
            sacraRst.FilterMessageCollection.messages[RuleName] = sacraResultsResources.FilterPrisonerMsg;

            RuleName = sacraResultsResources.FilterDateToRule;
            sacraRst.FilterRuleCollection.rules[RuleName] = sacraRst.Validation.FilterDateTo;
            sacraRst.FilterMessageCollection.messages[RuleName] = sacraResultsResources.FilterDateToMsg;

            RuleName = sacraResultsResources.FilterDateFromRule;
            sacraRst.FilterRuleCollection.rules[RuleName] = sacraRst.Validation.FilterDateFrom;
            sacraRst.FilterMessageCollection.messages[RuleName] = sacraResultsResources.FilterDateFromMsg;

            RuleName = sacraResultsResources.FilterDateRule;
            sacraRst.FilterRuleCollection.rules[RuleName] = sacraRst.Validation.FilterDate;
            sacraRst.FilterMessageCollection.messages[RuleName] = sacraResultsResources.FilterDateMsg;
            // Result Panel
            RuleName = sacraResultsResources.ResultPrisonerARule;
            sacraRst.ResultRuleCollection.rules[RuleName] = sacraRst.Validation.PrisonerARequired;
            sacraRst.ResultMessageCollection.messages[RuleName] = sacraResultsResources.ResultPrisonerAMsg;

            RuleName = sacraResultsResources.ResultAssessmentResultRule;
            sacraRst.ResultRuleCollection.rules[RuleName] = sacraRst.Validation.AssessmentResultRequired;
            sacraRst.ResultMessageCollection.messages[RuleName] = sacraResultsResources.ResultAssessmentResultMsg;

            RuleName = sacraResultsResources.ResultAdditionalCommentsRule;
            sacraRst.ResultRuleCollection.rules[RuleName] = sacraRst.Validation.AdditionalCommentsRequired;
            sacraRst.ResultMessageCollection.messages[RuleName] = sacraResultsResources.ResultAdditionalCommentsMsg;

            RuleName = sacraResultsResources.ResultPrisonerBRule;
            sacraRst.ResultRuleCollection.rules[RuleName] = sacraRst.Validation.PrisonerBRequired;
            sacraRst.ResultMessageCollection.messages[RuleName] = sacraResultsResources.ResultPrisonerBMsg;

            RuleName = sacraResultsResources.ResultPrisonerRule;
            sacraRst.ResultRuleCollection.rules[RuleName] = sacraRst.Validation.SamePrisonerSelected;
            sacraRst.ResultMessageCollection.messages[RuleName] = function () { return sacraRst.GetDuplicatePrisonerMessage(sacraRst.LastDuplicatePrisonerMeta); };

            var container = $("#dvSacraResult");
            if (sacraRst.sacraReportViewModel.get('PanelCurrentMode') === sacraRst.PanelMode.Default) {
                container.kendoValidator({
                    rules: sacraRst.FilterRuleCollection.rules,
                    messages: sacraRst.FilterMessageCollection.messages
                });
            }
            else {
                container.kendoValidator({
                    rules: sacraRst.ResultRuleCollection.rules,
                    messages: sacraRst.ResultMessageCollection.messages
                });
            }
        }
    }
};
//On DOM Ready
$(document).ready(function () {
    sacraRst = new window.SacraResult();
    sacraRst.Init();
    sacraRst.InitSearchEnter();
    uim.GetCurrentInput = FILTER_PRISONER_FIELD;
    uim.GetCurrentGrid = SACRA_RESULT_GRID;
});

