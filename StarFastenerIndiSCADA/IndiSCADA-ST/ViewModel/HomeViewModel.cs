using IndiSCADAEntity.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading; 


namespace IndiSCADA_ST.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        #region"Declaration"
        DispatcherTimer DispatchTimerView = new DispatcherTimer();
        System.ComponentModel.BackgroundWorker _BackgroundWorkerView = new System.ComponentModel.BackgroundWorker();

        private bool _UserResponse;
        public bool UserResponse
        {
            get { return _UserResponse; }
            set { _UserResponse = value; OnPropertyChanged("UserResponse"); }
        }


        #endregion
         
        #region ICommand
        private readonly ICommand _ExitLanguageSelectionPopup;
        public ICommand ExitLanguageSelectionPopup
        {
            get { return _ExitLanguageSelectionPopup; }
        }
        private readonly ICommand _OpenLanguageSelection;
        public ICommand OpenLanguageSelection
        {
            get { return _OpenLanguageSelection; }
        }


        private readonly ICommand _ChangeLanguageClick;
        public ICommand ChangeLanguageClick
        {
            get { return _ChangeLanguageClick; }
        }
        private readonly ICommand _OpenSummeryPopup; 
        public ICommand OpenSummeryPopup
        {
            get { return _OpenSummeryPopup; }
        }
        private readonly ICommand _ExitSummeryPopup; 
        public ICommand ExitSummeryPopup
        {
            get { return _ExitSummeryPopup; }
        }
        private readonly ICommand _Exit;//
        public ICommand Exit
        {
            get { return _Exit; }
        }

        private readonly ICommand _ExitYes;//
        public ICommand ExitYes
        {
            get { return _ExitYes; }
        }

        private readonly ICommand _ExitNo;//
        public ICommand ExitNo
        {
            get { return _ExitNo; }
        }

        private readonly ICommand _WriteCycleTime;//
        public ICommand WriteCycleTime
        {
            get { return _WriteCycleTime; }
        }
        private readonly ICommand _RefreshCycleTimeSet;//
        public ICommand RefreshCycleTimeSet
        {
            get { return _RefreshCycleTimeSet; }
        }

        #endregion
    
        #region"Destructor"
        ~HomeViewModel()
        {
            try
            {
                DispatchTimerView.Stop();
            }
            catch { }
        }
        #endregion
    
        #region "Consrtuctor"
        public HomeViewModel()
        {
            try
            {

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
                    //#SCP this will enable error log for now i kept it false after designing configuration i will modify this  DESKTOP-UI951JD\SQLEXPRESS
                    //#SCP calculate shift on load event renmaining for now i kept it 1 T420-PC\FTVIEWX64TAGDB
                    IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable = true;
                    IndiSCADAGlobalLibrary.AccessConfig.Shift = 1;
                }
                catch (Exception ex)
                {
                    //  ErrorLogger.LogError.ErrorLog("HomeViewModel HomeViewModel() StartDate", DateTime.Now.ToString(), ex.Message, ex.InnerException.Message, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
                try
                {
                    IndiSCADAGlobalLibrary.AccessConfig.GetConnectionString = ConfigurationManager.ConnectionStrings["GetConnectionString"].ConnectionString;

                }
                catch (Exception ex)
                {
                   // ErrorLogger.LogError.ErrorLog("HomeViewModel HomeViewModel() GetConnectionString", DateTime.Now.ToString(), ex.Message, ex.InnerException.Message, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                    //ErrorLogger.LogError.ErrorLog("HomeViewModel HomeViewModel() DispatchTimerView", DateTime.Now.ToString(), ex.Message, ex.InnerException.Message, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
                try
                {
                    _Exit = new RelayCommand(ExitButtonCommandClick);
                    _ExitYes = new RelayCommand(ExitYesButtonCommandClick);
                    _ExitNo = new RelayCommand(ExitNoButtonCommandClick);


                    _WriteCycleTime = new RelayCommand(SetCycleTimeButtonCommandClick);
                    _RefreshCycleTimeSet = new RelayCommand(RefreshCycleTimeSetClick);
                    _ExitSummeryPopup = new RelayCommand(ExitSummeryClick);
                    _OpenSummeryPopup = new RelayCommand(OpenSummeryClick);

                    try
                    {
                        DeviceCommunication.CommunicationWithPLC.StartPlcComminicationAndDataLog();
                        DeviceCommunication.CommunicationNew.StartPlcComminicationAndDataLog();
                    }
                    catch(Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("HomeViewModel HomeViewModel() StartPlcComminicationAndDataLog()", DateTime.Now.ToString(), ex.InnerException.Message, ex.InnerException.Message, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }

                    try
                    {
                        DeviceCommunication.TrendDataLog.StartTrendDataLog();
                        DeviceCommunication.AlarmAndEventDataLog.StartAlarmAndEventTracking();
                        DeviceCommunication.PLCDateTimeSync.StartPLCDateTimeSyncTracking();
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("HomeViewModel HomeViewModel() StartTrendDataLog()", DateTime.Now.ToString(), ex.InnerException.Message, ex.InnerException.Message, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }

                    try
                    {
                        IndiSCADAGlobalLibrary.AccessConfig.GetMySQLConnectionString = ConfigurationManager.ConnectionStrings["GetMySQLConnectionString"].ConnectionString;
                    }
                    catch (Exception ex)
                    {
                        // log.Error("HomeViewModel HomeViewModel() GetConnectionString", DateTime.Now.ToString(), ex.Message, ex.InnerException.Message, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }

                    #region start IOT
                    try
                    {
                        DeviceCommunication.IOTConnection _IOTobj = new DeviceCommunication.IOTConnection(); 
                    }
                    catch (Exception ex)
                    {
                      //  log.Error("HomeViewModel HomeViewModel() GetConnectionString" + ex.Message + " " + ex.InnerException.Message);
                    }
                    #endregion


                    PlantAM = IndiSCADABusinessLogic.HomeBusinessLogic.GetPlantStatus("AM");
                    PlantCycle = IndiSCADABusinessLogic.HomeBusinessLogic.GetPlantStatus("CycleON");
                    W1AM = IndiSCADABusinessLogic.HomeBusinessLogic.GetWagonStatus(1);
                    W2AM = IndiSCADABusinessLogic.HomeBusinessLogic.GetWagonStatus(2);
                    W3AM = IndiSCADABusinessLogic.HomeBusinessLogic.GetWagonStatus(3);
                    W4AM = IndiSCADABusinessLogic.HomeBusinessLogic.GetWagonStatus(4);

                    W5AM = IndiSCADABusinessLogic.HomeBusinessLogic.GetWagonStatus(5);
                    W6AM = IndiSCADABusinessLogic.HomeBusinessLogic.GetWagonStatus(6);
                    W7AM = IndiSCADABusinessLogic.HomeBusinessLogic.GetWagonStatus(7);

                    IndiSCADAGlobalLibrary.TagList.ProductionDetails.ShiftNum = IndiSCADAGlobalLibrary.AccessConfig.Shift.ToString();//added by sas
                    IndiSCADAGlobalLibrary.TagList.ProductionDetails.ShiftNo = IndiSCADAGlobalLibrary.AccessConfig.Shift.ToString();//added by sas

                }
                catch (Exception ex)
                {
                   ErrorLogger.LogError.ErrorLog("HomeViewModel HomeViewModel() GetWagonStatus", DateTime.Now.ToString(), ex.Message, ex.InnerException.Message, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }


                #region Previous and current Downtime Shift from plc logic,     
                //  App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    try
                    {           //for testing
                                //string[] RealShiftWiseDownTime = { "124254", "54542", "45453", "45424", "22225" };
                                //string[] PreviousShiftWiseDownTime = { "12", "13", "14", "15", "16" };
                                //IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime = RealShiftWiseDownTime;
                                //IndiSCADAGlobalLibrary.TagList.PreviousShiftWiseDownTime = PreviousShiftWiseDownTime;



                        //fetch downtime summary from database LiveDowntimeShiftSummary

                        IndiSCADAGlobalLibrary.TagList.ProductionDetails.UpdationDateTime = DateTime.Now.ToString();


                        int AutoTotalDownTime = 0; int SemiTotalDownTime = 0; int ManualTotalDownTime = 0; int MaintainanceTotalDownTime = 0; int CompleteTotalDownTime = 0;
                        LiveDowntimeShiftSummary = IndiSCADABusinessLogic.HomeBusinessLogic.GetPreviousDowntimeShiftSummary();
                        //log.Error("HomeViewModel DoWork() LiveDowntimeShiftSummary count" + LiveDowntimeShiftSummary.Count());

                        if (LiveDowntimeShiftSummary != null)
                        {
                            foreach (var item in LiveDowntimeShiftSummary)
                            {

                                //log.Error("HomeViewModel DoWork() LiveDowntimeShiftSummary foreachloop" + LiveDowntimeShiftSummary.Count());

                                //previous
                                if (Convert.ToInt32(item.ShiftNo) == IndiSCADAGlobalLibrary.AccessConfig.Shift)
                                {
                                    IndiSCADAGlobalLibrary.TagList.ProductionDetails.ShiftNo = item.ShiftNo.ToString();
                                    IndiSCADAGlobalLibrary.TagList.ProductionDetails.AutoPreviousDowntimeShift = item.DownTime.ToString();
                                    IndiSCADAGlobalLibrary.TagList.ProductionDetails.SemiPreviousDowntimeShift = item.SemiDownTime.ToString();    //Used To Service
                                    IndiSCADAGlobalLibrary.TagList.ProductionDetails.ManualPreviousDowntimeShift = item.ManualDownTime.ToString();
                                    IndiSCADAGlobalLibrary.TagList.ProductionDetails.MaintenancePreviousDowntimeShift = item.MaintenanceDownTime.ToString();
                                    IndiSCADAGlobalLibrary.TagList.ProductionDetails.CompletePreviousDowntimeShift = item.CompleteDownTime.ToString();
                                }

                                //total
                                if (item.DownTime.ToString() != "")
                                {
                                    AutoTotalDownTime = AutoTotalDownTime + Convert.ToInt32(item.DownTime);
                                }
                                if (item.SemiDownTime.ToString() != "")
                                {
                                    SemiTotalDownTime = SemiTotalDownTime + Convert.ToInt32(item.SemiDownTime);
                                }
                                if (item.ManualDownTime.ToString() != "")
                                {
                                    ManualTotalDownTime = ManualTotalDownTime + Convert.ToInt32(item.ManualDownTime);
                                }
                                if (item.MaintenanceDownTime.ToString() != "")
                                {
                                    MaintainanceTotalDownTime = MaintainanceTotalDownTime + Convert.ToInt32(item.MaintenanceDownTime);
                                }
                                if (item.CompleteDownTime.ToString() != "")
                                {
                                    CompleteTotalDownTime = CompleteTotalDownTime + Convert.ToInt32(item.CompleteDownTime);
                                }
                            }


                            IndiSCADAGlobalLibrary.TagList.ProductionDetails.AutoTotalDownTime = AutoTotalDownTime + Convert.ToInt32(IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime[0]);
                            IndiSCADAGlobalLibrary.TagList.ProductionDetails.SemiTotalDownTime = SemiTotalDownTime + Convert.ToInt32(IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime[1]); ;
                            IndiSCADAGlobalLibrary.TagList.ProductionDetails.ManualTotalDownTime = ManualTotalDownTime + Convert.ToInt32(IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime[2]); ;
                            IndiSCADAGlobalLibrary.TagList.ProductionDetails.MaintenanceTotalDownTime = MaintainanceTotalDownTime + Convert.ToInt32(IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime[3]); ;
                            IndiSCADAGlobalLibrary.TagList.ProductionDetails.CompleteTotalDownTime = CompleteTotalDownTime + Convert.ToInt32(IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime[4]); ;

                            TotalDowntimeShiftSummary = new ObservableCollection<HomeEntity>();
                            HomeEntity _HomeEntityName = new HomeEntity();
                            _HomeEntityName.AutoTotalDownTime = AutoTotalDownTime;
                            _HomeEntityName.SemiTotalDownTime = SemiTotalDownTime;
                            _HomeEntityName.ManualTotalDownTime = ManualTotalDownTime;
                            _HomeEntityName.MaintenanceTotalDownTime = MaintainanceTotalDownTime;
                            _HomeEntityName.CompleteTotalDownTime = CompleteTotalDownTime;
                            TotalDowntimeShiftSummary.Add(_HomeEntityName);
                        }

                        //live
                        IndiSCADAGlobalLibrary.TagList.ProductionDetails.AutoLiveDowntimeShift = IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime[0];
                        IndiSCADAGlobalLibrary.TagList.ProductionDetails.SemiLiveDowntimeShift = IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime[1];
                        IndiSCADAGlobalLibrary.TagList.ProductionDetails.ManualLiveDowntimeShift = IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime[2];
                        IndiSCADAGlobalLibrary.TagList.ProductionDetails.MaintainanceLiveDowntimeShift = IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime[3];
                        IndiSCADAGlobalLibrary.TagList.ProductionDetails.CompleteLiveDowntimeShift = IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime[4];


                    }
                    catch (Exception ex)
                    {
                        //log.Error("HomeViewModel DoWork() Previous and current Downtime Shift()" + ex.Message);
                    }

                }//));
                #endregion


                //language 
                try
                {
                    _ExitLanguageSelectionPopup= new RelayCommand(ExitLanguageSelectionClick);
                    _ChangeLanguageClick = new RelayCommand(ChangeLanguagClick);
                    _OpenLanguageSelection = new RelayCommand(OpenLanguageSelectionClick);
                    LanguageList = new ObservableCollection<string>();
                    LanguageList.Add("English");
                    LanguageList.Add("हिंदी");
                    LanguageList.Add("मराठी");

                    string Language = IndiSCADABusinessLogic.HomeBusinessLogic.GetSelectedLanguage();
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Language);
                }
                catch (Exception ex)
                {
                    //  ErrorLogger.LogError.ErrorLog("HomeViewModel HomeViewModel() StartDate", DateTime.Now.ToString(), ex.Message, ex.InnerException.Message, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }

                //Shift Setting change check
                try
                {
                    int result = IndiSCADABusinessLogic.HomeBusinessLogic.CheckShiftSettingData();
                    if (result > 0)
                    {
                        IndiSCADAGlobalLibrary.TagList.IsShiftSettingChanged = true;
                    }

                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError.ErrorLog("HomeViewModel HomeViewModel() Check Shift Setting Data", DateTime.Now.ToString(), ex.Message, ex.InnerException.Message, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeViewModel HomeViewModel()", DateTime.Now.ToString(), ex.Message, ex.InnerException.Message , IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        #endregion

        #region"property"

        #region shiftwise area
        private string _Shift1Area;
        public string Shift1Area
        {
            get { return _Shift1Area; }
            set { _Shift1Area = value; OnPropertyChanged("Shift1Area"); }
        }
        private string _Shift2Area;
        public string Shift2Area
        {
            get { return _Shift2Area; }
            set { _Shift2Area = value; OnPropertyChanged("Shift2Area"); }
        }
        private string _Shift3Area;
        public string Shift3Area
        {
            get { return _Shift3Area; }
            set { _Shift3Area = value; OnPropertyChanged("Shift3Area"); }
        }
        #endregion

        #region down time
        private ObservableCollection<HomeEntity> _LiveDowntimeShiftSummary = new ObservableCollection<HomeEntity>();
        public ObservableCollection<HomeEntity> LiveDowntimeShiftSummary
        {
            get { return _LiveDowntimeShiftSummary; }
            set { _LiveDowntimeShiftSummary = value; OnPropertyChanged("LiveDowntimeShiftSummary"); }
        }

        private ObservableCollection<HomeEntity> _TotalDowntimeShiftSummary = new ObservableCollection<HomeEntity>();
        public ObservableCollection<HomeEntity> TotalDowntimeShiftSummary
        {
            get { return _TotalDowntimeShiftSummary; }
            set { _TotalDowntimeShiftSummary = value; OnPropertyChanged("TotalDowntimeShiftSummary"); }
        }

        private ObservableCollection<HomeEntity> _PreviousDowntimeShiftSummary = new ObservableCollection<HomeEntity>();
        public ObservableCollection<HomeEntity> PreviousDowntimeShiftSummary
        {
            get { return _PreviousDowntimeShiftSummary; }
            set { _PreviousDowntimeShiftSummary = value; OnPropertyChanged("PreviousDowntimeShiftSummary"); }
        }
        #endregion

        private bool _IsOpenLangugePopup;
        public bool IsOpenLangugePopup
        {
            get
            {
                return _IsOpenLangugePopup;
            }
            set
            {
                _IsOpenLangugePopup = value;
                OnPropertyChanged("IsOpenLangugePopup");
            }
        }
        private string _SelectedLang;
        public string SelectedLang
        {
            get
            {
                return _SelectedLang;
            }
            set
            {
                _SelectedLang = value;
                OnPropertyChanged("SelectedLang");
            }
        }
        private ObservableCollection<string> _LanguageList;
        public ObservableCollection<string> LanguageList
        {
            get { return _LanguageList; }
            set { _LanguageList = value; OnPropertyChanged("LanguageList"); }
        }
  

        private string _DisplaySummary;
        public string DisplaySummary
        {
            get
            {
                return _DisplaySummary;
            }
            set
            {
                _DisplaySummary = value;
                OnPropertyChanged("DisplaySummary");
            }
        }
        private string _SummaryName;
        public string SummaryName
        {
            get
            {
                return _SummaryName;
            }
            set
            {
                _SummaryName = value;
                OnPropertyChanged("SummaryName");
            }
        }
        private DataTable _SummaryPopupData = new DataTable();
        public DataTable SummaryPopupData
        {
            get { return _SummaryPopupData; }
            set { _SummaryPopupData = value; OnPropertyChanged("SummaryPopupData"); }
        }
        private bool _IsOpenSummeryPopup;
        public bool IsOpenSummeryPopup
        {
            get
            {
                return _IsOpenSummeryPopup;
            }
            set
            {
                _IsOpenSummeryPopup = value;
                OnPropertyChanged("IsOpenSummeryPopup");
            }
        }

        private string _HDEProcess;
        public string HDEProcess
        {
            get
            {
                return _HDEProcess;
            }
            set
            {
                _HDEProcess = value;
                OnPropertyChanged("HDEProcess");
            }
        }
        private bool _PlantCommunicationStatus;
        public bool PlantCommunicationStatus
        {
            get
            {
                return _PlantCommunicationStatus;
            }
            set
            {
                _PlantCommunicationStatus = value;
                OnPropertyChanged("PlantCommunicationStatus");
            }
        }

        private string _ActualCycleTime;
        public string ActualCycleTime
        {
            get
            {
                return _ActualCycleTime;
            }
            set
            {
                _ActualCycleTime = value;
                OnPropertyChanged("ActualCycleTime");
            }
        }
        private string _SetCycleTime;
        public string SetCycleTime
        {
            get
            {
                return _SetCycleTime;
            }
            set
            {
                _SetCycleTime = value;
                OnPropertyChanged("SetCycleTime");
            }
        }
        private string _LastCycleTime;
        public string LastCycleTime
        {
            get
            {
                return _LastCycleTime;
            }
            set
            {
                _LastCycleTime = value;
                OnPropertyChanged("LastCycleTime");
            }
        }
        private string _AvgCycleTime;
        public string AvgCycleTime
        {
            get
            {
                return _AvgCycleTime;
            }
            set
            {
                _AvgCycleTime = value;
                OnPropertyChanged("AvgCycleTime");
            }
        }
        private bool _SetCycleTimeReadOnly;
        public bool SetCycleTimeReadOnly
        {
            get
            {
                return _SetCycleTimeReadOnly;
            }
            set
            {
                _SetCycleTimeReadOnly = value;
                OnPropertyChanged("SetCycleTimeReadOnly");

            }
        }
        private bool _isCycleTimeEdit=true;
        public bool isCycleTimeEdit
        {
            
            get
            {
                return _isCycleTimeEdit;
            }
            set
            {
                _isCycleTimeEdit = value;
                OnPropertyChanged("isCycleTimeEdit");

            }
        }
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; OnPropertyChanged("UserName"); }
        }
        private string _AlarmCount;
        public string AlarmCount
        {
            get { return _AlarmCount; }
            set { _AlarmCount = value; OnPropertyChanged("AlarmCount"); }
        }

        private string _AlarmNACKCount;
        public string AlarmNACKCount
        {
            get { return _AlarmNACKCount; }
            set { _AlarmNACKCount = value; OnPropertyChanged("AlarmNACKCount"); }
        }

        private string _AlarmONCount;
        public string AlarmONCount
        {
            get { return _AlarmONCount; }
            set { _AlarmONCount = value; OnPropertyChanged("AlarmONCount"); }
        }
        private string _TotalQuantity;
        public string TotalQuantity
        {
            get { return _TotalQuantity; }
            set { _TotalQuantity = value; OnPropertyChanged("TotalQuantity"); }
        }

        private string _Shift1Quantity;
        public string Shift1Quantity
        {
            get { return _Shift1Quantity; }
            set { _Shift1Quantity = value; OnPropertyChanged("Shift1Quantity"); }
        }

        private string _Shift2Quantity;
        public string Shift2Quantity
        {
            get { return _Shift2Quantity; }
            set { _Shift2Quantity = value; OnPropertyChanged("Shift2Quantity"); }
        }

        private string _Shift3Quantity;
        public string Shift3Quantity
        {
            get { return _Shift3Quantity; }
            set { _Shift3Quantity = value; OnPropertyChanged("Shift3Quantity"); }
        }

        private string _Shift1LoadCount;
        public string Shift1LoadCount
        {
            get { return _Shift1LoadCount; }
            set { _Shift1LoadCount = value; OnPropertyChanged("Shift1LoadCount"); }
        }
        private string _Shift2LoadCount;
        public string Shift2LoadCount
        {
            get { return _Shift2LoadCount; }
            set { _Shift2LoadCount = value; OnPropertyChanged("Shift2LoadCount"); }
        }
        private string _Shift3LoadCount;
        public string Shift3LoadCount
        {
            get { return _Shift3LoadCount; }
            set { _Shift3LoadCount = value; OnPropertyChanged("Shift3LoadCount"); }
        }
        private string _Shift1Name;
        public string Shift1Name
        {
            get { return _Shift1Name; }
            set { _Shift1Name = value; OnPropertyChanged("Shift1Name"); }
        }

        private string _Shift2Name;
        public string Shift2Name
        {
            get { return _Shift2Name; }
            set { _Shift2Name = value; OnPropertyChanged("Shift2Name"); }
        }
        private string _Shift3Name;
        public string Shift3Name
        {
            get { return _Shift3Name; }
            set { _Shift3Name = value; OnPropertyChanged("Shift3Name"); }
        }

        private string _TotalLoads;
        public string TotalLoads
        {
            get { return _TotalLoads; }
            set { _TotalLoads = value; OnPropertyChanged("TotalLoads"); }
        }

        private string _LoadCount;
        public string LoadCount
        {
            get { return _LoadCount; }
            set { _LoadCount = value; OnPropertyChanged("LoadCount"); }
        }

        private string _ProductionGraphLabel;
        public string ProductionGraphLabel
        {
            get { return _ProductionGraphLabel; }
            set { _ProductionGraphLabel = value; OnPropertyChanged("ProductionGraphLabel"); }
        }

        private string _AlarmGraphLabel;
        public string AlarmGraphLabel
        {
            get { return _AlarmGraphLabel; }
            set { _AlarmGraphLabel = value; OnPropertyChanged("AlarmGraphLabel"); }
        }

        private string _ChemicalGraphLabel;
        public string ChemicalGraphLabel
        {
            get { return _ChemicalGraphLabel; }
            set { _ChemicalGraphLabel = value; OnPropertyChanged("ChemicalGraphLabel"); }
        }
        private string _CurrentGraphLabel;
        public string CurrentGraphLabel
        {
            get { return _CurrentGraphLabel; }
            set { _CurrentGraphLabel = value; OnPropertyChanged("CurrentGraphLabel"); }
        }
        
        private ObservableCollection<HomeEntity> _AlarmSummaryGraph = new ObservableCollection<HomeEntity>();
        public ObservableCollection<HomeEntity> AlarmSummaryGraph
        {
            get { return _AlarmSummaryGraph; }
            set { _AlarmSummaryGraph = value; OnPropertyChanged("AlarmSummaryGraph"); }
        }
        private ObservableCollection<HomeEntity> _PartAreaSummary = new ObservableCollection<HomeEntity>();
        public ObservableCollection<HomeEntity> PartAreaSummary
        {
            get { return _PartAreaSummary; }
            set { _PartAreaSummary = value; OnPropertyChanged("PartAreaSummary"); }
        }
        private ObservableCollection<HomeEntity> _PartNameSummary = new ObservableCollection<HomeEntity>();
        public ObservableCollection<HomeEntity> PartNameSummary
        {
            get { return _PartNameSummary; }
            set { _PartNameSummary = value; OnPropertyChanged("PartNameSummary"); }
        }
        private ObservableCollection<HomeEntity> _ChemicalConsumptionSummary = new ObservableCollection<HomeEntity>();
        public ObservableCollection<HomeEntity> ChemicalConsumptionSummary
        {
            get { return _ChemicalConsumptionSummary; }
            set { _ChemicalConsumptionSummary = value; OnPropertyChanged("ChemicalConsumptionSummary"); }
        }
        private ObservableCollection<HomeEntity> _CurrentConsumptionSummary = new ObservableCollection<HomeEntity>();
        public ObservableCollection<HomeEntity> CurrentConsumptionSummary
        {
            get { return _CurrentConsumptionSummary; }
            set { _CurrentConsumptionSummary = value; OnPropertyChanged("CurrentConsumptionSummary"); }
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
        private string _PlantCycle;
        public string PlantCycle
        {
            get
            {
                return _PlantCycle;
            }
            set
            {
                _PlantCycle = value;
                OnPropertyChanged("PlantCycle");
            }
        }

        private string _W1AM;
        public string W1AM
        {
            get
            {
                return _W1AM;
            }
            set
            {
                _W1AM = value;
                OnPropertyChanged("W1AM");
            }
        }

        private string _W2AM;
        public string W2AM
        {
            get
            {
                return _W2AM;
            }
            set
            {
                _W2AM = value;
                OnPropertyChanged("W2AM");
            }
        }

        private string _W3AM;
        public string W3AM
        {
            get
            {
                return _W3AM;
            }
            set
            {
                _W3AM = value;
                OnPropertyChanged("W3AM");
            }
        }

        private string _W4AM;
        public string W4AM
        {
            get
            {
                return _W4AM;
            }
            set
            {
                _W4AM = value;
                OnPropertyChanged("W4AM");
            }
        }

        private string _W5AM;
        public string W5AM
        {
            get
            {
                return _W5AM;
            }
            set
            {
                _W5AM = value;
                OnPropertyChanged("W5AM");
            }
        }

        private string _W6AM;
        public string W6AM
        {
            get
            {
                return _W6AM;
            }
            set
            {
                _W6AM = value;
                OnPropertyChanged("W6AM");
            }
        }
        private string _W7AM;
        public string W7AM
        {
            get
            {
                return _W7AM;
            }
            set
            {
                _W7AM = value;
                OnPropertyChanged("W7AM");
            }
        }
        private string _W8AM;
        public string W8AM
        {
            get
            {
                return _W8AM;
            }
            set
            {
                _W8AM = value;
                OnPropertyChanged("W8AM");
            }
        }

        
        private ObservableCollection<TankDetailsEntity> _ZincPlatingliveSummary = new ObservableCollection<TankDetailsEntity>();
        public ObservableCollection<TankDetailsEntity> ZincPlatingliveSummary
        {
            get { return _ZincPlatingliveSummary; }
            set { _ZincPlatingliveSummary = value; OnPropertyChanged("ZincPlatingliveSummary"); }
        }
        #endregion

        #region "public/private methods"

        #region summery popup
        private void OpenSummeryClick(object _commandparameters)
        {
            try
            {
                DisplaySummary = _commandparameters.ToString();
                SummaryName = DisplaySummary;
                if (DisplaySummary == "Alarm Summary")
                {
                    SummaryPopupData = IndiSCADABusinessLogic.HomeBusinessLogic.GetSummaryData(DisplaySummary);
                }
                else if (DisplaySummary == "Chemical Summary")
                {
                    SummaryPopupData = IndiSCADABusinessLogic.HomeBusinessLogic.GetSummaryData(DisplaySummary);
                }
                else if (DisplaySummary == "Rectifier Summary")
                {
                    SummaryPopupData = IndiSCADABusinessLogic.HomeBusinessLogic.GetSummaryData(DisplaySummary);
                }
                
                else if (DisplaySummary == "Part Summary")
                {
                    SummaryPopupData = IndiSCADABusinessLogic.HomeBusinessLogic.GetSummaryData(DisplaySummary);
                }
                else if (DisplaySummary == "Live Alarm Summary")
                {
                    SummaryPopupData = IndiSCADABusinessLogic.HomeBusinessLogic.GetSummaryData(DisplaySummary);
                }
                else if (DisplaySummary == "Acknoledge Alarm Summary")
                {
                    SummaryPopupData = IndiSCADABusinessLogic.HomeBusinessLogic.GetSummaryData(DisplaySummary);
                }
                
                else if (DisplaySummary == "Wagon1 Alarm Summary")
                {
                    SummaryPopupData = IndiSCADABusinessLogic.HomeBusinessLogic.GetSummaryData(DisplaySummary);
                }
                else if (DisplaySummary == "Wagon2 Alarm Summary")
                {
                    SummaryPopupData = IndiSCADABusinessLogic.HomeBusinessLogic.GetSummaryData(DisplaySummary);
                }
                else if (DisplaySummary == "Wagon3 Alarm Summary")
                {
                    SummaryPopupData = IndiSCADABusinessLogic.HomeBusinessLogic.GetSummaryData(DisplaySummary);
                }
                else if (DisplaySummary == "Wagon4 Alarm Summary")
                {
                    SummaryPopupData = IndiSCADABusinessLogic.HomeBusinessLogic.GetSummaryData(DisplaySummary);
                }
                else if (DisplaySummary == "Wagon5 Alarm Summary")
                {
                    SummaryPopupData = IndiSCADABusinessLogic.HomeBusinessLogic.GetSummaryData(DisplaySummary);
                }
                else if (DisplaySummary == "Wagon6 Alarm Summary")
                {
                    SummaryPopupData = IndiSCADABusinessLogic.HomeBusinessLogic.GetSummaryData(DisplaySummary);
                }
                else if (DisplaySummary == "Wagon7 Alarm Summary")
                {
                    SummaryPopupData = IndiSCADABusinessLogic.HomeBusinessLogic.GetSummaryData(DisplaySummary);
                }
                else if (DisplaySummary == "Wagon8 Alarm Summary")
                {
                    SummaryPopupData = IndiSCADABusinessLogic.HomeBusinessLogic.GetSummaryData(DisplaySummary);
                }
                


               IsOpenSummeryPopup = true;
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("HomeViewModel OpenSummeryClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void ExitSummeryClick(object _commandparameters)
        {
            try
            {
                IsOpenSummeryPopup = false; 
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("HomeViewModel ExitSummeryClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        #endregion

        private void OpenLanguageSelectionClick(object _commandparameters)
        {
            try
            {
                IsOpenLangugePopup = true;
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("HomeViewModel OpenLanguageSelectionClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void ExitLanguageSelectionClick(object _commandparameters)
        {
            try
            {
                IsOpenLangugePopup = false;
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("HomeViewModel ExitLanguageSelectionClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }        
        private void ChangeLanguagClick(object _commandparameters)
        {
            try
            {
                IsOpenLangugePopup = false;
                String Selectedlang = _commandparameters.ToString();

                if (_commandparameters.ToString() == "English")
                {
                     Selectedlang = "en";
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                }
                else if (_commandparameters.ToString() == "हिंदी")
                {
                    Selectedlang = "hi-IN";
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("hi-IN");
                }
                else if (_commandparameters.ToString() == "मराठी")
                {
                    Selectedlang = "mr-IN";
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("mr-IN");
                }

                if (MessageBox.Show("Applicaton restart required to change language. Do you want to change language?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    bool LanguageSelectionSuccessful = IndiSCADABusinessLogic.HomeBusinessLogic.UpdateSelectedLanguage(Selectedlang);

                    MessageBox.Show("Please restart Application.");
                    //System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                    //Application.Current.Shutdown();
                }
                else
                { 
                }

            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("HomeViewModel ChangeLanguagClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        

        private void RefreshCycleTimeSetClick(object _commandparameters)
        {
            try
            {
                string username = IndiSCADAGlobalLibrary.UserLoginDetails.UserName; // commented for testing purpose
                string useraccesslevel = IndiSCADAGlobalLibrary.UserLoginDetails.AccessLevel;

                if (username != null)
                {
                    if (useraccesslevel == "Admin" || useraccesslevel == "Supervisor")
                    {
                        isCycleTimeEdit = false;
                    }
                    else
                    {
                        isCycleTimeEdit = true;
                    }
                }
                else
                {
                    //LoginView loginView = new LoginView();
                    //loginView.ShowDialog();
                    isCycleTimeEdit = true;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeViewModel RefreshCycleTimeSetClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        private void SetCycleTimeButtonCommandClick(object _commandparameters)
        {
            try
            {
                string username = IndiSCADAGlobalLibrary.UserLoginDetails.UserName; // commented for testing purpose
                string useraccesslevel = IndiSCADAGlobalLibrary.UserLoginDetails.AccessLevel;

                if (username != null)
                {
                    if (useraccesslevel== "Admin" || useraccesslevel== "Supervisor")
                    {
                        isCycleTimeEdit = false;

                        try
                        {
                            if (Convert.ToInt32(_SetCycleTime.ToString()) >= 0 && Convert.ToInt32(_SetCycleTime.ToString()) <=9999)
                            {
                                IndiSCADABusinessLogic.SettingLogic.WriteValueToPLC("CYCLETIMESETCYCLETIME", 0, SetCycleTime);
                            }
                            else
                            {
                                MessageBox.Show("Range for cycle time between 0 to 9999");
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("HomeViewModel SetCycleTimeButtonCommandClick()", DateTime.Now.ToString(), ex.Message, "No", true);
                           
                        }
                        
                    }
                    else
                    {
                    }
                }
                else
                {
                    //LoginView loginView = new LoginView();
                    //loginView.ShowDialog();
                }
                //  DispatchTimerView.Stop();
            }
            catch (Exception SetCycleTimeButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("HomeViewModel SetCycleTimeButtonCommandClick()", DateTime.Now.ToString(), SetCycleTimeButtonCommandClick.Message, "No", true);

            }
            isCycleTimeEdit = true;
        }

        private void ExitButtonCommandClick(object _commandparameters)
        {
            try
            {
                UserResponse = true;
              //  DispatchTimerView.Stop();
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("HomeViewModel ExitButtonCommandClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }

        private void ExitYesButtonCommandClick(object _commandparameters)
        {
            try
            {

                DispatchTimerView.Stop();
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("HomeViewModel ExitYesButtonCommandClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }

        private void ExitNoButtonCommandClick(object _commandparameters)
        {
            try
            {
                UserResponse = false;
               //  DispatchTimerView.Stop();
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("HomeViewModel ExitNoButtonCommandClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }



        private void DoWork(object Sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {

                //Chemical Consumption details Summary and Total quantity
              //  App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        CurrentConsumptionSummary = IndiSCADABusinessLogic.HomeBusinessLogic.GetCurrentConsumptionSummary();
                        IndiSCADAGlobalLibrary.TagList.CurrentConsumptionSummaryIOT = CurrentConsumptionSummary;
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork() ChemicalConsumptionSummary()", DateTime.Now.ToString(), ex.Message, "No", true);
                    }

                }//));

                //Chemical Consumption details Summary and Total quantity
               // App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {
                    ChemicalConsumptionSummary = IndiSCADABusinessLogic.HomeBusinessLogic.GetChemicalConsumptionSummary();
                    IndiSCADAGlobalLibrary.TagList.ChemicalConsumptionSummaryIOT = ChemicalConsumptionSummary;
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork() ChemicalConsumptionSummary()", DateTime.Now.ToString(), ex.Message, "No", true);
                }

            }//));

                DateTime StartDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "08:00:00");
                DateTime EndDate = StartDate.AddDays(1);

                string username = IndiSCADAGlobalLibrary.UserLoginDetails.UserName;
                string useraccesslevel = IndiSCADAGlobalLibrary.UserLoginDetails.AccessLevel;

                //    //HDE process
                //    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                //    {
                //        try
                //        {
                //            HDEProcess = IndiSCADABusinessLogic.HomeBusinessLogic.GetHDEProcess();
                //        }
                //        catch (Exception ex)
                //        {
                //             ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork() GetHDEProcess()", DateTime.Now.ToString(), ex.Message, "No", true);
                //        }
                //    }));

                //communication status
               // App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        if (IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected == true)
                        {
                            PlantCommunicationStatus = true;
                        }
                        else
                        {
                            PlantCommunicationStatus = false;
                        }

                        IndiSCADAGlobalLibrary.TagList.DateTimeDetails.PlantStatus = PlantCommunicationStatus.ToString();
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork() while checking PLC Connection", DateTime.Now.ToString(), ex.Message, "No", true);
                    }

                }//));

                //set cycle time
                //App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        if (username != null)
                        {
                            if (useraccesslevel == "Admin" || useraccesslevel == "Supervisor")
                            {
                                SetCycleTimeReadOnly = false;
                            }
                            else
                            {
                                SetCycleTimeReadOnly = true;
                            }
                        }
                        else
                        {
                            SetCycleTimeReadOnly = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork() while user access level", DateTime.Now.ToString(), ex.Message, "No", true);
                    }

                }//));

                //to write cycle time               
               // App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            if (isCycleTimeEdit == true)//do not refresh text box while editing value
                            {
                                SetCycleTime = IndiSCADABusinessLogic.HomeBusinessLogic.GetSetCycleTime();
                                IndiSCADAGlobalLibrary.TagList.ProductionDetails.setCycleTime = SetCycleTime;
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork() GetSetCycleTime()", DateTime.Now.ToString(), ex.Message, "No", true);
                        }
                    }//));

                // production summary Graph labels
                try
                {
                    ProductionGraphLabel = StartDate.ToShortDateString();
                    AlarmGraphLabel =    StartDate.ToShortDateString();
                    ChemicalGraphLabel =  StartDate.ToShortDateString();
                    CurrentGraphLabel =  StartDate.ToShortDateString();
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork()  Graph labels", DateTime.Now.ToString(), ex.Message, "No", true);
                }

                //alarm Summary
                try
                {
                    //AlarmSummaryGraph
                   // App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        AlarmSummaryGraph = IndiSCADABusinessLogic.HomeBusinessLogic.GetAlarmSummary();
                        IndiSCADAGlobalLibrary.TagList.AlarmSummaryGraphIOT = AlarmSummaryGraph;
                    }//));
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork() GetAlarmSummary()", DateTime.Now.ToString(), ex.Message, "No", true);
                }

                //cycle time
                try
                {

                  //  App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {

                            UserName = IndiSCADAGlobalLibrary.UserLoginDetails.UserName;
                            ActualCycleTime = IndiSCADABusinessLogic.HomeBusinessLogic.GetActualCycleTime();
                                //SetCycleTime = IndiSCADABusinessLogic.HomeBusinessLogic.GetSetCycleTime(); //changed by sbs
                                LastCycleTime = IndiSCADABusinessLogic.HomeBusinessLogic.GetLastCycleTime(); // commented as address not available in ICD
                                AvgCycleTime = IndiSCADABusinessLogic.HomeBusinessLogic.GetAvgCycleTime();

                            IndiSCADAGlobalLibrary.TagList.ProductionDetails.ActualCycleTime = ActualCycleTime;
                            IndiSCADAGlobalLibrary.TagList.ProductionDetails.LastCycleTime = LastCycleTime;
                            IndiSCADAGlobalLibrary.TagList.ProductionDetails.AvgCycleTime = AvgCycleTime;

                        }//));
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork() GetActualCycleTime()", DateTime.Now.ToString(), ex.Message, "No", true);
                }

                //alarm count
                try
                {
                 //   App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        HomeEntity _Data = IndiSCADABusinessLogic.HomeBusinessLogic.GetAlarmTotalCount();

                        if (_Data != null)
                        {
                            AlarmONCount = _Data.AlarmONCount.ToString();
                            AlarmNACKCount = _Data.AlarmNotACKCount.ToString();

                            IndiSCADAGlobalLibrary.TagList.ProductionDetails.ONAlarmCount = AlarmONCount;
                            IndiSCADAGlobalLibrary.TagList.ProductionDetails.NotACKAlarmCount = AlarmNACKCount;
                        }
                    }//));
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork() GetAlarmTotalCount()", DateTime.Now.ToString(), ex.Message, "No", true);
                }


                // Plant and wagon status
                try
                {
                   // App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        PlantAM = IndiSCADABusinessLogic.HomeBusinessLogic.GetPlantStatus("AM");
                        PlantCycle = IndiSCADABusinessLogic.HomeBusinessLogic.GetPlantStatus("CycleON");
                        W1AM = IndiSCADABusinessLogic.HomeBusinessLogic.GetWagonStatus(1);
                        W2AM = IndiSCADABusinessLogic.HomeBusinessLogic.GetWagonStatus(2);
                        W3AM = IndiSCADABusinessLogic.HomeBusinessLogic.GetWagonStatus(3);
                        W4AM = IndiSCADABusinessLogic.HomeBusinessLogic.GetWagonStatus(4);
                        W5AM = IndiSCADABusinessLogic.HomeBusinessLogic.GetWagonStatus(5);
                        W6AM = IndiSCADABusinessLogic.HomeBusinessLogic.GetWagonStatus(6);
                        W7AM = IndiSCADABusinessLogic.HomeBusinessLogic.GetWagonStatus(7);
                        //W8AM = IndiSCADABusinessLogic.HomeBusinessLogic.GetWagonStatus(8);

                        IndiSCADAGlobalLibrary.TagList.ProductionDetails.W1AM = W1AM;
                        IndiSCADAGlobalLibrary.TagList.ProductionDetails.W2AM = W2AM;
                        IndiSCADAGlobalLibrary.TagList.ProductionDetails.W3AM = W3AM;
                        IndiSCADAGlobalLibrary.TagList.ProductionDetails.W4AM = W4AM;
                        IndiSCADAGlobalLibrary.TagList.ProductionDetails.W5AM = W5AM;
                        IndiSCADAGlobalLibrary.TagList.ProductionDetails.W6AM = W6AM;
                        IndiSCADAGlobalLibrary.TagList.ProductionDetails.W7AM = W7AM;
                        //IndiSCADAGlobalLibrary.TagList.ProductionDetails.W8AM = W8AM;
                        IndiSCADAGlobalLibrary.TagList.ProductionDetails.PlantAM = PlantAM;
                        IndiSCADAGlobalLibrary.TagList.ProductionDetails.CycleStartStop = PlantCycle;
                    }//));
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork() GetPlantStatus() ,GetWagonStatus()", DateTime.Now.ToString(), ex.Message, "No", true);
                }

                //Part details Summary
               // App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    PartNameSummary = IndiSCADABusinessLogic.HomeBusinessLogic.GetPartNameSummary();
                    IndiSCADAGlobalLibrary.TagList.PartNameSummaryIOT = PartNameSummary;


                    //string[] animals = { "Cat", "Alligator", "fox", "donkey", "Cat", "alligator" };
                    try
                    {
                        if (IndiSCADAGlobalLibrary.TagList.LoadNoAtStation != null)
                        {
                            if (IndiSCADAGlobalLibrary.TagList.LoadNoAtStation.Length > 0)
                            {
                                //var totalloads = IndiSCADAGlobalLibrary.TagList.LoadNoAtStation.Count(s => s != "0");
                                int max_loadno = 0;
                                foreach (string str in IndiSCADAGlobalLibrary.TagList.LoadNoAtStation)
                                {
                                    try
                                    {
                                        int loadno = Convert.ToInt32(str);
                                        if (loadno > max_loadno)
                                        {
                                            max_loadno = loadno;
                                        }
                                    }
                                    catch { }
                                }

                                TotalLoads = max_loadno.ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork() GetPartNameSummary()", DateTime.Now.ToString(), ex.Message, "No", true);
                    }
                }//));

                {
                    try
                    {
                        PartAreaSummary = IndiSCADABusinessLogic.HomeBusinessLogic.GetPartAreaSummary(true);
                      //  ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork() GetPartAreaSummary()" + PartAreaSummary.Count, DateTime.Now.ToString(), "", "No", true);
                        IndiSCADAGlobalLibrary.TagList.PartAreaSummaryIOT = PartAreaSummary;
                    }
                    catch (Exception ex)
                    { ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork() GetPartAreaSummary() Error", DateTime.Now.ToString(), ex.Message, "No", true); }
                }
                //toatl quntity
                // App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        ObservableCollection<HomeEntity> _DataQuantity = IndiSCADABusinessLogic.HomeBusinessLogic.GetTotalQuantity();

                        if (_DataQuantity != null)
                        {
                            if (_DataQuantity.Count > 0)
                            {
                                TotalQuantity = _DataQuantity[0].TotalQuantityCount.ToString();
                            }
                            else
                            {
                                TotalQuantity = "0";
                            }
                        }
                    }
                    catch (Exception ex)
                    { }
                }//));

                //shift wise 
               // App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            try
                            {
                                ObservableCollection<HomeEntity> _DataLoads = IndiSCADABusinessLogic.HomeBusinessLogic.GetTotalLoads();

                        if (_DataLoads != null)
                        {
                            if (_DataLoads.Count > 0)
                            {
                                LoadCount = _DataLoads[0].TotalLoadCount.ToString();

                            }
                            else
                            {
                                //LoadCount = "0";
                            }
                        }

                        #region Shiftwise Quantity
                        ObservableCollection<HomeEntity> _Shift1Quantity = IndiSCADABusinessLogic.HomeBusinessLogic.GetShiftQuantity(1);

                        if (_Shift1Quantity != null)
                        {
                            if (_Shift1Quantity.Count > 0)
                            {
                                Shift1Quantity = _Shift1Quantity[0].Shift1QuantityCount.ToString();
                            }
                            else
                            {
                                Shift1Quantity = "0";
                            }
                            Shift1Name = "Shift A";
                        }

                                ObservableCollection<HomeEntity> _Shift2Quantity = IndiSCADABusinessLogic.HomeBusinessLogic.GetShiftQuantity(2);
                        if (_Shift2Quantity != null)
                        {
                            if (_Shift2Quantity.Count > 0)
                            {
                                Shift2Quantity = _Shift2Quantity[0].Shift2QuantityCount.ToString();
                            }
                            else
                            {
                                Shift2Quantity = "0";
                            }
                            Shift2Name = "Shift B";
                        }

                                ObservableCollection<HomeEntity> _Shift3Quantity = IndiSCADABusinessLogic.HomeBusinessLogic.GetShiftQuantity(3);
                        if (_Shift3Quantity != null)
                        {
                            if (_Shift3Quantity.Count > 0)
                            {
                                Shift3Quantity = _Shift3Quantity[0].Shift3QuantityCount.ToString();

                            }
                            else
                            {
                                Shift3Quantity = "0";
                            }
                            Shift3Name = "Shift C";
                        }
                        try
                        {
                            IndiSCADAGlobalLibrary.TagList.ProductionDetails.Shift1QuantityCount = Convert.ToInt32(Shift1Quantity);
                            IndiSCADAGlobalLibrary.TagList.ProductionDetails.Shift2QuantityCount = Convert.ToInt32(Shift2Quantity);
                            IndiSCADAGlobalLibrary.TagList.ProductionDetails.Shift3QuantityCount = Convert.ToInt32(Shift3Quantity);
                            IndiSCADAGlobalLibrary.TagList.ProductionDetails.TotalLoadCount = Convert.ToInt32(LoadCount);
                        }
                        catch
                        {

                        }
                        #endregion

                        #region shiftwise loadcound

                        ObservableCollection<HomeEntity> LoadCountShift1 = IndiSCADABusinessLogic.HomeBusinessLogic.GetLoadCount(1);
                                if (LoadCountShift1 != null)
                                {
                                    if (LoadCountShift1.Count > 0)
                                    {
                                        Shift1LoadCount = LoadCountShift1[0].Shift1LoadCount.ToString();
                                    }
                                    else
                                    {
                                        Shift1LoadCount = "";
                                    }
                                }
                                ObservableCollection<HomeEntity> LoadCountShift2 = IndiSCADABusinessLogic.HomeBusinessLogic.GetLoadCount(2);
                                if (LoadCountShift2 != null)
                                {
                                    if (LoadCountShift2.Count > 0)
                                    {
                                        Shift2LoadCount = LoadCountShift2[0].Shift2LoadCount.ToString();
                                    }
                                    else
                                    {
                                        Shift2LoadCount = "";
                                    }
                                }
                                ObservableCollection<HomeEntity> LoadCountShift3 = IndiSCADABusinessLogic.HomeBusinessLogic.GetLoadCount(3);
                                if (LoadCountShift3 != null)
                                {
                                    if (LoadCountShift3.Count > 0)
                                    {
                                        Shift3LoadCount = LoadCountShift3[0].Shift3LoadCount.ToString();
                                    }
                                    else
                                    {
                                        Shift3LoadCount = "";
                                    }
                                }
                                try
                                {
                                    IndiSCADAGlobalLibrary.TagList.ProductionDetails.Shift1LoadCount = Convert.ToInt32(Shift1LoadCount);
                                    IndiSCADAGlobalLibrary.TagList.ProductionDetails.Shift2LoadCount = Convert.ToInt32(Shift2LoadCount);
                                    IndiSCADAGlobalLibrary.TagList.ProductionDetails.Shift3LoadCount = Convert.ToInt32(Shift3LoadCount);
                                }
                                catch
                                {

                                }
                        #endregion

                        #region Shift wise part area
                        try
                        {
                            ObservableCollection<HomeEntity> AreaShift1 = IndiSCADABusinessLogic.HomeBusinessLogic.GetShiftWisePartArea(1, true);
                         //   ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork() GetShiftWisePartArea()" + AreaShift1.Count, DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                            if (AreaShift1 != null)
                            {
                                if (AreaShift1.Count > 0)
                                {
                                    Shift1Area = AreaShift1[0].Shift1PartArea.ToString();
                                  //  ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork() GetShiftWisePartArea()" + Shift1Area, DateTime.Now.ToString(),"", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                                }
                                else
                                {
                                    Shift1Area = "";
                                }
                            }
                            ObservableCollection<HomeEntity> AreaShift2 = IndiSCADABusinessLogic.HomeBusinessLogic.GetShiftWisePartArea(2, true);
                            if (AreaShift2 != null)
                            {
                                if (AreaShift2.Count > 0)
                                {
                                    Shift2Area = AreaShift2[0].Shift1PartArea.ToString();
                                 //   ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork() GetShiftWisePartArea()" + Shift2Area, DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                                }
                                else
                                {
                                    Shift2Area = "";
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            ObservableCollection<HomeEntity> AreaShift3 = IndiSCADABusinessLogic.HomeBusinessLogic.GetShiftWisePartArea(3, true);
                            if (AreaShift3 != null)
                            {
                                if (AreaShift3.Count > 0)
                                {
                                    Shift3Area = AreaShift3[0].Shift1PartArea.ToString();
                                   // ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork() GetShiftWisePartArea()" + Shift2Area, DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                                }
                                else
                                {
                                    Shift3Area = "";
                                }
                            }
                        }catch { }
                        try
                        {
                            IndiSCADAGlobalLibrary.TagList.ProductionDetails.Shift1PartArea = Convert.ToInt64(Shift1Area);
                            IndiSCADAGlobalLibrary.TagList.ProductionDetails.Shift2PartArea = Convert.ToInt64(Shift2Area);
                            //   IndiSCADAGlobalLibrary.TagList.ProductionDetails.Shift3PartArea = Convert.ToInt32(Shift3Area);
                            IndiSCADAGlobalLibrary.TagList.ProductionDetails.PartArea = Convert.ToInt64(Shift2Area) + Convert.ToInt64(Shift1Area);
                         //   ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork() All PartArea()" + IndiSCADAGlobalLibrary.TagList.ProductionDetails.PartArea, DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                        }
                        catch
                        {

                        }
                        #endregion

                        #region zinc plating live value reading
                        try
                        {
                            ZincPlatingliveSummary = IndiSCADABusinessLogic.HomeBusinessLogic.GetLiveZincPlatingSummary();
                            IndiSCADAGlobalLibrary.TagList.ZincPlatingLine = ZincPlatingliveSummary;
                        }
                        catch (Exception ex)
                        {
                            //log.Error("HomeViewModel DoWork() Zinc Plating live values" + ex.Message);
                        }
                        #endregion

                        #region poka yoke live value reading
                        try
                        {
                            #region New
                            //string[] DosingOutput =new string[] { "1", "1", "1", "1", "1", "0", "0", "0", "0", "0"  }; 
                            //string[] RectifierAlarm = new string[] { "1", "1", "1", "1", "0", "0", "0", "0", "0", "0", "0", "1", "1", "1", "1" }; 
                            string[] Livepokayoke = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("LivePokaYokeZincPlatingLine");
                           // ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork() poka yoke values reading" + Livepokayoke.Length, DateTime.Now.ToString(), "", "No", true);
                            //string[] Temp_pokayoke = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("TempPokaYoke");
                            //string[] pH_pokayoke = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("pHPokaYoke");
                            var listPokaYoke = new List<string>();
                            listPokaYoke.AddRange(Livepokayoke);
                            //listPokaYoke.AddRange(Temp_pokayoke);
                            //listPokaYoke.AddRange(pH_pokayoke);
                            IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine = listPokaYoke.ToArray();

                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Degreasing_1_Rectifier = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[0];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Anodic_2_Rectifier = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[1];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Al_Zn_1_Rectifier = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[2];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Al_Zn_2_Rectifier = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[3];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Al_Zn_3_Rectifier = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[4];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Al_Zn_4_Rectifier = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[5];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Al_Zn_5_Rectifier = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[6];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Al_Zn_6_Rectifier = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[7];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Al_Zn_7_Rectifier = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[8];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Al_Zn_8_Rectifier = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[9];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Al_Zn_9_Rectifier = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[10];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Al_Zn_10_Rectifier = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[11];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Al_Zn_11_Rectifier = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[12];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Al_Zn_12_Rectifier = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[13];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Al_Zn_13_Rectifier = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[14];

                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Hot_soak_Cleaning_1_Temp = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[15];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Hot_soak_Cleaning_2_Temp = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[16];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Anodic_Cleaning_1_Temp = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[17];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Anodic_Cleaning_2_Temp = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[18];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Akaline_Zinc_1_Temp = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[19];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Akaline_Zinc_2_Temp = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[20];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Akaline_Zinc_3_Temp = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[21];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Akaline_Zinc_4_Temp = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[22];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Akaline_Zinc_5_Temp = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[23];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Passivation_1_Temp = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[24];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Passivation_2_Temp = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[25];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Top_Coat_1_Temp = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[26];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Top_Coat_2_Temp = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[27];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Basket_Wash_Temp = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[28];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Dryer_1_Temp = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[29];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Dryer_2_Temp = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[30];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.ZINC_GEN_1_Temp = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[31];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.ZINC_GEN_2_Temp = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[32];


                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Nitric_pH = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[33];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Passivation_1_pH = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[34];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Passivation_2_pH = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[35];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.OilSkimmer_2_Motor1 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[0];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Oil_skimmer_2_Pump = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[1];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.ScrubberPump = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[2];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlkalineZinc_21_24 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[3];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlkalineZinc_25_28 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[4];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlkalineZinc_29_34 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[5];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlkalineZinc_35_38 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[6];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Passivation_2 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[7];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.TopCoat_1 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[8];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.TopCoat_2 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[9];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.TopCoat_3 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[10];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlkalineZinc_21_28 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[11];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlkalineZinc_21_28 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[12];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlkalineZinc_21_28 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[13];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlkalineZinc_21_28 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[14];

                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlkalineZinc_29_34 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[15];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlkalineZinc_29_34 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[16];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlkalineZinc_29_34 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[17];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlkalineZinc_29_34 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[18];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlkalineZinc_35_38 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[19];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlkalineZinc_35_38 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[20];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlkalineZinc_35_38 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[21];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlkalineZinc_35_38 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[22];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Nitric_Stn_44 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[23];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Passivation_2_Stn_51 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[24];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Passivation_2_Stn_51 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[25];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Passivation_2_Stn_51 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[26];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.TopCoat_2_Stn_64 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[27];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.TopCoat_2_Stn_64 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[28];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.TopCoat_1_Stn_63 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[29];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.TopCoat_1_Stn_63 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[30];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.TopCoat_3_Stn_65 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[31];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.TopCoat_3_Stn_65 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[32];


                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Anodic_1 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[33];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Anodic_2 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[34];

                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Anodic_3 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[35];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_1 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[36];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_2 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[37];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_3 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[38];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_4 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[39];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_5 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[40];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_6 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[41];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_7 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[42];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_8 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[43];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_9 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[44];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_10 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[45];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_11 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[46];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_12 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[47];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_13 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[48];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_14 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[49];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_15 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[50];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_16 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[51];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_17 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[52];
                            //IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_18 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[53];


                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Oil_skimmer_2_Motor_P1 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[0];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Oil_skimmer_2_Pump_P2 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[1];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.ScrubberPump_P3 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[2];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.FilterPump_6_P4 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[3];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.FilterPump_7_P5 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[4];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.FilterPump_8_P6 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[5];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.FilterPump_9_P7 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[6];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.FilterPump_13_P8 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[7];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.FilterPump_15_P9 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[8];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.FilterPump_16_P10 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[9];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.FilterPump_17_P11 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[10];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.DosingPump_1_P12 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[11];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.DosingPump_P13 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[12];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.DosingPump_3_P14 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[13];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.DosingPump_4_P15 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[14];

                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.DosingPump_5_P16 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[15];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.DosingPump_6_P17 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[16];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.DosingPump_7_P18 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[17];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.DosingPump_8_P19 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[18];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.DosingPump_9_P20 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[19];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.DosingPump_10_P21 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[20];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.DosingPump_11_P22 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[21];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.DosingPump_12_P23 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[22];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.DosingPump_13_P24 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[23];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.DosingPump_17_P25 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[24];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.DosingPump_18_P26 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[25];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.DosingPump_19_P27 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[26];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.DosingPump_20_P28 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[27];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.DosingPump_21_P29 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[28];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.DosingPump_22_P30 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[29];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.DosingPump_23_P31 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[30];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.DosingPump_24_P32 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[31];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.DosingPump_25_P33 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[32];


                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Anodic_1_P34 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[33];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Anodic_2_P35 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[34];

                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.Anodic_3_P36 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[35];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_1_P37 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[36];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_2_P38 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[37];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_3_P39 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[38];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_4_P40 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[39];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_5_P41 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[40];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_6_P42 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[41];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_7_P43 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[42];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_8_P44 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[43];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_9_P45 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[44];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_10_P46 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[45];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_11_P47 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[46];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_12_P48 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[47];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_13_P49 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[48];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_14_P50 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[49];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_15_P51 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[50];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_16_P52 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[51];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_17_P53 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[52];
                            IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine.AlZn_18_P54 = IndiSCADAGlobalLibrary.TagList.LivePokaYokeZincPlatingLine[53];

                            #endregion
                        }
                        catch (Exception ex)
                        {
                          //  log.Error("HomeViewModel DoWork() poka yoke Zinc Plating live values" + ex.Message + "No");
                        }
                        #endregion

                        #region Previous and current Downtime Shift from plc logic,     
                        //  App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            try
                            {           //for testing
                                        //string[] RealShiftWiseDownTime = { "124254", "54542", "45453", "45424", "22225" };
                                        //string[] PreviousShiftWiseDownTime = { "12", "13", "14", "15", "16" };
                                        //IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime = RealShiftWiseDownTime;
                                        //IndiSCADAGlobalLibrary.TagList.PreviousShiftWiseDownTime = PreviousShiftWiseDownTime;



                                //fetch downtime summary from database LiveDowntimeShiftSummary

                                IndiSCADAGlobalLibrary.TagList.ProductionDetails.UpdationDateTime = DateTime.Now.ToString();


                                int AutoTotalDownTime = 0; int SemiTotalDownTime = 0; int ManualTotalDownTime = 0; int MaintainanceTotalDownTime = 0; int CompleteTotalDownTime = 0;
                                LiveDowntimeShiftSummary = IndiSCADABusinessLogic.HomeBusinessLogic.GetPreviousDowntimeShiftSummary();
                                //log.Error("HomeViewModel DoWork() LiveDowntimeShiftSummary count" + LiveDowntimeShiftSummary.Count());

                                if (LiveDowntimeShiftSummary != null)
                                {
                                    foreach (var item in LiveDowntimeShiftSummary)
                                    {

                                        //log.Error("HomeViewModel DoWork() LiveDowntimeShiftSummary foreachloop" + LiveDowntimeShiftSummary.Count());

                                        //previous
                                        if (Convert.ToInt32(item.ShiftNo) == IndiSCADAGlobalLibrary.AccessConfig.Shift)
                                        {
                                            IndiSCADAGlobalLibrary.TagList.ProductionDetails.ShiftNo = item.ShiftNo.ToString();
                                            IndiSCADAGlobalLibrary.TagList.ProductionDetails.AutoPreviousDowntimeShift = item.DownTime.ToString();
                                            IndiSCADAGlobalLibrary.TagList.ProductionDetails.SemiPreviousDowntimeShift = item.SemiDownTime.ToString();    //Used To Service
                                            IndiSCADAGlobalLibrary.TagList.ProductionDetails.ManualPreviousDowntimeShift = item.ManualDownTime.ToString();
                                            IndiSCADAGlobalLibrary.TagList.ProductionDetails.MaintenancePreviousDowntimeShift = item.MaintenanceDownTime.ToString();
                                            IndiSCADAGlobalLibrary.TagList.ProductionDetails.CompletePreviousDowntimeShift = item.CompleteDownTime.ToString();
                                        }

                                        //total
                                        if (item.DownTime.ToString() != "")
                                        {
                                            AutoTotalDownTime = AutoTotalDownTime + Convert.ToInt32(item.DownTime);
                                        }
                                        if (item.SemiDownTime.ToString() != "")
                                        {
                                            SemiTotalDownTime = SemiTotalDownTime + Convert.ToInt32(item.SemiDownTime);
                                        }
                                        if (item.ManualDownTime.ToString() != "")
                                        {
                                            ManualTotalDownTime = ManualTotalDownTime + Convert.ToInt32(item.ManualDownTime);
                                        }
                                        if (item.MaintenanceDownTime.ToString() != "")
                                        {
                                            MaintainanceTotalDownTime = MaintainanceTotalDownTime + Convert.ToInt32(item.MaintenanceDownTime);
                                        }
                                        if (item.CompleteDownTime.ToString() != "")
                                        {
                                            CompleteTotalDownTime = CompleteTotalDownTime + Convert.ToInt32(item.CompleteDownTime);
                                        }
                                    }


                                    IndiSCADAGlobalLibrary.TagList.ProductionDetails.AutoTotalDownTime = AutoTotalDownTime + Convert.ToInt32(IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime[0]);
                                    IndiSCADAGlobalLibrary.TagList.ProductionDetails.SemiTotalDownTime = SemiTotalDownTime + Convert.ToInt32(IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime[1]); ;
                                    IndiSCADAGlobalLibrary.TagList.ProductionDetails.ManualTotalDownTime = ManualTotalDownTime + Convert.ToInt32(IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime[2]); ;
                                    IndiSCADAGlobalLibrary.TagList.ProductionDetails.MaintenanceTotalDownTime = MaintainanceTotalDownTime + Convert.ToInt32(IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime[3]); ;
                                    IndiSCADAGlobalLibrary.TagList.ProductionDetails.CompleteTotalDownTime = CompleteTotalDownTime + Convert.ToInt32(IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime[4]); ;

                                    TotalDowntimeShiftSummary = new ObservableCollection<HomeEntity>();
                                    HomeEntity _HomeEntityName = new HomeEntity();
                                    _HomeEntityName.AutoTotalDownTime = AutoTotalDownTime;
                                    _HomeEntityName.SemiTotalDownTime = SemiTotalDownTime;
                                    _HomeEntityName.ManualTotalDownTime = ManualTotalDownTime;
                                    _HomeEntityName.MaintenanceTotalDownTime = MaintainanceTotalDownTime;
                                    _HomeEntityName.CompleteTotalDownTime = CompleteTotalDownTime;
                                    TotalDowntimeShiftSummary.Add(_HomeEntityName);
                                }

                                //live
                                IndiSCADAGlobalLibrary.TagList.ProductionDetails.AutoLiveDowntimeShift = IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime[0];
                                IndiSCADAGlobalLibrary.TagList.ProductionDetails.SemiLiveDowntimeShift = IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime[1];
                                IndiSCADAGlobalLibrary.TagList.ProductionDetails.ManualLiveDowntimeShift = IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime[2];
                                IndiSCADAGlobalLibrary.TagList.ProductionDetails.MaintainanceLiveDowntimeShift = IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime[3];
                                IndiSCADAGlobalLibrary.TagList.ProductionDetails.CompleteLiveDowntimeShift = IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime[4];


                            }
                            catch (Exception ex)
                            {
                                //log.Error("HomeViewModel DoWork() Previous and current Downtime Shift()" + ex.Message);
                            }

                        }//));
                        #endregion

                        #region send chemical concentration data
                        //try
                        //{
                        //    DataTable ChemConcentrationDT = IndiSCADABusinessLogic.SettingLogic.GetChemConcentration();
                        //    IndiSCADAGlobalLibrary.TagList.ParameterConcentrationData.ZincConcentration = ChemConcentrationDT.Rows[0]["Zinc"].ToString();//IndiSCADAGlobalLibrary.TagList.ConcentrationValue[0];
                        //    IndiSCADAGlobalLibrary.TagList.ParameterConcentrationData.CausticSodaConcentration = ChemConcentrationDT.Rows[0]["CausticSoda"].ToString();//IndiSCADAGlobalLibrary.TagList.ConcentrationValue[1];
                        //    log.Error("HomeViewModel DoWork() ZincParameterval", DateTime.Now.ToString(), ChemConcentrationDT.Rows[0]["Zinc"].ToString(), "No", true);
                        //    log.Error("HomeViewModel DoWork() CausticSodaParameterval", DateTime.Now.ToString(), ChemConcentrationDT.Rows[0]["CausticSoda"].ToString(), "No", true);
                        //}
                        //catch (Exception ex)
                        //{
                        //    log.Error("HomeViewModel DoWork() chemical concentration values", DateTime.Now.ToString(), ex.Message, "No", true);
                        //}
                        #endregion

                        #region passivation
                        try
                        {
                            //IndiSCADAGlobalLibrary.TagList.PassivationData.NitricDip = IndiSCADABusinessLogic.HomeBusinessLogic.GetPassivationSilverSummary("NitricDipLoadCount");
                            //IndiSCADAGlobalLibrary.TagList.PassivationData.Passivation1 = IndiSCADABusinessLogic.HomeBusinessLogic.GetPassivationSilverSummary("Passivation1LoadCount");
                            //IndiSCADAGlobalLibrary.TagList.PassivationData.Passivation2 = IndiSCADABusinessLogic.HomeBusinessLogic.GetPassivationSilverSummary("Passivation2LoadCount");
                            //IndiSCADAGlobalLibrary.TagList.PassivationData.PassivationZincIron = IndiSCADABusinessLogic.HomeBusinessLogic.GetPassivationSilverSummary("PassZincIronLoadCount");
                        }
                        catch (Exception ex)
                        {
                         //   log.Error("HomeViewModel DoWork() passivation summary values" + ex.Message + "No");
                        }
                        #endregion

                        #region  ERP data
                        //try
                        //{
                        //    IndiSCADAGlobalLibrary.TagList.ERPData.ERPInwardQuantity = "160";
                        //    IndiSCADAGlobalLibrary.TagList.ERPData.ERPRunningQuantity = "50";
                        //    IndiSCADAGlobalLibrary.TagList.ERPData.ERPCompletedQuantity = "10";
                        //    IndiSCADAGlobalLibrary.TagList.ERPData.ERPBalancedQuantity = "100";
                        //}
                        //catch (Exception ex)
                        //{
                        //    log.Error("HomeViewModel DoWork() ERP values"+ ex.Message+ "No");
                        //}
                        #endregion

                        #region  H2D process data
                        //try
                        //{
                        //    H2DPartSummary = IndiSCADABusinessLogic.HomeBusinessLogic.GetH2DPartSummary();
                        //    IndiSCADAGlobalLibrary.TagList.H2DData = H2DPartSummary;
                        //}
                        //catch (Exception ex)
                        //{
                        //    log.Error("HomeViewModel DoWork() H2D Process"+ ex.Message, "No");
                        //}
                        #endregion
                    }
                    catch (Exception ex)
                            { }
                        }//));


                // Shift Calculation
                try
                {
                    string[] ShiftFromDateTODate = new string[] { };
                    ShiftFromDateTODate = this.ShiftCalculation();
                    if (ShiftFromDateTODate.Length != 0 && ShiftFromDateTODate != null)
                    {
                        IndiSCADAGlobalLibrary.AccessConfig.Shift = Convert.ToInt32(ShiftFromDateTODate[0]);
                    }
                    else
                    {
                        IndiSCADAGlobalLibrary.AccessConfig.Shift = 1;
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork() ShiftCalculation()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }


                IndiSCADAGlobalLibrary.TagList.DateTimeDetails.UpdationDateTime = DateTime.Now.ToString();

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                ErrorLogger.LogError.ErrorLog("HomeViewModel DispatcherTickEvent()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }

        public string[] ShiftCalculation()
        {
            try
            {
                #region "Old logic"
                //int h, m, s, nowSec;
                //DateTime SD, ED, tempED, midD, tempmidD, fromDate, toDate;
                //string Shift = "";
                //int savedH, savedM, savedS, savedTotalSecFirst, savedTotalSecSecond, savedTotalSecThird;
                //DateTime curFirstShiftTime;
                //DateTime plcDateTime;
                //string tt = "";
                //string d1String = DateTime.Now.ToString();

                //fromDate = DateTime.Now;
                //toDate = DateTime.Now;

                //string plcDT = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year + "-" + DateTime.Now.ToLongTimeString();
                //d1String = DateTime.Now.ToString();
                //plcDateTime = DateTime.Parse(d1String);

                //h = plcDateTime.Hour * (60 * 60);
                //m = plcDateTime.Minute * 60;
                //s = plcDateTime.Second;
                //nowSec = (h + m + s);

                //if (IndiSCADA_ST.Properties.Settings.Default.FirstShiftTime != string.Empty)
                //{ curFirstShiftTime = Convert.ToDateTime(IndiSCADA_ST.Properties.Settings.Default.FirstShiftTime); }
                //else
                //{ curFirstShiftTime = DateTime.Parse(plcDateTime.Day + "/" + plcDateTime.Month + "/" + plcDateTime.Year + "07:00:00 AM"); }
                //savedH = curFirstShiftTime.Hour * (60 * 60);
                //savedM = curFirstShiftTime.Minute * 60;
                //savedS = curFirstShiftTime.Second;

                ////added by sbs for 1 shift from 8morning ,2nd from 8 evening
                //savedTotalSecFirst = savedH + savedM + savedS;
                //savedTotalSecSecond = savedTotalSecFirst + 43200; // 28800+43200=72000= means 8pm
                ////savedTotalSecThird = savedTotalSecSecond + 10600;

                //if (((nowSec >= savedTotalSecFirst) && (nowSec < savedTotalSecSecond)))
                //{
                //    Shift = "1";
                //}
                //else if (nowSec >= savedTotalSecSecond)  // && (nowSec >= savedTotalSecSecond) && (nowSec < savedTotalSecThird)
                //{
                //    Shift = "2";
                //}
                ////else if (nowSec >= savedTotalSecThird)
                ////{
                ////    Shift = "3";
                ////} 

                //string[] ShiftFromDateTODate = new string[] { Shift, fromDate.ToString(), toDate.ToString() };
                //return ShiftFromDateTODate;

                #endregion


                #region "New Logic"

                try
                {
                    string[] shiftnumber = new string[1];

                    shiftnumber[0] = IndiSCADABusinessLogic.HomeBusinessLogic.GetShiftNumber();

                    return shiftnumber;

                }

                catch (Exception ex)
                {
                    ErrorLogger.LogError.ErrorLog("HomeViewModel ShiftCalculation()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    string[] aa = new string[] { "", "", "" };
                    return aa;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeViewModel ShiftCalculation()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                string[] aa = new string[] { "", "", "" };
                return aa;
            }


        }
        #endregion
    }
}
