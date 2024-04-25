using IndiSCADA_ST.View;
using IndiSCADAEntity.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace IndiSCADA_ST.ViewModel
{
    public class TankDetailsViewModel : BaseViewModel
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
        private readonly ICommand _UpdateLoadType;
        public ICommand UpdateLoadType
        {
            get { return _UpdateLoadType; }
        }
        private readonly ICommand _UpdateLoadNo;
        public ICommand UpdateLoadNo
        {
            get { return _UpdateLoadNo; }
        }
        private readonly ICommand _DoubleClickCommand;
        public ICommand DoubleClickCommand
        {
            get { return _DoubleClickCommand; }
        }

        private readonly ICommand _OpenNextLoadScn;
        public ICommand OpenNextLoadScn
        {
            get { return _OpenNextLoadScn; }
        }

        private readonly ICommand _StopRefresh;
        public ICommand StopRefresh
        { 
            get { return _StopRefresh; }
        }
        
        #endregion
        #region"Destructor"
        ~TankDetailsViewModel()
        {
            try
            {
                DispatchTimerView.Stop();
            }
            catch { }
        }
        #endregion
        #region "Consrtuctor"
        public TankDetailsViewModel()
        {
            try
            {
                _BackgroundWorkerView.DoWork += DoWork;
                DispatchTimerView.Interval = TimeSpan.FromMilliseconds(500);
                DispatchTimerView.Tick += DispatcherTickEvent;
                DispatchTimerView.Start();
                _Exit = new RelayCommand(ExitButtonCommandClick);
                _UpdateLoadType= new RelayCommand(UpdateLoadTypeCommandClick);
                _UpdateLoadNo = new RelayCommand(UpdateLoadNumberCommandClick);
                _DoubleClickCommand = new RelayCommand(StationNoDoubleClick);
                _OpenNextLoadScn = new RelayCommand(OpenNextLoadScnClick);
                TankDetailsData = IndiSCADABusinessLogic.TankDetailsLogic.GetTanlDetails();
                //_StationNo = new RelayCommand(StationNoDoubleClick);
                _StopRefresh = new RelayCommand(StopTempDataRefreshClick);


            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TankDetailsViewModel ()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        #endregion
        #region "public/private methods"
        private void OpenNextLoadScnClick(object _commandparameters)
        {
            try
            {
                NextLoadView NLV = new NextLoadView(); //this is the change, code for redirect  
                NLV.ShowDialog();
            }
            catch(Exception ex) { ErrorLogger.LogError.ErrorLog("TankDetailsViewModel () Error while opening nextLoadScreen after clicking on partDescription", DateTime.Now.ToString(), ex.Message, "No", true); }
        }


        private void StationNoDoubleClick(object _StationNo)
        {
            try
            {
                if (_StationNo != null)
            {
                int stationNo = Convert.ToInt32(_StationNo);

                NextLoadUpdate _NextLoadUpdate = new NextLoadUpdate(stationNo);
                _NextLoadUpdate.Show();

                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("TankDetailsViewModel StationNoDoubleClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
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
                ErrorLogger.LogError.ErrorLog("TankDetailsViewModel ExitButtonCommandClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void UpdateLoadNumberCommandClick(object _StationIndex)
        {
            try
            {
                if (_StationIndex != null)//_TemperatureIndex is  an index value of tag
                {
                    string username = IndiSCADAGlobalLibrary.UserLoginDetails.UserName; // commented for testing purpose
                    string useraccesslevel = IndiSCADAGlobalLibrary.UserLoginDetails.AccessLevel;

                    //if (username != null)
                    //{
                    //    if (useraccesslevel == "Admin" || useraccesslevel == "Supervisor")//check admin or supervisor is login
                    //    {
                    //        string[] CycleStartStop = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Cycleinprocess");// check cycle is stopeed
                    //        if (CycleStartStop.Length != 0)
                    //        {
                    //            int index = Convert.ToInt32(_StationIndex);
                    //            TankDetailsEntity _TempData = TankDetailsData[index];

                    //            string userEnteredLoadNo = _TempData.LoadNumber.ToString(); 


                    //            int i =0;
                    //            foreach (var item in TankDetailsData)
                    //            {
                    //                if (TankDetailsData[i].LoadNumber.ToString() == userEnteredLoadNo) //Checked entered load no  is not in tank right now
                    //                {
                    //                    MessageBox.Show("Load Number you entered is already present in tank.");
                    //                }
                    //                else
                    //                {
                    //                    int loadno=Convert.ToInt32(TankDetailsData[i].LoadNumber);
                    //                    if (Convert.ToInt32(userEnteredLoadNo) < loadno + 1) //check entered load no is less than upcoming load no from plc
                    //                    {

                    //                        string[] WagonDataLog1=IndiSCADAGlobalLibrary.TagList.W1DataLog;
                    //                        string W1loadNo=WagonDataLog1[12];
                    //                        string[] WagonDataLog2 = IndiSCADAGlobalLibrary.TagList.W2DataLog;
                    //                        string W2loadNo = WagonDataLog2[12];
                    //                        string[] WagonDataLog3 = IndiSCADAGlobalLibrary.TagList.W3DataLog;
                    //                        string W3loadNo = WagonDataLog3[12];
                    //                        string[] WagonDataLog4 = IndiSCADAGlobalLibrary.TagList.W4DataLog;
                    //                        string W4loadNo = WagonDataLog4[12];
                    //                        string[] WagonDataLog5 = IndiSCADAGlobalLibrary.TagList.W5DataLog;
                    //                        string W5loadNo = WagonDataLog5[12];

                    //                        //check that entered load no is not in wagon
                    //                        if(W1loadNo!= userEnteredLoadNo && W2loadNo != userEnteredLoadNo && W3loadNo != userEnteredLoadNo && W4loadNo != userEnteredLoadNo && W5loadNo != userEnteredLoadNo)
                    //                        {
                    //                            if (userEnteredLoadNo == "0" || userEnteredLoadNo == "")
                    //                            {
                    //                                MessageBox.Show("Load number should be non zero and non empty!");
                    //                            }
                    //                            else
                    //                            {
                    //                                IndiSCADABusinessLogic.SettingLogic.WriteValueToPLC("LoadNoAtStation", index, userEnteredLoadNo);
                    //                            }
                    //                        }
                    //                        else
                    //                        {
                    //                            MessageBox.Show("Entered laod no is in wagon");
                    //                        }
                    //                    }
                    //                    else
                    //                    {
                    //                        MessageBox.Show("Load no must be less than upcoming load no from plc");
                    //                    }
                    //                }
                    //                i++;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            MessageBox.Show("Stop cycle and try again");
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("Only admin and supervisor can change load Number");
                    //    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("please login first to edit load no.");
                    //}
                }
                isLoadTypeDataEdit = false;
                isLoadNoDataEdit = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TankDetailsViewModel UpdateLoadNumberCommandClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        private void UpdateLoadTypeCommandClick(object _StationIndex)
        {
            try
            {
                ////if (_StationIndex != null)//_TemperatureIndex is  an index value of tag
                ////{
                ////    string username = IndiSCADAGlobalLibrary.UserLoginDetails.UserName; // commented for testing purpose
                ////    string useraccesslevel = IndiSCADAGlobalLibrary.UserLoginDetails.AccessLevel;

                ////    if (username != null)
                ////    {
                ////        if (useraccesslevel == "Admin" || useraccesslevel == "Supervisor")
                ////        {
                           
                ////                int index = Convert.ToInt32(_StationIndex);
                ////            TankDetailsEntity _TempData = TankDetailsData[index];

                ////            if (Convert.ToInt16(_TempData.LoadType.ToString()) > 0 && Convert.ToInt16(_TempData.LoadType.ToString()) < 5 && _TempData.LoadType.ToString() != "")
                ////            {
                ////                IndiSCADABusinessLogic.SettingLogic.WriteValueToPLC("LoadTypeatStationArrayLoadType", index, _TempData.LoadType);
                ////            }
                ////            else
                ////            {
                ////                MessageBox.Show("Please enter Loadtype between 1 to 4");
                ////            }
                ////        }
                ////    }
                ////}
                isLoadTypeDataEdit = false;
                isLoadNoDataEdit = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TankDetailsViewModel UpdateLoadTypeClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }

        private void StopTempDataRefreshClick(object _commandparameters)
        {
            try
            {
                isLoadTypeDataEdit = true;//stop refresh data 
                isLoadNoDataEdit = true;
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel StopTempDataRefreshClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void DoWork(object Sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {

                bool loadTypeReadOnly = false; bool LoadNoReadOnly = false;
                string username = IndiSCADAGlobalLibrary.UserLoginDetails.UserName;
                string useraccesslevel = IndiSCADAGlobalLibrary.UserLoginDetails.AccessLevel;
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        if (username != null)
                        {
                            if (useraccesslevel == "Admin" || useraccesslevel == "Supervisor")
                            {
                                loadTypeReadOnly = false;
                                LoadNoReadOnly = false;
                            }
                            else
                            {
                                loadTypeReadOnly = true;
                                LoadNoReadOnly = true;
                            }
                        }
                        else
                        {
                            loadTypeReadOnly = true;
                            LoadNoReadOnly = true;
                        }
                    }
                    catch (Exception ex)
                    { ErrorLogger.LogError.ErrorLog("TankDetailsViewModel DoWork() while fetching user access level", DateTime.Now.ToString(), ex.Message, "No", true); }

                }));

                if (isLoadTypeDataEdit == false && isLoadNoDataEdit == false) // do not refresh if loadtype edit is active
                {
                    //  App.Current.Dispatcher.BeginInvoke(new Action(() => { TankDetailsData = IndiSCADABusinessLogic.TankDetailsLogic.GetTanlDetails(); }));
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        //RectifierIntputs = IndiSCADABusinessLogic.SettingLogic.GetRectifierInputs();
                        ObservableCollection<TankDetailsEntity> TankDetailsIP = IndiSCADABusinessLogic.TankDetailsLogic.GetTanlDetails();
                        int TankIndex = 0;
                        if (TankDetailsIP != null)
                        {
                            foreach (var item in TankDetailsIP)
                            {
                                NextloadPartShow = IndiSCADABusinessLogic.TankDetailsLogic.displayNextLoadPart();

                                TankDetailsData[TankIndex].LoadNumber = item.LoadNumber;
                                TankDetailsData[TankIndex].LoadType = item.LoadType;
                                TankDetailsData[TankIndex].Duration = item.Duration;
                                TankDetailsData[TankIndex].SetLoadTypeReadOnly = loadTypeReadOnly;
                                TankDetailsData[TankIndex].SetLoadNoReadOnly = LoadNoReadOnly;
                                string normaldur = item.Duration;

                                try
                                {
                                    string[] durationSplit = normaldur.Split(':');
                                    string hr = ""; string min = ""; string second = "";


                                    if (durationSplit[0].ToString() != "0")//&& durationSplit[1].ToString() != "0" && durationSplit[2].ToString() != "0" )
                                    {
                                        hr = durationSplit[0].ToString() + " h : ";
                                    }
                                    else { hr = "0"; }

                                    if (durationSplit[1].ToString() != "0")
                                    {
                                        min = durationSplit[1].ToString() + " m : ";
                                    }
                                    else { min = ":0"; }

                                    if (durationSplit[2].ToString() != "0")
                                    {
                                        second = durationSplit[2].ToString() + " s";
                                    }
                                    else { second = ":0"; }

                                    TankDetailsData[TankIndex].Duration = hr + min + second;

                                }
                                catch (Exception ex)
                                {
                                    TankDetailsData[TankIndex].Duration = "";
                                    ErrorLogger.LogError.ErrorLog("TankDetailsViewModel DoWork() Error while reading duration", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                                }

                                TankDetailsData[TankIndex].ActualCurrent = item.ActualCurrent;
                                TankDetailsData[TankIndex].ActualTemperature = item.ActualTemperature;
                                TankDetailsData[TankIndex].ActualVoltage = item.ActualVoltage;
                                TankDetailsData[TankIndex].PartName = item.PartName;
                                //TankDetailsData[TankIndex].MECLNumber = item.MECLNumber;
                                TankDetailsData[TankIndex].pH = item.pH;

                                TankIndex = TankIndex + 1;
                            }
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TankDetailsViewModel DoWork()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                ErrorLogger.LogError.ErrorLog("TankDetailsViewModel DispatcherTickEvent()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        #endregion
        #region Public/Private Property        
     
        private ObservableCollection<TankDetailsEntity> _TankDetailsData = new ObservableCollection<TankDetailsEntity>();
        public ObservableCollection<TankDetailsEntity> TankDetailsData
        {
            get { return _TankDetailsData; }
            set { _TankDetailsData = value; OnPropertyChanged("TankDetailsData"); }
        }
        private string _NextloadPartShow ;
        public string NextloadPartShow
        {
            get { return _NextloadPartShow; }
            set { _NextloadPartShow = value; OnPropertyChanged("NextloadPartShow"); }
        }

        private bool _isLoadTypeDataEdit = new bool();
        public bool isLoadTypeDataEdit
        {
            get { return _isLoadTypeDataEdit; }
            set { _isLoadTypeDataEdit = value; OnPropertyChanged("isLoadTypeDataEdit"); }
        }
        private bool _isLoadNoDataEdit = new bool();
        public bool isLoadNoDataEdit
        {
            get { return _isLoadNoDataEdit; }
            set { _isLoadNoDataEdit = value; OnPropertyChanged("isLoadNoDataEdit"); }
        }
        private string _LoadNumber;
        public string LoadNumber
        {
            get
            {
                return _LoadNumber;
            }
            set
            {
                _LoadNumber = value;
                OnPropertyChanged("LoadNumber");
            }
        }
        private string _LoadType;
        public string LoadType
        {
            get
            {
                return _LoadType;
            }
            set
            {
                _LoadType = value;
                OnPropertyChanged("LoadType");
            }
        }
        #endregion
    }
}
