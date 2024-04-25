using System;
using IndiSCADAEntity.Entity;
using IndiSCADAGlobalLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADABusinessLogic
{
    public class SystemStatusLogic
    {
        #region"Declaration"
        //Read only once wagon input paramters
        static ServiceResponse<IList> _WagonInputParameterList = IndiSCADATranslation.SystemStatusTranslation.GetparameterNameFromDB("System Status");

        static ServiceResponse<IList> _FilterParameterList = IndiSCADATranslation.SystemStatusTranslation.GetparameterNameFromDB("Filter Status");
        static ServiceResponse<IList> _ScrubberParameterList = IndiSCADATranslation.SystemStatusTranslation.GetparameterNameFromDB("Scrubber Status");
        static ServiceResponse<IList> _RectifierParameterList = IndiSCADATranslation.SystemStatusTranslation.GetparameterNameFromDB("Rectifier Status");
        static ServiceResponse<IList> _TemperatureParameterList = IndiSCADATranslation.SystemStatusTranslation.GetparameterNameFromDB("Temperature Status");
        static ServiceResponse<IList> _pHParameterList = IndiSCADATranslation.SystemStatusTranslation.GetparameterNameFromDB("pH Status");
        static ServiceResponse<IList> _RectifierValueParameterList = IndiSCADATranslation.SystemStatusTranslation.GetparameterNameFromDB("RectifierValue Status");
        static ServiceResponse<IList> _DosingParameterList = IndiSCADATranslation.SystemStatusTranslation.GetparameterNameFromDB("Dosing Status");
        static ServiceResponse<IList> _OilSkimmerParameterList = IndiSCADATranslation.SystemStatusTranslation.GetparameterNameFromDB("OilSkimmer Status");
        static ServiceResponse<IList> _ChillerParameterList = IndiSCADATranslation.SystemStatusTranslation.GetparameterNameFromDB("Chiller Status");
        static ServiceResponse<IList> _BarrelMotorParameterList = IndiSCADATranslation.SystemStatusTranslation.GetparameterNameFromDB("BarrelMotor Status");


        static ServiceResponse<IList> _TempAddressList = IndiSCADATranslation.SystemStatusTranslation.GetTempAddressFromDB("Temperature Controller Alarms");
        static ServiceResponse<IList> _PHAddressList = IndiSCADATranslation.SystemStatusTranslation.GetPHAddressFromDB("ph Alarm");
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

        public static ObservableCollection<SystemStatusEntity> GetWagonInputs()
        {
            ObservableCollection<SystemStatusEntity> _WagonInputs = new ObservableCollection<SystemStatusEntity>();
            try
            {

                //Get W1Inputs
                string[] Wagon1Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("RunningStatus");
                //Get W2Inputs
                string[] Wagon2Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("TripStatus");
                int index = 0;
                if (_WagonInputParameterList.Response != null)
                {
                    IList<SystemStatusEntity> lstIOStatus = (IList<SystemStatusEntity>)(_WagonInputParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        SystemStatusEntity _IOStatusEntity = new SystemStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == Wagon1Inputs.Length && lstIOStatus.Count == Wagon2Inputs.Length)
                            {
                                _IOStatusEntity.RunningStatus = ValueFromArray(Wagon1Inputs, index);
                                _IOStatusEntity.TripStatus = ValueFromArray(Wagon2Inputs, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetWagonInputs() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _WagonInputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _WagonInputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetWagonInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _WagonInputs;
            }
        }

        public static ObservableCollection<SystemStatusEntity> GetFilterIntputs()
        {
            ObservableCollection<SystemStatusEntity> _FilterInputs = new ObservableCollection<SystemStatusEntity>();
            try
            {

                //Get W1Inputs
                string[] Wagon1Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("FilterRunningStatus");
                //Get W2Inputs
                string[] Wagon2Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("FilterTripStatus");
                int index = 0;
                if (_FilterParameterList.Response != null)
                {
                    IList<SystemStatusEntity> lstIOStatus = (IList<SystemStatusEntity>)(_FilterParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        SystemStatusEntity _IOStatusEntity = new SystemStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == Wagon1Inputs.Length && lstIOStatus.Count == Wagon2Inputs.Length)
                            {
                                _IOStatusEntity.RunningStatus = ValueFromArray(Wagon1Inputs, index);
                                _IOStatusEntity.TripStatus = ValueFromArray(Wagon2Inputs, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetFilterIntputs() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _FilterInputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _FilterInputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetFilterIntputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _FilterInputs;
            }
        }
        public static ObservableCollection<SystemStatusEntity> GetScrubberIntputs()
        {
            ObservableCollection<SystemStatusEntity> _ScrubberInputs = new ObservableCollection<SystemStatusEntity>();
            try
            {
                //Get W1Inputs
                string[] Wagon1Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("ScrubberRunningStatus");
                //Get W2Inputs
                string[] Wagon2Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("ScrubberTripStatus");
                int index = 0;
                if (_ScrubberParameterList.Response != null)
                {
                    IList<SystemStatusEntity> lstIOStatus = (IList<SystemStatusEntity>)(_ScrubberParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        SystemStatusEntity _IOStatusEntity = new SystemStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == Wagon1Inputs.Length && lstIOStatus.Count == Wagon2Inputs.Length)
                            {
                                _IOStatusEntity.RunningStatus = ValueFromArray(Wagon1Inputs, index);
                                _IOStatusEntity.TripStatus = ValueFromArray(Wagon2Inputs, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetScrubberIntputs() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _ScrubberInputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _ScrubberInputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetScrubberIntputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _ScrubberInputs;
            }
        }
        public static ObservableCollection<SystemStatusEntity> GetRectifierIntputs()
        {
            ObservableCollection<SystemStatusEntity> _RectifierInputs = new ObservableCollection<SystemStatusEntity>();
            try
            {

                //Get W1Inputs
                string[] Wagon1Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("RectifierRunningStatus");
                //Get W2Inputs
                string[] Wagon2Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("RectifierTripStatus");
                int index = 0;
                if (_RectifierParameterList.Response != null)
                {
                    IList<SystemStatusEntity> lstIOStatus = (IList<SystemStatusEntity>)(_RectifierParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        SystemStatusEntity _IOStatusEntity = new SystemStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == Wagon1Inputs.Length && lstIOStatus.Count == Wagon2Inputs.Length)
                            {
                                _IOStatusEntity.RunningStatus = ValueFromArray(Wagon1Inputs, index);
                                _IOStatusEntity.TripStatus = ValueFromArray(Wagon2Inputs, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetRectifierIntputs() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _RectifierInputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _RectifierInputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetRectifierIntputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _RectifierInputs;
            }
        }
        public static ObservableCollection<SystemStatusEntity> GetTemperatureIntputs()
        {
            ObservableCollection<SystemStatusEntity> _TemperatureInputs = new ObservableCollection<SystemStatusEntity>();
            try
            {

                //Get W1Inputs
                string[] Wagon1Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("TemperatureRunningStatus");

                #region  find running status using alarms logic
                string[] AddressValues = new String[Wagon1Inputs.Length*2];
                int i = 0;
                string[] TemperatureTripStatusInputs = new String[Wagon1Inputs.Length];
                string[] address = new string[1];
                //string[] TemperatureTripLHInputs = new String[11];
                if (_TempAddressList.Response != null)
                {                   
                    IList<SystemStatusEntity> TempAddress = (IList<SystemStatusEntity>)(_TempAddressList.Response);
                    foreach (var item in TempAddress)
                    {
                        String[] arrData = new String[] { item.ParameterName.ToString() };

                        ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetTemperatureIntputs() Read Address : "+ item.ParameterName.ToString(), DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                        string[] AlarmTagValue = DeviceCommunication.CommunicationWithPLC.Read_BlockAddress(arrData, 1);
                        AddressValues[i] = AlarmTagValue[0];
                        i++;
                    }
                }
                for (int j = 0; j < Wagon1Inputs.Length; j++)
                {
                    if (AddressValues[j] == "1") 
                    {
                        TemperatureTripStatusInputs[j] = "1";
                    }
                    else  if (AddressValues[j + 1] == "1")
                    {
                        TemperatureTripStatusInputs[j] = "2";
                    }
                    else
                    {
                        TemperatureTripStatusInputs[j] = "0";
                    }
                }
                #endregion

                ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetTemperatureIntputs() Readed TemperatureTripStatusInputs : " + TemperatureTripStatusInputs.ToString(), DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);


                //Get W2Inputs
                //string[] Wagon2Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("TemperatureTripStatus");

                int index = 0;
                if (_TemperatureParameterList.Response != null)
                {
                    IList<SystemStatusEntity> lstIOStatus = (IList<SystemStatusEntity>)(_TemperatureParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        SystemStatusEntity _IOStatusEntity = new SystemStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == Wagon1Inputs.Length && lstIOStatus.Count == TemperatureTripStatusInputs.Length)
                            {
                                _IOStatusEntity.RunningStatus = ValueFromArray(Wagon1Inputs, index);
                                _IOStatusEntity.TripStatus = ValueFromArray(TemperatureTripStatusInputs, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetTemperatureIntputs() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _TemperatureInputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _TemperatureInputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetTemperatureIntputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _TemperatureInputs;
            }
        }
        public static ObservableCollection<SystemStatusEntity> GetpHIntputs()
        {
            ObservableCollection<SystemStatusEntity> _pHInputs = new ObservableCollection<SystemStatusEntity>();
            try
            {

                //Get W1Inputs
                string[] Wagon1Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("pHRunningStatus");
                //Get W2Inputs
                //string[] Wagon2Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("pHTripStatus");

                #region  find running status using alarms logic
                string[] AddressValues = new String[Wagon1Inputs.Length * 2];
                int i = 0;
                string[] PHTripStatusInputs = new String[Wagon1Inputs.Length];
                string[] address = new string[1];
                //string[] TemperatureTripLHInputs = new String[Wagon1Inputs.Length];
                if (_PHAddressList.Response != null)
                {
                    IList<SystemStatusEntity> PHAddress = (IList<SystemStatusEntity>)(_PHAddressList.Response);
                    foreach (var item in PHAddress)
                    {
                        String[] arrData = new String[] { item.ParameterName.ToString() };
                        string[] AlarmTagValue = DeviceCommunication.CommunicationWithPLC.Read_BlockAddress(arrData, 1);
                        AddressValues[i] = AlarmTagValue[0];
                        i++;
                    }
                }
                for (int j = 0; j < Wagon1Inputs.Length; j++)
                {
                    if (AddressValues[j] == "1") //low
                    {
                        PHTripStatusInputs[j] = "1";
                        //TemperatureTripLHInputs[j] = "Low";
                    }
                    else if (AddressValues[j + 1] == "1") //high
                    {
                        PHTripStatusInputs[j] = "2";
                        //TemperatureTripLHInputs[j] = "High";
                    }
                    else //normal
                    {
                        PHTripStatusInputs[j] = "0";
                        //TemperatureTripLHInputs[j] = "";
                    }
                }
                #endregion


                int index = 0;
                if (_pHParameterList.Response != null)
                {
                    IList<SystemStatusEntity> lstIOStatus = (IList<SystemStatusEntity>)(_pHParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        SystemStatusEntity _IOStatusEntity = new SystemStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == Wagon1Inputs.Length && lstIOStatus.Count == PHTripStatusInputs.Length)
                            {
                                _IOStatusEntity.RunningStatus = ValueFromArray(Wagon1Inputs, index);
                                _IOStatusEntity.TripStatus = ValueFromArray(PHTripStatusInputs, index);
                                //_IOStatusEntity.Value = ValueFromArray(TemperatureTripLHInputs, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetpHIntputs() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _pHInputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _pHInputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetpHIntputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _pHInputs;
            }
        }
        public static ObservableCollection<SystemStatusEntity> GetRectifierHighLowInputs()
        {
            ObservableCollection<SystemStatusEntity> _RectifierValuesInputs = new ObservableCollection<SystemStatusEntity>();
            try
            {

                //Get W1Inputs
                string[] Wagon1Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("RectifierValuesRunningStatus");
                //Get W2Inputs
                string[] Wagon2Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("RectifierValuesTripStatus");
                int index = 0;
                if (_RectifierValueParameterList.Response != null)
                {
                    IList<SystemStatusEntity> lstIOStatus = (IList<SystemStatusEntity>)(_RectifierValueParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        SystemStatusEntity _IOStatusEntity = new SystemStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == Wagon1Inputs.Length && lstIOStatus.Count == Wagon2Inputs.Length)
                            {
                                _IOStatusEntity.RunningStatus = ValueFromArray(Wagon1Inputs, index);
                                _IOStatusEntity.TripStatus = ValueFromArray(Wagon2Inputs, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetRectifierHighLowInputs() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _RectifierValuesInputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _RectifierValuesInputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetRectifierHighLowInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _RectifierValuesInputs;
            }
        }
        public static ObservableCollection<SystemStatusEntity> GetDosingIntputs()
        {
            ObservableCollection<SystemStatusEntity> _DosingInputs = new ObservableCollection<SystemStatusEntity>();
            try
            {

                //Get W1Inputs
                string[] Wagon1Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("DosingRunningStatus");
                //Get W2Inputs
                //string[] Wagon2Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("DosingTripStatus");
                int index = 0;
                if (_DosingParameterList.Response != null)
                {
                    IList<SystemStatusEntity> lstIOStatus = (IList<SystemStatusEntity>)(_DosingParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        SystemStatusEntity _IOStatusEntity = new SystemStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == Wagon1Inputs.Length)// && lstIOStatus.Count == Wagon2Inputs.Length)
                            {
                                _IOStatusEntity.RunningStatus = ValueFromArray(Wagon1Inputs, index);
                                //_IOStatusEntity.TripStatus = ValueFromArray(Wagon2Inputs, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetDosingIntputs() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _DosingInputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _DosingInputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetDosingIntputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _DosingInputs;
            }
        }
        public static ObservableCollection<SystemStatusEntity> GetOilSkimmerIntputs()
        {
            ObservableCollection<SystemStatusEntity> _OilSkimmerInputs = new ObservableCollection<SystemStatusEntity>();
            try
            {

                //Get W1Inputs
                string[] Wagon1Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("OilSkimmerRunningStatus");
                //Get W2Inputs
                string[] Wagon2Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("OilSkimmerTripStatus");
                int index = 0;
                if (_OilSkimmerParameterList.Response != null)
                {
                    IList<SystemStatusEntity> lstIOStatus = (IList<SystemStatusEntity>)(_OilSkimmerParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        SystemStatusEntity _IOStatusEntity = new SystemStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == Wagon1Inputs.Length && lstIOStatus.Count == Wagon2Inputs.Length)
                            {
                                _IOStatusEntity.RunningStatus = ValueFromArray(Wagon1Inputs, index);
                                _IOStatusEntity.TripStatus = ValueFromArray(Wagon2Inputs, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetOilSkimmerIntputs() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _OilSkimmerInputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _OilSkimmerInputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetOilSkimmerIntputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _OilSkimmerInputs;
            }
        }
        public static ObservableCollection<SystemStatusEntity> GetChillerIntputs()
        {
            ObservableCollection<SystemStatusEntity> _ChillerInputs = new ObservableCollection<SystemStatusEntity>();
            try
            {

                //Get W1Inputs
                string[] Wagon1Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("ChillerRunningStatus");
                //Get W2Inputs
                string[] Wagon2Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("ChillerTripStatus");
                int index = 0;
                if (_ChillerParameterList.Response != null)
                {
                    IList<SystemStatusEntity> lstIOStatus = (IList<SystemStatusEntity>)(_ChillerParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        SystemStatusEntity _IOStatusEntity = new SystemStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == Wagon1Inputs.Length && lstIOStatus.Count == Wagon2Inputs.Length)
                            {
                                _IOStatusEntity.RunningStatus = ValueFromArray(Wagon1Inputs, index);
                                _IOStatusEntity.TripStatus = ValueFromArray(Wagon2Inputs, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetChillerIntputs() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _ChillerInputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _ChillerInputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetChillerIntputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _ChillerInputs;
            }
        }
        public static ObservableCollection<SystemStatusEntity> GetBarrelRotationMotorIntputs()
        {
            ObservableCollection<SystemStatusEntity> _BarrelMotorInputs = new ObservableCollection<SystemStatusEntity>();
            try
            {

                //Get W1Inputs
                string[] Wagon1Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("BarrelMotorRunningStatus");
                //Get W2Inputs
                string[] Wagon2Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("BarrelMotorTripStatus");
                int index = 0;
                if (_BarrelMotorParameterList.Response != null)
                {
                    IList<SystemStatusEntity> lstIOStatus = (IList<SystemStatusEntity>)(_BarrelMotorParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        SystemStatusEntity _IOStatusEntity = new SystemStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == Wagon1Inputs.Length && lstIOStatus.Count == Wagon2Inputs.Length)
                            {
                                _IOStatusEntity.RunningStatus = ValueFromArray(Wagon1Inputs, index);
                                _IOStatusEntity.TripStatus = ValueFromArray(Wagon2Inputs, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetBarrelRotationMotorIntputs() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _BarrelMotorInputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _BarrelMotorInputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SystemStatusLogic GetBarrelRotationMotorIntputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _BarrelMotorInputs;
            }
        }

        #endregion
    }
}
