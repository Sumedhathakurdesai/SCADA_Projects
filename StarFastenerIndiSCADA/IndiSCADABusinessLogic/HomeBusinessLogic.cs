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

namespace IndiSCADABusinessLogic
{
    public static class HomeBusinessLogic
    {
        #region"Declaration"
        static ServiceResponse<IList> _ShiftMasterData = IndiSCADATranslation.HomeViewTranslation.GetShiftMasterData("AllRunningShiftData");
        #endregion

        #region Public/private methods
        public static ObservableCollection<TankDetailsEntity> GetLiveZincPlatingSummary()
        {
            //Get alarm summary
            ObservableCollection<TankDetailsEntity> _result = new ObservableCollection<TankDetailsEntity>();
            try
            {
                ServiceResponse<IList> _TranslationOP = IndiSCADATranslation.HomeViewTranslation.GetLiveZincPlatingSummary();
                if (_TranslationOP.Response != null)
                {
                    if (_TranslationOP.Status == ResponseType.S)
                    {
                        IList<TankDetailsEntity> _conversionList = (IList<TankDetailsEntity>)(_TranslationOP.Response);
                        _result = new ObservableCollection<TankDetailsEntity>(_conversionList);
                    }
                }
            }
            catch (Exception ex)
            {
                //log.Error("HomeBusinessLogic GetLiveZincPlatingSummary()" + ex.Message + IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }

        public static ObservableCollection<HomeEntity> GetShiftWisePartArea(int ShiftNumber, bool CompletedLoadOnly)
        {
            //Get alarm summary
            ObservableCollection<HomeEntity> _result = new ObservableCollection<HomeEntity>();
            try
            {
                ServiceResponse<IList> _TranslationOP = IndiSCADATranslation.HomeViewTranslation.GetPartArea(ShiftNumber, CompletedLoadOnly);
                if (_TranslationOP.Response != null)
                {
                    if (_TranslationOP.Status == ResponseType.S)
                    {
                        IList<HomeEntity> _conversionList = (IList<HomeEntity>)(_TranslationOP.Response);
                        _result = new ObservableCollection<HomeEntity>(_conversionList);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork() GetShiftWisePartArea()" + ex.Message, DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                // log.Error("HomeBusinessLogic GetLoadCount()" + ex.Message + IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }

        public static DataTable GetSummaryData(string SummaryForGraph)
        {
            DataTable SummaryInfo = new DataTable();
            ServiceResponse<DataTable> _LocalData = new ServiceResponse<DataTable>();
            try
            {
                //Get alarm summary 
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

                if (SummaryForGraph == "Alarm Summary")
                {
                     _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("AlarmSummaryDetails", StartDate, EndDate);
                }
                else if (SummaryForGraph == "Chemical Summary")
                {
                     _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("ChemicalConsumptionDetails", StartDate, EndDate);
                }
                else if (SummaryForGraph == "Rectifier Summary")
                {
                     _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("CurrentConsumptionSummaryDetails", StartDate, EndDate);
                }
                else if (SummaryForGraph == "Part Summary")
                {
                    _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("PartNumberSummaryDetails", StartDate, EndDate);
                }
                else if (SummaryForGraph == "Live Alarm Summary")
                {
                    _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("AlarmTotalCountDetails", StartDate, EndDate);
                }
                else if (SummaryForGraph == "Acknoledge Alarm Summary")
                {
                    _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("ACKAlarmSummaryDetails", StartDate, EndDate);
                }
                else if (SummaryForGraph == "Wagon1 Alarm Summary")
                {
                    _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("W1AlarmSummaryDetails", StartDate, EndDate);
                }
                else if (SummaryForGraph == "Wagon2 Alarm Summary")
                {
                    _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("W2AlarmSummaryDetails", StartDate, EndDate);
                }
                else if (SummaryForGraph == "Wagon3 Alarm Summary")
                {
                    _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("W3AlarmSummaryDetails", StartDate, EndDate);
                }
                else if (SummaryForGraph == "Wagon4 Alarm Summary")
                {
                    _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("W4AlarmSummaryDetails", StartDate, EndDate);
                }
                else if (SummaryForGraph == "Wagon5 Alarm Summary")
                {
                    _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("W5AlarmSummaryDetails", StartDate, EndDate);
                }
                else if (SummaryForGraph == "Wagon6 Alarm Summary")
                {
                    _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("W6AlarmSummaryDetails", StartDate, EndDate);
                }
                else if (SummaryForGraph == "Wagon7 Alarm Summary")
                {
                    _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("W7AlarmSummaryDetails", StartDate, EndDate);
                }
                else if (SummaryForGraph == "Wagon8 Alarm Summary")
                {
                    _LocalData = IndiSCADADataAccess.DataAccessSelect.HomeViewGraphData("W8AlarmSummaryDetails", StartDate, EndDate);
                }


                SummaryInfo = _LocalData.Response;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetSummaryData()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return SummaryInfo;
        }
        public static int CheckShiftSettingData()
        {
            try
            {
                ServiceResponse<IList> _ShiftNewData = IndiSCADATranslation.HomeViewTranslation.GetShiftMasterData("NewShiftData");
                if (_ShiftNewData.Response != null)
                {
                    IList<ShiftMasterEntity> lstShifts = (IList<ShiftMasterEntity>)(_ShiftNewData.Response);
                    return lstShifts.Count;
                }
                return 0;
            }
            catch { return 0; }
        }

        public static string GetActualCycleTime()
        {
            string Value = "";
            try
            {
                string[] _result = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("CYCLETIMESETCYCLETIME");//Actualcycle time
                Value = _result[2];
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetActualCycleTime()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return Value;
        }
        public static string GetSetCycleTime()
        {
            string Value = "";
            try
            {
                string[] _result = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("CYCLETIMESETCYCLETIME");//SetCycleTime
                Value = _result[0];
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetSetCycleTime()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return Value;
        }

        public static string GetHDEProcess()
        {
             string value = "";
            HomeEntity _result = new HomeEntity();
            try
            {
                ServiceResponse<HomeEntity> _TranslationOP = IndiSCADATranslation.HomeViewTranslation.GetHDEPrecess();
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetHDEProcess1()", DateTime.Now.ToString(), _TranslationOP.Response.HDEProcess.ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                if (_TranslationOP.Response != null)
                {
                    if (_TranslationOP.Status == ResponseType.S)
                    {
                        //  IList<HomeEntity> _conversionList = (IList<HomeEntity>)(_TranslationOP.Response);
                        _result = _TranslationOP.Response;
                        value = _result.HDEProcess;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetHDEProcess()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return "HDE Process : "+value;
        }
        
        public static string GetLastCycleTime()
        {
            string Value = "";
            try
            {
                string[] _result = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("CYCLETIMESETCYCLETIME");//LastCycleTime
                Value = _result[1];
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetSetCycleTime()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return Value;
        }

        public static string GetAvgCycleTime()
        {
            //Get avg cycle time
            string value = "";
            HomeEntity _result = new HomeEntity();
            try
            {
                ServiceResponse<HomeEntity> _TranslationOP = IndiSCADATranslation.HomeViewTranslation.GetAvgCycleTime();
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetAlarmTotalCount()", DateTime.Now.ToString(), _TranslationOP.Response.AvgCycleTime.ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                if (_TranslationOP.Response != null)
                {
                    if (_TranslationOP.Status == ResponseType.S)
                    {
                        //  IList<HomeEntity> _conversionList = (IList<HomeEntity>)(_TranslationOP.Response);
                        _result = _TranslationOP.Response;
                        value = _result.AvgCycleTime;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetAlarmTotalCount()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return value;
        }
        public static string GetShiftNumber()
        {
            string shiftnumber = "";
            TimeSpan currenttime = DateTime.Now.TimeOfDay;

            #region "check for next day to change shift values"
            try
            {
                if (TagList.IsShiftSettingChanged == true)
                {
                    // Get New shift data
                    ServiceResponse<IList> _ShiftNewData = IndiSCADATranslation.HomeViewTranslation.GetShiftMasterData("NewShiftData");
                    if (_ShiftNewData.Response != null)
                    {
                        IList<ShiftMasterEntity> lstShifts = (IList<ShiftMasterEntity>)(_ShiftNewData.Response);
                        DateTime Shiftmodifieddate = new DateTime();

                        if (lstShifts.Count > 0)
                        {
                            foreach (var item in lstShifts)
                            {
                                if (item.ShiftNumber.Trim() == "1")
                                {
                                    DateTime date1 = Convert.ToDateTime(item.ModifiedDate);
                                    string firstshifttime = lstShifts[0].ShiftStartTime;
                                    string date2 = date1.AddDays(1).ToShortDateString() + " " + firstshifttime;
                                    Shiftmodifieddate = Convert.ToDateTime(date2);
                                    break;
                                }
                            }
                        }
                        if (Shiftmodifieddate < DateTime.Now)
                        {
                            // delete the shift old shift master records
                            try
                            {
                                ServiceResponse<int> _result = IndiSCADADataAccess.DataAccessDelete.DeleteOldShiftMasterData();
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("ConfigurationLogic AddNewShiftTiming ()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            // update the new entry as active
                            try
                            {
                                ServiceResponse<int> _result = IndiSCADADataAccess.DataAccessUpdate.UpdateShiftNewData();
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("ConfigurationLogic AddNewShiftTiming ()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            // reset shift master data

                            _ShiftMasterData = IndiSCADATranslation.HomeViewTranslation.GetShiftMasterData("AllRunningShiftData");
                            TagList.IsShiftSettingChanged = false;

                        }
                    }
                }



            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetShiftNumber() Check Validity", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }

            #endregion

            #region "get shift number"
            try
            {



                int index = 0;
                if (_ShiftMasterData.Response != null)
                {
                    IList<ShiftMasterEntity> lstShifts = (IList<ShiftMasterEntity>)(_ShiftMasterData.Response);
                    foreach (var item in lstShifts)
                    {

                        ShiftMasterEntity _SEntity = new ShiftMasterEntity();
                        _SEntity.ShiftStartTime = item.ShiftStartTime;
                        _SEntity.ShiftEndTime = item.ShiftEndTime;
                        _SEntity.ShiftNumber = item.ShiftNumber;
                        TimeSpan starttime = TimeSpan.Parse(_SEntity.ShiftStartTime);
                        TimeSpan endtime = TimeSpan.Parse(_SEntity.ShiftEndTime);
                        if ((currenttime >= starttime) && (currenttime < endtime))
                        {
                            shiftnumber = _SEntity.ShiftNumber;
                            break;
                        }
                    }
                }

            }

            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetShiftNumber()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            #endregion

            return shiftnumber;
        }
        public static ObservableCollection<HomeEntity> GetAlarmSummary()
        {
            //Get alarm summary
            ObservableCollection<HomeEntity> _result = new ObservableCollection<HomeEntity>();
            try
            {
                 ServiceResponse<IList> _TranslationOP = IndiSCADATranslation.HomeViewTranslation.GetAlarmDataSummary();
                if (_TranslationOP.Response != null)
                {
                    if (_TranslationOP.Status  == ResponseType.S)
                    {
                        IList<HomeEntity> _conversionList = (IList<HomeEntity>)(_TranslationOP.Response); 
                        _result = new ObservableCollection<HomeEntity>(_conversionList);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetAlarmSummary()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }
        public static string GetWagonStatus(int wagonNo)
        {
            try
            {
                string WagonStatus = " ";

                //Get Wagon AM
                string[] WagonAutoMan =DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("WagonAutoMan"); // { "1", "1", "0", "0", "0", "0", "1", "1" }; 

                //ErrorLogger.LogError.ErrorLog("OverviewLogic" + " GetWagonStatus()"+ WagonAutoMan[0]+ " " + WagonAutoMan[1] + " " + WagonAutoMan[2] + " " + WagonAutoMan[3] + " " + WagonAutoMan[4] + " " + WagonAutoMan[5] , DateTime.Now.ToString(), "", null, true);

                if (wagonNo == 1)
                {
                    WagonStatus = WagonAutoMan[0];
                }
                if (wagonNo == 2)
                {
                    WagonStatus = WagonAutoMan[1];
                }
                if (wagonNo == 3)
                {
                    WagonStatus = WagonAutoMan[2];
                }
                if (wagonNo == 4)
                {
                    WagonStatus = WagonAutoMan[3];
                }
                if (wagonNo == 5)
                {
                    WagonStatus = WagonAutoMan[4];
                }
                if (wagonNo == 6)
                {
                    WagonStatus = WagonAutoMan[5];
                }
                if (wagonNo == 7)
                {
                    WagonStatus = WagonAutoMan[6];
                }
                if (wagonNo == 8)
                {
                    WagonStatus = WagonAutoMan[7];
                }
                return WagonStatus;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OverviewLogic" + " GetWagonStatus()", DateTime.Now.ToString(), ex.Message, null, true);
                return null;
            }
        }
        public static string GetPlantStatus(string Parameter)
        {
            try
            {
                string PlantStatus = " ";
                //Get Wagon AM
                string[] SystemInPut =  DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("SystemInput");//{ "1", "1", "1", "1", "1" };
      
                if (Parameter == "AM")//System_AutoManual
                {
                    PlantStatus = SystemInPut[0];
                }
                if (Parameter == "Service")//Cycle_Start
                {
                    PlantStatus = SystemInPut[1];
                }
                if (Parameter == "CycleON")//Cycle Intialise
                {
                    PlantStatus = SystemInPut[1];
                }
                if (Parameter == "Reset")// Emergency Stop
                {
                    PlantStatus = SystemInPut[3];
                }
                if (Parameter == "PowerON")//Power On
                {
                    PlantStatus = SystemInPut[4];
                }
                return PlantStatus;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OverviewLogic" + " PlantStatus()", DateTime.Now.ToString(), ex.Message, null, true);
                return null;
            }
        }
        public static HomeEntity GetAlarmTotalCount()
        {
            //Get alarm summary
            HomeEntity _result = new HomeEntity();
            try
            {
                ServiceResponse<HomeEntity> _TranslationOP = IndiSCADATranslation.HomeViewTranslation.GetAlarmDataTotalCount();
              //  ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetAlarmTotalCount()", DateTime.Now.ToString(), _TranslationOP.Response.AlarmONCount.ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                if (_TranslationOP.Response != null)
                {
                    if (_TranslationOP.Status == ResponseType.S)
                    {
                      //  IList<HomeEntity> _conversionList = (IList<HomeEntity>)(_TranslationOP.Response);
                        _result = _TranslationOP.Response;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetAlarmTotalCount()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }
        //public static ObservableCollection<HomeEntity> GetLoadUnderProcess()
        //{
        //    //Get alarm summary
        //    ObservableCollection<HomeEntity> _result = new ObservableCollection<HomeEntity>();
        //    try
        //    {
        //        ServiceResponse<IList> _TranslationOP = IndiSCADATranslation.HomeViewTranslation.GetAlarmDataTotalCount();
        //        if (_TranslationOP.Response != null)
        //        {
        //            if (_TranslationOP.Status == ResponseType.S)
        //            {
        //                IList<HomeEntity> _conversionList = (IList<HomeEntity>)(_TranslationOP.Response);
        //                _result = new ObservableCollection<HomeEntity>(_conversionList);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetAlarmTotalCount()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
        //    }
        //    return _result;
        //}
        public static ObservableCollection<HomeEntity> GetTotalQuantity()
        {
            //Get alarm summary
            ObservableCollection<HomeEntity> _result = new ObservableCollection<HomeEntity>();
            try
            {
                ServiceResponse<IList> _TranslationOP = IndiSCADATranslation.HomeViewTranslation.GetTotalQuantity();
                if (_TranslationOP.Response != null)
                {
                    if (_TranslationOP.Status == ResponseType.S)
                    {
                        IList<HomeEntity> _conversionList = (IList<HomeEntity>)(_TranslationOP.Response);
                        _result = new ObservableCollection<HomeEntity>(_conversionList);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetTotalQuantity()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }
        public static ObservableCollection<HomeEntity> GetTotalLoads()
        {
            //Get alarm summary
            ObservableCollection<HomeEntity> _result = new ObservableCollection<HomeEntity>();
            try
            {
                ServiceResponse<IList> _TranslationOP = IndiSCADATranslation.HomeViewTranslation.GetTotalLoads();
                if (_TranslationOP.Response != null)
                {
                    if (_TranslationOP.Status == ResponseType.S)
                    {
                        IList<HomeEntity> _conversionList = (IList<HomeEntity>)(_TranslationOP.Response);
                        _result = new ObservableCollection<HomeEntity>(_conversionList);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetTotalLoads()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }
        public static ObservableCollection<HomeEntity> GetShiftQuantity(int ShiftNumber)
        {
            //Get alarm summary
            ObservableCollection<HomeEntity> _result = new ObservableCollection<HomeEntity>();
            try
            {
                ServiceResponse<IList> _TranslationOP = IndiSCADATranslation.HomeViewTranslation.GetShiftQuantity(ShiftNumber);
                if (_TranslationOP.Response != null)
                {
                    if (_TranslationOP.Status == ResponseType.S)
                    {
                        IList<HomeEntity> _conversionList = (IList<HomeEntity>)(_TranslationOP.Response);
                        _result = new ObservableCollection<HomeEntity>(_conversionList);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetShiftQuantity()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }

        public static ObservableCollection<HomeEntity> GetLoadCount(int ShiftNumber)
        {
            //Get alarm summary
            ObservableCollection<HomeEntity> _result = new ObservableCollection<HomeEntity>();
            try
            {
                ServiceResponse<IList> _TranslationOP = IndiSCADATranslation.HomeViewTranslation.GetShiftLoadCount(ShiftNumber);
                if (_TranslationOP.Response != null)
                {
                    if (_TranslationOP.Status == ResponseType.S)
                    {
                        IList<HomeEntity> _conversionList = (IList<HomeEntity>)(_TranslationOP.Response);
                        _result = new ObservableCollection<HomeEntity>(_conversionList);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetLoadCount()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }
        public static ObservableCollection<HomeEntity> GetPartNameSummary()
        {
            //Get alarm summary
            ObservableCollection<HomeEntity> _result = new ObservableCollection<HomeEntity>();
            try
            {
                ServiceResponse<IList> _TranslationOP = IndiSCADATranslation.HomeViewTranslation.GetPartNumberDataSummary();
                if (_TranslationOP.Response != null)
                {
                    if (_TranslationOP.Status == ResponseType.S)
                    {
                        IList<HomeEntity> _conversionList = (IList<HomeEntity>)(_TranslationOP.Response);
                        _result = new ObservableCollection<HomeEntity>(_conversionList);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetPartNameSummary()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }
        public static ObservableCollection<HomeEntity> GetChemicalConsumptionSummary()
        {
            //Get alarm summary
            ObservableCollection<HomeEntity> _result = new ObservableCollection<HomeEntity>();
            try
            { 
                ServiceResponse<IList> _TranslationOP = IndiSCADATranslation.HomeViewTranslation.GetChemicalConsumptionDataSummary();
              
                if (_TranslationOP.Response != null)
                {
                    if (_TranslationOP.Status == ResponseType.S)
                    {

                        IList<HomeEntity> _conversionList = (IList<HomeEntity>)(_TranslationOP.Response);
                        _result = new ObservableCollection<HomeEntity>(_conversionList);
                    }                   
                }           
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetChemicalConsumptionSummary()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }

            return _result;
        }

        public static ObservableCollection<HomeEntity> GetPartAreaSummary(bool CompletedLoadOnly)
        {
            //Get alarm summary
            ObservableCollection<HomeEntity> _result = new ObservableCollection<HomeEntity>();
            try
            {
                ServiceResponse<IList> _TranslationOP = IndiSCADATranslation.HomeViewTranslation.GetPartAreaDataSummary(CompletedLoadOnly);
              //  ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetPartAreaSummary()", DateTime.Now.ToString(), _TranslationOP.Message.ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                if (_TranslationOP.Response != null)
                {
                    if (_TranslationOP.Status == ResponseType.S)
                    {
                        IList<HomeEntity> _conversionList = (IList<HomeEntity>)(_TranslationOP.Response);
                        _result = new ObservableCollection<HomeEntity>(_conversionList);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetPartAreaSummary()", DateTime.Now.ToString(),ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                //log.Error("HomeBusinessLogic GetPartNameSummary()" + ex.Message + IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }

        public static ObservableCollection<HomeEntity> GetCurrentConsumptionSummary()
        {
            //Get alarm summary
            ObservableCollection<HomeEntity> _result = new ObservableCollection<HomeEntity>();
            try
            { 
                ServiceResponse<IList> _TranslationOP = IndiSCADATranslation.HomeViewTranslation.GetCurrentConsumptionDataSummary();

                if (_TranslationOP.Response != null)
                {
                    if (_TranslationOP.Status == ResponseType.S)
                    {

                        IList<HomeEntity> _conversionList = (IList<HomeEntity>)(_TranslationOP.Response);
                        _result = new ObservableCollection<HomeEntity>(_conversionList);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetCurrentConsumptionSummary()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }

            return _result;
        }
        public static string GetSelectedLanguage()
        {
            //Get alarm summary
            ObservableCollection<HomeEntity> _result = new ObservableCollection<HomeEntity>();
            try
            {
                ServiceResponse<DataTable> _LocalData = new ServiceResponse<DataTable>();
                _LocalData = IndiSCADADataAccess.DataAccessSelect.CurentLanguageData("GetLanguage");
                DataTable dt = _LocalData.Response;
                string SelectedLanguage = dt.Rows[0]["SelectedLanguage"].ToString();
                return SelectedLanguage;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetSelectedLanguage()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }

            return "";
        }
        public static bool UpdateSelectedLanguage(string Selectedlang)
        {
            //Get alarm summary
            ObservableCollection<HomeEntity> _result = new ObservableCollection<HomeEntity>();
            try
            {
                ServiceResponse<DataTable> _LocalData = new ServiceResponse<DataTable>();
                _LocalData = IndiSCADADataAccess.DataAccessSelect.UpdateCurentLanguage("UpdateLanguage", Selectedlang); 
                return true;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic UpdateSelectedLanguage()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return false;
        }

        #endregion

        #region
        public static ObservableCollection<HomeEntity> GetPreviousDowntimeShiftSummary()
        {
            //Get alarm summary
            ObservableCollection<HomeEntity> _result = new ObservableCollection<HomeEntity>();
            try
            {
                ServiceResponse<IList> _TranslationOP = IndiSCADATranslation.HomeViewTranslation.GetPreviousDowntimeShiftSummaryData();

                if (_TranslationOP.Response != null)
                {
                    if (_TranslationOP.Status == ResponseType.S)
                    {

                        IList<HomeEntity> _conversionList = (IList<HomeEntity>)(_TranslationOP.Response);
                        _result = new ObservableCollection<HomeEntity>(_conversionList);
                    }
                }
            }
            catch (Exception ex)
            {
                //log.Error("HomeBusinessLogic GetChemicalConsumptionSummary()" + ex.Message + IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }

            return _result;
        }
        #endregion
    }
}
