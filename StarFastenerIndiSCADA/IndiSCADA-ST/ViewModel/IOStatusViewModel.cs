using IndiSCADAEntity.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace IndiSCADA_ST.ViewModel
{
    public class IOStatusViewModel : BaseViewModel
    {
        #region"Declaration"
        DispatcherTimer DispatchTimerView = new DispatcherTimer();
        System.ComponentModel.BackgroundWorker _BackgroundWorkerView = new System.ComponentModel.BackgroundWorker();
        #endregion
        #region ICommand
        private readonly ICommand _Exit;//
        public ICommand Exit
        {
            get { return _Exit; }
        }
        #endregion
        #region"Destructor"
        ~IOStatusViewModel()
        {
            try
            {
                DispatchTimerView.Stop();
            }
            catch { }
        }
        #endregion
        #region "Consrtuctor"
        public IOStatusViewModel()
        {
            try
            {
                _BackgroundWorkerView.DoWork += DoWork;
                DispatchTimerView.Interval = TimeSpan.FromSeconds(1);
                DispatchTimerView.Tick += DispatcherTickEvent;
                DispatchTimerView.Start();
                _Exit = new RelayCommand(ExitButtonCommandClick);
                WagonIntputs = IndiSCADABusinessLogic.IOStatusLogic.GetWagonInputs();
                BarrelMotorOnOff = IndiSCADABusinessLogic.IOStatusLogic.GetLoaderInput();
                LoaderOutput = IndiSCADABusinessLogic.IOStatusLogic.GetLoaderOutput();
                BarrelMotorStatus = IndiSCADABusinessLogic.IOStatusLogic.GetBarrelMotorStatus();
                //WagonBasketIntputs = IndiSCADABusinessLogic.IOStatusLogic.GetWagonBasketInputs();
                //WagonBasketOP = IndiSCADABusinessLogic.IOStatusLogic.GetWagonBasketOutPut();
                WagonOutputs = IndiSCADABusinessLogic.IOStatusLogic.GetWagonOutPut();
                SystemInputs = IndiSCADABusinessLogic.IOStatusLogic.GetSystemInPut();
                DryerInPuts = IndiSCADABusinessLogic.IOStatusLogic.GetDryerInut();
                DryerOutPuts = IndiSCADABusinessLogic.IOStatusLogic.GetDryerOutPut();
                CTInputs = IndiSCADABusinessLogic.IOStatusLogic.GetCTInPut();
                CTOutPuts = IndiSCADABusinessLogic.IOStatusLogic.GetCTOutPut();
                UnloaderIP = IndiSCADABusinessLogic.IOStatusLogic.GetUnloaderInputStatusInputs();
                UnloaderOP = IndiSCADABusinessLogic.IOStatusLogic.GetUnloaderOutPutStatusInputs();
                 
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusViewModel ()", DateTime.Now.ToString(), ex.Message, "No", true);
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
                ErrorLogger.LogError.ErrorLog(" IOStatusViewModel ExitButtonCommandClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void DoWork(object Sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                App.Current.Dispatcher.BeginInvoke(new Action(() => { WagonIntputs = IndiSCADABusinessLogic.IOStatusLogic.GetWagonInputs(); }));
                App.Current.Dispatcher.BeginInvoke(new Action(() => { WagonOutputs = IndiSCADABusinessLogic.IOStatusLogic.GetWagonOutPut(); }));
                //App.Current.Dispatcher.BeginInvoke(new Action(() => { WagonBasketIntputs = IndiSCADABusinessLogic.IOStatusLogic.GetWagonBasketInputs(); }));
                //App.Current.Dispatcher.BeginInvoke(new Action(() => { WagonBasketOP = IndiSCADABusinessLogic.IOStatusLogic.GetWagonBasketOutPut(); }));
                App.Current.Dispatcher.BeginInvoke(new Action(() => { SystemInputs = IndiSCADABusinessLogic.IOStatusLogic.GetSystemInPut(); }));
                App.Current.Dispatcher.BeginInvoke(new Action(() => { SystemOutPuts = IndiSCADABusinessLogic.IOStatusLogic.GetSystemOutPut(); }));

                App.Current.Dispatcher.BeginInvoke(new Action(() => { UnloaderIP = IndiSCADABusinessLogic.IOStatusLogic.GetUnloaderInputStatusInputs(); }));
                App.Current.Dispatcher.BeginInvoke(new Action(() => { UnloaderOP = IndiSCADABusinessLogic.IOStatusLogic.GetUnloaderOutPutStatusInputs(); }));

                App.Current.Dispatcher.BeginInvoke(new Action(() => { CTInputs = IndiSCADABusinessLogic.IOStatusLogic.GetCTInPut(); }));
                App.Current.Dispatcher.BeginInvoke(new Action(() => { CTOutPuts = IndiSCADABusinessLogic.IOStatusLogic.GetCTOutPut(); }));
                App.Current.Dispatcher.BeginInvoke(new Action(() => { DryerInPuts = IndiSCADABusinessLogic.IOStatusLogic.GetDryerInut(); }));
                App.Current.Dispatcher.BeginInvoke(new Action(() => { DryerOutPuts = IndiSCADABusinessLogic.IOStatusLogic.GetDryerOutPut(); }));
                //App.Current.Dispatcher.BeginInvoke(new Action(() => { TripStatusInPuts = IndiSCADABusinessLogic.IOStatusLogic.GetTripStatusInputs(); }));


                App.Current.Dispatcher.BeginInvoke(new Action(() => { BarrelMotorOnOff = IndiSCADABusinessLogic.IOStatusLogic.GetLoaderInput(); }));
                App.Current.Dispatcher.BeginInvoke(new Action(() => { LoaderOutput = IndiSCADABusinessLogic.IOStatusLogic.GetLoaderOutput(); }));

                App.Current.Dispatcher.BeginInvoke(new Action(() => { BarrelMotorStatus = IndiSCADABusinessLogic.IOStatusLogic.GetBarrelMotorStatus(); }));
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusViewModel DoWork()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                ErrorLogger.LogError.ErrorLog("IOStatusViewModel DispatcherTickEvent()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        #endregion
        #region Public/Private Property
        private ObservableCollection<IOStatusEntity> _WagonIntputs = new ObservableCollection<IOStatusEntity>();
        public ObservableCollection<IOStatusEntity> WagonIntputs
        {
            get { return _WagonIntputs; }
            set { _WagonIntputs = value; OnPropertyChanged("WagonIntputs"); }
        }
        private ObservableCollection<IOStatusEntity> _WagonBasketIntputs = new ObservableCollection<IOStatusEntity>();
        public ObservableCollection<IOStatusEntity> WagonBasketIntputs
        {
            get { return _WagonBasketIntputs; }
            set { _WagonBasketIntputs = value; OnPropertyChanged("WagonBasketIntputs"); }
        }
        private ObservableCollection<IOStatusEntity> _WagonBasketOP = new ObservableCollection<IOStatusEntity>();
        public ObservableCollection<IOStatusEntity> WagonBasketOP
        {
            get { return _WagonBasketOP; }
            set { _WagonBasketOP = value; OnPropertyChanged("WagonBasketOP"); }
        }
        private ObservableCollection<IOStatusEntity> _UnloaderOP = new ObservableCollection<IOStatusEntity>();
        public ObservableCollection<IOStatusEntity> UnloaderOP
        {
            get { return _UnloaderOP; }
            set { _UnloaderOP = value; OnPropertyChanged("UnloaderOP"); }
        }
        private ObservableCollection<IOStatusEntity> _BarrelMotorOnOff = new ObservableCollection<IOStatusEntity>();
        public ObservableCollection<IOStatusEntity> BarrelMotorOnOff
        {
            get { return _BarrelMotorOnOff; }
            set { _BarrelMotorOnOff = value; OnPropertyChanged("BarrelMotorOnOff"); }
        }
        private ObservableCollection<IOStatusEntity> _LoaderOutput = new ObservableCollection<IOStatusEntity>();
        public ObservableCollection<IOStatusEntity> LoaderOutput
        {
            get { return _LoaderOutput; }
            set { _LoaderOutput = value; OnPropertyChanged("LoaderOutput"); }
        }
        
        private ObservableCollection<IOStatusEntity> _BarrelMotorStatus = new ObservableCollection<IOStatusEntity>();
        public ObservableCollection<IOStatusEntity> BarrelMotorStatus
        {
            get { return _BarrelMotorStatus; }
            set { _BarrelMotorStatus = value; OnPropertyChanged("BarrelMotorStatus"); }
        }
        
        private ObservableCollection<IOStatusEntity> _UnloaderIP = new ObservableCollection<IOStatusEntity>();
        public ObservableCollection<IOStatusEntity> UnloaderIP
        {
            get { return _UnloaderIP; }
            set { _UnloaderIP = value; OnPropertyChanged("UnloaderIP"); }
        }
        private ObservableCollection<IOStatusEntity> _WagonOutputs = new ObservableCollection<IOStatusEntity>();
        public ObservableCollection<IOStatusEntity> WagonOutputs
        {
            get { return _WagonOutputs; }
            set { _WagonOutputs = value; OnPropertyChanged("WagonOutputs"); }
        }
        private ObservableCollection<IOStatusEntity> _SystemInputs = new ObservableCollection<IOStatusEntity>();
        public ObservableCollection<IOStatusEntity> SystemInputs
        {
            get { return _SystemInputs; }
            set { _SystemInputs = value; OnPropertyChanged("SystemInputs"); }
        }
        private ObservableCollection<IOStatusEntity> _SystemOutPuts = new ObservableCollection<IOStatusEntity>();
        public ObservableCollection<IOStatusEntity> SystemOutPuts
        {
            get { return _SystemOutPuts; }
            set { _SystemOutPuts = value; OnPropertyChanged("SystemOutPuts"); }
        }
        private ObservableCollection<IOStatusEntity> _CTInputs = new ObservableCollection<IOStatusEntity>();
        public ObservableCollection<IOStatusEntity> CTInputs
        {
            get { return _CTInputs; }
            set { _CTInputs = value; OnPropertyChanged("CTInputs"); }
        }

        private ObservableCollection<IOStatusEntity> _CTOutPuts = new ObservableCollection<IOStatusEntity>();
        public ObservableCollection<IOStatusEntity> CTOutPuts
        {
            get { return _CTOutPuts; }
            set { _CTOutPuts = value; OnPropertyChanged("CTOutPuts"); }
        }

        private ObservableCollection<IOStatusEntity> _DryerOutPuts = new ObservableCollection<IOStatusEntity>();
        public ObservableCollection<IOStatusEntity> DryerOutPuts
        {
            get { return _DryerOutPuts; }
            set { _DryerOutPuts = value; OnPropertyChanged("DryerOutPuts"); }
        }

        private ObservableCollection<IOStatusEntity> _TripStatusInPuts = new ObservableCollection<IOStatusEntity>();
        public ObservableCollection<IOStatusEntity> TripStatusInPuts
        {
            get { return _DryerInPuts; }
            set { _DryerInPuts = value; OnPropertyChanged("TripStatusInPuts"); }
        }
        private ObservableCollection<IOStatusEntity> _DryerInPuts = new ObservableCollection<IOStatusEntity>();
        public ObservableCollection<IOStatusEntity> DryerInPuts
        {
            get { return _DryerInPuts; }
            set { _DryerInPuts = value; OnPropertyChanged("DryerInPuts"); }
        }
        #endregion
    }
}
