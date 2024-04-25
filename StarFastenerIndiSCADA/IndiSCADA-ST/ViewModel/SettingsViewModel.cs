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
    public class SettingsViewModel : BaseViewModel
    {

        #region"Declaration"
        DispatcherTimer DispatchTimerView = new DispatcherTimer();
        System.ComponentModel.BackgroundWorker _BackgroundWorkerView = new System.ComponentModel.BackgroundWorker();
        //bool isSettingStartup = false;

        #endregion

        #region ICommand

        
        private readonly ICommand _UpdateSetPh;
        public ICommand UpdateSetPh
        {
            get { return _UpdateSetPh; }
        }

        private readonly ICommand _NitricStationSelectionOnOFF;
        public ICommand NitricStationSelectionOnOFF
        {
            get { return _NitricStationSelectionOnOFF; }
        }

        //wcs commands
        private readonly ICommand _UpdateWagon1WCSSP;
        public ICommand UpdateWagon1WCSSP
        {
            get { return _UpdateWagon1WCSSP; }
        }
        private readonly ICommand _UpdateWagon2WCSSP;
        public ICommand UpdateWagon2WCSSP
        {
            get { return _UpdateWagon2WCSSP; }
        }
        private readonly ICommand _UpdateWagon3WCSSP;
        public ICommand UpdateWagon3WCSSP
        {
            get { return _UpdateWagon3WCSSP; }
        }
        private readonly ICommand _UpdateWagon4WCSSP;
        public ICommand UpdateWagon4WCSSP
        {
            get { return _UpdateWagon4WCSSP; }
        }
        private readonly ICommand _UpdateWagon5WCSSP;
        public ICommand UpdateWagon5WCSSP
        {
            get { return _UpdateWagon5WCSSP; }
        }
        private readonly ICommand _UpdateWagon6WCSSP;
        public ICommand UpdateWagon6WCSSP
        {
            get { return _UpdateWagon6WCSSP; }
        }
        private readonly ICommand _UpdateWagon7WCSSP;
        public ICommand UpdateWagon7WCSSP
        {
            get { return _UpdateWagon7WCSSP; }
        }


        private readonly ICommand _RectifierLowSP;
        public ICommand RectifierLowSP
        {
            get { return _RectifierLowSP; }
        }
        private readonly ICommand _RectifierHighSP;
        public ICommand RectifierHighSP
        {
            get { return _RectifierHighSP; }
        }

        private readonly ICommand _RefreshStationSet;
        public ICommand RefreshStationSet
        {
            get { return _RefreshStationSet; }
        }
        private readonly ICommand _StationSelectionOnOFF;
        public ICommand StationSelectionOnOFF
        {
            get { return _StationSelectionOnOFF; }
        }
        private readonly ICommand _WriteStationSelectionValue;//
        public ICommand WriteStationSelectionValue
        {
            get { return _WriteStationSelectionValue; }
        }
        private readonly ICommand _Exit;
        public ICommand Exit
        {
            get { return _Exit; }
        }
        private readonly ICommand _BaseBarrelMotorONOFF;
        public ICommand BaseBarrelMotorONOFF
        {
            get { return _BaseBarrelMotorONOFF; }
        }
        private readonly ICommand _RectifierAutoManual;
        public ICommand RectifierAutoManual
        {
            get { return _RectifierAutoManual; }
        }
        private readonly ICommand _RectifierampReset;
        public ICommand RectifierampReset
        {
            get { return _RectifierampReset; }
        }
        private readonly ICommand _RectifierOnOff;
        public ICommand RectifierOnOff
        {
            get { return _RectifierOnOff; }
        }
        private readonly ICommand _UpdateManualCurrent;
        public ICommand UpdateManualCurrent
        {
            get { return _UpdateManualCurrent; }
        }

        private readonly ICommand _UpdateCalculatedCurrent;
        public ICommand UpdateCalculatedCurrent
        {
            get { return _UpdateCalculatedCurrent; }
        }
        private readonly ICommand _StopDispatcherTimer;
        public ICommand StopDispatcherTimer
        {
            get { return _StopDispatcherTimer; }
        }
        private readonly ICommand _UpdateTempHighSP;
        public ICommand UpdateTempHighSP
        {
            get { return _UpdateTempHighSP; }
        }
        private readonly ICommand _UpdateTempLowSP;
        public ICommand UpdateTempLowSP
        {
            get { return _UpdateTempLowSP; }
        }
        private readonly ICommand _UpdateTempActualSP;
        public ICommand UpdateTempActualSP
        {
            get { return _UpdateTempActualSP; }
        }
        private readonly ICommand _StopTempRefresh;
        public ICommand StopTempRefresh
        {
            get { return _StopTempRefresh; }
        }
        private readonly ICommand _UpdatepHHighSP;
        public ICommand UpdatepHHighSP
        {
            get { return _UpdatepHHighSP; }
        }
        private readonly ICommand _UpdatepHLowSP;
        public ICommand UpdatepHLowSP
        {
            get { return _UpdatepHLowSP; }
        }
        private readonly ICommand _UpdatepHActualSP;
        public ICommand UpdatepHActualSP
        {
            get { return _UpdatepHActualSP; }
        }
        private readonly ICommand _StoppHRefresh;
        public ICommand StoppHRefresh
        {
            get { return _StoppHRefresh; }
        }
        private readonly ICommand _BarrelMotorOnOFF;
        public ICommand BarrelMotorOnOFF
        {
            get { return _BarrelMotorOnOFF; }
        }
        private readonly ICommand _OilSkimmerONOFF;
        public ICommand OilSkimmerONOFF
        {
            get { return _OilSkimmerONOFF; }
        }

        #region"Top Spray-----------------------------------------------"
        private readonly ICommand _TopSprayManualOnOFF;
        public ICommand TopSprayManualOnOFF
        {
            get { return _TopSprayManualOnOFF; }
        }
        private readonly ICommand _TopSprayServiceOnOFF;
        public ICommand TopSprayServiceOnOFF
        {
            get { return _TopSprayServiceOnOFF; }
        }
        private readonly ICommand _TopSprayAutoManual;
        public ICommand TopSprayAutoManual
        {
            get { return _TopSprayAutoManual; }
        }
        private readonly ICommand _DosingAutoManual;
        public ICommand DosingAutoManual
        {
            get { return _DosingAutoManual; }
        }
        private readonly ICommand _DosingManualONOFF;
        public ICommand DosingManualONOFF
        {
            get { return _DosingManualONOFF; }
        }
        private readonly ICommand _DosingTimerFlowrateBased;
        public ICommand DosingTimerFlowrateBased
        {
            get { return _DosingTimerFlowrateBased; }
        }
        private readonly ICommand _UpdateDosingQuantity;
        public ICommand UpdateDosingQuantity
        {
            get { return _UpdateDosingQuantity; }
        }
        private readonly ICommand _UpdateDosingFlowRate;
        public ICommand UpdateDosingFlowRate
        {
            get { return _UpdateDosingFlowRate; }
        }
        private readonly ICommand _UpdateDosingTime;
        public ICommand UpdateDosingTime
        {
            get { return _UpdateDosingTime; }
        }
        private readonly ICommand _UpdateDosingAmp;
        public ICommand UpdateDosingAmp
        {
            get { return _UpdateDosingAmp; }
        }
        #endregion 
        #region"Mechanical Agitation-----------------------------------------------"
        private readonly ICommand _MechAgitationManualOnOFF;
        public ICommand MechAgitationManualOnOFF
        {
            get { return _MechAgitationManualOnOFF; }
        }
        private readonly ICommand _MechAgitationAutoMan;
        public ICommand MechAgitationAutoMan
        {
            get { return _MechAgitationAutoMan; }
        }
        private readonly ICommand _MechAgitationServiceOnOFF;
        public ICommand MechAgitationServiceOnOFF
        {
            get { return _MechAgitationServiceOnOFF; }
        }
        #endregion
        #region"Filter Pump-----------------------------------------------"
        private readonly ICommand _FilterPumpOnOFF;
        public ICommand FilterPumpOnOFF
        {
            get { return _FilterPumpOnOFF; }
        }
        #endregion
        #region"Tank Bypass-----------------------------------------------"
        private readonly ICommand _TankBypassOnOFF;
        public ICommand TankBypassOnOFF
        {
            get { return _TankBypassOnOFF; }
        }
        private readonly ICommand _TrayBypassOnOFF;
        public ICommand TrayBypassOnOFF
        {
            get { return _TrayBypassOnOFF; }
        }

        private readonly ICommand _BarrelBypassOnOFF;
        public ICommand BarrelBypassOnOFF
        {
            get { return _BarrelBypassOnOFF; }
        }

        #endregion
        #region "Utility ........................................."
        private readonly ICommand _UtilityManualOnOFF;
        public ICommand UtilityManualOnOFF
        {
            get { return _UtilityManualOnOFF; }
        }

        #endregion
        #region out of range -----------------------------------

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

        #endregion

        #region"Destructor"
        ~SettingsViewModel()
        {
            try
            {
                DispatchTimerView.Stop();
            }
            catch { }
        }
        #endregion
    
        #region "Consrtuctor"
        public SettingsViewModel()
        {
            try
            {
                IndiSCADAGlobalLibrary.TagList.SettingScreenOpen = true;

                _BackgroundWorkerView.DoWork += DoWork;
                DispatchTimerView.Interval = TimeSpan.FromSeconds(1);
                DispatchTimerView.Tick += DispatcherTickEvent;
                DispatchTimerView.Start();
                _Exit = new RelayCommand(ExitButtonCommandClick);
                _BaseBarrelMotorONOFF = new RelayCommand(BaseBarrelMotorONOFFClick);
                _RectifierOnOff = new RelayCommand(RectifierOnOFFClick);
                _RectifierAutoManual = new RelayCommand(RectifierAutoManClick);
                _UpdateManualCurrent = new RelayCommand(UpdateManualCurrentClick);
                _UpdateCalculatedCurrent = new RelayCommand(UpdateCalculatedCurrentClick);
                _UpdateTempHighSP = new RelayCommand(UpdateTemperatureHighSPClick);
                _StopDispatcherTimer = new RelayCommand(StopDispatcherTimerClick);
                _StopTempRefresh = new RelayCommand(StopTempDataRefreshClick);
                _UpdateTempLowSP = new RelayCommand(UpdateTemperatureLowSPClick);
                _UpdateTempActualSP = new RelayCommand(UpdateTemperaturActualSPClick);
                _TopSprayManualOnOFF = new RelayCommand(TopSprayManualONOFFClick);
                _TopSprayAutoManual = new RelayCommand(TopSprayAutoManualClick);
                _TopSprayServiceOnOFF = new RelayCommand(TopSprayServiceONOFFClick);
                _MechAgitationServiceOnOFF = new RelayCommand(AgitationServiceONOFFClick);
                _MechAgitationAutoMan = new RelayCommand(AgitationAutoManualClick);
                _MechAgitationManualOnOFF = new RelayCommand(AgitationManualONOFFClick);
                _FilterPumpOnOFF = new RelayCommand(FilterPumpManualONOFFClick);
                _UtilityManualOnOFF = new RelayCommand(UtilityManualONOFFClick);
                _TankBypassOnOFF = new RelayCommand(TankBypassONOFFClick);
                _TrayBypassOnOFF = new RelayCommand(TrayBypassONOFFClick);
                _BarrelBypassOnOFF = new RelayCommand(BarrelBypassOnOFFClick);
                _RectifierampReset = new RelayCommand(RectifierresetamphrClick);
                _OilSkimmerONOFF = new RelayCommand(OilSkimmerONOFFclick);
                _UpdatepHHighSP = new RelayCommand(UpdatepHHighSPClick);
                _UpdatepHLowSP = new RelayCommand(UpdatepHLowSPClick);
                _NitricStationSelectionOnOFF = new RelayCommand(NitricStationSelectionClick);
                //WCS
                _UpdateWagon1WCSSP = new RelayCommand(UpdateWagon1WCSClick);
                _UpdateWagon2WCSSP = new RelayCommand(UpdateWagon2WCSClick);
                _UpdateWagon3WCSSP = new RelayCommand(UpdateWagon3WCSClick);
                _UpdateWagon4WCSSP = new RelayCommand(UpdateWagon4WCSClick);
                _UpdateWagon5WCSSP = new RelayCommand(UpdateWagon5WCSClick);
                _UpdateWagon6WCSSP = new RelayCommand(UpdateWagon6WCSClick);
                _UpdateWagon7WCSSP = new RelayCommand(UpdateWagon7WCSClick);

                _DosingAutoManual = new RelayCommand(DosingAutoManualClick);
                _DosingManualONOFF = new RelayCommand(DosingManualONOFFClick);
                _DosingTimerFlowrateBased = new RelayCommand(DosingTimerFlowrateBasecClick);


                _UpdateDosingQuantity = new RelayCommand(UpdateDosingQuantityClick);
                _UpdateDosingFlowRate = new RelayCommand(UpdateDosingFlowRateClick);
                _UpdateDosingTime = new RelayCommand(UpdateDosingSetTimeClick);
                _UpdateDosingAmp = new RelayCommand(UpdateDosingSetAmpClick);
                _UpdateSetPh = new RelayCommand(UpdateDosingSetPHClick);

                _RectifierLowSP = new RelayCommand(RectifierLowSPClick);
                _RectifierHighSP = new RelayCommand(RectifierHighSPClick);

                _StationSelectionOnOFF = new RelayCommand(StationSelectionONOFFClick);
                _WriteStationSelectionValue = new RelayCommand(SetStationSelectionValueClick);
                _RefreshStationSet = new RelayCommand(RefreshStationSetClick);
                               
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
                _NewProgramName = new RelayCommand(NewProgramNameClick);
                _DipTimeLowBypass = new RelayCommand(DipTimeLowBypassClick);
                _DipTimeHighBypass = new RelayCommand(DipTimeHighBypassClick);
                _EditDipTime = new RelayCommand(DipTimeEditClick);
                _DeleteDipTime = new RelayCommand(DipTimeDeleteClick);

                _DipTimeLowBypassEdit = new RelayCommand(DipTimeLowBypassClickEdit);
                _DipTimeHighBypassEdit = new RelayCommand(DipTimeHighBypassClickEdit);

                ProgramNameText = "";

                SelectedSettingTab = 0;

                //RefreshComboProgramNo();
                //isSettingStartup = true;

                RectifierIntputs = IndiSCADABusinessLogic.SettingLogic.GetRectifierInputs();

                //WagonIntputs = IndiSCADABusinessLogic.SettingLogic.GetWagonInputs();
                //Wagon2Intputs = IndiSCADABusinessLogic.SettingLogic.GetWagon2Inputs();
                //Wagon3Intputs = IndiSCADABusinessLogic.SettingLogic.GetWagon3Inputs();
                //Wagon4Intputs = IndiSCADABusinessLogic.SettingLogic.GetWagon4Inputs();
                //Wagon5Intputs = IndiSCADABusinessLogic.SettingLogic.GetWagon5Inputs();
                //Wagon6Intputs = IndiSCADABusinessLogic.SettingLogic.GetWagon6Inputs();
                //Wagon7Intputs = IndiSCADABusinessLogic.SettingLogic.GetWagon7Inputs();                 

                //TemperatureIntputs = IndiSCADABusinessLogic.SettingLogic.GetTemperatureInputs();
                //pHIntputs = IndiSCADABusinessLogic.SettingLogic.GetpHInputs();
                 
                //TankBypassInputs = IndiSCADABusinessLogic.SettingLogic.GetTankBypassInputs();
                //TrayBypassInputs = IndiSCADABusinessLogic.SettingLogic.GetTrayBypassInputs();
                //FilterPumpData = IndiSCADABusinessLogic.SettingLogic.GetFilterPumpInputs();
                //BarrelBypassInputs = IndiSCADABusinessLogic.SettingLogic.GetBarrelBypassInputs();
                //NitricStationSelection = IndiSCADABusinessLogic.SettingLogic.GetNitricSelection();

                //BaseBarrelMotorIntputs = IndiSCADABusinessLogic.SettingLogic.GetBaseBarrelMotorInputs();
                //BaseBarrelMotorIntputs2 = IndiSCADABusinessLogic.SettingLogic.GetBaseBarrelMotorInputs2();

                //BaseOilSkimmerrIntputs = IndiSCADABusinessLogic.SettingLogic.GetOilSkimmerMotorInputs();

                //DosingIntputs = IndiSCADABusinessLogic.SettingLogic.GetDosingPumpInputs();

                //pHCollection = IndiSCADABusinessLogic.OutOfRangeLogic.GetORpHInputs();
                //TempCollection = IndiSCADABusinessLogic.OutOfRangeLogic.GetORTemperatureInputs();
                //DipTimeCollectionNew = IndiSCADABusinessLogic.OutOfRangeLogic.GetORDipTimeInputs();





                //BarrelMotorDATA = IndiSCADABusinessLogic.SettingLogic.GetBarrelMotorInputs();
                //TopSprayData = IndiSCADABusinessLogic.SettingLogic.GetTopSprayInputs();
                //UtilityData = IndiSCADABusinessLogic.SettingLogic.GetUtilityInputs();
                //AgitationData = IndiSCADABusinessLogic.SettingLogic.GetMechanicalAgitationInputs();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel () constructor", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        #endregion

        #region "public/private methods"
        #region "Top Spray methods"
        private void TopSprayAutoManualClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("SprayPumpInputAutoManual", index);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel TopSprayAutoManualClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        private void TopSprayManualONOFFClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("TopSprayOnOFF", index);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel TopSprayManualONOFFClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        private void TopSprayServiceONOFFClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("TopSprayServiceOnOFF", index);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel TopSprayServiceONOFFClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        #endregion

        #region "Filter Pump methods"
        private void FilterPumpManualONOFFClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("FilterPumpOnOFF", index);

                    //Get Dosing pump Dosing AutoManual from PLC
                    string[] DosingAM = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("FilterPumpOnOFF");
                    int DosingIndex = 0;
                    if (DosingAM != null)
                    {
                        foreach (var item in DosingAM)
                        {
                            FilterPumpData[DosingIndex].ManualOnOff = DosingAM[DosingIndex];
                            DosingIndex = DosingIndex + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel FilterPumpManualONOFFClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        #endregion

        #region "Tank Bypass methods"
        private void StationSelectionONOFFClick(object _Index)
        {
            try
            {
                //if (_Index != null)//Index is an tag address position of array
                {
                    int index = 0; //Convert.ToInt32(_Index);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFMomentary("StationSelectionEnter", index);

                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel TrayBypassONOFFClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        private void SetStationSelectionValueClick(object _commandparameters)
        {
            try
            {
                if (Convert.ToInt32(StationSelection.ToString()) >= 25 && Convert.ToInt32(StationSelection.ToString()) <= 42)
                {
                    IndiSCADABusinessLogic.SettingLogic.WriteValueToPLC("StationSelection", 0, StationSelection);

                    StationSelection = IndiSCADABusinessLogic.SettingLogic.GetStationSelectionValue();
                    StationToBeOperated = IndiSCADABusinessLogic.SettingLogic.GetStationOperatedValue();
                    StationEnter = IndiSCADABusinessLogic.SettingLogic.GetStationEnterValue();
                }
                else
                {
                    MessageBox.Show("Range for station number is between 25 to 42");
                }
                isStationEdit = true;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeViewModel SetCycleTimeButtonCommandClick()", DateTime.Now.ToString(), ex.Message, "No", true);
                isStationEdit = true;
            }
            isStationEdit = true;
        }
        private void RefreshStationSetClick(object _Index)
        {
            try
            {
                isStationEdit = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel RefreshStationSetClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
            isStationEdit = false;
        }


        private void TankBypassONOFFClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("BypassplatingTankBypassPlating", index);

                    //Get Dosing pump Dosing AutoManual from PLC
                    string[] DosingAM = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("BypassplatingTankBypassPlating");
                    int DosingIndex = 0;
                    if (DosingAM != null)
                    {
                        foreach (var item in DosingAM)
                        {
                            TankBypassInputs[DosingIndex].BypassOnOff = DosingAM[DosingIndex];
                            DosingIndex = DosingIndex + 1;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel TankBypassONOFFClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        private void TrayBypassONOFFClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("TrayBypassBypass", index);

                    //Get Dosing pump Dosing AutoManual from PLC
                    string[] DosingAM = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("TrayBypassBypass");
                    int DosingIndex = 0;
                    if (DosingAM != null)
                    {
                        foreach (var item in DosingAM)
                        {
                            TrayBypassInputs[DosingIndex].BypassOnOff = DosingAM[DosingIndex];
                            DosingIndex = DosingIndex + 1;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel TrayBypassONOFFClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }

        private void BarrelBypassOnOFFClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("DryerBypass", index);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel BarrelBypassOnOFFClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }

        #endregion

        #region "Mechanical Agitation methods"
        private void AgitationManualONOFFClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("MechAgitationOnOFF", index);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel AgitationManualONOFFClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        private void AgitationServiceONOFFClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("MechAgitationServiceOnOFF", index);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel AgitationServiceONOFFClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        private void AgitationAutoManualClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("MechAgitationAutoManual", index);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel AgitationAutoManualClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        #endregion

        #region "Dsoing methods"

        private void DosingAutoManualClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("DOSINGAutoOrManual", index);

                    //Get Dosing pump Dosing AutoManual from PLC
                    string[] DosingAM = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("DOSINGAutoOrManual");
                    int DosingIndex = 0;
                    if (DosingAM != null)
                    {
                        foreach (var item in DosingAM)
                        {
                            DosingIntputs[DosingIndex].AutoManual = DosingAM[DosingIndex];
                            DosingIndex = DosingIndex + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel DosingAutoManualClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }

        private void DosingManualONOFFClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("DOSINGManualOffOrOn", index);

                    //Get Dosing pump Dosing AutoManual from PLC
                    string[] DosingAM = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("DOSINGManualOffOrOn");
                    int DosingIndex = 0;
                    if (DosingAM != null)
                    {
                        foreach (var item in DosingAM)
                        {
                            DosingIntputs[DosingIndex].ManualONOFF = DosingAM[DosingIndex];
                            DosingIndex = DosingIndex + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel DosingManualONOFFClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        private void DosingTimerFlowrateBasecClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("DOSINGTimerBasedOrFlowrateBased", index);

                    //Get Dosing pump Dosing AutoManual from PLC
                    string[] DosingAM = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("DOSINGTimerBasedOrFlowrateBased");
                    int DosingIndex = 0;
                    if (DosingAM != null)
                    {
                        foreach (var item in DosingAM)
                        {
                            DosingIntputs[DosingIndex].FlowRateTimerBased = DosingAM[DosingIndex];
                            DosingIndex = DosingIndex + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel DosingTimerFlowrateBasecClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }

        private void UpdateDosingQuantityClick(object _Index)
        {
            try
            {
                isTemperatureDataEdit = true;//stop refresh data 
                if (_Index != null)//Dosing index is  an index value of tag
                {
                    int index = Convert.ToInt32(_Index);
                    DosingSettingsEntity _Data = DosingIntputs[index];
                    index = index * 2;
                    IndiSCADABusinessLogic.SettingLogic.WriteValueToPLCReal("DosingQuantityInml", index, _Data.Quantity);

                    //Get Dosing pump Dosing AutoManual from PLC
                    string[] DosingAM = DeviceCommunication.CommunicationWithPLC.Real_ReadPLCTagValue("DosingQuantityInml");
                    int DosingIndex = 0;
                    if (DosingAM != null)
                    {
                        foreach (var item in DosingAM)
                        {
                            DosingIntputs[DosingIndex].Quantity = DosingAM[DosingIndex];
                            DosingIndex = DosingIndex + 1;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel UpdateDosingQuantityClick()", DateTime.Now.ToString(), ex.Message, "No", true);
                isTemperatureDataEdit = false;
            }
            isTemperatureDataEdit = false;
        }
        private void UpdateDosingFlowRateClick(object _Index)
        {
            try
            {
                isTemperatureDataEdit = true;//stop refresh data 
                if (_Index != null)//Dosing index is  an index value of tag
                {
                    int index = Convert.ToInt32(_Index);
                    DosingSettingsEntity _Data = DosingIntputs[index];
                    index = index * 2;
                    IndiSCADABusinessLogic.SettingLogic.WriteValueToPLCReal("DosingFlowRatemlperSec", index, _Data.FlowRate);

                    //Get Dosing pump Dosing AutoManual from PLC
                    string[] DosingAM = DeviceCommunication.CommunicationWithPLC.Real_ReadPLCTagValue("DosingFlowRatemlperSec");
                    int DosingIndex = 0;
                    if (DosingAM != null)
                    {
                        foreach (var item in DosingAM)
                        {
                            DosingIntputs[DosingIndex].FlowRate = DosingAM[DosingIndex];
                            DosingIndex = DosingIndex + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel UpdateDosingFlowRateClick()", DateTime.Now.ToString(), ex.Message, "No", true);
                isTemperatureDataEdit = false;
            }
            isTemperatureDataEdit = false;
        }
        private void UpdateDosingSetTimeClick(object _Index)
        {
            try
            {
                isTemperatureDataEdit = true;//stop refresh data 
                if (_Index != null)//Dosing index is  an index value of tag
                {
                    int index = Convert.ToInt32(_Index);
                    DosingSettingsEntity _Data = DosingIntputs[index];
                    //index = index * 2;
                    IndiSCADABusinessLogic.SettingLogic.WriteValueToPLC("DosingTimeInSec", index, _Data.SetTime);

                    //Get Dosing pump Dosing AutoManual from PLC
                    string[] DosingAM = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("DosingTimeInSec");
                    int DosingIndex = 0;
                    if (DosingAM != null)
                    {
                        foreach (var item in DosingAM)
                        {
                            DosingIntputs[DosingIndex].SetTime = DosingAM[DosingIndex];
                            DosingIndex = DosingIndex + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel UpdateDosingSetTimeClick()", DateTime.Now.ToString(), ex.Message, "No", true);
                isTemperatureDataEdit = false;
            }
            isTemperatureDataEdit = false;
        }

        private void UpdateDosingSetAmpClick(object _Index)
        {
            isTemperatureDataEdit = true;
            try
            {
                if (_Index != null) //Dosing index is  an index value of tag //in this project dosings are 9 but set and actual amp hr addresses are only 6
                {
                    int index = Convert.ToInt32(_Index);
                
                        DosingSettingsEntity _Data = DosingIntputs[index];
                        index = index * 2;
                        IndiSCADABusinessLogic.SettingLogic.WriteValueToPLCReal("DosingSetAmpHr", index, _Data.SetAmp);
                  

                    //Get Dosing pump Dosing AutoManual from PLC
                    string[] DosingAM = DeviceCommunication.CommunicationWithPLC.Real_ReadPLCTagValue("DosingSetAmpHr");
                    int DosingIndex = 0;
                    if (DosingAM != null)
                    {
                        foreach (var item in DosingAM)
                        {
                            DosingIntputs[DosingIndex].SetAmp = DosingAM[DosingIndex];
                            DosingIndex = DosingIndex + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel UpdateDosingSetAmpClick()", DateTime.Now.ToString(), ex.Message, "No", true);
                isTemperatureDataEdit = false;
            }
            isTemperatureDataEdit = false;
        }

        private void UpdateDosingSetPHClick(object _Index)
        {
            try
            {
                isTemperatureDataEdit = true;//stop refresh data 
                if (_Index != null)//Dosing index is  an index value of tag
                {
                    int index = Convert.ToInt32(_Index);
                    DosingSettingsEntity _Data = DosingIntputs[index];

                   float SetPh= float.Parse(_Data.setPH) * 100;

                    if (index == 12)
                    { IndiSCADABusinessLogic.SettingLogic.WriteValueToPLC("DosingSetPH", 0, SetPh.ToString()); }
                    else if (index == 13)
                    { IndiSCADABusinessLogic.SettingLogic.WriteValueToPLC("DosingSetPH", 1, SetPh.ToString()); }
                    else if (index == 16)
                    { IndiSCADABusinessLogic.SettingLogic.WriteValueToPLC("DosingSetPH", 2, SetPh.ToString()); }
                    else if (index == 19)
                    { IndiSCADABusinessLogic.SettingLogic.WriteValueToPLC("DosingSetPH", 3, SetPh.ToString()); }
                    

                    //Get Dosing pump Dosing AutoManual from PLC
                    string[] DosingAM = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("DosingSetPH");
                    int DosingIndex = 0;
                    if (DosingAM != null)
                    {
                        foreach (var item in DosingAM)
                        {
                            if (index == 12)
                            { DosingIntputs[DosingIndex].setPH = DosingAM[0]; }
                            else if (index == 13)
                            { DosingIntputs[DosingIndex].setPH = DosingAM[1]; }
                            else if (index == 16)
                            { DosingIntputs[DosingIndex].setPH = DosingAM[2]; }
                            else if (index == 19)
                            { DosingIntputs[DosingIndex].setPH = DosingAM[3]; }

                            DosingIndex = DosingIndex + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel UpdateDosingSetPHClick()", DateTime.Now.ToString(), ex.Message, "No", true);
                isTemperatureDataEdit = false;
            }
            isTemperatureDataEdit = false;
        }

        
        #endregion

        #region "Utility methods"
        private void UtilityAutoManualClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("UtilityAutoManual", index);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel UtilityAutoManualClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        private void UtilityManualONOFFClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("BarrelMotors", index);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel UtilityManualONOFFClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        private void UtilityServiceONOFFClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("UtilityServiceOnOFF", index);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel UtilityServiceONOFFClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        #endregion

        #region Out of range Methods

        private void StopTempClick(object _commandparameters)
        {
            try
            {
                isTempEdit = true;
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
                ispHEdit = true;
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
                isTempEdit = true;
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
                    IndiSCADABusinessLogic.OutOfRangeLogic.WriteValueToPLC("TempControllerTemperatureControllerSetPoint", index, _pHEntity.SetPointTemp);
                    isTempEdit = false;
                }
            }
            catch (Exception exExitButtonCommandClick1)
            {
                ErrorLogger.LogError.ErrorLog("OutOfRange UpdateSPTempClick()", DateTime.Now.ToString(), exExitButtonCommandClick1.Message, "No", true);
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

                if (ProgramNameText != "" && ProgramNameText.Length > 0)
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
                    catch (Exception ex)
                    {

                    }

                    if (DipTimeCollectionNew != null)
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
                    if (_DipTimeEntity.LowBypass == "0")
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
        void RefreshComboProgramNo()
        {
            try
            {
                ComboProgramName = new ObservableCollection<OutOfRangeSettingsDipTime>();
                ComboProgramName = IndiSCADABusinessLogic.OutOfRangeLogic.GetORDipTimeInputsProgramName();
            }
            catch (Exception ex)
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

        #endregion

        #region "Base Barrel Motor methods" 
        private void BaseBarrelMotorONOFFClick(object _Index)
        {
            try
            {
                if (_Index != null)//Index is an tag address position of array
                {
                    int index = Convert.ToInt32(_Index);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("BaseBarrelMotorONOFF", index);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel BaseBarrelMotorONOFFClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        #endregion


        #region WCS methods

        private void UpdateWagon1WCSClick(object _TemperatureIndex)
        {
            try
            {
                if (_TemperatureIndex != null)//_TemperatureIndex is  an index value of tag
                {
                    int index = Convert.ToInt32(_TemperatureIndex);
                    IOStatusEntity _TempData = WagonIntputs[index];
                     
                    {
                        index = index * 2;
                        IndiSCADABusinessLogic.SettingLogic.WriteValueToPLCWord("Wagon1WCSInput", index, _TempData.Value);
                    }

                    //Get Dosing pump Dosing AutoManual from PLC
                    string[] DosingAM = DeviceCommunication.CommunicationWithPLC.Word_ReadPLCTagValue("Wagon1WCSInput");
                    int DosingIndex = 0;
                    if (DosingAM != null)
                    {
                        foreach (var item in DosingAM)
                        {
                            WagonIntputs[DosingIndex].Value = DosingAM[DosingIndex];
                            DosingIndex = DosingIndex + 1;
                        }
                    }
                }
                isTemperatureDataEdit = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel UpdateWagon1WCSClick()", DateTime.Now.ToString(), ex.InnerException.ToString(), "No", true);
            }
            isTemperatureDataEdit = false;
        }
        private void UpdateWagon2WCSClick(object _TemperatureIndex)
        {
            try
            {
                if (_TemperatureIndex != null)//_TemperatureIndex is  an index value of tag
                {
                    int index = Convert.ToInt32(_TemperatureIndex);
                    IOStatusEntity _TempData = Wagon2Intputs[index];
                    //if (index == 1)
                    //{
                    //    IndiSCADABusinessLogic.SettingLogic.WriteValueToPLC("WagonSensorWidthInput", 1, _TempData.Value);
                    //}
                    //else
                    {
                        index = index * 2;
                        IndiSCADABusinessLogic.SettingLogic.WriteValueToPLCWord("Wagon2WCSInput", index, _TempData.Value);
                    }
                     
                    string[] DosingAM = DeviceCommunication.CommunicationWithPLC.Word_ReadPLCTagValue("Wagon2WCSInput");
                    int DosingIndex = 0;
                    if (DosingAM != null)
                    {
                        foreach (var item in DosingAM)
                        {
                            Wagon2Intputs[DosingIndex].Value = DosingAM[DosingIndex];
                            DosingIndex = DosingIndex + 1;
                        }
                    }
                }
                isTemperatureDataEdit = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel UpdateWagon2WCSClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
            isTemperatureDataEdit = false;
        }
        private void UpdateWagon3WCSClick(object _TemperatureIndex)
        {
            try
            {
                if (_TemperatureIndex != null)//_TemperatureIndex is  an index value of tag
                {
                    int index = Convert.ToInt32(_TemperatureIndex);
                    IOStatusEntity _TempData = Wagon3Intputs[index];
                    //if (index == 1)
                    //{
                    //    IndiSCADABusinessLogic.SettingLogic.WriteValueToPLC("WagonSensorWidthInput", 2, _TempData.Value);
                    //}
                    //else
                    {
                        index = index * 2;
                        IndiSCADABusinessLogic.SettingLogic.WriteValueToPLCWord("Wagon3WCSInput", index, _TempData.Value);
                    }
                    string[] DosingAM = DeviceCommunication.CommunicationWithPLC.Word_ReadPLCTagValue("Wagon3WCSInput");
                    int DosingIndex = 0;
                    if (DosingAM != null)
                    {
                        foreach (var item in DosingAM)
                        {
                            Wagon3Intputs[DosingIndex].Value = DosingAM[DosingIndex];
                            DosingIndex = DosingIndex + 1;
                        }
                    }
                }
                isTemperatureDataEdit = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel UpdateWagon3WCSClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
            isTemperatureDataEdit = false;
        }
        private void UpdateWagon4WCSClick(object _TemperatureIndex)
        {
            try
            {
                if (_TemperatureIndex != null)//_TemperatureIndex is  an index value of tag
                {
                    int index = Convert.ToInt32(_TemperatureIndex);
                    IOStatusEntity _TempData = Wagon4Intputs[index];
                  
                    {
                        index = index * 2;
                        IndiSCADABusinessLogic.SettingLogic.WriteValueToPLCWord("Wagon4WCSInput", index, _TempData.Value);
                    }

                    string[] DosingAM = DeviceCommunication.CommunicationWithPLC.Word_ReadPLCTagValue("Wagon4WCSInput");
                    int DosingIndex = 0;
                    if (DosingAM != null)
                    {
                        foreach (var item in DosingAM)
                        {
                            Wagon4Intputs[DosingIndex].Value = DosingAM[DosingIndex];
                            DosingIndex = DosingIndex + 1;
                        }
                    }
                }
                isTemperatureDataEdit = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel UpdateWagon4WCSClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
            isTemperatureDataEdit = false;
        }
        private void UpdateWagon5WCSClick(object _TemperatureIndex)
        {
            try
            {
                if (_TemperatureIndex != null)//_TemperatureIndex is  an index value of tag
                {
                    int index = Convert.ToInt32(_TemperatureIndex);
                    IOStatusEntity _TempData = Wagon5Intputs[index];
                    //if (index == 1)
                    //{
                    //    IndiSCADABusinessLogic.SettingLogic.WriteValueToPLC("WagonSensorWidthInput", 4, _TempData.Value);
                    //}
                    //else
                    {
                        index = index * 2;
                        IndiSCADABusinessLogic.SettingLogic.WriteValueToPLCWord("Wagon5WCSInput", index, _TempData.Value);
                    }

                    string[] DosingAM = DeviceCommunication.CommunicationWithPLC.Word_ReadPLCTagValue("Wagon5WCSInput");
                    int DosingIndex = 0;
                    if (DosingAM != null)
                    {
                        foreach (var item in DosingAM)
                        {
                            Wagon5Intputs[DosingIndex].Value = DosingAM[DosingIndex];
                            DosingIndex = DosingIndex + 1;
                        }
                    }
                }
                isTemperatureDataEdit = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel UpdateWagon5WCSClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
            isTemperatureDataEdit = false;
        }
        private void UpdateWagon6WCSClick(object _TemperatureIndex)
        {
            try
            {
                if (_TemperatureIndex != null)//_TemperatureIndex is  an index value of tag
                {
                    int index = Convert.ToInt32(_TemperatureIndex);
                    IOStatusEntity _TempData = Wagon6Intputs[index];
                    //if (index == 1)
                    //{
                    //    IndiSCADABusinessLogic.SettingLogic.WriteValueToPLC("WagonSensorWidthInput", 5, _TempData.Value);
                    //}
                    //else
                    {
                        index = index * 2;
                        IndiSCADABusinessLogic.SettingLogic.WriteValueToPLCWord("Wagon6WCSInput", index, _TempData.Value);
                    }

                    ObservableCollection<IOStatusEntity> Wagon6WCS = IndiSCADABusinessLogic.SettingLogic.GetWagon6Inputs();
                    int INDEX6 = 0;
                    if (Wagon6WCS != null)
                    {
                        foreach (var item in Wagon6WCS)
                        {
                            Wagon6Intputs[INDEX6].ID = Wagon6WCS[INDEX6].ID;
                            Wagon6Intputs[INDEX6].ParameterName = Wagon6WCS[INDEX6].ParameterName;
                            Wagon6Intputs[INDEX6].Value = Wagon6WCS[INDEX6].Value;
                            INDEX6 = INDEX6 + 1;
                        }
                    }
                }
                isTemperatureDataEdit = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel UpdateWagon6WCSClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
            isTemperatureDataEdit = false;
        }
        private void UpdateWagon7WCSClick(object _TemperatureIndex)
        {
            try
            {
                if (_TemperatureIndex != null)//_TemperatureIndex is  an index value of tag
                {
                    int index = Convert.ToInt32(_TemperatureIndex);
                    IOStatusEntity _TempData = Wagon7Intputs[index];
                    //if (index == 1)
                    //{
                    //    IndiSCADABusinessLogic.SettingLogic.WriteValueToPLC("WagonSensorWidthInput", 5, _TempData.Value);
                    //}
                    //else
                    {
                        index = 10;
                        index = index * 2;
                        IndiSCADABusinessLogic.SettingLogic.WriteValueToPLCWord("Wagon6WCSInput", index, _TempData.Value);
                    }

                    ObservableCollection<IOStatusEntity> Wagon7WCS = IndiSCADABusinessLogic.SettingLogic.GetWagon7Inputs();
                    int INDEX7 = 0;
                    if (Wagon7WCS != null)
                    {
                        try
                        {
                            foreach (var item in Wagon7WCS)
                            {
                                Wagon7Intputs[INDEX7].ID = Wagon7WCS[INDEX7].ID;
                                Wagon7Intputs[INDEX7].ParameterName = Wagon7WCS[INDEX7].ParameterName;
                                Wagon7Intputs[INDEX7].Value = Wagon7WCS[INDEX7].Value;
                                INDEX7 = INDEX7 + 1;
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("SettingsViewModel DoWork() GetWagon7Inputs()", DateTime.Now.ToString(), ex.Message, "No", true);
                        }
                    }
                }
                isTemperatureDataEdit = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel UpdateWagon7WCSClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
            isTemperatureDataEdit = false;
        }

        #endregion

        private void StopDispatcherTimerClick(object _commandparameters)
        {
            try
            {
                isRectifierManualCurrentEdit = true;//stop refresh data 
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel StopDispatcherTimerClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void StopTempDataRefreshClick(object _commandparameters)
        {
            try
            {

                isTemperatureDataEdit = true;//stop refresh data 
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel StopTempDataRefreshClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
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
                ErrorLogger.LogError.ErrorLog(" SettingsViewModel ExitButtonCommandClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }


        private string MultiplyBY(string SourceValue, int Multiplyfactor)
        {
            try
            {
                int value = 0;
                Double Fvalue = Convert.ToDouble(SourceValue) * Multiplyfactor;
                value = Convert.ToInt32(Fvalue.ToString());
                return value.ToString();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingviewModel MultiplyBY method by " + SourceValue, DateTime.Now.ToString(), "", null, true);
                return "0";
            }
        }
      
        #region temp write methods
        private void UpdateTemperatureHighSPClick(object _TemperatureIndex)
        {
            try
            {
                if (_TemperatureIndex != null)//_TemperatureIndex is  an index value of tag
                {
                    int index = Convert.ToInt32(_TemperatureIndex);
                    TemperatureSettingEntity _TempData = TemperatureIntputs[index];
                    IndiSCADABusinessLogic.SettingLogic.WriteValueToPLC("TempControllerTemperatureHighSetPoint", index, _TempData.HighSP);
                                        
                    string[] DosingAM = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("TempControllerTemperatureHighSetPoint");
                    int DosingIndex = 0;
                    if (DosingAM != null)
                    {
                        foreach (var item in DosingAM)
                        {
                            TemperatureIntputs[DosingIndex].HighSP = DosingAM[DosingIndex];
                            DosingIndex = DosingIndex + 1;
                        }
                    }
                }
                isTemperatureDataEdit = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel UpdateTemperatureHighSPClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
            isTemperatureDataEdit = false;
        }

        private void UpdateTemperatureLowSPClick(object _TemperatureIndex)
        {
            try
            {
                if (_TemperatureIndex != null)//_TemperatureIndex is  an index value of tag
                {
                    int index = Convert.ToInt32(_TemperatureIndex);
                    TemperatureSettingEntity _TempData = TemperatureIntputs[index];
                    IndiSCADABusinessLogic.SettingLogic.WriteValueToPLC("TempControllerTemperatureLowSetPoint", index, _TempData.LowSP);


                    string[] DosingAM = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("TempControllerTemperatureLowSetPoint");
                    int DosingIndex = 0;
                    if (DosingAM != null)
                    {
                        foreach (var item in DosingAM)
                        {
                            TemperatureIntputs[DosingIndex].LowSP = DosingAM[DosingIndex];
                            DosingIndex = DosingIndex + 1;
                        }
                    }
                }
                isTemperatureDataEdit = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel UpdateTemperatureLowSPClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
            isTemperatureDataEdit = false;
        }
        private void UpdateTemperaturActualSPClick(object _TemperatureIndex)
        {
            try
            {
                if (_TemperatureIndex != null)//_TemperatureIndex is  an index value of tag
                {
                    int index = Convert.ToInt32(_TemperatureIndex);
                    TemperatureSettingEntity _TempData = TemperatureIntputs[index];
                    IndiSCADABusinessLogic.SettingLogic.WriteValueToPLC("TempControllerTemperatureControllerSetPoint", index, _TempData.ActualSP);


                    string[] DosingAM = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("TempControllerTemperatureControllerSetPoint");
                    int DosingIndex = 0;
                    if (DosingAM != null)
                    {
                        foreach (var item in DosingAM)
                        {
                            TemperatureIntputs[DosingIndex].ActualSP = DosingAM[DosingIndex];
                            DosingIndex = DosingIndex + 1;
                        }
                    }
                }
                isTemperatureDataEdit = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel UpdateTemperaturActualSPClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
            isTemperatureDataEdit = false;
        }
        #endregion

        #region pH
        private void UpdatepHHighSPClick(object _pHIndex)
        {
            try
            {
                if (_pHIndex != null)//_TemperatureIndex is  an index value of tag
                {
                    int index = Convert.ToInt32(_pHIndex);
                    pHSettingEntity _pHData = pHIntputs[index];
                    IndiSCADABusinessLogic.SettingLogic.WriteValueToPLC("pHMeterpHHighsetPoint", index, MultiplyBY(_pHData.HighSP, 100));


                    string[] DosingAM = DivideBy(DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("pHMeterpHHighsetPoint"), 100, 5);
                    int DosingIndex = 0;
                    if (DosingAM != null)
                    {
                        foreach (var item in DosingAM)
                        {
                            pHIntputs[DosingIndex].HighSP = DosingAM[DosingIndex];
                            DosingIndex = DosingIndex + 1;
                        }
                    }
                }
                isTemperatureDataEdit = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel UpdatepHHighSPClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
            isTemperatureDataEdit = false;
        }
        private void UpdatepHLowSPClick(object _pHIndex)
        {
            try
            {
                if (_pHIndex != null)//_TemperatureIndex is  an index value of tag
                {
                    int index = Convert.ToInt32(_pHIndex);
                    pHSettingEntity _pHData = pHIntputs[index];
                    IndiSCADABusinessLogic.SettingLogic.WriteValueToPLC("pHMeterpHLowsetPoint", index, MultiplyBY(_pHData.LowSP, 100));

                    string[] DosingAM = DivideBy(DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("pHMeterpHLowsetPoint"), 100, 5);
                    int DosingIndex = 0;
                    if (DosingAM != null)
                    {
                        foreach (var item in DosingAM)
                        {
                            pHIntputs[DosingIndex].LowSP = DosingAM[DosingIndex];
                            DosingIndex = DosingIndex + 1;
                        }
                    }
                }
                isTemperatureDataEdit = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel UpdatepHLowSPClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
            isTemperatureDataEdit = false;
        }
        #endregion

        private void NitricStationSelectionClick(object PreviousSelection)
        {
            try
            { 
                IndiSCADABusinessLogic.SettingLogic.ONOFFButton("NitricTankSelection", 0); 
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel NitricStationSelectionClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        
        //OilSkimmerONOFFclick
        private void OilSkimmerONOFFclick(object _OilSkimmerIndex)
        {
            try
            {
                if (_OilSkimmerIndex != null)
                {
                    int index = Convert.ToInt32(_OilSkimmerIndex);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("OilSkimmerAutoManual", index);

                    //Get Dosing pump Dosing AutoManual from PLC
                    string[] DosingAM = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("OilSkimmerAutoManual");
                    int DosingIndex = 0;
                    if (DosingAM != null)
                    {
                        foreach (var item in DosingAM)
                        {
                            BaseOilSkimmerrIntputs[DosingIndex].BaseOilSkimmerOnOFF = DosingAM[DosingIndex];
                            DosingIndex = DosingIndex + 1;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel OilSkimmerONOFFclick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }

        #region rectifier write methods

        private void RectifierAutoManClick(object _RectifierIndex)
        {
            try
            {
                if (_RectifierIndex != null)//rectifier index is  an index value of tag
                {
                    int index = Convert.ToInt32(_RectifierIndex);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("RECTIFIERAutoOrManual", index);

                    string[] DosingAM = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("RECTIFIERAutoOrManual");
                    int RectIndex = 0;
                    if (DosingAM != null)
                    {
                        foreach (var item in DosingAM)
                        { 
                            RectifierIntputs[RectIndex].AutoManual = DosingAM[RectIndex]; 
                            RectIndex = RectIndex + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel RectifierAutoManClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        private void RectifierOnOFFClick(object _RectifierIndex)
        {
            try
            {
                if (_RectifierIndex != null)//rectifier index is  an index value of tag
                {
                    int index = Convert.ToInt32(_RectifierIndex);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("RECTIFIERManualONOrOFF", index);

                    string[] ManualOnOff = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("RECTIFIERManualONOrOFF");
                    int RectIndex = 0;
                    if (ManualOnOff != null)
                    {
                        foreach (var item in ManualOnOff)
                        {
                            RectifierIntputs[RectIndex].ManualOnOff = ManualOnOff[RectIndex];
                            RectIndex = RectIndex + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel RectifierOnOFFClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }

        private void RectifierLowSPClick(object _RectifierIndex)
        {
            try
            {
                if (_RectifierIndex != null)//rectifier index is  an index value of tag
                {
                    if (_RectifierIndex != null)//rectifier index is  an index value of tag
                    {
                        int index = Convert.ToInt32(_RectifierIndex);
                        RectifierEntity _RectifierData = RectifierIntputs[index];
                        IndiSCADABusinessLogic.SettingLogic.WriteValueToPLC("RectifierLowSP", index, _RectifierData.LowSP);


                        string[] LowSP = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("RectifierLowSP");
                        int RectIndex = 0;
                        if (LowSP != null)
                        {
                            foreach (var item in LowSP)
                            {
                                RectifierIntputs[RectIndex].LowSP = LowSP[RectIndex];
                                RectIndex = RectIndex + 1;
                            }
                        }
                    }
                    isRectifierManualCurrentEdit = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel RectifierLowSPClick()", DateTime.Now.ToString(), ex.Message, "No", true);
                isRectifierManualCurrentEdit = false;
            }
            isRectifierManualCurrentEdit = false;
        }
        private void RectifierHighSPClick(object _RectifierIndex)
        {
            try
            {
                if (_RectifierIndex != null)//rectifier index is  an index value of tag
                {
                    if (_RectifierIndex != null)//rectifier index is  an index value of tag
                    {
                        int index = Convert.ToInt32(_RectifierIndex);
                        RectifierEntity _RectifierData = RectifierIntputs[index];
                        IndiSCADABusinessLogic.SettingLogic.WriteValueToPLC("RectifierHighSP", index, _RectifierData.HighSP);


                        string[] HighSP = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("RectifierHighSP");
                        int RectIndex = 0;
                        if (HighSP != null)
                        {
                            foreach (var item in HighSP)
                            {
                                RectifierIntputs[RectIndex].HighSP = HighSP[RectIndex];
                                RectIndex = RectIndex + 1;
                            }
                        }
                    }
                    isRectifierManualCurrentEdit = false;
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel RectifierHighSPClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
            isRectifierManualCurrentEdit = false;
        }
        
        private void UpdateManualCurrentClick(object _RectifierIndex)
        {
            try
            {
                if (_RectifierIndex != null)//rectifier index is  an index value of tag
                {
                    int index = Convert.ToInt32(_RectifierIndex);
                    RectifierEntity _RectifierData = RectifierIntputs[index];
                    IndiSCADABusinessLogic.SettingLogic.WriteValueToPLC("RectifierManualCurrent", index, _RectifierData.ManualCurrent);


                    string[] ManualCurrent = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("RectifierManualCurrent");
                    int RectIndex = 0;
                    if (ManualCurrent != null)
                    {
                        foreach (var item in ManualCurrent)
                        {
                            RectifierIntputs[RectIndex].ManualCurrent = ManualCurrent[RectIndex];
                            RectIndex = RectIndex + 1;
                        }
                    }
                }
                isRectifierManualCurrentEdit = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel UpdateManualCurrentClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }

        private void UpdateCalculatedCurrentClick(object _RectifierIndex)
        {
            try
            {
                if (_RectifierIndex != null)//rectifier index is  an index value of tag
                {
                    int index = Convert.ToInt32(_RectifierIndex);
                    RectifierEntity _RectifierData = RectifierIntputs[index];
                    IndiSCADABusinessLogic.SettingLogic.WriteValueToPLC("RectifierCalculatedCurrentOrAutoCurrent", index, _RectifierData.Calculated);


                    string[] ManualCurrent = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("RectifierCalculatedCurrentOrAutoCurrent");
                    int RectIndex = 0;
                    if (ManualCurrent != null)
                    {
                        foreach (var item in ManualCurrent)
                        {
                            RectifierIntputs[RectIndex].Calculated = ManualCurrent[RectIndex];
                            RectIndex = RectIndex + 1;
                        }
                    }
                }
                isRectifierManualCurrentEdit = false;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel UpdateCalculatedCurrentClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        private void RectifierresetamphrClick(object _RectifierIndex)
        {
            try
            {
                if (_RectifierIndex != null)//rectifier index is  an index value of tag
                {
                    int index = Convert.ToInt32(_RectifierIndex);
                    IndiSCADABusinessLogic.SettingLogic.ONOFFButton("RECTIFIERResetCumulativeAmpHr", index);
                    //IndiSCADABusinessLogic.SettingLogic.ONOFFButton("RECTIFIERResetCumulativeAmpHr", index);


                    string[] ManualCurrent = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("RECTIFIERResetCumulativeAmpHr");
                    int RectIndex = 0;
                    if (ManualCurrent != null)
                    {
                        foreach (var item in ManualCurrent)
                        {
                            RectifierIntputs[RectIndex].ResetAmpHr = ManualCurrent[RectIndex];
                            RectIndex = RectIndex + 1;
                        }
                    } 
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel RectifierresetamphrClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }

        #endregion

        public static string[] DivideBy(string[] SourceValue, int devidefactor, int index)
        {
            try
            {
                string[] value = new string[SourceValue.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    Double Fvalue = Convert.ToDouble(SourceValue[i]) / devidefactor;
                    value[i] = Fvalue.ToString();
                }
                return value;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC DivideBy method", DateTime.Now.ToString(), "", null, true);
                return null;
            }
        }

        private void DoWork(object Sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                if (IndiSCADAGlobalLibrary.TagList.SettingScreenOpen == true)
                { 
                        TemperatureIntputs = IndiSCADABusinessLogic.SettingLogic.GetTemperatureInputs();
                        pHIntputs = IndiSCADABusinessLogic.SettingLogic.GetpHInputs();
                        DosingIntputs = IndiSCADABusinessLogic.SettingLogic.GetDosingPumpInputs();

                          //temp ph dosing
                            App.Current.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                try
                                {
                                    TemperatureIntputs = IndiSCADABusinessLogic.SettingLogic.GetTemperatureInputs();
                                    ObservableCollection<TemperatureSettingEntity> TemperatureIP = IndiSCADABusinessLogic.SettingLogic.GetTemperatureInputs();
                                    int TempIndex = 0;
                                    if (TemperatureIP != null)
                                    {
                                        foreach (var item in TemperatureIP)
                                        {
                                            TemperatureIntputs[TempIndex].ActualTemperature = TemperatureIP[TempIndex].ActualTemperature;
                                            TemperatureIntputs[TempIndex].ActualSP = TemperatureIP[TempIndex].ActualSP;
                                            TemperatureIntputs[TempIndex].HighSP = TemperatureIP[TempIndex].HighSP;
                                            TemperatureIntputs[TempIndex].LowSP = TemperatureIP[TempIndex].LowSP;
                                            TempIndex = TempIndex + 1;
                                        }
                                    }
                                    ObservableCollection<pHSettingEntity> pHIP = IndiSCADABusinessLogic.SettingLogic.GetpHInputs();
                                    int pHIndex = 0;
                                    if (pHIP != null)
                                    {
                                        foreach (var item in pHIP)
                                        {
                                            pHIntputs[pHIndex].ActualpH = pHIP[pHIndex].ActualpH;
                                            pHIntputs[pHIndex].HighSP = pHIP[pHIndex].HighSP;
                                            pHIntputs[pHIndex].LowSP = pHIP[pHIndex].LowSP;
                                            pHIndex = pHIndex + 1;
                                        }
                                    }

                                    ObservableCollection<DosingSettingsEntity> DosingIP = IndiSCADABusinessLogic.SettingLogic.GetDosingPumpInputs();
                                    int DosingIndex = 0;
                                    if (DosingIP != null)
                                    {
                                        foreach (var item in DosingIP)
                                        {
                                            DosingIntputs[DosingIndex].AutoManual = DosingIP[DosingIndex].AutoManual;
                                            DosingIntputs[DosingIndex].ManualONOFF = DosingIP[DosingIndex].ManualONOFF;
                                            DosingIntputs[DosingIndex].FlowRateTimerBased = DosingIP[DosingIndex].FlowRateTimerBased;
                                            DosingIntputs[DosingIndex].ONOFFStatus = DosingIP[DosingIndex].ONOFFStatus;

                                            DosingIntputs[DosingIndex].Quantity = DosingIP[DosingIndex].Quantity;
                                            DosingIntputs[DosingIndex].FlowRate = DosingIP[DosingIndex].FlowRate;
                                            DosingIntputs[DosingIndex].SetAmp = DosingIP[DosingIndex].SetAmp;
                                            DosingIntputs[DosingIndex].ActualAmp = DosingIP[DosingIndex].ActualAmp;

                                            DosingIntputs[DosingIndex].RemainingTime = DosingIP[DosingIndex].RemainingTime;
                                            DosingIntputs[DosingIndex].SetTime = DosingIP[DosingIndex].SetTime;
                                            DosingIntputs[DosingIndex].CumulativeAmphr = DosingIP[DosingIndex].CumulativeAmphr;

                                            DosingIntputs[DosingIndex].setPH = DosingIP[DosingIndex].setPH;
                                            DosingIntputs[DosingIndex].ActualpH = DosingIP[DosingIndex].ActualpH;
                                            
                                            DosingIndex = DosingIndex + 1;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ErrorLogger.LogError.ErrorLog("SettingsViewModel DoWork() GetTemperatureInputs(),GetpHInputs(),GetDosingPumpInputs()", DateTime.Now.ToString(), ex.Message, "No", true);
                                }
                            }));
                       


                        TankBypassInputs = IndiSCADABusinessLogic.SettingLogic.GetTankBypassInputs();
                        TrayBypassInputs = IndiSCADABusinessLogic.SettingLogic.GetTrayBypassInputs();
                        FilterPumpData = IndiSCADABusinessLogic.SettingLogic.GetFilterPumpInputs();

                        BarrelBypassInputs = IndiSCADABusinessLogic.SettingLogic.GetBarrelBypassInputs();
                        NitricStationSelection = IndiSCADABusinessLogic.SettingLogic.GetNitricSelection();

                        BaseBarrelMotorIntputs = IndiSCADABusinessLogic.SettingLogic.GetBaseBarrelMotorInputs();
                        BaseBarrelMotorIntputs2 = IndiSCADABusinessLogic.SettingLogic.GetBaseBarrelMotorInputs2();

                        BaseOilSkimmerrIntputs = IndiSCADABusinessLogic.SettingLogic.GetOilSkimmerMotorInputs();


                        WagonIntputs = IndiSCADABusinessLogic.SettingLogic.GetWagonInputs();
                        Wagon2Intputs = IndiSCADABusinessLogic.SettingLogic.GetWagon2Inputs();
                        Wagon3Intputs = IndiSCADABusinessLogic.SettingLogic.GetWagon3Inputs();
                        Wagon4Intputs = IndiSCADABusinessLogic.SettingLogic.GetWagon4Inputs();
                        Wagon5Intputs = IndiSCADABusinessLogic.SettingLogic.GetWagon5Inputs();
                        Wagon6Intputs = IndiSCADABusinessLogic.SettingLogic.GetWagon6Inputs();
                        Wagon7Intputs = IndiSCADABusinessLogic.SettingLogic.GetWagon7Inputs();
                     

                        //Oil skimmer inputs,filter pump

                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            try
                            {
                                //FilterPumpData = IndiSCADABusinessLogic.SettingLogic.GetFilterPumpInputs();

                                ObservableCollection<OilSkimmerEntity> OilSkimmerlIP = IndiSCADABusinessLogic.SettingLogic.GetOilSkimmerMotorInputs();
                                int Index = 0;
                                if (OilSkimmerlIP != null)
                                {
                                    foreach (var item in OilSkimmerlIP)
                                    {
                                        BaseOilSkimmerrIntputs[Index].BaseOilSkimmerOnOFF = OilSkimmerlIP[Index].BaseOilSkimmerOnOFF;
                                        BaseOilSkimmerrIntputs[Index].BaseOilSkimmerTrip = OilSkimmerlIP[Index].BaseOilSkimmerTrip;
                                        BaseOilSkimmerrIntputs[Index].BaseOilSkimmerStatus = OilSkimmerlIP[Index].BaseOilSkimmerStatus;

                                        Index = Index + 1;
                                    }
                                }
                            }
                            catch (Exception ex)
                            { ErrorLogger.LogError.ErrorLog("SettingsViewModel DoWork() GetBaseBarrelMotorInputs()", DateTime.Now.ToString(), ex.Message, "No", true); }
                        }));


                        //base barrel motor 1
                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            try
                            {
                                ObservableCollection<BaseBarrelMotorEntity> BaseBarrelIP = IndiSCADABusinessLogic.SettingLogic.GetBaseBarrelMotorInputs();
                                int Index = 0;
                                if (BaseBarrelIP != null)
                                {
                                    foreach (var item in BaseBarrelIP)
                                    {
                                        if (Index < 19)
                                        {
                                            BaseBarrelMotorIntputs[Index].BaseBarrelMotorOnOFF = BaseBarrelIP[Index].BaseBarrelMotorOnOFF;
                                            BaseBarrelMotorIntputs[Index].BaseBarrelMotorTrip = BaseBarrelIP[Index].BaseBarrelMotorTrip;
                                            BaseBarrelMotorIntputs[Index].BaseBarrelMotorStatus = BaseBarrelIP[Index].BaseBarrelMotorStatus;

                                        }
                                        Index = Index + 1;
                                    }
                                }
                            }
                            catch (Exception ex)
                            { ErrorLogger.LogError.ErrorLog("SettingsViewModel DoWork() GetBaseBarrelMotorInputs()", DateTime.Now.ToString(), ex.Message, "No", true); }
                        }));

                        //base barrel motor 2
                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            try
                            {
                                ObservableCollection<BaseBarrelMotorEntity> BaseBarrelIP = IndiSCADABusinessLogic.SettingLogic.GetBaseBarrelMotorInputs2();
                                int Index = 19;
                                if (BaseBarrelIP != null)
                                {
                                    foreach (var item in BaseBarrelIP)
                                    {
                                        if (Index > 18)
                                        {
                                            BaseBarrelMotorIntputs2[Index - 19].BaseBarrelMotorOnOFF = BaseBarrelIP[Index - 19].BaseBarrelMotorOnOFF;
                                            BaseBarrelMotorIntputs2[Index - 19].BaseBarrelMotorTrip = BaseBarrelIP[Index - 19].BaseBarrelMotorTrip;
                                            BaseBarrelMotorIntputs2[Index - 19].BaseBarrelMotorStatus = BaseBarrelIP[Index - 19].BaseBarrelMotorStatus;
                                        }
                                        Index = Index + 1;
                                    }
                                }
                            }
                            catch (Exception ex)
                            { ErrorLogger.LogError.ErrorLog("SettingsViewModel DoWork() GetBaseBarrelMotorInputs()", DateTime.Now.ToString(), ex.Message, "No", true); }
                        }));


                        ////wcs
                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            try
                            {
                                ObservableCollection<IOStatusEntity> Wagon7WCS = IndiSCADABusinessLogic.SettingLogic.GetWagon7Inputs();
                                int INDEX7 = 0;
                                if (Wagon7WCS != null)
                                {
                                    try
                                    {
                                        foreach (var item in Wagon7WCS)
                                        {
                                            Wagon7Intputs[INDEX7].ID = Wagon7WCS[INDEX7].ID;
                                            Wagon7Intputs[INDEX7].ParameterName = Wagon7WCS[INDEX7].ParameterName;
                                            Wagon7Intputs[INDEX7].Value = Wagon7WCS[INDEX7].Value;
                                            INDEX7 = INDEX7 + 1;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ErrorLogger.LogError.ErrorLog("SettingsViewModel DoWork() GetWagon7Inputs()", DateTime.Now.ToString(), ex.Message, "No", true);
                                    }
                                }

                                ObservableCollection<IOStatusEntity> Wagon1WCS = IndiSCADABusinessLogic.SettingLogic.GetWagonInputs();
                                int INDEX = 0;
                                if (Wagon1WCS != null)
                                {
                                    foreach (var item in Wagon1WCS)
                                    {
                                        WagonIntputs[INDEX].ID = Wagon1WCS[INDEX].ID;
                                        WagonIntputs[INDEX].ParameterName = Wagon1WCS[INDEX].ParameterName;
                                        WagonIntputs[INDEX].Value = Wagon1WCS[INDEX].Value;
                                        INDEX = INDEX + 1;
                                    }
                                }
                                ObservableCollection<IOStatusEntity> Wagon2WCS = IndiSCADABusinessLogic.SettingLogic.GetWagon2Inputs();
                                int INDEX2 = 0;
                                if (Wagon2WCS != null)
                                {
                                    foreach (var item in Wagon2WCS)
                                    {
                                        Wagon2Intputs[INDEX2].ID = Wagon2WCS[INDEX2].ID;
                                        Wagon2Intputs[INDEX2].ParameterName = Wagon2WCS[INDEX2].ParameterName;
                                        Wagon2Intputs[INDEX2].Value = Wagon2WCS[INDEX2].Value;
                                        INDEX2 = INDEX2 + 1;
                                    }
                                }
                                ObservableCollection<IOStatusEntity> Wagon3WCS = IndiSCADABusinessLogic.SettingLogic.GetWagon3Inputs();
                                int INDEX3 = 0;
                                if (Wagon3WCS != null)
                                {
                                    foreach (var item in Wagon3WCS)
                                    {
                                        Wagon3Intputs[INDEX3].ID = Wagon3WCS[INDEX3].ID;
                                        Wagon3Intputs[INDEX3].ParameterName = Wagon3WCS[INDEX3].ParameterName;
                                        Wagon3Intputs[INDEX3].Value = Wagon3WCS[INDEX3].Value;
                                        INDEX3 = INDEX3 + 1;
                                    }
                                }
                                ObservableCollection<IOStatusEntity> Wagon4WCS = IndiSCADABusinessLogic.SettingLogic.GetWagon4Inputs();
                                int INDEX4 = 0;
                                if (Wagon4WCS != null)
                                {
                                    foreach (var item in Wagon4WCS)
                                    {
                                        Wagon4Intputs[INDEX3].ID = Wagon4WCS[INDEX3].ID;
                                        Wagon4Intputs[INDEX3].ParameterName = Wagon4WCS[INDEX3].ParameterName;
                                        Wagon4Intputs[INDEX3].Value = Wagon4WCS[INDEX3].Value;
                                        INDEX4 = INDEX4 + 1;
                                    }
                                }
                                ObservableCollection<IOStatusEntity> Wagon5WCS = IndiSCADABusinessLogic.SettingLogic.GetWagon5Inputs();
                                int INDEX5 = 0;
                                if (Wagon5WCS != null)
                                {
                                    foreach (var item in Wagon5WCS)
                                    {
                                        Wagon5Intputs[INDEX5].ID = Wagon5WCS[INDEX5].ID;
                                        Wagon5Intputs[INDEX5].ParameterName = Wagon5WCS[INDEX5].ParameterName;
                                        Wagon5Intputs[INDEX5].Value = Wagon5WCS[INDEX5].Value;
                                        INDEX5 = INDEX5 + 1;
                                    }
                                }
                                ObservableCollection<IOStatusEntity> Wagon6WCS = IndiSCADABusinessLogic.SettingLogic.GetWagon6Inputs();
                                int INDEX6 = 0;
                                if (Wagon6WCS != null)
                                {
                                    foreach (var item in Wagon6WCS)
                                    {
                                        Wagon6Intputs[INDEX6].ID = Wagon6WCS[INDEX6].ID;
                                        Wagon6Intputs[INDEX6].ParameterName = Wagon6WCS[INDEX6].ParameterName;
                                        Wagon6Intputs[INDEX6].Value = Wagon6WCS[INDEX6].Value;
                                        INDEX6 = INDEX6 + 1;
                                    }
                                }


                                //App.Current.Dispatcher.BeginInvoke(new Action(() => { WagonIntputs = IndiSCADABusinessLogic.WCSLogic.GetWagonInputs(); }));
                                //App.Current.Dispatcher.BeginInvoke(new Action(() => { Wagon2Intputs = IndiSCADABusinessLogic.WCSLogic.GetWagon2Inputs(); }));
                                //App.Current.Dispatcher.BeginInvoke(new Action(() => { Wagon3Intputs = IndiSCADABusinessLogic.WCSLogic.GetWagon3Inputs(); }));
                                //App.Current.Dispatcher.BeginInvoke(new Action(() => { Wagon4Intputs = IndiSCADABusinessLogic.WCSLogic.GetWagon4Inputs(); }));
                                //App.Current.Dispatcher.BeginInvoke(new Action(() => { Wagon5Intputs = IndiSCADABusinessLogic.WCSLogic.GetWagon5Inputs(); }));
                                //App.Current.Dispatcher.BeginInvoke(new Action(() => { Wagon6Intputs = IndiSCADABusinessLogic.WCSLogic.GetWagon6Inputs(); }));
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("SettingsViewModel DoWork() GetRectifierInputs()", DateTime.Now.ToString(), ex.Message, "No", true);
                            }
                        }));



                        IndiSCADAGlobalLibrary.TagList.SettingScreenOpen = false;                   
                }

                //rectifier
                if (SelectedSettingTab == 0)
                {
                    if (isRectifierManualCurrentEdit == false)//do not refresh text box while editing value
                    {
                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            try
                            {       
                                ObservableCollection<RectifierEntity> RectifierIP = IndiSCADABusinessLogic.SettingLogic.GetRectifierInputs();
                                int RectIndex = 0;
                                if (RectifierIP != null)
                                {
                                    foreach (var item in RectifierIP)
                                    {
                                        RectifierIntputs[RectIndex].ActualCurrent = item.ActualCurrent;
                                        RectifierIntputs[RectIndex].ActualVoltage = item.ActualVoltage;
                                        RectifierIntputs[RectIndex].ManualCurrent = item.ManualCurrent;
                                        RectifierIntputs[RectIndex].AppliedCurrent = item.AppliedCurrent;
                                        RectifierIntputs[RectIndex].AmpHr = item.AmpHr;
                                        RectifierIntputs[RectIndex].LowSP = item.LowSP;
                                        RectifierIntputs[RectIndex].HighSP = item.HighSP;

                                        RectifierIntputs[RectIndex].AutoManual = item.AutoManual;
                                        RectifierIntputs[RectIndex].ManualOnOff = item.ManualOnOff;
                                        RectifierIntputs[RectIndex].AlarmStatus = item.AlarmStatus;

                                         RectifierIntputs[RectIndex].Calculated = item.Calculated;
                                        //RectifierIntputs[RectIndex].OnOffStatus = item.OnOffStatus;                                     
                                        RectifierIntputs[RectIndex].ResetAmpHr = item.ResetAmpHr;
                                        RectifierIntputs[RectIndex].CuAmpHr = item.CuAmpHr;

                                        RectIndex = RectIndex + 1;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("SettingsViewModel DoWork() GetRectifierInputs()", DateTime.Now.ToString(), ex.Message, "No", true);
                            }
                        }));
                    }
                }

                //temp ph, dosing
                else if (SelectedSettingTab == 1 || SelectedSettingTab == 2)
                {
                    if (isTemperatureDataEdit == false)//do not refresh text box while editing value
                    {
                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            try
                            {
                                if (SelectedSettingTab == 1)
                                {
                                    TemperatureIntputs = IndiSCADABusinessLogic.SettingLogic.GetTemperatureInputs();
                                    ObservableCollection<TemperatureSettingEntity> TemperatureIP = IndiSCADABusinessLogic.SettingLogic.GetTemperatureInputs();
                                    int TempIndex = 0;
                                    if (TemperatureIP != null)
                                    {
                                        foreach (var item in TemperatureIP)
                                        {
                                            TemperatureIntputs[TempIndex].ActualTemperature = TemperatureIP[TempIndex].ActualTemperature;
                                            TemperatureIntputs[TempIndex].ActualSP = TemperatureIP[TempIndex].ActualSP;
                                            TemperatureIntputs[TempIndex].HighSP = TemperatureIP[TempIndex].HighSP;
                                            TemperatureIntputs[TempIndex].LowSP = TemperatureIP[TempIndex].LowSP;
                                            TempIndex = TempIndex + 1;
                                        }
                                    }
                                    ObservableCollection<pHSettingEntity> pHIP = IndiSCADABusinessLogic.SettingLogic.GetpHInputs();
                                    int pHIndex = 0;
                                    if (pHIP != null)
                                    {
                                        foreach (var item in pHIP)
                                        {
                                            pHIntputs[pHIndex].ActualpH = pHIP[pHIndex].ActualpH;
                                            pHIntputs[pHIndex].HighSP = pHIP[pHIndex].HighSP;
                                            pHIntputs[pHIndex].LowSP = pHIP[pHIndex].LowSP;
                                            pHIndex = pHIndex + 1;
                                        }
                                    }
                                }

                                if (SelectedSettingTab == 2)
                                {
                                    ObservableCollection<DosingSettingsEntity> DosingIP = IndiSCADABusinessLogic.SettingLogic.GetDosingPumpInputs();
                                    int DosingIndex = 0;
                                    if (DosingIP != null)
                                    {
                                        foreach (var item in DosingIP)
                                        {
                                            DosingIntputs[DosingIndex].AutoManual = DosingIP[DosingIndex].AutoManual;
                                            DosingIntputs[DosingIndex].ManualONOFF = DosingIP[DosingIndex].ManualONOFF;
                                            DosingIntputs[DosingIndex].FlowRateTimerBased = DosingIP[DosingIndex].FlowRateTimerBased;
                                            DosingIntputs[DosingIndex].ONOFFStatus = DosingIP[DosingIndex].ONOFFStatus;

                                            DosingIntputs[DosingIndex].Quantity = DosingIP[DosingIndex].Quantity;
                                            DosingIntputs[DosingIndex].FlowRate = DosingIP[DosingIndex].FlowRate;
                                            DosingIntputs[DosingIndex].SetAmp = DosingIP[DosingIndex].SetAmp;
                                            DosingIntputs[DosingIndex].ActualAmp = DosingIP[DosingIndex].ActualAmp;

                                            DosingIntputs[DosingIndex].RemainingTime = DosingIP[DosingIndex].RemainingTime;
                                            DosingIntputs[DosingIndex].SetTime = DosingIP[DosingIndex].SetTime;                                            
                                            DosingIntputs[DosingIndex].CumulativeAmphr = DosingIP[DosingIndex].CumulativeAmphr;

                                            DosingIntputs[DosingIndex].setPH = DosingIP[DosingIndex].setPH;
                                            DosingIntputs[DosingIndex].ActualpH = DosingIP[DosingIndex].ActualpH;
                                             
                                            DosingIndex = DosingIndex + 1;
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("SettingsViewModel DoWork() GetTemperatureInputs(),GetpHInputs(),GetDosingPumpInputs()", DateTime.Now.ToString(), ex.Message, "No", true);
                            }
                        }));
                    }
                }

                //Oil skimmer inputs,filter pump
                else if(SelectedSettingTab == 3)
                {
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            FilterPumpData = IndiSCADABusinessLogic.SettingLogic.GetFilterPumpInputs();

                            ObservableCollection<OilSkimmerEntity> OilSkimmerlIP = IndiSCADABusinessLogic.SettingLogic.GetOilSkimmerMotorInputs();
                            int Index = 0;
                            if (OilSkimmerlIP != null)
                            {
                                foreach (var item in OilSkimmerlIP)
                                {
                                    BaseOilSkimmerrIntputs[Index].BaseOilSkimmerOnOFF = OilSkimmerlIP[Index].BaseOilSkimmerOnOFF;
                                    BaseOilSkimmerrIntputs[Index].BaseOilSkimmerTrip = OilSkimmerlIP[Index].BaseOilSkimmerTrip;
                                    BaseOilSkimmerrIntputs[Index].BaseOilSkimmerStatus = OilSkimmerlIP[Index].BaseOilSkimmerStatus;

                                    Index = Index + 1;
                                }
                            }
                        }
                        catch (Exception ex)
                        { ErrorLogger.LogError.ErrorLog("SettingsViewModel DoWork() GetBaseBarrelMotorInputs()", DateTime.Now.ToString(), ex.Message, "No", true); }
                    }));
                }

                //trary , tank bypass,station selection
                else if (SelectedSettingTab ==4)
                {
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            TankBypassInputs = IndiSCADABusinessLogic.SettingLogic.GetTankBypassInputs();
                            TrayBypassInputs = IndiSCADABusinessLogic.SettingLogic.GetTrayBypassInputs();

                            //dryer bypass
                            BarrelBypassInputs = IndiSCADABusinessLogic.SettingLogic.GetBarrelBypassInputs();
                            NitricStationSelection = IndiSCADABusinessLogic.SettingLogic.GetNitricSelection();
                        }
                        catch (Exception ex)
                        { ErrorLogger.LogError.ErrorLog("SettingsViewModel DoWork() GetBarrelMotorInputs()", DateTime.Now.ToString(), ex.Message, "No", true); }
                    }));


                    //to read/write station selection and station to be operated
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            if (isStationEdit == true)//do not refresh text box while editing value
                            {
                                StationSelection = IndiSCADABusinessLogic.SettingLogic.GetStationSelectionValue();
                                StationToBeOperated = IndiSCADABusinessLogic.SettingLogic.GetStationOperatedValue();
                                StationEnter = IndiSCADABusinessLogic.SettingLogic.GetStationEnterValue();

                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("HomeViewModel DoWork() GetSetCycleTime()", DateTime.Now.ToString(), ex.Message, "No", true);
                        }
                    }));

                }

                //base barrel motor
                else if(SelectedSettingTab == 5)
                {
                    //base barrel motor
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            ObservableCollection<BaseBarrelMotorEntity> BaseBarrelIP = IndiSCADABusinessLogic.SettingLogic.GetBaseBarrelMotorInputs();
                            int Index = 0;
                            if (BaseBarrelIP != null)
                            {
                                foreach (var item in BaseBarrelIP)
                                {
                                    if (Index < 19)
                                    {
                                        BaseBarrelMotorIntputs[Index].BaseBarrelMotorOnOFF = BaseBarrelIP[Index].BaseBarrelMotorOnOFF;
                                        BaseBarrelMotorIntputs[Index].BaseBarrelMotorTrip = BaseBarrelIP[Index].BaseBarrelMotorTrip;
                                        BaseBarrelMotorIntputs[Index].BaseBarrelMotorStatus = BaseBarrelIP[Index].BaseBarrelMotorStatus;

                                    }
                                    Index = Index + 1;
                                }
                            }
                        }
                        catch (Exception ex)
                        { ErrorLogger.LogError.ErrorLog("SettingsViewModel DoWork() GetBaseBarrelMotorInputs()", DateTime.Now.ToString(), ex.Message, "No", true); }
                    }));


                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            ObservableCollection<BaseBarrelMotorEntity> BaseBarrelIP = IndiSCADABusinessLogic.SettingLogic.GetBaseBarrelMotorInputs2();
                            int Index = 19;
                            if (BaseBarrelIP != null)
                            {
                                foreach (var item in BaseBarrelIP)
                                {
                                    if (Index > 18)
                                    {
                                        BaseBarrelMotorIntputs2[Index - 19].BaseBarrelMotorOnOFF = BaseBarrelIP[Index - 19].BaseBarrelMotorOnOFF;
                                        BaseBarrelMotorIntputs2[Index - 19].BaseBarrelMotorTrip = BaseBarrelIP[Index - 19].BaseBarrelMotorTrip;
                                        BaseBarrelMotorIntputs2[Index - 19].BaseBarrelMotorStatus = BaseBarrelIP[Index - 19].BaseBarrelMotorStatus;
                                    }
                                    Index = Index + 1;
                                }
                            }
                        }
                        catch (Exception ex)
                        { ErrorLogger.LogError.ErrorLog("SettingsViewModel DoWork() GetBaseBarrelMotorInputs()", DateTime.Now.ToString(), ex.Message, "No", true); }
                    }));
                }

                ////wcs
                else if (SelectedSettingTab == 6)
                {
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            ObservableCollection<IOStatusEntity> Wagon7WCS = IndiSCADABusinessLogic.SettingLogic.GetWagon7Inputs();
                            int INDEX7 = 0;
                            if (Wagon7WCS != null)
                            {
                                try
                                {
                                    foreach (var item in Wagon7WCS)
                                    {
                                        Wagon7Intputs[INDEX7].ID = Wagon7WCS[INDEX7].ID;
                                        Wagon7Intputs[INDEX7].ParameterName = Wagon7WCS[INDEX7].ParameterName;
                                        Wagon7Intputs[INDEX7].Value = Wagon7WCS[INDEX7].Value;
                                        INDEX7 = INDEX7 + 1;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ErrorLogger.LogError.ErrorLog("SettingsViewModel DoWork() GetWagon7Inputs()", DateTime.Now.ToString(), ex.Message, "No", true);
                                }
                            }

                            ObservableCollection<IOStatusEntity> Wagon1WCS = IndiSCADABusinessLogic.SettingLogic.GetWagonInputs();
                            int INDEX = 0;
                            if (Wagon1WCS != null)
                            {
                                foreach (var item in Wagon1WCS)
                                {
                                    WagonIntputs[INDEX].ID = Wagon1WCS[INDEX].ID;
                                    WagonIntputs[INDEX].ParameterName = Wagon1WCS[INDEX].ParameterName;
                                    WagonIntputs[INDEX].Value = Wagon1WCS[INDEX].Value;
                                    INDEX = INDEX + 1;
                                }
                            }
                            ObservableCollection<IOStatusEntity> Wagon2WCS = IndiSCADABusinessLogic.SettingLogic.GetWagon2Inputs();
                            int INDEX2 = 0;
                            if (Wagon2WCS != null)
                            {
                                foreach (var item in Wagon2WCS)
                                {
                                    Wagon2Intputs[INDEX2].ID = Wagon2WCS[INDEX2].ID;
                                    Wagon2Intputs[INDEX2].ParameterName = Wagon2WCS[INDEX2].ParameterName;
                                    Wagon2Intputs[INDEX2].Value = Wagon2WCS[INDEX2].Value;
                                    INDEX2 = INDEX2 + 1;
                                }
                            }
                            ObservableCollection<IOStatusEntity> Wagon3WCS = IndiSCADABusinessLogic.SettingLogic.GetWagon3Inputs();
                            int INDEX3 = 0;
                            if (Wagon3WCS != null)
                            {
                                foreach (var item in Wagon3WCS)
                                {
                                    Wagon3Intputs[INDEX3].ID = Wagon3WCS[INDEX3].ID;
                                    Wagon3Intputs[INDEX3].ParameterName = Wagon3WCS[INDEX3].ParameterName;
                                    Wagon3Intputs[INDEX3].Value = Wagon3WCS[INDEX3].Value;
                                    INDEX3 = INDEX3 + 1;
                                }
                            }
                            ObservableCollection<IOStatusEntity> Wagon4WCS = IndiSCADABusinessLogic.SettingLogic.GetWagon4Inputs();
                            int INDEX4 = 0;
                            if (Wagon4WCS != null)
                            {
                                foreach (var item in Wagon4WCS)
                                {
                                    Wagon4Intputs[INDEX3].ID = Wagon4WCS[INDEX3].ID;
                                    Wagon4Intputs[INDEX3].ParameterName = Wagon4WCS[INDEX3].ParameterName;
                                    Wagon4Intputs[INDEX3].Value = Wagon4WCS[INDEX3].Value;
                                    INDEX4 = INDEX4 + 1;
                                }
                            }
                            ObservableCollection<IOStatusEntity> Wagon5WCS = IndiSCADABusinessLogic.SettingLogic.GetWagon5Inputs();
                            int INDEX5 = 0;
                            if (Wagon5WCS != null)
                            {
                                foreach (var item in Wagon5WCS)
                                {
                                    Wagon5Intputs[INDEX5].ID = Wagon5WCS[INDEX5].ID;
                                    Wagon5Intputs[INDEX5].ParameterName = Wagon5WCS[INDEX5].ParameterName;
                                    Wagon5Intputs[INDEX5].Value = Wagon5WCS[INDEX5].Value;
                                    INDEX5 = INDEX5 + 1;
                                }
                            }
                            ObservableCollection<IOStatusEntity> Wagon6WCS = IndiSCADABusinessLogic.SettingLogic.GetWagon6Inputs();
                            int INDEX6 = 0;
                            if (Wagon6WCS != null)
                            {
                                foreach (var item in Wagon6WCS)
                                {
                                    Wagon6Intputs[INDEX6].ID = Wagon6WCS[INDEX6].ID;
                                    Wagon6Intputs[INDEX6].ParameterName = Wagon6WCS[INDEX6].ParameterName;
                                    Wagon6Intputs[INDEX6].Value = Wagon6WCS[INDEX6].Value;
                                    INDEX6 = INDEX6 + 1;
                                }
                            }


                            //App.Current.Dispatcher.BeginInvoke(new Action(() => { WagonIntputs = IndiSCADABusinessLogic.WCSLogic.GetWagonInputs(); }));
                            //App.Current.Dispatcher.BeginInvoke(new Action(() => { Wagon2Intputs = IndiSCADABusinessLogic.WCSLogic.GetWagon2Inputs(); }));
                            //App.Current.Dispatcher.BeginInvoke(new Action(() => { Wagon3Intputs = IndiSCADABusinessLogic.WCSLogic.GetWagon3Inputs(); }));
                            //App.Current.Dispatcher.BeginInvoke(new Action(() => { Wagon4Intputs = IndiSCADABusinessLogic.WCSLogic.GetWagon4Inputs(); }));
                            //App.Current.Dispatcher.BeginInvoke(new Action(() => { Wagon5Intputs = IndiSCADABusinessLogic.WCSLogic.GetWagon5Inputs(); }));
                            //App.Current.Dispatcher.BeginInvoke(new Action(() => { Wagon6Intputs = IndiSCADABusinessLogic.WCSLogic.GetWagon6Inputs(); }));
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("SettingsViewModel DoWork() GetRectifierInputs()", DateTime.Now.ToString(), ex.Message, "No", true);
                        }
                    }));
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel DoWork()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                IndiSCADAGlobalLibrary.TagList.SettingScreenOpen = false;
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
                ErrorLogger.LogError.ErrorLog("SettingsViewModel DispatcherTickEvent()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
      
        #endregion

        #region Public/Private Property
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
        
        private string _NitricStationSelection;
        public string NitricStationSelection
        {
            get
            {
                return _NitricStationSelection;
            }
            set
            {
                _NitricStationSelection = value;
                OnPropertyChanged("NitricStationSelection");
            }
        }

        #region station selection properties
        private string _StationEnter;
        public string StationEnter
        {
            get
            {
                return _StationEnter;
            }
            set
            {
                _StationEnter = value;
                OnPropertyChanged("StationEnter");
            }
        }

        private bool _isStationEdit = true;
        public bool isStationEdit
        { 
            get
            {
                return _isStationEdit;
            }
            set
            {
                _isStationEdit = value;
                OnPropertyChanged("isStationEdit"); 
            }
        }
        private string _StationSelection;
        public string StationSelection
        {
            get
            {
                return _StationSelection;
            }
            set
            {
                _StationSelection = value;
                OnPropertyChanged("StationSelection");
            }
        }
        private string _StationToBeOperated;
        public string StationToBeOperated
        {
            get
            {
                return _StationToBeOperated;
            }
            set
            {
                _StationToBeOperated = value;
                OnPropertyChanged("StationToBeOperated");
            }
        }
        #endregion

        #region WCS Property
        private ObservableCollection<IOStatusEntity> _WagonIntputs = new ObservableCollection<IOStatusEntity>();
        public ObservableCollection<IOStatusEntity> WagonIntputs
        {
            get { return _WagonIntputs; }
            set { _WagonIntputs = value; OnPropertyChanged("WagonIntputs"); }
        }
        private ObservableCollection<IOStatusEntity> _Wagon2Intputs = new ObservableCollection<IOStatusEntity>();
        public ObservableCollection<IOStatusEntity> Wagon2Intputs
        {
            get { return _Wagon2Intputs; }
            set { _Wagon2Intputs = value; OnPropertyChanged("Wagon2Intputs"); }
        }
        private ObservableCollection<IOStatusEntity> _Wagon3Intputs = new ObservableCollection<IOStatusEntity>();
        public ObservableCollection<IOStatusEntity> Wagon3Intputs
        {
            get { return _Wagon3Intputs; }
            set { _Wagon3Intputs = value; OnPropertyChanged("Wagon3Intputs"); }
        }
        private ObservableCollection<IOStatusEntity> _Wagon4Intputs = new ObservableCollection<IOStatusEntity>();
        public ObservableCollection<IOStatusEntity> Wagon4Intputs
        {
            get { return _Wagon4Intputs; }
            set { _Wagon4Intputs = value; OnPropertyChanged("Wagon4Intputs"); }
        }
        private ObservableCollection<IOStatusEntity> _Wagon5Intputs = new ObservableCollection<IOStatusEntity>();
        public ObservableCollection<IOStatusEntity> Wagon5Intputs
        {
            get { return _Wagon5Intputs; }
            set { _Wagon5Intputs = value; OnPropertyChanged("Wagon5Intputs"); }
        }
        private ObservableCollection<IOStatusEntity> _Wagon6Intputs = new ObservableCollection<IOStatusEntity>();
        public ObservableCollection<IOStatusEntity> Wagon6Intputs
        {
            get { return _Wagon6Intputs; }
            set { _Wagon6Intputs = value; OnPropertyChanged("Wagon6Intputs"); }
        }
        private ObservableCollection<IOStatusEntity> _Wagon7Intputs = new ObservableCollection<IOStatusEntity>();
        public ObservableCollection<IOStatusEntity> Wagon7Intputs
        {
            get { return _Wagon7Intputs; }
            set { _Wagon7Intputs = value; OnPropertyChanged("Wagon7Intputs"); }
        }
        
        #endregion
        private bool _isRectifierManualCurrentEdit = new bool();
        public bool isRectifierManualCurrentEdit
        {
            get { return _isRectifierManualCurrentEdit; }
            set { _isRectifierManualCurrentEdit = value; OnPropertyChanged("isRectifierManualCurrentEdit"); }
        }
        private bool _isTemperatureDataEdit = new bool();
        public bool isTemperatureDataEdit
        {
            get { return _isTemperatureDataEdit; }
            set { _isTemperatureDataEdit = value; OnPropertyChanged("isTemperatureDataEdit"); }
        }

        private ObservableCollection<TankBypassEntity> _TankBypassInputs = new ObservableCollection<TankBypassEntity>();
        public ObservableCollection<TankBypassEntity> TankBypassInputs
        {
            get { return _TankBypassInputs; }
            set { _TankBypassInputs = value; OnPropertyChanged("TankBypassInputs"); }
        }
        private ObservableCollection<TankBypassEntity> _TrayBypassInputs = new ObservableCollection<TankBypassEntity>();
        public ObservableCollection<TankBypassEntity> TrayBypassInputs
        {
            get { return _TrayBypassInputs; }
            set { _TrayBypassInputs = value; OnPropertyChanged("TrayBypassInputs"); }
        }

        private ObservableCollection<TankBypassEntity> _BarrelBypassInputs = new ObservableCollection<TankBypassEntity>();
        public ObservableCollection<TankBypassEntity> BarrelBypassInputs
        {
            get { return _BarrelBypassInputs; }
            set { _BarrelBypassInputs = value; OnPropertyChanged("BarrelBypassInputs"); }
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
        private ObservableCollection<BaseBarrelMotorEntity> _BaseBarrelMotorIntputs = new ObservableCollection<BaseBarrelMotorEntity>();
        public ObservableCollection<BaseBarrelMotorEntity> BaseBarrelMotorIntputs
        {
            get { return _BaseBarrelMotorIntputs; }
            set { _BaseBarrelMotorIntputs = value; OnPropertyChanged("BaseBarrelMotorIntputs"); }
        }

        private ObservableCollection<OilSkimmerEntity> _BaseOilSkimmerrIntputs = new ObservableCollection<OilSkimmerEntity>();
        public ObservableCollection<OilSkimmerEntity> BaseOilSkimmerrIntputs
        {
            get { return _BaseOilSkimmerrIntputs; }
            set { _BaseOilSkimmerrIntputs = value; OnPropertyChanged("BaseOilSkimmerrIntputs"); }
        }

        private ObservableCollection<BaseBarrelMotorEntity> _BaseBarrelMotorIntputs2 = new ObservableCollection<BaseBarrelMotorEntity>();
        public ObservableCollection<BaseBarrelMotorEntity> BaseBarrelMotorIntputs2
        {
            get { return _BaseBarrelMotorIntputs2; }
            set { _BaseBarrelMotorIntputs2 = value; OnPropertyChanged("BaseBarrelMotorIntputs2"); }
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
        private ObservableCollection<FilterPumpSettingsEntity> _FilterPumpSettingsEntity = new ObservableCollection<FilterPumpSettingsEntity>();
        public ObservableCollection<FilterPumpSettingsEntity> FilterPumpData
        {
            get { return _FilterPumpSettingsEntity; }
            set { _FilterPumpSettingsEntity = value; OnPropertyChanged("FilterPumpData"); }
        }
        private ObservableCollection<TopSpraySettingsEntity> _TopSprayData = new ObservableCollection<TopSpraySettingsEntity>();
        public ObservableCollection<TopSpraySettingsEntity> TopSprayData
        {
            get { return _TopSprayData; }
            set { _TopSprayData = value; OnPropertyChanged("TopSprayData"); }
        }

        private ObservableCollection<UtilitySettingEntity> _UtilityData = new ObservableCollection<UtilitySettingEntity>();
        public ObservableCollection<UtilitySettingEntity> UtilityData
        {
            get { return _UtilityData; }
            set { _UtilityData = value; OnPropertyChanged("UtilityData"); }
        }
        private ObservableCollection<UtilitySettingEntity> _BarrelMotorDATA = new ObservableCollection<UtilitySettingEntity>();
        public ObservableCollection<UtilitySettingEntity> BarrelMotorDATA
        {
            get { return _BarrelMotorDATA; }
            set { _BarrelMotorDATA = value; OnPropertyChanged("BarrelMotorDATA"); }
        }
        private ObservableCollection<MechanicalAgitationSettingsEntity> _MechanicalAgitation = new ObservableCollection<MechanicalAgitationSettingsEntity>();
        public ObservableCollection<MechanicalAgitationSettingsEntity> AgitationData
        {
            get { return _MechanicalAgitation; }
            set { _MechanicalAgitation = value; OnPropertyChanged("AgitationData"); }
        }

        private ObservableCollection<OutOfRangesSettingsEntitypH> _pHCollection = new ObservableCollection<OutOfRangesSettingsEntitypH>();
        public ObservableCollection<OutOfRangesSettingsEntitypH> pHCollection
        {
            get { return _pHCollection; }
            set { _pHCollection = value; OnPropertyChanged("pHCollection"); }
        }
        private string _selectedprogramNo;
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
        private string _ProgramNameText;
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
