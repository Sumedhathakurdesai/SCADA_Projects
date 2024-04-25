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
    public static class DataAccessUpdate
    {
        #region Public/Private Property
        #endregion

        #region Public Private  method

        #region chemical name editable
        public static ServiceResponse<int> UpdateChemicalNameMasterData(ChemicalNameMasterEntity chemicalEntity)
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
                    new SqlParameter("@Selection","Update"),
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
        public static void UpdateJsonFile_MySql(string UpdateQuery)
        {
            SQLHelper.serverExcecuteNonQuery(UpdateQuery);
        }
        public static ServiceResponse<int> UpdateShiftNewData()
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Selection","UpdateNew"),
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

        public static ServiceResponse<int> UpdateChemicalMaster(int Id, bool OnOffCondition)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@ID",Id),
                     new SqlParameter("@OnOffCondition",OnOffCondition),
                    new SqlParameter("@Selection","UpdateCondition"),
                };
                int result = SQLHelper.ExecuteNonQuery("SP_ChemicalMaster", sqlParameters.ToArray());

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
        public static ServiceResponse<int> UpdateORDipTimeData(OutOfRangeSettingsDipTime _UpdateORDipTimeTable)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@ProgramNo",_UpdateORDipTimeTable.ProgramNo),
                    new SqlParameter("@StationName",_UpdateORDipTimeTable.ParameterName),
                    new SqlParameter("@DipTimeToleranceHigh",_UpdateORDipTimeTable.HighSPDipTime),
                    new SqlParameter("@DipTimeToleranceLow",_UpdateORDipTimeTable.LowSPDipTime),
                    new SqlParameter("@DipLowBypass",_UpdateORDipTimeTable.LowBypass),
                    new SqlParameter("@DipHighBypass",_UpdateORDipTimeTable.HighBypass),

                    new SqlParameter("@Selection","Update"),
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


        public static ServiceResponse<int> UpdateLoadDipTimeWithTemperature(LoadDipTimeEntity UpdateLoadDipTimeTable)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@LoadNumber",UpdateLoadDipTimeTable.LoadNumber),
                    new SqlParameter("@DipOutTime",UpdateLoadDipTimeTable.DipOutTime),
                    new SqlParameter("@DipOutShift",UpdateLoadDipTimeTable.DipOutShift),
                    new SqlParameter("@TemperatureActual",UpdateLoadDipTimeTable.TemperatureActual),
                    new SqlParameter("@TemperatureSetHigh",UpdateLoadDipTimeTable.TemperatureSetHigh),
                    new SqlParameter("@TemperatureSetLow",UpdateLoadDipTimeTable.TemperatureSetLow),
                    new SqlParameter("@StationNo",UpdateLoadDipTimeTable.StationNo),
                    new SqlParameter("@Status",UpdateLoadDipTimeTable.Status),

                    new SqlParameter("@ORTempLowSP",UpdateLoadDipTimeTable.ORTempLowSP),
                    new SqlParameter("@ORTempHighSP",UpdateLoadDipTimeTable.ORTempHighSP),
                    new SqlParameter("@ORTempAvg",UpdateLoadDipTimeTable.ORTempAvg),
                    new SqlParameter("@ORTempLowTime",UpdateLoadDipTimeTable.ORTempLowTime),
                    new SqlParameter("@ORTempHighTime",UpdateLoadDipTimeTable.ORTempHighTime),

                    new SqlParameter("@ORDipTime",UpdateLoadDipTimeTable.ORDipTime),                             

                    new SqlParameter("@Selection","WithTemperature"),
                };
                int result=SQLHelper.ExecuteNonQuery("SP_LoadDipTime", sqlParameters.ToArray());
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

        public static ServiceResponse<int> UpdateLoadDiptimeOR(string LoadNumber, int StationNo, int isTempOR, int isPHOR, int isCurrOR, int isTimeOR)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@LoadNumber",LoadNumber),
                    new SqlParameter("@StationNo",StationNo),

                    new SqlParameter("@istempOR",isTempOR),
                    new SqlParameter("@isphOR",isPHOR),
                    new SqlParameter("@iscurrOR",isCurrOR),
                    new SqlParameter("@istimeOR",isTimeOR),

                    new SqlParameter("@Selection","updateORpara"),
                };
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

        public static ServiceResponse<int> UpdateStationOutOfRange(string LoadNumber)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@LoadNumber",LoadNumber),
                    new SqlParameter("@Selection","UpdateStnOR"),
                };
                int result = SQLHelper.ExecuteNonQuery("SP_LoadData", sqlParameters.ToArray());

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

        public static ServiceResponse<int> UpdateLoadDipTimeWithTemperaturePh(LoadDipTimeEntity UpdateLoadDipTimeTable)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@LoadNumber",UpdateLoadDipTimeTable.LoadNumber),
                    new SqlParameter("@DipOutTime",UpdateLoadDipTimeTable.DipOutTime),
                    new SqlParameter("@DipOutShift",UpdateLoadDipTimeTable.DipOutShift),

                    new SqlParameter("@TemperatureActual",UpdateLoadDipTimeTable.TemperatureActual),
                    new SqlParameter("@TemperatureSetHigh",UpdateLoadDipTimeTable.TemperatureSetHigh),
                    new SqlParameter("@TemperatureSetLow",UpdateLoadDipTimeTable.TemperatureSetLow),

                    new SqlParameter("@ActualPH",UpdateLoadDipTimeTable.ActualpH),

                    new SqlParameter("@StationNo",UpdateLoadDipTimeTable.StationNo),
                    new SqlParameter("@Status",UpdateLoadDipTimeTable.Status),

                    new SqlParameter("@ORTempLowSP",UpdateLoadDipTimeTable.ORTempLowSP),
                    new SqlParameter("@ORTempHighSP",UpdateLoadDipTimeTable.ORTempHighSP),
                    new SqlParameter("@ORTempAvg",UpdateLoadDipTimeTable.ORTempAvg),
                    new SqlParameter("@ORTempLowTime",UpdateLoadDipTimeTable.ORTempLowTime),
                    new SqlParameter("@ORTempHighTime",UpdateLoadDipTimeTable.ORTempHighTime),

                    new SqlParameter("@ORpHLowSP",UpdateLoadDipTimeTable.ORpHLowSP),
                    new SqlParameter("@ORpHHighSP",UpdateLoadDipTimeTable.ORpHHighSP),
                    new SqlParameter("@ORpHLowTime",UpdateLoadDipTimeTable.ORpHLowTime),
                    new SqlParameter("@ORpHHighTime",UpdateLoadDipTimeTable.ORpHHighTime),
                    new SqlParameter("@ORpHAvg",UpdateLoadDipTimeTable.ORpHAvg),

                    new SqlParameter("@ORDipTime",UpdateLoadDipTimeTable.ORDipTime),

                    new SqlParameter("@Selection","WithTemperaturePh"),
                };
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

        public static ServiceResponse<int> UpdateLoadDipTimeWithTemperatureRectifier(LoadDipTimeEntity UpdateLoadDipTimeTable)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@LoadNumber",UpdateLoadDipTimeTable.LoadNumber),
                    new SqlParameter("@DipOutTime",UpdateLoadDipTimeTable.DipOutTime),
                    new SqlParameter("@DipOutShift",UpdateLoadDipTimeTable.DipOutShift),

                    new SqlParameter("@TemperatureActual",UpdateLoadDipTimeTable.TemperatureActual),
                    new SqlParameter("@TemperatureSetHigh",UpdateLoadDipTimeTable.TemperatureSetHigh),
                    new SqlParameter("@TemperatureSetLow",UpdateLoadDipTimeTable.TemperatureSetLow),

                    new SqlParameter("@ActualCurrent",UpdateLoadDipTimeTable.ActualCurrent),
                    new SqlParameter("@SetCurrent",UpdateLoadDipTimeTable.SetCurrent),
                    new SqlParameter("@ActualVoltage",UpdateLoadDipTimeTable.ActualVoltage),
                    new SqlParameter("@AvgCurrent",UpdateLoadDipTimeTable.AvgCurrent),
                    new SqlParameter("@AmpHr",UpdateLoadDipTimeTable.AmpHr),


                    new SqlParameter("@StationNo",UpdateLoadDipTimeTable.StationNo),
                    new SqlParameter("@Status",UpdateLoadDipTimeTable.Status),

                    new SqlParameter("@ORTempLowSP",UpdateLoadDipTimeTable.ORTempLowSP),
                    new SqlParameter("@ORTempHighSP",UpdateLoadDipTimeTable.ORTempHighSP),
                    new SqlParameter("@ORTempAvg",UpdateLoadDipTimeTable.ORTempAvg),
                    new SqlParameter("@ORTempLowTime",UpdateLoadDipTimeTable.ORTempLowTime),
                    new SqlParameter("@ORTempHighTime",UpdateLoadDipTimeTable.ORTempHighTime),

                    new SqlParameter("@ORDipTime",UpdateLoadDipTimeTable.ORDipTime),

                    new SqlParameter("@Selection","WithTemperatureRectifier"),
                };
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

        public static ServiceResponse<int> UpdateLoadDipTimeWithRectifier(LoadDipTimeEntity UpdateLoadDipTimeTable)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@LoadNumber",UpdateLoadDipTimeTable.LoadNumber),
                    new SqlParameter("@DipOutTime",UpdateLoadDipTimeTable.DipOutTime),
                    new SqlParameter("@DipOutShift",UpdateLoadDipTimeTable.DipOutShift),                    
                    new SqlParameter("@ActualCurrent",UpdateLoadDipTimeTable.ActualCurrent),
                    new SqlParameter("@SetCurrent",UpdateLoadDipTimeTable.SetCurrent),
                    new SqlParameter("@ActualVoltage",UpdateLoadDipTimeTable.ActualVoltage),
                    new SqlParameter("@AvgCurrent",UpdateLoadDipTimeTable.AvgCurrent),
                    new SqlParameter("@StationNo",UpdateLoadDipTimeTable.StationNo),
                    new SqlParameter("@Status",UpdateLoadDipTimeTable.Status),

                     new SqlParameter("@ORDipTime",UpdateLoadDipTimeTable.ORDipTime),

                    new SqlParameter("@Selection","WithRectifier"),
                };
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
        public static ServiceResponse<int> UpdateLoadDipTimeWithTemperaturePH(LoadDipTimeEntity UpdateLoadDipTimeTable)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@LoadNumber",UpdateLoadDipTimeTable.LoadNumber),
                    new SqlParameter("@DipOutTime",UpdateLoadDipTimeTable.DipOutTime),
                    new SqlParameter("@DipOutShift",UpdateLoadDipTimeTable.DipOutShift),
                    new SqlParameter("@StationNo",UpdateLoadDipTimeTable.StationNo),
                    new SqlParameter("@TemperatureActual",UpdateLoadDipTimeTable.TemperatureActual),
                    new SqlParameter("@TemperatureSetHigh",UpdateLoadDipTimeTable.TemperatureSetHigh),
                    new SqlParameter("@TemperatureSetLow",UpdateLoadDipTimeTable.TemperatureSetLow),
                    new SqlParameter("@ActualPH",UpdateLoadDipTimeTable.ActualpH),
                    new SqlParameter("@Status",UpdateLoadDipTimeTable.Status),
                    new SqlParameter("@Selection","WithTemperaturePH"),
                };
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
        public static ServiceResponse<int> UpdateLoadDipTimeWithpH(LoadDipTimeEntity UpdateLoadDipTimeTable)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@LoadNumber",UpdateLoadDipTimeTable.LoadNumber),
                    new SqlParameter("@DipOutTime",UpdateLoadDipTimeTable.DipOutTime),
                    new SqlParameter("@DipOutShift",UpdateLoadDipTimeTable.DipOutShift),                   
                    new SqlParameter("@StationNo",UpdateLoadDipTimeTable.StationNo),
                    new SqlParameter("@ActualPH",UpdateLoadDipTimeTable.ActualpH),
                    new SqlParameter("@Status",UpdateLoadDipTimeTable.Status),

                    new SqlParameter("@ORpHLowSP",UpdateLoadDipTimeTable.ORpHLowSP),
                    new SqlParameter("@ORpHHighSP",UpdateLoadDipTimeTable.ORpHHighSP),
                    new SqlParameter("@ORpHLowTime",UpdateLoadDipTimeTable.ORpHLowTime),
                    new SqlParameter("@ORpHHighTime",UpdateLoadDipTimeTable.ORpHHighTime),
                    new SqlParameter("@ORpHAvg",UpdateLoadDipTimeTable.ORpHAvg),

                    new SqlParameter("@ORDipTime",UpdateLoadDipTimeTable.ORDipTime),

                    new SqlParameter("@Selection","WithPH"),
                };
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

        public static ServiceResponse<int> UpdateLoadDipTime(LoadDipTimeEntity UpdateLoadDipTimeTable)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@LoadNumber",UpdateLoadDipTimeTable.LoadNumber),
                    new SqlParameter("@DipOutTime",UpdateLoadDipTimeTable.DipOutTime),
                    new SqlParameter("@DipOutShift",UpdateLoadDipTimeTable.DipOutShift),
                    new SqlParameter("@StationNo",UpdateLoadDipTimeTable.StationNo),
                    new SqlParameter("@Status",UpdateLoadDipTimeTable.Status),
                    new SqlParameter("@Selection","WithDipTime"),
                };
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

        public static ServiceResponse<int> UpdateLoadData(LoadDataEntity UpdateLoadDataTable)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@LoadNumber",UpdateLoadDataTable.LoadNumber),
                    new SqlParameter("@LoadOutTime",UpdateLoadDataTable.LoadOutTime),
                    new SqlParameter("@LoadOutShift",UpdateLoadDataTable.LoadOutShift),
                    new SqlParameter("@LoadType",UpdateLoadDataTable.LoadType),
                    new SqlParameter("@isEnd",UpdateLoadDataTable.isEnd),
                    new SqlParameter("@Selection","Update"),
                };
                int result = SQLHelper.ExecuteNonQuery("SP_LoadData", sqlParameters.ToArray());
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
        public static ServiceResponse<int> UpdateEventsData(EventLogEntity InsertEventData)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Description",InsertEventData.Description),
                    new SqlParameter("@EndDate",InsertEventData.EndDateTime),
                    new SqlParameter("@isComplete",InsertEventData.isComplete),
                    new SqlParameter("@Selection","UpdateData"),
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
        public static ServiceResponse<int> UpdateAlarmMaster(AlarmMasterEntity InsertAlarmsData)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@AlarmName",InsertAlarmsData.AlarmName),
                    new SqlParameter("@isON",InsertAlarmsData.isON),
                    new SqlParameter("@isACK",InsertAlarmsData.isACK),
                    new SqlParameter("@isOFF",InsertAlarmsData.isOFF),
                    new SqlParameter("@CausesDownTime",InsertAlarmsData.CausesDownTime),
                    new SqlParameter("@Selection","UpdateDataAlarm"),
                };
                DataTable _DTRecords = new DataTable();
                int result = SQLHelper.ExecuteNonQuery("SPAlarmMaster", sqlParameters.ToArray());
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
        public static ServiceResponse<int> UpdateAlarmMasterOffAlarm(string AlarmText)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@AlarmText",AlarmText),
                    new SqlParameter("@isON",false),
                    new SqlParameter("@isACK",false),
                    new SqlParameter("@isOFF",true),
                    new SqlParameter("@Selection","UpdateMasterOff"),
                };
                DataTable _DTRecords = new DataTable();
                int result = SQLHelper.ExecuteNonQuery("SPAlarmMaster", sqlParameters.ToArray());
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
        public static ServiceResponse<int> UpdateAlarmMasterACK(AlarmMasterEntity InsertAlarmsData)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@AlarmName",InsertAlarmsData.AlarmName),
                    new SqlParameter("@isACK",InsertAlarmsData.isACK),
                    new SqlParameter("@Selection","UpdateDataAlarmACK"),
                };
                DataTable _DTRecords = new DataTable();
                int result = SQLHelper.ExecuteNonQuery("SPAlarmMaster", sqlParameters.ToArray());
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
        public static ServiceResponse<int> UpdateNewUserData(UserMasterEntity InsertUserMaster)
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
                    new SqlParameter("@Selection","UpdateData"),
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

        public static ServiceResponse<int> UpdateNextLoadSettingData(NextLoadSettingsEntity InsertNextloadMasterSetting)
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
                    new SqlParameter("@IsInReport",InsertNextloadMasterSetting.isInReport),
                    new SqlParameter("@status","UPDATE"),
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

        public static ServiceResponse<int> UpdatePartMasterData(string query)
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

        public static ServiceResponse<int> UpdateNextLoadSettingFormula(string parametername, string formula)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            string query = "update NextLoadMasterSettings set CalculationFormula='" + formula + "', IsCalculationRequired='True' where ParameterName like '" + parametername + "'";

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

        public static ServiceResponse<int> UpdateHangerTypeMasterData(DataRow dr)
        {
            ServiceResponse<int> _FinalAccessResponse = new ServiceResponse<int>();
            string query = "update HangerTypeMaster set PartQuantity = " + dr["PartQuantity"] + " where HangerType like '" + dr["HangerType"] + "'";

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
        #endregion
    }
}
