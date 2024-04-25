using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace IndiSCADA_ST.ViewModel
{
    public class PartMasterViewModel : BaseViewModel
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
        ~PartMasterViewModel()
        {
            try
            {
                DispatchTimerView.Stop();
            }
            catch { }
        }
        #endregion
        #region "Consrtuctor"
        public PartMasterViewModel()
        {
            try
            {
                _BackgroundWorkerView.DoWork += DoWork;
                DispatchTimerView.Interval = TimeSpan.FromSeconds(5);
                DispatchTimerView.Tick += DispatcherTickEvent;
                DispatchTimerView.Start();
                _Exit = new RelayCommand(ExitButtonCommandClick);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("PartMasterViewModel ()", DateTime.Now.ToString(), ex.Message, "No", true);
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
                ErrorLogger.LogError.ErrorLog("PartMasterViewModel ExitButtonCommandClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void DoWork(object Sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                //App.Current.Dispatcher.BeginInvoke(new Action(() => { WagonIntputs = IndiSCADABusinessLogic.IOStatusLogic.GetWagonInputs(); }));
                //App.Current.Dispatcher.BeginInvoke(new Action(() => { WagonOutputs = IndiSCADABusinessLogic.IOStatusLogic.GetWagonOutPut(); }));
                //App.Current.Dispatcher.BeginInvoke(new Action(() => { SystemInputs = IndiSCADABusinessLogic.IOStatusLogic.GetSystemInPut(); }));
                //App.Current.Dispatcher.BeginInvoke(new Action(() => { SystemOutPuts = IndiSCADABusinessLogic.IOStatusLogic.GetSystemOutPut(); }));

                //App.Current.Dispatcher.BeginInvoke(new Action(() => { CTInputs = IndiSCADABusinessLogic.IOStatusLogic.GetCTInPut(); }));
                //App.Current.Dispatcher.BeginInvoke(new Action(() => { CTOutPuts = IndiSCADABusinessLogic.IOStatusLogic.GetCTOutPut(); }));
                //App.Current.Dispatcher.BeginInvoke(new Action(() => { DryerInPuts = IndiSCADABusinessLogic.IOStatusLogic.GetDryerInut(); }));
                //App.Current.Dispatcher.BeginInvoke(new Action(() => { DryerOutPuts = IndiSCADABusinessLogic.IOStatusLogic.GetDryerOutPut(); }));
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("PartMasterViewModel DoWork()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                ErrorLogger.LogError.ErrorLog("PartMasterViewModel DispatcherTickEvent()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        #endregion
        #region Public/Private Property
        //private ObservableCollection<IOStatusEntity> _DryerInPuts = new ObservableCollection<IOStatusEntity>();
        //public ObservableCollection<IOStatusEntity> DryerInPuts
        //{
        //    get { return _DryerInPuts; }
        //    set { _DryerInPuts = value; OnPropertyChanged("DryerInPuts"); }
        //}
        #endregion
    }
}
