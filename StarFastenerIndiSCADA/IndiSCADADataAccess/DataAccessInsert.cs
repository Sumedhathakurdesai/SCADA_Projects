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
    public static class DataAccessInsert
    {
        #region Public/Private Property
        #endregion
        #region Public Private  method

        #region Shiftwise Downtime settings
        public static ServiceResponse<int> InsertShitwiseDowntimeData(DateTime ShiftDate, int ShiftNo, string DownTime, string SemiDownTime, string ManualDownTime, string MaintainanceDT, string CompleteDT)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                string sqlQry = "INSERT INTO ShiftWiseDownTimeSummary(ShiftDateTime,ShiftNo,DownTime,SemiDownTime,ManualDownTime,MaintenanceDownTime,CompleteDownTime) VALUES('" + ShiftDate + "','" + ShiftNo.ToString() + "','" + DownTime + "','" + SemiDownTime + "','" + ManualDownTime + "','" + MaintainanceDT + "','" + CompleteDT + "')";
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ContinuousTagRead() insert Query  = " + sqlQry, DateTime.Now.ToString(), " ", null, true);
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

        #region chemical name editable
        public static ServiceResponse<int> InsertChemicalNameMasterData(ChemicalNameMasterEntity chemicalEntity)
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
                    new SqlParameter("@Selection","INSERT"),
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


        public static ServiceResponse<int> InsertShiftMasterData(ShiftMasterEntity InsertShiftMasterData)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@ShiftNumber",InsertShiftMasterData.ShiftNumber.Trim()),
                    new SqlParameter("@ShiftEndTime",InsertShiftMasterData.ShiftEndTime),
                    new SqlParameter("@ShiftStartTime",InsertShiftMasterData.ShiftStartTime),
                    new SqlParameter("@modifieddate",DateTime.Now),
                    new SqlParameter("@Selection","INSERT"),
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
        public static ServiceResponse<int> insertLNInToLoadPartData(string ln, DataTable dtPartColumns, DataTable dtPartData)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                foreach (DataRow drpart in dtPartData.Rows)
                {
                    string lstColumns = "", lstValues = "";

                    foreach (DataRow dr in dtPartColumns.Rows)
                    {
                        string colname = dr["ColumnName"].ToString();
                        lstColumns += "[" + colname + "],";
                        lstValues += "'" + drpart[colname].ToString() + "',";

                    }
                    string query = "INSERT INTO LoadPartDetails(LoadNumber," + lstColumns.Substring(0, lstColumns.Length - 1) + " ) VALUES('" + ln + "'," + lstValues.Substring(0, lstValues.Length - 1) + ")";

                    DataTable _DTRecords = new DataTable();
                    List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",query),
                        };
                    int result = SQLHelper.ExecuteNonQuery("sp_TableCreateUsingQuery", sqlParameters.ToArray());

                    _FinalAccessResponse.Response = result;
                    _FinalAccessResponse.Message = "";
                    _FinalAccessResponse.Status = ResponseType.S;
                }
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

        public static ServiceResponse<int> DeleteFromLoadPartDetails(string query)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                DataTable _DTRecords = new DataTable();
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",query),
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


        public static ServiceResponse<int> InsertORDipTimeData(OutOfRangeSettingsDipTime _DataORDipTime)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@ProgramNo",_DataORDipTime.ProgramNo),
                     new SqlParameter("@StationNO",_DataORDipTime.StationNO),
                    new SqlParameter("@StationName",_DataORDipTime.ParameterName ),
                    new SqlParameter("@DipTimeToleranceHigh",_DataORDipTime.HighSPDipTime ),
                    new SqlParameter("@DipTimeToleranceLow",_DataORDipTime.LowSPDipTime),
                    new SqlParameter("@DipLowBypass",_DataORDipTime.LowBypass),
                    new SqlParameter("@DipHighBypass",_DataORDipTime.HighBypass),

                    new SqlParameter("@Selection","Insert"),
                };
               // DataTable _DTRecords = new DataTable();
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

        public static ServiceResponse<int> InsertChemicalConsumptionData(DosingOperationDataEntity _DataChemicalConsumption)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@ChemicalName",_DataChemicalConsumption.ChemicalName),
                    new SqlParameter("@DateTimeCol",DateTime.Now ),
                    new SqlParameter("@ModeOfOperation",_DataChemicalConsumption.ModeOfOperation ),
                    new SqlParameter("@SetQuantity",_DataChemicalConsumption.SetQuantity ),
                    new SqlParameter("@SetFlowRate",_DataChemicalConsumption.SetFlowRate),
                    new SqlParameter("@SetTime",_DataChemicalConsumption.SetTime),
                    new SqlParameter("@PumpNo",_DataChemicalConsumption.PumpNo),
                    new SqlParameter("@Selection","InsertData"),
                };
                DataTable _DTRecords = new DataTable();
                int result = SQLHelper.ExecuteNonQuery("SPChemicalConsumption", sqlParameters.ToArray());
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
        public static ServiceResponse<int> InsertLoadData(LoadDataEntity InsertLoadData)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@LoadNumber",InsertLoadData.LoadNumber),
                    new SqlParameter("@LoadNumberWithTime",InsertLoadData.LoadNumberWithTime),
                    new SqlParameter("@LoadInTime",InsertLoadData.LoadInTime),
                    new SqlParameter("@LoadOutTime",InsertLoadData.LoadOutTime),
                    new SqlParameter("@Operator",InsertLoadData.Operator),
                    new SqlParameter("@LoadType",InsertLoadData.LoadType),
                    new SqlParameter("@isStart",InsertLoadData.isStart),
                    new SqlParameter("@isEnd",InsertLoadData.isEnd),
                    new SqlParameter("@isStationExceedTime",InsertLoadData.isStationExceedTime),
                    new SqlParameter("@LoadInShift",InsertLoadData.LoadInShift),
                    new SqlParameter("@LoadOutShift",InsertLoadData.LoadOutShift),
                    new SqlParameter("@LastCycleTime",InsertLoadData.LastCycleTime),
                    new SqlParameter("@Selection","Insert"),
                };
                DataTable _DTRecords = new DataTable();
                int result =SQLHelper.ExecuteNonQuery("SP_LoadData", sqlParameters.ToArray());
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
        public static ServiceResponse<int> InsertLoadDipTime(LoadDipTimeEntity InsertLoadDipTime)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@LoadNumber",InsertLoadDipTime.LoadNumber),
                    new SqlParameter("@DipInTime",InsertLoadDipTime.DipInTime),
                    new SqlParameter("@DipOutTime",InsertLoadDipTime.DipOutTime),
                    new SqlParameter("@DipOutShift",InsertLoadDipTime.DipOutShift),
                    new SqlParameter("@DipInShift",InsertLoadDipTime.DipInShift),
                    new SqlParameter("@StationNo",InsertLoadDipTime.StationNo),
                    new SqlParameter("@Selection","Insert"),
                };
                DataTable _DTRecords = new DataTable();
                int result = SQLHelper.ExecuteNonQuery("SP_LoadDipTime", sqlParameters.ToArray());
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
        public static ServiceResponse<int> InsertTrendTemperatureData(TemperatureTrendentity InsertTrendTemperature,string [] values)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@InTime",InsertTrendTemperature.DateTimeCol),
                    new SqlParameter("@ValueList",string.Join(",",values)),
                    new SqlParameter("@Selection","InsertData"),
                };
                DataTable _DTRecords = new DataTable();
                int result = SQLHelper.ExecuteNonQuery("TempTrendDataLog", sqlParameters.ToArray());
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
        public static ServiceResponse<int> InsertTrendRectifier(RectifierTrendEntity InsertTrendRectifier, string[] Actualvalues, string[] SP)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@InTime",InsertTrendRectifier.DateTimeCol),
                    new SqlParameter("@ValueList",string.Join(",",Actualvalues)),
                     new SqlParameter("@SPValueList",string.Join(",",SP)),
                    new SqlParameter("@Selection","InsertData"),
                };
                DataTable _DTRecords = new DataTable();
                int result = SQLHelper.ExecuteNonQuery("SPCurrentTrendDataLog", sqlParameters.ToArray());
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
        public static ServiceResponse<int> InsertEventsData(EventLogEntity InsertEventData)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Description",InsertEventData.Description),
                    new SqlParameter("@EndDate",InsertEventData.EndDateTime),
                    new SqlParameter("@StartDate",InsertEventData.StartDateTime),
                    new SqlParameter("@isComplete",InsertEventData.isComplete),
                    new SqlParameter("@GroupName",InsertEventData.GroupName),
                    new SqlParameter("@EventText",InsertEventData.EventText),
                    new SqlParameter("@Selection","InsertData"),
                };
                DataTable _DTRecords = new DataTable();
                int result = SQLHelper.ExecuteNonQuery("EventsDataLog", sqlParameters.ToArray());
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
        public static ServiceResponse<int> InsertAlarmData(AlarmDataEntity InsertAlarmsData)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@AlarmName",InsertAlarmsData.AlarmName),
                    new SqlParameter("@AlarmText",InsertAlarmsData.AlarmText),
                    new SqlParameter("@AlarmDateTime",InsertAlarmsData.AlarmDateTime),
                    new SqlParameter("@AlarmCondition",InsertAlarmsData.AlarmCondition),
                    new SqlParameter("@CausesDownTime",InsertAlarmsData.CausesDownTime),
                    new SqlParameter("@AlarmDuration",InsertAlarmsData.AlarmDuration),
                    new SqlParameter("@AlarmGroup",InsertAlarmsData.AlarmType),
                    new SqlParameter("@Selection","InsertData"),
                };
                DataTable _DTRecords = new DataTable();
                int result = SQLHelper.ExecuteNonQuery("SPAlarmData", sqlParameters.ToArray());
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
        public static ServiceResponse<int> InsertNewUserData(UserMasterEntity InsertUserMaster)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@UserName",InsertUserMaster.UserName),
                    new SqlParameter("@UserPassword",InsertUserMaster.UserPassword),
                    new SqlParameter("@UserRole",InsertUserMaster.UserRole),
                    new SqlParameter("@EmailID",InsertUserMaster.EmailID),
                    new SqlParameter("@MobileNo",InsertUserMaster.MobileNo),
                    new SqlParameter("@ReturnValue",ParameterDirection.Output),
                    new SqlParameter("@Selection","InsertData"),
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
        public static ServiceResponse<int> InsertUserActivity(UserActivityEntity InsertUserActivity)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Activity",InsertUserActivity.Activity),
                    new SqlParameter("@DateTimeCol",InsertUserActivity.DateTimeCol),
                    new SqlParameter("@UserName",InsertUserActivity.UserName),
                    new SqlParameter("@Selection","InsertData"),
                };
                DataTable _DTRecords = new DataTable();
                int result = SQLHelper.ExecuteNonQuery("SPUserActivity", sqlParameters.ToArray());
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

        public static ServiceResponse<int> InsertNextLoadSettingData(NextLoadSettingsEntity InsertNextloadMasterSetting)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@ParameterName",InsertNextloadMasterSetting.ParameterName),
                    new SqlParameter("@DataType",InsertNextloadMasterSetting.DataType),
                    new SqlParameter("@Unit",InsertNextloadMasterSetting.Unit),
                    new SqlParameter("@isPrimaryKey",InsertNextloadMasterSetting.isPrimaryKey),
                    new SqlParameter("@TaskName",InsertNextloadMasterSetting.TaskName),
                    new SqlParameter("@isDownloadToPLC",InsertNextloadMasterSetting.isDownloadToPlc),
                    new SqlParameter("@isInReport",InsertNextloadMasterSetting.isInReport),
                    new SqlParameter("@status","INSERT"),
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

        public static ServiceResponse<int> InsertPartMasterData(string query)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                DataTable _DTRecords = new DataTable();
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",query),
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

        public static ServiceResponse<int> insertLoadNoToLoadPartDetails(string loadnumber, DataTable dtSetting, DataTable dtPartData)
        {
            string cmdInsertQuery = "";
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                foreach (DataRow drpart in dtPartData.Rows)
                {
                    string lstColumns = "", lstValues = "";
                    //added by SBS parameter name and column name seperate logic
                    foreach (DataRow dr in dtSetting.Rows)
                    {
                        string colname = dr["ParameterName"].ToString();


                        string partName = "";
                        try
                        {
                            DataRow[] results = dtSetting.Select(" ParameterName='" + colname + "'");
                            partName = results[0]["ColumnName"].ToString();
                        }
                        catch (Exception ex)
                        {

                        }
                        lstColumns += "[" + partName + "],";
                        lstValues += "'" + drpart[partName].ToString() + "',";

                    }
                    cmdInsertQuery = "INSERT INTO LoadPartDetails(LoadNumber,DateTimeCol," + lstColumns.Substring(0, lstColumns.Length - 1) + " ) VALUES('" + loadnumber + "','" + DateTime.Now + "'," + lstValues.Substring(0, lstValues.Length - 1) + ")";
                    //-----------------------------------------------

                    //string lstColumns = "", lstValues = "";

                    //foreach (DataRow dr in dtSetting.Rows)
                    //{
                    //    string colname = dr["ParameterName"].ToString();
                    //    //lstColumns += "[" + colname + "],";
                    //    lstColumns += "[" +colname.Replace(" ", string.Empty).Trim().ToString()+ "],";
                    //    lstValues += "'"+drpart[colname.Replace(" ", string.Empty).Trim().ToString()].ToString()+"'"+",";


                    //}
                    //cmdInsertQuery = "INSERT INTO LoadPartDetails(LoadNumber,DateTimeCol," + lstColumns.Substring(0, lstColumns.Length - 1) + " ) VALUES('" + loadnumber + "','"+ DateTime.Now +"'," + lstValues.Substring(0, lstValues.Length - 1) + ")";
                    ErrorLogger.LogError.ErrorLog("Data access layer query insertLoadNoToLoadPartDetails : " + cmdInsertQuery, DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                    try
                    {
                        List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",cmdInsertQuery),
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
                }
            }
            catch (Exception ex)
            { }

            return _FinalAccessResponse;
        }
        #endregion
    }
}
