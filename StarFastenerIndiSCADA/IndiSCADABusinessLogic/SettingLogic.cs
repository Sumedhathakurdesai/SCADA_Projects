using IndiSCADAEntity.Entity;
using IndiSCADAGlobalLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADABusinessLogic
{
    public static class SettingLogic
    {
        #region"Declaration"

        static ServiceResponse<IList> _WagonInputParameterLists = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Wagon1 WCS Input");
        static ServiceResponse<IList> _Wagon2InputParameterLists = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Wagon2 WCS Input");
        static ServiceResponse<IList> _Wagon3InputParameterLists = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Wagon3 WCS Input");
        static ServiceResponse<IList> _Wagon4InputParameterLists = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Wagon4 WCS Input");
        static ServiceResponse<IList> _Wagon5InputParameterLists = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Wagon5 WCS Input");
        static ServiceResponse<IList> _Wagon6InputParameterLists = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Wagon6 WCS Input");
        static ServiceResponse<IList> _Wagon7InputParameterLists = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Wagon7 WCS Input");

        //Read only once Rectifier Station Names
        static ServiceResponse<IList> _RectifierStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Rectifier");
        //Read only once Temperature Station Names
        static ServiceResponse<IList> _TemperatureStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("TemperatureSetting");
        //Read only once pH Station Names
        static ServiceResponse<IList> _pHStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("pH");
        //Read only once Barrel Motor StationName 
        static ServiceResponse<IList> _BarrelMotorStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Barrel Motor");
        //Read only once Dosing pump
        static ServiceResponse<IList> _DosingPumpStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("DosingSetting");
        //Read only once Base Barrel Motor
        static ServiceResponse<IList> _BaseBarrelMotorStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("BaseBarrelSetting");
        static ServiceResponse<IList> _OilSkimmerStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("OilSkimmerStationName");

        //Read only once Top Spray Station Names
        static ServiceResponse<IList> _TopSprayStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("TopSpray");
        //Read only once Top Spray Station Names
        static ServiceResponse<IList> _UtilityStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Utility");
        //Read only once Mechanical Agitation StationName 
        static ServiceResponse<IList> _MechanicalAgitationStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("MechanicalAgitationStationName");
        //Read only once Filter Pump StationName 
        static ServiceResponse<IList> _FilterPumpStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("FilterPumpStationName");

        static ServiceResponse<IList> _TankBypassStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("TankBypass");
        static ServiceResponse<IList> _TrayBypassStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("TrayBypass");
        static ServiceResponse<IList> _BarrelBypassStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("DryerBypass");
        static ServiceResponse<IList> _RotationBypassStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("RotationBypass");


        //added by sbs to read chemical name from database
        static DataTable chemicalNm = IndiSCADATranslation.IOStatusTranslation.getchemicalList();
        //fetch chemicals percentage from chemical name master
        static DataTable ChemicalNamesWithPercentageDT = IndiSCADADataAccess.DataAccessSelect.getAllChemicalNamePercentageData();
 

    //Station Number details
    static string[] StationName = new string[]{
            "LOAD/ UNLOAD STATION",
            "SOAK CLEANING 1",
            "SOAK CLEANING 2",
            "ULTRASONIC CLEANING",
            "HOT WATER RINSE",
            "ANODIC CLEANING 1",
            "WATER RINSE 1",
            "WATER RINSE 2",
            "ACID PICKLING",
            "WATER RINSE 3",
            "WATER RINSE 4",
            "ACID ACTIVATION",
            "CROSS TANK",
            "WARM WATER RINSE 8",
            "ELECTROLESS NI 1",
            "ELECTROLESS NI 2",
            "ELECTROLESS NI 3",
            "ELECTROLESS NI 4",
            "NI DRAG OUT",
            "WATER RINSE 9",
            "WATER RINSE10",
            "HOT WATER RINSE",
            "HOT AIR DRYING-CUSTOMER SCOPE",
            "UNLOADING"};
        #endregion
        #region Public/private methods
        public static string ValueFromArray(string[] InputArray, int index)
        {
            try
            {
                return InputArray[index];
            }
            catch
            {
                return "0";
            }
        }
        public static void ONOFFMomentary(string TaskName, int index)
        {
            ErrorLogger.LogError.ErrorLog("SettingLogic ONOFFMomentary() Write Momentary writting for TaskName=" + TaskName + " postion =" + index, DateTime.Now.ToString(), "", null, true);
            string TagName = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName(TaskName, index);
            try
            {
                {
                    int LT1 = 1;
                    DeviceCommunication.CommunicationWithPLC.WriteValues(TagName, LT1.ToString());
                }
                System.Threading.Thread.Sleep(500);
                {
                    int LT1 = 0;
                    DeviceCommunication.CommunicationWithPLC.WriteValues(TagName, LT1.ToString());
                }
            }

            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingLogic ONOFFMomentary() Write Momentary writting for TagName=" + TagName + " postion =" + index, DateTime.Now.ToString(), "", null, true);
            }


        }

        public static void ONOFFButton(string TaskName,int index)
        {
            try
            {
                //Get AutoManual from PLC
                string[] ValueToRead = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue(TaskName);
                string TagName = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName(TaskName,index);
                ErrorLogger.LogError.ErrorLog("SettingLogic ONOFFButton(TagName)", DateTime.Now.ToString(), TagName, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                ErrorLogger.LogError.ErrorLog("SettingLogic ONOFFButton(Value)", DateTime.Now.ToString(), ValueToRead[index].ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                if (TagName != null)
                {
                    if (TagName.Length > 0)
                    {
                        if (ValueToRead[index] == "1")
                        {
                            int LT1 = 0;
                            //DeviceCommunication.CommunicationWithPLC.WriteValues(TagName, LT1.ToString());

                            string TagName1 = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName(TaskName, index);
                            DeviceCommunication.CommunicationWithPLC.WriteBoolValues(TagName1, LT1.ToString());
                        }
                    }
                    if (TagName.Length > 0)
                    {
                        if (ValueToRead[index] == "0")
                        {
                            int LT1 = 1;
                            //DeviceCommunication.CommunicationWithPLC.WriteValues(TagName, LT1.ToString());

                            string TagName1 = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName(TaskName, index);
                            DeviceCommunication.CommunicationWithPLC.WriteBoolValues(TagName1, LT1.ToString());
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingLogic ONOFFButton()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        
        }
        public static void WriteValueToPLC(string TaskName, int index,string value)
        {
            try
            {
                if (IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected == true) ////added by SBS to avoid hanging when there is no PLC communication 
                {
                    try
                    {
                        string TagName = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName(TaskName, index);
                        DeviceCommunication.CommunicationWithPLC.WriteValues(TagName, value);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("SettingLogic WriteValueToPLC() ReadTagName", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("SettingLogic WriteValueToPLC()" + TaskName, DateTime.Now.ToString(), "IsPLCConnected is false", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingLogic WriteValueToPLC() " + TaskName, DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        public static void WriteValueToPLCReal(string TaskName, int index, string value)
        {
            try
            {
                if (IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected == true) ////added by SBS to avoid hanging when there is no PLC communication 
                {

                    try
                    {
                        string TagName = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName(TaskName, index);
                        ErrorLogger.LogError.ErrorLog("NextLoadView set TagName=" + TagName, DateTime.Now.ToString(), "", "No", true);
                        DeviceCommunication.CommunicationWithPLC.WriteValues_Real(TagName, value);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("SettingLogic WriteValueToPLCReal() WriteValues_Real", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("SettingLogic WriteValueToPLCReal()" + TaskName, DateTime.Now.ToString(), "IsPLCConnected is false", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingLogic WriteValueToPLCReal() " + TaskName, DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        public static void WriteBoolValueToPLC(string TaskName, int index, string value)
        {
            try
            {
                if (IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected == true) ////added by SBS to avoid hanging when there is no PLC communication 
                {
                    try
                    {
                        string TagName = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName(TaskName, index);
                        DeviceCommunication.CommunicationWithPLC.WriteValues(TagName, value);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("SettingLogic WriteBoolValueToPLC() ReadTagName", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("SettingLogic WriteBoolValueToPLC()" + TaskName, DateTime.Now.ToString(), "IsPLCConnected is false", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingLogic WriteBoolValueToPLC() " + TaskName, DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        public static void WriteValueToPLCWord(string TaskName, int index, string value)
        {
            ErrorLogger.LogError.ErrorLog("SettingLogic WriteValueToPLCWord() TaskName"+ TaskName+ " index" + index+ " value"+ value, DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
        
            try
            {
                if (IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected == true) ////added by SBS to avoid hanging when there is no PLC communication 
                {

                    try
                    {
                        string TagName = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName(TaskName, index);
                        DeviceCommunication.CommunicationWithPLC.WriteValues_Word(TagName, value);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("SettingLogic WriteValueToPLCWord() WriteValues_Real", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("SettingLogic WriteValueToPLCWord()" + TaskName, DateTime.Now.ToString(), "IsPLCConnected is false", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingLogic WriteValueToPLCWord() " + TaskName, DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }


        //ok
        public static ObservableCollection<RectifierEntity> GetRectifierInputs()
        {
            ObservableCollection<RectifierEntity> __RectifierData = new ObservableCollection<RectifierEntity>();
            try
            {
                //string[] ActualCurrent = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21" };
                //string[] ActualVoltage = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21" };
                //string[] ManualCurrent = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21" };
                //string[] AppliedCurrent = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21" };
                //string[] RectifierAmpHr = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21" };
                //string[] RectifierHighSP = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21" };
                //string[] RectifierLowSP = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21" };
                //string[] AutoManual = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21" };
                //string[] ManulaONOFF = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21" };
                //string[] RectifierAlarmStatus = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21" };
                //string[] ResetAmpHr = { "1", "1", "1", "1", "1", "1", "0", "0", "0", "0", "11", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };
                //string[] RectifierCumulativeAmpPerHr = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                //string[] Calculatedcurrent = { "1", "1", "1", "1", "1", "1", "0", "0", "0", "0", "11", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };


                string[] ActualCurrent; string[] ActualVoltage; string[] ManualCurrent; string[] AppliedCurrent;
                string[] RectifierHighSP; string[] RectifierLowSP; string[] AutoManual; string[] ManulaONOFF;
                string[] RectifierAlarmStatus; string[] ResetAmpHr; string[] RectifierCumulativeAmpPerHr; string[] RectifierAmpHr; string[] Calculatedcurrent;
                if (IndiSCADAGlobalLibrary.TagList.SettingScreenOpen == true)
                {
                    ActualCurrent = IndiSCADAGlobalLibrary.TagList.ActualCurrent;
                    ActualVoltage = IndiSCADAGlobalLibrary.TagList.ActualVoltage;
                    ManualCurrent = IndiSCADAGlobalLibrary.TagList.ManualCurrent;
                    AppliedCurrent = IndiSCADAGlobalLibrary.TagList.AppliedCurrentValues;
                    RectifierHighSP = IndiSCADAGlobalLibrary.TagList.RectifierHighSP;
                    RectifierLowSP = IndiSCADAGlobalLibrary.TagList.RectifierLowSP;
                    AutoManual = IndiSCADAGlobalLibrary.TagList.AutoManual;
                    ManulaONOFF = IndiSCADAGlobalLibrary.TagList.ManulaONOFF;
                    RectifierAlarmStatus = IndiSCADAGlobalLibrary.TagList.RectifierAlarmStatus;
                    ResetAmpHr = IndiSCADAGlobalLibrary.TagList.ResetAmpHr;
                    RectifierCumulativeAmpPerHr = IndiSCADAGlobalLibrary.TagList.RectifierCumulativeAmpPerHr;
                    RectifierAmpHr = IndiSCADAGlobalLibrary.TagList.RectifierAmpHr;
                    Calculatedcurrent = IndiSCADAGlobalLibrary.TagList.Calculatedcurrent;
                }
                else
                {
                    //Actual Current
                    ActualCurrent = IndiSCADAGlobalLibrary.TagList.ActualCurrent;
                    //Actual Voltage
                    ActualVoltage = IndiSCADAGlobalLibrary.TagList.ActualVoltage;
                    //Get ManualCurrent from PLC
                    ManualCurrent = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("RectifierManualCurrent");
                    //Get AppliedCurrent from PLC
                    AppliedCurrent = IndiSCADAGlobalLibrary.TagList.AppliedCurrentValues;//DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("RectifierAppliedCurrent"); 
                                                                                         //Get RectifierAmpHr from PLC
                    RectifierHighSP = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("RectifierHighSP");
                    //Get RectifierAmpHr from PLC
                    RectifierLowSP = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("RectifierLowSP");
                    AutoManual = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("RECTIFIERAutoOrManual");
                    //Get ManulaONOFF from PLC
                    ManulaONOFF = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("RECTIFIERManualONOrOFF");
                    //Get RectifierAlarmStatus from PLC
                    RectifierAlarmStatus = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("RECTIFIERAlarmStatus");
                    //Get Reset amphr Status from PLC
                    ResetAmpHr = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("RECTIFIERResetCumulativeAmpHr");
                    //Get RectifierCumulativeAmpPerHr from PLC
                    RectifierCumulativeAmpPerHr = DeviceCommunication.CommunicationWithPLC.Real_ReadPLCTagValue("RectifierCumulativeAmpHr");
                    //Get RectifierAmpHr from PLC
                    RectifierAmpHr = DeviceCommunication.CommunicationWithPLC.Real_ReadPLCTagValue("RectifierAmpHr");


                    //Get Calculatedcurrent from PLC
                    Calculatedcurrent = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("RectifierCalculatedCurrentOrAutoCurrent");

                    //Get ONOFF Status from PLC
                    //string[] ONOffStatus = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("RECTIFIERONOrOFFStatus");
                }

                int index = 0;
                if (_RectifierStationName.Response != null)
                {
                    IList<IOStatusEntity> lstRectifier = (IList<IOStatusEntity>)(_RectifierStationName.Response);

                    ErrorLogger.LogError.ErrorLog("settingLogic() GetRectifierInputs() lstRectifier.Count=" + lstRectifier.Count + " ManulaONOFF" + ManulaONOFF.Length + " ManualCurrent" + ManualCurrent.Length + " ActualCurrent" + ActualCurrent.Length, DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                     

                    foreach (var item in lstRectifier)
                    {
                        RectifierEntity _RectifierEntity = new RectifierEntity();
                        _RectifierEntity.RectifierName = item.ParameterName;
                        try
                        {
                            if (lstRectifier.Count == AutoManual.Length && lstRectifier.Count == ManulaONOFF.Length)
                            {
                                _RectifierEntity.RectifierNo = index.ToString();

                                _RectifierEntity.ActualCurrent = ValueFromArray(ActualCurrent, index);
                                _RectifierEntity.ActualVoltage = ValueFromArray(ActualVoltage, index);
                                _RectifierEntity.ManualCurrent = ValueFromArray(ManualCurrent, index);
                                _RectifierEntity.AppliedCurrent = ValueFromArray(AppliedCurrent, index);

                                if (index > 2)
                                {
                                    _RectifierEntity.AmpHr = ValueFromArray(RectifierAmpHr, index);
                                    _RectifierEntity.CuAmpHr = ValueFromArray(RectifierCumulativeAmpPerHr, index);
                                }
                                else
                                {
                                    _RectifierEntity.AmpHr = "";
                                }


                                _RectifierEntity.LowSP = ValueFromArray(RectifierLowSP, index);
                                _RectifierEntity.HighSP = ValueFromArray(RectifierHighSP, index);

                                   
                                _RectifierEntity.AutoManual = ValueFromArray(AutoManual, index);
                                _RectifierEntity.ManualOnOff = ValueFromArray(ManulaONOFF, index);
                                _RectifierEntity.Calculated = ValueFromArray(Calculatedcurrent, index);
                                //_RectifierEntity.OnOffStatus = ValueFromArray(ONOffStatus, index);

                                _RectifierEntity.AlarmStatus = ValueFromArray(RectifierAlarmStatus, index);
                                _RectifierEntity.ResetAmpHr = ValueFromArray(ResetAmpHr, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("SettingLogic GetRectifierInputs() RectifierName", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        __RectifierData.Add(_RectifierEntity);
                        index = index + 1;
                    }
                }
                return __RectifierData;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingLogic GetRectifierInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return __RectifierData;
            }
        }
        //ok
        public static ObservableCollection<TemperatureSettingEntity> GetTemperatureInputs()
        {
            ObservableCollection<TemperatureSettingEntity> __TemperatureData = new ObservableCollection<TemperatureSettingEntity>();
            try
            {
                //string[] TempHighSP = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24","25" };
                //string[] TempLowSP = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25" };
                //string[] TempSP = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25" };
                //string[] TemperatureActual = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25" };

                string[] TempHighSP; string[] TempLowSP;  string[] TempSP;  string[] TemperatureActual;
                if (IndiSCADAGlobalLibrary.TagList.SettingScreenOpen == true)
                {
                    //Get TempHighSP from PLC
                    TempHighSP = IndiSCADAGlobalLibrary.TagList.TempHigh;
                    //Get TempLowSP from PLC
                    TempLowSP = IndiSCADAGlobalLibrary.TagList.TempLow;
                    //Get TempSP from PLC
                    TempSP = IndiSCADAGlobalLibrary.TagList.TempSetpt;
                    //Get Actual Temp from PLC
                    TemperatureActual = IndiSCADAGlobalLibrary.TagList.TemperatureActual;
                }
                else
                {
                    //Get TempHighSP from PLC
                    TempHighSP = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("TempControllerTemperatureHighSetPoint");
                    //Get TempLowSP from PLC
                    TempLowSP = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("TempControllerTemperatureLowSetPoint");
                    //Get TempSP from PLC
                    TempSP = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("TempControllerTemperatureControllerSetPoint");
                    //Get Actual Temp from PLC
                    TemperatureActual = IndiSCADAGlobalLibrary.TagList.TemperatureActual;
                }

                int index = 0;
                if (_TemperatureStationName.Response != null)
                {
                    IList<IOStatusEntity> lstTemperature = (IList<IOStatusEntity>)(_TemperatureStationName.Response);
                    foreach (var item in lstTemperature)
                    {
                        TemperatureSettingEntity _TemperatureStationName = new TemperatureSettingEntity();
                        _TemperatureStationName.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstTemperature.Count == TempHighSP.Length && lstTemperature.Count == TempLowSP.Length)
                            {
                                _TemperatureStationName.TemperatureID = index.ToString();
                                _TemperatureStationName.ActualTemperature = ValueFromArray(TemperatureActual, index);
                                _TemperatureStationName.ActualSP = ValueFromArray(TempSP, index);
                                _TemperatureStationName.HighSP = ValueFromArray(TempHighSP, index);
                                _TemperatureStationName.LowSP = ValueFromArray(TempLowSP, index);
                            }
                        }
                        catch { }
                        __TemperatureData.Add(_TemperatureStationName);
                        index = index + 1;
                    }
                }
                return __TemperatureData;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingLogic GetTemperatureInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return __TemperatureData;
            }
        }
        //ok
        public static ObservableCollection<pHSettingEntity> GetpHInputs()
        {
            ObservableCollection<pHSettingEntity> _pHData = new ObservableCollection<pHSettingEntity>();
            try
            {
                //string[] pHHighSP = new string[] { "1", "2", "3", "4" };
                //string[] pHLowSP = new string[] { "11", "12", "13", "14" };
                //string[] pHActual = new string[] { "31", "32", "33", "34" };
                string[] pHHighSP; string[] pHLowSP; string[] pHActual;
                if (IndiSCADAGlobalLibrary.TagList.SettingScreenOpen == true)
                {
                    //Get pHHighSP from PLC
                     pHHighSP = IndiSCADAGlobalLibrary.TagList.pHHigh; 
                    //Get pHLowSP from PLC
                     pHLowSP = IndiSCADAGlobalLibrary.TagList.pHLow; 
                    //Get Actual pH from PLC
                    pHActual = IndiSCADAGlobalLibrary.TagList.pHActual;
                }
                else
                {
                    //Get pHHighSP from PLC
                     pHHighSP =  DivideBy(DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("pHMeterpHHighsetPoint"), 100, 5);
                    //Get pHLowSP from PLC
                    pHLowSP =   DivideBy(DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("pHMeterpHLowsetPoint"), 100, 5);
                    //Get Actual pH from PLC
                    pHActual = IndiSCADAGlobalLibrary.TagList.pHActual;
                }

                int index = 0;
                if (_pHStationName.Response != null)
                {
                    IList<IOStatusEntity> lstpH = (IList<IOStatusEntity>)(_pHStationName.Response);
                    foreach (var item in lstpH)
                    {
                        pHSettingEntity _pHStationName = new pHSettingEntity();
                        _pHStationName.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstpH.Count == pHHighSP.Length && lstpH.Count == pHLowSP.Length)
                            {
                                _pHStationName.pHID = index.ToString();
                                _pHStationName.ActualpH = ValueFromArray(pHActual, index);
                                _pHStationName.HighSP = ValueFromArray(pHHighSP, index);
                                _pHStationName.LowSP = ValueFromArray(pHLowSP, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("SettingLogic GetpHInputs() lstpH", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _pHData.Add(_pHStationName);
                        index = index + 1;
                    }
                }
                return _pHData;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingLogic GetpHInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _pHData;
            }
        }
        //ok
        public static ObservableCollection<UtilitySettingEntity> GetBarrelMotorInputs()
        {
            ObservableCollection<UtilitySettingEntity> __FilterPumpSettingsData = new ObservableCollection<UtilitySettingEntity>();
            try
            {

                //Get FilterPumpOnOFF from PLC
                string[] FilterPumpOnOFF = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("BarrelMotorOnOff");
                //Get FilterPumpTripOP from PLC
                string[] FilterPumpStatusOP = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("BarrelMotorStatus");
                //Get FilterPumpStatusOP from PLC
                //string[] FilterPumpStatusOP = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("FilterPumpOutput");
                int index = 0;
                if (_BarrelMotorStationName.Response != null)
                {
                    IList<IOStatusEntity> lstFilterPump = (IList<IOStatusEntity>)(_BarrelMotorStationName.Response);
                    foreach (var item in lstFilterPump)
                    {
                        UtilitySettingEntity _FilterPumpStationName = new UtilitySettingEntity();
                        _FilterPumpStationName.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstFilterPump.Count == FilterPumpOnOFF.Length && lstFilterPump.Count == FilterPumpStatusOP.Length)
                            {
                                _FilterPumpStationName.UtilityID = index.ToString();
                                _FilterPumpStationName.ManualOnOff = ValueFromArray(FilterPumpOnOFF, index);
                                _FilterPumpStationName.Status = ValueFromArray(FilterPumpStatusOP, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("SettingLogic GetBarrelMotorInputs() FilterPumpStationName", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        __FilterPumpSettingsData.Add(_FilterPumpStationName);
                        index = index + 1;
                    }
                }
                return __FilterPumpSettingsData;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingLogic GetBarrelMotorInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return __FilterPumpSettingsData;
            }
        }
        //ok but some input address are pending
        public static ObservableCollection<DosingSettingsEntity> GetDosingPumpInputs()
        {
            ObservableCollection<DosingSettingsEntity> __DosingPumpSettingsData = new ObservableCollection<DosingSettingsEntity>();
            try
            {
                //string[] DosingAM = new string[] { "1", "1", "1", "0", "0", "0", "0", "1", "1", "1", "0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };
                //string[] DosingManualONOFF = new string[] { "1", "1", "1", "0", "0", "0", "0", "1", "1", "1", "0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };
                //string[] TimeFlowBased = new string[] { "1", "1", "1", "0", "0", "0", "0", "1", "1", "1", "0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };
                //string[] DosingOP = new string[] { "1", "1", "1", "0", "0", "0", "0", "1", "1", "1", "0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };
                //string[] DosingQuantity = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21" };
                //string[] DosingFlowRate = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21" };
                //string[] DosingAmpSP = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                //string[] DosingActualAmp = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21" };
                //string[] DosingRemaningTime = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21" };
                //string[] DosingTimeSP = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21" };
                //string[] DosingcumAmpHr = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
                //string[] SetPH = new string[] { "1", "2", "3", "4" };
                //string[] ActualPH = new string[] { "1", "2", "3", "4" };



                string[] DosingAM; string[] DosingManualONOFF; string[] TimeFlowBased; string[] DosingOP; string[] DosingQuantity; string[] SetPH; string[] ActualPH;
                string[] DosingFlowRate; string[] DosingAmpSP; string[] DosingActualAmp; string[] DosingRemaningTime; string[] DosingTimeSP; string[] DosingcumAmpHr;
                if (IndiSCADAGlobalLibrary.TagList.SettingScreenOpen == true)
                {
                    DosingAM = IndiSCADAGlobalLibrary.TagList.DosingAutoManual;               //Get DosingManualONOFF from PLC   
                    DosingManualONOFF = IndiSCADAGlobalLibrary.TagList.DosingManualOffOn;
                    TimeFlowBased = IndiSCADAGlobalLibrary.TagList.DosingTimeFlowrate;
                    DosingOP = IndiSCADAGlobalLibrary.TagList.DosingOnOrOffStatus;
                    DosingQuantity = IndiSCADAGlobalLibrary.TagList.DosingQuantity;
                    DosingFlowRate = IndiSCADAGlobalLibrary.TagList.DosingFlowRate;
                    DosingAmpSP = IndiSCADAGlobalLibrary.TagList.DosingSetAmpHr;
                    DosingActualAmp = IndiSCADAGlobalLibrary.TagList.DosingActualAmpHr;
                    DosingRemaningTime = IndiSCADAGlobalLibrary.TagList.DosingRemainingTime;
                    DosingTimeSP = IndiSCADAGlobalLibrary.TagList.DosingTimeInSec;
                    DosingcumAmpHr = IndiSCADAGlobalLibrary.TagList.DosingCumulativeAmpHr;
                    SetPH = IndiSCADAGlobalLibrary.TagList.SetPH;
                    ActualPH = IndiSCADAGlobalLibrary.TagList.DosingpHActual;
                }
                else
                {
                    //Get Dosing pump Dosing AutoManual from PLC
                    DosingAM = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("DOSINGAutoOrManual");                //Get DosingManualONOFF from PLC   

                    DosingManualONOFF = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("DOSINGManualOffOrOn");

                    //Get TimeFlowBased from PLC
                    TimeFlowBased = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("DOSINGTimerBasedOrFlowrateBased");

                    //Get DosingOP from PLC
                    DosingOP = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("DOSINGOnOrOffStatus");

                    //Get DosingQuantity from PLC
                    DosingQuantity = DeviceCommunication.CommunicationWithPLC.Real_ReadPLCTagValue("DosingQuantityInml");

                    //Get DosingFlowRate from PLC
                    DosingFlowRate = DeviceCommunication.CommunicationWithPLC.Real_ReadPLCTagValue("DosingFlowRatemlperSec");

                    //Get DosingActualAmp from PLC
                    DosingActualAmp = DeviceCommunication.CommunicationWithPLC.Real_ReadPLCTagValue("DosingActualAmpHr");


                    //Get DosingRemaningTime from PLC
                    DosingRemaningTime = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("DosingRemainingTimeInSec");

                    //Get DosingTimeSP from PLC
                    DosingTimeSP = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("DosingTimeInSec");


                    //Get DosingAmpSP from PLC
                    DosingAmpSP = DeviceCommunication.CommunicationWithPLC.Real_ReadPLCTagValue("DosingSetAmpHr");

                    DosingcumAmpHr = DeviceCommunication.CommunicationWithPLC.Real_ReadPLCTagValue("DosingCumulativeAmpHr");//dINT 

                    SetPH = DivideBy(DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("DosingSetPH"), 100, 1);

                    ActualPH = DivideBy(DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("DosingActPH"), 100, 1);
                }


                int index = 0;
                if (_DosingPumpStationName.Response != null)
                {
                    string[] ChemicalNameToshow = new string[] { "", "" };

                    IList<IOStatusEntity> lstDosingPump = (IList<IOStatusEntity>)(_DosingPumpStationName.Response);
                    foreach (var item in lstDosingPump)
                    {
                        DosingSettingsEntity _DsoingPumpStationName = new DosingSettingsEntity();
                        _DsoingPumpStationName.StationName = item.ParameterName;

                        string ChemicalNm = ""; 
                     
                         DataRow[] rw = ChemicalNamesWithPercentageDT.Select(" PumpNo='" + Convert.ToString(index + 1) + "'");
                        if (rw.Count() > 0)
                        {
                            if (rw.Count() == 1)
                            {
                                _DsoingPumpStationName.ChemicalName1 = rw[0]["ChemicalName"].ToString();
                            }
                            else if (rw.Count() == 2)
                            {
                                _DsoingPumpStationName.ChemicalName1 = rw[0]["ChemicalName"].ToString();
                                _DsoingPumpStationName.ChemicalName2 = rw[1]["ChemicalName"].ToString();
                            }
                            else if (rw.Count() == 3)
                            {
                                _DsoingPumpStationName.ChemicalName1 = rw[0]["ChemicalName"].ToString();
                                _DsoingPumpStationName.ChemicalName2 = rw[1]["ChemicalName"].ToString();
                                _DsoingPumpStationName.ChemicalName3 = rw[2]["ChemicalName"].ToString();
                            }
                            else if (rw.Count() == 4)
                            {
                                _DsoingPumpStationName.ChemicalName1 = rw[0]["ChemicalName"].ToString();
                                _DsoingPumpStationName.ChemicalName2 = rw[1]["ChemicalName"].ToString();
                                _DsoingPumpStationName.ChemicalName3 = rw[2]["ChemicalName"].ToString();
                                _DsoingPumpStationName.ChemicalName4 = rw[3]["ChemicalName"].ToString(); 
                            }
                            else if (rw.Count() == 5)
                            {
                                _DsoingPumpStationName.ChemicalName1 = rw[0]["ChemicalName"].ToString();
                                _DsoingPumpStationName.ChemicalName2 = rw[1]["ChemicalName"].ToString();
                                _DsoingPumpStationName.ChemicalName3 = rw[2]["ChemicalName"].ToString();
                                _DsoingPumpStationName.ChemicalName4 = rw[3]["ChemicalName"].ToString(); 
                                _DsoingPumpStationName.ChemicalName5 = rw[4]["ChemicalName"].ToString();
                            }
                        }
                        else
                        {
                            rw = chemicalNm.Select(" PumpNo='" + Convert.ToString(index + 1) + "'");
                            _DsoingPumpStationName.ChemicalName1 =  rw[0]["ChemicalName"].ToString();
                        }

                        //previous commented by sbs
                        //DataRow[] rw = chemicalNm.Select(" PumpNo='" + Convert.ToString(index+1) + "'");
                        //string ChemicalNm = rw[0]["ChemicalName"].ToString();
                         
                        ErrorLogger.LogError.ErrorLog("settingLogic() GetDosingPumpInputs() 2 lstDosingPump.Count=" + lstDosingPump.Count + " DosingAM" + DosingAM.Length + " DosingRemaningTime" + DosingRemaningTime.Length + " DosingcumAmpHr" + DosingcumAmpHr.Length, DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                         
                        try
                        {
                            if (lstDosingPump.Count == DosingAM.Length && lstDosingPump.Count == DosingManualONOFF.Length)
                            {
                                _DsoingPumpStationName.DosingID = index.ToString();
                                _DsoingPumpStationName.AutoManual = ValueFromArray(DosingAM, index);
                                _DsoingPumpStationName.ManualONOFF = ValueFromArray(DosingManualONOFF, index);
                                _DsoingPumpStationName.FlowRateTimerBased = ValueFromArray(TimeFlowBased, index);
                                _DsoingPumpStationName.ONOFFStatus = ValueFromArray(DosingOP, index);
                          
                                //added by sbs because we get value for first 6 dosing.so index is outof bound then place "" 
                                try
                                {
                                    _DsoingPumpStationName.Quantity = ValueFromArray(DosingQuantity, index);
                                    _DsoingPumpStationName.FlowRate = ValueFromArray(DosingFlowRate, index);
                                    _DsoingPumpStationName.RemainingTime = ValueFromArray(DosingRemaningTime, index);


                                    //dosing with current
                                    if (index<12)
                                    {
                                        _DsoingPumpStationName.SetAmp = ValueFromArray(DosingAmpSP, index);
                                        _DsoingPumpStationName.CumulativeAmphr = ValueFromArray(DosingcumAmpHr, index);
                                        _DsoingPumpStationName.ActualAmp = ValueFromArray(DosingActualAmp, index);
                                    }
                                    else
                                    {
                                        _DsoingPumpStationName.SetAmp = "";
                                        _DsoingPumpStationName.CumulativeAmphr = "";
                                    }
                                     


                                    //dosing with ph
                                    if (index == 12)
                                    {
                                        _DsoingPumpStationName.setPH = ValueFromArray(SetPH, 0);
                                        _DsoingPumpStationName.ActualpH = ValueFromArray(ActualPH, 0);
                                    }
                                    else if (index == 13)
                                    {
                                        _DsoingPumpStationName.setPH = ValueFromArray(SetPH, 1);
                                        _DsoingPumpStationName.ActualpH = ValueFromArray(ActualPH, 1);
                                    }
                                    else if (index == 16)
                                    {
                                        _DsoingPumpStationName.setPH = ValueFromArray(SetPH, 2);
                                        _DsoingPumpStationName.ActualpH = ValueFromArray(ActualPH, 2);
                                    }
                                    else if (index == 19)
                                    {
                                        _DsoingPumpStationName.setPH = ValueFromArray(SetPH, 3);
                                        _DsoingPumpStationName.ActualpH = ValueFromArray(ActualPH, 3);
                                    } 
                                }
                                catch (Exception ex)
                                {
                                    ErrorLogger.LogError.ErrorLog("settingLogic() GetDosingPumpInputs() Error while reading SetAmp/ActualAmp/RemainingTime", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                                    _DsoingPumpStationName.Quantity = "";
                                    _DsoingPumpStationName.FlowRate = "";
                                    _DsoingPumpStationName.SetAmp = ""; 
                                    _DsoingPumpStationName.ActualAmp = "";
                                    _DsoingPumpStationName.RemainingTime = "";
                                    _DsoingPumpStationName.CumulativeAmphr = "";
                                }


                                _DsoingPumpStationName.SetTime = ValueFromArray(DosingTimeSP, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("settingLogic() GetDosingPumpInputs() Error in for loop", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        __DosingPumpSettingsData.Add(_DsoingPumpStationName);
                        
                        index = index + 1;
                    }
                }
                return __DosingPumpSettingsData;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingLogic GetDosingPumpInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return __DosingPumpSettingsData;
            }
        }
         //ok      
        public static ObservableCollection<TankBypassEntity> GetTankBypassInputs()
        {
            ObservableCollection<TankBypassEntity> _TankData = new ObservableCollection<TankBypassEntity>();
            try
            {

                //string[] BypassOnOff = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18" };

                string[] BypassOnOff;
                if (IndiSCADAGlobalLibrary.TagList.SettingScreenOpen == true)
                {
                    //Get pHHighSP from PLC
                    BypassOnOff = IndiSCADAGlobalLibrary.TagList.TankBypass;
                  
                }
                else
                {
                    //Get Bypass status from PLC
                    BypassOnOff = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("BypassplatingTankBypassPlating");
                } 

                int index = 0;
                if (_TankBypassStationName.Response != null)
                {
                    IList<IOStatusEntity> lstTanks = (IList<IOStatusEntity>)(_TankBypassStationName.Response);
                    foreach (var item in lstTanks)
                    {
                        TankBypassEntity _TankBypassEntity = new TankBypassEntity();
                        _TankBypassEntity.TankName = item.ParameterName;
                        try
                        {
                            if (lstTanks.Count == BypassOnOff.Length )
                            {
                                _TankBypassEntity.TankBypassID = index.ToString();
                                _TankBypassEntity.BypassOnOff = ValueFromArray(BypassOnOff, index);                             
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("SettingLogic GetTankBypassInputs() TankBypassStationName", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _TankData.Add(_TankBypassEntity);
                        index = index + 1;
                    }
                }
                return _TankData;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingLogic GetTankBypassInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _TankData;
            }
        }
        //ok
        public static ObservableCollection<TankBypassEntity> GetTrayBypassInputs()
        {
            ObservableCollection<TankBypassEntity> _TrayData = new ObservableCollection<TankBypassEntity>();
            try
            {
                //string[] BypassOnOff = new string[] { "1", "0", "1", "4", "5", "6", "7" };//, "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19" };
                //Get Bypass status from PLC

    
                string[] BypassOnOff;
                if (IndiSCADAGlobalLibrary.TagList.SettingScreenOpen == true)
                { 
                    BypassOnOff = IndiSCADAGlobalLibrary.TagList.TrayBypass;

                }
                else
                { 
                    BypassOnOff = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("TrayBypassBypass");
                }

                int index = 0;
                if (_TrayBypassStationName.Response != null)
                {
                    IList<IOStatusEntity> lstTanks = (IList<IOStatusEntity>)(_TrayBypassStationName.Response);
                    foreach (var item in lstTanks)
                    {
                        TankBypassEntity _TrayBypassEntity = new TankBypassEntity();
                        _TrayBypassEntity.TankName = item.ParameterName;
                        try
                        {
                            if (lstTanks.Count == BypassOnOff.Length)
                            {
                                _TrayBypassEntity.TankBypassID = index.ToString();
                                _TrayBypassEntity.BypassOnOff = ValueFromArray(BypassOnOff, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("SettingLogic GetTankBypassInputs() TrayBypassEntity", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _TrayData.Add(_TrayBypassEntity);
                        index = index + 1;
                    }
                }
                return _TrayData;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingLogic GetTankBypassInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _TrayData;
            }
        }
        //ok
        public static ObservableCollection<TankBypassEntity> GetBarrelBypassInputs()
        {
            ObservableCollection<TankBypassEntity> _TrayData = new ObservableCollection<TankBypassEntity>();
            try
            {
                //string[] BypassOnOff = new string[] { "1", "1", "1" };



                string[] BypassOnOff;
                if (IndiSCADAGlobalLibrary.TagList.SettingScreenOpen == true)
                {
                    BypassOnOff = IndiSCADAGlobalLibrary.TagList.DryerBypass; 
                }
                else
                {
                     BypassOnOff = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("DryerBypass");
                }


                int index = 0;
                if (_BarrelBypassStationName.Response != null)
                {
                    IList<IOStatusEntity> lstTanks = (IList<IOStatusEntity>)(_BarrelBypassStationName.Response);
                    foreach (var item in lstTanks)
                    {
                        TankBypassEntity _TrayBypassEntity = new TankBypassEntity();
                        _TrayBypassEntity.TankName = item.ParameterName;
                        try
                        {
                            if (lstTanks.Count == BypassOnOff.Length)
                            {
                                _TrayBypassEntity.TankBypassID = index.ToString();
                                _TrayBypassEntity.BypassOnOff = ValueFromArray(BypassOnOff, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("SettingLogic GetBarrelBypassInputs() TrayBypassEntity", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _TrayData.Add(_TrayBypassEntity);
                        index = index + 1;
                    }
                }
                return _TrayData;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingLogic GetBarrelBypassInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _TrayData;
            }
        }

        public static string GetNitricSelection()
        {
            try
            {
                //string[] BypassOnOff = new string[] { "1" };

                string[] BypassOnOff = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("NitricTankSelection");

                return BypassOnOff[0];
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingLogic GetBarrelBypassInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return "";
            }
        }
        

        public static ObservableCollection<BaseBarrelMotorEntity> GetBaseBarrelMotorInputs()
        {
            ObservableCollection<BaseBarrelMotorEntity> __BaseBarrelMotorData = new ObservableCollection<BaseBarrelMotorEntity>();
            try
            {
                //string[] BaseBarrelMotorONOFF = new string[] { "1", "0", "0", "1", "0", "0", "1", "0", "0", "1", "0", "0", "1", "1", "0", "0", "0", "0", "1", "0", "1", "1", "1", "1", "1", "0", "1", "1", "1", "1", "1", "0", "0", "0", "0", "1", "1", "1" };
                //string[] BaseBarrelMotorTrip = new string[] { "1", "0", "0", "1", "0", "0", "1", "0", "0", "1", "0", "0", "1", "1", "0", "0", "0", "0", "1", "0", "1", "1", "1", "1", "1", "0", "1", "1", "1", "1", "1", "0", "0", "0", "0", "1", "1", "1" };
                //string[] BaseBarrelMotorStatus = new string[] { "1", "0", "0", "1", "0", "0", "1", "0", "0", "1", "0", "0", "1", "1", "0", "0", "0", "0", "1", "0", "1", "1", "1", "1", "1", "0", "1", "1", "1", "1", "1", "0", "0", "0", "0", "1", "1", "1" };



                string[] BaseBarrelMotorONOFF; string[] BaseBarrelMotorTrip; string[] BaseBarrelMotorStatus;
                if (IndiSCADAGlobalLibrary.TagList.SettingScreenOpen == true)
                {
                    BaseBarrelMotorONOFF = IndiSCADAGlobalLibrary.TagList.BaseBarrelMotorONOFF;
                    BaseBarrelMotorTrip = IndiSCADAGlobalLibrary.TagList.BaseBarrelMotorTrip;
                    BaseBarrelMotorStatus = IndiSCADAGlobalLibrary.TagList.BaseBarrelMotorStatus;
                }
                else
                {
                     BaseBarrelMotorONOFF = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("BaseBarrelMotorONOFF");
                     BaseBarrelMotorTrip = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("BaseBarrelMotorTrip");
                     BaseBarrelMotorStatus = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("BaseBarrelMotorStatus");
                }


                int index = 0;
                if (_BaseBarrelMotorStationName.Response != null)
                { 
                    IList<IOStatusEntity> lstDosingPump = (IList<IOStatusEntity>)(_BaseBarrelMotorStationName.Response);

                    ErrorLogger.LogError.ErrorLog("settingLogic() GetOilSkimmerMotorInputs() 1 lstDosingPump.Count=" + lstDosingPump.Count + " BaseBarrelMotorONOFF" + BaseBarrelMotorONOFF.Length + " BaseBarrelMotorTrip" + BaseBarrelMotorTrip.Length + " BaseBarrelMotorStatus" + BaseBarrelMotorStatus.Length, DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);


                    foreach (var item in lstDosingPump)
                    {
                        if (index < 19)
                        {
                            BaseBarrelMotorEntity _BaseBarrelMotorStationName = new BaseBarrelMotorEntity();
                            _BaseBarrelMotorStationName.BaseBarrelMotorName = item.ParameterName;

                            try
                            {
                                if (lstDosingPump.Count == BaseBarrelMotorONOFF.Length && lstDosingPump.Count == BaseBarrelMotorTrip.Length)
                                {
                                    _BaseBarrelMotorStationName.BaseBarrelMotorID = index.ToString();
                                    _BaseBarrelMotorStationName.BaseBarrelMotorOnOFF = ValueFromArray(BaseBarrelMotorONOFF, index);
                                    _BaseBarrelMotorStationName.BaseBarrelMotorTrip = ValueFromArray(BaseBarrelMotorTrip, index);
                                    _BaseBarrelMotorStationName.BaseBarrelMotorStatus = ValueFromArray(BaseBarrelMotorStatus, index);
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("settingLogic() GetBaseBarrelMotorInputs() Error in for loop", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                            __BaseBarrelMotorData.Add(_BaseBarrelMotorStationName);
                        }

                        index = index + 1;
                    }
                }
                return __BaseBarrelMotorData;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingLogic GetBaseBarrelMotorInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return __BaseBarrelMotorData;
            }
        }
        public static ObservableCollection<BaseBarrelMotorEntity> GetBaseBarrelMotorInputs2()
        {
            ObservableCollection<BaseBarrelMotorEntity> __BaseBarrelMotorData = new ObservableCollection<BaseBarrelMotorEntity>();
            try
            {
                //string[] BaseBarrelMotorONOFF = new string[] { "1", "0", "0", "1", "0", "0", "1", "0", "0", "1", "0", "0", "1", "1", "0", "0", "0", "0", "1", "0", "1", "1", "1", "1", "1", "0", "1", "1", "1", "1", "1", "0", "0", "0", "0", "1", "1", "1" };
                //string[] BaseBarrelMotorTrip = new string[] { "1", "0", "0", "1", "0", "0", "1", "0", "0", "1", "0", "0", "1", "1", "0", "0", "0", "0", "1", "0", "1", "1", "1", "1", "1", "0", "1", "1", "1", "1", "1", "0", "0", "0", "0", "1", "1", "1" };
                //string[] BaseBarrelMotorStatus = new string[] { "1", "0", "0", "1", "0", "0", "1", "0", "0", "1", "0", "0", "1", "1", "0", "0", "0", "0", "1", "0", "1", "1", "1", "1", "1", "0", "1", "1", "1", "1", "1", "0", "0", "0", "0", "1", "1", "1" };
                 
                string[] BaseBarrelMotorONOFF; string[] BaseBarrelMotorTrip; string[] BaseBarrelMotorStatus;

                if (IndiSCADAGlobalLibrary.TagList.SettingScreenOpen == true)
                {
                    BaseBarrelMotorONOFF = IndiSCADAGlobalLibrary.TagList.BaseBarrelMotorONOFF;
                    BaseBarrelMotorTrip = IndiSCADAGlobalLibrary.TagList.BaseBarrelMotorTrip;
                    BaseBarrelMotorStatus = IndiSCADAGlobalLibrary.TagList.BaseBarrelMotorStatus;
                }
                else
                {
                    BaseBarrelMotorONOFF = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("BaseBarrelMotorONOFF");
                    BaseBarrelMotorTrip = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("BaseBarrelMotorTrip");
                    BaseBarrelMotorStatus = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("BaseBarrelMotorStatus");
                }



                int index = 0;
                if (_BaseBarrelMotorStationName.Response != null)
                {
                   

                    IList<IOStatusEntity> lstDosingPump = (IList<IOStatusEntity>)(_BaseBarrelMotorStationName.Response);

                    ErrorLogger.LogError.ErrorLog("settingLogic() GetOilSkimmerMotorInputs() 2 lstDosingPump.Count=" + lstDosingPump.Count + " BaseBarrelMotorONOFF" + BaseBarrelMotorONOFF.Length + " BaseBarrelMotorTrip" + BaseBarrelMotorTrip.Length + " BaseBarrelMotorStatus" + BaseBarrelMotorStatus.Length, DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);


                    foreach (var item in lstDosingPump)
                    {
                        if (index > 18)
                        {
                            BaseBarrelMotorEntity _BaseBarrelMotorStationName = new BaseBarrelMotorEntity();
                            _BaseBarrelMotorStationName.BaseBarrelMotorName = item.ParameterName;

                            try
                            {
                                if (lstDosingPump.Count == BaseBarrelMotorONOFF.Length && lstDosingPump.Count == BaseBarrelMotorTrip.Length)
                                {
                                    _BaseBarrelMotorStationName.BaseBarrelMotorID = index.ToString();
                                    _BaseBarrelMotorStationName.BaseBarrelMotorOnOFF = ValueFromArray(BaseBarrelMotorONOFF, index);
                                    _BaseBarrelMotorStationName.BaseBarrelMotorTrip = ValueFromArray(BaseBarrelMotorTrip, index);
                                    _BaseBarrelMotorStationName.BaseBarrelMotorStatus = ValueFromArray(BaseBarrelMotorStatus, index);
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("settingLogic() GetBaseBarrelMotorInputs() Error in for loop", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                            __BaseBarrelMotorData.Add(_BaseBarrelMotorStationName);
                        }

                        index = index + 1;
                    }
                }
                return __BaseBarrelMotorData;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingLogic GetBaseBarrelMotorInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return __BaseBarrelMotorData;
            }
        }


        public static ObservableCollection<OilSkimmerEntity> GetOilSkimmerMotorInputs()
        {
            ObservableCollection<OilSkimmerEntity> __BaseBarrelMotorData = new ObservableCollection<OilSkimmerEntity>();
            try
            {
                //string[] BaseOilSkimmerONOFF = new string[] { "1", "0", "0", "1" };
                //string[] BaseOilSkimmerTrip = new string[] { "1", "0", "0", "1" };
                //string[] BaseOilSkimmerStatus = new string[] { "1", "0", "0", "1" };

                string[] BaseOilSkimmerONOFF; string[] BaseOilSkimmerTrip; string[] BaseOilSkimmerStatus;
                if (IndiSCADAGlobalLibrary.TagList.SettingScreenOpen == true)
                {
                    BaseOilSkimmerONOFF = IndiSCADAGlobalLibrary.TagList.OilSkimmerAutoManual;
                    BaseOilSkimmerTrip = IndiSCADAGlobalLibrary.TagList.OilSkimmerTrip;
                    BaseOilSkimmerStatus = IndiSCADAGlobalLibrary.TagList.OilSkimmerOutput;
                }
                else
                {
                    BaseOilSkimmerONOFF = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("OilSkimmerAutoManual");
                    BaseOilSkimmerTrip = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("OilSkimmerTrip");
                    BaseOilSkimmerStatus = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("OilSkimmerOutput");
                }


                int index = 0;
                if (_OilSkimmerStationName.Response != null)
                {
                    IList<IOStatusEntity> lstDosingPump = (IList<IOStatusEntity>)(_OilSkimmerStationName.Response);
                    foreach (var item in lstDosingPump)
                    {
                        ErrorLogger.LogError.ErrorLog("settingLogic() GetOilSkimmerMotorInputs() lstDosingPump.Count=" + lstDosingPump.Count + " BaseOilSkimmerONOFF" + BaseOilSkimmerONOFF.Length + " BaseOilSkimmerTrip" + BaseOilSkimmerTrip.Length + " BaseOilSkimmerStatus" + BaseOilSkimmerStatus.Length, DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                        OilSkimmerEntity _BaseBarrelMotorStationName = new OilSkimmerEntity();
                            _BaseBarrelMotorStationName.OilSkimmerMotorName = item.ParameterName;

                            try
                            {
                                if (lstDosingPump.Count == BaseOilSkimmerONOFF.Length && lstDosingPump.Count == BaseOilSkimmerTrip.Length)
                                {
                                    _BaseBarrelMotorStationName.OilSkimmerMotorID = index.ToString();
                                    _BaseBarrelMotorStationName.BaseOilSkimmerOnOFF = ValueFromArray(BaseOilSkimmerONOFF, index);
                                    _BaseBarrelMotorStationName.BaseOilSkimmerTrip = ValueFromArray(BaseOilSkimmerTrip, index);
                                    _BaseBarrelMotorStationName.BaseOilSkimmerStatus = ValueFromArray(BaseOilSkimmerStatus, index);
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("settingLogic() GetOilSkimmerMotorInputs() Error in for loop", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                            __BaseBarrelMotorData.Add(_BaseBarrelMotorStationName);
                      

                        index = index + 1;
                    }
                }
                return __BaseBarrelMotorData;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingLogic GetBaseBarrelMotorInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return __BaseBarrelMotorData;
            }
        }


        public static string[] DivideBy(string[] SourceValue, int devidefactor,int index)
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

     
        public static ObservableCollection<TopSpraySettingsEntity> GetTopSprayInputs()
        {
            ObservableCollection<TopSpraySettingsEntity> __TopSprayData = new ObservableCollection<TopSpraySettingsEntity>();
            try
            {

                //TopSprayAutoManual
                string[] TopSprayAutoManual = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("SprayPumpInputAutoManual");
                //Get TopSprayOnOFF from PLC
                string[] TopSprayOnOFF = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("SprayPumpInputOnOff");
                //Get TopSprayServiceOnOFF from PLC
                //string[] TopSprayServiceOnOFF = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("TopSprayServiceOnOFF");
                //Get TopSprayTripOP from PLC
                string[] TopSprayTripOP = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("SprayPumpInputTrip");
                //Get TopSprayStatusOP from PLC
                string[] TopSprayStatusOP = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("SprayPumpOutput");
                int index = 0;
                if (_TopSprayStationName.Response != null)
                {
                    IList<IOStatusEntity> lstTopSpray = (IList<IOStatusEntity>)(_TopSprayStationName.Response);
                    foreach (var item in lstTopSpray)
                    {
                        TopSpraySettingsEntity _TopSprayStationName = new TopSpraySettingsEntity();
                        _TopSprayStationName.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstTopSpray.Count == TopSprayTripOP.Length && lstTopSpray.Count == TopSprayAutoManual.Length)
                            {
                                _TopSprayStationName.TopSprayID = index.ToString();
                                _TopSprayStationName.AutoManual = ValueFromArray(TopSprayAutoManual, index);
                                _TopSprayStationName.ManualOnOff = ValueFromArray(TopSprayOnOFF, index);
                                //_TopSprayStationName.ServiceOnOff = ValueFromArray(TopSprayServiceOnOFF,index);
                                _TopSprayStationName.TripStatus = ValueFromArray(TopSprayTripOP, index);
                                _TopSprayStationName.Status = ValueFromArray(TopSprayStatusOP, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("SettingLogic GetTopSprayInputs() lstTopSpray", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        __TopSprayData.Add(_TopSprayStationName);
                        index = index + 1;
                    }
                }
                return __TopSprayData;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingLogic GetTopSprayInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return __TopSprayData;
            }
        }

        public static ObservableCollection<UtilitySettingEntity> GetUtilityInputs()
        {
            ObservableCollection<UtilitySettingEntity> __UtilityData = new ObservableCollection<UtilitySettingEntity>();
            try
            {

                
                //Get UtilityOnOFF from PLC
                string[] UtilityOnOFF = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("UtilityInputOnOff");
                //Get TopSprayServiceOnOFF from PLC
                //string[] TopSprayServiceOnOFF = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("TopSprayServiceOnOFF");
                //Get TopSprayTripOP from PLC
                string[] UtilityTripOP = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("UtilityInputTrip");
                //Get TopSprayStatusOP from PLC
                string[] UtilityStatusOP = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("UtilityOutputOutput");
                int index = 0;
                if (_UtilityStationName.Response != null)
                {
                    IList<IOStatusEntity> lstUtility = (IList<IOStatusEntity>)(_UtilityStationName.Response);
                    foreach (var item in lstUtility)
                    {
                        UtilitySettingEntity _UtilityStationName = new UtilitySettingEntity();
                        _UtilityStationName.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstUtility.Count == UtilityTripOP.Length && lstUtility.Count == UtilityOnOFF.Length)
                            {
                                _UtilityStationName.UtilityID = index.ToString();
                                //_UtilityStationName.AutoManual = ValueFromArray(TopSprayAutoManual, index);
                                _UtilityStationName.ManualOnOff = ValueFromArray(UtilityOnOFF, index);
                                //_TopSprayStationName.ServiceOnOff = ValueFromArray(TopSprayServiceOnOFF,index);
                                _UtilityStationName.TripStatus = ValueFromArray(UtilityTripOP, index);
                                _UtilityStationName.Status = ValueFromArray(UtilityStatusOP, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("SettingLogic GetUtilityInputs() UtilityStationName", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        __UtilityData.Add(_UtilityStationName);
                        index = index + 1;
                    }
                }
                return __UtilityData;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingLogic GetUtilityInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return __UtilityData;
            }
        }

        public static ObservableCollection<MechanicalAgitationSettingsEntity> GetMechanicalAgitationInputs()
        {
            ObservableCollection<MechanicalAgitationSettingsEntity> __MechanicalAgitationSettingsData = new ObservableCollection<MechanicalAgitationSettingsEntity>();
            try
            {

                //Get MechAgitationHomeProxy from PLC
                string[] MechAgitationHomeProxy = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("MechanicalAgitationInputProximity");
                //Get MechAgitationTripStatus from PLC
                string[] MechAgitationTripStatus = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("MechanicalAgitationInputCBTrip");
                //Get MechAgitationAutoManual from PLC
                string[] MechAgitationAutoManual = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("MechanicalAgitationInputAutoManual");
                //Get MechAgitationOnOFF from PLC
                string[] MechAgitationOnOFF = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("MechanicalAgitationInputOnOff");
                //Get MechAgitationServiceOnOFF from PLC
                //string[] MechAgitationServiceOnOFF = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("MechAgitationServiceOnOFF");
                //Get MechAgitationStatusOP from PLC
                string[] MechAgitationStatusOP = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("MechanicalAgitationOutput");
                int index = 0;
                if (_MechanicalAgitationStationName.Response != null)
                {
                    IList<IOStatusEntity> lstAgitation = (IList<IOStatusEntity>)(_MechanicalAgitationStationName.Response);
                    foreach (var item in lstAgitation)
                    {
                        MechanicalAgitationSettingsEntity _MechanicalAgitationSettings = new MechanicalAgitationSettingsEntity();
                        _MechanicalAgitationSettings.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstAgitation.Count == MechAgitationAutoManual.Length && lstAgitation.Count == MechAgitationTripStatus.Length)
                            {
                                _MechanicalAgitationSettings.AgitationID = index.ToString();
                                _MechanicalAgitationSettings.ManualOnOff = ValueFromArray(MechAgitationOnOFF, index);
                                //_MechanicalAgitationSettings.ServiceOnOff = ValueFromArray(MechAgitationServiceOnOFF,index);
                                _MechanicalAgitationSettings.TripStatus = ValueFromArray(MechAgitationTripStatus, index);
                                _MechanicalAgitationSettings.Status = ValueFromArray(MechAgitationStatusOP, index);
                                _MechanicalAgitationSettings.HomeProxy = ValueFromArray(MechAgitationHomeProxy, index);
                                _MechanicalAgitationSettings.AutoManual = ValueFromArray(MechAgitationAutoManual, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("SettingLogic GetMechanicalAgitationInputs() MechanicalAgitationSettings", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        __MechanicalAgitationSettingsData.Add(_MechanicalAgitationSettings);
                        index = index + 1;
                    }
                }
                return __MechanicalAgitationSettingsData;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingLogic GetMechanicalAgitationInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return __MechanicalAgitationSettingsData;
            }
        }

        public static ObservableCollection<FilterPumpSettingsEntity> GetFilterPumpInputs()
        {
            ObservableCollection<FilterPumpSettingsEntity> __FilterPumpSettingsData = new ObservableCollection<FilterPumpSettingsEntity>();
            try
            {
                //string[] FilterPumpOnOFF = { "1","1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };
                //string[] FilterPumpTripOP = { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };
                //string[] FilterPumpStatusOP = { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };

                string[] FilterPumpOnOFF; string[] FilterPumpStatusOP; string[] FilterPumpTripOP;
                if (IndiSCADAGlobalLibrary.TagList.SettingScreenOpen == true)
                {
                    FilterPumpOnOFF = IndiSCADAGlobalLibrary.TagList.FilterPumpOnOFF;
                    FilterPumpTripOP = IndiSCADAGlobalLibrary.TagList.FilterPumpInputCBTrip;
                    FilterPumpStatusOP = IndiSCADAGlobalLibrary.TagList.FilterPumpOutput;
                }
                else
                {
                    FilterPumpOnOFF = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("FilterPumpOnOFF");
                    FilterPumpTripOP = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("FilterPumpInputCBTrip");
                    FilterPumpStatusOP = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("FilterPumpOutput");
                } 


                int index = 0;
                if (_FilterPumpStationName.Response != null)
                {
                    IList<IOStatusEntity> lstFilterPump = (IList<IOStatusEntity>)(_FilterPumpStationName.Response);
                    foreach (var item in lstFilterPump)
                    {
                        FilterPumpSettingsEntity _FilterPumpStationName = new FilterPumpSettingsEntity();
                        _FilterPumpStationName.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstFilterPump.Count == FilterPumpOnOFF.Length && lstFilterPump.Count == FilterPumpTripOP.Length)
                            {
                                _FilterPumpStationName.PumpOnOffID = index.ToString();
                                _FilterPumpStationName.ManualOnOff = ValueFromArray(FilterPumpOnOFF, index);
                                _FilterPumpStationName.TripStatus = ValueFromArray(FilterPumpTripOP, index);
                                _FilterPumpStationName.Status = ValueFromArray(FilterPumpStatusOP, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("SettingLogic GetFilterPumpInputs() FilterPumpSettingsData", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        __FilterPumpSettingsData.Add(_FilterPumpStationName);
                        index = index + 1;
                    }
                }
                return __FilterPumpSettingsData;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingLogic GetFilterPumpInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return __FilterPumpSettingsData;
            }
        }

        public static ObservableCollection<IOStatusEntity> GetWagonInputs()
        {
            ObservableCollection<IOStatusEntity> _WagonInputs = new ObservableCollection<IOStatusEntity>();
            try
            {
                //string[] Wagon1Inputs = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };

                string[] Wagon1Inputs;
                if (IndiSCADAGlobalLibrary.TagList.SettingScreenOpen == true)
                {
                    Wagon1Inputs = IndiSCADAGlobalLibrary.TagList.Wagon1WCSInput; 
                }
                else
                {
                      Wagon1Inputs = DeviceCommunication.CommunicationWithPLC.Word_ReadPLCTagValue("Wagon1WCSInput");
                }

                //string[] WagonSensorWidthInputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("WagonSensorWidthInput");
                //string[] WagonSensorWidthInputs = { "11", "22", "33", "44", "55" };

                int index = 0;
                if (_WagonInputParameterLists.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus2 = (IList<IOStatusEntity>)(_WagonInputParameterLists.Response);
                    foreach (var item in lstIOStatus2)
                    {
                        IOStatusEntity _IOStatusEntity = new IOStatusEntity();

                        _IOStatusEntity.ID = index.ToString();
                        _IOStatusEntity.ParameterName = item.ParameterName;

                        //if (index == 1)
                        //{
                        //    try
                        //    {
                        //        if (lstIOStatus2.Count == Wagon1Inputs.Length)
                        //        {
                        //            _IOStatusEntity.Value = ValueFromArray(WagonSensorWidthInputs, 0);
                        //        }
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagonInputs() WagonSensorWidthInput", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        //    }
                        //}
                        //else
                        //{
                            try
                            {
                                if (lstIOStatus2.Count == Wagon1Inputs.Length)
                                {
                                    _IOStatusEntity.Value = ValueFromArray(Wagon1Inputs, index);
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagonInputs() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        //}

                        _WagonInputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _WagonInputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagonInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _WagonInputs;
            }
        }
        public static ObservableCollection<IOStatusEntity> GetWagon2Inputs()
        {
            ObservableCollection<IOStatusEntity> _Wagon2Inputs = new ObservableCollection<IOStatusEntity>();
            try
            {


                //Get W1Inputs
                //string[] Wagon1Inputs = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        
                string[] Wagon1Inputs;
                if (IndiSCADAGlobalLibrary.TagList.SettingScreenOpen == true)
                {
                    Wagon1Inputs = IndiSCADAGlobalLibrary.TagList.Wagon2WCSInput;
                }
                else
                {
                    Wagon1Inputs = DeviceCommunication.CommunicationWithPLC.Word_ReadPLCTagValue("Wagon2WCSInput");
                }

                //string[] WagonSensorWidthInputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("WagonSensorWidthInput");
                int index = 0;
                if (_Wagon2InputParameterLists.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_Wagon2InputParameterLists.Response);
                    foreach (var item in lstIOStatus)
                    {
                        IOStatusEntity _IOStatusEntity = new IOStatusEntity();
                        _IOStatusEntity.ID = index.ToString();
                        _IOStatusEntity.ParameterName = item.ParameterName;

                        //if (index == 1)
                        //{
                        //    try
                        //    {
                        //        if (lstIOStatus.Count == Wagon1Inputs.Length)
                        //        {
                        //            _IOStatusEntity.Value = ValueFromArray(WagonSensorWidthInputs, 1);
                        //        }
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagonInputs() WagonSensorWidthInput", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        //    }
                        //}
                        //else
                        //{
                            try
                            {
                                if (lstIOStatus.Count == Wagon1Inputs.Length)
                                {
                                    _IOStatusEntity.Value = ValueFromArray(Wagon1Inputs, index);
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagon2Inputs() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        //}

                        _Wagon2Inputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _Wagon2Inputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagon2Inputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _Wagon2Inputs;
            }
        }
        public static ObservableCollection<IOStatusEntity> GetWagon3Inputs()
        {
            ObservableCollection<IOStatusEntity> _Wagon3Inputs = new ObservableCollection<IOStatusEntity>();
            try
            {
                //Get W1Inputs
         
                //string[] Wagon1Inputs = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" };

                string[] Wagon1Inputs;
                if (IndiSCADAGlobalLibrary.TagList.SettingScreenOpen == true)
                {
                    Wagon1Inputs = IndiSCADAGlobalLibrary.TagList.Wagon3WCSInput;
                }
                else
                {
                    Wagon1Inputs = DeviceCommunication.CommunicationWithPLC.Word_ReadPLCTagValue("Wagon3WCSInput");
                }


                //string[] WagonSensorWidthInputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("WagonSensorWidthInput");
                //string[] WagonSensorWidthInputs = { "11", "22", "33", "44", "55" };


                int index = 0;
                if (_Wagon3InputParameterLists.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_Wagon3InputParameterLists.Response);
                    foreach (var item in lstIOStatus)
                    {
                        IOStatusEntity _IOStatusEntity = new IOStatusEntity();
                        _IOStatusEntity.ID = index.ToString();
                        _IOStatusEntity.ParameterName = item.ParameterName;

                        //if (index == 1)
                        //{
                        //    try
                        //    {
                        //        if (lstIOStatus.Count == Wagon1Inputs.Length)
                        //        {
                        //            _IOStatusEntity.Value = ValueFromArray(WagonSensorWidthInputs, 2);
                        //        }
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagonInputs() WagonSensorWidthInput", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        //    }
                        //}
                        //else
                        //{
                            try
                            {
                                if (lstIOStatus.Count == Wagon1Inputs.Length)
                                {
                                    _IOStatusEntity.Value = ValueFromArray(Wagon1Inputs, index);
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagon3Inputs() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        //}

                        _Wagon3Inputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _Wagon3Inputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagon3Inputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _Wagon3Inputs;
            }
        }
        public static ObservableCollection<IOStatusEntity> GetWagon4Inputs()
        {
            ObservableCollection<IOStatusEntity> _Wagon4Inputs = new ObservableCollection<IOStatusEntity>();
            try
            { 
                //string[] Wagon1Inputs = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" };

                string[] Wagon1Inputs;
                if (IndiSCADAGlobalLibrary.TagList.SettingScreenOpen == true)
                {
                    Wagon1Inputs = IndiSCADAGlobalLibrary.TagList.Wagon4WCSInput;
                }
                else
                {
                    Wagon1Inputs = DeviceCommunication.CommunicationWithPLC.Word_ReadPLCTagValue("Wagon4WCSInput");
                }



                //string[] WagonSensorWidthInputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("WagonSensorWidthInput");

                int index = 0;
                if (_Wagon4InputParameterLists.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_Wagon4InputParameterLists.Response);
                    foreach (var item in lstIOStatus)
                    {
                        IOStatusEntity _IOStatusEntity = new IOStatusEntity();
                        _IOStatusEntity.ID = index.ToString();
                        _IOStatusEntity.ParameterName = item.ParameterName;

                        //if (index == 1)
                        //{
                        //    try
                        //    {
                        //        if (lstIOStatus.Count == Wagon1Inputs.Length)
                        //        {
                        //            _IOStatusEntity.Value = ValueFromArray(WagonSensorWidthInputs, 3);
                        //        }
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagonInputs() WagonSensorWidthInput", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        //    }
                        //}
                        //else
                        //{
                            try
                            {
                                if (lstIOStatus.Count == Wagon1Inputs.Length)
                                {
                                    _IOStatusEntity.Value = ValueFromArray(Wagon1Inputs, index);
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagon4Inputs() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        //}

                        _Wagon4Inputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _Wagon4Inputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagon4Inputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _Wagon4Inputs;
            }
        }
        public static ObservableCollection<IOStatusEntity> GetWagon5Inputs()
        {
            ObservableCollection<IOStatusEntity> _Wagon5Inputs = new ObservableCollection<IOStatusEntity>();
            try
            {
                //string[] Wagon1Inputs = { "1", "2", "3", "4", "5", "6", "7", "8", "9" }; 
        


                string[] Wagon1Inputs;
                if (IndiSCADAGlobalLibrary.TagList.SettingScreenOpen == true)
                {
                    Wagon1Inputs = IndiSCADAGlobalLibrary.TagList.Wagon5WCSInput;
                }
                else
                {
                    Wagon1Inputs = DeviceCommunication.CommunicationWithPLC.Word_ReadPLCTagValue("Wagon5WCSInput");
                }


                //string[] WagonSensorWidthInputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("WagonSensorWidthInput");

                int index = 0;
                if (_Wagon5InputParameterLists.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_Wagon5InputParameterLists.Response);
                    foreach (var item in lstIOStatus)
                    {
                        IOStatusEntity _IOStatusEntity = new IOStatusEntity();
                        _IOStatusEntity.ID = index.ToString();
                        _IOStatusEntity.ParameterName = item.ParameterName;

                        //if (index == 1)
                        //{
                        //    try
                        //    {
                        //        if (lstIOStatus.Count == Wagon1Inputs.Length)
                        //        {
                        //            _IOStatusEntity.Value = ValueFromArray(WagonSensorWidthInputs, 4);
                        //        }
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagonInputs() WagonSensorWidthInput", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        //    }
                        //}
                        //else
                        //{
                            try
                            {
                                if (lstIOStatus.Count == Wagon1Inputs.Length)
                                {
                                    _IOStatusEntity.Value = ValueFromArray(Wagon1Inputs, index);
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagon5Inputs() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        //}

                        _Wagon5Inputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _Wagon5Inputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagon5Inputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _Wagon5Inputs;
            }
        }
        public static ObservableCollection<IOStatusEntity> GetWagon6Inputs()
        {
            ObservableCollection<IOStatusEntity> _Wagon6Inputs = new ObservableCollection<IOStatusEntity>();
            try
            {
                //string[] Wagon1Inputs = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10","11" };
                

                string[] Wagon1Inputs;
                if (IndiSCADAGlobalLibrary.TagList.SettingScreenOpen == true)
                {
                    Wagon1Inputs = IndiSCADAGlobalLibrary.TagList.Wagon6WCSInput;
                }
                else
                {
                    Wagon1Inputs = Wagon1Inputs = DeviceCommunication.CommunicationWithPLC.Word_ReadPLCTagValue("Wagon6WCSInput");
                }



                //string[] WagonSensorWidthInputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("WagonSensorWidthInput");

                int index = 0;
                if (_Wagon6InputParameterLists.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_Wagon6InputParameterLists.Response);
                    foreach (var item in lstIOStatus)
                    {
                        IOStatusEntity _IOStatusEntity = new IOStatusEntity();
                        _IOStatusEntity.ID = index.ToString();
                        _IOStatusEntity.ParameterName = item.ParameterName;

                        //if (index == 1)
                        //{
                        //    try
                        //    {
                        //        if (lstIOStatus.Count == Wagon1Inputs.Length)
                        //        {
                        //            _IOStatusEntity.Value = ValueFromArray(WagonSensorWidthInputs, 5);
                        //        }
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagonInputs() WagonSensorWidthInput", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        //    }
                        //}
                        //else
                        //{
                            try
                            {
                                if (lstIOStatus.Count == Wagon1Inputs.Length-1)
                                {
                                    _IOStatusEntity.Value = ValueFromArray(Wagon1Inputs, index);
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagon6Inputs() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        //}

                        _Wagon6Inputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _Wagon6Inputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagon6Inputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _Wagon6Inputs;
            }
        }
        public static ObservableCollection<IOStatusEntity> GetWagon7Inputs()
        {
            ObservableCollection<IOStatusEntity> _Wagon7Inputs = new ObservableCollection<IOStatusEntity>();
            try
            {
                //string[] Wagon1Inputs = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" };
           
                string[] Wagon1Inputs;
                if (IndiSCADAGlobalLibrary.TagList.SettingScreenOpen == true)
                {
                    Wagon1Inputs = IndiSCADAGlobalLibrary.TagList.Wagon7WCSInput;
                }
                else
                {
                    Wagon1Inputs = DeviceCommunication.CommunicationWithPLC.Word_ReadPLCTagValue("Wagon6WCSInput");
                }



                //string[] WagonSensorWidthInputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("WagonSensorWidthInput");

                int index = 0;
                if (_Wagon7InputParameterLists.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_Wagon7InputParameterLists.Response);
                    foreach (var item in lstIOStatus)
                    {
                        IOStatusEntity _IOStatusEntity = new IOStatusEntity();
                        _IOStatusEntity.ID = index.ToString();
                        _IOStatusEntity.ParameterName = item.ParameterName;

                        //if (index == 1)
                        //{
                        //    try
                        //    {
                        //        if (lstIOStatus.Count == Wagon1Inputs.Length)
                        //        {
                        //            _IOStatusEntity.Value = ValueFromArray(WagonSensorWidthInputs, 5);
                        //        }
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagon7Inputs() WagonSensorWidthInput", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        //    }
                        //}
                        //else
                        //{
                            try
                            {
                                //if (lstIOStatus.Count == Wagon1Inputs.Length)
                                //{
                                    _IOStatusEntity.Value = ValueFromArray(Wagon1Inputs, 10);
                                //}
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagon7Inputs() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        //}

                        _Wagon7Inputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _Wagon7Inputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagon7Inputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _Wagon7Inputs;
            }
        }
        
        #region station selection 
        public static string GetStationSelectionValue()
        {
            string Value = "";
            try
            {
                string[] _result =   DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("StationSelection");//Actualcycle time
                Value = _result[0];
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetActualCycleTime()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return Value;
        }
        public static string GetStationEnterValue()
        {
            string Value = "";
            try
            {
                string[] _result =   DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("StationSelectionEnter");//Actualcycle time
                Value = _result[0];
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetActualCycleTime()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return Value;
        }
        public static string GetStationOperatedValue()
        {
            string Value = "";
            try
            {
                string[] _result =  DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("StationSelection");//Actualcycle time
                Value = _result[1];
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetActualCycleTime()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return Value;
        }
        #endregion
        #endregion
    }
}
