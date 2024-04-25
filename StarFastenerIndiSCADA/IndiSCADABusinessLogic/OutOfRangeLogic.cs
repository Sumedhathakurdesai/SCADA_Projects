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
    public static class OutOfRangeLogic
    {
                #region"Declaration"
        //Read only once Rectifier Station Names
        static ServiceResponse<IList> _RectifierStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Rectifier");
        //Read only once Temperature Station Names
        static ServiceResponse<IList> _TemperatureStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("OR");
        //Read only once pH Station Names
        static ServiceResponse<IList> _pHStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("pH");
        //Read only once Top Spray Station Names
        static ServiceResponse<IList> _TopSprayStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("TopSpray");
        //Read only once Top Spray Station Names
        static ServiceResponse<IList> _UtilityStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Utility");
        //Read only once Mechanical Agitation StationName 
        static ServiceResponse<IList> _MechanicalAgitationStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("MechanicalAgitationStationName");
        //Read only once Filter Pump StationName 
        static ServiceResponse<IList> _FilterPumpStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("FilterPumpStationName");
        //Read only once Barrel Motor StationName 
        static ServiceResponse<IList> _BarrelMotorStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Barrel Motor");
        //Read only once Dosing pump
        static ServiceResponse<IList> _DosingPumpStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Dosing");

        static ServiceResponse<IList> _TankBypassStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("TankBypass");

        static ServiceResponse<IList> _TrayBypassStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("TrayBypass");




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
        public static void ONOFFButton(string TaskName, int index)
        {
            try
            {
                //Get AutoManual from PLC
                string[] ValueToRead = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue(TaskName);
                string TagName = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName(TaskName, index);
                ErrorLogger.LogError.ErrorLog("ORLogic ONOFFButton(TagName)", DateTime.Now.ToString(), TagName, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                ErrorLogger.LogError.ErrorLog("ORLogic ONOFFButton(Value)", DateTime.Now.ToString(), ValueToRead[index].ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                if (TagName != null)
                {
                    if (TagName.Length > 0)
                    {
                        if (ValueToRead[index] == "1")
                        {
                            int LT1 = 0;
                            DeviceCommunication.CommunicationWithPLC.WriteValues(TagName, LT1.ToString());
                        }
                    }
                    if (TagName.Length > 0)
                    {
                        if (ValueToRead[index] == "0")
                        {
                            int LT1 = 1;
                            DeviceCommunication.CommunicationWithPLC.WriteValues(TagName, LT1.ToString());
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ORLogic ONOFFButton()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }

        }
        public static void WriteValueToPLC(string TaskName, int index, string value)
        {
            try
            {
                string TagName = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName(TaskName, index);
                DeviceCommunication.CommunicationWithPLC.WriteValues(TagName, value);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ORLogic WriteValueToPLC()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }

        }
        public static void WriteValueToPLCReal(string TaskName, int index, string value)
        {
            try
            {
                string TagName = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName(TaskName, index);
                DeviceCommunication.CommunicationWithPLC.WriteValues_Real(TagName, value);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ORLogic WriteValueToPLC()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }

        }
        public static void  InsertORDipTimeInputs(ObservableCollection<OutOfRangeSettingsDipTime> _InsertORDipTimeData,string ProgramNo)
        {
            try
            {
                foreach (var item in _InsertORDipTimeData)
                {
                    item.ProgramNo = ProgramNo;
                    IndiSCADADataAccess.DataAccessInsert.InsertORDipTimeData(item);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ORLogic InsertORDipTimeInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }

        }
        public static void UpdateORDipTimeInputs(OutOfRangeSettingsDipTime _UpdateORDipTimeInputs)
        {
            try
            {
                //foreach (var item in _UpdateORDipTimeInputs)
                {
                    IndiSCADADataAccess.DataAccessUpdate.UpdateORDipTimeData(_UpdateORDipTimeInputs);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ORLogic UpdateORDipTimeInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }

        }
        public static void DeleteORDipTimeInputs(OutOfRangeSettingsDipTime _DeleteORDipTimeInputs)
        {
            try
            {
                IndiSCADADataAccess.DataAccessDelete.DeleteORDipTimeData(_DeleteORDipTimeInputs);
                
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ORLogic DeleteORDipTimeInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }

        }
        public static ObservableCollection<OutOfRangeSettingsDipTime> GetORDipTimeInputs()
        {
            ObservableCollection<OutOfRangeSettingsDipTime> __ORDipTimeData = new ObservableCollection<OutOfRangeSettingsDipTime>();
            try
            {

                ServiceResponse<DataTable> _StationList = IndiSCADADataAccess.DataAccessSelect.ReturnDataTable("StationMasterTankDetails", "SP_StationMaster");
                int index = 0;
                if (_StationList.Response != null)
                {
                    DataTable _DTStationList = _StationList.Response;
                    foreach (DataRow row in _DTStationList.Rows)
                    {
                        OutOfRangeSettingsDipTime _DetailsEntity = new OutOfRangeSettingsDipTime();
                        _DetailsEntity.DipTimeID = index;
                        _DetailsEntity.ParameterName = row["StationName"].ToString();//station name
                        _DetailsEntity.StationNO = row["StationNumber"].ToString();//station Number
                        _DetailsEntity.LowBypass ="0";//Low Bypass
                        _DetailsEntity.HighBypass = "0";//High Bypass
                        _DetailsEntity.HighSPDipTime = "0";//High Time
                        _DetailsEntity.LowSPDipTime = "0";//Low time
                        __ORDipTimeData.Add(_DetailsEntity);
                        index = index + 1;
                    }
                }
                return __ORDipTimeData;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ORLogic GetORDipTimeInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return __ORDipTimeData;
            }
        }
        public static ObservableCollection<OutOfRangeSettingsDipTime> GetORDipTimeInputsProgramName()
        {
            ObservableCollection<OutOfRangeSettingsDipTime> __ORDipTimeData = new ObservableCollection<OutOfRangeSettingsDipTime>();
            try
            {

                ServiceResponse<IList> _TranslationOP = IndiSCADATranslation.ORDipTimeTranslation.GetProgramName();
                if (_TranslationOP.Response != null)
                {
                    if (_TranslationOP.Status == ResponseType.S)
                    {
                        IList<OutOfRangeSettingsDipTime> _conversionList = (IList<OutOfRangeSettingsDipTime>)(_TranslationOP.Response);
                        __ORDipTimeData = new ObservableCollection<OutOfRangeSettingsDipTime>(_conversionList);
                    }
                }
                return __ORDipTimeData;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ORLogic GetORDipTimeInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return __ORDipTimeData;
            }
        }

        public static ObservableCollection<OutOfRangeSettingsDipTime> GetORDipTimeInputsForedit(string ProgramNo)
        {
            ObservableCollection<OutOfRangeSettingsDipTime> __ORDipTimeData = new ObservableCollection<OutOfRangeSettingsDipTime>();
            try
            {

                ServiceResponse<IList> _TranslationOP = IndiSCADATranslation.ORDipTimeTranslation.GetDipTimeFromProgramName(ProgramNo);
                if (_TranslationOP.Response != null)
                {
                    if (_TranslationOP.Status == ResponseType.S)
                    {
                        IList<OutOfRangeSettingsDipTime> _conversionList = (IList<OutOfRangeSettingsDipTime>)(_TranslationOP.Response);
                        __ORDipTimeData = new ObservableCollection<OutOfRangeSettingsDipTime>(_conversionList);
                    }
                }
                return __ORDipTimeData;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ORLogic GetORDipTimeInputsForedit()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return __ORDipTimeData;
            }
        }
        public static ObservableCollection<OutOfRangesSettingsEntityTemp> GetORTemperatureInputs()
        {
            ObservableCollection<OutOfRangesSettingsEntityTemp> __TemperatureData = new ObservableCollection<OutOfRangesSettingsEntityTemp>();
            try
            {
                

                //Get TempHighSP from PLC
                string[] TempHighSP = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("ORTEMPERATUREHighSetPoint");
                //Get TempLowSP from PLC
                string[] TempLowSP = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("ORTEMPERATURELowSetPoint");
                //Get TempSP from PLC
                string[] TempSP = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("TempControllerTemperatureControllerSetPoint");
                //Get Actual TempLowBypass from PLC
                string[] TempLowBypass = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("ORTEMPERATURELowBypass");
                //Get Actual TempHighBypass from PLC
                string[] TempHighBypass = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("ORTEMPERATUREHighBypass");
                //Get Actual Temp Timer from PLC
                string[] TempDelay = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("ORTEMPERATUREDelay");
                //Get avg Temp Timer from PLC
                string[] Tempavg = DeviceCommunication.CommunicationWithPLC.Word_ReadPLCTagValue("ORTEMPERATUREAvg");
                //
                int index = 0;
                if (_TemperatureStationName.Response != null)
                {
                    IList<IOStatusEntity> lstTemperature = (IList<IOStatusEntity>)(_TemperatureStationName.Response);
                    foreach (var item in lstTemperature)
                    {
                        OutOfRangesSettingsEntityTemp _TemperatureStationName = new OutOfRangesSettingsEntityTemp();
                        _TemperatureStationName.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstTemperature.Count == TempHighSP.Length && lstTemperature.Count == TempLowSP.Length)
                            {
                                _TemperatureStationName.TempID = index;
                                _TemperatureStationName.HighSPTemp = ValueFromArray(TempHighSP, index);
                                _TemperatureStationName.LowSPTemp = ValueFromArray(TempLowSP, index);
                                _TemperatureStationName.SetPointTemp = ValueFromArray(TempSP, index);
                                _TemperatureStationName.LowBypassTemp = ValueFromArray(TempLowBypass, index);
                                _TemperatureStationName.HighBypassTemp = ValueFromArray(TempHighBypass, index);
                                _TemperatureStationName.DelayTemp = ValueFromArray(TempDelay, index);
                                _TemperatureStationName.AvgTemp = ValueFromArray(Tempavg, index);
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
                ErrorLogger.LogError.ErrorLog("ORLogic GetORTemperatureInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return __TemperatureData;
            }
        }
        public static string[] DivideBy(string[] SourceValue, int devidefactor)
        {
            try
            {
                string[] value = new string[12];
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
        public static ObservableCollection<OutOfRangesSettingsEntitypH> GetORpHInputs()
        {
            ObservableCollection<OutOfRangesSettingsEntitypH> _pHData = new ObservableCollection<OutOfRangesSettingsEntitypH>();
            try
            {
                //Get pHHighSP from PLC
                string[] pHHighSP = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("ORpHHighSetPoint");
                //Get pHLowSP from PLC
                string[] pHLowSP = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("ORpHLowSetPoint");
                //Get pHLowBypass pH from PLC
                string[] pHLowBypass = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("ORpHLowBypass");
                //Get pHLowBypass pH from PLC
                string[] pHHighBypass = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("ORpHHighBypass");
                //Get pHdelay pH from PLC
                string[] pHdelay = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("ORpHDelay");
                //Get AVG pH from PLC
                string[] pHavg = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("ORpHAvg");

                int index = 0;
                if (_pHStationName.Response != null)
                {
                    IList<IOStatusEntity> lstpH = (IList<IOStatusEntity>)(_pHStationName.Response);
                    foreach (var item in lstpH)
                    {
                        OutOfRangesSettingsEntitypH _pHStationName = new OutOfRangesSettingsEntitypH();
                        _pHStationName.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstpH.Count == pHHighSP.Length && lstpH.Count == pHLowSP.Length)
                            {
                                _pHStationName.pHID = index;
                                _pHStationName.HighSPpH = ValueFromArray(pHHighSP, index);
                                _pHStationName.LowSPpH = ValueFromArray(pHLowSP, index);
                                _pHStationName.LowBypasspH = ValueFromArray(pHLowBypass, index);
                                _pHStationName.HighBypasspH = ValueFromArray(pHHighBypass, index);
                                _pHStationName.DelaypH = ValueFromArray(pHdelay, index);
                                _pHStationName.AvgpH = ValueFromArray(pHavg, index);
                            }
                        }
                        catch { }
                        _pHData.Add(_pHStationName);
                        index = index + 1;
                    }
                }
                return _pHData;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ORLogic GetORpHInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _pHData;
            }
        }
       
        #endregion
    }
}
