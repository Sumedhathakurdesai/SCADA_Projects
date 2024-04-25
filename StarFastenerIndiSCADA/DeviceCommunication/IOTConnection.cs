using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Threading;
using IndiSCADAEntity.Entity;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data;
using IndiSCADAGlobalLibrary;
using System.Net;
using System.Threading;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
//using System.ComponentModel.DataAnnotations; 

namespace DeviceCommunication
{
    public partial class IOTConnection
    {
        #region"Declaration"        
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        System.ComponentModel.BackgroundWorker _MyDashboardWorker = new System.ComponentModel.BackgroundWorker();
        DispatcherTimer _ThreadDashBoardUpdate = new DispatcherTimer();
        static System.Security.Cryptography.X509Certificates.X509Certificate _certificate = new System.Security.Cryptography.X509Certificates.X509Certificate();
        #endregion

        //previous working broker
        //static MqttClient _mqttcon = new MqttClient("broker.emqx.io");

        //static MqttClient _mqttcon = new MqttClient("broker.emqx.io", 8083, false, _certificate);
        static MqttClient _mqttcon = new MqttClient("broker.emqx.io");

        static MySqlConnection myConnection = new MySqlConnection();
        static bool connected = false;
        DataTable Dt = new DataTable();
        string selectQuery = "select * from Star_Live";
        static bool InternetConnectedFlag = false;

        string[] TopicArray = { "TPCurrentConsumption-StarFasteners", "TPChemicalConsumption-StarFasteners", "TPPartNameSummary-StarFasteners",  "TPAlarmSummaryGraph-StarFasteners", "TPProductionSummary-StarFasteners", "TPZincPlatingLineData-StarFasteners", "TPPokaYokeZincPlatingLineData-StarFasteners", "TPConcentration-StarFasteners", "TPERP-StarFasteners", "TPH2D-StarFasteners", "TPPassivation-StarFasteners", "TP_TotalRecordCount-StarFasteners" };


        //tried
        //static MqttClient _mqttcon = new MqttClient("broker.mqttdashboard.com");


        //static MqttClient _mqttcon = new MqttClient("broker.hivemq.com");
        //Test

        //  MqttClient client = new MqttClient("127.0.0.1", 1883, false, null, MqttSslProtocols.None); // 1883 default

        // Then create the client referencing the certs

        //static MqttClient _mqttcon = new MqttClient("broker.emqx.io", 8083, true, caCert, clientCert, MqttSslProtocols.TLSv1_2);



        public static string JSONData = "";

        public IOTConnection()
        {
            try
            {
                //System.Security.Cryptography.X509Certificates.X509Certificate _certificate = new System.Security.Cryptography.X509Certificates.X509Certificate();
                //_mqttcon = new MqttClient("broker.hivemq.com", 876, true, _certificate);

                //  log.Error("IOTConnection()" + "No" + IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                ErrorLogger.LogError.ErrorLog("IOTConnection()", DateTime.Now.ToString(), "", "", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                //_mqttcon.MqttMsgPublished += Device_MqttMsgPublished;
                //_mqttcon.MqttMsgSubscribed += Device_MqttMsgSubscribed;
                //_mqttcon.MqttMsgPublishReceived += Device_MqttMsgPublishReceived;//subscribe

                _MyDashboardWorker.DoWork += _MyDashboardWorker_DoWork;
                _ThreadDashBoardUpdate.Interval = TimeSpan.FromSeconds(5);
                _ThreadDashBoardUpdate.Tick += _ThreadDashBoardUpdateTrigger;
                _ThreadDashBoardUpdate.Start();
                ConnectDatabaseServer();
                //ConnectMqttServer();
            }
            catch (Exception ex)
            {
                log.Error(" IOTConnection() Contructor" + ex.Message);
            }
        }
        public static void ConnectDatabaseServer()
        {
            try
            {
                SQLHelper.CreateConnection();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConnectDatabaseServer() error", DateTime.Now.ToString(), ex.Message, "", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                log.Error("ConnectMqttServer()" + ex.Message);
                log.Error("ConnectMqttServer() InnerException" + ex.InnerException.ToString());
            }
        }
        public static void ConnectMqttServer()
        {
            try
            {
                string clientId = "StarFastener1";
                _mqttcon.Connect(clientId, null, null, true, MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true, "error", "error", false, 60000);
                log.Error("ConnectMqttServer()" + "Server connected");
            }
            catch (Exception ex)
            {
                log.Error("ConnectMqttServer()" + ex.Message);
                // log.Error("ConnectMqttServer()" , ex.InnerException );
            }
        }

        private static bool chkInternet_Connection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private void _MyDashboardWorker_DoWork(object Sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                //log.Error("_MyDashboardWorker_DoWork()" , "", null, true);
                //if (_mqttcon.IsConnected == true)
                //{

                //}
                //else
                //{
                //    ConnectMqttServer();

                //}
                try
                {
                    InternetConnectedFlag = chkInternet_Connection();// Here we check the internet connection is available or not.
                    if (InternetConnectedFlag == true)
                    {
                    }
                    else
                    {
                        //ErrorLogger.LogError("noInternetConnection");
                        ErrorLogger.LogError.ErrorLog("_MyDashboardWorker_DoWork() noInternetConnection", DateTime.Now.ToString(), "", "", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        for (int i = 0; i <= 4; i++)//here we check the internet connection after every 5 sec
                        {
                            InternetConnectedFlag = chkInternet_Connection();
                            if (InternetConnectedFlag == true)
                            {
                                break;
                            }
                            else
                            {
                                InternetConnectedFlag = false;
                                Thread.Sleep(1000);
                            }
                        }
                        if (InternetConnectedFlag == false)
                        {
                            ////Here we generate Alarm of the internet connection
                            //Query = "update InternetAlarm set Condition='1'";
                            //objDBConnection.ExecuteNonQuery(Query);
                        }
                        else
                        {
                            // Reconnecting to MySQL db
                            try
                            {
                                log.Error("Reconnecting to MySQL DB");
                                ConnectDatabaseServer();
                                //log.Error("Reconnecting to MySQL DB Done Status =" + myConnection.State);
                            }
                            catch { }
                        }
                    }
                }
                catch { }

                try
                {
                    Dt = SQLHelper.serverExcecuteSelectQuery(selectQuery);

                    for (int i = 0; i <= TopicArray.Length - 1; i++)
                    {
                        try
                        {
                            var results = from myRow in Dt.AsEnumerable()
                                          where myRow.Field<string>("Topic_Name") == (TopicArray[i].ToString())
                                          select myRow;
                            if (!results.Any())
                            {
                                string insertQuery = "insert into Star_Live (Topic_Name) values ('" + TopicArray[i].ToString() + "');";
                                IndiSCADADataAccess.DataAccessUpdate.UpdateJsonFile_MySql(insertQuery);
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Error("WhileCreatingTopic " + ex.Message + IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }

                    }
                }
                catch (Exception ex){  }

              //  ErrorLogger.LogError.ErrorLog("_MyDashboardWorker_DoWork() updating records start", DateTime.Now.ToString(), "", "", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                ObservableCollection<HomeEntity> CurrentConsumptionSummaryIOT = IndiSCADAGlobalLibrary.TagList.CurrentConsumptionSummaryIOT;
                if (CurrentConsumptionSummaryIOT != null)
                {
                    string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(CurrentConsumptionSummaryIOT);
                    // log.Error("_MyDashboardWorker_DoWork CurrentConsumptionSummaryIOT =" + jsonString , "", "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    //_mqttcon.Publish("TPCurrentConsumption-StarFasteners", Encoding.UTF8.GetBytes(jsonString), 1, true);
                    IndiSCADADataAccess.DataAccessUpdate.UpdateJsonFile_MySql("update Star_Live set Json_String ='" + jsonString + "' where Topic_Name = 'TPCurrentConsumption-StarFasteners'");
                }

                ObservableCollection<HomeEntity> ChemicalConsumptionSummaryIOT = IndiSCADAGlobalLibrary.TagList.ChemicalConsumptionSummaryIOT;
                if (ChemicalConsumptionSummaryIOT != null)
                {
                    string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(ChemicalConsumptionSummaryIOT);
                    // log.Error("_MyDashboardWorker_DoWork ChemicalConsumptionSummaryIOT =" + jsonString , "", "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    //_mqttcon.Publish("TPChemicalConsumption-StarFasteners", Encoding.UTF8.GetBytes(jsonString), 1, true);
                    IndiSCADADataAccess.DataAccessUpdate.UpdateJsonFile_MySql("update Star_Live set Json_String ='" + jsonString + "' where Topic_Name = 'TPChemicalConsumption-StarFasteners'");
                }
                ObservableCollection<HomeEntity> PartNameSummary = IndiSCADAGlobalLibrary.TagList.PartNameSummaryIOT;
                if (PartNameSummary != null)
                {
                    string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(PartNameSummary);
                    // log.Error("_MyDashboardWorker_DoWork PartNameSummary =" + jsonString , "", "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    //_mqttcon.Publish("TPPartNameSummary-StarFasteners", Encoding.UTF8.GetBytes(jsonString), 1, true);
                    IndiSCADADataAccess.DataAccessUpdate.UpdateJsonFile_MySql("update Star_Live set Json_String ='" + jsonString + "' where Topic_Name = 'TPPartNameSummary-StarFasteners'");
                }


                ObservableCollection<HomeEntity> PartAreaSummary = IndiSCADAGlobalLibrary.TagList.PartAreaSummaryIOT;
                if (PartAreaSummary != null)
                {
                    string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(PartAreaSummary);
                    // log.Error("_MyDashboardWorker_DoWork PartAreaSummary =" + jsonString , "", "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    //_mqttcon.Publish("TPPartAreaSummary-StarFasteners", Encoding.UTF8.GetBytes(jsonString), 1, true);
                    IndiSCADADataAccess.DataAccessUpdate.UpdateJsonFile_MySql("update Star_Live set Json_String ='" + jsonString + "' where Topic_Name = 'TPPartAreaSummary-StarFasteners'");
                }

                ObservableCollection<HomeEntity> AlarmSummaryGraph = IndiSCADAGlobalLibrary.TagList.AlarmSummaryGraphIOT;
                if (AlarmSummaryGraph != null)
                {
                    string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(AlarmSummaryGraph);
                    // log.Error("_MyDashboardWorker_DoWork AlarmSummaryGraph =" + jsonString , "", "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    //_mqttcon.Publish("TPAlarmSummaryGraph-StarFasteners", Encoding.UTF8.GetBytes(jsonString), 1, true);
                    IndiSCADADataAccess.DataAccessUpdate.UpdateJsonFile_MySql("update Star_Live set Json_String ='" + jsonString + "' where Topic_Name = 'TPAlarmSummaryGraph-StarFasteners'");
                }
                

                HomeEntity _DataProduction = IndiSCADAGlobalLibrary.TagList.ProductionDetails;
                _DataProduction.SendingDate = DateTime.Now.ToString();
                _DataProduction.UpdationDateTime = DateTime.Now.ToString();

                if (_DataProduction != null)
                {
                    string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(_DataProduction);
                    //string jsonString = "{\"ShiftNo\":null,\"DownTime\":30,\"SemiDownTime\":0,\"ManualDownTime\":0,\"MaintenanceDownTime\":0,\"CompleteDownTime\":0,\"AutoTotalDownTime\":0,\"SemiTotalDownTime\":0,\"ManualTotalDownTime\":0,\"MaintenanceTotalDownTime\":0,\"CompleteTotalDownTime\":0,\"AutoLiveDowntimeShift\":null,\"SemiLiveDowntimeShift\":null,\"ManualLiveDowntimeShift\":null,\"MaintainanceLiveDowntimeShift\":null,\"CompleteLiveDowntimeShift\":null,\"ShiftNum\":null,\"AutoPreviousDowntimeShift\":null,\"SemiPreviousDowntimeShift\":null,\"ManualPreviousDowntimeShift\":null,\"MaintenancePreviousDowntimeShift\":null,\"CompletePreviousDowntimeShift\":null,\"TotalDownTime\":0,\"SendingDate\":null,\"HDEProcess\":null,\"AlarmName\":null,\"AlarmONCount\":0,\"AlarmCount\":0,\"AlarmNotACKCount\":0,\"PartName\":null,\"PartNameCount\":0,\"TotalQuantityCount\":0,\"TotalLoadCount\":0,\"Shift1QuantityCount\":0,\"Shift2QuantityCount\":0,\"Shift3QuantityCount\":0,\"Shift1LoadCount\":0,\"Shift2LoadCount\":0,\"Shift3LoadCount\":0,\"ChemicalName\":null,\"ChemicalConsumptionCount\":0,\"RectifierStnName\":null,\"CurrentConsumptionCount\":0,\"AvgCycleTime\":\"\",\"PlantAM\":null,\"CycleStartStop\":null,\"W1AM\":null,\"W2AM\":null,\"W3AM\":null,\"W4AM\":null,\"W5AM\":null,\"W6AM\":null,\"W7AM\":null,\"W8AM\":null,\"ActualCycleTime\":\"\",\"ONAlarmCount\":\"21\",\"NotACKAlarmCount\":\"0\",\"setCycleTime\":\"\",\"LastCycleTime\":\"\",\"UpdationDateTime\":null,\"PlantStatus\":null,\"PartArea\":0,\"Shift1PartArea\":0,\"Shift2PartArea\":0,\"Shift3PartArea\":0,\"ShiftDate\":\"2021-09-27\",\"DegreasingTemp\":null,\"DescalingTemp\":null,\"AnodicCleaningTemp\":null,\"AnodicCleaningCurrent\":null,\"ZincPlatingTemp\":null,\"PlatingTime\":null,\"ZincPlatingCurrent\":null,\"TrivalentPassivationPH\":null,\"DMWaterConductivity\":null,\"TriPassivationTemp\":null,\"SealantTemp\":null,\"SealerPH\":null,\"ConveyorOven\":null,\"OvenTime\":null,\"PokayokeLoading\":null,\"PokayokeDegreasing\":null,\"PokayokeAnodicCleaning\":null,\"PokayokeAcidPickling\":null,\"PokayokeZnPlating1\":null,\"PokayokeZnPlating2\":null,\"PokayokeZnPlating3\":null,\"PokayokeNitricDip\":null,\"PokayokeTrivalentPassivation1\":null,\"PokayokeTrivalentPassivation2\":null,\"PokayokeSealant1\":null,\"PokayokeSealant2\":null,\"ZincConcentration\":null,\"CausticSodaConcentration\":null,\"ERPInwardQuantity\":null,\"ERPRunningQuantity\":null,\"ERPCompletedQuantity\":null,\"ERPBalancedQuantity\":null,\"Quantity\":0,\"PassivationSilver\":null,\"PassivationYellow\":null,\"PassivationBlack\":null,\"PassivationZincIron\":null}";
                    // log.Error("_MyDashboardWorker_DoWork TPProductionSummary = " + jsonString , "", "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    //_mqttcon.Publish("TPProductionSummary-StarFasteners", Encoding.UTF8.GetBytes(jsonString), 1, true);
                    IndiSCADADataAccess.DataAccessUpdate.UpdateJsonFile_MySql("update Star_Live set Json_String ='" + jsonString + "' where Topic_Name = 'TPProductionSummary-StarFasteners'");

                }


                ObservableCollection<TankDetailsEntity> _ZincPlatingLineData = IndiSCADAGlobalLibrary.TagList.ZincPlatingLine;
                if (_ZincPlatingLineData != null)
                {
                    string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(_ZincPlatingLineData);
                    // log.Error("_MyDashboardWorker_DoWork ZincPlatingLineData =" + jsonString , "", "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    //_mqttcon.Publish("TPZincPlatingLineData-StarFasteners", Encoding.UTF8.GetBytes(jsonString), 1, true);
                    IndiSCADADataAccess.DataAccessUpdate.UpdateJsonFile_MySql("update Star_Live set Json_String ='" + jsonString + "' where Topic_Name = 'TPZincPlatingLineData-StarFasteners'");
                }

                //HomeEntity _HistoryZincPlatingLineData = IndiSCADAGlobalLibrary.TagList.HistoryZincPlatingData;
                //if (_HistoryZincPlatingLineData != null)
                //{
                //    string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(_HistoryZincPlatingLineData);
                //    log.Error("_MyDashboardWorker_DoWork HistoryZincPlatingData =" + jsonString , "", "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                //    _mqttcon.Publish("TPZincPlatingHistoryData-StarFasteners", Encoding.UTF8.GetBytes(jsonString), 1, true);
                //}
                try
                {
                    RecordCountEntity _TotalRecordData = GetTotalRecordCount();//IndiSCADAGlobalLibrary.TagList.TotalRecordData;
                    if (_TotalRecordData != null)
                    {
                        _TotalRecordData.SendingDate = DateTime.Now.ToString();
                        string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(_TotalRecordData);
                        log.Error("_MyDashboardWorker_DoWork _TotalRecordData Json =  " + jsonString);
                        //_mqttcon.Publish("TPPokaYokeZincPlatingLineData-StarFasteners", Encoding.UTF8.GetBytes(jsonString), 1, true);
                        IndiSCADADataAccess.DataAccessUpdate.UpdateJsonFile_MySql("update Star_Live set Json_String ='" + jsonString + "' where Topic_Name = 'TP_TotalRecordCount-StarFasteners'");
                        log.Error("_MyDashboardWorker_DoWork _TotalRecordData Query =update Star_Live set Json_String ='" + jsonString + "' where Topic_Name = 'TP_TotalRecordCount-StarFasteners'");
                    }
                }
                catch(Exception ex) { log.Error("_MyDashboardWorker_DoWork _TotalRecordData Json error =  " + ex.Message); }

                HomeEntity _PokaYokeZincPlatingLineData = IndiSCADAGlobalLibrary.TagList.PokaYokeZincPlatingLine;
                if (_PokaYokeZincPlatingLineData != null)
                {
                    string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(_PokaYokeZincPlatingLineData);
                    // log.Error("_MyDashboardWorker_DoWork PokaYokeZincPlatingLineData = " + jsonString , "", "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    //_mqttcon.Publish("TPPokaYokeZincPlatingLineData-StarFasteners", Encoding.UTF8.GetBytes(jsonString), 1, true);
                    IndiSCADADataAccess.DataAccessUpdate.UpdateJsonFile_MySql("update Star_Live set Json_String ='" + jsonString + "' where Topic_Name = 'TPPokaYokeZincPlatingLineData-StarFasteners'");

                    log.Error("_MyDashboardWorker_DoWork _TotalRecordData Query =update Star_Live set Json_String ='" + jsonString + "' where Topic_Name = 'TPPokaYokeZincPlatingLineData-StarFasteners'");
                }
                //try
                //{
                //    DataTable ChemConcentrationDT =SettingLogic.GetChemConcentration();
                //    IndiSCADAGlobalLibrary.TagList.ParameterConcentrationData.ZincConcentration = ChemConcentrationDT.Rows[0]["Zinc"].ToString();//IndiSCADAGlobalLibrary.TagList.ConcentrationValue[0];
                //    IndiSCADAGlobalLibrary.TagList.ParameterConcentrationData.CausticSodaConcentration = ChemConcentrationDT.Rows[0]["CausticSoda"].ToString();//IndiSCADAGlobalLibrary.TagList.ConcentrationValue[1];
                //    log.Error("HomeViewModel DoWork() ZincParameterval" , ChemConcentrationDT.Rows[0]["Zinc"].ToString(), "No", true);
                //    log.Error("HomeViewModel DoWork() CausticSodaParameterval" , ChemConcentrationDT.Rows[0]["CausticSoda"].ToString(), "No", true);
                //}
                //catch (Exception ex)
                //{
                //    log.Error("HomeViewModel DoWork() chemical concentration values" +ex.Message, "No", true);
                //}

                HomeEntity _ConcentrationData = IndiSCADAGlobalLibrary.TagList.ParameterConcentrationData;
                if (_ConcentrationData != null)
                {
                    string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(_ConcentrationData);
                    //log.Error("_MyDashboardWorker_DoWork ConcentrationData = " + jsonString , "", "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    //_mqttcon.Publish("TPConcentration-StarFasteners", Encoding.UTF8.GetBytes(jsonString), 1, true);
                    IndiSCADADataAccess.DataAccessUpdate.UpdateJsonFile_MySql("update Star_Live set Json_String ='" + jsonString + "' where Topic_Name = 'TPConcentration-StarFasteners'");
                }

                HomeEntity _ERPData = IndiSCADAGlobalLibrary.TagList.ERPData;
                if (_ERPData != null)
                {
                    string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(_ERPData);
                    // log.Error("_MyDashboardWorker_DoWork_ERPData = " + jsonString , "", "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    //_mqttcon.Publish("TPERP-StarFasteners", Encoding.UTF8.GetBytes(jsonString), 1, true);
                    IndiSCADADataAccess.DataAccessUpdate.UpdateJsonFile_MySql("update Star_Live set Json_String ='" + jsonString + "' where Topic_Name = 'TPERP-StarFasteners'");
                }


                ObservableCollection<HomeEntity> _H2DData = IndiSCADAGlobalLibrary.TagList.H2DData;
                if (_H2DData != null)
                {
                    string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(_H2DData);
                    // log.Error("_MyDashboardWorker_DoWork_H2DData = " + jsonString , "", "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    //_mqttcon.Publish("TPH2D-StarFasteners", Encoding.UTF8.GetBytes(jsonString), 1, true);
                    IndiSCADADataAccess.DataAccessUpdate.UpdateJsonFile_MySql("update Star_Live set Json_String ='" + jsonString + "' where Topic_Name = 'TPH2D-StarFasteners'");
                }


                HomeEntity _Passivationata = IndiSCADAGlobalLibrary.TagList.PassivationData;
                if (_Passivationata != null)
                {
                    string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(_Passivationata);
                    // log.Error("_MyDashboardWorker_DoWork_Passivationata = " + jsonString , "", "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    //_mqttcon.Publish("TPPassivation-StarFasteners", Encoding.UTF8.GetBytes(jsonString), 1, true);
                    IndiSCADADataAccess.DataAccessUpdate.UpdateJsonFile_MySql("update Star_Live set Json_String ='" + jsonString + "' where Topic_Name = 'TPPassivation-StarFasteners'");
                }

                //HomeEntity _UpdationDateTime = IndiSCADAGlobalLibrary.TagList.DateTimeDetails;
                //if (_UpdationDateTime != null)
                //{
                //    string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(_UpdationDateTime);
                //    _mqttcon.Publish("TPPlantStatus", Encoding.UTF8.GetBytes(jsonString), 1, true);
                //}
            }
            catch (Exception ex)
            {
                log.Error("_MyDashboardWorker_DoWork" + ex.Message + IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        void _ThreadDashBoardUpdateTrigger(object sender, EventArgs e)
        {
            try
            {
                if (_MyDashboardWorker.IsBusy != true)
                {
                    _MyDashboardWorker.RunWorkerAsync();
                }
            }
            catch (Exception exThreadDashBoardUpdateTrigger)
            {
                log.Error("_ThreadDashBoardUpdateTrigger" + exThreadDashBoardUpdateTrigger.Message + IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        void Device_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            try
            {
                log.Error("Device_MqttMsgPublishReceived()" + Encoding.UTF8.GetString(e.Message) + IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                //Console.WriteLine(Encoding.UTF8.GetString(e.Message));
            }
            catch (Exception ex)
            {
                log.Error("Device_MqttMsgPublishReceived" + ex.Message + "No" + IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        void Device_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            try
            {

            }
            catch (Exception error)
            {
                log.Error("Device_MqttMsgPublished()" + error.Message + "No" + IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        void Device_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            try
            {
                //Console.WriteLine(Encoding.UTF8.GetString(e.ToString()));
            }
            catch (Exception error)
            {
                log.Error("Device_MqttMsgSubscribed()" + error.Message + "No" + IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }

        public static RecordCountEntity GetTotalRecordCount()
        {
            //Get alarm summary
            RecordCountEntity _result = new RecordCountEntity();
            try
            {
                DataTable AlarmCount = IndiSCADATranslation.HomeViewTranslation.GetTotalRecordCount("AlarmRecordCount").Response;
                DataTable EventCount = IndiSCADATranslation.HomeViewTranslation.GetTotalRecordCount("EventCount").Response;
                DataTable LoadDataCount = IndiSCADATranslation.HomeViewTranslation.GetTotalRecordCount("LoadDataCount").Response;
                DataTable LoadDipTimeCount = IndiSCADATranslation.HomeViewTranslation.GetTotalRecordCount("LoadDiptimeCount").Response;
                DataTable LoadPartDetailsCount = IndiSCADATranslation.HomeViewTranslation.GetTotalRecordCount("LoadPartDetailsCount").Response;
              //  DataTable DowntimeCount = IndiSCADATranslation.HomeViewTranslation.GetTotalRecordCount("ShiftWiseDownTimeSummaryCount").Response;
                //DataTable TrendCurrentCount = IndiSCADATranslation.HomeViewTranslation.GetTotalRecordCount("TrendCurrentDataCount").Response;
                //DataTable TrendTempCount = IndiSCADATranslation.HomeViewTranslation.GetTotalRecordCount("TrendTempDataCount").Response;

                if (AlarmCount != null)
                {
                    double AlarmCt = Convert.ToDouble(AlarmCount.Rows[0][0].ToString());
                    double EventCt = Convert.ToDouble(EventCount.Rows[0][0].ToString());
                    double LoadDataCt = Convert.ToDouble(LoadDataCount.Rows[0][0].ToString());
                    double LoadDipCt = Convert.ToDouble(LoadDipTimeCount.Rows[0][0].ToString());
                    double PartCt = Convert.ToDouble(LoadPartDetailsCount.Rows[0][0].ToString());
                    //  double DowntimeCt = Convert.ToDouble(DowntimeCount.Rows[0][0].ToString());
                    //double TCurrentCt = Convert.ToDouble(TrendCurrentCount.Rows[0][0].ToString());
                    //double TTempCt = Convert.ToDouble(TrendTempCount.Rows[0][0].ToString());

                    string Total_Records = (AlarmCt + EventCt + LoadDataCt + LoadDipCt + PartCt).ToString() ; // + TCurrentCt + TTempCt).ToString();

                    _result.Total_Records = Total_Records;
                }
                //  log.Error("HomeBusinessLogic GetAlarmTotalCount()", DateTime.Now.ToString(), _TranslationOP.Response.AlarmONCount.ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                //if (_TranslationOP.Response != null)
                //{
                //    if (_TranslationOP.Status == ResponseType.S)
                //    {
                //        //  IList<HomeEntity> _conversionList = (IList<HomeEntity>)(_TranslationOP.Response);
                //        _result = _TranslationOP.Response;
                //    }
                //}
            }
            catch (Exception ex)
            {
                log.Error("HomeBusinessLogic GetTotalRecordCount()" + ex.Message + IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }
    }

    internal class MySqlConnection
    {
    }
}
