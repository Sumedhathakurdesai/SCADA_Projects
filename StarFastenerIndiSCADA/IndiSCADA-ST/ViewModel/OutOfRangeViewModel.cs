using IndiSCADAEntity.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace IndiSCADA_ST.ViewModel
{
    public class OutOfRangeViewModel : BaseViewModel
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
        private readonly ICommand _NewProgramName;//
        public ICommand NewProgramName
        {
            get { return _NewProgramName; }
        }
        private readonly ICommand _SaveProgramName;//
        public ICommand SaveProgramName
        {
            get { return _SaveProgramName; }
        }
        private readonly ICommand _DipTimeLowBypass;//
        public ICommand DipTimeLowBypass
        {
            get { return _DipTimeLowBypass; }
        }
        private readonly ICommand _DipTimeHighBypass;//
        public ICommand DipTimeHighBypass
        {
            get { return _DipTimeHighBypass; }
        }
        private readonly ICommand _DipTimeLowBypassEdit;//
        public ICommand DipTimeLowBypassEdit
        {
            get { return _DipTimeLowBypassEdit; }
        }
        private readonly ICommand _DipTimeHighBypassEdit;//
        public ICommand DipTimeHighBypassEdit
        {
            get { return _DipTimeHighBypassEdit; }
        }
        private readonly ICommand _UpdateLowpH;//
        public ICommand UpdateLowpH
        {
            get { return _UpdateLowpH; }
        }
        private readonly ICommand _UpdateHighpH;//
        public ICommand UpdateHighpH
        {
            get { return _UpdateHighpH; }
        }
        private readonly ICommand _UpdateavgpH;//
        public ICommand UpdateavgpH
        {
            get { return _UpdateavgpH; }
        }
        private readonly ICommand _UpdateTimerpH;//
        public ICommand UpdateTimerpH
        {
            get { return _UpdateTimerpH; }
        }
        private readonly ICommand _pHLowBypass;//
        public ICommand pHLowBypass
        {
            get { return _pHLowBypass; }
        }
        private readonly ICommand _pHHighBypass;//
        public ICommand pHHighBypass
        {
            get { return _pHHighBypass; }
        }
        
        private readonly ICommand _StoppH;//
        public ICommand StoppH
        {
            get { return _StoppH; }
        }
        
        private readonly ICommand _StopTemp;//
        public ICommand StopTemp
        {
            get { return _StopTemp; }
        }
        private readonly ICommand _UpdateLowTemp;//
        public ICommand UpdateLowTemp
        {
            get { return _UpdateLowTemp; }
        }
        private readonly ICommand _UpdateHightemp;//
        public ICommand UpdateHightemp
        {
            get { return _UpdateHightemp; }
        }
        private readonly ICommand _UpdateSettemp;//
        public ICommand UpdateSettemp
        {
            get { return _UpdateSettemp; }
        }
        private readonly ICommand _UpdateAvgtemp;//
        public ICommand UpdateAvgtemp
        {
            get { return _UpdateAvgtemp; }
        }
        private readonly ICommand _UpdateTimertemp;//
        public ICommand UpdateTimertemp
        {
            get { return _UpdateTimertemp; }
        }
        private readonly ICommand _UpdateLowSpBypasstemp;//
        public ICommand UpdateLowSpBypasstemp
        {
            get { return _UpdateLowSpBypasstemp; }
        }
        private readonly ICommand _UpdateHighSpBypasstempEdit;//
        public ICommand UpdateHighSpBypasstempEdit
        {
            get { return _UpdateHighSpBypasstempEdit; }
        }
        private readonly ICommand _UpdateLowSpBypasstempEdit;//
        public ICommand UpdateLowSpBypasstempEdit
        {
            get { return _UpdateLowSpBypasstempEdit; }
        }
        private readonly ICommand _UpdateHighSpBypasstemp;//
        public ICommand UpdateHighSpBypasstemp
        {
            get { return _UpdateHighSpBypasstemp; }
        }
        private readonly ICommand _EditDipTime;//
        public ICommand EditDipTime
        {
            get { return _EditDipTime; }
        }
        private readonly ICommand _DeleteDipTime;//
        public ICommand DeleteDipTime
        {
            get { return _DeleteDipTime; }
        }
        #endregion
        #region"Destructor"
        ~OutOfRangeViewModel()
        {
            try
            {
                DispatchTimerView.Stop();
            }
            catch { }
        }
        #endregion
        #region "Consrtuctor"
        public OutOfRangeViewModel()
        {
            try
            {
                _BackgroundWorkerView.DoWork += DoWork;
                DispatchTimerView.Interval = TimeSpan.FromSeconds(1);
                DispatchTimerView.Tick += DispatcherTickEvent;
                DispatchTimerView.Start();
                _Exit = new RelayCommand(ExitButtonCommandClick);
                _UpdateLowpH = new RelayCommand(UpdatepHLowSpClick);
                _StoppH = new RelayCommand(StoppHRefreshClick);
                _UpdateLowTemp = new RelayCommand(UpdateLowTempClick);
                _StopTemp = new RelayCommand(StopTempClick);

                _UpdateHighpH = new RelayCommand(UpdateHighpHClick);
                _UpdateavgpH = new RelayCommand(UpdateAvgpHClick);

                _UpdateTimerpH = new RelayCommand(UpdateTimerpHClick);

                _pHLowBypass = new RelayCommand(pHLowBypassClick);
                _pHHighBypass = new RelayCommand(pHHighBypassClick);

                _UpdateHightemp = new RelayCommand(UpdateHighTempClick);
                _UpdateSettemp = new RelayCommand(UpdateSPTempClick);

                _UpdateAvgtemp = new RelayCommand(UpdateavgTempClick);
                _UpdateTimertemp = new RelayCommand(UpdateTimerTempClick);

                _UpdateLowSpBypasstemp = new RelayCommand(TempLowBypassClick);
                _UpdateHighSpBypasstemp = new RelayCommand(TempHighBypassClick);

                _SaveProgramName = new RelayCommand(SaveProgramNameClick);
                _NewProgramName= new RelayCommand(NewProgramNameClick);
                _DipTimeLowBypass = new RelayCommand(DipTimeLowBypassClick);
                _DipTimeHighBypass = new RelayCommand(DipTimeHighBypassClick);
                _EditDipTime = new RelayCommand(DipTimeEditClick);
                _DeleteDipTime = new RelayCommand(DipTimeDeleteClick);


                _DipTimeLowBypassEdit = new RelayCommand(DipTimeLowBypassClickEdit);
                _DipTimeHighBypassEdit = new RelayCommand(DipTimeHighBypassClickEdit);

                pHCollection = IndiSCADABusinessLogic.OutOfRangeLogic.GetORpHInputs();
                TempCollection = IndiSCADABusinessLogic.OutOfRangeLogic.GetORTemperatureInputs();
                DipTimeCollectionNew = IndiSCADABusinessLogic.OutOfRangeLogic.GetORDipTimeInputs();
                ProgramNameText = "";
                RefreshComboProgramNo();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OutOfRange ()", DateTime.Now.ToString(), ex.Message, "No", true);
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
                ErrorLogger.LogError.ErrorLog("OutOfRange ExitButtonCommandClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void StopTempClick(object _commandparameters)
        {
            try
            {
                isTempEdit  = true;
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OutOfRange StopTempClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
       
       private void StoppHRefreshClick(object _commandparameters)
        {
            try
            {
                ispHEdit  = true;
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OutOfRange StoppHRefreshClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }

        }
        private void UpdateHighpHClick(object _Index)
        {
            try
            {
                ispHEdit = true;
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    OutOfRangesSettingsEntitypH _pHEntity = pHCollection[index];
                    IndiSCADABusinessLogic.OutOfRangeLogic.WriteValueToPLC("ORpHHighSetPoint", index, _pHEntity.HighSPpH);
                    ispHEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OutOfRange UpdateHighpHClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            ispHEdit = false;
        }
        private void UpdatepHLowSpClick(object _Index)
        {
            try
            {
                _ispHEdit = true;
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    OutOfRangesSettingsEntitypH _pHEntity = pHCollection[index];
                    IndiSCADABusinessLogic.OutOfRangeLogic.WriteValueToPLC("ORpHLowSetPoint", index, _pHEntity.LowSPpH);
                    _ispHEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OutOfRange UpdatepHLowSpClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            _ispHEdit = false;
        }
        //UpdateAvgpHClick
        private void UpdateAvgpHClick(object _Index)
        {
            try
            {
                _ispHEdit = true;
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    OutOfRangesSettingsEntitypH _pHEntity = pHCollection[index];
                    IndiSCADABusinessLogic.OutOfRangeLogic.WriteValueToPLC("ORpHAvg", index, _pHEntity.AvgpH);
                    _ispHEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OutOfRange UpdateAvgpHClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            _ispHEdit = false;
        }
        //UpdateTimerpHClick
        private void UpdateTimerpHClick(object _Index)
        {
            try
            {
                _ispHEdit = true;
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    OutOfRangesSettingsEntitypH _pHEntity = pHCollection[index];
                    IndiSCADABusinessLogic.OutOfRangeLogic.WriteValueToPLC("ORpHDelay", index, _pHEntity.DelaypH);
                    _ispHEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OutOfRange UpdateTimerpHClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            _ispHEdit = false;
        }
        private void pHLowBypassClick(object _RectifierIndex)
        {
            try
            {
                if (_RectifierIndex != null)//
                {
                    int index = Convert.ToInt32(_RectifierIndex);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("ORpHLowBypass", index);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel pHLowBypassClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        private void pHHighBypassClick(object _RectifierIndex)
        {
            try
            {
                if (_RectifierIndex != null)//
                {
                    int index = Convert.ToInt32(_RectifierIndex);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("ORpHHighBypass", index);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel pHHighBypassClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        //UpdateLowTempClick
        private void UpdateLowTempClick(object _Index)
        {
            try
            {
                isTempEdit = true ;
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    OutOfRangesSettingsEntityTemp _pHEntity = TempCollection[index];
                    IndiSCADABusinessLogic.OutOfRangeLogic.WriteValueToPLC("ORTEMPERATURELowSetPoint", index, _pHEntity.LowSPTemp);
                    isTempEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OutOfRange UpdateLowTempClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            isTempEdit = false;
        }
        private void UpdateHighTempClick(object _Index)
        {
            try
            {
                isTempEdit = true;
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    OutOfRangesSettingsEntityTemp _pHEntity = TempCollection[index];
                    IndiSCADABusinessLogic.OutOfRangeLogic.WriteValueToPLC("ORTEMPERATUREHighSetPoint", index, _pHEntity.HighSPTemp);
                    isTempEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OutOfRange UpdateHighTempClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            isTempEdit = false;
        }
        private void UpdateTimerTempClick(object _Index)
        {
            try
            {
                isTempEdit = true;
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    OutOfRangesSettingsEntityTemp _pHEntity = TempCollection[index];
                    IndiSCADABusinessLogic.OutOfRangeLogic.WriteValueToPLC("ORTEMPERATUREDelay", index, _pHEntity.DelayTemp);
                    isTempEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OutOfRange UpdateTimerTempClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            isTempEdit = false;
        }
        private void UpdateSPTempClick(object _Index)
        {
            try
            {
                isTempEdit = true;
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    OutOfRangesSettingsEntityTemp _pHEntity = TempCollection[index];
                    IndiSCADABusinessLogic.OutOfRangeLogic.WriteValueToPLC("TempControllerSetPoint", index, _pHEntity.SetPointTemp);
                    isTempEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OutOfRange UpdateSPTempClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            isTempEdit = false;
        }
        private void UpdateavgTempClick(object _Index)
        {
            try
            {
                isTempEdit = true;
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    OutOfRangesSettingsEntityTemp _pHEntity = TempCollection[index];
                    IndiSCADABusinessLogic.OutOfRangeLogic.WriteValueToPLC("ORTEMPERATUREAvg", index, _pHEntity.AvgTemp);
                    isTempEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OutOfRange UpdateavgTempClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
            isTempEdit = false;
        }
        private void TempLowBypassClick(object _Index)
        {
            try
            {
                if (_Index != null)//
                {
                    int index = Convert.ToInt32(_Index);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("ORTEMPERATURELowBypass", index);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ORViewModel TempLowBypassClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        private void TempHighBypassClick(object _Index)
        {
            try
            {
                if (_Index != null)//
                {
                    int index = Convert.ToInt32(_Index);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("ORTEMPERATUREHighBypass", index);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ORViewModel TempHighBypassClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }

        //NewProgramNameClick
        private void NewProgramNameClick(object _Index)
        {
            try
            {
                DipTimeCollectionNew = IndiSCADABusinessLogic.OutOfRangeLogic.GetORDipTimeInputs();
                ProgramNameText = "";
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ORViewModel NewProgramNameClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        //DipTimeEditClick
        private void DipTimeEditClick(object _Index)
        {
            try
            {
                if (selectedprogramNo != null && selectedprogramNo != "")
                {
                    if (DipTimeCollectionEdit != null)
                    {
                        int index = Convert.ToInt32(_Index);
                        OutOfRangeSettingsDipTime _DipTimeEntity = DipTimeCollectionEdit[index];
                        IndiSCADABusinessLogic.OutOfRangeLogic.UpdateORDipTimeInputs(_DipTimeEntity);

                        if (MessageBox.Show("Are you sure you want to Edit part " + selectedprogramNo + " ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            MessageBox.Show("Program No - " + selectedprogramNo + " Edited successfully.");
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select program name.");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ORViewModel DipTimeEditClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        //
        private void DipTimeDeleteClick(object _Index)
        {
            try
            {
                if (selectedprogramNo != null && selectedprogramNo != "")
                {
                    OutOfRangeSettingsDipTime objDiptTime = new OutOfRangeSettingsDipTime();
                    objDiptTime.ProgramNo = selectedprogramNo;
                    IndiSCADABusinessLogic.OutOfRangeLogic.DeleteORDipTimeInputs(objDiptTime);
                    selectedprogramNo = "";
                    RefreshComboProgramNo();
                    DipTimeCollectionEdit = new ObservableCollection<OutOfRangeSettingsDipTime>();

                    if (MessageBox.Show("Are you sure you want to delete part " + objDiptTime.ProgramNo + " ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        MessageBox.Show("Program No - " + objDiptTime.ProgramNo + " Deleted successfully.");
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please select program name.");
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ORViewModel DipTimeEditClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        //SaveProgramNameClick
        private void SaveProgramNameClick(object _Index)
        {
            try
            {
                OutOfRangeSettingsDipTime objDiptTime = new OutOfRangeSettingsDipTime();
                objDiptTime.ProgramNo = selectedprogramNo;

                if (ProgramNameText !="" && ProgramNameText.Length > 0)
                {
                    ObservableCollection<OutOfRangeSettingsDipTime> __ORDipTimeData = new ObservableCollection<OutOfRangeSettingsDipTime>();

                    __ORDipTimeData = ComboProgramName;//IndiSCADABusinessLogic.OutOfRangeLogic.GetORDipTimeInputsProgramName();

                    try
                    {
                        if (__ORDipTimeData != null)
                        {
                            foreach (var item in __ORDipTimeData)
                            {
                                if (item.ProgramNo == ProgramNameText)
                                {
                                    MessageBox.Show("Program Number " + ProgramNameText + " is already exists !");
                                    return;
                                }
                            }
                        }
                    }
                    catch(Exception ex)
                    {

                    }

                    if (DipTimeCollectionNew !=null)
                    {
                        IndiSCADABusinessLogic.OutOfRangeLogic.InsertORDipTimeInputs(DipTimeCollectionNew, ProgramNameText);
                    }
                    DipTimeCollectionNew = IndiSCADABusinessLogic.OutOfRangeLogic.GetORDipTimeInputs();
                    
                    RefreshComboProgramNo();
                    MessageBox.Show("Program No - " + ProgramNameText + " Saved successfully.");
                    ProgramNameText = "";
                }
                else
                {
                    MessageBox.Show("Please enter program name.");
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ORViewModel SaveProgramNameClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        //DipTimeLowBypassClick
        private void DipTimeLowBypassClick(object _Index)
        {
            try
            {
                if (_Index != null)//
                {
                    int index = Convert.ToInt32(_Index);
                    OutOfRangeSettingsDipTime _DipTimeEntity = DipTimeCollectionNew[index];
                    if (_DipTimeEntity.LowBypass =="0")
                    {
                        DipTimeCollectionNew[index].LowBypass = "1";
                    }
                    else
                    {
                        DipTimeCollectionNew[index].LowBypass = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ORViewModel DipTimeLowBypassClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        //DipTimeHighBypassClick
        private void DipTimeHighBypassClick(object _Index)
        {
            try
            {
                if (_Index != null)//
                {
                    int index = Convert.ToInt32(_Index);
                    OutOfRangeSettingsDipTime _DipTimeEntity = DipTimeCollectionNew[index];
                    if (_DipTimeEntity.HighBypass == "0")
                    {
                        DipTimeCollectionNew[index].HighBypass = "1";
                    }
                    else
                    {
                        DipTimeCollectionNew[index].HighBypass = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ORViewModel DipTimeHighBypassClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        //DipTimeLowBypassClick
        private void DipTimeLowBypassClickEdit(object _Index)
        {
            try
            {
                if (_Index != null)//
                {
                    int index = Convert.ToInt32(_Index);
                    OutOfRangeSettingsDipTime _DipTimeEntity = DipTimeCollectionEdit[index];
                    if (_DipTimeEntity.LowBypass == "0")
                    {
                        DipTimeCollectionEdit[index].LowBypass = "1";
                    }
                    else
                    {
                        DipTimeCollectionEdit[index].LowBypass = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ORViewModel DipTimeLowBypassClickEdit()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        //DipTimeHighBypassClick
        private void DipTimeHighBypassClickEdit(object _Index)
        {
            try
            {
                if (_Index != null)//
                {
                    int index = Convert.ToInt32(_Index);
                    OutOfRangeSettingsDipTime _DipTimeEntity = DipTimeCollectionEdit[index];
                    if (_DipTimeEntity.HighBypass == "0")
                    {
                        DipTimeCollectionEdit[index].HighBypass = "1";
                    }
                    else
                    {
                        DipTimeCollectionEdit[index].HighBypass = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ORViewModel DipTimeHighBypassClickEdit()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        private void DoWork(object Sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {

                App.Current.Dispatcher.BeginInvoke(new Action(() => {
                    if (ispHEdit == false)
                    {
                        ObservableCollection<OutOfRangesSettingsEntitypH> pHORIP = IndiSCADABusinessLogic.OutOfRangeLogic.GetORpHInputs(); ;
                        int pHORIndex = 0;
                        if (pHORIP != null)
                        {
                            try
                            {
                                foreach (var item in pHORIP)
                                {
                                    pHCollection[pHORIndex].HighSPpH = item.HighSPpH;
                                    pHCollection[pHORIndex].LowSPpH = item.LowSPpH;
                                    pHCollection[pHORIndex].LowBypasspH = item.LowBypasspH;
                                    pHCollection[pHORIndex].HighBypasspH = item.HighBypasspH;
                                    pHCollection[pHORIndex].DelaypH = item.DelaypH;
                                    pHCollection[pHORIndex].AvgpH = item.AvgpH;
                                    pHORIndex = pHORIndex + 1;
                                }
                            }
                            catch (Exception ex) { ErrorLogger.LogError.ErrorLog("ORViewModel GetORpHInputs()", DateTime.Now.ToString(), ex.Message, "No", true); }
                        }
                    }
                }));
                App.Current.Dispatcher.BeginInvoke(new Action(() => {
                    if (isTempEdit  == false)
                    {
                        ObservableCollection<OutOfRangesSettingsEntityTemp> tempORIP = IndiSCADABusinessLogic.OutOfRangeLogic.GetORTemperatureInputs(); ;
                        int tempORIndex = 0;
                        if (tempORIP != null)
                        {
                            try
                            {
                                foreach (var item in tempORIP)
                                {
                                    TempCollection[tempORIndex].HighSPTemp = item.HighSPTemp;
                                    TempCollection[tempORIndex].LowSPTemp = item.LowSPTemp;
                                    TempCollection[tempORIndex].SetPointTemp = item.SetPointTemp;
                                    TempCollection[tempORIndex].LowBypassTemp = item.LowBypassTemp;
                                    TempCollection[tempORIndex].HighBypassTemp = item.HighBypassTemp;
                                    TempCollection[tempORIndex].DelayTemp = item.DelayTemp;
                                    TempCollection[tempORIndex].AvgTemp = item.AvgTemp;
                                    tempORIndex = tempORIndex + 1;
                                }
                            }
                            catch (Exception ex) { ErrorLogger.LogError.ErrorLog("ORViewModel GetORTemperatureInputs()", DateTime.Now.ToString(), ex.Message, "No", true); }
                        }
                    }
                }));

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OutOfRange DoWork()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        void RefreshComboProgramNo()
        {
            try
            {
                ComboProgramName = new ObservableCollection<OutOfRangeSettingsDipTime>();
                ComboProgramName = IndiSCADABusinessLogic.OutOfRangeLogic.GetORDipTimeInputsProgramName();
            }
            catch(Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OutOfRange RefreshComboProgramNo()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }

        }
        void RefreshEditProgramData()
        {
            try
            {
                if (selectedprogramNo != null && selectedprogramNo != "")
                {
                    DipTimeCollectionEdit = new ObservableCollection<OutOfRangeSettingsDipTime>();
                    OutOfRangeSettingsDipTime objDipTimeValue = new OutOfRangeSettingsDipTime();
                    objDipTimeValue.ProgramNo = selectedprogramNo;
                    DipTimeCollectionEdit = IndiSCADABusinessLogic.OutOfRangeLogic.GetORDipTimeInputsForedit(objDipTimeValue.ProgramNo);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OutOfRange RefreshComboProgramNo()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                ErrorLogger.LogError.ErrorLog("OutOfRange DispatcherTickEvent()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        #endregion
        #region Public/Private Property
        private ObservableCollection<OutOfRangesSettingsEntitypH> _pHCollection = new ObservableCollection<OutOfRangesSettingsEntitypH>();
        public ObservableCollection<OutOfRangesSettingsEntitypH> pHCollection
        {
            get { return _pHCollection; }
            set { _pHCollection = value; OnPropertyChanged("pHCollection"); }
        }
        private string _selectedprogramNo ;
        public string selectedprogramNo
        {
            get { return _selectedprogramNo; }
            set { _selectedprogramNo = value; OnPropertyChanged("selectedprogramNo"); RefreshEditProgramData(); }
        }
        private ObservableCollection<OutOfRangeSettingsDipTime> _ComboProgramName = new ObservableCollection<OutOfRangeSettingsDipTime>();
        public ObservableCollection<OutOfRangeSettingsDipTime> ComboProgramName
        {
            get { return _ComboProgramName; }
            set { _ComboProgramName = value; OnPropertyChanged("ComboProgramName"); }
        }
        private bool _ispHEdit = new bool();
        public bool ispHEdit
        {
            get { return _ispHEdit; }
            set { _ispHEdit = value; OnPropertyChanged("ispHEdit"); }
        }
        private string _ProgramNameText ;
        public string ProgramNameText
        {
            get { return _ProgramNameText; }
            set { _ProgramNameText = value; OnPropertyChanged("ProgramNameText"); }
        }
        private ObservableCollection<OutOfRangeSettingsDipTime> _DipTimeCollectionNew = new ObservableCollection<OutOfRangeSettingsDipTime>();
        public ObservableCollection<OutOfRangeSettingsDipTime> DipTimeCollectionNew
        {
            get { return _DipTimeCollectionNew; }
            set { _DipTimeCollectionNew = value; OnPropertyChanged("DipTimeCollectionNew"); }
        }
        private ObservableCollection<OutOfRangeSettingsDipTime> _DipTimeCollectionEdit = new ObservableCollection<OutOfRangeSettingsDipTime>();
        public ObservableCollection<OutOfRangeSettingsDipTime> DipTimeCollectionEdit
        {
            get { return _DipTimeCollectionEdit; }
            set { _DipTimeCollectionEdit = value; OnPropertyChanged("DipTimeCollectionEdit"); }
        }
        private ObservableCollection<OutOfRangesSettingsEntityTemp> _TempCollection = new ObservableCollection<OutOfRangesSettingsEntityTemp>();
        public ObservableCollection<OutOfRangesSettingsEntityTemp> TempCollection
        {
            get { return _TempCollection; }
            set { _TempCollection = value; OnPropertyChanged("TempCollection"); }
        }
        //
        private ObservableCollection<AlarmTimeEntity> _CTAlarmTime = new ObservableCollection<AlarmTimeEntity>();
        public ObservableCollection<AlarmTimeEntity> CTAlarmTime
        {
            get { return _CTAlarmTime; }
            set { _CTAlarmTime = value; OnPropertyChanged("CTAlarmTime"); }
        }
        //
       
        private bool _isTempEdit = new bool();
        public bool isTempEdit
        {
            get { return _isTempEdit; }
            set { _isTempEdit = value; OnPropertyChanged("isTempEdit"); }
        }
        #endregion
    }
}
