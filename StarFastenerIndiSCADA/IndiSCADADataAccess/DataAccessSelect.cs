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
    public static class DataAccessSelect
    {
        #region Public/Private Property
        #endregion
        #region Public Private  method

        #region log4settings

        public static ServiceResponse<DataTable> SelectLog4SettingData(string Query)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();

            try
            {
                DataTable _DTRecords = new DataTable();
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",Query),
                        };
                SQLHelper.Fill(_DTRecords, "sp_TableCreateUsingQuery", sqlParameters.ToArray());

                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        #endregion
        public static ServiceResponse<DataTable> HomeViewGraphDataShiftWise(string ParameterType, DateTime StartDate, DateTime EndDate, int ShiftNumber, bool CompletedLoadOnly)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@StartDate",StartDate.ToString("yyyy/MM/dd HH:mm:ss tt")),
                    new SqlParameter("@EndDate",EndDate.ToString("yyyy/MM/dd HH:mm:ss tt")),
                    new SqlParameter("@ShiftNumber",ShiftNumber),
                    new SqlParameter("@ParameterType",ParameterType),
                    new SqlParameter("@ShowCompletedLoads",CompletedLoadOnly),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SPHomeView", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static ServiceResponse<DataTable> HomeViewGraphData(string ParameterType, DateTime StartDate, DateTime EndDate, bool CompletedLoadOnly)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@StartDate",StartDate.ToString("yyyy/MM/dd HH:mm:ss")),
                    new SqlParameter("@EndDate",EndDate.ToString("yyyy/MM/dd HH:mm:ss")),
                    new SqlParameter("@ParameterType",ParameterType),
                   // new SqlParameter("@ShowCompletedLoads",CompletedLoadOnly),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SPHomeView", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
                _FinalAccessResponse.Status = ResponseType.S;
            }
            catch (Exception Ex)
            {
                _FinalAccessResponse.Response = null;
                _FinalAccessResponse.Message = Ex.Message;
                _FinalAccessResponse.Status = ResponseType.E;
                _FinalAccessResponse.ErrorLevel = ErrorLevel.A;
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic HomeViewGraphData() Error ", DateTime.Now.ToString(), Ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _FinalAccessResponse;
        }
        public static ServiceResponse<DataTable> ReturnDataTableForStation(string ParamaterName, string SPName, int StationNo)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@StationNo",StationNo),
                    new SqlParameter("@Selection",ParamaterName),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, SPName, sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        #region downtime shift      

        public static ServiceResponse<DataTable> SelectShiftWiseDownTimeSummary(DateTime ShiftDate, int ShiftNo)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                string sqlQry = "SELECT * From ShiftWiseDownTimeSummary where  CAST(ShiftDateTime as DATE) = '" + ShiftDate.ToShortDateString() + "' and ShiftNo='" + ShiftNo + "' order by ShiftNo asc";
                DataTable _DTRecords = new DataTable();
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",sqlQry),
                        };
                SQLHelper.Fill(_DTRecords, "sp_TableCreateUsingQuery", sqlParameters.ToArray());

                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
                _FinalAccessResponse.Status = ResponseType.S;


                //List<SqlParameter> sqlParameters = new List<SqlParameter>()
                //{
                //    new SqlParameter("@ShiftDate",ShiftDate.ToShortDateString()),
                //    new SqlParameter("@ShiftNumber",ShiftNo.ToString()),
                //    new SqlParameter("@Selection","ShiftWiseDownTimeSummary"),
                //};
                //DataTable _DTRecords = new DataTable();
                //SQLHelper.Fill(_DTRecords, "SP_ShiftMaster", sqlParameters.ToArray());
                //_FinalAccessResponse.Response = _DTRecords;
                //_FinalAccessResponse.Message = "";
                //_FinalAccessResponse.Status = ResponseType.S;
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

        public static ServiceResponse<DataTable> SelectShiftTimeDetails()
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Selection","AllRunningShiftData"),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SP_ShiftMaster", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
        #endregion


        #region "Chemical name master"
        public static ServiceResponse<DataTable> getChemicalNameData(ChemicalNameMasterEntity chemicalEntity)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@ChemicalName",chemicalEntity.ChemicalName),
                    new SqlParameter("@PumpNumber",chemicalEntity.PumpName),
                    new SqlParameter("@PumpName",chemicalEntity.PumpName),
                    new SqlParameter("@Selection","isExists"),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SP_ChemicalNameMaster", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
        public static ServiceResponse<DataTable> getAllChemicalNameData()
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Selection","SelectAll"),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SP_ChemicalNameMaster", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
        public static DataTable getAllChemicalNamePercentageData()
        {
           DataTable _FinalAccessResponse = new DataTable();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Selection","SelectAll"),
                }; 
                SQLHelper.Fill(_FinalAccessResponse, "SP_ChemicalNameMaster", sqlParameters.ToArray());
            }
            catch (Exception Ex)
            {
            }
            return _FinalAccessResponse;
        }


        public static ServiceResponse<DataTable> getTotalPercentageEnteredForChemical(ChemicalNameMasterEntity chemicalEntity)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@ChemicalName",chemicalEntity.ChemicalName),
                    new SqlParameter("@PumpNumber",chemicalEntity.PumpNumber),
                    new SqlParameter("@PumpName",chemicalEntity.PumpName),
                    new SqlParameter("@Selection","TotalChemicalPercent"),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SP_ChemicalNameMaster", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static ServiceResponse<DataTable> SelectChemicalMasterData(string query)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();

            try
            {
                string sqlQry = query;
                DataTable _DTRecords = new DataTable();
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",sqlQry),
                        };
                SQLHelper.Fill(_DTRecords, "sp_TableCreateUsingQuery", sqlParameters.ToArray());

                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
        public static ServiceResponse<DataTable> SelectAllChemicalNameMasterData()
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Selection","AllChemicalNameData"),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SP_ChemicalNameMaster", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static ServiceResponse<DataTable> getRemainingPumpData()
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Selection","RemainingPump"),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SP_ChemicalNameMaster", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
        #endregion

        #region "ShiftMaster"
        public static ServiceResponse<DataTable> getShiftMasterData(string shiftno)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@ShiftNumber",shiftno),
                    new SqlParameter("@Selection","IsShiftNoPresent"),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SP_ShiftMaster", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static ServiceResponse<DataTable> SelectAllShiftMasterData()
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Selection","AllShiftData"),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SP_ShiftMaster", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static ServiceResponse<DataTable> SelectAllRunningShiftMasterData(string para)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Selection",para),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SP_ShiftMaster", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
        #endregion



        public static ServiceResponse<DataTable> getHDEProcess()
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();

            try
            {
                string sqlQry = "select HDEProcess from PartMaster where isSelectedForNextLoad=1 ";
                DataTable _DTRecords = new DataTable();
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",sqlQry),
                        };
                SQLHelper.Fill(_DTRecords, "sp_TableCreateUsingQuery", sqlParameters.ToArray());

                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static ServiceResponse<DataTable> getConditionFromChemicalMaster()
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();

            try
            {
                string sqlQry = "SELECT OnOffCondition FROM ChemicalMaster";
                DataTable _DTRecords = new DataTable();
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",sqlQry),
                        };
                SQLHelper.Fill(_DTRecords, "sp_TableCreateUsingQuery", sqlParameters.ToArray());

                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
        public static ServiceResponse<DataTable> getAllFromChemicalMaster()
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();

            try
            {
                string sqlQry = "SELECT * FROM ChemicalMaster";
                DataTable _DTRecords = new DataTable();
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",sqlQry),
                        };
                SQLHelper.Fill(_DTRecords, "sp_TableCreateUsingQuery", sqlParameters.ToArray());

                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
        public static ServiceResponse<DataTable> getDataPartMasterForDataLogging1()
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                string sqlQry = "SELECT * FROM PartMaster where [isSelectedforNextLoad]='1'";
                DataTable _DTRecords = new DataTable();
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",sqlQry),
                        };
                SQLHelper.Fill(_DTRecords, "sp_TableCreateUsingQuery", sqlParameters.ToArray());

                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static ServiceResponse<DataTable> getDataNextLoadMasterSettings()
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();

            try
            {
                string sqlQry = "SELECT * FROM NextLoadMasterSettings where IsInReport='True'";
                DataTable _DTRecords = new DataTable();
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",sqlQry),
                        };
                SQLHelper.Fill(_DTRecords, "sp_TableCreateUsingQuery", sqlParameters.ToArray());

                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
        public static ServiceResponse<DataTable> DTCheckLoadisPResent(string Lno)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();

            try
            {

                string sqlQry = "SELECT * From LoadPartDetails where LoadNumber like '" + Lno + "'";
                DataTable _DTRecords = new DataTable();
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",sqlQry),
                        };
                SQLHelper.Fill(_DTRecords, "sp_TableCreateUsingQuery", sqlParameters.ToArray());

                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static ServiceResponse<DataTable> getLoadNumber(int StationNo)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();

            try
            {

                string sqlQry = "select LoadNumber from LoadDipTime where StationNo=" + StationNo + " and Status=0 order by LoadNumber desc";
                DataTable _DTRecords = new DataTable();
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",sqlQry),
                        };
                SQLHelper.Fill(_DTRecords, "sp_TableCreateUsingQuery", sqlParameters.ToArray());

                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
        public static ServiceResponse<DataTable> SelectORDipTimeDataFromprogramName(string programName)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@ProgramNo",programName),
                    new SqlParameter("@Selection","selectByProgram"),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SP_ORdipTime", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static ServiceResponse<DataTable> SelectDistinctprogramName()
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Selection","selectDistinctProgram"),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SP_ORdipTime", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static ServiceResponse<DataTable> IsLoadNumberPresentInLoadData(string LoadNumber)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@LoadNumber",LoadNumber),
                    new SqlParameter("@Selection","IsPresentLoadNo"),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SP_LoadData", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
        public static ServiceResponse<DataTable> SelectAlarmData(string AlarmName)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@AlarmName",AlarmName),
                    new SqlParameter("@Selection","SelectStartUpAlarm"),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SPAlarmData", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
       
        public static ServiceResponse<DataTable> IsLoadNumberPresentInLoadDipTime(string LoadNumber,int StationNo)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@LoadNumber",LoadNumber),
                    new SqlParameter("@StationNo",StationNo),
                    new SqlParameter("@Selection","IsPresentLoadNo"),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SP_LoadDipTime", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
        public static ServiceResponse<string> SelectPartNumberFromLoadNo(string LoadNumber)
        {
            ServiceResponse<string> _FinalAccessResponse = new ServiceResponse<string>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@LoadNumber",LoadNumber),
                    new SqlParameter("@Selection","SelectPartName"),
                };

                _FinalAccessResponse = SQLHelper.ReturnValueSP("SelectPartName", sqlParameters.ToArray());
              
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

        public static ServiceResponse<DataTable> SelectPartNumberFromLoadPartDetails(string LoadNumber)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@LoadNumber",LoadNumber),
                    new SqlParameter("@Selection","SelectPart"),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SP_SelectPartNameInLoadPartDetails", sqlParameters.ToArray());             
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static ServiceResponse<string> GetAlarmHelpFromAlarmName(AlarmDataEntity _AlarmDataEntity)
        {
            ServiceResponse<string> _FinalAccessResponse = new ServiceResponse<string>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@AlarmName",_AlarmDataEntity.AlarmName),
                    new SqlParameter("@ReturnValuestring",SqlDbType.NVarChar, 1000),
                };

                _FinalAccessResponse = SQLHelper.ReturnValueSPString("SPAlarmHelp", sqlParameters.ToArray());

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
        public static ServiceResponse<DataTable> SelectEventMasterData()
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Selection","Select"),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SPEventMaster", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
        
        public static ServiceResponse<DataTable> GetAlarmAndEventHistory(DateTime StartDate,DateTime EndDate)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@StartDate",StartDate),
                    new SqlParameter("@EndDate",EndDate),
                    new SqlParameter("@Selection","AlarmEventHistory"),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "EventsDataLog", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
        public static ServiceResponse<DataTable> SelectAlarmMasterData()
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Selection","Select"),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SPAlarmMaster", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
        public static ServiceResponse<DataTable> SelectIOStatusParameterDataDataTable(string ParameterType)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@ParameterType",ParameterType),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SPScreenParameter", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
        public static ServiceResponse<DataTable> HomeViewGraphData(string ParameterType,DateTime StartDate,DateTime EndDate)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@StartDate",StartDate.ToString("yyyy/MM/dd HH:mm:ss")),
                    new SqlParameter("@EndDate",EndDate.ToString("yyyy/MM/dd HH:mm:ss")),
                    new SqlParameter("@ParameterType",ParameterType),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SPHomeView", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static ServiceResponse<DataTable> HomeViewGraphDataShiftWise(string ParameterType, DateTime StartDate, DateTime EndDate,int ShiftNumber)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@StartDate",StartDate.ToString("yyyy/MM/dd HH:mm:ss tt")),
                    new SqlParameter("@EndDate",EndDate.ToString("yyyy/MM/dd HH:mm:ss tt")),
                    new SqlParameter("@ShiftNumber",ShiftNumber),
                    new SqlParameter("@ParameterType",ParameterType),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SPHomeView", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
        public static ServiceResponse<DataTable> ReturnDataTable(string ParamaterName,string SPName)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Selection",ParamaterName),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, SPName, sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
        public static ServiceResponse<string> LoginUserAccess(UserMasterEntity _UserData)
        {
            ServiceResponse<string> _FinalAccessResponse = new ServiceResponse<string>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@UserName",_UserData.UserName),
                    new SqlParameter("@UserPassword",_UserData.UserPassword),
                    new SqlParameter("@UserRole",_UserData.UserRole),
                    new SqlParameter("@ReturnValue",ParameterDirection.Output),
                    new SqlParameter("@Selection","Login"),
                };

                _FinalAccessResponse = SQLHelper.ReturnValueSP("UserMasterSP", sqlParameters.ToArray());

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
        public static ServiceResponse<DataTable> SelectUserMasterData()
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Selection","Select"),
                    new SqlParameter("@ReturnValue",ParameterDirection.Output),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "UserMasterSP", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static ServiceResponse<DataTable> SelectNextLoadMasterSettingData()
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@status","SELECT"),
                };

                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "sp_NextLoadMasterSettingData", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static ServiceResponse<DataTable> SelectScreenwiseNextLoadMasterSettingData(string screenName)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@ScreenName",screenName),
                };

                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SelectScreenwiseNextLoadMasterSettingData", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static ServiceResponse<DataTable> SelectPartMasterDataforColumn(string colname, string colvalue)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();

            try
            {
                string sqlQry = "Select * from PartMaster where " + colname + " ='" + colvalue + "'";
                DataTable _DTRecords = new DataTable();
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",sqlQry),
                        };
                SQLHelper.Fill(_DTRecords, "sp_TableCreateUsingQuery", sqlParameters.ToArray());

                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static ServiceResponse<DataTable> SelectPartMasterData(string partnumber, string selection, string Description)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();

            try
            {
                DataTable _DTRecords = new DataTable();
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@PartNumber",partnumber),
                            new SqlParameter("@Description",Description),
                            new SqlParameter("@status",selection),
                        };
                SQLHelper.Fill(_DTRecords, "sp_PartMasterData", sqlParameters.ToArray());

                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static ServiceResponse<DataTable> SelectPartNumberListData()
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();

            try
            {
                string sqlQry = "Select distinct PartNumber from PartMaster";
                DataTable _DTRecords = new DataTable();
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",sqlQry),
                        };
                SQLHelper.Fill(_DTRecords, "sp_TableCreateUsingQuery", sqlParameters.ToArray());

                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static DataTable SelectChemicalconsumptionData()
        {
            DataTable _FinalAccessResponse = new DataTable();

            try
            {
                string sqlQry = "Select * from ChemicalMaster";
                DataTable _DTRecords = new DataTable();
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",sqlQry),
                        };
                SQLHelper.Fill(_DTRecords, "sp_TableCreateUsingQuery", sqlParameters.ToArray());

                _FinalAccessResponse = _DTRecords;
            }
            catch (Exception Ex)
            {
                _FinalAccessResponse = null;
            }
            return _FinalAccessResponse;
        }
        public static ServiceResponse<DataTable> SelectPartDesciptionListData()
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();

            try
            {
                string sqlQry = "Select distinct Description from PartMaster";
                DataTable _DTRecords = new DataTable();
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",sqlQry),
                        };
                SQLHelper.Fill(_DTRecords, "sp_TableCreateUsingQuery", sqlParameters.ToArray());

                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static ServiceResponse<DataTable> SelectHagerTypeMasterData(string query)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();

            try
            {
                string sqlQry = query;
                DataTable _DTRecords = new DataTable();
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",sqlQry),
                        };
                SQLHelper.Fill(_DTRecords, "sp_TableCreateUsingQuery", sqlParameters.ToArray());

                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static ServiceResponse<DataTable> SelectPartMasterWithQuery(string query)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();

            try
            {
                string sqlQry = query;
                DataTable _DTRecords = new DataTable();
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",sqlQry),
                        };
                SQLHelper.Fill(_DTRecords, "sp_TableCreateUsingQuery", sqlParameters.ToArray());

                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static ServiceResponse<DataTable> SelectAddressFromAlarmMasterUsingGroupName(string GroupName)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Selection","SelectDataByGroup"),
                    new SqlParameter("@AlarmGroup",GroupName),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SPAlarmMaster", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static ServiceResponse<DataTable> CurentLanguageData(string ParameterType)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                { 
                    new SqlParameter("@ParameterType",ParameterType),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SPHomeView", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
        public static ServiceResponse<DataTable> UpdateCurentLanguage(string ParameterType, string Selectedlang)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Selectedlang",Selectedlang),
                    new SqlParameter("@ParameterType",ParameterType),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SPHomeView", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
        

        #region Datalogging_LoadPartDetails
        public static ServiceResponse<DataTable> getDataNextLoadMasterSettingsForDataLogging()
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@status","DATALOG"),
                };

                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "sp_NextLoadMasterSettingData", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static ServiceResponse<DataTable> getDataPartMasterForDataLogging()
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();

            try
            {
                DataTable _DTRecords = new DataTable();
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {                         
                            new SqlParameter("@status","DATALOG"),
                        };
                SQLHelper.Fill(_DTRecords, "sp_PartMasterData", sqlParameters.ToArray());

                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
        #endregion
        #region"Overview"
        public static ServiceResponse<DataTable> OverviewStationSelect()
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Selection","Select"),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SPOverview", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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

        public static ServiceResponse<DataTable> OverviewStationSelectLine(int lineno)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@LineNo",lineno),
                    new SqlParameter("@Selection","SelectLine"),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SPOverview", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
        public static ServiceResponse<DataTable> TrendHistoricalData(DateTime StartDate,DateTime EndDate,string Selection)
        {
            ServiceResponse<DataTable> _FinalAccessResponse = new ServiceResponse<DataTable>();
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@StartDate",StartDate),
                    new SqlParameter("@EndDate",EndDate),
                    new SqlParameter("@Selection",Selection),
                };
                DataTable _DTRecords = new DataTable();
                SQLHelper.Fill(_DTRecords, "SPTrendHistorical", sqlParameters.ToArray());
                _FinalAccessResponse.Response = _DTRecords;
                _FinalAccessResponse.Message = "";
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
        #endregion
        #endregion
    }
}
