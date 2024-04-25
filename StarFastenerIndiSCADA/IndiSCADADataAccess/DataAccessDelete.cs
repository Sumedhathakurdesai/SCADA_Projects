using IndiSCADAEntity.Entity;
using IndiSCADAGlobalLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADADataAccess
{
    public static class DataAccessDelete
    {
        #region Public/Private Property
        #endregion
        #region Public Private  method
        #region chemical name editable
        public static ServiceResponse<int> DeleteChemicalNameMasterData(ChemicalNameMasterEntity chemicalEntity)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@PumpNumber",chemicalEntity.PumpNumber),
                    new SqlParameter("@PumpName",chemicalEntity.PumpName.Trim()),
                    new SqlParameter("@ChemicalName",chemicalEntity.ChemicalName),
                    new SqlParameter("@ChemicalPercentage",Convert.ToSingle(chemicalEntity.ChemicalPercentage)),
                    new SqlParameter("@modifieddate",DateTime.Now),
                    new SqlParameter("@Selection","Delete"),
                };
                DataTable _DTRecords = new DataTable();
                int result = SQLHelper.ExecuteNonQuery("SP_ChemicalNameMaster", sqlParameters.ToArray());
                _FinalAccessResponse.Response = result;
                _FinalAccessResponse.Message = "";
                _FinalAccessResponse.Status = ResponseType.S;
            }
            catch (Exception Ex)
            {
                _FinalAccessResponse.Response = 0;
                _FinalAccessResponse.Message = Ex.Message;
                _FinalAccessResponse.Status = ResponseType.E;
                _FinalAccessResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalAccessResponse;
        }
        #endregion

        public static ServiceResponse<int> DeleteOldShiftMasterData()
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Selection","DeleteOldShift"),
                };
                DataTable _DTRecords = new DataTable();
                int result = SQLHelper.ExecuteNonQuery("SP_ShiftMaster", sqlParameters.ToArray());
                _FinalAccessResponse.Response = result;
                _FinalAccessResponse.Message = "";
                _FinalAccessResponse.Status = ResponseType.S;
            }
            catch (Exception Ex)
            {
                _FinalAccessResponse.Response = 0;
                _FinalAccessResponse.Message = Ex.Message;
                _FinalAccessResponse.Status = ResponseType.E;
                _FinalAccessResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalAccessResponse;
        }
        public static ServiceResponse<int> DeleteORDipTimeData(OutOfRangeSettingsDipTime programNO)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@ProgramNo",programNO.ProgramNo),

                    new SqlParameter("@Selection","delete"),
                };
                int result = SQLHelper.ExecuteNonQuery("SP_ORdipTime", sqlParameters.ToArray());
                _FinalAccessResponse.Response = result;
                _FinalAccessResponse.Message = "";
                _FinalAccessResponse.Status = ResponseType.S;
            }
            catch (Exception Ex)
            {
                _FinalAccessResponse.Response = 0;
                _FinalAccessResponse.Message = Ex.Message;
                _FinalAccessResponse.Status = ResponseType.E;
                _FinalAccessResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalAccessResponse;
        }
        public static ServiceResponse<int> DeleteAllShiftMasterData()
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Selection","Delete"),
                };
                DataTable _DTRecords = new DataTable();
                int result = SQLHelper.ExecuteNonQuery("SP_ShiftMaster", sqlParameters.ToArray());
                _FinalAccessResponse.Response = result;
                _FinalAccessResponse.Message = "";
                _FinalAccessResponse.Status = ResponseType.S;
            }
            catch (Exception Ex)
            {
                _FinalAccessResponse.Response = 0;
                _FinalAccessResponse.Message = Ex.Message;
                _FinalAccessResponse.Status = ResponseType.E;
                _FinalAccessResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalAccessResponse;
        }
        public static ServiceResponse<DataTable> GetAlarmDetails()
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@AlarmId",null),
                    new SqlParameter("@AlarmDateTime",null),
                    new SqlParameter("@AlarmText",null),
                    new SqlParameter("@AlarmCondition",null),
                    new SqlParameter("@LineID",null),
                    new SqlParameter("@BatchID",null),
                    new SqlParameter("@UserName",null),
                    new SqlParameter("@status","SELECT"),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "sp_alarmdata_opertaion", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "Data Saved Successfuly";
                _FinalAccessResponse.Status = ResponseType.S;
            }
            catch (Exception Ex)
            {
                _FinalAccessResponse.Response = null;
                _FinalAccessResponse.Message = Ex.Message;
                _FinalAccessResponse.Status = ResponseType.E;
                _FinalAccessResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalAccessResponse;
        }

        public static ServiceResponse<int> DeleteNewUserData(UserMasterEntity DeleteUserMasterData)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@UserName",DeleteUserMasterData.UserName),
                    new SqlParameter("@UserPassword",DeleteUserMasterData.UserPassword),
                    new SqlParameter("@UserRole",DeleteUserMasterData.UserRole),
                    new SqlParameter("@EmailID",DeleteUserMasterData.EmailID),
                    new SqlParameter("@MobileNo",DeleteUserMasterData.MobileNo),
                    new SqlParameter("@ReturnValue",ParameterDirection.Output),
                    new SqlParameter("@Selection","DeleteUserData"),
                };
                DataTable _DTRecords = new DataTable();
                int result = SQLHelper.ExecuteNonQuery("UserMasterSP", sqlParameters.ToArray());
                _FinalAccessResponse.Response = result;
                _FinalAccessResponse.Message = "";
                _FinalAccessResponse.Status = ResponseType.S;
            }
            catch (Exception Ex)
            {
                _FinalAccessResponse.Response = 0;
                _FinalAccessResponse.Message = Ex.Message;
                _FinalAccessResponse.Status = ResponseType.E;
                _FinalAccessResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalAccessResponse;
        }

        public static ServiceResponse<int> DeleteNextLoadSettingData(NextLoadSettingsEntity DeleteNextloadData)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@ParameterName",DeleteNextloadData.ParameterName),
                    new SqlParameter("@status","DELETE"),
                };
                DataTable _DTRecords = new DataTable();
                int result = SQLHelper.ExecuteNonQuery("sp_NextLoadMasterSettingData", sqlParameters.ToArray());
                _FinalAccessResponse.Response = result;
                _FinalAccessResponse.Message = "";
                _FinalAccessResponse.Status = ResponseType.S;
            }
            catch (Exception Ex)
            {
                _FinalAccessResponse.Response = 0;
                _FinalAccessResponse.Message = Ex.Message;
                _FinalAccessResponse.Status = ResponseType.E;
                _FinalAccessResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalAccessResponse;
        }

        public static ServiceResponse<int> DeletePartMasterData(string partnumber)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                string sqlQry = "Delete from PartMaster where PartNumber='" + partnumber + "'";
                DataTable _DTRecords = new DataTable();
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",sqlQry),
                        };
                int result = SQLHelper.ExecuteNonQuery("sp_TableCreateUsingQuery", sqlParameters.ToArray());

                _FinalAccessResponse.Response = result;
                _FinalAccessResponse.Message = "";
                _FinalAccessResponse.Status = ResponseType.S;
            }
            catch (Exception Ex)
            {
                _FinalAccessResponse.Response = 0;
                _FinalAccessResponse.Message = Ex.Message;
                _FinalAccessResponse.Status = ResponseType.E;
                _FinalAccessResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalAccessResponse;
        }

        public static ServiceResponse<int> DeleteHangerMasterData(string htype)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                string sqlQry = "Delete from HangerTypeMaster where HangerType='" + htype + "'";
                DataTable _DTRecords = new DataTable();
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",sqlQry),
                        };
                int result = SQLHelper.ExecuteNonQuery("sp_TableCreateUsingQuery", sqlParameters.ToArray());

                _FinalAccessResponse.Response = result;
                _FinalAccessResponse.Message = "";
                _FinalAccessResponse.Status = ResponseType.S;
            }
            catch (Exception Ex)
            {
                _FinalAccessResponse.Response = 0;
                _FinalAccessResponse.Message = Ex.Message;
                _FinalAccessResponse.Status = ResponseType.E;
                _FinalAccessResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalAccessResponse;
        }
        #endregion
    }
}
