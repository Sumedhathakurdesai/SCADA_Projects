using IndiSCADAEntity.Entity;
using IndiSCADAGlobalLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADABusinessLogic
{
    public static class AlarmEventLogic
    {
        #region Public/private methods


        public static void VerifyDateTimeWithPLC()
        {
            try
            {
                DateTime CurrentDateTime = DateTime.Now;
                string day = "", month = "", year = "", hours = "", minutes = "", seconds = "", dayofweek = "";
                TimeSpan time1 = new TimeSpan(23, 58, 0); //10 o'clock
                TimeSpan time2 = new TimeSpan(0, 1, 0); //12 o'clock
                TimeSpan now = DateTime.Now.TimeOfDay;
                string[] arrControlBits = new string[2];
                string[] arrAddress = null;

                try
                {
                    if (arrAddress == null)
                    {
                        arrAddress = new System.String[7];
                        arrControlBits = new System.String[2];
                        for (int i = 0; i < 7; i++)
                        {
                            arrAddress[i] = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName("DateTimeSynch", i);
                        }

                        for (int i = 0; i < 2; i++)
                        {
                            arrControlBits[i] = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName("DateTimeCorrection", i);
                        }
                    }

                }
                catch (Exception ex) { }


                if ((now < time1) && (now > time2))
                {
                    #region "Assign datetime values"
                    day = CurrentDateTime.Day.ToString();
                    month = CurrentDateTime.Month.ToString();
                    year = CurrentDateTime.Year.ToString();
                    hours = CurrentDateTime.Hour.ToString();
                    minutes = CurrentDateTime.Minute.ToString();
                    seconds = CurrentDateTime.Second.ToString();
                    dayofweek = ((int)CurrentDateTime.DayOfWeek).ToString();
                    #endregion

                    // Write the values in PLC
                    #region "Write"
                    try
                    {
                        short[] arrDateTimeValue = new short[7];            //Data for datetime
                        arrDateTimeValue[0] = (Convert.ToInt16(year));
                        arrDateTimeValue[1] = (Convert.ToInt16(month));
                        arrDateTimeValue[2] = (Convert.ToInt16(day));
                        arrDateTimeValue[3] = (Convert.ToInt16(hours));
                        arrDateTimeValue[4] = (Convert.ToInt16(minutes));
                        arrDateTimeValue[5] = (Convert.ToInt16(seconds));
                        arrDateTimeValue[6] = (Convert.ToInt16(dayofweek));

                        // objLogging.LogError(DateTime.Now, "PLCDateTimeSynch() Write Values 1 -", "" , 0);
                        //arrDateTimeValue[7] = (Convert.ToInt16(1)); // Confirmation bit                        

                        short[] arrDeviceValue = new short[1];          //Data for 'DeviceValue'
                        arrDeviceValue[0] = Convert.ToInt16(1);
                        System.String[] arrData = new System.String[1];

                        if (arrAddress != null)
                        {
                            if (arrAddress.Length > 0)
                            {
                                try
                                {
                                    // objLogging.LogError(DateTime.Now, "PLCDateTimeSynch() Write Values -", arrAddress[0], 0);
                                    try
                                    {
                                        arrDeviceValue[0] = Convert.ToInt16(arrDateTimeValue[0]);
                                        arrData = new System.String[] { arrAddress[0] };
                                        DeviceCommunication.CommunicationWithPLC.WriteValuesArray(arrData, arrDeviceValue, 1);
                                    }
                                    catch { }

                                    try
                                    {
                                        //  objLogging.LogError(DateTime.Now, "PLCDateTimeSynch() Write Values -", arrAddress[1], 0);
                                        arrDeviceValue[0] = Convert.ToInt16(arrDateTimeValue[1]);
                                        arrData = new System.String[] { arrAddress[1] };
                                        DeviceCommunication.CommunicationWithPLC.WriteValuesArray(arrData, arrDeviceValue, 1);
                                    }
                                    catch { }

                                    try
                                    {
                                        // objLogging.LogError(DateTime.Now, "PLCDateTimeSynch() Write Values -", arrAddress[2], 0);
                                        arrDeviceValue[0] = Convert.ToInt16(arrDateTimeValue[2]);
                                        arrData = new System.String[] { arrAddress[2] };
                                        DeviceCommunication.CommunicationWithPLC.WriteValuesArray(arrData, arrDeviceValue, 1);
                                    }
                                    catch { }

                                    try
                                    {
                                        //  objLogging.LogError(DateTime.Now, "PLCDateTimeSynch() Write Values -", arrAddress[3], 0);
                                        arrDeviceValue[0] = Convert.ToInt16(arrDateTimeValue[3]);
                                        arrData = new System.String[] { arrAddress[3] };
                                        DeviceCommunication.CommunicationWithPLC.WriteValuesArray(arrData, arrDeviceValue, 1);
                                    }
                                    catch { }
                                    try
                                    {
                                        //  objLogging.LogError(DateTime.Now, "PLCDateTimeSynch() Write Values -", arrAddress[4], 0);
                                        arrDeviceValue[0] = Convert.ToInt16(arrDateTimeValue[4]);
                                        arrData = new System.String[] { arrAddress[4] };
                                        DeviceCommunication.CommunicationWithPLC.WriteValuesArray(arrData, arrDeviceValue, 1);
                                    }
                                    catch { }

                                    try
                                    {
                                        //  objLogging.LogError(DateTime.Now, "PLCDateTimeSynch() Write Values -", arrAddress[5], 0);
                                        arrDeviceValue[0] = Convert.ToInt16(arrDateTimeValue[5]);
                                        arrData = new System.String[] { arrAddress[5] };
                                        DeviceCommunication.CommunicationWithPLC.WriteValuesArray(arrData, arrDeviceValue, 1);
                                    }
                                    catch { }

                                    try
                                    {
                                        //  objLogging.LogError(DateTime.Now, "PLCDateTimeSynch() Write Values -", arrAddress[6], 0);
                                        arrDeviceValue[0] = Convert.ToInt16(arrDateTimeValue[6]);
                                        arrData = new System.String[] { arrAddress[6] };
                                        DeviceCommunication.CommunicationWithPLC.WriteValuesArray(arrData, arrDeviceValue, 1);
                                    }
                                    catch { }
                                    try
                                    {
                                        arrDeviceValue[0] = Convert.ToInt16(1);
                                        arrData = new System.String[] { arrControlBits[0] }; // Confirmation Bit
                                        DeviceCommunication.CommunicationWithPLC.WriteValuesArray(arrData, arrDeviceValue, 1);
                                    }
                                    catch { }
                                    try
                                    {
                                        arrDeviceValue = new short[1];          //Data for 'DeviceValue'
                                        arrDeviceValue[0] = Convert.ToInt16(1);
                                        arrData = new System.String[] { arrControlBits[1] };  // Acknowledgement bit
                                        DeviceCommunication.CommunicationWithPLC.WriteValuesArray(arrData, arrDeviceValue, 1);
                                    }
                                    catch { }
                                }
                                catch (Exception ex)
                                {
                                    ErrorLogger.LogError.ErrorLog("AlarmEventLogic VerifyDateTimeWithPLC()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                                }
                            }
                        }
                    }
                    catch (Exception ex) { ErrorLogger.LogError.ErrorLog("AlarmEventLogic VerifyDateTimeWithPLC()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable); }
                }
                #endregion
            }
            catch { }
        }


        public static ObservableCollection<AlarmDataEntity> GetAlarmLiveRecords()
        {
            //Get alarm list
            return DeviceCommunication.AlarmAndEventDataLog.GeAlarmRecords();
        }
        public static ServiceResponse<string> GetAlarmHelpFromAlarmName(AlarmDataEntity _AlarmDataEntity)
        {
            return IndiSCADADataAccess.DataAccessSelect.GetAlarmHelpFromAlarmName(_AlarmDataEntity);
        }
        public static void AckSelAlarm(AlarmDataEntity _AlarmDataEntity, int index)
        {
            try
            {
                AlarmDataEntity _AlarmDataToSaveEntity = new AlarmDataEntity();
                _AlarmDataToSaveEntity.AlarmDateTime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:MM:ss"));
                _AlarmDataToSaveEntity.AlarmCondition = "ACK";
                _AlarmDataToSaveEntity.AlarmDuration = _AlarmDataEntity.AlarmDuration;
                _AlarmDataToSaveEntity.AlarmGroup = _AlarmDataEntity.AlarmGroup;
                _AlarmDataToSaveEntity.AlarmID = _AlarmDataEntity.AlarmID;
                _AlarmDataToSaveEntity.AlarmName = _AlarmDataEntity.AlarmName;
                _AlarmDataToSaveEntity.AlarmPriority = _AlarmDataEntity.AlarmPriority;
                _AlarmDataToSaveEntity.AlarmText = _AlarmDataEntity.AlarmText;
                _AlarmDataToSaveEntity.AlarmType = _AlarmDataEntity.AlarmType;
                _AlarmDataToSaveEntity.CausesDownTime = _AlarmDataEntity.CausesDownTime;
                _AlarmDataToSaveEntity.isACK = _AlarmDataEntity.isACK;
                _AlarmDataToSaveEntity.isOFF = _AlarmDataEntity.isOFF;
                _AlarmDataToSaveEntity.isON = _AlarmDataEntity.isON;
                ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessInsert.InsertAlarmData(_AlarmDataToSaveEntity);//insert alarm data
                if (_QueryResult.Status == ResponseType.E)
                {
                    ErrorLogger.LogError.ErrorLog("AlarmEventLogic InsertAlarmData(ACK)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
                AlarmMasterEntity _AlarmMaster = new AlarmMasterEntity();
                _AlarmMaster.isON = false;
                _AlarmMaster.isACK = true;
                _AlarmMaster.isOFF = false;
                _AlarmMaster.AlarmName = _AlarmDataEntity.AlarmName;
                _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateAlarmMasterACK(_AlarmMaster);//update alarm master
                if (_QueryResult.Status == ResponseType.E)
                {
                    ErrorLogger.LogError.ErrorLog("AlarmEventLogic UpdateAlarmMaster(ACK)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
                DeviceCommunication.AlarmAndEventDataLog.AckSelAlarm(_AlarmDataEntity, index);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmEventLogic AckSelAlarm()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        public static void AckAllAlarm(ObservableCollection<AlarmDataEntity> _AlarmData)
        {
            try
            {
                foreach (var _AlarmDataEntity in _AlarmData)
                {
                    AlarmDataEntity _AlarmDataToSaveEntity = new AlarmDataEntity();
                    _AlarmDataToSaveEntity.AlarmDateTime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:MM:ss"));
                    _AlarmDataToSaveEntity.AlarmCondition = "ACK";
                    _AlarmDataToSaveEntity.AlarmDuration = _AlarmDataEntity.AlarmDuration;
                    _AlarmDataToSaveEntity.AlarmGroup = _AlarmDataEntity.AlarmGroup;
                    _AlarmDataToSaveEntity.AlarmID = _AlarmDataEntity.AlarmID;
                    _AlarmDataToSaveEntity.AlarmName = _AlarmDataEntity.AlarmName;
                    _AlarmDataToSaveEntity.AlarmPriority = _AlarmDataEntity.AlarmPriority;
                    _AlarmDataToSaveEntity.AlarmText = _AlarmDataEntity.AlarmText;
                    _AlarmDataToSaveEntity.AlarmType = _AlarmDataEntity.AlarmType;
                    _AlarmDataToSaveEntity.CausesDownTime = _AlarmDataEntity.CausesDownTime;
                    _AlarmDataToSaveEntity.isACK = _AlarmDataEntity.isACK;
                    _AlarmDataToSaveEntity.isOFF = _AlarmDataEntity.isOFF;
                    _AlarmDataToSaveEntity.isON = _AlarmDataEntity.isON;
                    ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessInsert.InsertAlarmData(_AlarmDataToSaveEntity);//insert alarm data
                    if (_QueryResult.Status == ResponseType.E)
                    {
                        ErrorLogger.LogError.ErrorLog("AlarmEventLogic InsertAlarmData(ACKALL)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                    AlarmMasterEntity _AlarmMaster = new AlarmMasterEntity();
                    _AlarmMaster.isON = false;
                    _AlarmMaster.isACK = true;
                    _AlarmMaster.isOFF = false;
                    _AlarmMaster.AlarmName = _AlarmDataEntity.AlarmName;
                    _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateAlarmMasterACK(_AlarmMaster);//update alarm master
                    if (_QueryResult.Status == ResponseType.E)
                    {
                        ErrorLogger.LogError.ErrorLog("AlarmEventLogic UpdateAlarmMaster(ACKALL)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                DeviceCommunication.AlarmAndEventDataLog.AckALLAlarm();

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmEventLogic AckAllAlarm()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        public static void ResetSelAlarm(AlarmDataEntity _AlarmDataEntity, int index)
        {
            try
            {

                AlarmDataEntity _AlarmDataToSaveEntity = new AlarmDataEntity();
                _AlarmDataToSaveEntity.AlarmDateTime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:MM:ss"));
                _AlarmDataToSaveEntity.AlarmCondition = "RESET";
                _AlarmDataToSaveEntity.AlarmDuration = _AlarmDataEntity.AlarmDuration;
                _AlarmDataToSaveEntity.AlarmGroup = _AlarmDataEntity.AlarmGroup;
                _AlarmDataToSaveEntity.AlarmID = _AlarmDataEntity.AlarmID;
                _AlarmDataToSaveEntity.AlarmName = _AlarmDataEntity.AlarmName;
                _AlarmDataToSaveEntity.AlarmPriority = _AlarmDataEntity.AlarmPriority;
                _AlarmDataToSaveEntity.AlarmText = _AlarmDataEntity.AlarmText;
                _AlarmDataToSaveEntity.AlarmType = _AlarmDataEntity.AlarmType;
                _AlarmDataToSaveEntity.CausesDownTime = _AlarmDataEntity.CausesDownTime;
                _AlarmDataToSaveEntity.isACK = _AlarmDataEntity.isACK;
                _AlarmDataToSaveEntity.isOFF = _AlarmDataEntity.isOFF;
                _AlarmDataToSaveEntity.isON = _AlarmDataEntity.isON;
                ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessInsert.InsertAlarmData(_AlarmDataToSaveEntity);//insert alarm data
                if (_QueryResult.Status == ResponseType.E)
                {
                    ErrorLogger.LogError.ErrorLog("AlarmEventLogic InsertAlarmData(RESETSel)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
                DeviceCommunication.AlarmAndEventDataLog.ResetSelAlarm(_AlarmDataEntity, index);

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmEventLogic ResetSelAlarm()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        public static void ResetAllAlarm(ObservableCollection<AlarmDataEntity> _AlarmData)
        {
            try
            {
                foreach (var _AlarmDataEntity in _AlarmData)
                {
                    AlarmDataEntity _AlarmDataToSaveEntity = new AlarmDataEntity();
                    _AlarmDataToSaveEntity.AlarmDateTime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:MM:ss"));
                    _AlarmDataToSaveEntity.AlarmCondition = "RESET";
                    _AlarmDataToSaveEntity.AlarmDuration = _AlarmDataEntity.AlarmDuration;
                    _AlarmDataToSaveEntity.AlarmGroup = _AlarmDataEntity.AlarmGroup;
                    _AlarmDataToSaveEntity.AlarmID = _AlarmDataEntity.AlarmID;
                    _AlarmDataToSaveEntity.AlarmName = _AlarmDataEntity.AlarmName;
                    _AlarmDataToSaveEntity.AlarmPriority = _AlarmDataEntity.AlarmPriority;
                    _AlarmDataToSaveEntity.AlarmText = _AlarmDataEntity.AlarmText;
                    _AlarmDataToSaveEntity.AlarmType = _AlarmDataEntity.AlarmType;
                    _AlarmDataToSaveEntity.CausesDownTime = _AlarmDataEntity.CausesDownTime;
                    _AlarmDataToSaveEntity.isACK = _AlarmDataEntity.isACK;
                    _AlarmDataToSaveEntity.isOFF = _AlarmDataEntity.isOFF;
                    _AlarmDataToSaveEntity.isON = _AlarmDataEntity.isON;
                    ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessInsert.InsertAlarmData(_AlarmDataToSaveEntity);//insert alarm data
                    if (_QueryResult.Status == ResponseType.E)
                    {
                        ErrorLogger.LogError.ErrorLog("AlarmEventLogic InsertAlarmData(RESETALL)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                DeviceCommunication.AlarmAndEventDataLog.ResetALLAlarm();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmEventLogic ResetAllAlarm()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        public static ServiceResponse<DataTable> GetAlarmAndEventHistory(DateTime StartDate, DateTime EndDate)
        {
            return IndiSCADADataAccess.DataAccessSelect.GetAlarmAndEventHistory(StartDate, EndDate);
        }
        #endregion
    }
}
