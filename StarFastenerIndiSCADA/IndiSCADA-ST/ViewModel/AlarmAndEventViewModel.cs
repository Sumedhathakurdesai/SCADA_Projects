using IndiSCADAEntity.Entity;
using IndiSCADAGlobalLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace IndiSCADA_ST.ViewModel
{
   public class AlarmAndEventViewModel: BaseViewModel
    {
        #region"Disposable"
       
        #endregion
        #region"Declaration"
        DispatcherTimer DispatchTimerView = new DispatcherTimer();
        System.ComponentModel.BackgroundWorker _BackgroundWorkerView = new System.ComponentModel.BackgroundWorker();
        #endregion
        #region ICommand
        private readonly ICommand _VerifyDateTime;
        public ICommand VerifyDateTime
        {
            get { return _VerifyDateTime; }
        }
        private readonly ICommand _FilterAlarmAndEventHistoryClick;
        public ICommand FilterAlarmAndEventHistoryClick
        {
            get { return _FilterAlarmAndEventHistoryClick; }
        }
        private readonly ICommand _AckSel;
        public ICommand AckSel
        {
            get { return _AckSel; }
        }
        private readonly ICommand _AckAll;
        public ICommand AckAll
        {
            get { return _AckAll; }
        }
        private readonly ICommand _ResetSel;
        public ICommand ResetSel
        {
            get { return _ResetSel; }
        }
        private readonly ICommand _ResetAll;
        public ICommand ResetAll
        {
            get { return _ResetAll; }
        }
        private readonly ICommand _History;
        public ICommand History
        {
            get { return _History; }
        }
        private readonly ICommand _Help;
        public ICommand Help
        {
            get { return _Help; }
        }
        private readonly ICommand _Exit;//
        public ICommand Exit
        {
            get { return _Exit; }
        }
        private readonly ICommand _PopupExit;//
        public ICommand PopupExit
        {
            get { return _PopupExit; }
        }
        #endregion
        #region Public/Private Property
        private AlarmDataEntity _DGVSelectedRowData = new AlarmDataEntity();
        public AlarmDataEntity DGVSelectedRow
        {
            get { return _DGVSelectedRowData; }
            set { _DGVSelectedRowData = value; OnPropertyChanged("DGVSelectedRow"); }
        }
        private int _DGVSelectedRowindex ;
        public int DGVSelectedRowindex
        {
            get { return _DGVSelectedRowindex; }
            set { _DGVSelectedRowindex = value; OnPropertyChanged("DGVSelectedRowindex"); }
        }
        private DateTime _SelectedStartDate = DateTime.Now;
        public DateTime SelectedStartDate
        {
            get { return _SelectedStartDate; }
            set { _SelectedStartDate = value; OnPropertyChanged("SelectedStartDate"); }
        }
        private DateTime _SelectedStartTime = DateTime.Now;
        public DateTime SelectedStartTime
        {
            get { return _SelectedStartTime; }
            set { _SelectedStartTime = value; OnPropertyChanged("SelectedStartTime"); }
        }
        private DateTime _SelectedEndDate = DateTime.Now;
        public DateTime SelectedEndDate
        {
            get { return _SelectedEndDate; }
            set { _SelectedEndDate = value; OnPropertyChanged("SelectedEndDate"); }
        }
        private DateTime _SelectedEndTime = DateTime.Now;
        public DateTime SelectedEndTime
        {
            get { return _SelectedEndTime; }
            set { _SelectedEndTime = value; OnPropertyChanged("SelectedEndTime"); }
        }
        private string _AlarmHelpText ;
        public string AlarmHelpText
        {
            get { return _AlarmHelpText; }
            set { _AlarmHelpText = value; OnPropertyChanged("AlarmHelpText"); }
        }
        private bool _isOpenHelpPopup = new bool();
        public bool isOpenHelpPopup
        {
            get { return _isOpenHelpPopup; }
            set { _isOpenHelpPopup = value; OnPropertyChanged("isOpenHelpPopup"); }
        }
        private bool _isOpenHistoryPopup = new bool();
        public bool isOpenHistoryPopup
        {
            get { return _isOpenHistoryPopup; }
            set { _isOpenHistoryPopup = value; OnPropertyChanged("isOpenHistoryPopup"); }
        }
        private DataTable _AlarmEventHistory = new DataTable();
        public DataTable AlarmEventHistory
        {
            get { return _AlarmEventHistory; }
            set { _AlarmEventHistory = value; OnPropertyChanged("AlarmEventHistory"); }
        }
        private  ObservableCollection<AlarmDataEntity> AlarmCollection =new ObservableCollection<AlarmDataEntity>();
        public  ObservableCollection<AlarmDataEntity> AlarmData
        {
            get { return AlarmCollection; }
            set { AlarmCollection = value; OnPropertyChanged("AlarmData"); }
        }
        private ObservableCollection<AlarmMasterEntity> _AlarmHelp = new ObservableCollection<AlarmMasterEntity>();
        public ObservableCollection<AlarmMasterEntity> AlarmHelp
        {
            get { return _AlarmHelp; }
            set { _AlarmHelp = value; OnPropertyChanged("AlarmHelp"); }
        }
        #endregion
        ~AlarmAndEventViewModel()
        {
            try
            {
                DispatchTimerView.Stop();
            }
            catch { }
        }
        #region Constructor
        public AlarmAndEventViewModel()
        {
            


            _BackgroundWorkerView.DoWork += DoWork;
            DispatchTimerView.Interval = TimeSpan.FromSeconds(1);
            DispatchTimerView.Tick += DispatcherTickEvent;
            DispatchTimerView.Start();

            _AckAll = new RelayCommand(AckAllButtonClick);
            _AckSel = new RelayCommand(AckSelButtonClick);
            _ResetAll = new RelayCommand(ResetAllButtonClick);
            _ResetSel = new RelayCommand(ResetSelButtonClick);
            _Help = new RelayCommand(HelpButtonClick);
            _History= new RelayCommand(HistoryButtonClick);
            _Exit = new RelayCommand(ExitButtonCommandClick);
            _PopupExit = new RelayCommand(PopupExitButtonCommandClick);
            _FilterAlarmAndEventHistoryClick = new RelayCommand(FilterButtonClick);

            _VerifyDateTime = new RelayCommand(VerifyDateTimeButtonClick);

            AlarmData = IndiSCADABusinessLogic.AlarmEventLogic.GetAlarmLiveRecords();
            //AlarmDataEntity _al = new AlarmDataEntity();
            //_al.AlarmCondition = "ON";
            //_al.AlarmName = "OW1 not in lower within alarm time";
            //_al.AlarmText = "OW1 not in lower within alarm time";
            //_al.isON = true;
            //_al.AlarmDateTime = DateTime.Now ;
            //AlarmData.Add(_al);
        }

        private void PopupExitButtonCommandClick(object obj)
        {
            isOpenHistoryPopup = false;
        }
        #endregion

        #region Public/Private Method

        private void VerifyDateTimeButtonClick(object objCommandParameter)
        {
            try
            {

                IndiSCADABusinessLogic.AlarmEventLogic.VerifyDateTimeWithPLC();

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventViewModel VerifyDateTimeButtonClick()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }

        private void ExitButtonCommandClick(object _commandparameters)
        {
            try
            {
                DispatchTimerView.Stop();
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventViewModel ExitButtonCommandClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        
        private void AckSelButtonClick(object objCommandParameter)
        {
            try
            {
                if (DGVSelectedRow != null && DGVSelectedRowindex > 0)
                {
                    AlarmDataEntity _Selectedentity = DGVSelectedRow;
                    IndiSCADABusinessLogic.AlarmEventLogic.AckSelAlarm(_Selectedentity, DGVSelectedRowindex);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventViewModel AckSelButtonClick()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        private void AckAllButtonClick(object objCommandParameter)
        {
            try
            {
                if (AlarmData != null)
                {
                    IndiSCADABusinessLogic.AlarmEventLogic.AckAllAlarm(AlarmData);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventViewModel AckAllButtonClick()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        private void HistoryButtonClick(object objCommandParameter)
        {
            try
            {
                DateTime StartDate = DateTime.Now;
                DateTime EndDate = StartDate.AddDays(-7);
                ServiceResponse<DataTable> _QueryResult = IndiSCADABusinessLogic.AlarmEventLogic.GetAlarmAndEventHistory(EndDate,StartDate);
                if (_QueryResult.Status == ResponseType.S)
                {
                    if (_QueryResult.Response != null)
                    {
                        AlarmEventHistory = _QueryResult.Response;
                    }
                    else
                    {
                        AlarmEventHistory = null;
                    }
                }
                else
                {
                    AlarmEventHistory = null;
                }
                isOpenHistoryPopup = true;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventViewModel HistoryButtonClick()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        private void FilterButtonClick(object objCommandParameter)
        {
            try
            {
                DateTime StartDate = Convert.ToDateTime(SelectedStartDate.ToShortDateString() +" "+ SelectedStartTime.ToShortTimeString());
                DateTime EndDate = Convert.ToDateTime(SelectedEndDate.ToShortDateString() + " " + SelectedEndTime.ToShortTimeString());
                ServiceResponse<DataTable> _QueryResult = IndiSCADABusinessLogic.AlarmEventLogic.GetAlarmAndEventHistory(StartDate,EndDate);
                if (_QueryResult.Status == ResponseType.S)
                {
                    if (_QueryResult.Response != null)
                    {
                        AlarmEventHistory = _QueryResult.Response;
                    }
                    else
                    {
                        AlarmEventHistory = null;
                    }
                }
                else
                {
                    AlarmEventHistory = null;
                }
                //isOpenHistoryPopup = true;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventViewModel FilterButtonClick()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        private void HelpButtonClick(object objCommandParameter)
        {
            try
            {
                if (DGVSelectedRow != null)
                {
                    ServiceResponse<string> _QueryResult = IndiSCADABusinessLogic.AlarmEventLogic.GetAlarmHelpFromAlarmName(DGVSelectedRow);
                    if(_QueryResult.Status ==  ResponseType.S)
                    {
                        if (_QueryResult.Response != null)
                        {
                            AlarmHelpText = _QueryResult.Response;
                        }
                        else
                        {
                            AlarmHelpText = "";
                        }
                    }
                    else
                    {
                        AlarmHelpText = "";
                    }
                    isOpenHelpPopup = true;
                }
                
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventViewModel HelpButtonClick()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        private void ResetSelButtonClick(object objCommandParameter)
        {
            try
            {
                if (DGVSelectedRow != null && DGVSelectedRowindex != null)
                {
                    IndiSCADABusinessLogic.AlarmEventLogic.ResetSelAlarm(DGVSelectedRow, DGVSelectedRowindex);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventViewModel ResetSelButtonClick()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        private void ResetAllButtonClick(object objCommandParameter)
        {
            try
            {
                if (AlarmData != null)
                {
                    IndiSCADABusinessLogic.AlarmEventLogic.ResetAllAlarm(AlarmData);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventViewModel ResetAllButtonClick()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        private void DoWork(object Sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                App.Current.Dispatcher.BeginInvoke(new Action(() => 
                {
                    AlarmData = IndiSCADABusinessLogic.AlarmEventLogic.GetAlarmLiveRecords();
                    AlarmData = new ObservableCollection<AlarmDataEntity>(AlarmData.OrderByDescending(x => x.AlarmDateTime));
                }));
               
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventViewModel DoWork()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                ErrorLogger.LogError.ErrorLog("AlarmAndEventViewModel DispatcherTickEvent()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        
        #endregion
    }
}
