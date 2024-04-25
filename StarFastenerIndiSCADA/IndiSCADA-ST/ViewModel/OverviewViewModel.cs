using IndiSCADAEntity.Entity;
using IndiSCADAGlobalLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Configuration;
using System.Data;

namespace IndiSCADA_ST.ViewModel
{
    public class OverviewViewModel : BaseViewModel
    {
        #region"Declaration"
        DispatcherTimer DispatchTimerView = new DispatcherTimer();
        System.ComponentModel.BackgroundWorker _BackgroundWorkerView = new System.ComponentModel.BackgroundWorker();
        bool isSettingStartup = false;
        #endregion

        #region ICommand
        private readonly ICommand _Exit;//
        public ICommand Exit
        {
            get { return _Exit; }
        } 

        private readonly ICommand _ShowTooltipData;//
        public ICommand ShowTooltipData
        {
            get { return _ShowTooltipData; }
        }

        private readonly ICommand _TankClickCommand;
        public ICommand TankClickCommand
        {
            get { return _TankClickCommand; }
        }

        private readonly ICommand _ExitTankInfoPopupCommand;
        public ICommand ExitTankInfoPopupCommand
        {
            get { return _ExitTankInfoPopupCommand; }
        }
        #endregion

        #region"Destructor"
        ~OverviewViewModel()
        {
            try
            {
                DispatchTimerView.Stop();
            }
            catch { }
        }
        #endregion

        #region "Consrtuctor"
        public OverviewViewModel()
        {
            try
            {    

                try
                {
                    isSettingStartup = true;

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
                    //#SCP this will enable error log for now i kept it false after designing configuration i will modify this  DESKTOP-UI951JD\SQLEXPRESS
                    //#SCP calculate shift on load event renmaining for now i kept it 1 T420-PC\FTVIEWX64TAGDB
                    IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable = true;
                    IndiSCADAGlobalLibrary.AccessConfig.Shift = 1;
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError.ErrorLog("OverviewViewModel OverviewViewModel() StartDate", DateTime.Now.ToString(), ex.Message, ex.InnerException.Message, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
                try
                {
                    IndiSCADAGlobalLibrary.AccessConfig.GetConnectionString = ConfigurationManager.ConnectionStrings["GetConnectionString"].ConnectionString;

                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError.ErrorLog("OverviewViewModel OverviewViewModel() GetConnectionString", DateTime.Now.ToString(), ex.Message, ex.InnerException.Message, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
                try
                {
                    //Dispatcher timer start
                    _BackgroundWorkerView.DoWork += DoWork;
                    DispatchTimerView.Interval = TimeSpan.FromSeconds(1);
                    DispatchTimerView.Tick += DispatcherTickEvent;
                    DispatchTimerView.Start();
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError.ErrorLog("OverviewViewModel OverviewViewModel() DispatchTimerView", DateTime.Now.ToString(), ex.Message, ex.InnerException.Message, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
                //try
                //{
                //    DeviceCommunication.CommunicationWithPLC.StartPlcComminicationAndDataLog();
                //    DeviceCommunication.TrendDataLog.StartTrendDataLog();
                //    DeviceCommunication.AlarmAndEventDataLog.StartAlarmAndEventTracking();

                //}
                //catch (Exception ex)
                //{
                //    ErrorLogger.LogError.ErrorLog("OverviewViewModel OverviewViewModel() GetWagonStatus", DateTime.Now.ToString(), ex.Message, ex.InnerException.Message, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                //}
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel OverviewViewModel()", DateTime.Now.ToString(), ex.Message, ex.InnerException.Message, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }

            try
            {
                //_BackgroundWorkerView.DoWork += DoWork;
                //DispatchTimerView.Interval = TimeSpan.FromMilliseconds(50);
                //DispatchTimerView.Tick += DispatcherTickEvent;
                //DispatchTimerView.Start();

                _Exit = new RelayCommand(ExitButtonCommandClick);
                //_TankClickCommand = new RelayCommand(TankStationNoClick);
                _ExitTankInfoPopupCommand = new RelayCommand(ExitTankInfoPopupClick);
                OpenTankInfo = false;

                //ObservableCollection<OverViewEntity> _we = new ObservableCollection<OverViewEntity>();
                //ServiceResponse<IList> _result = IndiSCADABusinessLogic.OverviewLogic.SelectStationName();                
                //if (_result.Response != null)
                //{
                //    IList<OverViewEntity> _IListresult = (IList<OverViewEntity>)(_result.Response);
                //    _we = new ObservableCollection<OverViewEntity>(_IListresult);
                //}

                //StationsData = new ObservableCollection<StationDetails>();
                //StationsData.Add(new StationDetails()
                //{
                //    StationData = _we
                //});

                try
                {
                    ObservableCollection<OverViewEntity> _we = new ObservableCollection<OverViewEntity>();
                    ServiceResponse<IList> _result = IndiSCADABusinessLogic.OverviewLogic.SelectStationName(1);
                    if (_result.Response != null)
                    {
                        IList<OverViewEntity> _IListresult = (IList<OverViewEntity>)(_result.Response);
                        //_IListresult.Reverse();
                        _we = new ObservableCollection<OverViewEntity>(_IListresult.Where(x => x.LineNo == "1"));
                        //_we.Reverse();
                    }
                    StationsData1 = new ObservableCollection<StationDetails>();
                    StationsData1.Add(new StationDetails()
                    {
                        StationData1 = _we
                    });

                    ObservableCollection<OverViewEntity> _we2 = new ObservableCollection<OverViewEntity>();
                    ServiceResponse<IList> _result2 = IndiSCADABusinessLogic.OverviewLogic.SelectStationName(2);
                    if (_result2.Response != null)
                    {
                        IList<OverViewEntity> _IListresult = (IList<OverViewEntity>)(_result2.Response);
                        _we2 = new ObservableCollection<OverViewEntity>(_IListresult.Where(x => x.LineNo == "2"));
                    }
                    StationsData2 = new ObservableCollection<StationDetails>();
                    StationsData2.Add(new StationDetails()
                    {
                        StationData2 = _we2
                    });

                    //ObservableCollection<OverViewEntity> _we3 = new ObservableCollection<OverViewEntity>();
                    //ServiceResponse<IList> _result3 = IndiSCADABusinessLogic.OverviewLogic.SelectStationName(3);
                    //if (_result3.Response != null)
                    //{
                    //    IList<OverViewEntity> _IListresult = (IList<OverViewEntity>)(_result3.Response);
                    //    _we3 = new ObservableCollection<OverViewEntity>(_IListresult.Where(x => x.LineNo == "3"));
                    //}
                    //StationsData3 = new ObservableCollection<StationDetails>();
                    //StationsData3.Add(new StationDetails()
                    //{
                    //    StationData3 = _we3
                    //});

                    _ShowTooltipData = new RelayCommand(NetworkArchitectureMouseClick);
                   
                }
                catch (Exception ex) { ErrorLogger.LogError.ErrorLog("OverviewViewModel1()", DateTime.Now.ToString(), ex.Message, "No", true); }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel ()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        #endregion

        //#region "Consrtuctor"
        //public OverviewViewModel()
        //{
        //    try
        //    {
        //        try
        //        {
        //            DateTime StartDate = new DateTime(), EndDate = new DateTime();

        //            StartDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
        //            EndDate = StartDate.AddDays(1);
        //            //int currentseconds = DateTime.Now.Hour * 60 * 60;
        //            int currentseconds = Convert.ToInt32(TimeSpan.Parse(DateTime.Now.ToShortTimeString()).TotalSeconds);
        //            if (currentseconds > 0 && currentseconds < 28800)
        //            {
        //                EndDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "07:00:00");
        //                StartDate = EndDate.AddDays(-1);
        //            }
        //            //#SCP this will enable error log for now i kept it false after designing configuration i will modify this  DESKTOP-UI951JD\SQLEXPRESS
        //            //#SCP calculate shift on load event renmaining for now i kept it 1 T420-PC\FTVIEWX64TAGDB
        //            IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable = true;
        //            IndiSCADAGlobalLibrary.AccessConfig.Shift = 1;
        //        }
        //        catch (Exception ex)
        //        {
        //            ErrorLogger.LogError.ErrorLog("OverviewViewModel () constructor1", DateTime.Now.ToString(), ex.Message, "No", true);
        //        }
        //        try
        //        {
        //            IndiSCADAGlobalLibrary.AccessConfig.GetConnectionString = ConfigurationManager.ConnectionStrings["GetConnectionString"].ConnectionString;

        //        }
        //        catch (Exception ex)
        //        {
        //            ErrorLogger.LogError.ErrorLog("OverviewViewModel () constructor GetConnectionString", DateTime.Now.ToString(), ex.Message, "No", true);
        //        }
        //        try
        //        {
        //            //Dispatcher timer start
        //            //_BackgroundWorkerView.DoWork += DoWork;
        //            //DispatchTimerView.Interval = TimeSpan.FromSeconds(1);
        //            //DispatchTimerView.Tick += DispatcherTickEvent;
        //            //DispatchTimerView.Start();
        //        }
        //        catch (Exception ex)
        //        {
        //            ErrorLogger.LogError.ErrorLog("OverviewViewModel () constructor DoWork", DateTime.Now.ToString(), ex.Message, "No", true);
        //        }
        //        //try
        //        //{
        //        //    //DeviceCommunication.CommunicationWithPLC.StartPlcComminicationAndDataLog();
        //        //    //DeviceCommunication.TrendDataLog.StartTrendDataLog();
        //        //    //DeviceCommunication.AlarmAndEventDataLog.StartAlarmAndEventTracking();

        //        //}
        //        //catch (Exception ex)
        //        //{
        //        //  //  ErrorLogger.LogError.ErrorLog("HomeViewModel HomeViewModel() GetWagonStatus", DateTime.Now.ToString(), ex.Message, ex.InnerException.Message, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.LogError.ErrorLog("OverviewViewModel () constructor3", DateTime.Now.ToString(), ex.Message, "No", true);
        //    }

        //    try
        //    {
        //        _BackgroundWorkerView.DoWork += DoWork;
        //        DispatchTimerView.Interval = TimeSpan.FromMilliseconds(50);
        //        DispatchTimerView.Tick += DispatcherTickEvent;
        //        DispatchTimerView.Start();
        //        _Exit = new RelayCommand(ExitButtonCommandClick);

        //        //ObservableCollection<OverViewEntity> _we = new ObservableCollection<OverViewEntity>();
        //        //ServiceResponse<IList> _result = IndiSCADABusinessLogic.OverviewLogic.SelectStationName();                
        //        //if (_result.Response != null)
        //        //{
        //        //    IList<OverViewEntity> _IListresult = (IList<OverViewEntity>)(_result.Response);
        //        //    _we = new ObservableCollection<OverViewEntity>(_IListresult);
        //        //}

        //        //StationsData = new ObservableCollection<StationDetails>();
        //        //StationsData.Add(new StationDetails()
        //        //{
        //        //    StationData = _we
        //        //});

        //        try
        //        {
        //            ObservableCollection<OverViewEntity> _we = new ObservableCollection<OverViewEntity>();
        //            ServiceResponse<IList> _result = IndiSCADABusinessLogic.OverviewLogic.SelectStationName(1);
        //            if (_result.Response != null)
        //            {
        //                IList<OverViewEntity> _IListresult = (IList<OverViewEntity>)(_result.Response);
        //                //_IListresult.Reverse();
        //                _we = new ObservableCollection<OverViewEntity>(_IListresult.Where(x => x.LineNo == "1"));
        //                //_we.Reverse();
        //            }
        //            StationsData1 = new ObservableCollection<StationDetails>();
        //            StationsData1.Add(new StationDetails()
        //            {
        //                StationData1 = _we
        //            });

        //            ObservableCollection<OverViewEntity> _we2 = new ObservableCollection<OverViewEntity>();
        //            ServiceResponse<IList> _result2 = IndiSCADABusinessLogic.OverviewLogic.SelectStationName(2);
        //            if (_result2.Response != null)
        //            {
        //                IList<OverViewEntity> _IListresult = (IList<OverViewEntity>)(_result2.Response);
        //                _we2 = new ObservableCollection<OverViewEntity>(_IListresult.Where(x => x.LineNo == "2"));
        //            }
        //            StationsData2 = new ObservableCollection<StationDetails>();
        //            StationsData2.Add(new StationDetails()
        //            {
        //                StationData2 = _we2
        //            });

        //            //ObservableCollection<OverViewEntity> _we3 = new ObservableCollection<OverViewEntity>();
        //            //ServiceResponse<IList> _result3 = IndiSCADABusinessLogic.OverviewLogic.SelectStationName(3);
        //            //if (_result3.Response != null)
        //            //{
        //            //    IList<OverViewEntity> _IListresult = (IList<OverViewEntity>)(_result3.Response);
        //            //    _we3 = new ObservableCollection<OverViewEntity>(_IListresult.Where(x => x.LineNo == "3"));
        //            //}
        //            //StationsData3 = new ObservableCollection<StationDetails>();
        //            //StationsData3.Add(new StationDetails()
        //            //{
        //            //    StationData3 = _we3
        //            //});
        //        }
        //        catch (Exception ex) { ErrorLogger.LogError.ErrorLog("OverviewViewModel () constructor5", DateTime.Now.ToString(), ex.Message, "No", true); }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.LogError.ErrorLog("OverviewViewModel () constructor6", DateTime.Now.ToString(), ex.Message, "No", true);
        //    }
        //}
        //#endregion

        #region "public/private methods"

        #region overview popup
        private void ExitTankInfoPopupClick(object e)
        {
            try
            {
                OpenTankInfo = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OveerviewViewModel ExitTankInfoPopupClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        public void TankStationNoClick(string _StationIndex)
        {
            try
            {
                PopupLowTemp = ""; PopupHighTemp = ""; PopupActualTemp = ""; PopupLowpH = ""; PopupHighpH = ""; PopupActualpH = "";
                PopupSetCurrent = ""; PopupActualCurrent = ""; PopupStationNo = ""; PopupStationName = ""; PopupLoadNumber = ""; PopupPartName = "";
                if (_StationIndex != null)//_TemperatureIndex is  an index value of tag
                {
                    if (_StationIndex != null)//_TemperatureIndex is  an index value of tag
                    {
                        int index = Convert.ToInt16(_StationIndex.ToString()); //convert station no into index
                        try
                        {
                            ServiceResponse<DataTable> _StationList = IndiSCADABusinessLogic.OverviewLogic.GetStationNameIndex(index);
                            if (_StationList.Response != null)
                            {
                                DataTable _DTStationList = _StationList.Response;

                                try
                                {
                                    if (_DTStationList.Rows[0]["TemperatureValueIndex"].ToString() != "")
                                    {
                                        int iindex = Convert.ToInt16(_DTStationList.Rows[0]["TemperatureValueIndex"].ToString());
                                        PopupLowTemp = TemperatureIntputs[iindex].LowSP.ToString();
                                        PopupHighTemp = TemperatureIntputs[iindex].HighSP.ToString();
                                        PopupActualTemp = TemperatureIntputs[iindex].ActualTemperature.ToString();
                                    }
                                    else
                                    {
                                        PopupLowTemp = "-";
                                        PopupHighTemp = "-";
                                        PopupActualTemp = "-";
                                    }
                                }
                                catch (Exception ex) { }
                                try
                                {
                                    if (_DTStationList.Rows[0]["CurrentValueIndex"].ToString() != "")
                                    {
                                        int iindex = Convert.ToInt16(_DTStationList.Rows[0]["CurrentValueIndex"].ToString());
                                        PopupSetCurrent = RectifierIntputs[iindex].Calculated.ToString();
                                        PopupActualCurrent = RectifierIntputs[iindex].ActualCurrent.ToString();
                                    }
                                    else
                                    {
                                        PopupSetCurrent = "-";
                                        PopupActualCurrent = "-";
                                    }
                                }
                                catch (Exception ex) { }
                                try
                                {
                                    if (_DTStationList.Rows[0]["pHValueIndex"].ToString() != "")
                                    {
                                        int iindex = Convert.ToInt16(_DTStationList.Rows[0]["pHValueIndex"].ToString());
                                        PopupLowpH = pHIntputs[iindex].LowSP.ToString();
                                        PopupHighpH = pHIntputs[iindex].HighSP.ToString();
                                        PopupActualpH = pHIntputs[iindex].ActualpH.ToString();
                                    }
                                    else
                                    {
                                        PopupLowpH = "-";
                                        PopupHighpH = "-";
                                        PopupActualpH = "-";
                                    }
                                }
                                catch (Exception ex) { }
                            }
                        }
                        catch (Exception ex) { }
                         
                        int Stationindex = Convert.ToInt16(_StationIndex.ToString()) - 1;

                        ObservableCollection<TankDetailsEntity> TankDetailsIP = IndiSCADABusinessLogic.TankDetailsLogic.GetTanlDetails();

                        PopupStationNo = "Tank No: " + TankDetailsIP[Stationindex].StationNo.ToString();
                        PopupStationName = "Tank Name : " + TankDetailsIP[Stationindex].StationName.ToString();

                        PopupLoadNumber = TankDetailsIP[Stationindex].LoadNumber.ToString();
                        PopupPartName = TankDetailsIP[Stationindex].PartName.ToString();

                        //PopupActualTemp = TankDetailsIP[Stationindex].ActualTemperature.ToString();
                        //PopupActualpH = TankDetailsIP[Stationindex].pH.ToString();
                        //PopupActualCurrent = TankDetailsIP[Stationindex].ActualCurrent.ToString();

                    }
                }
                OpenTankInfo = true;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OveerviewViewModel TankClick()", DateTime.Now.ToString(), ex.Message, "No", true);
                OpenTankInfo = true;
            }
        }
        #endregion 




        private void NetworkArchitectureMouseClick(object _commandparameters)
        {
            try
            {
                ErrorLogger.LogError.ErrorLog("HomeViewModel OpenSummeryClick() commandparameters" + _commandparameters.ToString(), DateTime.Now.ToString(), "", "No", true);
                TooltipData = "";
                if (_commandparameters.ToString().Contains("Temperature"))
                {
                    #region   Temoerature
                    if (_commandparameters.ToString() == "Temperature 1")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[0].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[0].LowSP.ToString();
                        //string Values =  System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[0].ActualSP.ToString();

                        ErrorLogger.LogError.ErrorLog("HomeViewModel OpenSummeryClick() Temperature 1" + DisplayData, DateTime.Now.ToString(), "", "No", true);

                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[0].ActualTemperature.ToString();

                        ErrorLogger.LogError.ErrorLog("HomeViewModel OpenSummeryClick() Actual Temp " + TemperatureIntputs[0].ActualTemperature.ToString(), DateTime.Now.ToString(), "", "No", true);

                        //Values = Values + System.Environment.NewLine + "Set Temp : 1";
                        //Values = Values + System.Environment.NewLine + "Actual Temp : 4";

                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 2")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[1].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[1].LowSP.ToString();
                        //string Values = System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[1].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[1].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 3")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[2].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[2].LowSP.ToString();
                        //string Values = System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[2].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[2].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 4")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[3].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[3].LowSP.ToString();
                        //string Values = System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[3].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[3].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 5")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[4].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[4].LowSP.ToString();
                        //string Values = System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[4].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[4].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 6")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[5].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[5].LowSP.ToString();
                        //string Values = System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[5].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[5].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 7")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[6].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[6].LowSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[6].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[6].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 8")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[7].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[7].LowSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[7].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[7].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 9")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[8].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[8].LowSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[8].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[8].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 10")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[9].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[9].LowSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[9].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[9].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 11")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[10].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[10].LowSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[10].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[10].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 12")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[11].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[11].LowSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[11].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[11].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 13")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[12].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[12].LowSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[12].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[12].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 14")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[13].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[13].LowSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[13].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[13].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 15")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[14].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[14].LowSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[14].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[14].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 16")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[15].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[15].LowSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[15].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[15].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 17")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[16].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[16].LowSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[16].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[16].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 18")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[17].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[17].LowSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[17].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[17].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 19")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[18].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[18].LowSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[18].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[18].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 20")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[19].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[19].LowSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[19].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[19].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 21")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[20].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[20].LowSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[20].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[20].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 22")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[21].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[21].LowSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[21].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[21].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 23")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[22].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[22].LowSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[22].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[22].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 24")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[23].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[23].LowSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[23].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[23].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Temperature 25")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + TemperatureIntputs[24].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + TemperatureIntputs[24].LowSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Set Temp : " + TemperatureIntputs[24].ActualSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Temp : " + TemperatureIntputs[24].ActualTemperature.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    #endregion
                }
                if (_commandparameters.ToString().Contains("Rectifier"))
                {
                    #region recifier
                    if (_commandparameters.ToString() == "Rectifier 1")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + RectifierIntputs[0].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + RectifierIntputs[0].LowSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Current : " + RectifierIntputs[0].ActualCurrent.ToString();
                        Values = Values + System.Environment.NewLine + "Actual Voltage : " + RectifierIntputs[0].ActualVoltage.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Rectifier 2")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + RectifierIntputs[1].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + RectifierIntputs[1].LowSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Current : " + RectifierIntputs[1].ActualCurrent.ToString();
                        Values = Values + System.Environment.NewLine + "Actual Voltage : " + RectifierIntputs[1].ActualVoltage.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Rectifier 3")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + RectifierIntputs[2].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + RectifierIntputs[2].LowSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Current : " + RectifierIntputs[2].ActualCurrent.ToString();
                        Values = Values + System.Environment.NewLine + "Actual Voltage : " + RectifierIntputs[2].ActualVoltage.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Rectifier 4")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + RectifierIntputs[3].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + RectifierIntputs[3].LowSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Current : " + RectifierIntputs[3].ActualCurrent.ToString();
                        Values = Values + System.Environment.NewLine + "Actual Voltage : " + RectifierIntputs[3].ActualVoltage.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Rectifier 5")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + RectifierIntputs[4].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + RectifierIntputs[4].LowSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Current : " + RectifierIntputs[4].ActualCurrent.ToString();
                        Values = Values + System.Environment.NewLine + "Actual Voltage : " + RectifierIntputs[4].ActualVoltage.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Rectifier 6")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + RectifierIntputs[5].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + RectifierIntputs[5].LowSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Current : " + RectifierIntputs[5].ActualCurrent.ToString();
                        Values = Values + System.Environment.NewLine + "Actual Voltage : " + RectifierIntputs[5].ActualVoltage.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Rectifier 7")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + RectifierIntputs[6].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + RectifierIntputs[6].LowSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Current : " + RectifierIntputs[6].ActualCurrent.ToString();
                        Values = Values + System.Environment.NewLine + "Actual Voltage : " + RectifierIntputs[6].ActualVoltage.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Rectifier 8")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + RectifierIntputs[7].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + RectifierIntputs[7].LowSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Current : " + RectifierIntputs[7].ActualCurrent.ToString();
                        Values = Values + System.Environment.NewLine + "Actual Voltage : " + RectifierIntputs[7].ActualVoltage.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Rectifier 9")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + RectifierIntputs[8].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + RectifierIntputs[8].LowSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Current : " + RectifierIntputs[8].ActualCurrent.ToString();
                        Values = Values + System.Environment.NewLine + "Actual Voltage : " + RectifierIntputs[8].ActualVoltage.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Rectifier 10")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + RectifierIntputs[9].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + RectifierIntputs[9].LowSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Current : " + RectifierIntputs[9].ActualCurrent.ToString();
                        Values = Values + System.Environment.NewLine + "Actual Voltage : " + RectifierIntputs[9].ActualVoltage.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Rectifier 11")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + RectifierIntputs[10].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + RectifierIntputs[10].LowSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Current : " + RectifierIntputs[10].ActualCurrent.ToString();
                        Values = Values + System.Environment.NewLine + "Actual Voltage : " + RectifierIntputs[10].ActualVoltage.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Rectifier 12")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + RectifierIntputs[11].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + RectifierIntputs[11].LowSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Current : " + RectifierIntputs[11].ActualCurrent.ToString();
                        Values = Values + System.Environment.NewLine + "Actual Voltage : " + RectifierIntputs[11].ActualVoltage.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "Rectifier 13")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + RectifierIntputs[11].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + RectifierIntputs[11].LowSP.ToString();
                        string Values = System.Environment.NewLine + "Actual Current : " + RectifierIntputs[12].ActualCurrent.ToString();
                        Values = Values + System.Environment.NewLine + "Actual Voltage : " + RectifierIntputs[12].ActualVoltage.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    #endregion
                }

                if (_commandparameters.ToString().Contains("pH"))
                {
                     #region pH
                    if (_commandparameters.ToString() == "pH 1")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values =  System.Environment.NewLine +"High SP : " +pHIntputs[0].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine+ "Low SP : " + pHIntputs[0].LowSP.ToString();
                        string Values = System.Environment.NewLine + "Actual pH : " + pHIntputs[0].ActualpH.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "pH 2")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + pHIntputs[1].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + pHIntputs[1].LowSP.ToString();
                        string Values = System.Environment.NewLine + "Actual pH : " + pHIntputs[1].ActualpH.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "pH 3")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + pHIntputs[2].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + pHIntputs[2].LowSP.ToString();
                        string Values = System.Environment.NewLine + "Actual pH : " + pHIntputs[2].ActualpH.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    else if (_commandparameters.ToString() == "pH 4")
                    {
                        string DisplayData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                        //string Values = System.Environment.NewLine + "High SP : " + pHIntputs[3].HighSP.ToString();
                        //Values = Values + System.Environment.NewLine + "Low SP : " + pHIntputs[3].LowSP.ToString();
                        string Values = System.Environment.NewLine + "Actual pH : " + pHIntputs[3].ActualpH.ToString();
                        TooltipData = DisplayData + Values;
                    }
                    #endregion
                }

                if (_commandparameters.ToString().Contains("WCS"))
                {
                     #region WCS
                    if (_commandparameters.ToString() == "WCS 1")
                    {
                        TooltipData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                    }
                    else if (_commandparameters.ToString() == "WCS 2")
                    {
                        TooltipData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                    }
                    else if (_commandparameters.ToString() == "WCS 3")
                    {
                        TooltipData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                    }
                    else if (_commandparameters.ToString() == "WCS 4")
                    {
                        TooltipData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                    }
                    else if (_commandparameters.ToString() == "WCS 5")
                    {
                        TooltipData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                    }
                    else if (_commandparameters.ToString() == "WCS 6")
                    {
                        TooltipData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                    }
                    else if (_commandparameters.ToString() == "WCS 7")
                    {
                        TooltipData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                    }
                    #endregion
                }
                else
                { 
                    if (_commandparameters.ToString() == "HMI")
                    {
                        TooltipData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                    }
                    else if (_commandparameters.ToString() == "Main PLC 1")
                    {
                        TooltipData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                    }
                    else if (_commandparameters.ToString() == "SCADA PC")
                    {
                        TooltipData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                    }
                    else if (_commandparameters.ToString() == "Ethernet Switch")
                    {
                        TooltipData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                    }


                    else if (_commandparameters.ToString() == "Device 1")
                    {
                        TooltipData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                    }
                    else if (_commandparameters.ToString() == "Device 2")
                    {
                        TooltipData = IndiSCADABusinessLogic.OverviewLogic.GetNetworkData(_commandparameters.ToString());
                    }

                }
                
                //image
                TooltipImageName = IndiSCADABusinessLogic.OverviewLogic.GetImageName(_commandparameters.ToString()); 
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("HomeViewModel OpenSummeryClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }

        #region "Wagon Movement Logic"

        private void W1Move()
        {
            #region "Wagon "
            try
            {
                if (IndiSCADAGlobalLibrary.TagList.WagonMovment != null)
                {
                    int WagonPosition = Convert.ToInt16(IndiSCADAGlobalLibrary.TagList.WagonMovment[0]);
                    //int WagonPosition = 10;
                    //rotate from  left to right
                    int TimeToTravel = 3;
                    int xpo = Convert.ToInt32(W1.X / 35); 

                    if (WagonPosition != 0)
                    {
                        if (WagonPosition > xpo)
                        {
                            TimeToTravel = (WagonPosition - xpo) * 1;
                        }
                        else
                        {
                            TimeToTravel = (xpo - WagonPosition) * 1;
                        }
                    }
                    int wagonPositionXValue = (WagonPosition - 1) * 35;
                    Wagon1Movement(wagonPositionXValue, TimeToTravel);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork(Wagon 1)", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            #endregion
        }
        private void W2Move()
        {
            #region "Wagon "
            try
            {
                if (IndiSCADAGlobalLibrary.TagList.WagonMovment != null)
                {
                    int WagonPosition = Convert.ToInt16(IndiSCADAGlobalLibrary.TagList.WagonMovment[1]);
                    //rotate from  left to right
                    // int  WagonPosition = 15;
                    int TimeToTravel = 3;
                    int xpo = Convert.ToInt32(W2.X / 35);
                    if (WagonPosition != 0)
                    {
                        if (WagonPosition > xpo)
                        {
                            TimeToTravel = (WagonPosition - xpo) * 1;
                        }
                        else
                        {
                            TimeToTravel = (xpo - WagonPosition) * 1;
                        }
                    }
                    int wagonPositionXValue = (WagonPosition - 1) * 35;
                    Wagon2Movement(wagonPositionXValue, TimeToTravel);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork(Wagon 2)", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            #endregion
        }
        private void W3Move()
        {
            #region "Wagon "
            try
            {
                if (IndiSCADAGlobalLibrary.TagList.WagonMovment != null)
                {
                    int WagonPosition = Convert.ToInt16(IndiSCADAGlobalLibrary.TagList.WagonMovment[2]);
                    //rotate from right to left
                  //  int  WagonPosition = 18;
                    int TimeToTravel = 3;
                    int xpo = Convert.ToInt32(W3.X / 35);
                    if (WagonPosition != 0)
                    {
                        if (WagonPosition > xpo)
                        {
                            TimeToTravel = (WagonPosition - xpo) * 1;
                        }
                        else
                        {
                            TimeToTravel = (xpo - WagonPosition) * 1;
                        }
                    }
                    int wagonPositionXValue = (WagonPosition - 1) * 35;
                    Wagon3Movement(wagonPositionXValue, TimeToTravel);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork(Wagon 3)", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            #endregion
        }
        private void W4Move()
        {
            #region "Wagon "
            try
            {
                if (IndiSCADAGlobalLibrary.TagList.WagonMovment != null)
                {
                    //  int  WagonPosition = 18;
                    int WagonPosition = Convert.ToInt16(IndiSCADAGlobalLibrary.TagList.WagonMovment[3]);

                    //rotate from right to left
                    int TimeToTravel = 3;
                    int xpo = Convert.ToInt32(W4.X / 35);
                    if (WagonPosition != 0)
                    {
                        if (WagonPosition > xpo)
                        {
                            TimeToTravel = (WagonPosition - xpo) * 1;
                        }
                        else
                        {
                            TimeToTravel = (xpo - WagonPosition) * 1;
                        }
                    }
                    int wagonPositionXValue = (WagonPosition - 1) * 35;
                    Wagon4Movement(wagonPositionXValue, TimeToTravel);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork(Wagon 4)", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            #endregion
        }
        private void W5Move()
        {
            #region "Wagon "
            try
            { 
                if (IndiSCADAGlobalLibrary.TagList.WagonMovment != null)
                {
                    int WagonPosition = Convert.ToInt16(IndiSCADAGlobalLibrary.TagList.WagonMovment[4]);
                    //rotate from left to right 

                    int TimeToTravel = 3;
                    int xpo = Convert.ToInt32(W5.X / 35);
                    if (WagonPosition != 0)
                    {
                        if (WagonPosition > xpo)
                        {
                            TimeToTravel = (WagonPosition - xpo) * 1;
                        }
                        else
                        {
                            TimeToTravel = (xpo - WagonPosition) * 1;
                        }
                    }
                    if (WagonPosition >= 69)
                    {
                        int wagonPositionXValue = (WagonPosition - 69) * 35;
                        Wagon5Movement(wagonPositionXValue, TimeToTravel);
                    }
                    else
                    {
                        int wagonPositionXValue = (69 - WagonPosition) * 35;
                        Wagon5Movement(wagonPositionXValue, TimeToTravel);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork(Wagon 5)", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            #endregion
        }
        private void W6Move()
        {
            #region "Wagon "
            try
            {
                if (IndiSCADAGlobalLibrary.TagList.WagonMovment != null)
                {
                    int WagonPosition = Convert.ToInt16(IndiSCADAGlobalLibrary.TagList.WagonMovment[5]);
                    //rotate from left to right 

                    int TimeToTravel = 3;
                    int xpo = Convert.ToInt32(W6.X / 35);
                    if (WagonPosition != 0)
                    {
                        if (WagonPosition > xpo)
                        {
                            TimeToTravel = (WagonPosition - xpo) * 1;
                        }
                        else
                        {
                            TimeToTravel = (xpo - WagonPosition) * 1;
                        }
                    }
                    if (WagonPosition >= 69)
                    {
                        int wagonPositionXValue = (WagonPosition - 69) * 35;
                        Wagon6Movement(wagonPositionXValue, TimeToTravel);
                    }
                    else
                    {
                        int wagonPositionXValue = (69 - WagonPosition) * 35;
                        Wagon6Movement(wagonPositionXValue, TimeToTravel);
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork(Wagon 1)", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            #endregion
        }
        private void W7Move()
        {
            #region "Wagon "
            try
            {
                if (IndiSCADAGlobalLibrary.TagList.WagonMovment != null)
                {
                    int WagonPosition = Convert.ToInt16(IndiSCADAGlobalLibrary.TagList.WagonMovment[6]);
                    //rotate from left to right 

                    int TimeToTravel = 3;
                    int xpo = Convert.ToInt32(W7.X / 35);
                    if (WagonPosition != 0)
                    {
                        if (WagonPosition > xpo)
                        {
                            TimeToTravel = (WagonPosition - xpo) * 1;
                        }
                        else
                        {
                            TimeToTravel = (xpo - WagonPosition) * 1;
                        }
                    }
                    if (WagonPosition >= 69)
                    {
                        int wagonPositionXValue = (WagonPosition - 69) * 35;
                        Wagon7Movement(wagonPositionXValue, TimeToTravel);
                    }
                    else
                    {
                        int wagonPositionXValue = (69 - WagonPosition) * 35;
                        Wagon7Movement(wagonPositionXValue, TimeToTravel);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork(Wagon 1)", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            #endregion

        }
        //private void W8Move()
        //{
        //    #region "Wagon "
        //    try
        //    {
        //        if (IndiSCADAGlobalLibrary.TagList.WagonMovment != null)
        //        {
        //            int WagonPosition = Convert.ToInt16(IndiSCADAGlobalLibrary.TagList.WagonMovment[7]);

        //            // int WagonPosition = 58;
        //            //rotate from right to left
        //            int TimeToTravel = 3;
        //            int xpo = Convert.ToInt32(W8.X / 45);

        //            if (WagonPosition != 0)
        //            {
        //                if (WagonPosition > xpo)
        //                {
        //                    TimeToTravel = (WagonPosition - xpo) * 1;
        //                }
        //                else
        //                {
        //                    TimeToTravel = (xpo - WagonPosition) * 1;
        //                }
        //            }

        //            int wagonPositionXValue = (WagonPosition - 43) * 45;
        //            Wagon8Movement(wagonPositionXValue, TimeToTravel);

        //            //int WagonPosition = Convert.ToInt16(IndiSCADAGlobalLibrary.TagList.WagonMovment[7]);

        //            ////  int  WagonPosition = 58;
        //            ////rotate from right to left
        //            //int TimeToTravel = 3;
        //            //int xpo = Convert.ToInt32(W8.X / 45);

        //            //if (WagonPosition != 0)
        //            //{
        //            //    if (WagonPosition > xpo)
        //            //    {
        //            //        TimeToTravel = (WagonPosition - xpo) * 1;
        //            //    }
        //            //    else
        //            //    {
        //            //        TimeToTravel = (xpo - WagonPosition) * 1;
        //            //    }
        //            //}
        //            //if (WagonPosition >= 60)
        //            //{
        //            //    int wagonPositionXValue = (WagonPosition - 60) * 45;
        //            //    Wagon8Movement(wagonPositionXValue, TimeToTravel);
        //            //}
        //            //else
        //            //{
        //            //    int wagonPositionXValue = (60 - WagonPosition) * 45;
        //            //    Wagon8Movement(wagonPositionXValue, TimeToTravel);
        //            //}
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork(Wagon 1)", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
        //    }
        //    #endregion

        //}

        private void Wagon1Movement(int W1position, int TimeTOReach)
        {
            try
            {
                if (W1position > 0)
                {
                    if (TimeTOReach > 0)
                    {
                        //DoubleAnimation anim1 = new DoubleAnimation(Convert.ToInt32(W1position), TimeSpan.FromSeconds(TimeTOReach));
                        //W1.BeginAnimation(TranslateTransform.XProperty, anim1);
                        W1.X = W1position;
                        OnPropertyChanged("W1");
                    }
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel Wagon1Movement()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void Wagon2Movement(int W2position, int TimeTOReach)
        {
            try
            {
                if (W2position > 0)
                {
                    if (TimeTOReach > 0)
                    {
                        W2.X = W2position;
                        OnPropertyChanged("W2");
                    }
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel Wagon2Movement()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void Wagon3Movement(int W3position, int TimeTOReach)
        {
            try
            {
                if (W3position > 0)
                {
                    if (TimeTOReach > 0)
                    {
                        W3.X = W3position;
                        OnPropertyChanged("W3");
                    }
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel Wagon3Movement()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void Wagon4Movement(int W4position, int TimeTOReach)
        {
            try
            {
                if (W4position > 0)
                {
                    if (TimeTOReach > 0)
                    {
                        W4.X = W4position;
                        OnPropertyChanged("W4");
                    }
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel Wagon4Movement()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void Wagon5Movement(int W5position, int TimeTOReach)
        {
            try
            {
                if (W5position > 0)
                {
                    if (TimeTOReach > 0)
                    {
                        W5.X = W5position;
                        OnPropertyChanged("W5");
                    }
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel Wagon5Movement()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void Wagon6Movement(int W6position, int TimeTOReach)
        {
            try
            {
                if (W6position > 0)
                {
                    if (TimeTOReach > 0)
                    {
                        W6.X = W6position;
                        OnPropertyChanged("W6");
                    }
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel Wagon4Movement()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void Wagon7Movement(int W7position, int TimeTOReach)
        {
            try
            {
                if (W7position > 0)
                {
                    if (TimeTOReach > 0)
                    {
                        W7.X = W7position;
                        OnPropertyChanged("W7");
                    }
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel Wagon7Movement()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void Wagon8Movement(int W8position, int TimeTOReach)
        {
            try
            {
                if (W8position > 0)
                {
                    if (TimeTOReach > 0)
                    {
                        W8.X = W8position;
                        OnPropertyChanged("W8");
                    }
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel Wagon8Movement()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        #endregion

        #region "wagonmovement old logic Commented"
        //private void Wagon1Movement(string W1position, int TimeTOReach)
        //{
        //    try
        //    {
        //        if (W1position.Length > 0)
        //        {
        //            if (TimeTOReach > 0)
        //            {
        //                DoubleAnimation anim1 = new DoubleAnimation(Convert.ToInt32(W1position), TimeSpan.FromSeconds(TimeTOReach));
        //                W1.BeginAnimation(TranslateTransform.XProperty, anim1);
        //            }
        //        }
        //    }
        //    catch (Exception exExitButtonCommandClick)
        //    {
        //        ErrorLogger.LogError.ErrorLog("OverviewViewModel Wagon1Movement()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
        //    }
        //}
        //private void Wagon2Movement(string W2position, int TimeTOReach)
        //{
        //    try
        //    {
        //        if (W2position.Length > 0)
        //        {
        //            if (TimeTOReach > 0)
        //            {
        //                DoubleAnimation anim1 = new DoubleAnimation(Convert.ToInt32(W2position), TimeSpan.FromSeconds(TimeTOReach));
        //                W2.BeginAnimation(TranslateTransform.XProperty, anim1);
        //            }
        //        }
        //    }
        //    catch (Exception exExitButtonCommandClick)
        //    {
        //        ErrorLogger.LogError.ErrorLog("OverviewViewModel Wagon2Movement()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
        //    }
        //}
        //private void Wagon3Movement(string W3position, int TimeTOReach)
        //{
        //    try
        //    {
        //        if (W3position.Length > 0)
        //        {
        //            if (TimeTOReach > 0)
        //            {
        //                DoubleAnimation anim1 = new DoubleAnimation(Convert.ToInt32(W3position), TimeSpan.FromSeconds(TimeTOReach));
        //                W3.BeginAnimation(TranslateTransform.XProperty, anim1);
        //            }
        //        }
        //    }
        //    catch (Exception exExitButtonCommandClick)
        //    {
        //        ErrorLogger.LogError.ErrorLog("OverviewViewModel Wagon3Movement()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
        //    }
        //}
        //private void Wagon4Movement(string W4position,int TimeTOReach)
        //{
        //    try
        //    {
        //        if (W4position.Length > 0)
        //        {
        //            if (TimeTOReach > 0)
        //            {
        //                DoubleAnimation anim1 = new DoubleAnimation(Convert.ToInt32(W4position), TimeSpan.FromSeconds(TimeTOReach));
        //                W4.BeginAnimation(TranslateTransform.XProperty, anim1);
        //            }
        //        }
        //    }
        //    catch (Exception exExitButtonCommandClick)
        //    {
        //        ErrorLogger.LogError.ErrorLog("OverviewViewModel Wagon4Movement()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
        //    }
        //}
        //private void Wagon5Movement(string W5position, int TimeTOReach)
        //{
        //    try
        //    {
        //        if (W5position.Length > 0)
        //        {
        //            if (TimeTOReach > 0)
        //            {
        //                DoubleAnimation anim1 = new DoubleAnimation(Convert.ToInt32(W5position), TimeSpan.FromSeconds(TimeTOReach));
        //                W5.BeginAnimation(TranslateTransform.XProperty, anim1);
        //            }
        //        }
        //    }
        //    catch (Exception exExitButtonCommandClick)
        //    {
        //        ErrorLogger.LogError.ErrorLog("OverviewViewModel Wagon5Movement()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
        //    }
        //}
        #endregion

        private void ExitButtonCommandClick(object _commandparameters)
        {
            try
            {
                DispatchTimerView.Stop();
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel ExitButtonCommandClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        
        private void DoWork(object Sender, System.ComponentModel.DoWorkEventArgs e)
        { 
            try
            {
                if (isSettingStartup == true)
                {   
                    //Station data
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            ObservableCollection<OverViewEntity> _we = new ObservableCollection<OverViewEntity>();
                            ServiceResponse<IList> _result = IndiSCADABusinessLogic.OverviewLogic.SelectStationName(1);
                            if (_result.Response != null)
                            {
                                IList<OverViewEntity> _IListresult = (IList<OverViewEntity>)(_result.Response);
                                _we = new ObservableCollection<OverViewEntity>(_IListresult.Where(x => x.LineNo == "1"));
                            }
                            StationsData1 = new ObservableCollection<StationDetails>();
                            StationsData1.Add(new StationDetails()
                            {
                                StationData1 = _we
                            });

                            ObservableCollection<OverViewEntity> _we2 = new ObservableCollection<OverViewEntity>();
                            ServiceResponse<IList> _result2 = IndiSCADABusinessLogic.OverviewLogic.SelectStationName(2);
                            if (_result2.Response != null)
                            {
                                IList<OverViewEntity> _IListresult = (IList<OverViewEntity>)(_result2.Response);
                                _we2 = new ObservableCollection<OverViewEntity>(_IListresult.Where(x => x.LineNo == "2"));
                            }
                            StationsData2 = new ObservableCollection<StationDetails>();
                            StationsData2.Add(new StationDetails()
                            {
                                StationData2 = _we2
                            });

                            //ObservableCollection<OverViewEntity> _we3 = new ObservableCollection<OverViewEntity>();
                            //ServiceResponse<IList> _result3 = IndiSCADABusinessLogic.OverviewLogic.SelectStationName(3);
                            //if (_result3.Response != null)
                            //{
                            //    IList<OverViewEntity> _IListresult = (IList<OverViewEntity>)(_result3.Response);
                            //    _we3 = new ObservableCollection<OverViewEntity>(_IListresult.Where(x => x.LineNo == "3"));
                            //}
                            //StationsData3 = new ObservableCollection<StationDetails>();
                            //StationsData3.Add(new StationDetails()
                            //{
                            //    StationData3 = _we3
                            //});
                        }
                        catch (Exception ex)
                        { ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork() SelectStationName()", DateTime.Now.ToString(), ex.Message, "No", true); }
                    }));

                    //Wagon Operation
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            W1NextStep = IndiSCADABusinessLogic.OverviewLogic.GetWagonCurrentOperation(1);
                            W2NextStep = IndiSCADABusinessLogic.OverviewLogic.GetWagonCurrentOperation(2);
                            W3NextStep = IndiSCADABusinessLogic.OverviewLogic.GetWagonCurrentOperation(3);
                            W4NextStep = IndiSCADABusinessLogic.OverviewLogic.GetWagonCurrentOperation(4);
                            W5NextStep = IndiSCADABusinessLogic.OverviewLogic.GetWagonCurrentOperation(5);
                            W6NextStep = IndiSCADABusinessLogic.OverviewLogic.GetWagonCurrentOperation(6);
                            W7NextStep = IndiSCADABusinessLogic.OverviewLogic.GetWagonCurrentOperation(7);
                            //W8NextStep = IndiSCADABusinessLogic.OverviewLogic.GetWagonCurrentOperation(8);

                            W1AutoMan = IndiSCADABusinessLogic.OverviewLogic.GetWagonStatus(1);
                            W2AutoMan = IndiSCADABusinessLogic.OverviewLogic.GetWagonStatus(2);
                            W3AutoMan = IndiSCADABusinessLogic.OverviewLogic.GetWagonStatus(3);
                            W4AutoMan = IndiSCADABusinessLogic.OverviewLogic.GetWagonStatus(4);
                            W5AutoMan = IndiSCADABusinessLogic.OverviewLogic.GetWagonStatus(5);
                            W6AutoMan = IndiSCADABusinessLogic.OverviewLogic.GetWagonStatus(6);
                            W7AutoMan = IndiSCADABusinessLogic.OverviewLogic.GetWagonStatus(7);
                            //W8AutoMan = IndiSCADABusinessLogic.OverviewLogic.GetWagonStatus(8);

                            PlantAM = IndiSCADABusinessLogic.OverviewLogic.GetPlantStatus("AM");
                            PlantService = IndiSCADABusinessLogic.OverviewLogic.GetPlantStatus("Service");
                            PlantCycleON = IndiSCADABusinessLogic.OverviewLogic.GetPlantStatus("CycleON");
                            PlantReset = IndiSCADABusinessLogic.OverviewLogic.GetPlantStatus("Reset");
                            PlantPowerON = IndiSCADABusinessLogic.OverviewLogic.GetPlantStatus("PowerON");
                        }
                        catch (Exception ex)
                        { ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork() GetWagonCurrentOperation(), GetWagonStatus(), GetPlantStatus()", DateTime.Now.ToString(), ex.Message, "No", true); }
                    }));
                    
                    // Wagon movement
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            if (IndiSCADAGlobalLibrary.TagList.WagonMovment.Length > 0)
                            {
                                if (IndiSCADAGlobalLibrary.TagList.WagonMovment[0] != null)
                                    W1Move();
                                if (IndiSCADAGlobalLibrary.TagList.WagonMovment[1] != null)
                                    W2Move();
                                if (IndiSCADAGlobalLibrary.TagList.WagonMovment[2] != null)
                                    W3Move();
                                if (IndiSCADAGlobalLibrary.TagList.WagonMovment[3] != null)
                                    W4Move();
                                if (IndiSCADAGlobalLibrary.TagList.WagonMovment[4] != null)
                                    W5Move();
                                if (IndiSCADAGlobalLibrary.TagList.WagonMovment[5] != null)
                                    W6Move();
                                if (IndiSCADAGlobalLibrary.TagList.WagonMovment[6] != null)
                                    W7Move();

                                //if (IndiSCADAGlobalLibrary.TagList.WagonMovment[7] != null)
                                //    W8Move();
                            }
                        }
                        catch (Exception ex)
                        { ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork() WagonMovment()", DateTime.Now.ToString(), ex.Message, "No", true); }
                    }));

                    //Temperature values
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            TemperatureIntputs = IndiSCADABusinessLogic.SettingLogic.GetTemperatureInputs();
                        }
                        catch (Exception ex)
                        { ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork() GetTemperatureInputs()", DateTime.Now.ToString(), ex.Message, "No", true); }
                    }));

                    //pH
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            pHIntputs = IndiSCADABusinessLogic.SettingLogic.GetpHInputs();
                        }
                        catch (Exception ex)
                        { ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork() GetpHInputs()", DateTime.Now.ToString(), ex.Message, "No", true); }
                    }));

                    //Rectifier
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            RectifierIntputs = IndiSCADABusinessLogic.SettingLogic.GetRectifierInputs();
                        }
                        catch (Exception ex)
                        { ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork() GetRectifierInputs()", DateTime.Now.ToString(), ex.Message, "No", true); }
                    }));

                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            App.Current.Dispatcher.BeginInvoke(new Action(() => { WagonPrerequisites = IndiSCADABusinessLogic.OverviewLogic.GetWagonPrerequisites(); }));
                            App.Current.Dispatcher.BeginInvoke(new Action(() => { CTPrerequisites = IndiSCADABusinessLogic.OverviewLogic.GetCTPrerequisites(); }));
                            App.Current.Dispatcher.BeginInvoke(new Action(() => { UnloaderPrerequisites = IndiSCADABusinessLogic.OverviewLogic.GetUnloaderPrerequisites(); }));
                            App.Current.Dispatcher.BeginInvoke(new Action(() => { DryerPrerequisites = IndiSCADABusinessLogic.OverviewLogic.GetDryerPrerequisites(); }));
                            //App.Current.Dispatcher.BeginInvoke(new Action(() => { BarrelPrerequisites = IndiSCADABusinessLogic.OverviewLogic.GetBarrelPrerequisites(); }));
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork() Prerequisites", DateTime.Now.ToString(), ex.Message, "No", true);
                        }
                    }));
                }

                //overview
                if (SelectedSettingTab == 0)
                {
                    //Station data
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            ObservableCollection<OverViewEntity> _we = new ObservableCollection<OverViewEntity>();
                            ServiceResponse<IList> _result = IndiSCADABusinessLogic.OverviewLogic.SelectStationName(1);
                            if (_result.Response != null)
                            {
                                IList<OverViewEntity> _IListresult = (IList<OverViewEntity>)(_result.Response);
                                _we = new ObservableCollection<OverViewEntity>(_IListresult.Where(x => x.LineNo == "1"));
                            }
                            StationsData1 = new ObservableCollection<StationDetails>();
                            StationsData1.Add(new StationDetails()
                            {
                                StationData1 = _we
                            });

                            ObservableCollection<OverViewEntity> _we2 = new ObservableCollection<OverViewEntity>();
                            ServiceResponse<IList> _result2 = IndiSCADABusinessLogic.OverviewLogic.SelectStationName(2);
                            if (_result2.Response != null)
                            {
                                IList<OverViewEntity> _IListresult = (IList<OverViewEntity>)(_result2.Response);
                                _we2 = new ObservableCollection<OverViewEntity>(_IListresult.Where(x => x.LineNo == "2"));
                            }
                            StationsData2 = new ObservableCollection<StationDetails>();
                            StationsData2.Add(new StationDetails()
                            {
                                StationData2 = _we2
                            });

                            //ObservableCollection<OverViewEntity> _we3 = new ObservableCollection<OverViewEntity>();
                            //ServiceResponse<IList> _result3 = IndiSCADABusinessLogic.OverviewLogic.SelectStationName(3);
                            //if (_result3.Response != null)
                            //{
                            //    IList<OverViewEntity> _IListresult = (IList<OverViewEntity>)(_result3.Response);
                            //    _we3 = new ObservableCollection<OverViewEntity>(_IListresult.Where(x => x.LineNo == "3"));
                            //}
                            //StationsData3 = new ObservableCollection<StationDetails>();
                            //StationsData3.Add(new StationDetails()
                            //{
                            //    StationData3 = _we3
                            //});
                        }
                        catch (Exception ex)
                        { ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork() SelectStationName()", DateTime.Now.ToString(), ex.Message, "No", true); }
                    }));

                    //Wagon Operation
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            W1NextStep = IndiSCADABusinessLogic.OverviewLogic.GetWagonCurrentOperation(1);
                            W2NextStep = IndiSCADABusinessLogic.OverviewLogic.GetWagonCurrentOperation(2);
                            W3NextStep = IndiSCADABusinessLogic.OverviewLogic.GetWagonCurrentOperation(3);
                            W4NextStep = IndiSCADABusinessLogic.OverviewLogic.GetWagonCurrentOperation(4);
                            W5NextStep = IndiSCADABusinessLogic.OverviewLogic.GetWagonCurrentOperation(5);
                            W6NextStep = IndiSCADABusinessLogic.OverviewLogic.GetWagonCurrentOperation(6);
                            W7NextStep = IndiSCADABusinessLogic.OverviewLogic.GetWagonCurrentOperation(7);
                            //W8NextStep = IndiSCADABusinessLogic.OverviewLogic.GetWagonCurrentOperation(8);

                            W1AutoMan = IndiSCADABusinessLogic.OverviewLogic.GetWagonStatus(1);
                            W2AutoMan = IndiSCADABusinessLogic.OverviewLogic.GetWagonStatus(2);
                            W3AutoMan = IndiSCADABusinessLogic.OverviewLogic.GetWagonStatus(3);
                            W4AutoMan = IndiSCADABusinessLogic.OverviewLogic.GetWagonStatus(4);
                            W5AutoMan = IndiSCADABusinessLogic.OverviewLogic.GetWagonStatus(5);
                            W6AutoMan = IndiSCADABusinessLogic.OverviewLogic.GetWagonStatus(6);
                            W7AutoMan = IndiSCADABusinessLogic.OverviewLogic.GetWagonStatus(7);
                            //W8AutoMan = IndiSCADABusinessLogic.OverviewLogic.GetWagonStatus(8);

                            PlantAM = IndiSCADABusinessLogic.OverviewLogic.GetPlantStatus("AM");
                            PlantService = IndiSCADABusinessLogic.OverviewLogic.GetPlantStatus("Service");
                            PlantCycleON = IndiSCADABusinessLogic.OverviewLogic.GetPlantStatus("CycleON");
                            PlantReset = IndiSCADABusinessLogic.OverviewLogic.GetPlantStatus("Reset");
                            PlantPowerON = IndiSCADABusinessLogic.OverviewLogic.GetPlantStatus("PowerON");
                        }
                        catch (Exception ex)
                        { ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork() GetWagonCurrentOperation(), GetWagonStatus(), GetPlantStatus()", DateTime.Now.ToString(), ex.Message, "No", true); }
                    }));


                    // Wagon movement
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            if (IndiSCADAGlobalLibrary.TagList.WagonMovment.Length > 0)
                            {
                                if (IndiSCADAGlobalLibrary.TagList.WagonMovment[0] != null)
                                    W1Move();
                                if (IndiSCADAGlobalLibrary.TagList.WagonMovment[1] != null)
                                    W2Move();
                                if (IndiSCADAGlobalLibrary.TagList.WagonMovment[2] != null)
                                    W3Move();
                                if (IndiSCADAGlobalLibrary.TagList.WagonMovment[3] != null)
                                    W4Move();
                                if (IndiSCADAGlobalLibrary.TagList.WagonMovment[4] != null)
                                    W5Move();
                                if (IndiSCADAGlobalLibrary.TagList.WagonMovment[5] != null)
                                    W6Move();
                                if (IndiSCADAGlobalLibrary.TagList.WagonMovment[6] != null)
                                    W7Move();

                                //if (IndiSCADAGlobalLibrary.TagList.WagonMovment[7] != null)
                                //    W8Move();
                            }
                        }
                        catch (Exception ex)
                        { ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork() WagonMovment()", DateTime.Now.ToString(), ex.Message, "No", true); }
                    }));

                }

                //data view
                else if (SelectedSettingTab == 1)
                {

                    //Temperature values
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            TemperatureIntputs = IndiSCADABusinessLogic.SettingLogic.GetTemperatureInputs();
                        }
                        catch (Exception ex)
                        { ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork() GetTemperatureInputs()", DateTime.Now.ToString(), ex.Message, "No", true); }
                    }));

                    //pH
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            pHIntputs = IndiSCADABusinessLogic.SettingLogic.GetpHInputs();
                        }
                        catch (Exception ex)
                        { ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork() GetpHInputs()", DateTime.Now.ToString(), ex.Message, "No", true); }
                    }));

                    //Rectifier
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            RectifierIntputs = IndiSCADABusinessLogic.SettingLogic.GetRectifierInputs();
                        }
                        catch (Exception ex)
                        { ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork() GetRectifierInputs()", DateTime.Now.ToString(), ex.Message, "No", true); }
                    }));

                }

                //prerequisies
                else if (SelectedSettingTab == 2)
                {
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            App.Current.Dispatcher.BeginInvoke(new Action(() => { WagonPrerequisites = IndiSCADABusinessLogic.OverviewLogic.GetWagonPrerequisites(); }));
                            App.Current.Dispatcher.BeginInvoke(new Action(() => { CTPrerequisites = IndiSCADABusinessLogic.OverviewLogic.GetCTPrerequisites(); }));
                            App.Current.Dispatcher.BeginInvoke(new Action(() => { UnloaderPrerequisites = IndiSCADABusinessLogic.OverviewLogic.GetUnloaderPrerequisites(); }));
                            App.Current.Dispatcher.BeginInvoke(new Action(() => { DryerPrerequisites = IndiSCADABusinessLogic.OverviewLogic.GetDryerPrerequisites(); }));
                            //App.Current.Dispatcher.BeginInvoke(new Action(() => { BarrelPrerequisites = IndiSCADABusinessLogic.OverviewLogic.GetBarrelPrerequisites(); }));
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork() Prerequisites", DateTime.Now.ToString(), ex.Message, "No", true);
                        }
                    }));
                }

                //network diagram communiation  
                else if (SelectedSettingTab == 3)
                {
                    //App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    //{
                    //    try
                    //    {
                    //        W1Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("WCS1");
                    //        W2Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("WCS2");
                    //        W3Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("WCS3");
                    //        W4Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("WCS4");
                    //        W5Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("WCS5");
                    //        W6Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("WCS6");
                    //        W7Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("WCS7");

                    //        Temp1Communication= IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp1");
                    //        Temp2Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp2");
                    //        Temp3Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp3");
                    //        Temp4Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp4");
                    //        Temp5Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp5");
                    //        Temp6Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp6");
                    //        Temp7Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp7");
                    //        Temp8Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp8");
                    //        Temp9Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp9");
                    //        Temp10Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp10");
                    //        Temp11Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp11");
                    //        Temp12Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp12");
                    //        Temp13Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp13");
                    //        Temp14Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp14");
                    //        Temp15Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp15");
                    //        Temp16Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp16");
                    //        Temp17Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp17");
                    //        Temp18Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp18");
                    //        Temp19Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp19");
                    //        Temp20Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp20");
                    //        Temp21Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp21");
                    //        Temp22Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp22");
                    //        Temp23Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp23");
                    //        Temp24Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp24");
                    //        Temp25Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Temp25");

                    //        pH1Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("pH1");
                    //        pH2Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("pH2");
                    //        pH3Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("pH3");
                    //        pH4Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("pH4");

                    //        Rect1Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Rect1");
                    //        Rect2Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Rect2");
                    //        Rect3Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Rect3");
                    //        Rect4Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Rect4");
                    //        Rect5Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Rect5");
                    //        Rect6Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Rect6");
                    //        Rect7Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Rect7");
                    //        Rect8Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Rect8");
                    //        Rect9Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Rect9");
                    //        Rect10Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Rect10");
                    //        Rect11Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Rect11");
                    //        Rect12Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Rect12");
                    //        Rect13Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Rect13");

                    //        Device1Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Device1");
                    //        Device2Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("Device2");
                    //        ScadaPcCommunication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("ScadaPc");
                    //        HMICommunication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("HMI");
                    //        MainPLC1Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("MainPLC1");
                    //        MainPLC1_TO_ECAT1Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("MainPLC1_TO_ECAT1");
                    //        ECAT1_TO_ECAT2Communication = IndiSCADABusinessLogic.OverviewLogic.GetNetworkCommunication("ECAT1_TO_ECAT2");
                    //    }
                    //    catch (Exception ex)
                    //    { ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork() NetworkDesign()", DateTime.Now.ToString(), ex.Message, "No", true); }
                    //}));
                }

                isSettingStartup = false;
                ErrorLogger.LogError.ErrorLog("SettingsViewModel DoWork() isSettingStartup ==false", DateTime.Now.ToString(), "", "No", true);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        void DispatcherTickEvent(object sender, EventArgs e)
        {
            try
            {
                if (_BackgroundWorkerView.IsBusy != true)
                {
                    _BackgroundWorkerView.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel DispatcherTickEvent()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        #endregion

        #region Public/Private Property
        #region overview popup
        private string _PopupLoadNumber;
        public string PopupLoadNumber
        {
            get
            {
                return _PopupLoadNumber;
            }
            set
            {
                _PopupLoadNumber = value;
                OnPropertyChanged("PopupLoadNumber");
            }
        }
        private string _PopupPartName;
        public string PopupPartName
        {
            get
            {
                return _PopupPartName;
            }
            set
            {
                _PopupPartName = value;
                OnPropertyChanged("PopupPartName");
            }
        }
        private string _PopupStationNo;
        public string PopupStationNo
        {
            get
            {
                return _PopupStationNo;
            }
            set
            {
                _PopupStationNo = value;
                OnPropertyChanged("PopupStationNo");
            }
        }
        private string _PopupStationName;
        public string PopupStationName
        {
            get
            {
                return _PopupStationName;
            }
            set
            {
                _PopupStationName = value;
                OnPropertyChanged("PopupStationName");
            }
        }

        private string _PopupSetCurrent;
        public string PopupSetCurrent
        {
            get
            {
                return _PopupSetCurrent;
            }
            set
            {
                _PopupSetCurrent = value;
                OnPropertyChanged("PopupSetCurrent");
            }
        }
        private string _PopupActualCurrent;
        public string PopupActualCurrent
        {
            get
            {
                return _PopupActualCurrent;
            }
            set
            {
                _PopupActualCurrent = value;
                OnPropertyChanged("PopupActualCurrent");
            }
        }
        private string _PopupActualpH;
        public string PopupActualpH
        {
            get
            {
                return _PopupActualpH;
            }
            set
            {
                _PopupActualpH = value;
                OnPropertyChanged("PopupActualpH");
            }
        }
        private string _PopupLowpH;
        public string PopupLowpH
        {
            get
            {
                return _PopupLowpH;
            }
            set
            {
                _PopupLowpH = value;
                OnPropertyChanged("PopupLowpH");
            }
        }
        private string _PopupHighpH;
        public string PopupHighpH
        {
            get
            {
                return _PopupHighpH;
            }
            set
            {
                _PopupHighpH = value;
                OnPropertyChanged("PopupHighpH");
            }
        }

        private string _PopupActualTemp;
        public string PopupActualTemp
        {
            get
            {
                return _PopupActualTemp;
            }
            set
            {
                _PopupActualTemp = value;
                OnPropertyChanged("PopupActualTemp");
            }
        }
        private string _PopupLowTemp;
        public string PopupLowTemp
        {
            get
            {
                return _PopupLowTemp;
            }
            set
            {
                _PopupLowTemp = value;
                OnPropertyChanged("PopupLowTemp");
            }
        }
        private string _PopupHighTemp;
        public string PopupHighTemp
        {
            get
            {
                return _PopupHighTemp;
            }
            set
            {
                _PopupHighTemp = value;
                OnPropertyChanged("PopupHighTemp");
            }
        }

        private bool _OpenTankInfo;
        public bool OpenTankInfo
        {
            get
            {
                return _OpenTankInfo;
            }
            set
            {
                _OpenTankInfo = value;
                OnPropertyChanged("OpenTankInfo");
            }
        }
        #endregion 

        private int _SelectedSettingTab;
        public int SelectedSettingTab
        {
            get
            {
                return _SelectedSettingTab;
            }
            set
            {
                _SelectedSettingTab = value;
                OnPropertyChanged("SelectedSettingTab");
            }
        }

        private string _W1Communication;
        public string W1Communication
        {
            get
            {
                return _W1Communication;
            }
            set
            {
                _W1Communication = value;
                OnPropertyChanged("W1Communication");
            }
        }
        private string _W2Communication;
        public string W2Communication
        {
            get
            {
                return _W2Communication;
            }
            set
            {
                _W2Communication = value;
                OnPropertyChanged("W2Communication");
            }
        }
        private string _W3Communication;
        public string W3Communication
        {
            get
            {
                return _W3Communication;
            }
            set
            {
                _W3Communication = value;
                OnPropertyChanged("W3Communication");
            }
        }
        private string _W4Communication;
        public string W4Communication
        {
            get
            {
                return _W4Communication;
            }
            set
            {
                _W4Communication = value;
                OnPropertyChanged("W4Communication");
            }
        }
        private string _W5Communication;
        public string W5Communication
        {
            get
            {
                return _W1Communication;
            }
            set
            {
                _W1Communication = value;
                OnPropertyChanged("W1Communication");
            }
        }
        private string _W6Communication;
        public string W6Communication
        {
            get
            {
                return _W6Communication;
            }
            set
            {
                _W6Communication = value;
                OnPropertyChanged("W6Communication");
            }
        }
        private string _W7Communication;
        public string W7Communication
        {
            get
            {
                return _W7Communication;
            }
            set
            {
                _W7Communication = value;
                OnPropertyChanged("W7Communication");
            }
        }

        private string _Temp1Communication;
        public string Temp1Communication
        {
            get
            {
                return _Temp1Communication;
            }
            set
            {
                _Temp1Communication = value;
                OnPropertyChanged("Temp1Communication");
            }
        }
        private string _Temp2Communication;
        public string Temp2Communication
        {
            get
            {
                return _Temp2Communication;
            }
            set
            {
                _Temp2Communication = value;
                OnPropertyChanged("Temp2Communication");
            }
        }
        private string _Temp3Communication;
        public string Temp3Communication
        {
            get
            {
                return _Temp3Communication;
            }
            set
            {
                _Temp3Communication = value;
                OnPropertyChanged("Temp3Communication");
            }
        }
        private string _Temp4Communication;
        public string Temp4Communication
        {
            get
            {
                return _Temp4Communication;
            }
            set
            {
                _Temp4Communication = value;
                OnPropertyChanged("Temp4Communication");
            }
        }
        private string _Temp5Communication;
        public string Temp5Communication
        {
            get
            {
                return _Temp5Communication;
            }
            set
            {
                _Temp5Communication = value;
                OnPropertyChanged("Temp5Communication");
            }
        }
        private string _Temp6Communication;
        public string Temp6Communication
        {
            get
            {
                return _Temp6Communication;
            }
            set
            {
                _Temp6Communication = value;
                OnPropertyChanged("Temp6Communication");
            }
        }
        private string _Temp7Communication;
        public string Temp7Communication
        {
            get
            {
                return _Temp7Communication;
            }
            set
            {
                _Temp7Communication = value;
                OnPropertyChanged("Temp7Communication");
            }
        }
        private string _Temp8Communication;
        public string Temp8Communication
        {
            get
            {
                return _Temp8Communication;
            }
            set
            {
                _Temp8Communication = value;
                OnPropertyChanged("Temp8Communication");
            }
        }
        private string _Temp9Communication;
        public string Temp9Communication
        {
            get
            {
                return _Temp9Communication;
            }
            set
            {
                _Temp9Communication = value;
                OnPropertyChanged("Temp9Communication");
            }
        }
        private string _Temp10Communication;
        public string Temp10Communication
        {
            get
            {
                return _Temp10Communication;
            }
            set
            {
                _Temp10Communication = value;
                OnPropertyChanged("Temp10Communication");
            }
        }

        private string _Temp11Communication;
        public string Temp11Communication
        {
            get
            {
                return _Temp11Communication;
            }
            set
            {
                _Temp11Communication = value;
                OnPropertyChanged("Temp11Communication");
            }
        }
        private string _Temp12Communication;
        public string Temp12Communication
        {
            get
            {
                return _Temp12Communication;
            }
            set
            {
                _Temp12Communication = value;
                OnPropertyChanged("Temp12Communication");
            }
        }
        private string _Temp13Communication;
        public string Temp13Communication
        {
            get
            {
                return _Temp13Communication;
            }
            set
            {
                _Temp13Communication = value;
                OnPropertyChanged("Temp13Communication");
            }
        }
        private string _Temp14Communication;
        public string Temp14Communication
        {
            get
            {
                return _Temp14Communication;
            }
            set
            {
                _Temp14Communication = value;
                OnPropertyChanged("Temp14Communication");
            }
        }
        private string _Temp15Communication;
        public string Temp15Communication
        {
            get
            {
                return _Temp15Communication;
            }
            set
            {
                _Temp15Communication = value;
                OnPropertyChanged("Temp15Communication");
            }
        }
        private string _Temp16Communication;
        public string Temp16Communication
        {
            get
            {
                return _Temp16Communication;
            }
            set
            {
                _Temp16Communication = value;
                OnPropertyChanged("Temp16Communication");
            }
        }
        private string _Temp17Communication;
        public string Temp17Communication
        {
            get
            {
                return _Temp17Communication;
            }
            set
            {
                _Temp17Communication = value;
                OnPropertyChanged("Temp17Communication");
            }
        }
        private string _Temp18Communication;
        public string Temp18Communication
        {
            get
            {
                return _Temp18Communication;
            }
            set
            {
                _Temp18Communication = value;
                OnPropertyChanged("Temp18Communication");
            }
        }
        private string _Temp19Communication;
        public string Temp19Communication
        {
            get
            {
                return _Temp19Communication;
            }
            set
            {
                _Temp19Communication = value;
                OnPropertyChanged("Temp19Communication");
            }
        }
        private string _Temp20Communication;
        public string Temp20Communication
        {
            get
            {
                return _Temp20Communication;
            }
            set
            {
                _Temp20Communication = value;
                OnPropertyChanged("Temp20Communication");
            }
        }
        private string _Temp21Communication;
        public string Temp21Communication
        {
            get
            {
                return _Temp21Communication;
            }
            set
            {
                _Temp21Communication = value;
                OnPropertyChanged("Temp21Communication");
            }
        }
        private string _Temp22Communication;
        public string Temp22Communication
        {
            get
            {
                return _Temp22Communication;
            }
            set
            {
                _Temp22Communication = value;
                OnPropertyChanged("Temp22Communication");
            }
        }
        private string _Temp23Communication;
        public string Temp23Communication
        {
            get
            {
                return _Temp23Communication;
            }
            set
            {
                _Temp23Communication = value;
                OnPropertyChanged("Temp23Communication");
            }
        }
        private string _Temp24Communication;
        public string Temp24Communication
        {
            get
            {
                return _Temp24Communication;
            }
            set
            {
                _Temp24Communication = value;
                OnPropertyChanged("Temp24Communication");
            }
        }
        private string _Temp25Communication;
        public string Temp25Communication
        {
            get
            {
                return _Temp25Communication;
            }
            set
            {
                _Temp25Communication = value;
                OnPropertyChanged("Temp25Communication");
            }
        }

        private string _pH1Communication;
        public string pH1Communication
        {
            get
            {
                return _pH1Communication;
            }
            set
            {
                _pH1Communication = value;
                OnPropertyChanged("pH1Communication");
            }
        }
        private string _pH2Communication;
        public string pH2Communication
        {
            get
            {
                return _pH2Communication;
            }
            set
            {
                _pH2Communication = value;
                OnPropertyChanged("pH2Communication");
            }
        }
        private string _pH3Communication;
        public string pH3Communication
        {
            get
            {
                return _pH3Communication;
            }
            set
            {
                _pH3Communication = value;
                OnPropertyChanged("pH3Communication");
            }
        }
        private string _pH4Communication;
        public string pH4Communication
        {
            get
            {
                return _pH4Communication;
            }
            set
            {
                _pH4Communication = value;
                OnPropertyChanged("pH4Communication");
            }
        }

        private string _Rect1Communication;
        public string Rect1Communication
        {
            get
            {
                return _Rect1Communication;
            }
            set
            {
                _Rect1Communication = value;
                OnPropertyChanged("Rect1Communication");
            }
        }
        private string _Rect2Communication;
        public string Rect2Communication
        {
            get
            {
                return _Rect2Communication;
            }
            set
            {
                _Rect2Communication = value;
                OnPropertyChanged("Rect2Communication");
            }
        }
        private string _Rect3Communication;
        public string Rect3Communication
        {
            get
            {
                return _Rect3Communication;
            }
            set
            {
                _Rect3Communication = value;
                OnPropertyChanged("Rect3Communication");
            }
        }
        private string _Rect4Communication;
        public string Rect4Communication
        {
            get
            {
                return _Rect4Communication;
            }
            set
            {
                _Rect4Communication = value;
                OnPropertyChanged("Rect4Communication");
            }
        }
        private string _Rect5Communication;
        public string Rect5Communication
        {
            get
            {
                return _Rect5Communication;
            }
            set
            {
                _Rect5Communication = value;
                OnPropertyChanged("Rect5Communication");
            }
        }
        private string _Rect6Communication;
        public string Rect6Communication
        {
            get
            {
                return _Rect6Communication;
            }
            set
            {
                _Rect6Communication = value;
                OnPropertyChanged("Rect6Communication");
            }
        }
        private string _Rect7Communication;
        public string Rect7Communication
        {
            get
            {
                return _Rect7Communication;
            }
            set
            {
                _Rect7Communication = value;
                OnPropertyChanged("Rect7Communication");
            }
        }
        private string _Rect8Communication;
        public string Rect8Communication
        {
            get
            {
                return _Rect8Communication;
            }
            set
            {
                _Rect8Communication = value;
                OnPropertyChanged("Rect8Communication");
            }
        }
        private string _Rect9Communication;
        public string Rect9Communication
        {
            get
            {
                return _Rect9Communication;
            }
            set
            {
                _Rect9Communication = value;
                OnPropertyChanged("Rect9Communication");
            }
        }
        private string _Rect10Communication;
        public string Rect10Communication
        {
            get
            {
                return _Rect10Communication;
            }
            set
            {
                _Rect10Communication = value;
                OnPropertyChanged("Rect10Communication");
            }
        }
        private string _Rect11Communication;
        public string Rect11Communication
        {
            get
            {
                return _Rect11Communication;
            }
            set
            {
                _Rect11Communication = value;
                OnPropertyChanged("Rect11Communication");
            }
        }
        private string _Rect12Communication;
        public string Rect12Communication
        {
            get
            {
                return _Rect12Communication;
            }
            set
            {
                _Rect12Communication = value;
                OnPropertyChanged("Rect12Communication");
            }
        }
        private string _Rect13Communication;
        public string Rect13Communication
        {
            get
            {
                return _Rect13Communication;
            }
            set
            {
                _Rect13Communication = value;
                OnPropertyChanged("Rect13Communication");
            }
        }

        private string _Device1Communication;
        public string Device1Communication
        {
            get
            {
                return _Device1Communication;
            }
            set
            {
                _Device1Communication = value;
                OnPropertyChanged("Device1Communication");
            }
        }
        private string _Device2Communication;
        public string Device2Communication
        {
            get
            {
                return _Device2Communication;
            }
            set
            {
                _Device2Communication = value;
                OnPropertyChanged("Device2Communication");
            }
        }
        private string _ScadaPcCommunication;
        public string ScadaPcCommunication
        {
            get
            {
                return _ScadaPcCommunication;
            }
            set
            {
                _ScadaPcCommunication = value;
                OnPropertyChanged("ScadaPcCommunication");
            }
        }
        private string _HMICommunication;
        public string HMICommunication
        {
            get
            {
                return _HMICommunication;
            }
            set
            {
                _HMICommunication = value;
                OnPropertyChanged("HMICommunication");
            }
        }
        private string _MainPLC1Communication;
        public string MainPLC1Communication
        {
            get
            {
                return _MainPLC1Communication;
            }
            set
            {
                _MainPLC1Communication = value;
                OnPropertyChanged("MainPLC1Communication");
            }
        }
        private string _MainPLC1_TO_ECAT1Communication;
        public string MainPLC1_TO_ECAT1Communication
        {
            get
            {
                return _MainPLC1_TO_ECAT1Communication;
            }
            set
            {
                _MainPLC1_TO_ECAT1Communication = value;
                OnPropertyChanged("MainPLC1_TO_ECAT1Communication");
            }
        }
        private string _ECAT1_TO_ECAT2Communication;
        public string ECAT1_TO_ECAT2Communication
        {
            get
            {
                return _ECAT1_TO_ECAT2Communication;
            }
            set
            {
                _ECAT1_TO_ECAT2Communication = value;
                OnPropertyChanged("ECAT1_TO_ECAT2Communication");
            }
        }  




        private string _TooltipImageName;
        public string TooltipImageName
        {
            get
            {
                return _TooltipImageName;
            }
            set
            {
                _TooltipImageName = value;
                OnPropertyChanged("TooltipImageName");
            }
        }
        private string _TooltipData;
        public string TooltipData
        {
            get
            {
                return _TooltipData;
            }
            set
            {
                _TooltipData = value;
                OnPropertyChanged("TooltipData");
            }
        }
        private ObservableCollection<OverViewEntity> _WagonPrerequisites = new ObservableCollection<OverViewEntity>();
        public ObservableCollection<OverViewEntity> WagonPrerequisites
        {
            get { return _WagonPrerequisites; }
            set { _WagonPrerequisites = value; OnPropertyChanged("WagonPrerequisites"); }
        }
        private ObservableCollection<OverViewEntity> _CTPrerequisites = new ObservableCollection<OverViewEntity>();
        public ObservableCollection<OverViewEntity> CTPrerequisites
        {
            get { return _CTPrerequisites; }
            set { _CTPrerequisites = value; OnPropertyChanged("CTPrerequisites"); }
        }
        private ObservableCollection<OverViewEntity> _UnloaderPrerequisites = new ObservableCollection<OverViewEntity>();
        public ObservableCollection<OverViewEntity> UnloaderPrerequisites
        {
            get { return _UnloaderPrerequisites; }
            set { _UnloaderPrerequisites = value; OnPropertyChanged("UnloaderPrerequisites"); }
        }
        private ObservableCollection<OverViewEntity> _DryerPrerequisites = new ObservableCollection<OverViewEntity>();
        public ObservableCollection<OverViewEntity> DryerPrerequisites
        {
            get { return _DryerPrerequisites; }
            set { _DryerPrerequisites = value; OnPropertyChanged("DryerPrerequisites"); }
        }
        private ObservableCollection<OverViewEntity> _BarrelPrerequisites = new ObservableCollection<OverViewEntity>();
        public ObservableCollection<OverViewEntity> BarrelPrerequisites
        {
            get { return _BarrelPrerequisites; }
            set { _BarrelPrerequisites = value; OnPropertyChanged("BarrelPrerequisites"); }
        }

        private ObservableCollection<RectifierEntity> _RectifierIntputs = new ObservableCollection<RectifierEntity>();
        public ObservableCollection<RectifierEntity> RectifierIntputs
        {
            get { return _RectifierIntputs; }
            set { _RectifierIntputs = value; OnPropertyChanged("RectifierIntputs"); }
        }
        private ObservableCollection<DosingSettingsEntity> _DosingIntputs = new ObservableCollection<DosingSettingsEntity>();
        public ObservableCollection<DosingSettingsEntity> DosingIntputs
        {
            get { return _DosingIntputs; }
            set { _DosingIntputs = value; OnPropertyChanged("DosingIntputs"); }
        }
        private ObservableCollection<TemperatureSettingEntity> _TemperatureIntputs = new ObservableCollection<TemperatureSettingEntity>();
        public ObservableCollection<TemperatureSettingEntity> TemperatureIntputs
        {
            get { return _TemperatureIntputs; }
            set { _TemperatureIntputs = value; OnPropertyChanged("TemperatureIntputs"); }
        }
        private ObservableCollection<pHSettingEntity> _pHIntputs = new ObservableCollection<pHSettingEntity>();
        public ObservableCollection<pHSettingEntity> pHIntputs
        {
            get { return _pHIntputs; }
            set { _pHIntputs = value; OnPropertyChanged("pHIntputs"); }
        }
        private string _PlantAM;
        public string PlantAM
        {
            get
            {
                return _PlantAM;
            }
            set
            {
                _PlantAM = value;
                OnPropertyChanged("PlantAM");
            }
        }
        private string _PlantService;
        public string PlantService
        {
            get
            {
                return _PlantService;
            }
            set
            {
                _PlantService = value;
                OnPropertyChanged("PlantService");
            }
        }
        private string _PlantCycleON;
        public string PlantCycleON
        {
            get
            {
                return _PlantCycleON;
            }
            set
            {
                _PlantCycleON = value;
                OnPropertyChanged("PlantCycleON");
            }
        }
        private string _PlantReset;
        public string PlantReset
        {
            get
            {
                return _PlantReset;
            }
            set
            {
                _PlantReset = value;
                OnPropertyChanged("PlantReset");
            }
        }
        private string _PlantPowerON;
        public string PlantPowerON
        {
            get
            {
                return _PlantPowerON;
            }
            set
            {
                _PlantPowerON = value;
                OnPropertyChanged("PlantPowerON");
            }
        }
        private string _W1position = "0";
        public string W1position
        {
            get
            {
                return _W1position;
            }
            set
            {
                _W1position = value;
                OnPropertyChanged("W1position");
                DoubleAnimation anim1 = new DoubleAnimation(Convert.ToInt32(W1position), TimeSpan.FromSeconds(5));
                W1.BeginAnimation(TranslateTransform.XProperty, anim1);
            }
        }
        private string _W1NextStep ;
        public string W1NextStep
        {
            get
            {
                return _W1NextStep;
            }
            set
            {
                _W1NextStep = value;
                OnPropertyChanged("W1NextStep");
            }
        }
        private string _W2NextStep;
        public string W2NextStep
        {
            get
            {
                return _W2NextStep;
            }
            set
            {
                _W2NextStep = value;
                OnPropertyChanged("W2NextStep");
            }
        }
        private string _W3NextStep;
        public string W3NextStep
        {
            get
            {
                return _W3NextStep;
            }
            set
            {
                _W3NextStep = value;
                OnPropertyChanged("W3NextStep");
            }
        }
        private string _W4NextStep;
        public string W4NextStep
        {
            get
            {
                return _W4NextStep;
            }
            set
            {
                _W4NextStep = value;
                OnPropertyChanged("W4NextStep");
            }
        }
        private string _W5NextStep;
        public string W5NextStep
        {
            get
            {
                return _W5NextStep;
            }
            set
            {
                _W5NextStep = value;
                OnPropertyChanged("W5NextStep");
            }
        }
        private string _W6NextStep;
        public string W6NextStep
        {
            get
            {
                return _W6NextStep;
            }
            set
            {
                _W6NextStep = value;
                OnPropertyChanged("W6NextStep");
            }
        }

        private string _W7NextStep;
        public string W7NextStep
        {
            get
            {
                return _W7NextStep;
            }
            set
            {
                _W7NextStep = value;
                OnPropertyChanged("W7NextStep");
            }
        }

        private string _W8NextStep;
        public string W8NextStep
        {
            get
            {
                return _W8NextStep;
            }
            set
            {
                _W8NextStep = value;
                OnPropertyChanged("W8NextStep");
            }
        }

        private string _W1AutoMan;
        public string W1AutoMan
        {
            get
            {
                return _W1AutoMan;
            }
            set
            {
                _W1AutoMan = value;
                OnPropertyChanged("W1AutoMan");
            }
        }
        private string _W2AutoMan;
        public string W2AutoMan
        {
            get
            {
                return _W2AutoMan;
            }
            set
            {
                _W2AutoMan = value;
                OnPropertyChanged("W2AutoMan");
            }
        }
        private string _W3AutoMan;
        public string W3AutoMan
        {
            get
            {
                return _W3AutoMan;
            }
            set
            {
                _W3AutoMan = value;
                OnPropertyChanged("W3AutoMan");
            }
        }
        private string _W4AutoMan;
        public string W4AutoMan
        {
            get
            {
                return _W4AutoMan;
            }
            set
            {
                _W4AutoMan = value;
                OnPropertyChanged("W4AutoMan");
            }
        }
        private string _W5AutoMan;
        public string W5AutoMan
        {
            get
            {
                return _W5AutoMan;
            }
            set
            {
                _W5AutoMan = value;
                OnPropertyChanged("W5AutoMan");
            }
        }
        private string _W6AutoMan;
        public string W6AutoMan
        {
            get
            {
                return _W6AutoMan;
            }
            set
            {
                _W6AutoMan = value;
                OnPropertyChanged("W6AutoMan");
            }
        }

        private string _W7AutoMan;
        public string W7AutoMan
        {
            get
            {
                return _W7AutoMan;
            }
            set
            {
                _W7AutoMan = value;
                OnPropertyChanged("W7AutoMan");
            }
        }

        private string _W8AutoMan;
        public string W8AutoMan
        {
            get
            {
                return _W8AutoMan;
            }
            set
            {
                _W8AutoMan = value;
                OnPropertyChanged("W8AutoMan");
            }
        }

        private Brush _colorr = Brushes.Red;
        public Brush W1AutoManualColor
        {
            get
            {
                return _colorr;
            }
            set
            {
                _colorr = value;
                OnPropertyChanged("W1AutoManualColor");
            }
        }
        private string  _W1XPositionFrom;
        public string W1XPositionFrom
        {
            get { return _W1XPositionFrom; }
            set { _W1XPositionFrom = value; OnPropertyChanged("W1XPositionFrom"); }
        }
        private string _W1XPositionTo;
        public string W1XPositionTo
        {
            get { return _W1XPositionTo; }
            set { _W1XPositionTo = value; OnPropertyChanged("W1XPositionTo"); }
        }
        private TranslateTransform _W1=new TranslateTransform ();
        public TranslateTransform W1
        {
            get { return _W1; }
            set { _W1 = value; OnPropertyChanged("W1"); }
        }
        private TranslateTransform _W2 = new TranslateTransform();
        public TranslateTransform W2
        {
            get { return _W2; }
            set { _W2 = value; OnPropertyChanged("W2"); }
        }
        private TranslateTransform _W3 = new TranslateTransform();
        public TranslateTransform W3
        {
            get { return _W3; }
            set { _W3 = value; OnPropertyChanged("W3"); }
        }
        private TranslateTransform _W4 = new TranslateTransform();
        public TranslateTransform W4
        {
            get { return _W4; }
            set { _W4 = value; OnPropertyChanged("W4"); }
        }

        private TranslateTransform _W5 = new TranslateTransform();
        public TranslateTransform W5
        {
            get { return _W5; }
            set { _W5 = value; OnPropertyChanged("W5"); }
        }
        private TranslateTransform _W6 = new TranslateTransform();
        public TranslateTransform W6
        {
            get { return _W6; }
            set { _W6 = value; OnPropertyChanged("W6"); }
        }
        private TranslateTransform _W7 = new TranslateTransform();
        public TranslateTransform W7
        {
            get { return _W7; }
            set { _W7 = value; OnPropertyChanged("W7"); }
        }
        private TranslateTransform _W8 = new TranslateTransform();
        public TranslateTransform W8
        {
            get { return _W8; }
            set { _W8 = value; OnPropertyChanged("W8"); }
        }

        private ObservableCollection<StationDetails> _StationsData=new ObservableCollection<StationDetails> ();
        public ObservableCollection<StationDetails> StationsData
        {
            get { return _StationsData; }
            set { _StationsData = value; OnPropertyChanged("StationsData"); }
        }
        private ObservableCollection<StationDetails> _StationsData1 = new ObservableCollection<StationDetails>();
        public ObservableCollection<StationDetails> StationsData1
        {
            get { return _StationsData1; }
            set { _StationsData1 = value; OnPropertyChanged("StationsData1"); }
        }
        private ObservableCollection<StationDetails> _StationsData2 = new ObservableCollection<StationDetails>();
        public ObservableCollection<StationDetails> StationsData2
        {
            get { return _StationsData2; }
            set { _StationsData2 = value; OnPropertyChanged("StationsData2"); }
        }
        private ObservableCollection<StationDetails> _StationsData3 = new ObservableCollection<StationDetails>();
        public ObservableCollection<StationDetails> StationsData3
        {
            get { return _StationsData3; }
            set { _StationsData3 = value; OnPropertyChanged("StationsData3"); }
        } 
        #endregion
    }

}
