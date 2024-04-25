using IndiSCADAEntity.Entity;
using IndiSCADAGlobalLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADATranslation
{

    public static class HomeViewTranslation
    {
        #region Public Private  method
        public static string ValueFromArray(string[] InputArray, int index)
        {
            try
            {
                return InputArray[index];
            }
            catch
            {
                return "0";
            }
        }

        public static string[] getDurMinSec(string[] duration)
        {
            try
            {
                string[] result = new string[duration.Length];
                int index = 0;
                foreach (string dur in duration)
                {
                    int totalSeconds = Convert.ToInt32(dur);
                    int seconds = totalSeconds % 60;
                    int minutes = (totalSeconds / 60) % 60;
                    int hour = totalSeconds / 3600;
                    result[index] = hour + ":" + minutes + ":" + seconds;
                    index++;
                }
                return result;
            }
            catch (Exception ex)
            {
                //log.Error("TankDetailsLogic GetDuration()" + ex.Message + IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return null;
            }
        }
        public static ServiceResponse<IList> GetLiveZincPlatingSummary()
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                //ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.SelectIOStatusParameterDataDataTable("ZincPlatingTankNames");
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.ReturnDataTable("StationMasterIOT", "SPHomeView");
                if (_LocalData.HasError())
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = _LocalData.Message;
                    _FinalTranslationResponse.Status = ResponseType.E;
                    _FinalTranslationResponse.ErrorLevel = ErrorLevel.T;
                    return _FinalTranslationResponse;
                }



                //Translation
                if (_LocalData.Response != null)
                {
                    if (_LocalData.Response.Rows.Count > 0)
                    {
                        //get and assign live parameter values from array
                        //try
                        //{
                        //    string[] LiveZincPlating = IndiSCADAGlobalLibrary.TagList.LiveZincPlatingLine;
                        //    _LocalData.Response.Columns.Add("ParameterValue");
                        //    for (int i = 0; i < LiveZincPlating.Count(); i++)
                        //    {
                        //        _LocalData.Response.Rows[i]["ParameterValue"] = LiveZincPlating[i];
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    log.Error("HomeviewTranslation DoWork() GetLiveZincPlatingSummary()", DateTime.Now.ToString(), ex.Message, "No", true);
                        //}
                        //DataTable DT = _LocalData.Response;


                        //var _Query = (from DataRow dr in _LocalData.Response.Rows
                        //              select new TankDetailsEntity()
                        //              {
                        //                  StationName = (dr["ParameterName"].ToString()),
                        //                  Duration = (dr["ParameterName"].ToString()),
                        //                  ActualTemperature = (dr["ParameterName"].ToString()),
                        //                  ActualCurrent = (dr["ParameterName"].ToString()),
                        //                  pH = (dr["ParameterName"].ToString()),
                        //                  Conductivity = (dr["ParameterName"].ToString()),
                        //                  //ParameterValue = (int)Math.Round(float.Parse(dr["ParameterValue"].ToString())),

                        //              });


                        //use for testing   
                        //string[] ActualSP = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25" };
                        //IndiSCADAGlobalLibrary.TagList.TemperatureActual = ActualSP;
                        //string[] duration1 = new string[] { "1115", "5515", "5155", "15154", "55", "657", "757", "578", "7579", "1750", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25",
                        // "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25" ,
                        // "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19" };
                        //IndiSCADAGlobalLibrary.TagList.LoadDipTime = duration1;
                        //string[] ActualCurrent1 = new string[] {"1", "2", "3", "4", "5" , "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20","21" };
                        //IndiSCADAGlobalLibrary.TagList.ActualCurrent = ActualCurrent1;
                        //string[] pHActual1 = new string[] { "1", "2", "3", "4" };
                        //IndiSCADAGlobalLibrary.TagList.pHActual = pHActual1;
                        //string[] conductivity = new string[] { "1"};
                        //IndiSCADAGlobalLibrary.TagList.ActualConductivity = conductivity;

                        //string[] Temperature = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "17", "18", "19", "20", "21", "22", "23", "24", "25" };
                        //string[] ActualCurrent = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "17", "18", "19", "20", "21", "22", "23", "24", "25" };
                        //string[] ActualpH = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "17", "18", "19", "20", "21", "22", "23", "24", "25" };
                        //string[] Actualconductivity = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "17", "18", "19", "20", "21", "22", "23", "24", "25" };
                        //string[] Duration = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "17", "18", "19", "20", "21", "22", "23", "24", "25" };


                        string[] Temperature = IndiSCADAGlobalLibrary.TagList.TemperatureActual;
                        string[] ActualCurrent = IndiSCADAGlobalLibrary.TagList.ActualCurrent;
                        string[] ActualpH = IndiSCADAGlobalLibrary.TagList.pHActual;
                      //  string[] Actualconductivity = IndiSCADAGlobalLibrary.TagList.ActualConductivity;
                        string[] Duration = IndiSCADAGlobalLibrary.TagList.LoadDipTime; //DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("DipTimeForDisplayDipTime");  

                        string[] DurationHHMM = new string[] { };
                        try
                        {
                            DurationHHMM = getDurMinSec(Duration);
                        }
                        catch (Exception ex)
                        {
                            //log.Error("TankDetailsLogic DurationHHMM getDurMinSec()" + ex.Message + IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }

                        DataTable _DTStationList = _LocalData.Response;
                        ObservableCollection<TankDetailsEntity> _TankDetails = new ObservableCollection<TankDetailsEntity>();
                        foreach (DataRow row in _DTStationList.Rows)
                        {
                            //if (Convert.ToBoolean(row["SendToWebIndiscada"].ToString()) == true)
                            //{
                            TankDetailsEntity _TankDetailsEntity = new TankDetailsEntity();
                            _TankDetailsEntity.StationName = row["StationName"].ToString();//station name

                            try
                            {
                                _TankDetailsEntity.Duration = ValueFromArray(DurationHHMM, Convert.ToInt16(row["StationNumber"].ToString()));
                                if (row["TemperatureValueIndex"].ToString().Length > 0)
                                {
                                    try
                                    {
                                        int Tempindex = Convert.ToInt16(row["TemperatureValueIndex"].ToString());
                                        _TankDetailsEntity.ActualTemperature = ValueFromArray(Temperature, Tempindex) + "  °C";
                                    }
                                    catch { }
                                }
                                if (row["CurrentValueIndex"].ToString().Length > 0)
                                {
                                    try
                                    {
                                        int Tempindex = Convert.ToInt16(row["CurrentValueIndex"].ToString());
                                        _TankDetailsEntity.ActualCurrent = ValueFromArray(ActualCurrent, Tempindex) + "  A";
                                    }
                                    catch { }
                                }
                                if (row["pHValueIndex"].ToString().Length > 0)
                                {
                                    try
                                    {
                                        int pHindex = Convert.ToInt16(row["pHValueIndex"].ToString());
                                        _TankDetailsEntity.pH = ValueFromArray(ActualpH, pHindex) + "  pH";
                                    }
                                    catch { }
                                }
                                //if (row["ConductivityValueIndex"].ToString().Length > 0)
                                //{
                                //    try
                                //    {
                                //        int Conductiviyindex = Convert.ToInt16(row["ConductivityValueIndex"].ToString());
                                //        _TankDetailsEntity.Conductivity = ValueFromArray(Actualconductivity, Conductiviyindex) + "  ";
                                //    }
                                //    catch { }
                                //}
                            }
                            catch (Exception ex)
                            {
                                //log.Error("TankDetailsLogic GetTanlDetails() Error in foreach loop " + ex.Message + IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }



                            _TankDetails.Add(_TankDetailsEntity);
                        }


                        var _Query = new List<TankDetailsEntity>(_TankDetails);

                        _FinalTranslationResponse.Response = _Query.ToList<TankDetailsEntity>();
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
                else
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = "Data Received Successfuly";
                    _FinalTranslationResponse.Status = ResponseType.S;
                }
            }
            catch (Exception Ex)
            {
                _FinalTranslationResponse.Response = null;
                _FinalTranslationResponse.Message = Ex.Message;
                _FinalTranslationResponse.Status = ResponseType.E;
                _FinalTranslationResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalTranslationResponse;
        }
        public static ServiceResponse<IList> GetPartArea(int ShiftNumber, bool CompletedLoadOnly)
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                DateTime StartDate = new DateTime(), EndDate = new DateTime();
                StartDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
                EndDate = StartDate.AddDays(1);
                //int currentseconds = DateTime.Now.Hour * 60 * 60;
                int currentseconds = Convert.ToInt32(TimeSpan.Parse(DateTime.Now.ToShortTimeString()).TotalSeconds);
                if (currentseconds > 0 && currentseconds < 28800)
                {
                    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
                    StartDate = EndDate.AddDays(-1);
                }

                //DateTime currentdate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "00:00:00");
                //int result = TimeSpan.Compare(DateTime.Now.TimeOfDay, currentdate.TimeOfDay);
                //if (result >= 0)
                //{
                //    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "08:00:00");
                //    StartDate = EndDate.AddDays(-1);
                //}
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphDataShiftWise("TotalArea_Shift", StartDate, EndDate, ShiftNumber, CompletedLoadOnly);
                if (_LocalData.HasError())
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = _LocalData.Message;
                    _FinalTranslationResponse.Status = ResponseType.E;
                    _FinalTranslationResponse.ErrorLevel = ErrorLevel.T;
                    return _FinalTranslationResponse;
                }
                //Translation
                if (ShiftNumber == 1)
                {
                    if (_LocalData.Response != null)
                    {
                        if (_LocalData.Response.Rows.Count > 0)
                        {
                            var _Query = (from DataRow dr in _LocalData.Response.Rows
                                          select new HomeEntity()
                                          {
                                              Shift1PartArea = (long)Math.Round(float.Parse(dr["TotalArea_Shift"].ToString())),
                                          });
                            _FinalTranslationResponse.Response = _Query.ToList<HomeEntity>();
                            _FinalTranslationResponse.Message = "Data Received Successfuly";
                            _FinalTranslationResponse.Status = ResponseType.S;
                        }
                        else
                        {
                            _FinalTranslationResponse.Response = null;
                            _FinalTranslationResponse.Message = "Data Received Successfuly";
                            _FinalTranslationResponse.Status = ResponseType.S;
                        }
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
                else if (ShiftNumber == 2)
                {
                    if (_LocalData.Response != null)
                    {
                        if (_LocalData.Response.Rows.Count > 0)
                        {
                            var _Query = (from DataRow dr in _LocalData.Response.Rows
                                          select new HomeEntity()
                                          {
                                              Shift2PartArea = (int)Math.Round(float.Parse(dr["TotalArea_Shift"].ToString())),
                                          });
                            _FinalTranslationResponse.Response = _Query.ToList<HomeEntity>();
                            _FinalTranslationResponse.Message = "Data Received Successfuly";
                            _FinalTranslationResponse.Status = ResponseType.S;
                        }
                        else
                        {
                            _FinalTranslationResponse.Response = null;
                            _FinalTranslationResponse.Message = "Data Received Successfuly";
                            _FinalTranslationResponse.Status = ResponseType.S;
                        }
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
                else if (ShiftNumber == 3)
                {
                    if (_LocalData.Response != null)
                    {
                        if (_LocalData.Response.Rows.Count > 0)
                        {
                            var _Query = (from DataRow dr in _LocalData.Response.Rows
                                          select new HomeEntity()
                                          {
                                              Shift3PartArea = (int)Math.Round(float.Parse(dr["TotalArea_Shift"].ToString())),
                                          });
                            _FinalTranslationResponse.Response = _Query.ToList<HomeEntity>();
                            _FinalTranslationResponse.Message = "Data Received Successfuly";
                            _FinalTranslationResponse.Status = ResponseType.S;
                        }
                        else
                        {
                            _FinalTranslationResponse.Response = null;
                            _FinalTranslationResponse.Message = "Data Received Successfuly";
                            _FinalTranslationResponse.Status = ResponseType.S;
                        }
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
            }
            catch (Exception Ex)
            {
                _FinalTranslationResponse.Response = null;
                _FinalTranslationResponse.Message = Ex.Message;
                _FinalTranslationResponse.Status = ResponseType.E;
                _FinalTranslationResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalTranslationResponse;
        }
        public static ServiceResponse<IList> GetPartAreaDataSummary(bool CompletedLoadOnly)
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                DateTime StartDate = new DateTime(), EndDate = new DateTime();
                StartDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "08:00:00");
                EndDate = StartDate.AddDays(1);
                //int currentseconds = DateTime.Now.Hour * 60 * 60;
                int currentseconds = Convert.ToInt32(TimeSpan.Parse(DateTime.Now.ToShortTimeString()).TotalSeconds);
                if (currentseconds > 0 && currentseconds < 28800)
                {
                    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "08:00:00");
                    StartDate = EndDate.AddDays(-1);
                }

                //DateTime currentdate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "00:00:00");
                //int result = TimeSpan.Compare(DateTime.Now.TimeOfDay, currentdate.TimeOfDay);
                //if (result >= 0)
                //{
                //    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "08:00:00");
                //    StartDate = EndDate.AddDays(-1);
                //}
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("PartAreaSummary", StartDate, EndDate, CompletedLoadOnly);
                if (_LocalData.HasError())
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = _LocalData.Message;
                    _FinalTranslationResponse.Status = ResponseType.E;
                    _FinalTranslationResponse.ErrorLevel = ErrorLevel.T;
                    return _FinalTranslationResponse;
                }
                //Translation
                if (_LocalData.Response != null)
                {
                    if (_LocalData.Response.Rows.Count > 0)
                    {
                        var _Query = (from DataRow dr in _LocalData.Response.Rows
                                      select new HomeEntity()
                                      {
                                          PartName = (dr["PartNumber"].ToString()),
                                          PartArea = (long)Math.Round(double.Parse(dr["PartAreaCount"].ToString())),
                                         // PartArea = double.Parse(dr["PartAreaCount"].ToString()),
                                      });
                        _FinalTranslationResponse.Response = _Query.ToList<HomeEntity>();
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
                else
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = "Data Received Successfuly";
                    _FinalTranslationResponse.Status = ResponseType.S;
                }
            }
            catch (Exception Ex)
            {
                _FinalTranslationResponse.Response = null;
                _FinalTranslationResponse.Message = Ex.Message;
                _FinalTranslationResponse.Status = ResponseType.E;
                _FinalTranslationResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalTranslationResponse;
        }

        public static ServiceResponse<IList> GetPreviousDowntimeShiftSummaryData()
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                ServiceResponse<DataTable> _ShiftLocalData = IndiSCADADataAccess.DataAccessSelect.SelectAllRunningShiftMasterData("AllRunningShiftData");
                DataTable ShiftInfo_DT = _ShiftLocalData.Response;

                string StartTime = ShiftInfo_DT.Rows[0]["ShiftStartTime"].ToString();
                //string[] CurrentShiftTime = StartTime.Split(' ');
                string[] CurrentShiftHr = StartTime.Split(':');
                int currentsecend = Convert.ToInt16(CurrentShiftHr[0].ToString()) * 60 * 60;

                DateTime StartDate = new DateTime(), EndDate = new DateTime();
                StartDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + StartTime);
                EndDate = StartDate.AddDays(1);

                int currentseconds = Convert.ToInt32(TimeSpan.Parse(DateTime.Now.ToShortTimeString()).TotalSeconds);
                if (currentseconds > 0 && currentseconds < currentsecend)
                {
                    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + StartTime);
                    StartDate = EndDate.AddDays(-1);
                }

                //DateTime StartDate = new DateTime(), EndDate = new DateTime();
                //StartDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "08:00:00");
                //EndDate = StartDate.AddDays(1);


                //int currentseconds = Convert.ToInt32(TimeSpan.Parse(DateTime.Now.ToShortTimeString()).TotalSeconds);
                //if (currentseconds > 0 && currentseconds < 28800)
                //{
                //    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "08:00:00");
                //    StartDate = EndDate.AddDays(-1);
                //}


                //log.Error("HomeViewModel DoWork() PreviousDowntimeShiftSummary StartDate=" + StartDate + " EndDate" + EndDate + "" + "No");
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("PreviousDowntimeShiftSummary", StartDate, EndDate);
                if (_LocalData.HasError())
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = _LocalData.Message;
                    _FinalTranslationResponse.Status = ResponseType.E;
                    _FinalTranslationResponse.ErrorLevel = ErrorLevel.T;
                    return _FinalTranslationResponse;
                }
                //Translation
                if (_LocalData.Response != null)
                {
                    if (_LocalData.Response.Rows.Count > 0)
                    {
                        var _Query = (from DataRow dr in _LocalData.Response.Rows
                                      select new HomeEntity()
                                      {
                                          ShiftNo = (dr["ShiftNo"].ToString().Trim()),
                                          DownTime = Convert.ToInt32((dr["DownTime"].ToString().Trim())),
                                          SemiDownTime = Convert.ToInt32((dr["SemiDownTime"].ToString().Trim())),
                                          ManualDownTime = Convert.ToInt32((dr["ManualDownTime"].ToString().Trim())),
                                          MaintenanceDownTime = Convert.ToInt32((dr["MaintenanceDownTime"].ToString().Trim())),
                                          CompleteDownTime = Convert.ToInt32((dr["CompleteDownTime"].ToString().Trim())),
                                      });


                        //PartName = (dr["PartNumber"].ToString()),
                        //                  PartNameCount = Convert.ToInt32((dr["PartNameCount"].ToString())),



                        _FinalTranslationResponse.Response = _Query.ToList<HomeEntity>();
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
                else
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = "Data Received Successfuly";
                    _FinalTranslationResponse.Status = ResponseType.S;
                }
            }
            catch (Exception Ex)
            {
                _FinalTranslationResponse.Response = null;
                _FinalTranslationResponse.Message = Ex.Message;
                _FinalTranslationResponse.Status = ResponseType.E;
                _FinalTranslationResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalTranslationResponse;
        }
        public static ServiceResponse<IList> GetShiftMasterData(string para)
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.SelectAllRunningShiftMasterData(para);
                if (_LocalData.HasError())
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = _LocalData.Message;
                    _FinalTranslationResponse.Status = ResponseType.E;
                    _FinalTranslationResponse.ErrorLevel = ErrorLevel.T;
                    return _FinalTranslationResponse;
                }
                //Translation
                if (_LocalData.Response != null)
                {
                    if (_LocalData.Response.Rows.Count > 0)
                    {
                        var _Query = (from DataRow dr in _LocalData.Response.Rows
                                      select new ShiftMasterEntity()
                                      {
                                          ShiftNumber = (dr["ShiftNo"].ToString()),
                                          ModifiedDate = (dr["ModifiedDate"].ToString()),
                                          ShiftStartTime = (dr["ShiftStartTime"].ToString()),
                                          ShiftEndTime = (dr["ShiftEndTime"].ToString())
                                      });
                        //_FinalTranslationResponse.Response = _Query.ToList<NextLoadSettingsEntity>();
                        _FinalTranslationResponse.Response = _Query.ToList<ShiftMasterEntity>(); ;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
                else
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = "Data Received Successfuly";
                    _FinalTranslationResponse.Status = ResponseType.S;
                }
            }
            catch (Exception Ex)
            {
                _FinalTranslationResponse.Response = null;
                _FinalTranslationResponse.Message = Ex.Message;
                _FinalTranslationResponse.Status = ResponseType.E;
                _FinalTranslationResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalTranslationResponse;
        }

        public static ServiceResponse<IList> GetAlarmDataSummary()
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                DateTime StartDate = new DateTime(), EndDate = new DateTime();
                StartDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
                EndDate = StartDate.AddDays(1);
                //int currentseconds =(DateTime.Now.Hour * 60) + (DateTime.Now.Minute * 60) + (DateTime.Now.Second);
                int currentseconds = Convert.ToInt32(TimeSpan.Parse(DateTime.Now.ToShortTimeString()).TotalSeconds);
                if (currentseconds > 0 && currentseconds < 28800)
                {
                    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
                    StartDate = EndDate.AddDays(-1);
                }
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("AlarmGraph", StartDate, EndDate);
                if (_LocalData.HasError())
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = _LocalData.Message;
                    _FinalTranslationResponse.Status = ResponseType.E;
                    _FinalTranslationResponse.ErrorLevel = ErrorLevel.T;
                    return _FinalTranslationResponse;
                }
                //Translation
                if (_LocalData.Response != null)
                {
                    if (_LocalData.Response.Rows.Count > 0)
                    {
                        var _Query = (from DataRow dr in _LocalData.Response.Rows
                                      select new HomeEntity()
                                      {
                                          AlarmName = (dr["AlarmGroup"].ToString()),
                                          AlarmCount = Convert.ToInt16((dr["AlarmCount"].ToString())),
                                      });
                        _FinalTranslationResponse.Response = _Query.ToList<HomeEntity>();
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
                else
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = "Data Received Successfuly";
                    _FinalTranslationResponse.Status = ResponseType.S;
                }
            }
            catch (Exception Ex)
            {
                _FinalTranslationResponse.Response = null;
                _FinalTranslationResponse.Message = Ex.Message;
                _FinalTranslationResponse.Status = ResponseType.E;
                _FinalTranslationResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalTranslationResponse;
        }
        public static ServiceResponse<HomeEntity> GetAlarmDataTotalCount()
        {
            ServiceResponse<HomeEntity> _FinalTranslationResponse = new ServiceResponse<HomeEntity>();
            try
            {
                DateTime StartDate = new DateTime(), EndDate = new DateTime();
                StartDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
                EndDate = StartDate.AddDays(1);
                //int currentseconds = DateTime.Now.Hour * 60 * 60;
                int currentseconds = Convert.ToInt32(TimeSpan.Parse(DateTime.Now.ToShortTimeString()).TotalSeconds);
                if (currentseconds > 0 && currentseconds < 28800)
                {
                    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
                    StartDate = EndDate.AddDays(-1);
                }

                //DateTime currentdate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "00:00:00");
                //int result = TimeSpan.Compare(DateTime.Now.TimeOfDay, currentdate.TimeOfDay);
                //if (result >= 0)
                //{
                //    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "08:00:00");
                //    StartDate = EndDate.AddDays(-1);
                //}
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("AlarmTotalCount", StartDate, EndDate);
                if (_LocalData.HasError())
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = _LocalData.Message;
                    _FinalTranslationResponse.Status = ResponseType.E;
                    _FinalTranslationResponse.ErrorLevel = ErrorLevel.T;
                    return _FinalTranslationResponse;
                }
                //Translation
                if (_LocalData.Response != null)
                {
                    if (_LocalData.Response.Rows.Count > 0)
                    {
                        //var _Query = (from DataRow dr in _LocalData.Response.Rows
                        //              select new HomeEntity()
                        //              {
                        //                  AlarmCount = Convert.ToInt16((dr["AlarmCount"].ToString())),
                        //              });

                        HomeEntity _HomeAlarmCount = new HomeEntity();
                        _HomeAlarmCount.AlarmONCount = _LocalData.Response.Rows.Count;
                        int ackcount = 0;
                        foreach (DataRow dr in _LocalData.Response.Rows)
                        {
                            if ((dr["isACK"].ToString() == "1") || (dr["isACK"].ToString() == "True"))
                            {
                                ackcount++;
                            }
                        }
                        _HomeAlarmCount.AlarmNotACKCount = ackcount;

                        _FinalTranslationResponse.Response = _HomeAlarmCount;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
                else
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = "Data Received Successfuly";
                    _FinalTranslationResponse.Status = ResponseType.S;
                }
            }
            catch (Exception Ex)
            {
                _FinalTranslationResponse.Response = null;
                _FinalTranslationResponse.Message = Ex.Message;
                _FinalTranslationResponse.Status = ResponseType.E;
                _FinalTranslationResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalTranslationResponse;
        }
        public static ServiceResponse<HomeEntity> GetAvgCycleTime()
        {
            ServiceResponse<HomeEntity> _FinalTranslationResponse = new ServiceResponse<HomeEntity>();
            try
            {
                DateTime StartDate = new DateTime(), EndDate = new DateTime();
                StartDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
                EndDate = StartDate.AddDays(1);
                //int currentseconds = DateTime.Now.Hour * 60 * 60;
                int currentseconds = Convert.ToInt32(TimeSpan.Parse(DateTime.Now.ToShortTimeString()).TotalSeconds);
                if (currentseconds > 0 && currentseconds < 28800)
                {
                    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
                    StartDate = EndDate.AddDays(-1);
                }

                //DateTime currentdate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "00:00:00");
                //int result = TimeSpan.Compare(DateTime.Now.TimeOfDay, currentdate.TimeOfDay);
                //if (result >= 0)
                //{
                //    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "08:00:00");
                //    StartDate = EndDate.AddDays(-1);
                //}
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("AvgCycleTime", StartDate, EndDate);
                if (_LocalData.HasError())
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = _LocalData.Message;
                    _FinalTranslationResponse.Status = ResponseType.E;
                    _FinalTranslationResponse.ErrorLevel = ErrorLevel.T;
                    return _FinalTranslationResponse;
                }
                //Translation
                if (_LocalData.Response != null)
                {
                    if (_LocalData.Response.Rows.Count > 0)
                    {
                        HomeEntity _HomeCycletime = new HomeEntity();
                        _HomeCycletime.AvgCycleTime = _LocalData.Response.Rows[0]["AvgCycleTime"].ToString();

                        _FinalTranslationResponse.Response = _HomeCycletime;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
                else
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = "Data Received Successfuly";
                    _FinalTranslationResponse.Status = ResponseType.S;
                }
            }
            catch (Exception Ex)
            {
                _FinalTranslationResponse.Response = null;
                _FinalTranslationResponse.Message = Ex.Message;
                _FinalTranslationResponse.Status = ResponseType.E;
                _FinalTranslationResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalTranslationResponse;
        }

        public static ServiceResponse<IList> GetTotalQuantity()
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                DateTime StartDate = new DateTime(), EndDate = new DateTime();
                StartDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
                EndDate = StartDate.AddDays(1);
                //int currentseconds = DateTime.Now.Hour * 60 * 60;
                int currentseconds = Convert.ToInt32(TimeSpan.Parse(DateTime.Now.ToShortTimeString()).TotalSeconds);
                if (currentseconds > 0 && currentseconds < 28800)
                {
                    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
                    StartDate = EndDate.AddDays(-1);
                }

                //DateTime currentdate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "00:00:00");
                //int result = TimeSpan.Compare(DateTime.Now.TimeOfDay, currentdate.TimeOfDay);
                //if (result >= 0)
                //{
                //    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "08:00:00");
                //    StartDate = EndDate.AddDays(-1);
                //}
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("TotalQuantity", StartDate, EndDate);
                if (_LocalData.HasError())
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = _LocalData.Message;
                    _FinalTranslationResponse.Status = ResponseType.E;
                    _FinalTranslationResponse.ErrorLevel = ErrorLevel.T;
                    return _FinalTranslationResponse;
                }
                //Translation
                if (_LocalData.Response != null)
                {
                    if (_LocalData.Response.Rows.Count > 0)
                    {
                        var _Query = (from DataRow dr in _LocalData.Response.Rows
                                      select new HomeEntity()
                                      {
                                          TotalQuantityCount = Convert.ToInt32((dr["TotalQuantity"].ToString())),
                                      });
                        _FinalTranslationResponse.Response = _Query.ToList<HomeEntity>();
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
                else
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = "Data Received Successfuly";
                    _FinalTranslationResponse.Status = ResponseType.S;
                }
            }
            catch (Exception Ex)
            {
                _FinalTranslationResponse.Response = null;
                _FinalTranslationResponse.Message = Ex.Message;
                _FinalTranslationResponse.Status = ResponseType.E;
                _FinalTranslationResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalTranslationResponse;
        }

        public static ServiceResponse<IList> GetTotalLoads()
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                DateTime StartDate = new DateTime(), EndDate = new DateTime();
                StartDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
                EndDate = StartDate.AddDays(1);
                //int currentseconds = DateTime.Now.Hour * 60 * 60;
                int currentseconds = Convert.ToInt32(TimeSpan.Parse(DateTime.Now.ToShortTimeString()).TotalSeconds);
                if (currentseconds > 0 && currentseconds < 28800)
                {
                    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
                    StartDate = EndDate.AddDays(-1);
                }
                //DateTime currentdate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "00:00:00");
                //int result = TimeSpan.Compare(DateTime.Now.TimeOfDay, currentdate.TimeOfDay);
                //if (result >= 0)
                //{
                //    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "08:00:00");
                //    StartDate = EndDate.AddDays(-1);
                //}
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("TotalLoads", StartDate, EndDate);
                if (_LocalData.HasError())
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = _LocalData.Message;
                    _FinalTranslationResponse.Status = ResponseType.E;
                    _FinalTranslationResponse.ErrorLevel = ErrorLevel.T;
                    return _FinalTranslationResponse;
                }
                //Translation
                if (_LocalData.Response != null)
                {
                    if (_LocalData.Response.Rows.Count > 0)
                    {
                        var _Query = (from DataRow dr in _LocalData.Response.Rows
                                      select new HomeEntity()
                                      {
                                          TotalLoadCount = Convert.ToInt16((dr["TotalLoads"].ToString())),
                                      });
                        _FinalTranslationResponse.Response = _Query.ToList<HomeEntity>();
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
                else
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = "Data Received Successfuly";
                    _FinalTranslationResponse.Status = ResponseType.S;
                }
            }
            catch (Exception Ex)
            {
                _FinalTranslationResponse.Response = null;
                _FinalTranslationResponse.Message = Ex.Message;
                _FinalTranslationResponse.Status = ResponseType.E;
                _FinalTranslationResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalTranslationResponse;
        }

        public static ServiceResponse<IList> GetShiftQuantity(int ShiftNumber)
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                DateTime StartDate = new DateTime(), EndDate = new DateTime();
                StartDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
                EndDate = StartDate.AddDays(1);
                //int currentseconds = DateTime.Now.Hour * 60 * 60;
                int currentseconds = Convert.ToInt32(TimeSpan.Parse(DateTime.Now.ToShortTimeString()).TotalSeconds);
                if (currentseconds > 0 && currentseconds < 28800)
                {
                    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
                    StartDate = EndDate.AddDays(-1);
                }
                //DateTime currentdate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "00:00:00");
                //int result = TimeSpan.Compare(DateTime.Now.TimeOfDay, currentdate.TimeOfDay);
                //if (result >= 0)
                //{
                //    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "08:00:00");
                //    StartDate = EndDate.AddDays(-1);
                //}
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphDataShiftWise("TotalQuantity_Shift", StartDate, EndDate, ShiftNumber);
                if (_LocalData.HasError())
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = _LocalData.Message;
                    _FinalTranslationResponse.Status = ResponseType.E;
                    _FinalTranslationResponse.ErrorLevel = ErrorLevel.T;
                    return _FinalTranslationResponse;
                }
                //Translation
                if (ShiftNumber == 1)
                {
                    if (_LocalData.Response != null)
                    {
                        if (_LocalData.Response.Rows.Count > 0)
                        {
                            var _Query = (from DataRow dr in _LocalData.Response.Rows
                                          select new HomeEntity()
                                          {
                                              Shift1QuantityCount = Convert.ToInt32((dr["TotalQuantity_Shift"].ToString())),
                                          });
                            _FinalTranslationResponse.Response = _Query.ToList<HomeEntity>();
                            _FinalTranslationResponse.Message = "Data Received Successfuly";
                            _FinalTranslationResponse.Status = ResponseType.S;
                        }
                        else
                        {
                            _FinalTranslationResponse.Response = null;
                            _FinalTranslationResponse.Message = "Data Received Successfuly";
                            _FinalTranslationResponse.Status = ResponseType.S;
                        }
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
                else if (ShiftNumber == 2)
                {
                    if (_LocalData.Response != null)
                    {
                        if (_LocalData.Response.Rows.Count > 0)
                        {
                            var _Query = (from DataRow dr in _LocalData.Response.Rows
                                          select new HomeEntity()
                                          {
                                              Shift2QuantityCount = Convert.ToInt32((dr["TotalQuantity_Shift"].ToString())),
                                          });
                            _FinalTranslationResponse.Response = _Query.ToList<HomeEntity>();
                            _FinalTranslationResponse.Message = "Data Received Successfuly";
                            _FinalTranslationResponse.Status = ResponseType.S;
                        }
                        else
                        {
                            _FinalTranslationResponse.Response = null;
                            _FinalTranslationResponse.Message = "Data Received Successfuly";
                            _FinalTranslationResponse.Status = ResponseType.S;
                        }
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
                else if (ShiftNumber == 3)
                {
                    if (_LocalData.Response != null)
                    {
                        if (_LocalData.Response.Rows.Count > 0)
                        {
                            var _Query = (from DataRow dr in _LocalData.Response.Rows
                                          select new HomeEntity()
                                          {
                                              Shift3QuantityCount = Convert.ToInt32((dr["TotalQuantity_Shift"].ToString())),
                                          });
                            _FinalTranslationResponse.Response = _Query.ToList<HomeEntity>();
                            _FinalTranslationResponse.Message = "Data Received Successfuly";
                            _FinalTranslationResponse.Status = ResponseType.S;
                        }
                        else
                        {
                            _FinalTranslationResponse.Response = null;
                            _FinalTranslationResponse.Message = "Data Received Successfuly";
                            _FinalTranslationResponse.Status = ResponseType.S;
                        }
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
            }
            catch (Exception Ex)
            {
                _FinalTranslationResponse.Response = null;
                _FinalTranslationResponse.Message = Ex.Message;
                _FinalTranslationResponse.Status = ResponseType.E;
                _FinalTranslationResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalTranslationResponse;
        }

        public static ServiceResponse<IList> GetShiftLoadCount(int ShiftNumber)
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                DateTime StartDate = new DateTime(), EndDate = new DateTime();
                StartDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
                EndDate = StartDate.AddDays(1);
                //int currentseconds = DateTime.Now.Hour * 60 * 60;
                int currentseconds = Convert.ToInt32(TimeSpan.Parse(DateTime.Now.ToShortTimeString()).TotalSeconds);
                if (currentseconds > 0 && currentseconds < 28800)
                {
                    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
                    StartDate = EndDate.AddDays(-1);
                }

                //DateTime currentdate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "00:00:00");
                //int result = TimeSpan.Compare(DateTime.Now.TimeOfDay, currentdate.TimeOfDay);
                //if (result >= 0)
                //{
                //    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "08:00:00");
                //    StartDate = EndDate.AddDays(-1);
                //}
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphDataShiftWise("TotalLoads_Shift", StartDate, EndDate, ShiftNumber);
                if (_LocalData.HasError())
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = _LocalData.Message;
                    _FinalTranslationResponse.Status = ResponseType.E;
                    _FinalTranslationResponse.ErrorLevel = ErrorLevel.T;
                    return _FinalTranslationResponse;
                }
                //Translation
                if (ShiftNumber == 1)
                {
                    if (_LocalData.Response != null)
                    {
                        if (_LocalData.Response.Rows.Count > 0)
                        {
                            var _Query = (from DataRow dr in _LocalData.Response.Rows
                                          select new HomeEntity()
                                          {
                                              Shift1LoadCount = Convert.ToInt16((dr["TotalLoads_Shift"].ToString())),
                                          });
                            _FinalTranslationResponse.Response = _Query.ToList<HomeEntity>();
                            _FinalTranslationResponse.Message = "Data Received Successfuly";
                            _FinalTranslationResponse.Status = ResponseType.S;
                        }
                        else
                        {
                            _FinalTranslationResponse.Response = null;
                            _FinalTranslationResponse.Message = "Data Received Successfuly";
                            _FinalTranslationResponse.Status = ResponseType.S;
                        }
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
                else if (ShiftNumber == 2)
                {
                    if (_LocalData.Response != null)
                    {
                        if (_LocalData.Response.Rows.Count > 0)
                        {
                            var _Query = (from DataRow dr in _LocalData.Response.Rows
                                          select new HomeEntity()
                                          {
                                              Shift2LoadCount = Convert.ToInt16((dr["TotalLoads_Shift"].ToString())),
                                          });
                            _FinalTranslationResponse.Response = _Query.ToList<HomeEntity>();
                            _FinalTranslationResponse.Message = "Data Received Successfuly";
                            _FinalTranslationResponse.Status = ResponseType.S;
                        }
                        else
                        {
                            _FinalTranslationResponse.Response = null;
                            _FinalTranslationResponse.Message = "Data Received Successfuly";
                            _FinalTranslationResponse.Status = ResponseType.S;
                        }
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
                else if (ShiftNumber == 3)
                {
                    if (_LocalData.Response != null)
                    {
                        if (_LocalData.Response.Rows.Count > 0)
                        {
                            var _Query = (from DataRow dr in _LocalData.Response.Rows
                                          select new HomeEntity()
                                          {
                                              Shift3LoadCount = Convert.ToInt16((dr["TotalLoads_Shift"].ToString())),
                                          });
                            _FinalTranslationResponse.Response = _Query.ToList<HomeEntity>();
                            _FinalTranslationResponse.Message = "Data Received Successfuly";
                            _FinalTranslationResponse.Status = ResponseType.S;
                        }
                        else
                        {
                            _FinalTranslationResponse.Response = null;
                            _FinalTranslationResponse.Message = "Data Received Successfuly";
                            _FinalTranslationResponse.Status = ResponseType.S;
                        }
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
            }
            catch (Exception Ex)
            {
                _FinalTranslationResponse.Response = null;
                _FinalTranslationResponse.Message = Ex.Message;
                _FinalTranslationResponse.Status = ResponseType.E;
                _FinalTranslationResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalTranslationResponse;
        }
        public static ServiceResponse<IList> GetPartNumberDataSummary()
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                DateTime StartDate = new DateTime(), EndDate = new DateTime();
                StartDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
                EndDate = StartDate.AddDays(1);
                //int currentseconds = DateTime.Now.Hour * 60 * 60;
                int currentseconds = Convert.ToInt32(TimeSpan.Parse(DateTime.Now.ToShortTimeString()).TotalSeconds);
                if (currentseconds > 0 && currentseconds < 28800)
                {
                    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
                    StartDate = EndDate.AddDays(-1);
                }

                //DateTime currentdate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "00:00:00");
                //int result = TimeSpan.Compare(DateTime.Now.TimeOfDay, currentdate.TimeOfDay);
                //if (result >= 0)
                //{
                //    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "08:00:00");
                //    StartDate = EndDate.AddDays(-1);
                //}
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("PartNumberSummary", StartDate, EndDate);
                if (_LocalData.HasError())
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = _LocalData.Message;
                    _FinalTranslationResponse.Status = ResponseType.E;
                    _FinalTranslationResponse.ErrorLevel = ErrorLevel.T;
                    return _FinalTranslationResponse;
                }
                //Translation
                if (_LocalData.Response != null)
                {
                    if (_LocalData.Response.Rows.Count > 0)
                    {
                        var _Query = (from DataRow dr in _LocalData.Response.Rows
                                      select new HomeEntity()
                                      {
                                          PartName = (dr["PartNumber"].ToString()),
                                          PartNameCount = Convert.ToInt32((dr["PartNameCount"].ToString())),
                                      });
                        _FinalTranslationResponse.Response = _Query.ToList<HomeEntity>();
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
                else
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = "Data Received Successfuly";
                    _FinalTranslationResponse.Status = ResponseType.S;
                }
            }
            catch (Exception Ex)
            {
                _FinalTranslationResponse.Response = null;
                _FinalTranslationResponse.Message = Ex.Message;
                _FinalTranslationResponse.Status = ResponseType.E;
                _FinalTranslationResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalTranslationResponse;
        }
        public static ServiceResponse<IList> GetChemicalConsumptionDataSummary()
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                DateTime StartDate = new DateTime(), EndDate = new DateTime();
                StartDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
                EndDate = StartDate.AddDays(1);
                //int currentseconds = DateTime.Now.Hour * 60 * 60;
                int currentseconds = Convert.ToInt32(TimeSpan.Parse(DateTime.Now.ToShortTimeString()).TotalSeconds);
                if (currentseconds > 0 && currentseconds < 28800)
                {
                    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
                    StartDate = EndDate.AddDays(-1);
                }

                //DateTime currentdate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "00:00:00");
                //int result = TimeSpan.Compare(EndDate.TimeOfDay, currentdate.TimeOfDay);
                //if (result >= 0)
                //{
                //    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "08:00:00");
                //    StartDate = EndDate.AddDays(-1);
                //}

                ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork() ChemicalConsumptionSummary() startDate:"+StartDate+" EndDate:"+EndDate, DateTime.Now.ToString(), "", "No", true);

                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("ChemicalConsumptionSummary", StartDate, EndDate);
                if (_LocalData.HasError())
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = _LocalData.Message;
                    _FinalTranslationResponse.Status = ResponseType.E;
                    _FinalTranslationResponse.ErrorLevel = ErrorLevel.T;
                    return _FinalTranslationResponse;
                }
                //Translation
                if (_LocalData.Response != null)
                {
                    if (_LocalData.Response.Rows.Count > 0)
                    {
                        var _Query = (from DataRow dr in _LocalData.Response.Rows
                                      select new HomeEntity()
                                      {
                                          ChemicalName = (dr["ChemicalName"].ToString()),
                                          ChemicalConsumptionCount = Convert.ToInt32((dr["ChemicalConsumptionCount"].ToString())),
                                      });
                        _FinalTranslationResponse.Response = _Query.ToList<HomeEntity>();
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
                else
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = "Data Received Successfuly";
                    _FinalTranslationResponse.Status = ResponseType.S;
                }
            }
            catch (Exception Ex)
            {
                _FinalTranslationResponse.Response = null;
                _FinalTranslationResponse.Message = Ex.Message;
                _FinalTranslationResponse.Status = ResponseType.E;
                _FinalTranslationResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalTranslationResponse;
        }
        public static ServiceResponse<IList> GetCurrentConsumptionDataSummary()
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                DateTime StartDate = new DateTime(), EndDate = new DateTime();
                StartDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
                EndDate = StartDate.AddDays(1);
                //int currentseconds = DateTime.Now.Hour * 60 * 60;
                int currentseconds = Convert.ToInt32(TimeSpan.Parse(DateTime.Now.ToShortTimeString()).TotalSeconds);
                if (currentseconds > 0 && currentseconds < 28800)
                {
                    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
                    StartDate = EndDate.AddDays(-1);
                }

                //DateTime currentdate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "00:00:00");
                //int result = TimeSpan.Compare(EndDate.TimeOfDay, currentdate.TimeOfDay);
                //if (result >= 0)
                //{
                //    EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "08:00:00");
                //    StartDate = EndDate.AddDays(-1);
                //}

                
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("CurrentConsumptionSummary", StartDate, EndDate);
                if (_LocalData.HasError())
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = _LocalData.Message;
                    _FinalTranslationResponse.Status = ResponseType.E;
                    _FinalTranslationResponse.ErrorLevel = ErrorLevel.T;
                    return _FinalTranslationResponse;
                }
                //Translation
                if (_LocalData.Response != null)
                {
                    if (_LocalData.Response.Rows.Count > 0)
                    {
                        var _Query = (from DataRow dr in _LocalData.Response.Rows
                                      select new HomeEntity()
                                      {
                                          RectifierStnName = (dr["StationName"].ToString()),
                                          CurrentConsumptionCount = Convert.ToInt32((dr["CurrentConsumption"].ToString())),
                                      });
                        _FinalTranslationResponse.Response = _Query.ToList<HomeEntity>();
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
                else
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = "Data Received Successfuly";
                    _FinalTranslationResponse.Status = ResponseType.S;
                }
            }
            catch (Exception Ex)
            {
                _FinalTranslationResponse.Response = null;
                _FinalTranslationResponse.Message = Ex.Message;
                _FinalTranslationResponse.Status = ResponseType.E;
                _FinalTranslationResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalTranslationResponse;
        }
        
        public static ServiceResponse<HomeEntity> GetHDEPrecess()
        {
            ServiceResponse<HomeEntity> _FinalTranslationResponse = new ServiceResponse<HomeEntity>();
            try
            {
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.getHDEProcess();
                if (_LocalData.HasError())
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = _LocalData.Message;
                    _FinalTranslationResponse.Status = ResponseType.E;
                    _FinalTranslationResponse.ErrorLevel = ErrorLevel.T;
                    return _FinalTranslationResponse;
                }
                //Translation
                if (_LocalData.Response != null)
                {
                    if (_LocalData.Response.Rows.Count > 0)
                    {
                        HomeEntity _getHDEProcess = new HomeEntity();
                        _getHDEProcess.HDEProcess = _LocalData.Response.Rows[0]["HDEProcess"].ToString();

                        _FinalTranslationResponse.Response = _getHDEProcess;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
                else
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = "Data Received Successfuly";
                    _FinalTranslationResponse.Status = ResponseType.S;
                }
            }
            catch (Exception Ex)
            {
                _FinalTranslationResponse.Response = null;
                _FinalTranslationResponse.Message = Ex.Message;
                _FinalTranslationResponse.Status = ResponseType.E;
                _FinalTranslationResponse.ErrorLevel = ErrorLevel.A;
            }
            
            return _FinalTranslationResponse;
        }
        #endregion
    }
}
