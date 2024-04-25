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
    public class AlarmTimeViewModel : BaseViewModel
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
        private readonly ICommand _UpdateWagonTopDrip;//
        public ICommand UpdateWagonTopDrip
        {
            get { return _UpdateWagonTopDrip; }
        }
        private readonly ICommand _UpdateStationTopDrip;//
        public ICommand UpdateStationTopDrip
        {
            get { return _UpdateStationTopDrip; }
        }
        private readonly ICommand _UpdateCTAlarmTime;//
        public ICommand UpdateCTAlarmTime
        {
            get { return _UpdateCTAlarmTime; }
        }
        private readonly ICommand _UpdateCT2AlarmTime;//
        public ICommand UpdateCT2AlarmTime
        {
            get { return _UpdateCT2AlarmTime; }
        }
        private readonly ICommand _UpdateCT3AlarmTime;//
        public ICommand UpdateCT3AlarmTime
        {
            get { return _UpdateCT3AlarmTime; }
        }
        private readonly ICommand _StopWagonAlarmTimeRefresh;//
        public ICommand StopWagonAlarmTimeRefresh
        {
            get { return _StopWagonAlarmTimeRefresh; }
        }
        private readonly ICommand _UpdateWagon1AlarmTime;//
        public ICommand UpdateWagon1AlarmTime
        {
            get { return _UpdateWagon1AlarmTime; }
        }
        private readonly ICommand _UpdateWagon2AlarmTime;//
        public ICommand UpdateWagon2AlarmTime
        {
            get { return _UpdateWagon2AlarmTime; }
        }
        private readonly ICommand _UpdateWagon3AlarmTime;//
        public ICommand UpdateWagon3AlarmTime
        {
            get { return _UpdateWagon3AlarmTime; }
        }
        private readonly ICommand _UpdateWagon4AlarmTime;//
        public ICommand UpdateWagon4AlarmTime
        {
            get { return _UpdateWagon4AlarmTime; }
        }
        private readonly ICommand _UpdateWagon5AlarmTime;//
        public ICommand UpdateWagon5AlarmTime
        {
            get { return _UpdateWagon5AlarmTime; }
        }
        private readonly ICommand _UpdateWagon6AlarmTime;//
        public ICommand UpdateWagon6AlarmTime
        {
            get { return _UpdateWagon6AlarmTime; }
        }
        private readonly ICommand _UpdateWagon7AlarmTime;//
        public ICommand UpdateWagon7AlarmTime
        {
            get { return _UpdateWagon7AlarmTime; }
        }
        private readonly ICommand _UpdateWagon8AlarmTime;//
        public ICommand UpdateWagon8AlarmTime
        {
            get { return _UpdateWagon8AlarmTime; }
        }
        private readonly ICommand _UpdateDryerAlarmTime;//
        public ICommand UpdateDryerAlarmTime
        {
            get { return _UpdateDryerAlarmTime; }
        }
        private readonly ICommand _StopDryerAlarmTimeRefresh;//
        public ICommand StopDryerAlarmTimeRefresh
        {
            get { return _StopDryerAlarmTimeRefresh; }
        }


        private readonly ICommand _UpdateDryer2AlarmTime;//
        public ICommand UpdateDryer2AlarmTime
        {
            get { return _UpdateDryer2AlarmTime; }
        }
        private readonly ICommand _UpdateDryer3AlarmTime;//
        public ICommand UpdateDryer3AlarmTime
        {
            get { return _UpdateDryer3AlarmTime; }
        }

        private readonly ICommand _UpdateW5BasketAlarmTime;//
        public ICommand UpdateW5BasketAlarmTime
        {
            get { return _UpdateW5BasketAlarmTime; }
        }

        private readonly ICommand _UpdateW4BasketAlarmTime;//
        public ICommand UpdateW4BasketAlarmTime
        {
            get { return _UpdateW4BasketAlarmTime; }
        }
        private readonly ICommand _UpdateW6BasketAlarmTime;//
        public ICommand UpdateW6BasketAlarmTime
        {
            get { return _UpdateW6BasketAlarmTime; }
        }
        #endregion
        #region"Destructor"
        ~AlarmTimeViewModel()
        {
            try
            {
                DispatchTimerView.Stop();
            }
            catch { }
        }
        #endregion
        #region "Consrtuctor"
        public AlarmTimeViewModel()
        {
            try
            {
                _BackgroundWorkerView.DoWork += DoWork;
                DispatchTimerView.Interval = TimeSpan.FromSeconds(1);
                DispatchTimerView.Tick += DispatcherTickEvent;
                DispatchTimerView.Start();
                _Exit = new RelayCommand(ExitButtonCommandClick);

                _StopWagonAlarmTimeRefresh = new RelayCommand(StopWagonAlarmTimeRefreshClick);
                _UpdateWagon1AlarmTime = new RelayCommand(UpdateWagon1AlarmTimeClick);
                _UpdateWagon2AlarmTime = new RelayCommand(UpdateWagon2AlarmTimeClick);
                _UpdateWagon3AlarmTime = new RelayCommand(UpdateWagon3AlarmTimeClick);
                _UpdateWagon4AlarmTime = new RelayCommand(UpdateWagon4AlarmTimeClick);
                _UpdateWagon5AlarmTime = new RelayCommand(UpdateWagon5AlarmTimeClick);
                _UpdateWagon6AlarmTime = new RelayCommand(UpdateWagon6AlarmTimeClick);
                _UpdateWagon7AlarmTime = new RelayCommand(UpdateWagon7AlarmTimeClick);
                _UpdateWagon8AlarmTime = new RelayCommand(UpdateWagon8AlarmTimeClick);

                _UpdateDryerAlarmTime = new RelayCommand(UpdateDryerAlarmTimeClick);
                _UpdateDryer2AlarmTime = new RelayCommand(UpdateDryer2AlarmTimeClick);
                _UpdateDryer3AlarmTime = new RelayCommand(UpdateDryer3AlarmTimeClick);
                _StopDryerAlarmTimeRefresh = new RelayCommand(StopDryerAlarmTimeRefreshClick);

                _UpdateW4BasketAlarmTime = new RelayCommand(UpdateWagon4basketAlarmTimeClick);
                _UpdateW5BasketAlarmTime = new RelayCommand(UpdateWagon5basketAlarmTimeClick);
                _UpdateW6BasketAlarmTime = new RelayCommand(UpdateWagon6basketAlarmTimeClick);

                _UpdateCTAlarmTime = new RelayCommand(UpdateCTAlarmTimeClick);
                _UpdateCT2AlarmTime = new RelayCommand(UpdateCT2AlarmTimeClick);
                _UpdateCT3AlarmTime = new RelayCommand(UpdateCT3AlarmTimeClick);

                _UpdateWagonTopDrip = new RelayCommand(UpdateWagonTopDripClick);
                _UpdateStationTopDrip = new RelayCommand(UpdateStationTopDripClick);

                WagonAlarmTime = IndiSCADABusinessLogic.AlarmTimeLogic.GetWagonAlarmTime();


                DryerAlarmTime = IndiSCADABusinessLogic.AlarmTimeLogic.GetDryerAlarmTime();
                CTAlarmTime = IndiSCADABusinessLogic.AlarmTimeLogic.GetCTAlarmTime();

                WagonBasketAlarmTime = IndiSCADABusinessLogic.AlarmTimeLogic.GetWagonBasketAlarmTime();


                WagonTopDrip = IndiSCADABusinessLogic.AlarmTimeLogic.GetWagonTopDrip();

                StationTopDrip = IndiSCADABusinessLogic.AlarmTimeLogic.GetStationTopDrip();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmTimeViewModel ()", DateTime.Now.ToString(), ex.Message, "No", true);
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
                ErrorLogger.LogError.ErrorLog("OverviewViewModel ExitButtonCommandClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void StopDryerAlarmTimeRefreshClick(object _commandparameters)
        {
            try
            { 
                    _isDryerAlarmTimeEdit = true;
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel StopDryerAlarmTimeRefreshClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            _isDryerAlarmTimeEdit = true;
        }

        private void UpdateDryerAlarmTimeClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    AlarmTimeEntity _AlarmTimeEntity = DryerAlarmTime[index];
                    IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("Dryer1AlarmTime", index, _AlarmTimeEntity.D1);
                    _isDryerAlarmTimeEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel UpdateDryerAlarmTimeClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            _isDryerAlarmTimeEdit = false;
        }
        private void UpdateDryer2AlarmTimeClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    AlarmTimeEntity _AlarmTimeEntity = DryerAlarmTime[index];
                    IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("Dryer2AlarmTime", index, _AlarmTimeEntity.D2);
                    _isDryerAlarmTimeEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel UpdateDryer2AlarmTimeClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            _isDryerAlarmTimeEdit = false;

        }
        private void UpdateDryer3AlarmTimeClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    AlarmTimeEntity _AlarmTimeEntity = DryerAlarmTime[index];
                    IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("Dryer3AlarmTime", index, _AlarmTimeEntity.D3);
                    _isDryerAlarmTimeEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel UpdateDryer3AlarmTimeClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            _isDryerAlarmTimeEdit = false;

        }
        //UpdateCTAlarmTimeClick
        private void UpdateCTAlarmTimeClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    AlarmTimeEntity _AlarmTimeEntity = CTAlarmTime[index];
                    IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("CrossTrolley1AlarmTime", index, _AlarmTimeEntity.CT1);
                    _isDryerAlarmTimeEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel UpdateCTAlarmTimeClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            _isDryerAlarmTimeEdit = false;
        }

        private void UpdateCT2AlarmTimeClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    AlarmTimeEntity _AlarmTimeEntity = CTAlarmTime[index];
                    IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("CrossTrolley2AlarmTime", index, _AlarmTimeEntity.CT2);
                    isDryerAlarmTimeEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel UpdateCT2AlarmTimeClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void UpdateCT3AlarmTimeClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    AlarmTimeEntity _AlarmTimeEntity = CTAlarmTime[index];
                    IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("CrossTrolley3AlarmTime", index, _AlarmTimeEntity.CT3);
                    isDryerAlarmTimeEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel UpdateCT3AlarmTimeClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        //UpdateWagon4basketAlarmTimeClick
        private void UpdateWagon4basketAlarmTimeClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    AlarmTimeEntity _AlarmTimeEntity = WagonBasketAlarmTime[index];
                    IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("Wagon6BasketAlarmTime", index, _AlarmTimeEntity.W6);
                    _isDryerAlarmTimeEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel UpdateWagon6basketAlarmTimeClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            _isDryerAlarmTimeEdit = false;
        }
        private void UpdateWagon5basketAlarmTimeClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    AlarmTimeEntity _AlarmTimeEntity = WagonBasketAlarmTime[index];
                    IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("Wagon7BasketAlarmTime", index, _AlarmTimeEntity.W7);
                    _isDryerAlarmTimeEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel UpdateWagon7basketAlarmTimeClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            _isDryerAlarmTimeEdit = false;
        }
        private void UpdateWagon6basketAlarmTimeClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    AlarmTimeEntity _AlarmTimeEntity = WagonBasketAlarmTime[index];
                    IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("Wagon8BasketAlarmTime", index, _AlarmTimeEntity.W8);
                    _isDryerAlarmTimeEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel UpdateWagon8basketAlarmTimeClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            _isDryerAlarmTimeEdit = false;
        }
        //
        private void StopWagonAlarmTimeRefreshClick(object _commandparameters)
        {
            try
            {
                _isWagonAlarmTimeEdit = true;
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel StopWagonAlarmTimeRefreshClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }

        }

        private void UpdateWagon1AlarmTimeClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    AlarmTimeEntity _AlarmTimeEntity = WagonAlarmTime[index];
                    IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("Wagon1AlarmTime", index, _AlarmTimeEntity.W1);
                    _isWagonAlarmTimeEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel UpdateWagonAlarmTimeClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            _isWagonAlarmTimeEdit = false;
        }
        private void UpdateWagon2AlarmTimeClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    AlarmTimeEntity _AlarmTimeEntity = WagonAlarmTime[index];
                    IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("Wagon2AlarmTime", index, _AlarmTimeEntity.W2);
                    _isWagonAlarmTimeEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel UpdateWagon2AlarmTimeClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            _isWagonAlarmTimeEdit = false;
        }
        private void UpdateWagon3AlarmTimeClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    AlarmTimeEntity _AlarmTimeEntity = WagonAlarmTime[index];
                    IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("Wagon3AlarmTime", index, _AlarmTimeEntity.W3);
                    _isWagonAlarmTimeEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel UpdateWagon3AlarmTimeClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            _isWagonAlarmTimeEdit = false;
        }
        private void UpdateWagon4AlarmTimeClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    AlarmTimeEntity _AlarmTimeEntity = WagonAlarmTime[index];
                    IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("Wagon4AlarmTime", index, _AlarmTimeEntity.W4);
                    _isWagonAlarmTimeEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel UpdateWagon4AlarmTimeClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            _isWagonAlarmTimeEdit = false;
        }
        private void UpdateWagon5AlarmTimeClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    AlarmTimeEntity _AlarmTimeEntity = WagonAlarmTime[index];
                    IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("Wagon5AlarmTime", index, _AlarmTimeEntity.W5);
                    _isWagonAlarmTimeEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel UpdateWagon5AlarmTimeClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            _isWagonAlarmTimeEdit = false;
        }
        private void UpdateWagon6AlarmTimeClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    AlarmTimeEntity _AlarmTimeEntity = WagonAlarmTime[index];
                    IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("Wagon6AlarmTime", index, _AlarmTimeEntity.W6);
                    _isWagonAlarmTimeEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel UpdateWagon6AlarmTimeClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            _isWagonAlarmTimeEdit = false;
        }
        private void UpdateWagon7AlarmTimeClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    AlarmTimeEntity _AlarmTimeEntity = WagonAlarmTime[index];
                    IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("Wagon7AlarmTime", index, _AlarmTimeEntity.W7);
                    _isWagonAlarmTimeEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel UpdateWagon7AlarmTimeClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            _isWagonAlarmTimeEdit = false;
        }

        private void UpdateWagon8AlarmTimeClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    AlarmTimeEntity _AlarmTimeEntity = WagonAlarmTime[index];
                    IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("Wagon8AlarmTime", index, _AlarmTimeEntity.W8);
                    _isWagonAlarmTimeEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel UpdateWagon8AlarmTimeClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            _isWagonAlarmTimeEdit = false;
        }

        private void UpdateWagonTopDripClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    AlarmTimeEntity _AlarmTimeEntity = WagonTopDrip[index];
                    IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("WagonTopDrip", index, _AlarmTimeEntity.Value);
                    _isDryerAlarmTimeEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel UpdateWagonTopDripClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            _isDryerAlarmTimeEdit = false;
        }
        private void UpdateStationTopDripClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    AlarmTimeEntity _AlarmTimeEntity = StationTopDrip[index];
                    IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("StationTopDrip", index, _AlarmTimeEntity.Value);
                    _isDryerAlarmTimeEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel UpdateStationTopDripClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            _isDryerAlarmTimeEdit = false;
        }

        #endregion
        #region DoWork method
        private void DoWork(object Sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {

                App.Current.Dispatcher.BeginInvoke(new Action(() => {
                    if (isWagonAlarmTimeEdit == false)
                    {

                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            //RectifierIntputs = IndiSCADABusinessLogic.SettingLogic.GetRectifierInputs();
                            ObservableCollection<AlarmTimeEntity> AlarmtimeIP = IndiSCADABusinessLogic.AlarmTimeLogic.GetWagonAlarmTime(); ;
                            int AlarmIndex = 0;
                            if (AlarmtimeIP != null)
                            {
                                try
                                {
                                    foreach (var item in AlarmtimeIP)
                                    {

                                        WagonAlarmTime[AlarmIndex].W1 = item.W1;
                                        WagonAlarmTime[AlarmIndex].W2 = item.W2;
                                        WagonAlarmTime[AlarmIndex].W3 = item.W3;
                                        WagonAlarmTime[AlarmIndex].W4 = item.W4;
                                        WagonAlarmTime[AlarmIndex].W5 = item.W5;
                                        WagonAlarmTime[AlarmIndex].W6 = item.W6;
                                        WagonAlarmTime[AlarmIndex].W7 = item.W7;
                                        WagonAlarmTime[AlarmIndex].W8 = item.W8;
                                        AlarmIndex = AlarmIndex + 1;
                                    }
                                }
                                catch (Exception ex) { }
                            }
                        }));
                    }
                }));

                App.Current.Dispatcher.BeginInvoke(new Action(() => {
                    if (isDryerAlarmTimeEdit == false)
                    {

                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            //RectifierIntputs = IndiSCADABusinessLogic.SettingLogic.GetRectifierInputs();
                            ObservableCollection<AlarmTimeEntity> AlarmtimeIP = IndiSCADABusinessLogic.AlarmTimeLogic.GetDryerAlarmTime(); ;
                            int AlarmIndex = 0;
                            if (AlarmtimeIP != null)
                            {
                                try
                                {
                                    foreach (var item in AlarmtimeIP)
                                    {

                                        DryerAlarmTime[AlarmIndex].D1 = item.D1;
                                        DryerAlarmTime[AlarmIndex].D2 = item.D2;
                                        DryerAlarmTime[AlarmIndex].D3 = item.D3;

                                        AlarmIndex = AlarmIndex + 1;
                                    }
                                }
                                catch (Exception ex) { }
                            }
                        }));

                        
                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            //RectifierIntputs = IndiSCADABusinessLogic.SettingLogic.GetRectifierInputs();
                            ObservableCollection<AlarmTimeEntity> AlarmtimeIP = IndiSCADABusinessLogic.AlarmTimeLogic.GetCTAlarmTime();  
                            int AlarmIndex = 0;
                            if (AlarmtimeIP != null)
                            {
                                try
                                {
                                    foreach (var item in AlarmtimeIP)
                                    {

                                        CTAlarmTime[AlarmIndex].CT1 = item.CT1;
                                        CTAlarmTime[AlarmIndex].CT2 = item.CT2;
                                        CTAlarmTime[AlarmIndex].CT3 = item.CT3;

                                        AlarmIndex = AlarmIndex + 1;
                                    }
                                }
                                catch (Exception ex) { }
                            }
                        }));
                     
                        //WagonTopDrip
                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            ObservableCollection<AlarmTimeEntity> AlarmtimeIP = IndiSCADABusinessLogic.AlarmTimeLogic.GetWagonTopDrip(); 
                            int AlarmIndex = 0;
                            if (AlarmtimeIP != null)
                            {
                                try
                                {
                                    foreach (var item in AlarmtimeIP)
                                    {

                                        WagonTopDrip[AlarmIndex].Value = item.Value;
                                        AlarmIndex = AlarmIndex + 1;
                                    }
                                }
                                catch (Exception ex) { }
                            }
                        }));
                       
                        //StationTopDrip
                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            ObservableCollection<AlarmTimeEntity> AlarmtimeIP = IndiSCADABusinessLogic.AlarmTimeLogic.GetStationTopDrip(); 
                            int AlarmIndex = 0;
                            if (AlarmtimeIP != null)
                            {
                                try
                                {
                                    foreach (var item in AlarmtimeIP)
                                    {

                                        StationTopDrip[AlarmIndex].Value = item.Value;
                                        AlarmIndex = AlarmIndex + 1;
                                    }
                                }
                                catch (Exception ex) { }
                            }
                        }));
                    }
                }));

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmTimeViewModel DoWork()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                ErrorLogger.LogError.ErrorLog("AlarmTimeViewModel DispatcherTickEvent()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        #endregion
        #region Public/Private Property
        private ObservableCollection<AlarmTimeEntity> _WagonAlarmTime = new ObservableCollection<AlarmTimeEntity>();
        public ObservableCollection<AlarmTimeEntity> WagonAlarmTime
        {
            get { return _WagonAlarmTime; }
            set { _WagonAlarmTime = value; OnPropertyChanged("WagonAlarmTime"); }
        }
        private bool _isWagonAlarmTimeEdit = new bool();
        public bool isWagonAlarmTimeEdit
        {
            get { return _isWagonAlarmTimeEdit; }
            set { _isWagonAlarmTimeEdit = value; OnPropertyChanged("isWagonAlarmTimeEdit"); }
        }
        private ObservableCollection<AlarmTimeEntity> _DryerAlarmTime = new ObservableCollection<AlarmTimeEntity>();
        public ObservableCollection<AlarmTimeEntity> DryerAlarmTime
        {
            get { return _DryerAlarmTime; }
            set { _DryerAlarmTime = value; OnPropertyChanged("DryerAlarmTime"); }
        }
        //
        private ObservableCollection<AlarmTimeEntity> _CTAlarmTime = new ObservableCollection<AlarmTimeEntity>();
        public ObservableCollection<AlarmTimeEntity> CTAlarmTime
        {
            get { return _CTAlarmTime; }
            set { _CTAlarmTime = value; OnPropertyChanged("CTAlarmTime"); }
        }
        private ObservableCollection<AlarmTimeEntity> _WagonTopDrip = new ObservableCollection<AlarmTimeEntity>();
        public ObservableCollection<AlarmTimeEntity> WagonTopDrip
        {
            get { return _WagonTopDrip; }
            set { _WagonTopDrip = value; OnPropertyChanged("WagonTopDrip"); }
        }
        private ObservableCollection<AlarmTimeEntity> _StationTopDrip = new ObservableCollection<AlarmTimeEntity>();
        public ObservableCollection<AlarmTimeEntity> StationTopDrip
        {
            get { return _StationTopDrip; }
            set { _StationTopDrip = value; OnPropertyChanged("StationTopDrip"); }
        }
        
         
        //
        private ObservableCollection<AlarmTimeEntity> _WagonBasketAlarmTime = new ObservableCollection<AlarmTimeEntity>();
        public ObservableCollection<AlarmTimeEntity> WagonBasketAlarmTime
        {
            get { return _WagonBasketAlarmTime; }
            set { _WagonBasketAlarmTime = value; OnPropertyChanged("WagonBasketAlarmTime"); }
        }
        private bool _isDryerAlarmTimeEdit = new bool();
        public bool isDryerAlarmTimeEdit
        {
            get { return _isDryerAlarmTimeEdit; }
            set { _isDryerAlarmTimeEdit = value; OnPropertyChanged("isDryerAlarmTimeEdit"); }
        }
        #endregion
    }
}
