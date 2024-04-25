using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Windows.Input;
using IndiSCADAEntity.Entity;

namespace IndiSCADA_ST.ViewModel
{
    public class SystemStatusViewModel : BaseViewModel
    {
        #region"Declaration"
        DispatcherTimer DispatchTimerView = new DispatcherTimer();
        System.ComponentModel.BackgroundWorker _BackgroundWorkerView = new System.ComponentModel.BackgroundWorker();
        int ShowPopupWindow = 1;

        #endregion

        #region ICommand
        private readonly ICommand _Exit;//
        public ICommand Exit
        {
            get { return _Exit; }
        }
        #endregion

        #region"Destructor"
        ~SystemStatusViewModel()
        {
            try
            {
                DispatchTimerView.Stop();
            }
            catch { }
        }
        #endregion

        #region "Consrtuctor"
        public SystemStatusViewModel()
        {
            try
            {
                _BackgroundWorkerView.DoWork += DoWork;
                DispatchTimerView.Interval = TimeSpan.FromSeconds(1);
                DispatchTimerView.Tick += DispatcherTickEvent;
                DispatchTimerView.Start();
                _Exit = new RelayCommand(ExitButtonCommandClick);

                WagonIntputs = IndiSCADABusinessLogic.SystemStatusLogic.GetWagonInputs();
                FilterIntputs = IndiSCADABusinessLogic.SystemStatusLogic.GetFilterIntputs();
                ScrubberIntputs = IndiSCADABusinessLogic.SystemStatusLogic.GetScrubberIntputs();
                RectifierIntputs = IndiSCADABusinessLogic.SystemStatusLogic.GetRectifierIntputs();
                TemperatureIntputs = IndiSCADABusinessLogic.SystemStatusLogic.GetTemperatureIntputs();
                pHIntputs = IndiSCADABusinessLogic.SystemStatusLogic.GetpHIntputs();
                RectifierHighLowInputs = IndiSCADABusinessLogic.SystemStatusLogic.GetRectifierHighLowInputs();
                DosingIntputs = IndiSCADABusinessLogic.SystemStatusLogic.GetDosingIntputs();
                OilSkimmerIntputs = IndiSCADABusinessLogic.SystemStatusLogic.GetOilSkimmerIntputs();
                ChillerIntputs = IndiSCADABusinessLogic.SystemStatusLogic.GetChillerIntputs();
                BarrelRotationMotorIntputs = IndiSCADABusinessLogic.SystemStatusLogic.GetBarrelRotationMotorIntputs();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SystemStatusViewModel() constructor ", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        #endregion

        #region "public/private methods"
        private void ExitButtonCommandClick(object _commandparameters)
        {
            try
            {
                DispatchTimerView.Stop();
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog(" SystemStatusViewModel ExitButtonCommandClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }

        private void DoWork(object Sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                    App.Current.Dispatcher.BeginInvoke(new Action(() => { WagonIntputs = IndiSCADABusinessLogic.SystemStatusLogic.GetWagonInputs(); }));
                    App.Current.Dispatcher.BeginInvoke(new Action(() => { FilterIntputs = IndiSCADABusinessLogic.SystemStatusLogic.GetFilterIntputs(); }));
                    App.Current.Dispatcher.BeginInvoke(new Action(() => { ScrubberIntputs = IndiSCADABusinessLogic.SystemStatusLogic.GetScrubberIntputs(); }));
                    App.Current.Dispatcher.BeginInvoke(new Action(() => { RectifierIntputs = IndiSCADABusinessLogic.SystemStatusLogic.GetRectifierIntputs(); }));
                    App.Current.Dispatcher.BeginInvoke(new Action(() => { TemperatureIntputs = IndiSCADABusinessLogic.SystemStatusLogic.GetTemperatureIntputs(); }));
                    App.Current.Dispatcher.BeginInvoke(new Action(() => { pHIntputs = IndiSCADABusinessLogic.SystemStatusLogic.GetpHIntputs(); }));
                    App.Current.Dispatcher.BeginInvoke(new Action(() => { RectifierHighLowInputs = IndiSCADABusinessLogic.SystemStatusLogic.GetRectifierHighLowInputs(); }));
                    App.Current.Dispatcher.BeginInvoke(new Action(() => { DosingIntputs = IndiSCADABusinessLogic.SystemStatusLogic.GetDosingIntputs(); }));
                    App.Current.Dispatcher.BeginInvoke(new Action(() => { OilSkimmerIntputs = IndiSCADABusinessLogic.SystemStatusLogic.GetOilSkimmerIntputs(); }));
                    App.Current.Dispatcher.BeginInvoke(new Action(() => { ChillerIntputs = IndiSCADABusinessLogic.SystemStatusLogic.GetChillerIntputs(); }));
                    App.Current.Dispatcher.BeginInvoke(new Action(() => { BarrelRotationMotorIntputs = IndiSCADABusinessLogic.SystemStatusLogic.GetBarrelRotationMotorIntputs(); }));
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusViewModel DoWork1()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }

            try
            {
                if (Properties.Settings.Default.ScrollingEnable == 1) 
                {
                    if(ShowPopupWindow==1)
                    {
                        OpenPHPopup = false;
                        OpenTemperaturePopup = false;
                        OpenRectifierPopup = true;
                        //Rectifier
                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            try
                            {
                                RectifierPopUpIntputs =IndiSCADABusinessLogic.SettingLogic.GetRectifierInputs();
                            }
                            catch (Exception ex)
                            { ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork() GetRectifierInputs()", DateTime.Now.ToString(), ex.Message, "No", true); }
                        }));

                        //
                        System.Threading.Thread.Sleep(15000);
                        ShowPopupWindow = 2;
                    }
                    else if(ShowPopupWindow == 2)
                    {
                        OpenRectifierPopup = false;
                        OpenPHPopup = false;
                        OpenTemperaturePopup = true;
                        //Temperature values
                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            try
                            {
                                TemperaturePopUpIntputs = IndiSCADABusinessLogic.SettingLogic.GetTemperatureInputs();
                            }
                            catch (Exception ex)
                            { ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork() GetTemperatureInputs()", DateTime.Now.ToString(), ex.Message, "No", true); }
                        }));

                        System.Threading.Thread.Sleep(15000);
                        ShowPopupWindow = 3;
                    }
                    else if (ShowPopupWindow == 3)
                    {
                       
                        OpenRectifierPopup = false;
                        OpenTemperaturePopup = false;
                        OpenPHPopup = true;
                        //pH
                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            try
                            {
                                pHPopUpIntputs = IndiSCADABusinessLogic.SettingLogic.GetpHInputs();
                            }
                            catch (Exception ex)
                            { ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork() GetpHInputs()", DateTime.Now.ToString(), ex.Message, "No", true); }
                        }));
                        System.Threading.Thread.Sleep(15000);
                        ShowPopupWindow = 1;
                    }
                }
                else
                {
                    OpenRectifierPopup = false;
                    OpenTemperaturePopup = false;
                    OpenPHPopup = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusViewModel DoWork2()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                ErrorLogger.LogError.ErrorLog("SystemStatusViewModel DispatcherTickEvent()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        #endregion

        #region Public/Private Property
        private ObservableCollection<RectifierEntity> _RectifierPopUpIntputs = new ObservableCollection<RectifierEntity>();
        public ObservableCollection<RectifierEntity> RectifierPopUpIntputs
        {
            get { return _RectifierPopUpIntputs; }
            set { _RectifierPopUpIntputs = value; OnPropertyChanged("RectifierPopUpIntputs"); }
        }
        private ObservableCollection<TemperatureSettingEntity> _TemperaturePopUpIntputs = new ObservableCollection<TemperatureSettingEntity>();
        public ObservableCollection<TemperatureSettingEntity> TemperaturePopUpIntputs
        {
            get { return _TemperaturePopUpIntputs; }
            set { _TemperaturePopUpIntputs = value; OnPropertyChanged("TemperaturePopUpIntputs"); }
        }
        private ObservableCollection<pHSettingEntity> _pHPopUpIntputs = new ObservableCollection<pHSettingEntity>();
        public ObservableCollection<pHSettingEntity> pHPopUpIntputs
        {
            get { return _pHPopUpIntputs; }
            set { _pHPopUpIntputs = value; OnPropertyChanged("pHPopUpIntputs"); }
        }

        private bool _OpenRectifierPopup = new bool();
        public bool OpenRectifierPopup
        {
            get { return _OpenRectifierPopup; }
            set { _OpenRectifierPopup = value; OnPropertyChanged("OpenRectifierPopup"); }
        }
        private bool _OpenPHPopup = new bool();
        public bool OpenPHPopup
        {
            get { return _OpenPHPopup; }
            set { _OpenPHPopup = value; OnPropertyChanged("OpenPHPopup"); }
        }
        private bool _OpenTemperaturePopup = new bool();
        public bool OpenTemperaturePopup
        {
            get { return _OpenTemperaturePopup; }
            set { _OpenTemperaturePopup = value; OnPropertyChanged("OpenTemperaturePopup"); }
        }

        private ObservableCollection<SystemStatusEntity> _WagonIntputs = new ObservableCollection<SystemStatusEntity>();
        public ObservableCollection<SystemStatusEntity> WagonIntputs
        {
            get { return _WagonIntputs; }
            set { _WagonIntputs = value; OnPropertyChanged("WagonIntputs"); }
        }
        private ObservableCollection<SystemStatusEntity> _FilterIntputs = new ObservableCollection<SystemStatusEntity>();
        public ObservableCollection<SystemStatusEntity> FilterIntputs
        {
            get { return _FilterIntputs; }
            set { _FilterIntputs = value; OnPropertyChanged("FilterIntputs"); }
        }
        private ObservableCollection<SystemStatusEntity> _ScrubberIntputs = new ObservableCollection<SystemStatusEntity>();
        public ObservableCollection<SystemStatusEntity> ScrubberIntputs
        {
            get { return _ScrubberIntputs; }
            set { _ScrubberIntputs = value; OnPropertyChanged("ScrubberIntputs"); }
        }
        private ObservableCollection<SystemStatusEntity> _RectifierIntputs = new ObservableCollection<SystemStatusEntity>();
        public ObservableCollection<SystemStatusEntity> RectifierIntputs
        {
            get { return _RectifierIntputs; }
            set { _RectifierIntputs = value; OnPropertyChanged("RectifierIntputs"); }
        }
        private ObservableCollection<SystemStatusEntity> _TemperatureIntputs = new ObservableCollection<SystemStatusEntity>();
        public ObservableCollection<SystemStatusEntity> TemperatureIntputs
        {
            get { return _TemperatureIntputs; }
            set { _TemperatureIntputs = value; OnPropertyChanged("TemperatureIntputs"); }
        }
        private ObservableCollection<SystemStatusEntity> _pHIntputs = new ObservableCollection<SystemStatusEntity>();
        public ObservableCollection<SystemStatusEntity> pHIntputs
        {
            get { return _pHIntputs; }
            set { _pHIntputs = value; OnPropertyChanged("pHIntputs"); }
        }
        private ObservableCollection<SystemStatusEntity> _RectifierHighLowInputs = new ObservableCollection<SystemStatusEntity>();
        public ObservableCollection<SystemStatusEntity> RectifierHighLowInputs
        {
            get { return _RectifierHighLowInputs; }
            set { _RectifierHighLowInputs = value; OnPropertyChanged("RectifierHighLowInputs"); }
        }
        private ObservableCollection<SystemStatusEntity> _DosingIntputs = new ObservableCollection<SystemStatusEntity>();
        public ObservableCollection<SystemStatusEntity> DosingIntputs
        {
            get { return _DosingIntputs; }
            set { _DosingIntputs = value; OnPropertyChanged("DosingIntputs"); }
        }
        private ObservableCollection<SystemStatusEntity> _OilSkimmerIntputs = new ObservableCollection<SystemStatusEntity>();
        public ObservableCollection<SystemStatusEntity> OilSkimmerIntputs
        {
            get { return _OilSkimmerIntputs; }
            set { _OilSkimmerIntputs = value; OnPropertyChanged("OilSkimmerIntputs"); }
        }
        private ObservableCollection<SystemStatusEntity> _ChillerIntputs = new ObservableCollection<SystemStatusEntity>();
        public ObservableCollection<SystemStatusEntity> ChillerIntputs
        {
            get { return _ChillerIntputs; }
            set { _ChillerIntputs = value; OnPropertyChanged("ChillerIntputs"); }
        }
        private ObservableCollection<SystemStatusEntity> _BarrelRotationMotorIntputs = new ObservableCollection<SystemStatusEntity>();
        public ObservableCollection<SystemStatusEntity> BarrelRotationMotorIntputs
        {
            get { return _BarrelRotationMotorIntputs; }
            set { _BarrelRotationMotorIntputs = value; OnPropertyChanged("BarrelRotationMotorIntputs"); }
        }
        #endregion
    }
}
