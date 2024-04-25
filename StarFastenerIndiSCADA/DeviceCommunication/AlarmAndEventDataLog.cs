using IndiSCADAEntity.Entity;
using IndiSCADAGlobalLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace DeviceCommunication
{
    public static class AlarmAndEventDataLog
    {

        #region"Declaration"
        static DispatcherTimer DispatchTimerAlarmEvent = new DispatcherTimer();
        static System.ComponentModel.BackgroundWorker _BackgroundWorkerAlarmEvent = new System.ComponentModel.BackgroundWorker();
        private static bool isStartup = false;
        private static bool isEventArrayInitialise = false;
        static bool[] EventValue = new bool[101];
        //[IndiSCADAGlobalLibrary.TagList.EventValues.Length];
        #endregion
        #region "Property"
        private static ObservableCollection<AlarmDataEntity> AlarmCollection = new ObservableCollection<AlarmDataEntity>();
        public static ObservableCollection<AlarmDataEntity> AlarmData
        {
            get { return AlarmCollection; }
            set { AlarmCollection = value; }
        }

        public static object App { get; private set; }
        #endregion
        #region Public/Private Method
        public static ObservableCollection<AlarmDataEntity> GeAlarmRecords()
        {
            try
            {
                return AlarmData;
            }
            catch (Exception exThreadDashBoardUpdateTrigger)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog GeAlarmRecords()", DateTime.Now.ToString(), exThreadDashBoardUpdateTrigger.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return AlarmData;
            }
        }
        public static void ResetSelAlarm(AlarmDataEntity _AlarmSel, int index)
        {
            try
            {
                DispatchTimerAlarmEvent.Stop();
                if (_AlarmSel.AlarmCondition == "OFF")
                {
                    //var Alarmindex = AlarmData.IndexOf(AlarmData.Where(X => X.AlarmName == _AlarmSel.AlarmName).FirstOrDefault());
                    AlarmData.RemoveAt(index);
                }
                DispatchTimerAlarmEvent.Start();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog ResetSelAlarm()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                DispatchTimerAlarmEvent.Start();
            }
        }
        public static void AckSelAlarm(AlarmDataEntity _AlarmSel, int index)
        {
            try
            {
                DispatchTimerAlarmEvent.Stop();
                AlarmData[index].isACK = true;
                DispatchTimerAlarmEvent.Start();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog AckSelAlarm()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                DispatchTimerAlarmEvent.Start();
            }
        }
        public static void AckALLAlarm()
        {
            try
            {
                DispatchTimerAlarmEvent.Stop();
                for (int index = 0; index <= AlarmData.Count - 1; index++)
                {
                    AlarmData[index].isACK = true;
                }
                DispatchTimerAlarmEvent.Start();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog AckALLAlarm()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                DispatchTimerAlarmEvent.Start();
            }
        }
        public static void ResetALLAlarm()
        {
            try
            {
                DispatchTimerAlarmEvent.Stop();
                IList<AlarmDataEntity> _data = AlarmData.Where(i => i.AlarmCondition != "OFF").ToList<AlarmDataEntity>();
                AlarmData = new ObservableCollection<AlarmDataEntity>(_data);
                DispatchTimerAlarmEvent.Start();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog ResetALLAlarm()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                DispatchTimerAlarmEvent.Start();
            }
        }
        private static void DoWork(object Sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                //App.Current.Dispatcher.BeginInvoke(new Action(() => 
                //{
                GenerateEvents(); 
                //}));

            }
            catch (Exception exThreadDashBoardUpdateTrigger)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog DoWork()", DateTime.Now.ToString(), exThreadDashBoardUpdateTrigger.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            try
            {
                //App.Current.Dispatcher.BeginInvoke(new Action(() => 
                //{ 
                GenerateAlarms();
                //}));

            }
            catch (Exception exThreadDashBoardUpdateTrigger)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog DoWork()", DateTime.Now.ToString(), exThreadDashBoardUpdateTrigger.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        private static void DispatcherTickEvent(object sender, EventArgs e)
        {
            try
            {
                if (_BackgroundWorkerAlarmEvent.IsBusy != true)
                {
                    _BackgroundWorkerAlarmEvent.RunWorkerAsync();
                }
            }
            catch (Exception exDispatcherTickEvent)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog DispatcherTickEvent()", DateTime.Now.ToString(), exDispatcherTickEvent.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        //Start background worker to read plc tag and data log
        public static void StartAlarmAndEventTracking()
        {
            try
            {
                isStartup = true;
                DeclareAlarmArray();
                _BackgroundWorkerAlarmEvent.DoWork += DoWork;
                DispatchTimerAlarmEvent.Interval = TimeSpan.FromMilliseconds(500);
                DispatchTimerAlarmEvent.Tick += DispatcherTickEvent;
                DispatchTimerAlarmEvent.Start();
            }
            catch (Exception exStartPlcComminicationAndDataLog)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog StartAlarmAndEventTracking()", DateTime.Now.ToString(), exStartPlcComminicationAndDataLog.Message, null, true);
            }
        }
        public static string[] GetAlarmArray()
        {
            try
            {
                string[] TotalAlarms = new string[] { };
                var listAllAlarms = new List<string>();
                listAllAlarms.AddRange(IndiSCADAGlobalLibrary.TagList.W1Alarms);
                listAllAlarms.AddRange(IndiSCADAGlobalLibrary.TagList.W2Alarms);
                listAllAlarms.AddRange(IndiSCADAGlobalLibrary.TagList.W3Alarms);
                listAllAlarms.AddRange(IndiSCADAGlobalLibrary.TagList.W4Alarms);
                listAllAlarms.AddRange(IndiSCADAGlobalLibrary.TagList.W5Alarms);
                listAllAlarms.AddRange(IndiSCADAGlobalLibrary.TagList.W6Alarms);
                listAllAlarms.AddRange(IndiSCADAGlobalLibrary.TagList.TemperatureAlarms);
                listAllAlarms.AddRange(IndiSCADAGlobalLibrary.TagList.DryerAlarms);
                listAllAlarms.AddRange(IndiSCADAGlobalLibrary.TagList.CrossTrolleyAlarms);
                listAllAlarms.AddRange(IndiSCADAGlobalLibrary.TagList.RectifierAlarms1);
                listAllAlarms.AddRange(IndiSCADAGlobalLibrary.TagList.pHAlarm);

                TotalAlarms = listAllAlarms.ToArray();
                return TotalAlarms;
            }
            catch (Exception exStartPlcComminicationAndDataLog)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog GetAlarmArray()", DateTime.Now.ToString(), exStartPlcComminicationAndDataLog.Message, null, true);
                return null;
            }
        }

        public static string[] GetEventArray()
        {
            try
            {
                string[] TotalEvents = new string[] { };
                //TotalAlarms = new string[IndiSCADAGlobalLibrary.TagList.W1Alarms.Length + IndiSCADAGlobalLibrary.TagList.W2Alarms.Length + IndiSCADAGlobalLibrary.TagList.W3Alarms.Length + IndiSCADAGlobalLibrary.TagList.W4Alarms.Length + IndiSCADAGlobalLibrary.TagList.W5Alarms.Length + IndiSCADAGlobalLibrary.TagList.TemperatureAlarms.Length + IndiSCADAGlobalLibrary.TagList.DryerAlarms.Length + IndiSCADAGlobalLibrary.TagList.RectifierAlarms.Length];
                //IndiSCADAGlobalLibrary.TagList.W1Alarms.CopyTo(TotalAlarms, 0);
                //IndiSCADAGlobalLibrary.TagList.W2Alarms.CopyTo(TotalAlarms, IndiSCADAGlobalLibrary.TagList.W1Alarms.Length);
                //IndiSCADAGlobalLibrary.TagList.W3Alarms.CopyTo(TotalAlarms, IndiSCADAGlobalLibrary.TagList.W1Alarms.Length + IndiSCADAGlobalLibrary.TagList.W2Alarms.Length);
                //IndiSCADAGlobalLibrary.TagList.W4Alarms.CopyTo(TotalAlarms, IndiSCADAGlobalLibrary.TagList.W1Alarms.Length + IndiSCADAGlobalLibrary.TagList.W2Alarms.Length + IndiSCADAGlobalLibrary.TagList.W3Alarms.Length);
                //IndiSCADAGlobalLibrary.TagList.W5Alarms.CopyTo(TotalAlarms, IndiSCADAGlobalLibrary.TagList.W1Alarms.Length + IndiSCADAGlobalLibrary.TagList.W2Alarms.Length + IndiSCADAGlobalLibrary.TagList.W3Alarms.Length + IndiSCADAGlobalLibrary.TagList.W4Alarms.Length);
                //IndiSCADAGlobalLibrary.TagList.TemperatureAlarms.CopyTo(TotalAlarms, IndiSCADAGlobalLibrary.TagList.W1Alarms.Length + IndiSCADAGlobalLibrary.TagList.W2Alarms.Length + IndiSCADAGlobalLibrary.TagList.W3Alarms.Length + IndiSCADAGlobalLibrary.TagList.W4Alarms.Length + IndiSCADAGlobalLibrary.TagList.W5Alarms.Length);
                var listAllEvents = new List<string>();
                listAllEvents.AddRange(IndiSCADAGlobalLibrary.TagList.PlantEventValue);
                listAllEvents.AddRange(IndiSCADAGlobalLibrary.TagList.W1EventValue);
                listAllEvents.AddRange(IndiSCADAGlobalLibrary.TagList.W2EventValue);
                listAllEvents.AddRange(IndiSCADAGlobalLibrary.TagList.W3EventValue);
                listAllEvents.AddRange(IndiSCADAGlobalLibrary.TagList.W4EventValue);
                listAllEvents.AddRange(IndiSCADAGlobalLibrary.TagList.W4EventValue);//Rectifier
                listAllEvents.AddRange(IndiSCADAGlobalLibrary.TagList.W5EventValue);//othdryer
                listAllEvents.AddRange(IndiSCADAGlobalLibrary.TagList.W6EventValue);//Dosing

                TotalEvents = listAllEvents.ToArray();
                return TotalEvents;
            }
            catch (Exception exStartPlcComminicationAndDataLog)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog GetAlarmArray()", DateTime.Now.ToString(), exStartPlcComminicationAndDataLog.Message, null, true);
                return null;
            }
        }
        private static void GenerateAlarms()
        {
            try
            {
               // ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog GenerateAlarms() Execution start", DateTime.Now.ToString(), "", null, true);

                //string[] TotalAlarms = new string []{ "0","0","0","0" , "0", "0" , "0", "0" , "0", "0" , "0", "0" , "0", "0" , "0", "0" , "0", "0" };//GetAlarmArray();//Read All alarms in single array(wagon 1 , wagon 2 and rectifier etc)
                //IndiSCADAGlobalLibrary.TagList.WagonMovment = new string[] {"1", "1", "1", "1", "1" };
                string[] TotalAlarms = GetAlarmArray();//Read All alarms in single array(wagon 1 , wagon 2 and rectifier etc)
                ServiceResponse<DataTable> ResultAlarmMaster = IndiSCADADataAccess.DataAccessSelect.SelectAlarmMasterData();
                if (ResultAlarmMaster.Status == ResponseType.S)
                {
                    DataTable dtAlarmMaster = ResultAlarmMaster.Response;
                    // if (TotalAlarms != null && TotalAlarms.Length != 0)
                    {
                        for (int Rowindex = 0; Rowindex <= dtAlarmMaster.Rows.Count - 1; Rowindex++)
                        {
                            try
                            {
                                //Alarm On Condition execution----------------------------------------------------------------
                                String AlarmTagAddress = dtAlarmMaster.Rows[Rowindex]["AlarmAddress"].ToString();
                                String[] arrData = new String[] { AlarmTagAddress };
                                string[] AlarmTagValue = CommunicationWithPLC.Read_BlockAddress(arrData, 1);

                               // ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog GenerateAlarms() Address=" + AlarmTagAddress +  " Address data=" + AlarmTagValue[0] + " Condition=" + dtAlarmMaster.Rows[Rowindex]["AlarmOnCondition"].ToString(), DateTime.Now.ToString(), "", null, true);

                                #region "ON"
                                if (AlarmTagValue[0] == dtAlarmMaster.Rows[Rowindex]["AlarmOnCondition"].ToString().Trim())
                                { 
                                    if (dtAlarmMaster.Rows[Rowindex]["isON"].ToString().Equals("False"))
                                    {
                                        //ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog GenerateAlarms() Alarm On Address=" + AlarmTagAddress + " Address data=" + AlarmTagValue[0] + " Condition=" + dtAlarmMaster.Rows[Rowindex]["AlarmOnCondition"].ToString(), DateTime.Now.ToString(), "", null, true);

                                        string AlarmType = dtAlarmMaster.Rows[Rowindex]["AlarmGroup"].ToString();
                                        string AlarmText = "";
                                        string Operations = "";
                                        if (AlarmType == "W1 Alarms")
                                        {
                                            Operations = GetWagonCurrentOperation(1);
                                            AlarmText = dtAlarmMaster.Rows[Rowindex]["AlarmText"].ToString() + " At Stn " + IndiSCADAGlobalLibrary.TagList.WagonMovment[0].ToString() + " Current Operation: " + Operations + "";
                                            AlarmONCondition(AlarmText, dtAlarmMaster.Rows[Rowindex]["AlarmName"].ToString(), AlarmType, "True");
                                        }
                                        else if (AlarmType == "W2 Alarms")
                                        {
                                            Operations = GetWagonCurrentOperation(2);
                                            AlarmText = dtAlarmMaster.Rows[Rowindex]["AlarmText"].ToString() + " At Stn " + IndiSCADAGlobalLibrary.TagList.WagonMovment[1].ToString() + " Current Operation: " + Operations + "";
                                            AlarmONCondition(AlarmText, dtAlarmMaster.Rows[Rowindex]["AlarmName"].ToString(), AlarmType, "True");
                                        }
                                        else if (AlarmType == "W3 Alarms")
                                        {
                                            Operations = GetWagonCurrentOperation(3);
                                            AlarmText = dtAlarmMaster.Rows[Rowindex]["AlarmText"].ToString() + " At Stn " + IndiSCADAGlobalLibrary.TagList.WagonMovment[2].ToString() + " Current Operation: " + Operations + "";
                                            AlarmONCondition(AlarmText, dtAlarmMaster.Rows[Rowindex]["AlarmName"].ToString(), AlarmType, "True");
                                        }
                                        else if (AlarmType == "W4 Alarms")
                                        {
                                            Operations = GetWagonCurrentOperation(4);
                                            AlarmText = dtAlarmMaster.Rows[Rowindex]["AlarmText"].ToString() + " At Stn " + IndiSCADAGlobalLibrary.TagList.WagonMovment[3].ToString() + " Current Operation: " + Operations + "";
                                            AlarmONCondition(AlarmText, dtAlarmMaster.Rows[Rowindex]["AlarmName"].ToString(), AlarmType, "True");
                                        }
                                        else if (AlarmType == "W5 Alarms")
                                        {
                                            Operations = GetWagonCurrentOperation(5);
                                            AlarmText = dtAlarmMaster.Rows[Rowindex]["AlarmText"].ToString() + " At Stn " + IndiSCADAGlobalLibrary.TagList.WagonMovment[4].ToString() + " Current Operation: " + Operations + "";
                                            AlarmONCondition(AlarmText, dtAlarmMaster.Rows[Rowindex]["AlarmName"].ToString(), AlarmType, "True");
                                        }
                                        else if (AlarmType == "W6 Alarms")
                                        {
                                            Operations = GetWagonCurrentOperation(6);
                                            AlarmText = dtAlarmMaster.Rows[Rowindex]["AlarmText"].ToString() + " At Stn " + IndiSCADAGlobalLibrary.TagList.WagonMovment[5].ToString() + " Current Operation: " + Operations + "";
                                            AlarmONCondition(AlarmText, dtAlarmMaster.Rows[Rowindex]["AlarmName"].ToString(), AlarmType, "True");
                                        }
                                        else if (AlarmType == "W7 Alarms")
                                        {
                                            Operations = GetWagonCurrentOperation(7);
                                            AlarmText = dtAlarmMaster.Rows[Rowindex]["AlarmText"].ToString() + " At Stn " + IndiSCADAGlobalLibrary.TagList.WagonMovment[6].ToString() + " Current Operation: " + Operations + "";
                                            AlarmONCondition(AlarmText, dtAlarmMaster.Rows[Rowindex]["AlarmName"].ToString(), AlarmType, "True");
                                        }
                                        else if (AlarmType == "W8 Alarms")
                                        {
                                            Operations = GetWagonCurrentOperation(8);
                                            AlarmText = dtAlarmMaster.Rows[Rowindex]["AlarmText"].ToString() + " At Stn " + IndiSCADAGlobalLibrary.TagList.WagonMovment[7].ToString() + " Current Operation: " + Operations + "";
                                            AlarmONCondition(AlarmText, dtAlarmMaster.Rows[Rowindex]["AlarmName"].ToString(), AlarmType, "True");
                                        }
                                        else
                                        {
                                            AlarmText = dtAlarmMaster.Rows[Rowindex]["AlarmText"].ToString();
                                            AlarmONCondition(AlarmText, dtAlarmMaster.Rows[Rowindex]["AlarmName"].ToString(), AlarmType, "True");
                                        }
                                    }
                                    else
                                    {
                                        if (isStartup == true)
                                        {
                                            ifStartupShowAlreadyOnALarms(dtAlarmMaster.Rows[Rowindex]["AlarmText"].ToString());//execute only first time
                                        }
                                    }
                                }
                                #endregion
                                //Alarm OFF Condition Execution---------------------------------------------------------------
                                #region "OFF"
                                else if (AlarmTagValue[0] == dtAlarmMaster.Rows[Rowindex]["AlarmOFFCondition"].ToString().Trim())
                                {
                                    //ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog GenerateAlarms() Alarm Off Address=" + AlarmTagAddress + " Address data=" + AlarmTagValue[0] + " Condition=" + dtAlarmMaster.Rows[Rowindex]["AlarmOnCondition"].ToString(), DateTime.Now.ToString(), "", null, true);

                                    string duration = "0";
                                    if (dtAlarmMaster.Rows[Rowindex]["isON"].ToString().Equals("True"))// ////
                                    {
                                        if (AlarmData != null)
                                        {
                                            if (AlarmData.Count > 0)
                                            {
                                                foreach (var item in AlarmData)
                                                {
                                                    if (item.AlarmName == dtAlarmMaster.Rows[Rowindex]["AlarmName"].ToString() && item.AlarmCondition == "ON")
                                                    {
                                                        AlarmOFFCondition(item);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //checked alarm as off on scada start up
                                                if (isStartup == true)
                                                {
                                                    AlarmOFFOnstarupCondition(dtAlarmMaster.Rows[Rowindex]["AlarmText"].ToString());//execute only first time application start
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //checked alarm as off on scada start up
                                            //checked alarm as off on scada start up
                                            if (isStartup == true)
                                            {
                                                AlarmOFFOnstarupCondition(dtAlarmMaster.Rows[Rowindex]["AlarmText"].ToString());//execute only first time application start
                                            }
                                        }
                                        AlarmOFFOnstarupCondition(dtAlarmMaster.Rows[Rowindex]["AlarmText"].ToString());
                                    }
                                }// if (TotalAlarms != null && TotalAlarms.Length != 0)
                                #endregion

                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog ForLoop Alarm()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                    }
                    //else
                    //{
                    //    return;
                    //}
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog SelectAlarmMasterData()", DateTime.Now.ToString(), ResultAlarmMaster.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog GenerateAlarms()", DateTime.Now.ToString(), ex.Message, null, true);
            }
            isStartup = false;//after running first time change startup to false so that code will not execute again
        }
        //Get Wagon Current Operations
        private static void AlarmOFFOnstarupCondition(string AlarmText)
        {
            try
            {
                IndiSCADADataAccess.DataAccessUpdate.UpdateAlarmMasterOffAlarm(AlarmText);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog AlarmOFFOnstarupCondition()", DateTime.Now.ToString(), ex.Message, null, true);
            }
        }
        private static string GetWagonCurrentOperation(int wagonNo)
        {
            try
            {
                string WagonOperations = " ";
                if (wagonNo == 1)
                {
                    if (IndiSCADAGlobalLibrary.TagList.WagonNextStep != null && IndiSCADAGlobalLibrary.TagList.WagonNextStation.Length != 0)
                    {
                        string w1Operation = IndiSCADAGlobalLibrary.TagList.WagonNextStep[0];
                       
                        if (w1Operation == "4")
                        {
                            WagonOperations = "W1 Get From Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[0];
                        }
                        if (w1Operation == "5")
                        {
                            WagonOperations = "W1 Put On Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[0]; 
                        }

                    }
                    else
                    {
                        ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog getWagonCurrentOperation(W1)", DateTime.Now.ToString(), "Wagon1 Next step values null records.", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                if (wagonNo == 2)
                {
                    if (IndiSCADAGlobalLibrary.TagList.WagonNextStep != null && IndiSCADAGlobalLibrary.TagList.WagonNextStation.Length != 0)
                    {
                        string w1Operation = IndiSCADAGlobalLibrary.TagList.WagonNextStep[1];
                        if (w1Operation == "4")
                        {
                            WagonOperations = "W2 Get From Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[1];
                        }
                        if (w1Operation == "5")
                        {
                            WagonOperations = "W2 Put On Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[1]; 
                        }

                    }
                    else
                    {
                        ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog getWagonCurrentOperation(W2)", DateTime.Now.ToString(), "Wagon2 Next step values null records.", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                if (wagonNo == 3)
                {
                    if (IndiSCADAGlobalLibrary.TagList.WagonNextStep != null && IndiSCADAGlobalLibrary.TagList.WagonNextStation.Length != 0)
                    {
                        string w1Operation = IndiSCADAGlobalLibrary.TagList.WagonNextStep[2];
                        if (w1Operation == "4")
                        {
                            WagonOperations = "W3 Get From Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[2];
                        }
                        if (w1Operation == "5")
                        {
                            WagonOperations = "W3 Put On Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[2]; 
                        }

                    }
                    else
                    {
                        ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog getWagonCurrentOperation(W3)", DateTime.Now.ToString(), "Wagon3 Next step values null records.", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                if (wagonNo == 4)
                {
                    if (IndiSCADAGlobalLibrary.TagList.WagonNextStep != null && IndiSCADAGlobalLibrary.TagList.WagonNextStation.Length != 0)
                    {
                        string w1Operation = IndiSCADAGlobalLibrary.TagList.WagonNextStep[3];
                        if (w1Operation == "4")
                        {
                            WagonOperations = "W4 Get From Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[3];
                        }
                        if (w1Operation == "5")
                        {
                            WagonOperations = "W4 Put On Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[3]; 
                        }

                    }
                    else
                    {
                        ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog getWagonCurrentOperation(W4)", DateTime.Now.ToString(), "Wagon4 Next step values null records.", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                if (wagonNo == 5)
                {
                    if (IndiSCADAGlobalLibrary.TagList.WagonNextStep != null && IndiSCADAGlobalLibrary.TagList.WagonNextStation.Length != 0)
                    {
                        string w1Operation = IndiSCADAGlobalLibrary.TagList.WagonNextStep[4];
                        if (w1Operation == "4")
                        {
                            WagonOperations = "W5 Get From Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[4];
                        }
                        if (w1Operation == "5")
                        {
                            WagonOperations = "W5 Put On Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[4]; 
                        }

                    }
                    else
                    {
                        ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog getWagonCurrentOperation(W5)", DateTime.Now.ToString(), "Wagon5 Next step values null records.", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                if (wagonNo == 6)
                {
                    if (IndiSCADAGlobalLibrary.TagList.WagonNextStep != null && IndiSCADAGlobalLibrary.TagList.WagonNextStation.Length != 0)
                    {
                        string w1Operation = IndiSCADAGlobalLibrary.TagList.WagonNextStep[5];
                        if (w1Operation == "4")
                        {
                            WagonOperations = "W6 Get From Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[5];
                        }
                        if (w1Operation == "5")
                        {
                            WagonOperations = "W6 Put On Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[5]; 
                        }

                    }
                    else
                    {
                        ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog getWagonCurrentOperation(W6)", DateTime.Now.ToString(), "Wagon6 Next step values null records.", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                if (wagonNo == 7)
                {
                    if (IndiSCADAGlobalLibrary.TagList.WagonNextStep != null && IndiSCADAGlobalLibrary.TagList.WagonNextStation.Length != 0)
                    {
                        string w1Operation = IndiSCADAGlobalLibrary.TagList.WagonNextStep[6];
                        if (w1Operation == "4")
                        {
                            WagonOperations = "W7 Get From Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[6];
                        }
                        if (w1Operation == "5")
                        {
                            WagonOperations = "W7 Put On Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[6]; 
                        }

                    }
                    else
                    {
                        ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog getWagonCurrentOperation(W7)", DateTime.Now.ToString(), "Wagon7 Next step values null records.", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                if (wagonNo == 8)
                {
                    if (IndiSCADAGlobalLibrary.TagList.WagonNextStep != null && IndiSCADAGlobalLibrary.TagList.WagonNextStation.Length != 0)
                    {
                        string w1Operation = IndiSCADAGlobalLibrary.TagList.WagonNextStep[7];
                        if (w1Operation == "4")
                        {
                            WagonOperations = "W8 Get From Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[7];
                        }
                        if (w1Operation == "5")
                        {
                            WagonOperations = "W8 Put On Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[7]; 
                        }

                    }
                    else
                    {
                        ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog getWagonCurrentOperation(W8)", DateTime.Now.ToString(), "Wagon8 Next step values null records.", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                return WagonOperations;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog getWagonCurrentOperation()", DateTime.Now.ToString(), ex.Message, null, true);
                return null;
            }
        }
        private static void AlarmONStartUPCondition(string AlarmText, string AlarmName, string Alarmtype, string AlarmDateTime)
        {
            try
            {
                AlarmDataEntity _AlarmDataStartUp = new AlarmDataEntity();
                _AlarmDataStartUp.AlarmText = AlarmText;
                _AlarmDataStartUp.AlarmName = AlarmName;
                _AlarmDataStartUp.CausesDownTime = false;
                _AlarmDataStartUp.AlarmDateTime = DateTime.Parse(AlarmDateTime);
                _AlarmDataStartUp.AlarmType = Alarmtype;
                _AlarmDataStartUp.AlarmDuration = "";
                _AlarmDataStartUp.AlarmCondition = "ON";
                _AlarmDataStartUp.isON = true;
                _AlarmDataStartUp.isACK = true;
                _AlarmDataStartUp.isOFF = false;
                AlarmData.Add(_AlarmDataStartUp);//adding ON alarm in collection List this will use to view alarm list

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog AlarmONStartUPCondition()", DateTime.Now.ToString(), ex.Message, null, true);
            }
        }
        private static void AlarmONCondition(string AlarmText, string AlarmName, string Alarmtype, string CausesDownTime)
        {
            try
            {
                AlarmDataEntity _AlarmDataON = new AlarmDataEntity();
                _AlarmDataON.AlarmText = AlarmText;
                _AlarmDataON.AlarmName = AlarmName;
                _AlarmDataON.CausesDownTime = Convert.ToBoolean(CausesDownTime);
                _AlarmDataON.AlarmDateTime = DateTime.Now;//Convert.ToDateTime((DateTime.Now.ToString("yyyy/MM/dd HH:MM:ss tt")));
                _AlarmDataON.AlarmType = Alarmtype;
                _AlarmDataON.AlarmDuration = "";
                _AlarmDataON.AlarmCondition = "ON";
                _AlarmDataON.isON = true;
                _AlarmDataON.isOFF = false;
                _AlarmDataON.isACK = false;
                _AlarmDataON.AlarmPriority = _AlarmDataON.AlarmPriority;
                AlarmData.Add(_AlarmDataON);//adding ON alarm in collection List this will use to view alarm list
                ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessInsert.InsertAlarmData(_AlarmDataON);//insert alarm data
                if (_QueryResult.Status == ResponseType.E)
                {
                    ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog InsertAlarmData(ON)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
                else
                {
                    AlarmMasterEntity _AlarmMasterON = new AlarmMasterEntity();
                    _AlarmMasterON.isON = true;
                    _AlarmMasterON.isACK = false;
                    _AlarmMasterON.isOFF = false;
                    _AlarmMasterON.AlarmName = AlarmName;
                    _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateAlarmMaster(_AlarmMasterON);//update alarm master
                    if (_QueryResult.Status == ResponseType.E)
                    {
                        ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog UpdateAlarmMaster(ON)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog AlarmOnCondition()", DateTime.Now.ToString(), ex.Message, null, true);
            }
        }
        private static void getPriority()
        {




        }
        private static void ifStartupShowAlreadyOnALarms(string AlarmName)
        {
            try
            {
                //Get alarm details from database
                ServiceResponse<DataTable> _result = IndiSCADADataAccess.DataAccessSelect.SelectAlarmData(AlarmName);
                if (_result.Status == ResponseType.S)
                {
                    if (_result.Response != null)
                    {
                        DataTable DTResult = _result.Response;
                        string AlarmType = DTResult.Rows[0]["AlarmGroup"].ToString();
                        string AlarmText = DTResult.Rows[0]["AlarmText"].ToString();
                        string DateTimeAlarmON = DTResult.Rows[0]["AlarmDateTime"].ToString();
                        AlarmONStartUPCondition(AlarmText, AlarmName, AlarmType, DateTimeAlarmON);
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog ifStartupShowAlreadyOnALarms() AlarmName : " + AlarmName, DateTime.Now.ToString(), ex.Message, null, true);
            }
        }

        private static void initialiseEventValueArray()
        {
            try
            {
                //initialise event values
                for (int i = 0; i < EventValue.Length; i++)
                {
                    EventValue[i] = false;
                }
                isEventArrayInitialise = true;

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog initialiseEventValueArray()", DateTime.Now.ToString(), ex.Message, null, true);
            }
        }
        public static string GetAlarmDuration(DateTime DTAlarmON)
        {
            try
            {
                TimeSpan ts = new TimeSpan();
                ts = DateTime.Now - DTAlarmON;
                return ts.Days + ",  " + ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog GetAlarmDuration()", DateTime.Now.ToString(), ex.Message, null, true);
                return null;
            }
        }

        private static void AlarmOFFCondition(AlarmDataEntity _AlarmDataEntity)
        {
            try
            {
                AlarmDataEntity _AlarmData = new AlarmDataEntity();
                _AlarmData.AlarmText = _AlarmDataEntity.AlarmText;
                _AlarmData.AlarmName = _AlarmDataEntity.AlarmName;
                _AlarmData.AlarmDateTime = DateTime.Now;
                //_AlarmData.AlarmDateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:MM:ss tt"));
                _AlarmData.AlarmType = _AlarmDataEntity.AlarmType;
                _AlarmData.AlarmDuration = GetAlarmDuration(_AlarmDataEntity.AlarmDateTime);
                _AlarmData.AlarmCondition = "OFF";
                _AlarmData.isOFF = true;
                var Alarmindex = AlarmData.IndexOf((AlarmData.Where(X => X.AlarmName == _AlarmDataEntity.AlarmName && X.AlarmCondition == "ON")).FirstOrDefault());
                AlarmData[Alarmindex].AlarmCondition = "OFF";//adding OFF alarm in collection List this will use to view alarm list
                AlarmData[Alarmindex].AlarmDuration = GetAlarmDuration(_AlarmDataEntity.AlarmDateTime);
                AlarmData[Alarmindex].isON = false;
                AlarmData[Alarmindex].isOFF = true;
                AlarmData[Alarmindex].isACK = _AlarmDataEntity.isACK;
                AlarmData[Alarmindex].AlarmPriority = _AlarmDataEntity.AlarmPriority;
                ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessInsert.InsertAlarmData(_AlarmData);//insert alarm data
                if (_QueryResult.Status == ResponseType.E)
                {
                    ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog InsertAlarmData(OFF)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
                else
                {
                    AlarmMasterEntity _AlarmMaster = new AlarmMasterEntity();
                    _AlarmMaster.isON = false;
                    _AlarmMaster.isOFF = true;
                    _AlarmMaster.isACK = false;
                    _AlarmMaster.AlarmName = _AlarmDataEntity.AlarmName;
                    _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateAlarmMaster(_AlarmMaster);//update alarm master
                    if (_QueryResult.Status == ResponseType.E)
                    {
                        ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog UpdateAlarmMaster(OFF)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog AlarmOFFCondition()", DateTime.Now.ToString(), ex.Message, null, true);
            }
        }
        private static void AlarmAckCondition(AlarmDataEntity _AlarmDataEntity, int index)
        {
            try
            {
                AlarmData[index].isACK = true;

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog AlarmAckCondition()", DateTime.Now.ToString(), ex.Message, null, true);

            }
        }
        private static void DeclareAlarmArray()
        {
            try
            {
                //ServiceResponse<DataTable> ResultEventMaster = IndiSCADADataAccess.DataAccessSelect.SelectEventMasterData();
                //if (ResultEventMaster.Status == ResponseType.S)
                //{
                //    DataTable dtEventMaster = ResultEventMaster.Response;
                //    EventValue = new bool[dtEventMaster.Rows.Count];
                //}

            }
            catch
            {

            }

        }

        private static void GenerateEvents()
        {
            try
            {
                string Address_for_WagonOperations = "";
                string Address_for_StationNo = "";
                string operation = "";

                //string[] TotalEvents = new string[] { };
                if (isEventArrayInitialise == false)
                    initialiseEventValueArray();
                DataTable dtEventMaster = new DataTable();
                ServiceResponse<DataTable> ResultEventMaster = IndiSCADADataAccess.DataAccessSelect.SelectEventMasterData();
                if (ResultEventMaster.Status == ResponseType.S)
                {
                    dtEventMaster = ResultEventMaster.Response;
                    {
                        int indexfordtmaster = 0;
                        //----------------------------- start reading tags ------------------------------------------------
                        for (int index = 0; index <= dtEventMaster.Rows.Count - 1; index++)//
                        {
                            try
                            {
                                indexfordtmaster = index;//index - 1;
                                String EventTagAddress = dtEventMaster.Rows[indexfordtmaster]["EventAddress"].ToString();
                                String[] arrData = new String[] { EventTagAddress };
                                string[] EventTagValue = CommunicationWithPLC.Read_BlockAddress(arrData, 1);

                              //  ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog EventTagAddress1()" + EventTagAddress + "=" + EventTagValue[0] + "-" + EventValue.Length + "-" + dtEventMaster.Rows[indexfordtmaster]["EventOnCondition"].ToString(), DateTime.Now.ToString(), "", null, true);

                                if (EventTagValue[0].ToString().Trim() == dtEventMaster.Rows[indexfordtmaster]["EventOnCondition"].ToString().Trim())
                                {
                                   // ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog EventTagAddress2()" + EventTagValue[0], DateTime.Now.ToString(), "", null, true);
                                    if (EventValue[index] == false)
                                    {
                                       // ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog EventTagAddress3()" + EventValue[index].ToString(), DateTime.Now.ToString(), "", null, true);

                                        EventLogEntity InsertEventLog = new EventLogEntity();
                                        InsertEventLog.StartDateTime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss tt"));
                                        InsertEventLog.EndDateTime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss tt"));
                                        InsertEventLog.Description = dtEventMaster.Rows[indexfordtmaster]["EventText"].ToString();
                                        InsertEventLog.isComplete = false;
                                        InsertEventLog.GroupName = dtEventMaster.Rows[indexfordtmaster]["EventGroup"].ToString();

                                        if (dtEventMaster.Rows[indexfordtmaster]["WagonOperation"].ToString() != "NA" && dtEventMaster.Rows[indexfordtmaster]["StationNo"].ToString() != "NA")
                                        {
                                            Address_for_WagonOperations = dtEventMaster.Rows[indexfordtmaster]["WagonOperation"].ToString();
                                            String[] WagonOperationaddress = new String[] { Address_for_WagonOperations };
                                            string[] WagonOperation = CommunicationWithPLC.Read_BlockAddress(WagonOperationaddress, 1); //WagonMovment

                                            Address_for_StationNo = dtEventMaster.Rows[indexfordtmaster]["StationNo"].ToString();
                                            String[] StationNo_address = new String[] { Address_for_StationNo };
                                            string[] stationNo = CommunicationWithPLC.Read_BlockAddress(StationNo_address, 1); //stationNo

                                           // ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog EventGeneration() WagonOperation[0] = " + WagonOperation[0].ToString(), DateTime.Now.ToString(), "", null, true);
                                            if (WagonOperation[0] == "4")
                                            {
                                                operation = "Get Operation";
                                            }
                                            if (WagonOperation[0] == "5")
                                            {
                                                operation = "Put Operation";
                                            }

                                            InsertEventLog.EventText = dtEventMaster.Rows[indexfordtmaster]["EventText"].ToString() + " At Stn " + stationNo[0].ToString() + " During " + operation;
                                        }
                                        else
                                        {
                                            ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog EventGeneration() WagonOperation is NA", DateTime.Now.ToString(), "", null, true);
                                            InsertEventLog.EventText = dtEventMaster.Rows[indexfordtmaster]["EventText"].ToString();
                                        }

                                        ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessInsert.InsertEventsData(InsertEventLog);
                                        if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                                        {
                                            ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog GenerateEvents(ON)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                                        }
                                        else
                                        {
                                            EventValue[index] = true;
                                        }
                                    }
                                }
                                else if (EventTagValue[0].ToString().Trim() == dtEventMaster.Rows[indexfordtmaster]["EventOFFCondition"].ToString().Trim())
                                {
                                    EventLogEntity UpdateEventLog = new EventLogEntity();
                                    UpdateEventLog.StartDateTime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss tt"));//overflowe
                                    UpdateEventLog.EndDateTime = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss tt"));
                                    UpdateEventLog.Description = dtEventMaster.Rows[indexfordtmaster]["EventText"].ToString();
                                    UpdateEventLog.isComplete = true;
                                    ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateEventsData(UpdateEventLog);
                                    if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                                    {
                                        ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog GenerateEvents(OFF)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                                    }
                                    else
                                    {
                                        EventValue[index] = false;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog For Lopp event()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                    }
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog SelectEventMasterData()", DateTime.Now.ToString(), ResultEventMaster.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("AlarmAndEventDataLog GenerateEvents()", DateTime.Now.ToString(), ex.Message, null, true);
            }
        }

        #endregion
    }
}
