

using System;
using Corrections.Ioms.Bpl.Movement.SharedAccomAssmnt.Entities;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Corrections.IOMS.Common.Core.DataAccessHelper;

namespace Corrections.Ioms.Bpl.Movement.SharedAccomAssmnt.DataAbstration
{
    /// <summary>
    /// Wraps the OM_CELL_SHARING_PRISONER table in the IOMS schema in Oracle.
    /// </summary>
    /// <remarks>
    /// This table contains the following columns:
    /// CELL_SHARING_PRISONER_ID (Oracle DB Type = Char)
    /// CELL_SHARING_RISK_ASSMNT_ID (Oracle DB Type = Char)
    /// PRISONER_ID (Oracle DB Type = Char)
    /// PRISONER_ORDER_NUM (Oracle DB Type = Numeric)
    /// ACTION_BY (Oracle DB Type = Numeric)
    /// ACTION_TIME (Oracle DB Type = IUnknown)
    /// ACTION_TYPE (Oracle DB Type = Char)
    /// </remarks>
    public sealed class CellSharingPrisonerCUD : IDisposable
    {
        private const string IOMS_DB = "IomsDB";
        // Package
        private const string CREATE_UPDATE_PROCEDURE_OM_CELL_SHARING_PRISONER = "ioms.pkg_sacra_result.rw_om_cell_sharing_prisoner";
        private const string DELETE_PROCEDURE_OM_CELL_SHARING_PRISONER = "ioms.pkg_sacra_result.rw_delete_om_cell_sharing_prisoner";
        private const string DELETE_FROM_ORDER_PROCEDURE_OM_CELL_SHARING_PRISONER = "ioms.pkg_sacra_result.rw_delete_cell_sharing_prisoners_from_order";
        private const string OM_CELL_SHARING_PRISONER_TABLE = "IOMS.OM_CELL_SHARING_PRISONER";
        // Input param
        private const string PI_CELL_SHARING_PRISONER_ID = "PI_CELL_SHARING_PRISONER_ID";
        private const string PI_CELL_SHARING_RISK_ASSMNT_ID = "PI_CELL_SHARING_RISK_ASSMNT_ID";
        private const string PI_PRISONER_ID = "PI_PRISONER_ID";
        private const string PI_PRISONER_ORDER_NUM = "PI_PRISONER_ORDER_NUM";
        private const string PI_ACTION_BY = "PI_ACTION_BY";
        private const string PI_FROM_PRISONER_ORDER_NUM = "PI_FROM_PRISONER_ORDER_NUM";
        private const string PO_CELL_SHARING_PRISONER_ID = "PO_CELL_SHARING_PRISONER_ID";
        private const string PO_RESULT = "PO_RESULT";

        /// <inheritdoc />
        /// <summary>
        /// Represents method to release resources
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Creates a new record in the IOMS.OM_CELL_SHARING_PRISONER table using the details from the 
        /// CellSharingPrisoner entity.
        /// </summary>
        /// <param name="cellSharingPrisoner">CellSharingPrisoner entity</param>
        /// <returns>The new entity for a successful create, otherwise null.</returns>
        public static SACRAResultDetails AddPriosner(SACRAResultDetails resultDetails)
        {
            try
            {
                OracleParameter[] oraParams = new OracleParameter[]
                {
                    new OracleParameter(PI_CELL_SHARING_PRISONER_ID, OracleDbType.Char, DBNull.Value, ParameterDirection.Input),
                    new OracleParameter(PI_CELL_SHARING_RISK_ASSMNT_ID, OracleDbType.Char, resultDetails.ApplicationId, ParameterDirection.Input),
                    new OracleParameter(PI_PRISONER_ID, OracleDbType.Char, resultDetails.PrisonerFirstId, ParameterDirection.Input),
                    new OracleParameter(PI_PRISONER_ORDER_NUM, OracleDbType.Int32, resultDetails.PrisonerOrderNum, ParameterDirection.Input),
                    new OracleParameter(PI_ACTION_BY, OracleDbType.Int32, int.Parse(resultDetails.GetSysData(SysDataProps.UserIntId).ToString()), ParameterDirection.Input),
                    new OracleParameter(PO_CELL_SHARING_PRISONER_ID, OracleDbType.Char, 16, null, ParameterDirection.Output),
                    new OracleParameter(PO_RESULT, OracleDbType.Int32, 4, null, ParameterDirection.Output)
                };

                using (OracleHelper oracleHelper = new OracleHelper())
                {
                    oracleHelper.ExecuteNonQuery(IOMS_DB, CommandType.StoredProcedure, CREATE_UPDATE_PROCEDURE_OM_CELL_SHARING_PRISONER, oraParams);
                    var result = oraParams[oraParams.Length - 1].Value;

                    if (result != null)
                    {
                        int count = Convert.ToInt32(result.ToString());

                        if (count > 0)
                        {
                            resultDetails.CellSharingFirstPrisonerId = Convert.ToString(oraParams[oraParams.Length - 2].Value);
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(resultDetails.CellSharingFirstPrisonerId))
                {
                    return resultDetails;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while attempting to add a " +
                    "new record to the " + OM_CELL_SHARING_PRISONER_TABLE + " table using procedure " + CREATE_UPDATE_PROCEDURE_OM_CELL_SHARING_PRISONER, ex);
            }
        }

        /// <summary>
        /// Updates an existing record in the IOMS.OM_CELL_SHARING_PRISONER table using the details from the 
        /// CellSharingPrisoner entity.
        /// </summary>
        /// <param name="cellSharingPrisoner">CellSharingPrisoner entity</param>
        /// <returns>The updated entity for a successful update, otherwise null.</returns>
        public static SACRAResultDetails UpdatePriosner(SACRAResultDetails resultDetails)
        {
            bool isSuccess = false;
            try
            {
                OracleParameter[] oraParams = new OracleParameter[]
                {
                    new OracleParameter(PI_CELL_SHARING_PRISONER_ID, OracleDbType.Char, resultDetails.CellSharingFirstPrisonerId, ParameterDirection.Input),
                    new OracleParameter(PI_CELL_SHARING_RISK_ASSMNT_ID, OracleDbType.Char, resultDetails.ApplicationId, ParameterDirection.Input),
                    new OracleParameter(PI_PRISONER_ID, OracleDbType.Char, resultDetails.PrisonerFirstId, ParameterDirection.Input),
                    new OracleParameter(PI_PRISONER_ORDER_NUM, OracleDbType.Int32, resultDetails.PrisonerOrderNum, ParameterDirection.Input),
                    new OracleParameter(PI_ACTION_BY, OracleDbType.Int32, int.Parse(resultDetails.GetSysData(SysDataProps.UserIntId).ToString()), ParameterDirection.Input),
                    new OracleParameter(PO_CELL_SHARING_PRISONER_ID, OracleDbType.Char, 16, null, ParameterDirection.Output),
                    new OracleParameter(PO_RESULT, OracleDbType.Int32, 4, null, ParameterDirection.Output)
                };

                using (OracleHelper oracleHelper = new OracleHelper())
                {
                    oracleHelper.ExecuteNonQuery(IOMS_DB, CommandType.StoredProcedure, CREATE_UPDATE_PROCEDURE_OM_CELL_SHARING_PRISONER, oraParams);
                    var result = oraParams[oraParams.Length - 1].Value;

                    if (result != null)
                    {
                        int count = Convert.ToInt32(result.ToString());

                        if (count > 0)
                        {
                            isSuccess = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while attempting to update a " +
                    "record in the " + OM_CELL_SHARING_PRISONER_TABLE + " table using procedure " + CREATE_UPDATE_PROCEDURE_OM_CELL_SHARING_PRISONER, ex);
            }
            return isSuccess ? resultDetails : null;
        }

        /// <summary>
        /// Deletes an existing record in the IOMS.OM_CELL_SHARING_PRISONER table using the details from the 
        /// CellSharingPrisoner entity.
        /// </summary>
        /// <param name="cellSharingPrisoner">CellSharingPrisoner entity</param>
        /// <returns>True for for a successful delete, otherwise false.</returns>
        public static bool DeletePrisoner(SACRAResultDetails resultDetails)
        {
            bool isSuccess = false;
            try
            {
                OracleParameter[] oraParams = new OracleParameter[]
                {
                    new OracleParameter(PI_CELL_SHARING_PRISONER_ID, OracleDbType.Char, resultDetails.CellSharingFirstPrisonerId, ParameterDirection.Input),
                    new OracleParameter(PI_ACTION_BY, OracleDbType.Int32, int.Parse(resultDetails.GetSysData(SysDataProps.UserIntId).ToString()), ParameterDirection.Input),
                    new OracleParameter(PO_RESULT,  OracleDbType.Int32, 4, null, ParameterDirection.Output)
                };
                using (OracleHelper oracleHelper = new OracleHelper())
                {
                    oracleHelper.ExecuteNonQuery(IOMS_DB, CommandType.StoredProcedure, DELETE_PROCEDURE_OM_CELL_SHARING_PRISONER, oraParams);
                    var result = oraParams[oraParams.Length - 1].Value;

                    if (result != null)
                    {
                        int count = Convert.ToInt32(result.ToString());

                        if (count > 0)
                        {
                            isSuccess = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while attempting to delete a " +
                    "record from the " + OM_CELL_SHARING_PRISONER_TABLE + " table using procedure " + DELETE_PROCEDURE_OM_CELL_SHARING_PRISONER, ex);
            }
            return isSuccess;
        }


        /// <summary>
        /// Saves one OM_CELL_SHARING_PRISONER row for a compact sequential SACRA prisoner slot.
        /// </summary>
        public static string SavePrisoner(SACRAResultDetails resultDetails, string cellSharingPrisonerId,string prisonerId,short prisonerOrderNum)
        {
            try
            {
                object childIdParameterValue = string.IsNullOrWhiteSpace(cellSharingPrisonerId) ? (object)DBNull.Value : cellSharingPrisonerId;

                OracleParameter[] oraParams = new OracleParameter[]
                {
                    new OracleParameter(PI_CELL_SHARING_PRISONER_ID, OracleDbType.Char, childIdParameterValue, ParameterDirection.Input),
                    new OracleParameter(PI_CELL_SHARING_RISK_ASSMNT_ID, OracleDbType.Char, resultDetails.ApplicationId, ParameterDirection.Input),
                    new OracleParameter(PI_PRISONER_ID, OracleDbType.Char, prisonerId, ParameterDirection.Input),
                    new OracleParameter(PI_PRISONER_ORDER_NUM, OracleDbType.Int32, prisonerOrderNum, ParameterDirection.Input),
                    new OracleParameter(PI_ACTION_BY, OracleDbType.Int32, int.Parse(resultDetails.GetSysData(SysDataProps.UserIntId).ToString()), ParameterDirection.Input),
                    new OracleParameter(PO_CELL_SHARING_PRISONER_ID, OracleDbType.Char, 16, null, ParameterDirection.Output),
                    new OracleParameter(PO_RESULT, OracleDbType.Int32, 4, null, ParameterDirection.Output)
                };

                using (OracleHelper oracleHelper = new OracleHelper())
                {
                    oracleHelper.ExecuteNonQuery(IOMS_DB, CommandType.StoredProcedure, CREATE_UPDATE_PROCEDURE_OM_CELL_SHARING_PRISONER, oraParams);

                    var result = oraParams[oraParams.Length - 1].Value;
                    if (result != null && Convert.ToInt32(result.ToString()) > 0)
                    {
                        return Convert.ToString(oraParams[oraParams.Length - 2].Value);
                    }
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while attempting to save a " +
                    "record in the " + OM_CELL_SHARING_PRISONER_TABLE + " table using procedure " + CREATE_UPDATE_PROCEDURE_OM_CELL_SHARING_PRISONER, ex);
            }
        }

        /// <summary>
        /// Backward-compatible wrapper for code paths that still explicitly add one prisoner.
        /// </summary>
        public static string AddPrisoner(SACRAResultDetails resultDetails, string prisonerId, short prisonerOrderNum)
        {
            return SavePrisoner(resultDetails, string.Empty, prisonerId, prisonerOrderNum);
        }

        /// <summary>
        /// Backward-compatible wrapper for code paths that still explicitly update one prisoner.
        /// </summary>
        public static bool UpdatePrisoner(SACRAResultDetails resultDetails, string cellSharingPrisonerId, string prisonerId, short prisonerOrderNum)
        {
            string savedCellSharingPrisonerId = SavePrisoner(resultDetails, cellSharingPrisonerId, prisonerId, prisonerOrderNum);
            return !string.IsNullOrWhiteSpace(savedCellSharingPrisonerId);
        }

        /// <summary>
        /// Deletes all prisoner rows from the supplied order number onwards for the same SACRA assessment.
        /// This is used only when an existing assessment is saved with fewer selected prisoners.
        /// </summary>
        public static bool DeletePrisonersFromOrder(SACRAResultDetails resultDetails, short fromPrisonerOrderNum)
        {
            try
            {
                OracleParameter[] oraParams = new OracleParameter[]
                {
                    new OracleParameter(PI_CELL_SHARING_RISK_ASSMNT_ID, OracleDbType.Char, resultDetails.ApplicationId, ParameterDirection.Input),
                    new OracleParameter(PI_FROM_PRISONER_ORDER_NUM, OracleDbType.Int32, fromPrisonerOrderNum, ParameterDirection.Input),
                    new OracleParameter(PI_ACTION_BY, OracleDbType.Int32, int.Parse(resultDetails.GetSysData(SysDataProps.UserIntId).ToString()), ParameterDirection.Input),
                    new OracleParameter(PO_RESULT, OracleDbType.Int32, 4, null, ParameterDirection.Output)
                };

                using (OracleHelper oracleHelper = new OracleHelper())
                {
                    oracleHelper.ExecuteNonQuery(IOMS_DB, CommandType.StoredProcedure, DELETE_FROM_ORDER_PROCEDURE_OM_CELL_SHARING_PRISONER, oraParams);
                    return oraParams[oraParams.Length - 1].Value != null;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while attempting to delete extra " +
                    "records from the " + OM_CELL_SHARING_PRISONER_TABLE + " table using procedure " + DELETE_FROM_ORDER_PROCEDURE_OM_CELL_SHARING_PRISONER, ex);
            }
        }

        /// <summary>
        /// Deletes one OM_CELL_SHARING_PRISONER row by primary key.
        /// </summary>
        public static bool DeletePrisoner(SACRAResultDetails resultDetails, string cellSharingPrisonerId)
        {
            bool isSuccess = false;
            try
            {
                OracleParameter[] oraParams = new OracleParameter[]
                {
                    new OracleParameter(PI_CELL_SHARING_PRISONER_ID, OracleDbType.Char, cellSharingPrisonerId, ParameterDirection.Input),
                    new OracleParameter(PI_ACTION_BY, OracleDbType.Int32, int.Parse(resultDetails.GetSysData(SysDataProps.UserIntId).ToString()), ParameterDirection.Input),
                    new OracleParameter(PO_RESULT, OracleDbType.Int32, 4, null, ParameterDirection.Output)
                };
                using (OracleHelper oracleHelper = new OracleHelper())
                {
                    oracleHelper.ExecuteNonQuery(IOMS_DB, CommandType.StoredProcedure, DELETE_PROCEDURE_OM_CELL_SHARING_PRISONER, oraParams);
                    var result = oraParams[oraParams.Length - 1].Value;

                    if (result != null)
                    {
                        int count = Convert.ToInt32(result.ToString());

                        if (count > 0)
                        {
                            isSuccess = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while attempting to delete a " +
                    "record from the " + OM_CELL_SHARING_PRISONER_TABLE + " table using procedure " + DELETE_PROCEDURE_OM_CELL_SHARING_PRISONER, ex);
            }
            return isSuccess;
        }


    }
}