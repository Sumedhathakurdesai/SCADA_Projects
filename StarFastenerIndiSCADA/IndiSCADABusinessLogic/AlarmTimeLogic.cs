using IndiSCADAEntity.Entity;
using IndiSCADAGlobalLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADABusinessLogic
{
    public static class AlarmTimeLogic
    {
        #region"Declaration"
        //Read only once _Wagon AlarmTime Parameter
        static ServiceResponse<IList> _WagonAlarmTimeParameter = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Wagon Alarm Time");
        //Read only once _Dryer AlarmTime Parameter
        static ServiceResponse<IList> _DryerAlarmTimeParameter = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Dryer Alarm Time");
        static ServiceResponse<IList> _WagonBasketAlarmTimeParameter = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Wagon Basket Alarm Time");
        static ServiceResponse<IList> _CTAlarmTimeParameter = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Cross Trolley Alarm Time");

        static ServiceResponse<IList> _WagonTopDripParameter = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("WagonTopDrip");
        static ServiceResponse<IList> _StationTopDripParameter = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("StationTopDrip");

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
        public static void WriteValueToPLC(string TaskName, int index, string value)
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
                        ErrorLogger.LogError.ErrorLog("SettingLogic WriteValueToPLC() while reading ReadTagName", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("AlarmTimeLogic ReadPLCTagValue()" + TaskName, DateTime.Now.ToString(), "IsPLCConnected is false", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmTimeLogic ReadPLCTagValue() " + TaskName, DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }

        public static ObservableCollection<AlarmTimeEntity> GetWagonAlarmTime()
        {
            ObservableCollection<AlarmTimeEntity> _WagonAlarmTime = new ObservableCollection<AlarmTimeEntity>();
            try
            {
                //string[] Wagon1AlarmTime = {"1","2","3","4", "5", "6", "7", "8" };
                //string[] Wagon2AlarmTime = { "1", "2", "3", "4", "5", "6", "7", "8" };
                //string[] Wagon3AlarmTime = { "1", "2", "3", "4", "5", "6", "7", "8" };
                //string[] Wagon4AlarmTime = { "1", "2", "3", "4", "5", "6", "7", "8" };
                //string[] Wagon5AlarmTime = { "1", "2", "3", "4", "5", "6", "7", "8" };
                //string[] Wagon6AlarmTime = { "1", "2", "3", "4", "5", "6", "7", "8" };
                //string[] Wagon7AlarmTime = { "1", "2", "3", "4", "5", "6", "7", "8" };


                //Get Wagon1AlarmTime
                string[] Wagon1AlarmTime = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon1AlarmTime");
                //Get Wagon2AlarmTime
                string[] Wagon2AlarmTime = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon2AlarmTime");
                //Get Wagon2AlarmTime
                string[] Wagon3AlarmTime = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon3AlarmTime");
                //Get Wagon2AlarmTime
                string[] Wagon4AlarmTime = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon4AlarmTime");
                //Get Wagon2AlarmTime
                string[] Wagon5AlarmTime = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon5AlarmTime");
                string[] Wagon6AlarmTime = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon6AlarmTime");
                string[] Wagon7AlarmTime = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon7AlarmTime");


                int index = 0;
                if (_WagonAlarmTimeParameter.Response != null)
                {
                    IList<IOStatusEntity> lstWagonAlarmTime = (IList<IOStatusEntity>)(_WagonAlarmTimeParameter.Response);
                    foreach (var item in lstWagonAlarmTime)
                    {
                        AlarmTimeEntity _AlarmTimeEntity = new AlarmTimeEntity();
                        _AlarmTimeEntity.ParameterName = item.ParameterName;
                        _AlarmTimeEntity.AlarmTimeID = index.ToString();
                        try
                        {
                            if (lstWagonAlarmTime.Count == Wagon1AlarmTime.Length && lstWagonAlarmTime.Count == Wagon2AlarmTime.Length)
                            {
                                if (index < 4)
                                {
                                    _AlarmTimeEntity.W1 = ValueFromArray(Wagon1AlarmTime, index);
                                    _AlarmTimeEntity.W2 = ValueFromArray(Wagon2AlarmTime, index);
                                    _AlarmTimeEntity.W3 = ValueFromArray(Wagon3AlarmTime, index);
                                    _AlarmTimeEntity.W4 = ValueFromArray(Wagon4AlarmTime, index);
                                    _AlarmTimeEntity.W5 = ValueFromArray(Wagon5AlarmTime, index);
                                    _AlarmTimeEntity.W6 = ValueFromArray(Wagon6AlarmTime, index);
                                    _AlarmTimeEntity.W7 = ValueFromArray(Wagon7AlarmTime, index); 
                                }
                                else
                                {
                                    _AlarmTimeEntity.W5 = ValueFromArray(Wagon5AlarmTime, index);
                                    _AlarmTimeEntity.W6 = ValueFromArray(Wagon6AlarmTime, index);
                                    _AlarmTimeEntity.W7 = ValueFromArray(Wagon7AlarmTime, index); 
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("AlarmTimeLogic WagonAlarmTime() lstWagonAlarmTime", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _WagonAlarmTime.Add(_AlarmTimeEntity);
                        index = index + 1;
                    }
                }
                return _WagonAlarmTime;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic WagonAlarmTime()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _WagonAlarmTime;
            }
        }

        public static ObservableCollection<AlarmTimeEntity> GetWagonBasketAlarmTime()
        {
            ObservableCollection<AlarmTimeEntity> _WagonAlarmTime = new ObservableCollection<AlarmTimeEntity>();
            try
            {
                //Get Wagon3AlarmTime
                string[] Wagon6AlarmTime = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon6BasketAlarmTime");
                string[] Wagon7AlarmTime = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon7BasketAlarmTime");
                string[] Wagon8AlarmTime = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon8BasketAlarmTime");
                //Get Wagon4AlarmTime
                //string[] Wagon5AlarmTime = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("W5AlarmTime");
                int index = 0;
                if (_WagonBasketAlarmTimeParameter.Response != null)
                {
                    IList<IOStatusEntity> lstWagonAlarmTime = (IList<IOStatusEntity>)(_WagonBasketAlarmTimeParameter.Response);
                    foreach (var item in lstWagonAlarmTime)
                    {
                        AlarmTimeEntity _AlarmTimeEntity = new AlarmTimeEntity();
                        _AlarmTimeEntity.ParameterName = item.ParameterName;
                        _AlarmTimeEntity.AlarmTimeID = index.ToString();
                        try
                        {
                            if (lstWagonAlarmTime.Count == Wagon6AlarmTime.Length && lstWagonAlarmTime.Count == Wagon7AlarmTime.Length)
                            {
                                _AlarmTimeEntity.W6 = ValueFromArray(Wagon6AlarmTime, index);
                                _AlarmTimeEntity.W7 = ValueFromArray(Wagon7AlarmTime, index);
                                _AlarmTimeEntity.W8 = ValueFromArray(Wagon8AlarmTime, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagonBasketAlarmTime() lstWagonAlarmTime", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _WagonAlarmTime.Add(_AlarmTimeEntity);
                        index = index + 1;
                    }
                }
                return _WagonAlarmTime;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagonBasketAlarmTime()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _WagonAlarmTime;
            }
        }

        public static ObservableCollection<AlarmTimeEntity> GetDryerAlarmTime()
        {
            ObservableCollection<AlarmTimeEntity> _DryerAlarmTime = new ObservableCollection<AlarmTimeEntity>();
            try
            {
                //string[] Dryer1AlarmTime = { "1", "2"  };
                //string[] Dryer2AlarmTime = { "1", "2"  };
                //string[] Dryer3AlarmTime = { "1", "2" };


                string[] Dryer1AlarmTime = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Dryer1AlarmTime");
                string[] Dryer2AlarmTime = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Dryer2AlarmTime");
                string[] Dryer3AlarmTime = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Dryer3AlarmTime");

                int index = 0;
                if (_DryerAlarmTimeParameter.Response != null)
                {
                    IList<IOStatusEntity> lstDryerAlarmTime = (IList<IOStatusEntity>)(_DryerAlarmTimeParameter.Response);
                    foreach (var item in lstDryerAlarmTime)
                    {
                        AlarmTimeEntity _AlarmTimeEntity = new AlarmTimeEntity();
                        _AlarmTimeEntity.ParameterName = item.ParameterName;
                        _AlarmTimeEntity.AlarmTimeID = index.ToString();
                        try
                        {
                            if (lstDryerAlarmTime.Count == Dryer1AlarmTime.Length)
                            {
                                _AlarmTimeEntity.D1 = ValueFromArray(Dryer1AlarmTime, index);
                                _AlarmTimeEntity.D2 = ValueFromArray(Dryer2AlarmTime, index);
                                _AlarmTimeEntity.D3 = ValueFromArray(Dryer3AlarmTime, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetDryerAlarmTime() lstDryerAlarmTime", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _DryerAlarmTime.Add(_AlarmTimeEntity);
                        index = index + 1;
                    }
                }
                return _DryerAlarmTime;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetDryerAlarmTime()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _DryerAlarmTime;
            }
        }

        public static ObservableCollection<AlarmTimeEntity> GetCTAlarmTime()
        {
            ObservableCollection<AlarmTimeEntity> _CTAlarmTime = new ObservableCollection<AlarmTimeEntity>();
            try
            {
                //string[] CT1AlarmTime = { "1", "2", "3", "4" };

                //Get CTAlarmTime
                string[] CT1AlarmTime = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("CrossTrolley1AlarmTime");


                int index = 0;
                if (_CTAlarmTimeParameter.Response != null)
                {
                    IList<IOStatusEntity> lstDryerAlarmTime = (IList<IOStatusEntity>)(_CTAlarmTimeParameter.Response);
                    foreach (var item in lstDryerAlarmTime)
                    {
                        AlarmTimeEntity _AlarmTimeEntity = new AlarmTimeEntity();
                        _AlarmTimeEntity.ParameterName = item.ParameterName;
                        _AlarmTimeEntity.AlarmTimeID = index.ToString();
                        try
                        {
                            if (lstDryerAlarmTime.Count == CT1AlarmTime.Length)
                            {
                                _AlarmTimeEntity.CT1 = ValueFromArray(CT1AlarmTime, index);
                                //_AlarmTimeEntity.CT2 = ValueFromArray(CT2AlarmTime, index);
                                //_AlarmTimeEntity.CT3 = ValueFromArray(CT3AlarmTime, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetCTAlarmTime() lstDryerAlarmTime", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _CTAlarmTime.Add(_AlarmTimeEntity);
                        index = index + 1;
                    }
                }
                return _CTAlarmTime;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetCTAlarmTime()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _CTAlarmTime;
            }
        }


        public static ObservableCollection<AlarmTimeEntity> GetWagonTopDrip()
        {
            ObservableCollection<AlarmTimeEntity> _CTAlarmTime = new ObservableCollection<AlarmTimeEntity>();
            try
            {
                //string[] CT1AlarmTime = { "1", "2", "3", "4", "5", "6" };

                //Get CTAlarmTime
                string[] CT1AlarmTime = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("WagonTopDrip");


                int index = 0;
                if (_WagonTopDripParameter.Response != null)
                {
                    IList<IOStatusEntity> lstDryerAlarmTime = (IList<IOStatusEntity>)(_WagonTopDripParameter.Response);
                    foreach (var item in lstDryerAlarmTime)
                    {
                        AlarmTimeEntity _AlarmTimeEntity = new AlarmTimeEntity();
                        _AlarmTimeEntity.ParameterName = item.ParameterName;
                        _AlarmTimeEntity.AlarmTimeID = index.ToString();
                        try
                        {
                            if (lstDryerAlarmTime.Count == CT1AlarmTime.Length)
                            {
                                _AlarmTimeEntity.Value = ValueFromArray(CT1AlarmTime, index); 
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagonTopDrip() lst ", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _CTAlarmTime.Add(_AlarmTimeEntity);
                        index = index + 1;
                    }
                }
                return _CTAlarmTime;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagonTopDrip()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _CTAlarmTime;
            }
        }
        public static ObservableCollection<AlarmTimeEntity> GetStationTopDrip()
        {
            ObservableCollection<AlarmTimeEntity> _CTAlarmTime = new ObservableCollection<AlarmTimeEntity>();
            try
            {
                //string[] CT1AlarmTime = { "1", "2", "3", "4","5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" };

                //Get CTAlarmTime
                string[] CT1AlarmTime = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("StationTopDrip");


                int index = 0;
                if (_StationTopDripParameter.Response != null)
                {
                    IList<IOStatusEntity> lstDryerAlarmTime = (IList<IOStatusEntity>)(_StationTopDripParameter.Response);
                    foreach (var item in lstDryerAlarmTime)
                    {
                        AlarmTimeEntity _AlarmTimeEntity = new AlarmTimeEntity();
                        _AlarmTimeEntity.ParameterName = item.ParameterName;
                        _AlarmTimeEntity.AlarmTimeID = index.ToString();
                        try
                        {
                            if (lstDryerAlarmTime.Count == CT1AlarmTime.Length)
                            {
                                _AlarmTimeEntity.Value = ValueFromArray(CT1AlarmTime, index); 
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetStationTopDrip() lst ", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _CTAlarmTime.Add(_AlarmTimeEntity);
                        index = index + 1;
                    }
                }
                return _CTAlarmTime;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetStationTopDrip()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _CTAlarmTime;
            }
        }

        
        #endregion
    }
}
