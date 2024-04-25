using System;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using IndiSCADAEntity.Entity;
using IndiSCADAGlobalLibrary;
//using MitsubishiComm;
using OmronComm;

namespace DeviceCommunication
{
    public static class CommunicationWithPLC
    {

        #region"Declaration"
        static DispatcherTimer DispatchTimerCommWithPLC = new DispatcherTimer();
        static bool isApplicationStartUp = false;

        //public static MitsubishiComm.frmCommunication ReadWritePlcValue = new frmCommunication(3);
        public static OmronComm.frmCommunication ReadWritePlcValue = new OmronComm.frmCommunication();
        static System.ComponentModel.BackgroundWorker _BackgroundWorkerCommWithPLC = new System.ComponentModel.BackgroundWorker();

        public static ObservableCollection<WagonLoadStringEntity> WagonDetails = new ObservableCollection<WagonLoadStringEntity>();
        public static string[] PrevWagon1DataLog = new string[] { };
        public static string[] PrevWagon2DataLog = new string[] { };
        public static string[] PrevWagon3DataLog = new string[] { };
        public static string[] PrevWagon4DataLog = new string[] { };
        public static string[] PrevWagon5DataLog = new string[] { };
        public static string[] PrevWagon6DataLog = new string[] { };
        public static string[] PrevWagon7DataLog = new string[] { };
        //public static string[] PrevWagon8DataLog = new string[] { };


        #endregion
        #region "Property"
        #endregion
        #region Public/Private Method
        private static void DoWork(object Sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //string[] WagonDataLog = { "1","1","1","21","5","28","10","43","29","","","528","1"};
            //ProductionDataLog(WagonDataLog, "Wagon1");

            ErrorLogger.LogError.ErrorLog("CommunicationWithPLC DoWork() Started", DateTime.Now.ToString(), IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected.ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

            //  FetchOutOfRangeValues(30, "2020/9/29-26");
            //string[] a = new string[] { "6", "6" ,"1"};
            //WagonGetEventDataLog("2020/9/29-26",Convert.ToDateTime("2020-10-08 16:02:25.000"),a );

            //DataTable dt = (IndiSCADADataAccess.DataAccessSelect.getDataNextLoadMasterSettingsForDataLogging()).Response;
            //DataTable dtPartData = (IndiSCADADataAccess.DataAccessSelect.getDataPartMasterForDataLogging()).Response;
            //ServiceResponse<int> _InsertResult = (IndiSCADADataAccess.DataAccessInsert.insertLoadNoToLoadPartDetails("2020/9/18-13", dt, dtPartData));

            try
            {

                //check PLC communication
                IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected =   ReadWritePlcValue.getConnectionStatus();
                if(IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected==false)
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLC DoWork() getConnectionStatus IsPLCConnected==false", DateTime.Now.ToString(), IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected.ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                    ReadWritePlcValue.doConnect();
                }
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC DoWork() getConnectionStatus readed " + IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected.ToString(), DateTime.Now.ToString(),"", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                try
                {
                    //Wagon1 data log
                    ProductionDataLog(IndiSCADAGlobalLibrary.TagList.W1DataLog, "Wagon1");
                    //Wagon2 data log
                    ProductionDataLog(IndiSCADAGlobalLibrary.TagList.W2DataLog, "Wagon2");
                    //Wagon3 data log
                    ProductionDataLog(IndiSCADAGlobalLibrary.TagList.W3DataLog, "Wagon3");
                    //Wagon4 data log
                    ProductionDataLog(IndiSCADAGlobalLibrary.TagList.W4DataLog, "Wagon4");
                    //Wagon5 data log
                    ProductionDataLog(IndiSCADAGlobalLibrary.TagList.W5DataLog, "Wagon5");
                    //Wagon6 data log
                    ProductionDataLog(IndiSCADAGlobalLibrary.TagList.W6DataLog, "Wagon6");
                    //Wagon5 data log
                    ProductionDataLog(IndiSCADAGlobalLibrary.TagList.W7DataLog, "Wagon7");
                    //Wagon6 data log
                    //ProductionDataLog(IndiSCADAGlobalLibrary.TagList.W8DataLog, "Wagon8");
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLC DoWork() ProductionDataLog", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                }

                ReadPLCTagValues();

            }
            catch (Exception exThreadDashBoardUpdateTrigger)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC DoWork()", DateTime.Now.ToString(), exThreadDashBoardUpdateTrigger.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        private static void DispatcherTickEvent(object sender, EventArgs e)
        {
            try
            {
                if (_BackgroundWorkerCommWithPLC.IsBusy != true)
                {
                    _BackgroundWorkerCommWithPLC.RunWorkerAsync();
                }
            }
            catch (Exception exDispatcherTickEvent)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC DispatcherTickEvent()", DateTime.Now.ToString(), exDispatcherTickEvent.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        //Start background worker to read plc tag and data log
        public static void StartPlcComminicationAndDataLog()
        {
            try
            {
                IndiSCADAGlobalLibrary.TagList.SettingScreenContinuousTagRead = true;

                ReadWritePlcValue.doConnect();
                isApplicationStartUp = true;//Apploication Startup bit on 
                _BackgroundWorkerCommWithPLC.DoWork += DoWork;
                DispatchTimerCommWithPLC.Interval = TimeSpan.FromMilliseconds(500);
                DispatchTimerCommWithPLC.Tick += DispatcherTickEvent;
                DispatchTimerCommWithPLC.Start();
            }
            catch (Exception exStartPlcComminicationAndDataLog)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC StartPlcComminicationAndDataLog()", DateTime.Now.ToString(), exStartPlcComminicationAndDataLog.Message, null, true);
            }
        }

        #region"PLC Tag Read"

        public static void ReadPLCTagValues()
        {
            try
            {
                if (IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected)
                {
                    if (isApplicationStartUp)//if application started first time read tags which are not required to read continue
                    {
                        ApplicationStartUpTagRead();
                        InitialisePartData();
                        isApplicationStartUp = false;
                    }
                    //read tags continue alarm, data log , temperature,events , rectifier and dosing etc
                    //ContinuousTagRead();
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ReadPLCTagValues()", DateTime.Now.ToString(), "PLC communication failed.", null, true);
                }
            }
            catch (Exception exReadPLCTagValues)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ReadPLCTagValues()", DateTime.Now.ToString(), exReadPLCTagValues.Message, null, true);
            }
        }

        // On application startup initialise the part name and df number values for all stations
        private static void InitialisePartData()
        {
            try
            {
                if (TagList.LoadNoAtStation != null)
                {
                    if (TagList.LoadNoAtStation.Length > 0)
                    {
                        ErrorLogger.LogError.ErrorLog("InitialisePartData started", DateTime.Now.ToString(), "", null, true);

                        int index = 0;
                        foreach (string lno in TagList.LoadNoAtStation)
                        {
                            try
                            {
                                int loadnoatstation = Convert.ToInt32(lno);
                                if (loadnoatstation > 0)
                                {
                                    string loadnumber = GetLoadNoInitialise(loadnoatstation, index);
                                    string Runningpart = "NA"; string RunningMECLno = "NA";
                                    ServiceResponse<DataTable> QueryResult = IndiSCADADataAccess.DataAccessSelect.SelectPartNumberFromLoadPartDetails(loadnumber);
                                    if (QueryResult.Status == ResponseType.E)//query excuted with some erros
                                    {
                                        IndiSCADAGlobalLibrary.TagList.PartNameAtStation[index] = "NA";
                                        IndiSCADAGlobalLibrary.TagList.MECLnumberAtStation[index] = "NA";
                                        ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(" + loadnumber + ")", DateTime.Now.ToString(), QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                                    }
                                    else
                                    {
                                        DataTable dtparts = QueryResult.Response;
                                        if (dtparts != null)
                                        {
                                            if (dtparts.Rows.Count > 0)
                                            {
                                                Runningpart = dtparts.Rows[0]["PartNumber"].ToString();
                                                try
                                                {
                                                    for (int i = 1; i <= dtparts.Rows.Count - 1; i++)
                                                    {
                                                        Runningpart += "-" + dtparts.Rows[i]["PartNumber"].ToString();
                                                    }
                                                }
                                                catch (Exception ex)
                                                { ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdatePartNumberEntryOnputEvent() error while assigning Runningpart", DateTime.Now.ToString(), ex.Message, null, true); }

                                                //try
                                                //{
                                                //    RunningMECLno = dtparts.Rows[0]["MECLNumber"].ToString();
                                                //}
                                                //catch (Exception ex) { ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdatePartNumberEntryOnputEvent() error while assigning MECL no", DateTime.Now.ToString(), ex.Message, null, true); }

                                            }
                                        }
                                        IndiSCADAGlobalLibrary.TagList.PartNameAtStation[index] = Runningpart;
                                        //IndiSCADAGlobalLibrary.TagList.MECLnumberAtStation[index] = RunningMECLno;
                                    }
                                }
                                else
                                {
                                    IndiSCADAGlobalLibrary.TagList.PartNameAtStation[index] = "NA";
                                    //IndiSCADAGlobalLibrary.TagList.MECLnumberAtStation[index] = "NA";
                                }

                                index++;

                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC InitialistPartData() error", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                    }
                }
            }
            catch (Exception exInitialistPartData)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC InitialistPartData()", DateTime.Now.ToString(), exInitialistPartData.Message, null, true);
            }
        }
        //Get Load No and Date time of event
        private static string GetLoadNoInitialise(int loadno, int index)
        {
            try
            {

                StringBuilder LoadNumber = new StringBuilder();
                //LoadNumber.Append("20" + WagonDataLog[3] + "/");//Year 2020/

                //LoadNumber.Append("20" + IndiSCADAGlobalLibrary.TagList.YYAtStation[index] + "/");//Year 2020/ commented by sbs temperary

                string Year = DateTime.Now.Year.ToString();
                LoadNumber.Append(Year+"/");//LoadNumber.Append("2021/");

                LoadNumber.Append(IndiSCADAGlobalLibrary.TagList.MMDDAtStation[index].Substring(0, TagList.MMDDAtStation[index].Length - 2) + "/");//Month 2020/4/
                LoadNumber.Append(IndiSCADAGlobalLibrary.TagList.MMDDAtStation[index].Substring(IndiSCADAGlobalLibrary.TagList.MMDDAtStation[index].Length - 2) + "-");//Day 2020/4/1
                LoadNumber.Append(loadno);//Date 2020/4/16-1
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC GetLoadNoInitialise() " + loadno, DateTime.Now.ToString(), LoadNumber.ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return LoadNumber.ToString();
            }
            catch (Exception exProductionDataLog)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC GetLoadNoInitialise() " + loadno, DateTime.Now.ToString(), exProductionDataLog.Message, null, true);
                return null;
            }
        }

        //On application Start Up read address. this will read only single time
        private static void ApplicationStartUpTagRead()
        {
            try
            {
                //Tag Id 3----------------------------------------------------------------------------------------------  
                IndiSCADAGlobalLibrary.TagList.LoadNoAtStation = ReadPLCTagValue("LoadNumberatStationArrayLoadNumber");//Tag ID 3//LoadatStationArray1ForHangerPresent0ForHangerAbsent

                IndiSCADAGlobalLibrary.TagList.MMDDAtStation = ReadPLCTagValue("MMDDatStationMMDDatStation");//Tag ID 

                IndiSCADAGlobalLibrary.TagList.YYAtStation = ReadPLCTagValue("YYatStationYYatStation");//Tag ID 

                if (IndiSCADAGlobalLibrary.TagList.SettingScreenContinuousTagRead == true)
                {
                    IndiSCADAGlobalLibrary.TagList.ManualCurrent = ReadPLCTagValue("RectifierManualCurrent");
                    IndiSCADAGlobalLibrary.TagList.RectifierHighSP = ReadPLCTagValue("RectifierHighSP");
                    IndiSCADAGlobalLibrary.TagList.RectifierLowSP = ReadPLCTagValue("RectifierLowSP");
                    IndiSCADAGlobalLibrary.TagList.AutoManual = ReadPLCTagValue("RECTIFIERAutoOrManual");
                    IndiSCADAGlobalLibrary.TagList.ManulaONOFF = ReadPLCTagValue("RECTIFIERManualONOrOFF");
                    IndiSCADAGlobalLibrary.TagList.RectifierAlarmStatus = ReadPLCTagValue("RECTIFIERAlarmStatus");
                    IndiSCADAGlobalLibrary.TagList.ResetAmpHr = ReadPLCTagValue("RECTIFIERResetCumulativeAmpHr");
                    IndiSCADAGlobalLibrary.TagList.RectifierCumulativeAmpPerHr = Real_ReadPLCTagValue("RectifierCumulativeAmpHr");
                    IndiSCADAGlobalLibrary.TagList.RectifierAmpHr = Real_ReadPLCTagValue("RectifierAmpHr");
                    IndiSCADAGlobalLibrary.TagList.Calculatedcurrent = ReadPLCTagValue("RectifierCalculatedCurrentOrAutoCurrent");

                    IndiSCADAGlobalLibrary.TagList.TempHigh = ReadPLCTagValue("TempControllerTemperatureHighSetPoint");
                    IndiSCADAGlobalLibrary.TagList.TempLow = ReadPLCTagValue("TempControllerTemperatureLowSetPoint");
                    IndiSCADAGlobalLibrary.TagList.TempSetpt = ReadPLCTagValue("TempControllerTemperatureControllerSetPoint");

                    IndiSCADAGlobalLibrary.TagList.DosingAutoManual = ReadPLCTagValue("DOSINGAutoOrManual");                //Get DosingManualONOFF from PLC   
                    IndiSCADAGlobalLibrary.TagList.DosingManualOffOn = ReadPLCTagValue("DOSINGManualOffOrOn");
                    IndiSCADAGlobalLibrary.TagList.DosingTimeFlowrate = ReadPLCTagValue("DOSINGTimerBasedOrFlowrateBased");
                    IndiSCADAGlobalLibrary.TagList.DosingOnOrOffStatus = ReadPLCTagValue("DOSINGOnOrOffStatus");
                    IndiSCADAGlobalLibrary.TagList.DosingQuantity = Real_ReadPLCTagValue("DosingQuantityInml");
                    IndiSCADAGlobalLibrary.TagList.DosingFlowRate = Real_ReadPLCTagValue("DosingFlowRatemlperSec");
                    IndiSCADAGlobalLibrary.TagList.DosingSetAmpHr = Real_ReadPLCTagValue("DosingSetAmpHr");
                    IndiSCADAGlobalLibrary.TagList.DosingActualAmpHr = Real_ReadPLCTagValue("DosingActualAmpHr");
                    IndiSCADAGlobalLibrary.TagList.DosingRemainingTime = ReadPLCTagValue("DosingRemainingTimeInSec");
                    IndiSCADAGlobalLibrary.TagList.DosingTimeInSec = ReadPLCTagValue("DosingTimeInSec");
                    IndiSCADAGlobalLibrary.TagList.DosingCumulativeAmpHr = Real_ReadPLCTagValue("DosingCumulativeAmpHr");//dINT  
                    IndiSCADAGlobalLibrary.TagList.SetPH = DivideBy(ReadPLCTagValue("DosingSetPH"), 100);
                    IndiSCADAGlobalLibrary.TagList.DosingpHActual = DivideBy(ReadPLCTagValue("DosingActPH"), 100);

                    IndiSCADAGlobalLibrary.TagList.pHHigh = DivideBy(ReadPLCTagValue("pHMeterpHHighsetPoint"), 100, 5);
                    IndiSCADAGlobalLibrary.TagList.pHLow = DivideBy(ReadPLCTagValue("pHMeterpHLowsetPoint"), 100, 5);

                    IndiSCADAGlobalLibrary.TagList.Wagon1WCSInput = Word_ReadPLCTagValue("Wagon1WCSInput");
                    IndiSCADAGlobalLibrary.TagList.Wagon2WCSInput = Word_ReadPLCTagValue("Wagon2WCSInput");
                    IndiSCADAGlobalLibrary.TagList.Wagon3WCSInput = Word_ReadPLCTagValue("Wagon3WCSInput");
                    IndiSCADAGlobalLibrary.TagList.Wagon4WCSInput = Word_ReadPLCTagValue("Wagon4WCSInput");
                    IndiSCADAGlobalLibrary.TagList.Wagon5WCSInput = Word_ReadPLCTagValue("Wagon5WCSInput");
                    IndiSCADAGlobalLibrary.TagList.Wagon6WCSInput = Word_ReadPLCTagValue("Wagon6WCSInput");
                    IndiSCADAGlobalLibrary.TagList.Wagon7WCSInput = Word_ReadPLCTagValue("Wagon6WCSInput");

                    IndiSCADAGlobalLibrary.TagList.TankBypass = ReadPLCTagValue("BypassplatingTankBypassPlating");
                    IndiSCADAGlobalLibrary.TagList.TrayBypass = ReadPLCTagValue("TrayBypassBypass");

                    IndiSCADAGlobalLibrary.TagList.FilterPumpOnOFF = ReadPLCTagValue("FilterPumpOnOFF");
                    IndiSCADAGlobalLibrary.TagList.FilterPumpInputCBTrip = ReadPLCTagValue("FilterPumpInputCBTrip");
                    IndiSCADAGlobalLibrary.TagList.FilterPumpOutput = ReadPLCTagValue("FilterPumpOutput");

                    IndiSCADAGlobalLibrary.TagList.DryerBypass = ReadPLCTagValue("DryerBypass");

                    IndiSCADAGlobalLibrary.TagList.OilSkimmerAutoManual = ReadPLCTagValue("OilSkimmerAutoManual");
                    IndiSCADAGlobalLibrary.TagList.OilSkimmerTrip = ReadPLCTagValue("OilSkimmerTrip");
                    IndiSCADAGlobalLibrary.TagList.OilSkimmerOutput = ReadPLCTagValue("OilSkimmerOutput");

                    IndiSCADAGlobalLibrary.TagList.BaseBarrelMotorONOFF = ReadPLCTagValue("BaseBarrelMotorONOFF");
                    IndiSCADAGlobalLibrary.TagList.BaseBarrelMotorTrip = ReadPLCTagValue("BaseBarrelMotorTrip");
                    IndiSCADAGlobalLibrary.TagList.BaseBarrelMotorStatus = ReadPLCTagValue("BaseBarrelMotorStatus");


                    IndiSCADAGlobalLibrary.TagList.SettingScreenContinuousTagRead = false;

                }

            }
            catch (Exception exApplicationStartUpTagRead)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ApplicationStartUpTagRead()", DateTime.Now.ToString(), exApplicationStartUpTagRead.Message, null, true);

            }


        #region downtime HHMM
            try
            {
                //read HH MM for shift from PLC address
                //shift1_Start_Hour,shift1_Start_Min,shift1_End_Hour,shift1_End_Min
                string[] ShiftHHMM = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("ShiftHHMM"); //12  //{ "8","01","4","2", "14", "50", "4", "2", "14", "50", "4", "2", };//

                //Read shift time from database
                //SELECT * From [dbo].[ShiftMaster] where IsNewEntry=0 order by ShiftNo asc
                string ShiftNo = "";
                ServiceResponse<DataTable> QueryResult = IndiSCADADataAccess.DataAccessSelect.SelectShiftTimeDetails();
                DataTable dt = QueryResult.Response;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ShiftNo = dt.Rows[i]["ShiftNo"].ToString();

                    DateTime StartTime = Convert.ToDateTime(dt.Rows[i]["ShiftStartTime"].ToString().Trim());
                    DateTime EndTime = Convert.ToDateTime(dt.Rows[i]["ShiftEndTime"].ToString().Trim());

                    if (dt.Rows[i]["ShiftNo"].ToString().Trim() == "1")
                    {
                        //start Date
                        if (StartTime.Hour.ToString().Trim() != ShiftHHMM[0] || StartTime.Minute.ToString().Trim() != ShiftHHMM[1])
                        {
                            //HH
                            string TagName11 = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName("ShiftHHMM", 0);
                            WriteValues(TagName11, StartTime.Hour.ToString().Trim(), "int");

                            //MM
                            string TagName1 = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName("ShiftHHMM", 1);
                            WriteValues(TagName1, StartTime.Minute.ToString().Trim(), "int");
                        }
                        //End Date
                        if (EndTime.Hour.ToString().Trim() != ShiftHHMM[2] || EndTime.Minute.ToString().Trim() != ShiftHHMM[3])
                        {
                            //HH
                            string TagName3 = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName("ShiftHHMM", 2);
                            WriteValues(TagName3, EndTime.Hour.ToString().Trim(), "int");

                            //MM
                            string TagName1 = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName("ShiftHHMM", 3);
                            WriteValues(TagName1, EndTime.Minute.ToString().Trim(), "int");
                        }
                    }
                    else if (dt.Rows[i]["ShiftNo"].ToString().Trim() == "2")
                    {
                        //start Date
                        if (StartTime.Hour.ToString().Trim() != ShiftHHMM[4] || StartTime.Minute.ToString().Trim() != ShiftHHMM[5])
                        {
                            //HH
                            string TagName5 = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName("ShiftHHMM", 4);
                            WriteValues(TagName5, StartTime.Hour.ToString().Trim(), "int");

                            //MM
                            string TagName1 = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName("ShiftHHMM", 5);
                            WriteValues(TagName1, StartTime.Minute.ToString().Trim(), "int");
                        }
                        //End Date
                        if (EndTime.Hour.ToString().Trim() != ShiftHHMM[6] || EndTime.Minute.ToString().Trim() != ShiftHHMM[7])
                        {
                            //HH
                            string TagName2 = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName("ShiftHHMM", 6);
                            WriteValues(TagName2, EndTime.Hour.ToString().Trim(), "int");

                            //MM
                            string TagName1 = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName("ShiftHHMM", 7);
                            WriteValues(TagName1, EndTime.Minute.ToString().Trim(), "int");
                        }
                    }
                    else if (dt.Rows[i]["ShiftNo"].ToString().Trim() == "3")
                    {
                        //Temeprory Commented by sas
                        //start Date
                        if (StartTime.Hour.ToString().Trim() != ShiftHHMM[8] || StartTime.Minute.ToString().Trim() != ShiftHHMM[9])
                        {
                            //HH
                            string TagName4 = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName("ShiftHHMM", 8);
                            WriteValues(TagName4, StartTime.Hour.ToString().Trim(), "int");

                            //MM
                            string TagName1 = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName("ShiftHHMM", 9);
                            WriteValues(TagName1, StartTime.Minute.ToString().Trim(), "int");
                        }
                        //End Date
                        if (EndTime.Hour.ToString().Trim() != ShiftHHMM[10] || EndTime.Minute.ToString().Trim() != ShiftHHMM[11])
                        {
                            //HH
                            string TagNam = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName("ShiftHHMM", 10);
                            WriteValues(TagNam, EndTime.Hour.ToString().Trim(), "int");

                            //MM
                            string TagName1 = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName("ShiftHHMM", 11);
                            WriteValues(TagName1, EndTime.Minute.ToString().Trim(), "int");
                        }
                    }

                }

                //Write Number of Shfit
                string TagNM = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName("WriteTotalShift", 0);
                if (Convert.ToInt16(dt.Rows.Count) > 0 && Convert.ToInt16(dt.Rows.Count) < 4)//total shift must be 1/2/3
                {
                    WriteValues(TagNM, dt.Rows.Count.ToString(), "int");
                }


                //bit active to write HH MM values.
                string TagName = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName("EnterDownTime", 0);
                WriteValues(TagName, "1", "bool");


            }
            catch (Exception ex)
            {
                //log.Error("CommunicationWithPLC ApplicationStartUpTagRead SelectShiftTimeDetails()" + ex.Message);
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ApplicationStartUpTagRead SelectShiftTimeDetails()" + "", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            #endregion






        }

        public static void WriteValues(string addr, string data, String AddressType)
        {
            try
            {
                IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected = ReadWritePlcValue.getConnectionStatus();
                if (IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected == true) ////added by SBS to avoid hanging when there is no PLC communication 
                {
                    //log.Error("CommunicationWithPLC WriteValues() addr=" + addr + " data=" + data + " " + "Details save successfully");
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValues() addr=" + addr + " data=" + data + " ", DateTime.Now.ToString(),"Details save successfully",null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    try
                    {
                        short[] arrDeviceValue = new short[1];          //Data for 'DeviceValue'
                        arrDeviceValue[0] = Convert.ToInt16(data);
                        System.String[] arrData = new System.String[] { addr };
                        int result = ReadWritePlcValue.WriteTagValueInPLC(arrDeviceValue, arrData, 1); //mitsubishi, omron
                        //int result = ReadWritePlcValue.WriteTagValueInPLC(arrDeviceValue, arrData, 1, AddressType); //modbus

                        try
                        {
                            UserActivityEntity _insert = new UserActivityEntity();
                            _insert.DateTimeCol = DateTime.Now;
                            _insert.Activity = "Write Adreess : " + addr + " with Value :" + data;
                            if (IndiSCADAGlobalLibrary.UserLoginDetails.UserName == null)
                            {
                                _insert.UserName = "System";
                            }
                            else
                            {
                                _insert.UserName = IndiSCADAGlobalLibrary.UserLoginDetails.UserName;
                            }
                            //IndiSCADABusinessLogic.UserActivityLogic.InsertUserActivity(_insert);
                        }
                        catch (Exception ex)
                        {
                            //log.Error("LoginViewModel UserActivityInsert()" + ex.Message);
                            ErrorLogger.LogError.ErrorLog("LoginViewModel UserActivityInsert()" + "", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);


                        }

                        if (result == 0)
                        {
                            //log.Error("CommunicationWithPLC WriteValues()" + DateTime.Now.ToString() + "Details save successfully" + IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                           ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValues()" + "", DateTime.Now.ToString(),"Details save successfully",null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                        }
                        else
                        {
                            //log.Error("CommunicationWithPLC WriteValues()" + "Failed to save data" + IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValues()" +"", DateTime.Now.ToString(), "Failed to save data",null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                        }
                    }
                    catch (Exception ex)
                    {
                        //log.Error("CommunicationWithPLC WriteValues() Error while Writting" + ex.Message + IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValues() Error while Writting" +"", DateTime.Now.ToString(), ex.Message,null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                else
                {
                    //log.Error("CommunicationWithPLC WriteValues()" + "IsPLCConnected is false" + IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValues()"+ "", DateTime.Now.ToString(), "IsPLCConnected is false", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
            }
            catch (Exception ex)
            {
                //log.Error("CommunicationWithPLC WriteValues()" + ex.Message + IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValues()" +"", DateTime.Now.ToString(), ex.Message,null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

            }
        }


        //method which returns tag read values.
        public static string[] ReadPLCTagValue(string TaskName)
        {
            try
            {
                if (IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected == true) ////added by SBS to avoid hanging when there is no PLC communication 
                {
                    try
                    {
                        return ReadWritePlcValue.ReadTagValue(TaskName);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ReadPLCTagValue() While Writting :" + TaskName, DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        return null;
                    }
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ReadPLCTagValue()" + TaskName, DateTime.Now.ToString(), "IsPLCConnected is false", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    return null;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ReadPLCTagValue() " + TaskName, DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return null;
            }
        }

        //method which returns tag Real read values.
        public static string[] Real_ReadPLCTagValue(string TaskName)
        {
            try
            {
                if (IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected == true) ////added by SBS to avoid hanging when there is no PLC communication 
                {
                    try
                    {
                        return ReadWritePlcValue.ReadTag_RealValue(TaskName);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("CommunicationWithPLC Real_ReadPLCTagValue() while reading :" + TaskName, DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        return null;
                    }
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLC Real_ReadPLCTagValue()" + TaskName, DateTime.Now.ToString(), "IsPLCConnected is false", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    return null;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC Real_ReadPLCTagValue() " + TaskName, DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return null;
            }
        }

        //method which returns tag Real read values.
        public static string[] Read_BlockAddress(string[] TagName, int Length)
        {
            try
            {
                if (IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected == true) ////added by SBS to avoid hanging when there is no PLC communication 
                {
                    try
                    {
                        return ReadWritePlcValue.readDeviceRandom(TagName, Length);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("CommunicationWithPLC Read_BlockAddress()" + TagName[0], DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        return null;
                    }
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLC Read_BlockAddress()" + TagName[0], DateTime.Now.ToString(), "IsPLCConnected is false", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    return null;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC Read_BlockAddress() " + TagName[0], DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return null;
            }
        }

        //method which returns tag Word read values.
        public static string[] Word_ReadPLCTagValue(string TaskName)
        {
            try
            {
                if (IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected == true) ////added by SBS to avoid hanging when there is no PLC communication 
                {
                    try
                    {
                        return ReadWritePlcValue.ReadTag_DoubleWordValue(TaskName);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("CommunicationWithPLC Word_ReadPLCTagValue() " + TaskName, DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        return null;
                    }
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLC Word_ReadPLCTagValue()" + TaskName, DateTime.Now.ToString(), "IsPLCConnected is false", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    return null;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC Word_ReadPLCTagValue()" + TaskName, DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return null;
            }
        }
 

        //continuous tag read method
        private static void ContinuousTagRead()
        {


            try
            {
                if (IndiSCADAGlobalLibrary.TagList.SettingScreenContinuousTagRead == true)
                {
                    IndiSCADAGlobalLibrary.TagList.ManualCurrent = ReadPLCTagValue("RectifierManualCurrent");
                    IndiSCADAGlobalLibrary.TagList.RectifierHighSP = ReadPLCTagValue("RectifierHighSP");
                    IndiSCADAGlobalLibrary.TagList.RectifierLowSP = ReadPLCTagValue("RectifierLowSP");
                    IndiSCADAGlobalLibrary.TagList.AutoManual = ReadPLCTagValue("RECTIFIERAutoOrManual");
                    IndiSCADAGlobalLibrary.TagList.ManulaONOFF = ReadPLCTagValue("RECTIFIERManualONOrOFF");
                    IndiSCADAGlobalLibrary.TagList.RectifierAlarmStatus = ReadPLCTagValue("RECTIFIERAlarmStatus");
                    IndiSCADAGlobalLibrary.TagList.ResetAmpHr = ReadPLCTagValue("RECTIFIERResetCumulativeAmpHr");
                    IndiSCADAGlobalLibrary.TagList.RectifierCumulativeAmpPerHr = Real_ReadPLCTagValue("RectifierCumulativeAmpHr");
                    IndiSCADAGlobalLibrary.TagList.RectifierAmpHr = Real_ReadPLCTagValue("RectifierAmpHr");
                    IndiSCADAGlobalLibrary.TagList.Calculatedcurrent = ReadPLCTagValue("RectifierCalculatedCurrentOrAutoCurrent");

                    IndiSCADAGlobalLibrary.TagList.TempHigh = ReadPLCTagValue("TempControllerTemperatureHighSetPoint");
                    IndiSCADAGlobalLibrary.TagList.TempLow = ReadPLCTagValue("TempControllerTemperatureLowSetPoint");
                    IndiSCADAGlobalLibrary.TagList.TempSetpt = ReadPLCTagValue("TempControllerTemperatureControllerSetPoint");

                    IndiSCADAGlobalLibrary.TagList.DosingAutoManual = ReadPLCTagValue("DOSINGAutoOrManual");                //Get DosingManualONOFF from PLC   
                    IndiSCADAGlobalLibrary.TagList.DosingManualOffOn = ReadPLCTagValue("DOSINGManualOffOrOn");
                    IndiSCADAGlobalLibrary.TagList.DosingTimeFlowrate = ReadPLCTagValue("DOSINGTimerBasedOrFlowrateBased");
                    IndiSCADAGlobalLibrary.TagList.DosingOnOrOffStatus = ReadPLCTagValue("DOSINGOnOrOffStatus");
                    IndiSCADAGlobalLibrary.TagList.DosingQuantity = Real_ReadPLCTagValue("DosingQuantityInml");
                    IndiSCADAGlobalLibrary.TagList.DosingFlowRate = Real_ReadPLCTagValue("DosingFlowRatemlperSec");
                    IndiSCADAGlobalLibrary.TagList.DosingSetAmpHr = Real_ReadPLCTagValue("DosingSetAmpHr");
                    IndiSCADAGlobalLibrary.TagList.DosingActualAmpHr = Real_ReadPLCTagValue("DosingActualAmpHr");
                    IndiSCADAGlobalLibrary.TagList.DosingRemainingTime = ReadPLCTagValue("DosingRemainingTimeInSec");
                    IndiSCADAGlobalLibrary.TagList.DosingTimeInSec = ReadPLCTagValue("DosingTimeInSec");
                    IndiSCADAGlobalLibrary.TagList.DosingCumulativeAmpHr = Real_ReadPLCTagValue("DosingCumulativeAmpHr");//dINT  
                    IndiSCADAGlobalLibrary.TagList.SetPH = DivideBy(ReadPLCTagValue("DosingSetPH"), 100);
                    IndiSCADAGlobalLibrary.TagList.DosingpHActual = DivideBy(ReadPLCTagValue("DosingActPH"), 100);

                    IndiSCADAGlobalLibrary.TagList.pHHigh = DivideBy(ReadPLCTagValue("pHMeterpHHighsetPoint"), 100, 5);
                    IndiSCADAGlobalLibrary.TagList.pHLow = DivideBy(ReadPLCTagValue("pHMeterpHLowsetPoint"), 100, 5);

                    IndiSCADAGlobalLibrary.TagList.Wagon1WCSInput = Word_ReadPLCTagValue("Wagon1WCSInput");
                    IndiSCADAGlobalLibrary.TagList.Wagon2WCSInput = Word_ReadPLCTagValue("Wagon2WCSInput");
                    IndiSCADAGlobalLibrary.TagList.Wagon3WCSInput = Word_ReadPLCTagValue("Wagon3WCSInput");
                    IndiSCADAGlobalLibrary.TagList.Wagon4WCSInput = Word_ReadPLCTagValue("Wagon4WCSInput");
                    IndiSCADAGlobalLibrary.TagList.Wagon5WCSInput = Word_ReadPLCTagValue("Wagon5WCSInput");
                    IndiSCADAGlobalLibrary.TagList.Wagon6WCSInput = Word_ReadPLCTagValue("Wagon6WCSInput");
                    IndiSCADAGlobalLibrary.TagList.Wagon7WCSInput = Word_ReadPLCTagValue("Wagon6WCSInput");

                    IndiSCADAGlobalLibrary.TagList.TankBypass = ReadPLCTagValue("BypassplatingTankBypassPlating");
                    IndiSCADAGlobalLibrary.TagList.TrayBypass = ReadPLCTagValue("TrayBypassBypass");

                    IndiSCADAGlobalLibrary.TagList.FilterPumpOnOFF = ReadPLCTagValue("FilterPumpOnOFF");
                    IndiSCADAGlobalLibrary.TagList.FilterPumpInputCBTrip = ReadPLCTagValue("FilterPumpInputCBTrip");
                    IndiSCADAGlobalLibrary.TagList.FilterPumpOutput = ReadPLCTagValue("FilterPumpOutput");

                    IndiSCADAGlobalLibrary.TagList.DryerBypass = ReadPLCTagValue("DryerBypass");

                    IndiSCADAGlobalLibrary.TagList.OilSkimmerAutoManual = ReadPLCTagValue("OilSkimmerAutoManual");
                    IndiSCADAGlobalLibrary.TagList.OilSkimmerTrip = ReadPLCTagValue("OilSkimmerTrip");
                    IndiSCADAGlobalLibrary.TagList.OilSkimmerOutput = ReadPLCTagValue("OilSkimmerOutput");

                    IndiSCADAGlobalLibrary.TagList.BaseBarrelMotorONOFF = ReadPLCTagValue("BaseBarrelMotorONOFF");
                    IndiSCADAGlobalLibrary.TagList.BaseBarrelMotorTrip = ReadPLCTagValue("BaseBarrelMotorTrip");
                    IndiSCADAGlobalLibrary.TagList.BaseBarrelMotorStatus = ReadPLCTagValue("BaseBarrelMotorStatus");


                    IndiSCADAGlobalLibrary.TagList.SettingScreenContinuousTagRead = false;



                }



                ////Tag Id 1----------------------------------------------------------------------------------------------
                //IndiSCADAGlobalLibrary.TagList.W1Alarms = ReadPLCTagValue("W1Alarms");
                ////Tag Id 2----------------------------------------------------------------------------------------------
                //IndiSCADAGlobalLibrary.TagList.W2Alarms = ReadPLCTagValue("W2Alarms");
                ////Tag Id 3----------------------------------------------------------------------------------------------
                //IndiSCADAGlobalLibrary.TagList.W3Alarms = ReadPLCTagValue("W3Alarms");
                ////Tag Id 4----------------------------------------------------------------------------------------------
                //IndiSCADAGlobalLibrary.TagList.W4Alarms = ReadPLCTagValue("W4Alarms");
                ////Tag Id 5----------------------------------------------------------------------------------------------
                //IndiSCADAGlobalLibrary.TagList.W5Alarms = ReadPLCTagValue("W5Alarms");
                ////Tag Id 6----------------------------------------------------------------------------------------------
                //IndiSCADAGlobalLibrary.TagList.W6Alarms = ReadPLCTagValue("W6Alarms");
                ////Tag Id 5----------------------------------------------------------------------------------------------
                //IndiSCADAGlobalLibrary.TagList.TemperatureAlarms = ReadPLCTagValue("TemperatureControllerAlarms");
                ////Tag Id 6----------------------------------------------------------------------------------------------
                //IndiSCADAGlobalLibrary.TagList.DryerAlarms = ReadPLCTagValue("DryerAlarm"); // includes pH alarms
                ////Tag Id 7----------------------------------------------------------------------------------------------
                //IndiSCADAGlobalLibrary.TagList.CrossTrolleyAlarms = ReadPLCTagValue("CrossTrolly");

                ////Tag Id 8----------------------------------------------------------------------------------------------
                //IndiSCADAGlobalLibrary.TagList.RectifierAlarms1 = ReadPLCTagValue("RectifierAlarm");

                ////Tag Id 9----------------------------------------------------------------------------------------------
                //IndiSCADAGlobalLibrary.TagList.pHAlarm = ReadPLCTagValue("phAlarm");

                //Tag Id 119----------------------------------------------------------------------------------------------                
                IndiSCADAGlobalLibrary.TagList.W1DataLog = ReadPLCTagValue("Wagon1DataLog");
                //Tag Id 120----------------------------------------------------------------------------------------------
                IndiSCADAGlobalLibrary.TagList.W2DataLog = ReadPLCTagValue("Wagon2DataLog");
                //Tag Id 121----------------------------------------------------------------------------------------------                
                IndiSCADAGlobalLibrary.TagList.W3DataLog = ReadPLCTagValue("Wagon3DataLog");
                //Tag Id 122----------------------------------------------------------------------------------------------
                IndiSCADAGlobalLibrary.TagList.W4DataLog = ReadPLCTagValue("Wagon4DataLog");
                //Tag Id 123----------------------------------------------------------------------------------------------
                IndiSCADAGlobalLibrary.TagList.W5DataLog = ReadPLCTagValue("Wagon5DataLog");
                //Tag Id 124----------------------------------------------------------------------------------------------
                IndiSCADAGlobalLibrary.TagList.W6DataLog = ReadPLCTagValue("Wagon6DataLog");
                //Tag Id 124----------------------------------------------------------------------------------------------
                IndiSCADAGlobalLibrary.TagList.W7DataLog = ReadPLCTagValue("Wagon7DataLog");
                //Tag Id 124----------------------------------------------------------------------------------------------
                //IndiSCADAGlobalLibrary.TagList.W8DataLog = ReadPLCTagValue("Wagon8DataLog");
                //Tag Id 67----------------------------------------------------------------------------------------------
                IndiSCADAGlobalLibrary.TagList.TemperatureActual = ReadPLCTagValue("TempControllerActual");//DivideBy(ReadPLCTagValue("TempControllerActual"), 1);
                //Tag Id 65----------------------------------------------------------------------------------------------
                IndiSCADAGlobalLibrary.TagList.TemperatureHighSP = ReadPLCTagValue("TempControllerTemperatureHighSetPoint");
                //IndiSCADAGlobalLibrary.TagList.TemperatureHighSP = ReadPLCTagValue("TempControllerTemperatureHighSetPoint");
                //Tag Id 66----------------------------------------------------------------------------------------------
                //String[] Address1 = new String[] { "3181" };
                //IndiSCADAGlobalLibrary.TagList.TemperatureLowSP = Read_BlockAddress(Address1, 25);
                IndiSCADAGlobalLibrary.TagList.TemperatureLowSP = ReadPLCTagValue("TempControllerTemperatureLowSetPoint");
                //Tag Id 68----------------------------------------------------------------------------------------------
                //String[] Address2 = new String[] { "3061" };
                //IndiSCADAGlobalLibrary.TagList.TemperatureSetPoint = Read_BlockAddress(Address2, 25);
                IndiSCADAGlobalLibrary.TagList.TemperatureSetPoint = ReadPLCTagValue("TempControllerTemperatureControllerSetPoint");

                //Tag Id 12----------------------------------------------------------------------------------------------
                //String[] Address6 = new String[] { "5041" };
                //IndiSCADAGlobalLibrary.TagList.ActualCurrent = Read_BlockAddress(Address6, 20);
                IndiSCADAGlobalLibrary.TagList.ActualCurrent = ReadPLCTagValue("RectifierACTUALCurrent");
                //Tag Id 12----------------------------------------------------------------------------------------------
                //String[] Address7 = new String[] { "5041" };
                //IndiSCADAGlobalLibrary.TagList.AvgCurrentValues = Read_BlockAddress(Address7, 20);
                IndiSCADAGlobalLibrary.TagList.AvgCurrentValues = ReadPLCTagValue("RectifierAverageCurrent");

                IndiSCADAGlobalLibrary.TagList.AppliedCurrentValues = ReadPLCTagValue("RectifierAppliedCurrent");

                //IndiSCADAGlobalLibrary.TagList.AvgCurrentValues = ReadPLCTagValue("RectifierAverageCurrent");
                //Tag Id 13----------------------------------------------------------------------------------------------
                //String[] Address8 = new String[] { "5001" };
                //IndiSCADAGlobalLibrary.TagList.ActualVoltage = Read_BlockAddress(Address8, 20);
                IndiSCADAGlobalLibrary.TagList.ActualVoltage = DivideBy(ReadPLCTagValue("RectifierActualVoltage"), 100);
                //Tag Id 14----------------------------------------------------------------------------------------------
                //String[] Address9 = new String[] { "5121" };RectifierCalculatedCurrentOrAutoCurrent
                //IndiSCADAGlobalLibrary.TagList.AutoCurrentSP = Read_BlockAddress(Address9, 20);
                IndiSCADAGlobalLibrary.TagList.AutoCurrentSP = ReadPLCTagValue("RectifierCalculatedCurrentOrAutoCurrent");
                //Tag Id 15----------------------------------------------------------------------------------------------
                //String[] Address10 = new String[] { "5161" };
                //IndiSCADAGlobalLibrary.TagList.ManualCurrentSP = Read_BlockAddress(Address10, 20);
                IndiSCADAGlobalLibrary.TagList.ManualCurrentSP = ReadPLCTagValue("RectifierManualCurrent");

                //Tag Id 69----------------------------------------------------------------------------------------------
                //String[] Address3 = new String[] { "3901" };
                //IndiSCADAGlobalLibrary.TagList.pHActual = Read_BlockAddress(Address3, 4);
                IndiSCADAGlobalLibrary.TagList.pHActual = DivideBy(ReadPLCTagValue("pHMeterpHActual"), 100);
                //Tag Id 70----------------------------------------------------------------------------------------------
                //String[] Address4 = new String[] { "3941" };
                //IndiSCADAGlobalLibrary.TagList.pHHighSP = Read_BlockAddress(Address4, 4);
                IndiSCADAGlobalLibrary.TagList.pHHighSP = ReadPLCTagValue("pHMeterpHHighsetPoint");
                //Tag Id 71----------------------------------------------------------------------------------------------
                //String[] Address5 = new String[] { "3921" };
                //IndiSCADAGlobalLibrary.TagList.pHLowSP = Read_BlockAddress(Address5, 4);
                IndiSCADAGlobalLibrary.TagList.pHLowSP = ReadPLCTagValue("pHMeterpHLowsetPoint");
                //Tag Id 39----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.WagonMovment = ReadPLCTagValue("WagonLocations");
                //Tag Id 40----------------------------------------------------------------------------------------------

                ////Tag Id 82----------------------------------------------------------------------------------------------
                //IndiSCADAGlobalLibrary.TagList.PlantEventValue = ReadPLCTagValue("PlantEventLogging");
                ////Tag Id 83----------------------------------------------------------------------------------------------
                //IndiSCADAGlobalLibrary.TagList.W1EventValues = ReadPLCTagValue("W1EventLogging");
                ////Tag Id 84----------------------------------------------------------------------------------------------
                //IndiSCADAGlobalLibrary.TagList.W2EventValues = ReadPLCTagValue("W2EventLogging");
                ////Tag Id 85----------------------------------------------------------------------------------------------
                //IndiSCADAGlobalLibrary.TagList.W3EventValues = ReadPLCTagValue("W3EventLogging");
                ////Tag Id 86----------------------------------------------------------------------------------------------
                //IndiSCADAGlobalLibrary.TagList.W4EventValues = ReadPLCTagValue("W4EventLogging");
                ////Tag Id 86----------------------------------------------------------------------------------------------
                //IndiSCADAGlobalLibrary.TagList.W5EventValues = ReadPLCTagValue("W5EventLogging");
                ////Tag Id 86----------------------------------------------------------------------------------------------
                //IndiSCADAGlobalLibrary.TagList.W6EventValues = ReadPLCTagValue("W6EventLogging");




                IndiSCADAGlobalLibrary.TagList.WagonNextStep = ReadPLCTagValue("WagaonNextStep"); // address need to check with PLC  //????
                //Tag Id 41----------------------------------------------------------------------------------------------
                IndiSCADAGlobalLibrary.TagList.WagonNextStation = ReadPLCTagValue("WagaonNextStation");// address need to check with PLC  //????
                IndiSCADAGlobalLibrary.TagList.WagonMovment = ReadPLCTagValue("WagonMovement");
                ////Tag Id 40----------------------------------------------------------------------------------------------
                IndiSCADAGlobalLibrary.TagList.LastCycleTime = ReadPLCTagValue("CYCLETIMESETCYCLETIME");

                try
                {
                    IndiSCADAGlobalLibrary.TagList.LoadDipTime = ReadPLCTagValue("DipTimeForDisplayDipTime");
                }
                catch { }
                #region
                //commented

                ////out of range tags
                //IndiSCADAGlobalLibrary.TagList.ORTempLowCount = ReadPLCTagValue("ORTEMPERATURELowCount");
                //IndiSCADAGlobalLibrary.TagList.ORTempHighCount = ReadPLCTagValue("ORTEMPERATUREHighCount");
                //IndiSCADAGlobalLibrary.TagList.ORLowBypassTemp = ReadPLCTagValue("ORTEMPERATURELowBypass");
                //IndiSCADAGlobalLibrary.TagList.ORHighBypassTemp = ReadPLCTagValue("ORTEMPERATUREHighBypass");

                //IndiSCADAGlobalLibrary.TagList.ORpHLowCount = ReadPLCTagValue("ORpHLowCount");
                //IndiSCADAGlobalLibrary.TagList.ORpHHighCount = ReadPLCTagValue("ORpHHighCount");
                //IndiSCADAGlobalLibrary.TagList.ORpHLowBypass = ReadPLCTagValue("ORpHLowBypass");
                //IndiSCADAGlobalLibrary.TagList.ORpHHighBypass = ReadPLCTagValue("ORpHHighBypass");

                ////IndiSCADAGlobalLibrary.TagList.ORCurrentLow = ReadPLCTagValue("ORCurrentLow");
                ////IndiSCADAGlobalLibrary.TagList.ORCurrentHigh = ReadPLCTagValue("ORCurrentHigh");

                //IndiSCADAGlobalLibrary.TagList.ORDipTimeLow = ReadPLCTagValue("ORDipTimeEnterLow");
                //IndiSCADAGlobalLibrary.TagList.ORDipTimeHigh = ReadPLCTagValue("ORDipTimeEnterHigh");

                ////out of range for datalog
                //IndiSCADAGlobalLibrary.TagList.ORTempLowSP = ReadPLCTagValue("ORTEMPERATURELowSetPoint");
                //IndiSCADAGlobalLibrary.TagList.ORTempHighSP = ReadPLCTagValue("ORTEMPERATUREHighSetPoint");
                //IndiSCADAGlobalLibrary.TagList.ORTempAvg = ReadPLCTagValue("ORTEMPERATUREAvg");
                //IndiSCADAGlobalLibrary.TagList.ORTempLowTime = ReadPLCTagValue("ORTEMPERATURELowTime");
                //IndiSCADAGlobalLibrary.TagList.ORTempHighTime = ReadPLCTagValue("ORTEMPERATUREHighTime");

                //IndiSCADAGlobalLibrary.TagList.ORpHLowSP = ReadPLCTagValue("ORpHLowSetPoint");
                //IndiSCADAGlobalLibrary.TagList.ORpHHighSP = ReadPLCTagValue("ORpHHighSetPoint");
                //IndiSCADAGlobalLibrary.TagList.ORpHLowTime = ReadPLCTagValue("ORpHLowTime");
                //IndiSCADAGlobalLibrary.TagList.ORpHHighTime = ReadPLCTagValue("ORpHHighTime");
                //IndiSCADAGlobalLibrary.TagList.ORpHAvg = ReadPLCTagValue("ORpHAvg");

                ////IndiSCADAGlobalLibrary.TagList.ORCurrentAvg = ReadPLCTagValue("ORCurrentAvg");

                //IndiSCADAGlobalLibrary.TagList.ORDiptimeLowBypass = getDurMinSec(ReadPLCTagValue("ORDipTimeLowBypass"));
                //IndiSCADAGlobalLibrary.TagList.ORDiptimeHighBypass = getDurMinSec(ReadPLCTagValue("ORDipTimeHighBypass"));

                ////string[] Duration = ReadPLCTagValue("DipTimeForDisplayDipTime");
                ////string[] DurationHHMM = getDurMinSec(Duration);
               #endregion

                #region commented tags

                // //Tag Id 1----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.W1Alarms = ReadPLCTagValue("W1Alarms");
                // //Tag Id 2----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.W2Alarms = ReadPLCTagValue("W2Alarms");
                // //Tag Id 3----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.W3Alarms = ReadPLCTagValue("W3Alarms");
                // //Tag Id 4----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.W4Alarms = ReadPLCTagValue("W4Alarms");
                // //Tag Id 5----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.TemperatureAlarms = ReadPLCTagValue("TemperatureControllerAlarms");
                // //Tag Id 6----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.DryerAlarms = ReadPLCTagValue("DryerAlarm"); // includes pH alarms
                // //Tag Id 7----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.CrossTrolleyAlarms = ReadPLCTagValue("CrossTrolley1Alarm");

                // //Tag Id 8----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.RectifierAlarms1 = ReadPLCTagValue("RectifierAlarm");

                // //Tag Id 9----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.pHAlarm = ReadPLCTagValue("phAlarm");

                // //Tag Id 94----------------------------------------------------------------------------------------------                
                // IndiSCADAGlobalLibrary.TagList.W1DataLog = ReadPLCTagValue("Wagon1DataLog");
                // //Tag Id 95----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.W2DataLog = ReadPLCTagValue("Wagon2DataLog");
                // //Tag Id 96----------------------------------------------------------------------------------------------                
                // IndiSCADAGlobalLibrary.TagList.W3DataLog = ReadPLCTagValue("Wagon3DataLog");
                // //Tag Id 97----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.W4DataLog = ReadPLCTagValue("Wagon4DataLog");

                // //Tag Id 67----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.TemperatureActual = ReadPLCTagValue("TempControllerActual");
                // //Tag Id 65----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.TemperatureHighSP = ReadPLCTagValue("TempControllerHighSetPoint");
                // //Tag Id 66----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.TemperatureLowSP = ReadPLCTagValue("TempControllerLowSetPoint");
                // //Tag Id 68----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.TemperatureSetPoint = ReadPLCTagValue("TempControllerSetPoint");

                // //Tag Id 12----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.ActualCurrent = ReadPLCTagValue("RectifierACTUALCurrent");
                // //Tag Id 12----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.AvgCurrentValues = ReadPLCTagValue("RectifierAVERAGECurrent");
                // //Tag Id 13----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.ActualVoltage =DivideBy(ReadPLCTagValue("RectifierAcutalVoltage"),100); 
                // //Tag Id 14----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.AutoCurrentSP = ReadPLCTagValue("RectifierCalculatedCurrentAutoCurrent");
                // //Tag Id 15----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.ManualCurrentSP = ReadPLCTagValue("RectifierManualCurrent");

                // //Tag Id 69----------------------------------------------------------------------------------------------
                // //string[] pHH = new string[] { "2.1","3","0.1","1.4","4.2"};
                // IndiSCADAGlobalLibrary.TagList.pHActual = DivideBy(ReadPLCTagValue("PHMeterActual"),100);



                // //Tag Id 70----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.pHHighSP = ReadPLCTagValue("PHMeterHighsetPoint");
                // //Tag Id 71----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.pHLowSP = ReadPLCTagValue("PHMeterLowsetPoint");
                // //Tag Id 39----------------------------------------------------------------------------------------------
                //// IndiSCADAGlobalLibrary.TagList.WagonMovment = ReadPLCTagValue("WagonLocations");
                // //Tag Id 40----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.WagonNextStep = ReadPLCTagValue("WagaonNextStep"); // address need to check with PLC 
                // //Tag Id 41----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.WagonNextStation = ReadPLCTagValue("WagaonNextStation");// address need to check with PLC 
                // //Tag Id 82----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.PlantEventValue = ReadPLCTagValue("PlantEventLogging");
                // //Tag Id 83----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.W1EventValues = ReadPLCTagValue("W1EventLogging");
                // //Tag Id 84----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.W2EventValues = ReadPLCTagValue("W2EventLogging");
                // //Tag Id 85----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.W3EventValues = ReadPLCTagValue("W3EventLogging");
                // //Tag Id 86----------------------------------------------------------------------------------------------
                // IndiSCADAGlobalLibrary.TagList.W4EventValues = ReadPLCTagValue("W4EventLogging");

                // IndiSCADAGlobalLibrary.TagList.WagonMovment = ReadPLCTagValue("WagonMovement");
                // //Tag Id 40----------------------------------------------------------------------------------------------
                // // IndiSCADAGlobalLibrary.TagList.WagonNextStep = ReadPLCTagValue("WagaonNextStep");
                // //Tag Id 41----------------------------------------------------------------------------------------------
                // //IndiSCADAGlobalLibrary.TagList.WagonNextStation = ReadPLCTagValue("WagaonNextStation");

                // IndiSCADAGlobalLibrary.TagList.LoadTypeatStationArrayLoadType = ReadPLCTagValue("LoadTypeatStationArrayLoadType");





                // //out of range tags
                // IndiSCADAGlobalLibrary.TagList.ORTempLowCount = ReadPLCTagValue("ORTEMPERATURELowCount");
                // IndiSCADAGlobalLibrary.TagList.ORTempHighCount = ReadPLCTagValue("ORTEMPERATUREHighCount");
                // IndiSCADAGlobalLibrary.TagList.ORLowBypassTemp = ReadPLCTagValue("ORTEMPERATURELowBypass");
                // IndiSCADAGlobalLibrary.TagList.ORHighBypassTemp = ReadPLCTagValue("ORTEMPERATUREHighBypass");

                // IndiSCADAGlobalLibrary.TagList.ORpHLowCount = ReadPLCTagValue("ORpHLowCount");
                // IndiSCADAGlobalLibrary.TagList.ORpHHighCount = ReadPLCTagValue("ORpHHighCount");
                // IndiSCADAGlobalLibrary.TagList.ORpHLowBypass = ReadPLCTagValue("ORpHLowBypass");
                // IndiSCADAGlobalLibrary.TagList.ORpHHighBypass = ReadPLCTagValue("ORpHHighBypass");

                // IndiSCADAGlobalLibrary.TagList.ORCurrentLow = ReadPLCTagValue("ORCurrentLow");
                // IndiSCADAGlobalLibrary.TagList.ORCurrentHigh = ReadPLCTagValue("ORCurrentHigh");

                // IndiSCADAGlobalLibrary.TagList.ORDipTimeLow = ReadPLCTagValue("ORDiptimeEnterLow");
                // IndiSCADAGlobalLibrary.TagList.ORDipTimeHigh = ReadPLCTagValue("ORDiptimeEnterHigh");

                // //out of range for datalog
                // IndiSCADAGlobalLibrary.TagList.ORTempLowSP = ReadPLCTagValue("ORTEMPERATURELowSetPoint");
                // IndiSCADAGlobalLibrary.TagList.ORTempHighSP = ReadPLCTagValue("ORTEMPERATUREHighSetPoint");
                // IndiSCADAGlobalLibrary.TagList.ORTempAvg = ReadPLCTagValue("ORTEMPERATUREAvg");
                // IndiSCADAGlobalLibrary.TagList.ORTempLowTime = ReadPLCTagValue("ORTEMPERATURELowTime");
                // IndiSCADAGlobalLibrary.TagList.ORTempHighTime = ReadPLCTagValue("ORTEMPERATUREHighTime");

                // IndiSCADAGlobalLibrary.TagList.ORpHLowSP = ReadPLCTagValue("ORpHLowSetPoint");
                // IndiSCADAGlobalLibrary.TagList.ORpHHighSP = ReadPLCTagValue("ORpHHighSetPoint");
                // IndiSCADAGlobalLibrary.TagList.ORpHLowTime = ReadPLCTagValue("ORpHLowTime");
                // IndiSCADAGlobalLibrary.TagList.ORpHHighTime = ReadPLCTagValue("ORpHHighTime");
                // IndiSCADAGlobalLibrary.TagList.ORpHAvg = ReadPLCTagValue("ORpHAvg");

                // IndiSCADAGlobalLibrary.TagList.ORCurrentAvg = ReadPLCTagValue("ORCurrentAvg");

                // IndiSCADAGlobalLibrary.TagList.ORDiptimeLowBypass = getDurMinSec(ReadPLCTagValue("ORDiptimeLowBypass"));
                // IndiSCADAGlobalLibrary.TagList.ORDiptimeHighBypass = getDurMinSec(ReadPLCTagValue("ORDiptimeHighBypass"));
                // IndiSCADAGlobalLibrary.TagList.LastCycleTime = ReadPLCTagValue("LastCycleTime");
                // //string[] Duration = ReadPLCTagValue("DipTimeForDisplayDipTime");
                // //string[] DurationHHMM = getDurMinSec(Duration);
                #endregion

            }
            catch (Exception exContinuousTagRead)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ContinuousTagRead()", DateTime.Now.ToString(), exContinuousTagRead.Message, null, true);
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

        public static string[] getDurMinSec(string[] duration)
        {
            try
            {
                string[] result = new string[duration.Length];
                int index = 0;
                foreach (string dur in duration)
                {
                    int totalSeconds = Convert.ToInt32(dur);
                    int seconds = totalSeconds % 60;
                    int minutes = (totalSeconds / 60) % 60;
                    int hour = totalSeconds / 3600;
                    result[index] = hour + ":" + minutes + ":" + seconds;
                    index++;
                }
                return result;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TankDetailsLogic GetDuration()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return null;
            }
        }
        public static string[] DivideBy(string[] SourceValue, int devidefactor)
        {
            try
            {
                // string[] value = new string[12];
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
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC DivideBy method", DateTime.Now.ToString(), ex.ToString(), null, true);
                return null;
            }
        }

        #region"Wagon Data Log"
        private static void ProductionDataLog(string[] WagonDataLog, string WagonName)
        {
            try
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ProductionDataLog() WagonName : " + WagonName + " Getput:" + WagonDataLog[0] + " StationNO:" + WagonDataLog[1] + " LoadType:" + WagonDataLog[2] + " Year:" + WagonDataLog[3] + " Month:" + WagonDataLog[4] + " Day:" + WagonDataLog[5] + " Hour:" + WagonDataLog[6] + " Minutes:" + WagonDataLog[7] + " Seconds:" + WagonDataLog[8] + " LoadGenerationMMDD:" + WagonDataLog[10] + " LoadNO:" + WagonDataLog[11] + " ", DateTime.Now.ToString(), "", null, true);

                if (WagonDataLog != null && WagonDataLog.Length > 0)//check array is not null
                { 
                    // Add loadstring in collection
                    try
                    {
                        if (IndiSCADAGlobalLibrary.ConfigurationUpdate.StopWagonStatusDatalog == true)
                        {
                            if (IndiSCADAGlobalLibrary.TagList.DataLogDebug == true)
                            {
                                if (WagonName == "Wagon1")
                                {
                                    if (PrevWagon1DataLog != null)
                                    {
                                        bool result = PrevWagon1DataLog.SequenceEqual(WagonDataLog);
                                        if (result == false)
                                        {
                                            AddLoadStringInCollection(WagonDataLog, WagonName);
                                        }
                                    }
                                }
                                else if (WagonName == "Wagon2")
                                {
                                    if (PrevWagon2DataLog != null)
                                    {
                                        bool result = PrevWagon2DataLog.SequenceEqual(WagonDataLog);
                                        if (result == false)
                                        {
                                            AddLoadStringInCollection(WagonDataLog, WagonName);
                                        }
                                    }
                                }
                                if (WagonName == "Wagon3")
                                {
                                    if (PrevWagon3DataLog != null)
                                    {
                                        bool result = PrevWagon3DataLog.SequenceEqual(WagonDataLog);
                                        if (result == false)
                                        {
                                            AddLoadStringInCollection(WagonDataLog, WagonName);
                                        }
                                    }
                                }
                                else if (WagonName == "Wagon4")
                                {
                                    if (PrevWagon4DataLog != null)
                                    {
                                        bool result = PrevWagon4DataLog.SequenceEqual(WagonDataLog);
                                        if (result == false)
                                        {
                                            AddLoadStringInCollection(WagonDataLog, WagonName);
                                        }
                                    }
                                }
                                if (WagonName == "Wagon5")
                                {
                                    if (PrevWagon5DataLog != null)
                                    {
                                        bool result = PrevWagon5DataLog.SequenceEqual(WagonDataLog);
                                        if (result == false)
                                        {
                                            AddLoadStringInCollection(WagonDataLog, WagonName);
                                        }
                                    }
                                }
                                else if (WagonName == "Wagon6")
                                {
                                    if (PrevWagon6DataLog != null)
                                    {
                                        bool result = PrevWagon6DataLog.SequenceEqual(WagonDataLog);
                                        if (result == false)
                                        {
                                            AddLoadStringInCollection(WagonDataLog, WagonName);
                                        }
                                    }
                                }
                                if (WagonName == "Wagon7")
                                {
                                    if (PrevWagon7DataLog != null)
                                    {
                                        bool result = PrevWagon7DataLog.SequenceEqual(WagonDataLog);
                                        if (result == false)
                                        {
                                            AddLoadStringInCollection(WagonDataLog, WagonName);
                                        }
                                    }
                                }
                                //else if (WagonName == "Wagon8")
                                //{
                                //    if (PrevWagon8DataLog != null)
                                //    {
                                //        bool result = PrevWagon8DataLog.SequenceEqual(WagonDataLog);
                                //        if (result == false)
                                //        {
                                //            AddLoadStringInCollection(WagonDataLog, WagonName);
                                //        }
                                //    }
                                //}
                            }
                            else
                            {
                                if (WagonDetails.Count > 0)
                                {
                                    WagonDetails = new ObservableCollection<WagonLoadStringEntity>();
                                    PrevWagon1DataLog = new string[] { };
                                    PrevWagon2DataLog = new string[] { };
                                    PrevWagon3DataLog = new string[] { };
                                    PrevWagon4DataLog = new string[] { };
                                    PrevWagon5DataLog = new string[] { };
                                    PrevWagon6DataLog = new string[] { };
                                    PrevWagon7DataLog = new string[] { };
                                    //PrevWagon8DataLog = new string[] { };
                                }
                            }
                        }
                    }
                    catch
                    {
                        ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ProductionDataLog() AddLoadstring Error" + WagonName, DateTime.Now.ToString(), "", null, true);
                    }

                    int StationNumber = IntConvertion(WagonDataLog[1]);//Read Station Number
                    int LoadCounterNo = IntConvertion(WagonDataLog[11]);//ReadLoad No

                    if (LoadCounterNo != 0)
                    {
                        string LoadNumber = GetLoadNo(WagonDataLog, WagonName);//GetLoadNo Load No from array

                        DateTime LoadGetPutDateTime = DateTime.Parse(GetWagonEventDateTime(WagonDataLog, WagonName).ToString());//Get Date Time

                        ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ProductionDataLog(CheckGetPut) " + WagonName, DateTime.Now.ToString(), "LoadNumber value : " + LoadNumber, null, true);
                        //Get event execution-----------------------------------------------------------------------
                        if (WagonDataLog[0] == "1")
                        {
                            WagonGetEventDataLog(LoadNumber, LoadGetPutDateTime, WagonDataLog);
                        }
                        //Put event execution-----------------------------------------------------------------------
                        else if (WagonDataLog[0] == "2")
                        {
                            WagonPutEventDataLog(LoadNumber, LoadGetPutDateTime, WagonDataLog);
                            ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ProductionDataLog LoadNoAtStation StationNumber=" + StationNumber + " LoadCounterNo" + LoadCounterNo.ToString(), DateTime.Now.ToString(), "", null, true);
                            IndiSCADAGlobalLibrary.TagList.LoadNoAtStation[StationNumber - 1] = LoadCounterNo.ToString();

                        }
                        //when event is not valid--------------------------------------------------------------------
                        else
                        {
                            ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ProductionDataLog(CheckGetPut) " + WagonName, DateTime.Now.ToString(), "Get put event value is not valid. " + WagonDataLog[0], null, true);
                        }
                    }
                    else
                    {
                        ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ProductionDataLog(CheckGetPut) for wagon=" + WagonName+ " at station=" + StationNumber+" LoadNo = " + LoadCounterNo, DateTime.Now.ToString(), "Get put event value is not valid. " + WagonDataLog[0], null, true);
                    }
                }
            }
            catch (Exception exProductionDataLog)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ProductionDataLog() " + WagonName, DateTime.Now.ToString(), exProductionDataLog.Message, null, true);
            }
        }

        // 
        public static void AddLoadStringInCollection(string[] WagonDataLog, string WagonName)
        {
            try
            {
                if (WagonDetails != null)
                {
                    if ((WagonDataLog != null))
                    {
                        WagonLoadStringEntity _WagonDetailsEntity = new WagonLoadStringEntity();

                        _WagonDetailsEntity.WagonNumber = WagonName;
                        _WagonDetailsEntity.GetPut = WagonDataLog[0];
                        _WagonDetailsEntity.StationNo = WagonDataLog[1];
                        _WagonDetailsEntity.LoadType = WagonDataLog[2];
                        _WagonDetailsEntity.Year = WagonDataLog[3]; ;
                        _WagonDetailsEntity.Month = WagonDataLog[4]; ;
                        _WagonDetailsEntity.Day = WagonDataLog[5];
                        _WagonDetailsEntity.Hour = WagonDataLog[6];
                        _WagonDetailsEntity.Minutes = WagonDataLog[7];
                        _WagonDetailsEntity.Seconds = WagonDataLog[8];
                        _WagonDetailsEntity.MMDD = WagonDataLog[10];
                        _WagonDetailsEntity.LoadNumber = WagonDataLog[11];

                        if (WagonDataLog[10].Length >= 3)
                        {
                            _WagonDetailsEntity.isMMDDValid = true;
                        }
                        else
                        {
                            _WagonDetailsEntity.isMMDDValid = false;
                        }

                        if ((WagonDataLog[11].Equals("0")) || (WagonDataLog[11].Equals("")))
                        {
                            _WagonDetailsEntity.isLoadNoValid = false;
                        }
                        else
                        {
                            _WagonDetailsEntity.isLoadNoValid = true;
                        }

                        WagonDetails.Add(_WagonDetailsEntity);
                    }
                }
            }
            catch (Exception exProductionDataLog)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ProductionDataLog() " + WagonName, DateTime.Now.ToString(), exProductionDataLog.Message, null, true);
            }
        }

        //Int convertion
        private static int IntConvertion(string valueToConvert)
        {
            try
            {
                return Convert.ToInt16(valueToConvert);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC IntConvertion()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return 0;
            }
        }
        //Get event data logging
        private static void WagonGetEventDataLog(string LoadNumber, DateTime LoadGetPutEventDateTime, string[] WagonDataLog)
        {
            try
            {
                int StationNumber = IntConvertion(WagonDataLog[1]);//Read Station Number
                int LoadType = IntConvertion(WagonDataLog[2]);//Read Loadt type
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WagonGetEventDataLog() LoadNO:" + LoadNumber + " StationNo:" + StationNumber + " LoadType" + LoadType, DateTime.Now.ToString(), "", null, true);


                if (StationNumber == 1)//Loading Station insert data in LoadData Table
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WagonGetEventDataLog() Loading Station :  " + WagonDataLog[0], DateTime.Now.ToString(), "LoadNumber value : " + LoadNumber + " stationno=" + StationNumber, null, true);
                    ServiceResponse<DataTable> isLoadNumberPresentInLoadData = IndiSCADADataAccess.DataAccessSelect.IsLoadNumberPresentInLoadData(LoadNumber);//check Load No is Present In LoadData
                    if (isLoadNumberPresentInLoadData.Response != null)
                    {
                        if (isLoadNumberPresentInLoadData.Response.Rows.Count == 0)
                        {
                            string LoadNumberWithTime = GetLoadNoWithTime(WagonDataLog);//GetLoadNo Load No from array

                            //No Load Number is not present, insert laod details in Load Data.
                            LoadDataEntity _InsertLoadData = new LoadDataEntity();
                            ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WagonGetEventDataLog() isLoadNumberPresentInLoadData ==null " + WagonDataLog[0], DateTime.Now.ToString(), "LoadNumber value : " + LoadNumber + "Datetime :" + LoadGetPutEventDateTime, null, true);
                            _InsertLoadData.LoadNumber = LoadNumber;
                            _InsertLoadData.LoadNumberWithTime = LoadNumberWithTime;
                            _InsertLoadData.LoadType = LoadType;
                            _InsertLoadData.Operator = IndiSCADAGlobalLibrary.UserLoginDetails.UserName;
                            _InsertLoadData.LoadInTime = LoadGetPutEventDateTime;
                            _InsertLoadData.LoadOutTime = LoadGetPutEventDateTime;
                            _InsertLoadData.LoadInShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            _InsertLoadData.LoadOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            _InsertLoadData.isStart = true;
                            _InsertLoadData.isEnd = false;
                            _InsertLoadData.isStationExceedTime = false;
                            _InsertLoadData.LastCycleTime =  IndiSCADAGlobalLibrary.TagList.LastCycleTime[1];

                            // Add load details in LoadData and part details in LoadPartDetails table                            
                            ErrorLogger.LogError.ErrorLog("CommunicationWithPLC InsertLoadData() LoadNumber" + LoadNumber, DateTime.Now.ToString(), "", null, true);
                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessInsert.InsertLoadData(_InsertLoadData);//This will insert data to LoadData Table                        
                            try
                            {
                                ServiceResponse<DataTable> resultDB = IndiSCADADataAccess.DataAccessSelect.IsLoadNumberPresentInLoadData(LoadNumber);
                                DataTable dtParts = resultDB.Response;
                                if (dtParts != null)
                                {
                                    if (dtParts.Rows.Count > 0)
                                    {
                                        DataTable dt = (IndiSCADADataAccess.DataAccessSelect.getDataNextLoadMasterSettingsForDataLogging()).Response;
                                        DataTable dtPartData = (IndiSCADADataAccess.DataAccessSelect.getDataPartMasterForDataLogging()).Response;
                                        ServiceResponse<int> _InsertResult = (IndiSCADADataAccess.DataAccessInsert.insertLoadNoToLoadPartDetails(LoadNumber, dt, dtPartData));

                                        if (_InsertResult.Status == ResponseType.E)
                                        {
                                            ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WagonGetEventDataLog(InsertLoadPartDetails)", DateTime.Now.ToString(), _InsertResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                                        }
                                        // else if (_InsertResult.Status == ResponseType.S)  //If you dont want to repeate code then comment
                                        // {
                                        // string query = "Update PartMaster set isSelectedforNextLoad=0, NoOfHangers = '', PiecesPerHanger ='' , Quantity = '', Anodic1Current = '', Anodic2Current = '', AlkalineZincCurrent = '' where isSelectedforNextLoad = 1";
                                        // ServiceResponse<int> _UpdateResult = (IndiSCADADataAccess.DataAccessUpdate.UpdatePartMasterData(query));
                                        // }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WagonGetEventDataLog(InsertLoadPartDetails)", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WagonGetEventDataLog(InsertLoadData)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                    }
                    else
                    {
                        //Yes load no is present ignore 
                        return;
                    }
                }
                else //update load out time , temp, rectifier values to laod dip time table
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WagonGetEventDataLog() Else Part  " + WagonDataLog[0], DateTime.Now.ToString(), "LoadNumber value : " + LoadNumber + " stationno=" + StationNumber, null, true);
                    if (StationNumber != 69)//do not update unloading station data in load data table
                    {
                        LoadDipTimeEntity UpdateLoadDipTime = new LoadDipTimeEntity();
                        //Soak Degreasing temp1

                        //out of range 
                        try
                        {
                            FetchOutOfRangeValues(StationNumber, LoadNumber);
                        }
                        catch (Exception)
                        {

                        }

                       
                        if (StationNumber == 2)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;
                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[0]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[0]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[0]);

                            try
                            {
                                UpdateLoadDipTime.ORTempLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowSP[0]);
                                UpdateLoadDipTime.ORTempHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighSP[0]);
                                UpdateLoadDipTime.ORTempAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempAvg[0]);
                                UpdateLoadDipTime.ORTempLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowTime[0]);
                                UpdateLoadDipTime.ORTempHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighTime[0]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 2)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperature(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 2)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                        else if (StationNumber == 3 || StationNumber == 4)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;
                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[1]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[1]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[1]);

                            try
                            {
                                UpdateLoadDipTime.ORTempLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowSP[1]);
                                UpdateLoadDipTime.ORTempHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighSP[1]);
                                UpdateLoadDipTime.ORTempAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempAvg[1]);
                                UpdateLoadDipTime.ORTempLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowTime[1]);
                                UpdateLoadDipTime.ORTempHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighTime[1]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 3 and 4)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperature(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 3 and 4)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        } 
                        else if ((StationNumber == 6))
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;
                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[2]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[2]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[2]); 

                            try
                            {
                                UpdateLoadDipTime.ORTempLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowSP[2]);
                                UpdateLoadDipTime.ORTempHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighSP[2]);
                                UpdateLoadDipTime.ORTempAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempAvg[2]);
                                UpdateLoadDipTime.ORTempLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowTime[2]);
                                UpdateLoadDipTime.ORTempHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighTime[2]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 6)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperature(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 6)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }

                        else if ((StationNumber == 7))
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[3]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[3]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[3]);
                            
                            UpdateLoadDipTime.ActualVoltage = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualVoltage[0]);
                            UpdateLoadDipTime.ActualCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualCurrent[0]);
                            UpdateLoadDipTime.AvgCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AvgCurrentValues[0]);
                            UpdateLoadDipTime.SetCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AutoCurrentSP[0]);//AutoCurrentSP
                            UpdateLoadDipTime.AmpHr = float.Parse(IndiSCADAGlobalLibrary.TagList.RectifierAmpHr[0]);//AmpHr
                             

                            try
                            {
                                UpdateLoadDipTime.ORTempLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowSP[3]);
                                UpdateLoadDipTime.ORTempHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighSP[3]);
                                UpdateLoadDipTime.ORTempAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempAvg[3]);
                                UpdateLoadDipTime.ORTempLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowTime[3]);
                                UpdateLoadDipTime.ORTempHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighTime[3]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 7)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperatureRectifier(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 7)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                        else if ((StationNumber == 8))
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[4]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[4]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[4]);

                            UpdateLoadDipTime.ActualVoltage = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualVoltage[1]);
                            UpdateLoadDipTime.ActualCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualCurrent[1]);
                            UpdateLoadDipTime.AvgCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AvgCurrentValues[1]);
                            UpdateLoadDipTime.SetCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AutoCurrentSP[1]);
                            UpdateLoadDipTime.AmpHr = float.Parse(IndiSCADAGlobalLibrary.TagList.RectifierAmpHr[1]);//AmpHr

                            try
                            {
                                UpdateLoadDipTime.ORTempLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowSP[4]);
                                UpdateLoadDipTime.ORTempHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighSP[4]);
                                UpdateLoadDipTime.ORTempAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempAvg[4]);
                                UpdateLoadDipTime.ORTempLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowTime[4]);
                                UpdateLoadDipTime.ORTempHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighTime[4]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 8)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperatureRectifier(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 8)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                        else if (StationNumber == 14)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[5]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[5]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[5]);

                            UpdateLoadDipTime.ActualVoltage = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualVoltage[2]);
                            UpdateLoadDipTime.ActualCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualCurrent[2]);
                            UpdateLoadDipTime.AvgCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AvgCurrentValues[2]);
                            UpdateLoadDipTime.SetCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AutoCurrentSP[2]);
                            UpdateLoadDipTime.AmpHr = float.Parse(IndiSCADAGlobalLibrary.TagList.RectifierAmpHr[2]);//AmpHr

                            try
                            {
                                UpdateLoadDipTime.ORTempLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowSP[5]);
                                UpdateLoadDipTime.ORTempHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighSP[5]);
                                UpdateLoadDipTime.ORTempAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempAvg[5]);
                                UpdateLoadDipTime.ORTempLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowTime[5]);
                                UpdateLoadDipTime.ORTempHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighTime[5]);

                                //UpdateLoadDipTime.ORCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORCurrentAvg[0]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 14)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperatureRectifier(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 14)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        } 
                        else if (StationNumber == 21)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;
                            
                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[6]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[6]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[6]);

                            UpdateLoadDipTime.ActualVoltage = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualVoltage[3]);
                            UpdateLoadDipTime.ActualCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualCurrent[3]);
                            UpdateLoadDipTime.AvgCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AvgCurrentValues[3]);
                            UpdateLoadDipTime.SetCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AutoCurrentSP[3]);
                            UpdateLoadDipTime.AmpHr = float.Parse(IndiSCADAGlobalLibrary.TagList.RectifierAmpHr[3]);//AmpHr

                            try
                            {
                                UpdateLoadDipTime.ORpHLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowSP[6]);
                                UpdateLoadDipTime.ORpHHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighSP[6]);
                                UpdateLoadDipTime.ORpHLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowTime[6]);
                                UpdateLoadDipTime.ORpHHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighTime[6]);
                                UpdateLoadDipTime.ORpHAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHAvg[6]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 21)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperatureRectifier(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 21)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                        else if (StationNumber == 22)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[6]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[6]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[6]);

                            UpdateLoadDipTime.ActualVoltage = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualVoltage[4]);
                            UpdateLoadDipTime.ActualCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualCurrent[4]);
                            UpdateLoadDipTime.AvgCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AvgCurrentValues[4]);
                            UpdateLoadDipTime.SetCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AutoCurrentSP[4]);
                            UpdateLoadDipTime.AmpHr = float.Parse(IndiSCADAGlobalLibrary.TagList.RectifierAmpHr[4]);//AmpHr

                            try
                            {
                                UpdateLoadDipTime.ORpHLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowSP[6]);
                                UpdateLoadDipTime.ORpHHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighSP[6]);
                                UpdateLoadDipTime.ORpHLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowTime[6]);
                                UpdateLoadDipTime.ORpHHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighTime[6]);
                                UpdateLoadDipTime.ORpHAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHAvg[6]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 22)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperatureRectifier(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 22)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                        else if (StationNumber == 23)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[7]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[7]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[7]);

                            UpdateLoadDipTime.ActualVoltage = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualVoltage[5]);
                            UpdateLoadDipTime.ActualCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualCurrent[5]);
                            UpdateLoadDipTime.AvgCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AvgCurrentValues[5]);
                            UpdateLoadDipTime.SetCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AutoCurrentSP[5]);
                            UpdateLoadDipTime.AmpHr = float.Parse(IndiSCADAGlobalLibrary.TagList.RectifierAmpHr[5]);//AmpHr

                            try
                            {
                                UpdateLoadDipTime.ORpHLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowSP[7]);
                                UpdateLoadDipTime.ORpHHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighSP[7]);
                                UpdateLoadDipTime.ORpHLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowTime[7]);
                                UpdateLoadDipTime.ORpHHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighTime[7]);
                                UpdateLoadDipTime.ORpHAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHAvg[7]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 23)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperatureRectifier(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 23)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                        else if (StationNumber == 24)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[7]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[7]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[7]);

                            UpdateLoadDipTime.ActualVoltage = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualVoltage[6]);
                            UpdateLoadDipTime.ActualCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualCurrent[6]);
                            UpdateLoadDipTime.AvgCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AvgCurrentValues[6]);
                            UpdateLoadDipTime.SetCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AutoCurrentSP[6]);
                            UpdateLoadDipTime.AmpHr = float.Parse(IndiSCADAGlobalLibrary.TagList.RectifierAmpHr[6]);//AmpHr

                            try
                            {
                                UpdateLoadDipTime.ORpHLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowSP[7]);
                                UpdateLoadDipTime.ORpHHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighSP[7]);
                                UpdateLoadDipTime.ORpHLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowTime[7]);
                                UpdateLoadDipTime.ORpHHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighTime[7]);
                                UpdateLoadDipTime.ORpHAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHAvg[7]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 24)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperatureRectifier(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 24)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                        else if (StationNumber == 25)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[8]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[8]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[8]);

                            UpdateLoadDipTime.ActualVoltage = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualVoltage[7]);
                            UpdateLoadDipTime.ActualCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualCurrent[7]);
                            UpdateLoadDipTime.AvgCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AvgCurrentValues[7]);
                            UpdateLoadDipTime.SetCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AutoCurrentSP[7]);
                            UpdateLoadDipTime.AmpHr = float.Parse(IndiSCADAGlobalLibrary.TagList.RectifierAmpHr[7]);//AmpHr

                            try
                            {
                                UpdateLoadDipTime.ORpHLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowSP[8]);
                                UpdateLoadDipTime.ORpHHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighSP[8]);
                                UpdateLoadDipTime.ORpHLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowTime[8]);
                                UpdateLoadDipTime.ORpHHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighTime[8]);
                                UpdateLoadDipTime.ORpHAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHAvg[8]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 25)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperatureRectifier(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 25)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                        else if (StationNumber == 26)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[8]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[8]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[8]);

                            UpdateLoadDipTime.ActualVoltage = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualVoltage[8]);
                            UpdateLoadDipTime.ActualCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualCurrent[8]);
                            UpdateLoadDipTime.AvgCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AvgCurrentValues[8]);
                            UpdateLoadDipTime.SetCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AutoCurrentSP[8]);
                            UpdateLoadDipTime.AmpHr = float.Parse(IndiSCADAGlobalLibrary.TagList.RectifierAmpHr[8]);//AmpHr

                            try
                            {
                                UpdateLoadDipTime.ORpHLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowSP[8]);
                                UpdateLoadDipTime.ORpHHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighSP[8]);
                                UpdateLoadDipTime.ORpHLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowTime[8]);
                                UpdateLoadDipTime.ORpHHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighTime[8]);
                                UpdateLoadDipTime.ORpHAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHAvg[8]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 26)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperatureRectifier(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 26)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                        else if (StationNumber == 27)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[9]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[9]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[9]);

                            UpdateLoadDipTime.ActualVoltage = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualVoltage[9]);
                            UpdateLoadDipTime.ActualCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualCurrent[9]);
                            UpdateLoadDipTime.AvgCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AvgCurrentValues[9]);
                            UpdateLoadDipTime.SetCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AutoCurrentSP[9]);
                            UpdateLoadDipTime.AmpHr = float.Parse(IndiSCADAGlobalLibrary.TagList.RectifierAmpHr[9]);//AmpHr

                            try
                            {
                                UpdateLoadDipTime.ORpHLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowSP[9]);
                                UpdateLoadDipTime.ORpHHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighSP[9]);
                                UpdateLoadDipTime.ORpHLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowTime[9]);
                                UpdateLoadDipTime.ORpHHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighTime[9]);
                                UpdateLoadDipTime.ORpHAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHAvg[9]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 27)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperatureRectifier(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 27)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                        else if (StationNumber == 28)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[9]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[9]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[9]);

                            UpdateLoadDipTime.ActualVoltage = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualVoltage[10]);
                            UpdateLoadDipTime.ActualCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualCurrent[10]);
                            UpdateLoadDipTime.AvgCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AvgCurrentValues[10]);
                            UpdateLoadDipTime.SetCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AutoCurrentSP[10]);
                            UpdateLoadDipTime.AmpHr = float.Parse(IndiSCADAGlobalLibrary.TagList.RectifierAmpHr[10]);//AmpHr

                            try
                            {
                                UpdateLoadDipTime.ORpHLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowSP[9]);
                                UpdateLoadDipTime.ORpHHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighSP[9]);
                                UpdateLoadDipTime.ORpHLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowTime[9]);
                                UpdateLoadDipTime.ORpHHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighTime[9]);
                                UpdateLoadDipTime.ORpHAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHAvg[9]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 28)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperatureRectifier(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 28)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                        else if (StationNumber == 29)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[10]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[10]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[10]);

                            UpdateLoadDipTime.ActualVoltage = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualVoltage[11]);
                            UpdateLoadDipTime.ActualCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualCurrent[11]);
                            UpdateLoadDipTime.AvgCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AvgCurrentValues[11]);
                            UpdateLoadDipTime.SetCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AutoCurrentSP[11]);
                            UpdateLoadDipTime.AmpHr = float.Parse(IndiSCADAGlobalLibrary.TagList.RectifierAmpHr[11]);//AmpHr

                            try
                            {
                                UpdateLoadDipTime.ORpHLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowSP[10]);
                                UpdateLoadDipTime.ORpHHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighSP[10]);
                                UpdateLoadDipTime.ORpHLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowTime[10]);
                                UpdateLoadDipTime.ORpHHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighTime[10]);
                                UpdateLoadDipTime.ORpHAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHAvg[10]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 29)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperatureRectifier(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 29)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                        else if (StationNumber == 30)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[10]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[10]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[10]);

                            UpdateLoadDipTime.ActualVoltage = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualVoltage[12]);
                            UpdateLoadDipTime.ActualCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualCurrent[12]);
                            UpdateLoadDipTime.AvgCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AvgCurrentValues[12]);
                            UpdateLoadDipTime.SetCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AutoCurrentSP[12]);
                            UpdateLoadDipTime.AmpHr = float.Parse(IndiSCADAGlobalLibrary.TagList.RectifierAmpHr[12]);//AmpHr

                            try
                            {
                                UpdateLoadDipTime.ORpHLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowSP[10]);
                                UpdateLoadDipTime.ORpHHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighSP[10]);
                                UpdateLoadDipTime.ORpHLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowTime[10]);
                                UpdateLoadDipTime.ORpHHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighTime[10]);
                                UpdateLoadDipTime.ORpHAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHAvg[10]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 30)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperatureRectifier(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 30)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                        else if (StationNumber == 31)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[11]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[11]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[11]);

                            UpdateLoadDipTime.ActualVoltage = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualVoltage[13]);
                            UpdateLoadDipTime.ActualCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualCurrent[13]);
                            UpdateLoadDipTime.AvgCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AvgCurrentValues[13]);
                            UpdateLoadDipTime.SetCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AutoCurrentSP[13]);
                            UpdateLoadDipTime.AmpHr = float.Parse(IndiSCADAGlobalLibrary.TagList.RectifierAmpHr[13]);//AmpHr

                            try
                            {
                                UpdateLoadDipTime.ORpHLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowSP[11]);
                                UpdateLoadDipTime.ORpHHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighSP[11]);
                                UpdateLoadDipTime.ORpHLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowTime[11]);
                                UpdateLoadDipTime.ORpHHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighTime[11]);
                                UpdateLoadDipTime.ORpHAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHAvg[11]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 31)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperatureRectifier(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 31)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                        else if (StationNumber == 32)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[11]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[11]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[11]);

                            UpdateLoadDipTime.ActualVoltage = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualVoltage[14]);
                            UpdateLoadDipTime.ActualCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualCurrent[14]);
                            UpdateLoadDipTime.AvgCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AvgCurrentValues[14]);
                            UpdateLoadDipTime.SetCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AutoCurrentSP[14]);
                            UpdateLoadDipTime.AmpHr = float.Parse(IndiSCADAGlobalLibrary.TagList.RectifierAmpHr[14]);//AmpHr

                            try
                            {
                                UpdateLoadDipTime.ORpHLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowSP[11]);
                                UpdateLoadDipTime.ORpHHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighSP[11]);
                                UpdateLoadDipTime.ORpHLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowTime[11]);
                                UpdateLoadDipTime.ORpHHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighTime[11]);
                                UpdateLoadDipTime.ORpHAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHAvg[11]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 32)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperatureRectifier(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 32)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                        else if (StationNumber == 33)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[12]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[12]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[12]);

                            UpdateLoadDipTime.ActualVoltage = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualVoltage[15]);
                            UpdateLoadDipTime.ActualCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualCurrent[15]);
                            UpdateLoadDipTime.AvgCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AvgCurrentValues[15]);
                            UpdateLoadDipTime.SetCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AutoCurrentSP[15]);
                            UpdateLoadDipTime.AmpHr = float.Parse(IndiSCADAGlobalLibrary.TagList.RectifierAmpHr[15]);//AmpHr

                            try
                            {
                                UpdateLoadDipTime.ORpHLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowSP[12]);
                                UpdateLoadDipTime.ORpHHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighSP[12]);
                                UpdateLoadDipTime.ORpHLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowTime[12]);
                                UpdateLoadDipTime.ORpHHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighTime[12]);
                                UpdateLoadDipTime.ORpHAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHAvg[12]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 33)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperatureRectifier(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 33)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                        else if (StationNumber == 34)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[12]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[12]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[12]);

                            UpdateLoadDipTime.ActualVoltage = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualVoltage[16]);
                            UpdateLoadDipTime.ActualCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualCurrent[16]);
                            UpdateLoadDipTime.AvgCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AvgCurrentValues[16]);
                            UpdateLoadDipTime.SetCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AutoCurrentSP[16]);
                            UpdateLoadDipTime.AmpHr = float.Parse(IndiSCADAGlobalLibrary.TagList.RectifierAmpHr[16]);//AmpHr

                            try
                            {
                                UpdateLoadDipTime.ORpHLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowSP[12]);
                                UpdateLoadDipTime.ORpHHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighSP[12]);
                                UpdateLoadDipTime.ORpHLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowTime[12]);
                                UpdateLoadDipTime.ORpHHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighTime[12]);
                                UpdateLoadDipTime.ORpHAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHAvg[12]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 34)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperatureRectifier(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 34)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }

                        else if (StationNumber == 35)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[13]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[13]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[13]);

                            UpdateLoadDipTime.ActualVoltage = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualVoltage[17]);
                            UpdateLoadDipTime.ActualCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualCurrent[17]);
                            UpdateLoadDipTime.AvgCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AvgCurrentValues[17]);
                            UpdateLoadDipTime.SetCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AutoCurrentSP[17]);
                            UpdateLoadDipTime.AmpHr = float.Parse(IndiSCADAGlobalLibrary.TagList.RectifierAmpHr[17]);//AmpHr

                            try
                            {
                                UpdateLoadDipTime.ORpHLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowSP[13]);
                                UpdateLoadDipTime.ORpHHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighSP[13]);
                                UpdateLoadDipTime.ORpHLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowTime[13]);
                                UpdateLoadDipTime.ORpHHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighTime[13]);
                                UpdateLoadDipTime.ORpHAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHAvg[13]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 35)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperatureRectifier(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 35)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }

                        else if (StationNumber == 36)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[13]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[13]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[13]);

                            UpdateLoadDipTime.ActualVoltage = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualVoltage[18]);
                            UpdateLoadDipTime.ActualCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualCurrent[18]);
                            UpdateLoadDipTime.AvgCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AvgCurrentValues[18]);
                            UpdateLoadDipTime.SetCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AutoCurrentSP[18]);
                            UpdateLoadDipTime.AmpHr = float.Parse(IndiSCADAGlobalLibrary.TagList.RectifierAmpHr[18]);//AmpHr

                            try
                            {
                                UpdateLoadDipTime.ORpHLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowSP[13]);
                                UpdateLoadDipTime.ORpHHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighSP[13]);
                                UpdateLoadDipTime.ORpHLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowTime[13]);
                                UpdateLoadDipTime.ORpHHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighTime[13]);
                                UpdateLoadDipTime.ORpHAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHAvg[13]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 36)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperatureRectifier(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 36)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                        else if (StationNumber == 37)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[14]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[14]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[14]);

                            UpdateLoadDipTime.ActualVoltage = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualVoltage[19]);
                            UpdateLoadDipTime.ActualCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualCurrent[19]);
                            UpdateLoadDipTime.AvgCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AvgCurrentValues[19]);
                            UpdateLoadDipTime.SetCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AutoCurrentSP[19]);
                            UpdateLoadDipTime.AmpHr = float.Parse(IndiSCADAGlobalLibrary.TagList.RectifierAmpHr[19]);//AmpHr

                            try
                            {
                                UpdateLoadDipTime.ORpHLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowSP[14]);
                                UpdateLoadDipTime.ORpHHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighSP[14]);
                                UpdateLoadDipTime.ORpHLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowTime[14]);
                                UpdateLoadDipTime.ORpHHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighTime[14]);
                                UpdateLoadDipTime.ORpHAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHAvg[14]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 37)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperatureRectifier(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 37)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                        else if (StationNumber == 38)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[14]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[14]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[14]);

                            UpdateLoadDipTime.ActualVoltage = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualVoltage[20]);
                            UpdateLoadDipTime.ActualCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.ActualCurrent[20]);
                            UpdateLoadDipTime.AvgCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AvgCurrentValues[20]);
                            UpdateLoadDipTime.SetCurrent = IntConvertion(IndiSCADAGlobalLibrary.TagList.AutoCurrentSP[20]);
                            UpdateLoadDipTime.AmpHr = float.Parse(IndiSCADAGlobalLibrary.TagList.RectifierAmpHr[20]);//AmpHr

                            try
                            {
                                UpdateLoadDipTime.ORpHLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowSP[14]);
                                UpdateLoadDipTime.ORpHHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighSP[14]);
                                UpdateLoadDipTime.ORpHLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowTime[14]);
                                UpdateLoadDipTime.ORpHHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighTime[14]);
                                UpdateLoadDipTime.ORpHAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHAvg[14]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 38)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperatureRectifier(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 38)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        } 
               
                        else if (StationNumber == 44)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true; 

                            UpdateLoadDipTime.ActualpH = float.Parse(IndiSCADAGlobalLibrary.TagList.pHActual[0]);

                            try
                            {
                                UpdateLoadDipTime.ORpHLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowSP[0]);
                                UpdateLoadDipTime.ORpHHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighSP[0]);
                                UpdateLoadDipTime.ORpHLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowTime[0]);
                                UpdateLoadDipTime.ORpHHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighTime[0]);
                                UpdateLoadDipTime.ORpHAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHAvg[0]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 44)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithpH(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 44)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                    
                        else if (StationNumber == 47)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[15]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[15]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[15]);

                            UpdateLoadDipTime.ActualpH = float.Parse(IndiSCADAGlobalLibrary.TagList.pHActual[1]);

                            try
                            {
                                UpdateLoadDipTime.ORTempLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowSP[15]);
                                UpdateLoadDipTime.ORTempHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighSP[15]);
                                UpdateLoadDipTime.ORTempAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempAvg[15]);
                                UpdateLoadDipTime.ORTempLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowTime[15]);
                                UpdateLoadDipTime.ORTempHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighTime[15]);

                                UpdateLoadDipTime.ORpHLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowSP[1]);
                                UpdateLoadDipTime.ORpHHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighSP[1]);
                                UpdateLoadDipTime.ORpHLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowTime[1]);
                                UpdateLoadDipTime.ORpHHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighTime[1]);
                                UpdateLoadDipTime.ORpHAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHAvg[1]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 47)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperaturePH(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 47)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                        //Silver Black PASS 3....ph3,temp5
                        else if (StationNumber == 51)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;
                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[16]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[16]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[16]);
                            UpdateLoadDipTime.ActualpH = float.Parse(IndiSCADAGlobalLibrary.TagList.pHActual[2]);

                            try
                            {
                                UpdateLoadDipTime.ORTempLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowSP[16]);
                                UpdateLoadDipTime.ORTempHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighSP[16]);
                                UpdateLoadDipTime.ORTempAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempAvg[16]);
                                UpdateLoadDipTime.ORTempLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowTime[16]);
                                UpdateLoadDipTime.ORTempHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighTime[16]);

                                UpdateLoadDipTime.ORpHLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowSP[2]);
                                UpdateLoadDipTime.ORpHHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighSP[2]);
                                UpdateLoadDipTime.ORpHLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowTime[2]);
                                UpdateLoadDipTime.ORpHHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighTime[2]);
                                UpdateLoadDipTime.ORpHAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHAvg[2]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 51)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperaturePH(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 51)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
             
                        else if (StationNumber == 55)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;
                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[17]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[17]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[17]);

                            UpdateLoadDipTime.ActualpH = float.Parse(IndiSCADAGlobalLibrary.TagList.pHActual[3]);

                            try
                            {
                                UpdateLoadDipTime.ORTempLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowSP[17]);
                                UpdateLoadDipTime.ORTempHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighSP[17]);
                                UpdateLoadDipTime.ORTempAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempAvg[17]);
                                UpdateLoadDipTime.ORTempLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowTime[17]);
                                UpdateLoadDipTime.ORTempHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighTime[17]);

                                UpdateLoadDipTime.ORpHLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowSP[3]);
                                UpdateLoadDipTime.ORpHHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighSP[3]);
                                UpdateLoadDipTime.ORpHLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowTime[3]);
                                UpdateLoadDipTime.ORpHHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighTime[3]);
                                UpdateLoadDipTime.ORpHAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHAvg[3]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 55)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperaturePH(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 55)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        } 
                        else if (StationNumber == 59)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[18]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[18]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[18]);

                            try
                            {
                                UpdateLoadDipTime.ORTempLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowSP[18]);
                                UpdateLoadDipTime.ORTempHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighSP[18]);
                                UpdateLoadDipTime.ORTempAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempAvg[18]);
                                UpdateLoadDipTime.ORTempLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowTime[18]);
                                UpdateLoadDipTime.ORTempHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighTime[18]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 59)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperature(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 59)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        } 
                        else if (StationNumber == 63)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[19]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[19]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[19]);

                            try
                            {
                                UpdateLoadDipTime.ORTempLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowSP[19]);
                                UpdateLoadDipTime.ORTempHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighSP[19]);
                                UpdateLoadDipTime.ORTempAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempAvg[19]);
                                UpdateLoadDipTime.ORTempLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowTime[19]);
                                UpdateLoadDipTime.ORTempHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighTime[19]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 63)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperature(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 63)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                        else if (StationNumber == 64)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[20]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[20]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[20]);

                            try
                            {
                                UpdateLoadDipTime.ORTempLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowSP[20]);
                                UpdateLoadDipTime.ORTempHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighSP[20]);
                                UpdateLoadDipTime.ORTempAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempAvg[20]);
                                UpdateLoadDipTime.ORTempLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowTime[20]);
                                UpdateLoadDipTime.ORTempHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighTime[20]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 64)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperature(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 64)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                        else if (StationNumber == 65)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[21]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[21]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[21]);

                            try
                            {
                                UpdateLoadDipTime.ORTempLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowSP[21]);
                                UpdateLoadDipTime.ORTempHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighSP[21]);
                                UpdateLoadDipTime.ORTempAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempAvg[21]);
                                UpdateLoadDipTime.ORTempLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowTime[21]);
                                UpdateLoadDipTime.ORTempHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighTime[21]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 65)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperature(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 65)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }

                        else if (StationNumber == 66)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[22]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[22]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[22]);

                            try
                            {
                                UpdateLoadDipTime.ORTempLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowSP[22]);
                                UpdateLoadDipTime.ORTempHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighSP[22]);
                                UpdateLoadDipTime.ORTempAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempAvg[22]);
                                UpdateLoadDipTime.ORTempLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowTime[22]);
                                UpdateLoadDipTime.ORTempHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighTime[22]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 66)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperature(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 66)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }

                        else if (StationNumber == 67)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[23]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[23]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[23]);

                            try
                            {
                                UpdateLoadDipTime.ORTempLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowSP[23]);
                                UpdateLoadDipTime.ORTempHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighSP[23]);
                                UpdateLoadDipTime.ORTempAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempAvg[23]);
                                UpdateLoadDipTime.ORTempLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowTime[23]);
                                UpdateLoadDipTime.ORTempHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighTime[23]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 67)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperature(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 67)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                        else if (StationNumber == 68)
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;

                            UpdateLoadDipTime.TemperatureActual = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureActual[24]);
                            UpdateLoadDipTime.TemperatureSetHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureHighSP[24]);
                            UpdateLoadDipTime.TemperatureSetLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.TemperatureLowSP[24]);

                            try
                            {
                                UpdateLoadDipTime.ORTempLowSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowSP[24]);
                                UpdateLoadDipTime.ORTempHighSP = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighSP[24]);
                                UpdateLoadDipTime.ORTempAvg = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempAvg[24]);
                                UpdateLoadDipTime.ORTempLowTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowTime[24]);
                                UpdateLoadDipTime.ORTempHighTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighTime[24]);

                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 68)", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTimeWithTemperature(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station 68)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }

                        else
                        {
                            UpdateLoadDipTime.LoadNumber = LoadNumber;
                            UpdateLoadDipTime.StationNo = StationNumber;
                            UpdateLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                            UpdateLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                            UpdateLoadDipTime.Status = true;
                            try
                            {
                                UpdateLoadDipTime.ORDipTime = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTime[StationNumber - 1]);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(Station " + StationNumber + ")", DateTime.Now.ToString(), "Error While inserting out of range values", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                            ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDipTime(UpdateLoadDipTime);
                            if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(" + StationNumber + ")", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                    }
                }

                //update Part No Value
                UpdatePartNumberEntryOnGetEvent(StationNumber, LoadNumber);

            }
            catch (Exception exWagonGetEventDataLog)
            {
                ErrorLogger.LogError.ErrorLog("Error CommunicationWithPLC WagonGetEventDataLog() for loadNo :" + LoadNumber + " And LoadGetPUt datetime" + LoadGetPutEventDateTime, DateTime.Now.ToString(), exWagonGetEventDataLog.Message, null, true);
            }
        }

        private static void FetchOutOfRangeValues(int StationNumber, string LoadNumber)
        {
            try
            {
                #region out of range

                int ORTempLowCount = 0, ORTempHighCount = 0, ORpHLowCount = 0, ORpHHighCount = 0, ORCurrentLow = 0, ORCurrentHigh = 0, ORDiptimeLow = 0, ORDiptimeHigh = 0;
                bool ORLowBypassTemp = false, ORHighBypassTemp = false, ORpHLowBypass = false, ORpHHighBypass = false;
                string dipintime = "", dipsettime = "";
                int istemp = 0, istime = 0, isph = 0, iscurr = 0;
                bool isOutofrange = false;



                // OUT OF RANGE VALUES ASSIGN
                try
                {
                    #region Temperature value assign

                    if (StationNumber == 2)
                    {
                        ORTempLowCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowCount[0]);
                        ORTempHighCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighCount[0]);
                        ORLowBypassTemp = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORLowBypassTemp[0]);
                        ORHighBypassTemp = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORHighBypassTemp[0]);
                    }
                    else if (StationNumber == 4 || StationNumber == 5)
                    {
                        ORTempLowCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowCount[1]);
                        ORTempHighCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighCount[1]);
                        ORLowBypassTemp = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORLowBypassTemp[1]);
                        ORHighBypassTemp = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORHighBypassTemp[1]);
                    }
                    else if (StationNumber == 8)
                    {
                        ORTempLowCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowCount[2]);
                        ORTempHighCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighCount[2]);
                        ORLowBypassTemp = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORLowBypassTemp[2]);
                        ORHighBypassTemp = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORHighBypassTemp[2]);
                    }
                    else if (StationNumber == 18)
                    {
                        ORTempLowCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowCount[3]);
                        ORTempHighCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighCount[3]);
                        ORLowBypassTemp = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORLowBypassTemp[3]);
                        ORHighBypassTemp = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORHighBypassTemp[3]);
                    }
                    else if (StationNumber == 51)
                    {
                        ORTempLowCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowCount[4]);
                        ORTempHighCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighCount[4]);
                        ORLowBypassTemp = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORLowBypassTemp[4]);
                        ORHighBypassTemp = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORHighBypassTemp[4]);
                    }
                    else if (StationNumber == 55)
                    {
                        ORTempLowCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowCount[5]);
                        ORTempHighCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighCount[5]);
                        ORLowBypassTemp = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORLowBypassTemp[5]);
                        ORHighBypassTemp = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORHighBypassTemp[5]);
                    }
                    else if (StationNumber == 65)
                    {
                        ORTempLowCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowCount[6]);
                        ORTempHighCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighCount[6]);
                        ORLowBypassTemp = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORLowBypassTemp[6]);
                        ORHighBypassTemp = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORHighBypassTemp[6]);
                    }
                    else if (StationNumber == 67)
                    {
                        ORTempLowCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowCount[7]);
                        ORTempHighCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighCount[7]);
                        ORLowBypassTemp = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORLowBypassTemp[7]);
                        ORHighBypassTemp = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORHighBypassTemp[7]);
                    }
                    else if (StationNumber == 68)
                    {
                        ORTempLowCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowCount[8]);
                        ORTempHighCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighCount[8]);
                        ORLowBypassTemp = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORLowBypassTemp[8]);
                        ORHighBypassTemp = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORHighBypassTemp[8]);
                    }
                    else if (StationNumber == 69)
                    {
                        ORTempLowCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowCount[9]);
                        ORTempHighCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighCount[9]);
                        ORLowBypassTemp = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORLowBypassTemp[9]);
                        ORHighBypassTemp = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORHighBypassTemp[9]);
                    }
                    else if (StationNumber == 70)
                    {
                        ORTempLowCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempLowCount[10]);
                        ORTempHighCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORTempHighCount[10]);
                        ORLowBypassTemp = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORLowBypassTemp[10]);
                        ORHighBypassTemp = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORHighBypassTemp[10]);
                    }

                    #endregion

                    #region pH value assign
                    if (StationNumber == 46)
                    {
                        ORpHLowCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowCount[0]);
                        ORpHHighCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighCount[0]);
                        ORpHLowBypass = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORpHLowBypass[0]);
                        ORpHHighBypass = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORpHHighBypass[0]);
                    }
                    else if (StationNumber == 47)
                    {
                        ORpHLowCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowCount[1]);
                        ORpHHighCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighCount[1]);
                        ORpHLowBypass = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORpHLowBypass[1]);
                        ORpHHighBypass = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORpHHighBypass[1]);
                    }
                    else if (StationNumber == 51)
                    {
                        ORpHLowCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowCount[2]);
                        ORpHHighCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighCount[2]);
                        ORpHLowBypass = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORpHLowBypass[2]);
                        ORpHHighBypass = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORpHHighBypass[2]);
                    }
                    else if (StationNumber == 55)
                    {
                        ORpHLowCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowCount[3]);
                        ORpHHighCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighCount[3]);
                        ORpHLowBypass = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORpHLowBypass[3]);
                        ORpHHighBypass = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORpHHighBypass[3]);
                    }
                    else if (StationNumber == 59)
                    {
                        ORpHLowCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowCount[4]);
                        ORpHHighCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighCount[4]);
                        ORpHLowBypass = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORpHLowBypass[4]);
                        ORpHHighBypass = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORpHHighBypass[4]);
                    }
                    else if (StationNumber == 67)
                    {
                        ORpHLowCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowCount[5]);
                        ORpHHighCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighCount[5]);
                        ORpHLowBypass = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORpHLowBypass[5]);
                        ORpHHighBypass = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORpHHighBypass[5]);
                    }
                    else if (StationNumber == 68)
                    {
                        ORpHLowCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHLowCount[6]);
                        ORpHHighCount = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORpHHighCount[6]);
                        ORpHLowBypass = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORpHLowBypass[6]);
                        ORpHHighBypass = Convert.ToBoolean(IndiSCADAGlobalLibrary.TagList.ORpHHighBypass[6]);
                    }
                    #endregion

                    //  ORCurrentLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORCurrentLow[0]);
                    //  ORCurrentHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORCurrentHigh[0]);

                    ORDiptimeLow = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTimeLow[StationNumber - 1]);
                    ORDiptimeHigh = IntConvertion(IndiSCADAGlobalLibrary.TagList.ORDipTimeHigh[StationNumber - 1]);
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError.ErrorLog("Error in CommunicationWithPLC FetchOutOfRangeValues() OUT OF RANGE VALUES ASSIGN", DateTime.Now.ToString(), ex.Message, null, true);
                    ErrorLogger.LogError.ErrorLog("ORpHLowCount=" + ORpHLowCount + " ORpHHighCount=" + ORpHHighCount + " ORpHHighBypass=" + ORpHHighBypass + " ORpHLowBypass=" + ORpHLowBypass + " For stationNO=" + StationNumber + " LoadNumber" + LoadNumber + " ", DateTime.Now.ToString(), "", null, true);
                    ErrorLogger.LogError.ErrorLog("ORTempLowCount=" + ORTempLowCount + " ORTempHighCount=" + ORTempHighCount + " ORLowBypassTemp=" + ORLowBypassTemp + " ORHighBypassTemp=" + ORHighBypassTemp + " For stationNO=" + StationNumber + " LoadNumber" + LoadNumber + " ", DateTime.Now.ToString(), "", null, true);

                }

                try
                {
                    ErrorLogger.LogError.ErrorLog("Out of range values : ORTempLowCount=" + ORTempLowCount + " ORTempHighCount=" + ORTempHighCount + " ORpHLowCount=" + ORpHLowCount + " ORpHHighCount=" + ORpHHighCount + " ORCurrentLow=" + ORCurrentLow + " ORCurrentHigh=" + ORCurrentHigh + " ORDiptimeLow=" + ORDiptimeLow + " ORDiptimeHigh=" + ORDiptimeHigh + " For stationNO=" + StationNumber + " LoadNumber" + LoadNumber + " ", DateTime.Now.ToString(), "", null, true);

                    //update loaddata
                    if ((ORTempLowCount > 0) || (ORTempHighCount > 0) || (ORpHLowCount > 0) || (ORpHHighCount > 0) || (ORCurrentLow > 0) || (ORCurrentHigh > 0) || (ORDiptimeLow > 0) || (ORDiptimeHigh > 0))
                    {
                        if (((ORTempLowCount > 0) && (ORLowBypassTemp == false)) || ((ORTempHighCount > 0) && (ORHighBypassTemp == false)))
                        {
                            istemp = 1;
                            ErrorLogger.LogError.ErrorLog("istemp=1 Out of range values : ORTempLowCount=" + ORTempLowCount + " ORTempHighCount=" + ORTempHighCount + " ORLowBypassTemp=" + ORLowBypassTemp + " ORHighBypassTemp=" + ORHighBypassTemp + " For stationNO=" + StationNumber + " LoadNumber" + LoadNumber + " ", DateTime.Now.ToString(), "", null, true);
                        }
                        if (((ORpHLowCount > 0) && (ORpHHighBypass == false)) || ((ORpHHighCount > 0) && (ORpHLowBypass == false)))
                        {
                            isph = 1;
                            ErrorLogger.LogError.ErrorLog("isph=1 Out of range values : ORpHLowCount=" + ORpHLowCount + " ORpHHighCount=" + ORpHHighCount + " ORpHHighBypass=" + ORpHHighBypass + " ORpHLowBypass=" + ORpHLowBypass + " For stationNO=" + StationNumber + " LoadNumber" + LoadNumber + " ", DateTime.Now.ToString(), "", null, true);
                        }
                        //current logic is not in dham so commented SBS
                        //if ((ORCurrentLow > 0) || (ORCurrentHigh > 0))
                        //{
                        //    iscurr = 1;
                        //}
                        if ((ORDiptimeLow > 0) || (ORDiptimeHigh > 0))
                        {
                            istime = 1;
                            ErrorLogger.LogError.ErrorLog("istime=1 Out of range values : ORDiptimeLow=" + ORDiptimeLow + " ORDiptimeHigh=" + ORDiptimeHigh + " For stationNO=" + StationNumber + " LoadNumber" + LoadNumber + " ", DateTime.Now.ToString(), "", null, true);
                        }
                    }
                }
                catch (Exception ex)
                { ErrorLogger.LogError.ErrorLog("Error in CommunicationWithPLC FetchOutOfRangeValues()", DateTime.Now.ToString(), ex.Message, null, true); }


                try
                {
                    ServiceResponse<int> _QueryResult1 = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadDiptimeOR(LoadNumber, StationNumber, istemp, isph, iscurr, istime);
                    if (_QueryResult1.Status == ResponseType.E)//query excuted with some erros
                    {
                        ErrorLogger.LogError.ErrorLog("CommunicationWithPLC FetchOutOfRangeValues() UpdateLoadDiptimeOR", DateTime.Now.ToString(), _QueryResult1.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                catch (Exception ex)
                {

                }

                if (istemp == 1 || isph == 1 || iscurr == 1 || istime == 1)
                {
                    //objDBConnection.UpdateStationOutOfRange(LoadNo);

                    ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateStationOutOfRange(LoadNumber);
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLC FetchOutOfRangeValues() UpdateStationOutOfRange UPADTED", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);


                    if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                    {
                        ErrorLogger.LogError.ErrorLog("CommunicationWithPLC FetchOutOfRangeValues() UpdateStationOutOfRange", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                    isOutofrange = true;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC FetchOutOfRangeValues() ", DateTime.Now.ToString(), ex.Message, null, true);
            }
        }

        private static void UpdatePartNumberEntryOnGetEvent(int StationNumber, string LoadNumber)
        {
            try
            {
                IndiSCADAGlobalLibrary.TagList.PartNameAtStation[StationNumber - 1] = "NA";
                IndiSCADAGlobalLibrary.TagList.MECLnumberAtStation[StationNumber - 1] = "NA";
                IndiSCADAGlobalLibrary.TagList.LoadNoAtStation[StationNumber - 1] = "0";
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdatePartNumberEntryOnGetEvent() ", DateTime.Now.ToString(), ex.Message, null, true);
            }
        }
        private static void UpdatePartNumberEntryOnputEvent(int StationNumber, string LoadNumber)
        {
            try
            {
                string Runningpart = "NA"; string RunningMECLno = "NA";
                ServiceResponse<DataTable> QueryResult = IndiSCADADataAccess.DataAccessSelect.SelectPartNumberFromLoadPartDetails(LoadNumber);
                // ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdatePartNumberEntryOnputEvent(" + StationNumber + ")" + "LoadNumber = " + LoadNumber, DateTime.Now.ToString(),QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                if (QueryResult.Status == ResponseType.E)//query excuted with some erros
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdateLoadDipTime(" + StationNumber + ")", DateTime.Now.ToString(), QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
                else
                {
                    DataTable dtparts = QueryResult.Response;
                    if (dtparts != null)
                    {
                        if (dtparts.Rows.Count > 0)
                        {
                            Runningpart = dtparts.Rows[0]["PartNumber"].ToString();
                            try
                            {
                                for (int i = 1; i <= dtparts.Rows.Count - 1; i++)
                                {
                                    Runningpart += "-" + dtparts.Rows[i]["PartNumber"].ToString();
                                }
                            }
                            catch (Exception ex)
                            { ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdatePartNumberEntryOnputEvent() error while assigning Runningpart", DateTime.Now.ToString(), ex.Message, null, true); }

                            //try
                            //{
                            //    //RunningMECLno = dtparts.Rows[0]["MECLNumber"].ToString();
                            //}
                            //catch (Exception ex) { ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdatePartNumberEntryOnputEvent() error while assigning MECL no", DateTime.Now.ToString(), ex.Message, null, true); }

                        }
                    }
                    IndiSCADAGlobalLibrary.TagList.PartNameAtStation[StationNumber - 1] = Runningpart;
                    //IndiSCADAGlobalLibrary.TagList.MECLnumberAtStation[StationNumber - 1] = RunningMECLno;
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC UpdatePartNumberEntryOnputEvent() ", DateTime.Now.ToString(), ex.Message, null, true);
            }



        }
        //Put event data logging
        private static void WagonPutEventDataLog(string LoadNumber,  DateTime LoadGetPutEventDateTime, string[] WagonDataLog)
        {
            try
            {
                int StationNumber = IntConvertion(WagonDataLog[1]);//Read Station Number
                int LoadType = IntConvertion(WagonDataLog[2]);//Read Loadt type
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WagonPutEventDataLog() LoadNO:" + LoadNumber + " StationNo:" + StationNumber + " LoadType" + LoadType, DateTime.Now.ToString(), "", null, true);


                if (StationNumber == 69)//is station is UnLoading Station then Update  in LoadData Table Load Out Time
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WagonPutEventDataLog() Load put on UnLoading Station LoadNO:" + LoadNumber + " StationNo:" + StationNumber + " LoadType" + LoadType, DateTime.Now.ToString(), "", null, true);

                    LoadDataEntity _UpadateLoadData = new LoadDataEntity();
                    _UpadateLoadData.LoadNumber = LoadNumber;
                    _UpadateLoadData.LoadType = LoadType;
                    _UpadateLoadData.LoadOutTime = LoadGetPutEventDateTime;
                    _UpadateLoadData.LoadOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                    _UpadateLoadData.isEnd = true;
                    //   ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WagonGetEventDataLog(UpdateLoadData) Unloading loadnumber :(" + LoadNumber + " )", DateTime.Now.ToString(),"", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessUpdate.UpdateLoadData(_UpadateLoadData);//This will update data to LoadData Table against load number
                    if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                    {
                        ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WagonPutEventDataLog (UpdateLoadData)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }

                }
                else//Insert Loadip time
                {
                    if (StationNumber != 1)//do not insert loading station data in load data table
                    {
                        ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WagonPutEventDataLog() Station is not unloading/loading station LoadNO:" + LoadNumber + " StationNo:" + StationNumber + " LoadType" + LoadType, DateTime.Now.ToString(), "", null, true);


                        // check Load is present in LoadData or not 
                        ServiceResponse<DataTable> isLoadNumberPresentInLoadData = IndiSCADADataAccess.DataAccessSelect.IsLoadNumberPresentInLoadData(LoadNumber);//check Load No is Present In LoadData
                        if (isLoadNumberPresentInLoadData.Response != null)
                        {
                            if (isLoadNumberPresentInLoadData.Response.Rows.Count == 0)
                            {
                                string LoadNumberWithTime = GetLoadNoWithTime(WagonDataLog);//GetLoadNo Load No from array

                                //No Load Number is not present, insert laod details in Load Data.
                                LoadDataEntity _InsertLoadData = new LoadDataEntity();
                                _InsertLoadData.LoadNumber = LoadNumber;
                                _InsertLoadData.LoadNumberWithTime = LoadNumberWithTime;
                                _InsertLoadData.LoadType = LoadType;
                                _InsertLoadData.Operator = IndiSCADAGlobalLibrary.UserLoginDetails.UserName;
                                _InsertLoadData.LoadInTime = LoadGetPutEventDateTime;
                                _InsertLoadData.LoadOutTime = LoadGetPutEventDateTime;
                                _InsertLoadData.LoadInShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                                _InsertLoadData.LoadOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                                _InsertLoadData.isStart = true;
                                _InsertLoadData.isEnd = false;
                                _InsertLoadData.isStationExceedTime = false;

                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC LastCycleTime", DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                                _InsertLoadData.LastCycleTime =  IndiSCADAGlobalLibrary.TagList.LastCycleTime[1];
                                ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessInsert.InsertLoadData(_InsertLoadData);//This will insert data to LoadData Table
                                if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                                {
                                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WagonPutEventDataLog(InsertLoadData)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                                }
                            }
                        }
                        //
                        ServiceResponse<DataTable> isLoadNumberPresentInLoadDipTime = IndiSCADADataAccess.DataAccessSelect.IsLoadNumberPresentInLoadDipTime(LoadNumber, StationNumber);//check Load No is Present In LoadData
                        if (isLoadNumberPresentInLoadDipTime.Response != null)
                        {
                            if (isLoadNumberPresentInLoadDipTime.Response.Rows.Count == 0)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WagonPutEventDataLog() Insert new record in LoadDipTime table for LoadNO:" + LoadNumber + " StationNo:" + StationNumber + " LoadType" + LoadType, DateTime.Now.ToString(), "", null, true);
                                // Load number not present insert
                                LoadDipTimeEntity InsertLoadDipTime = new LoadDipTimeEntity();
                                InsertLoadDipTime.LoadNumber = LoadNumber;
                                InsertLoadDipTime.StationNo = StationNumber;
                                InsertLoadDipTime.DipOutTime = LoadGetPutEventDateTime;
                                InsertLoadDipTime.DipInTime = LoadGetPutEventDateTime;
                                InsertLoadDipTime.DipOutShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                                InsertLoadDipTime.DipInShift = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                                ServiceResponse<int> _QueryResult = IndiSCADADataAccess.DataAccessInsert.InsertLoadDipTime(InsertLoadDipTime);//This will insert in LoadDiptime Table 
                                if (_QueryResult.Status == ResponseType.E)//query excuted with some erros
                                {
                                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WagonPutEventDataLog(InsertLoadDipTime)", DateTime.Now.ToString(), _QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                                }
                            }
                        }
                        else
                        {
                            //Load is present do not insert again
                            return;
                        }
                    }
                }
                //update part no value
                UpdatePartNumberEntryOnputEvent(StationNumber, LoadNumber);
            }
            catch (Exception exWagonPutEventDataLog)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WagonPutEventDataLog() ", DateTime.Now.ToString(), exWagonPutEventDataLog.Message, null, true);
            }
        }
        //Get Load No and Date time of event
        private static string GetLoadNo(string[] WagonDataLog, string WagonName)
        {
            try
            {
                StringBuilder LoadNumber = new StringBuilder();
                LoadNumber.Append("20" + WagonDataLog[3] + "/");//Year 2020/
                LoadNumber.Append(WagonDataLog[10].Substring(0, WagonDataLog[10].Length - 2) + "/");//Month 2020/4/
                LoadNumber.Append(WagonDataLog[10].Substring(WagonDataLog[10].Length - 2) + "-");//Day 2020/4/1
                LoadNumber.Append(WagonDataLog[11]);//Date 2020/4/16-1
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC GetLoadNo() " + WagonName, DateTime.Now.ToString(), LoadNumber.ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return LoadNumber.ToString();
            }
            catch (Exception exProductionDataLog)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC GetLoadNo() " + WagonName, DateTime.Now.ToString(), exProductionDataLog.Message, null, true);
                return null;
            }
        }
        //Get Load No and Date time of event
        private static string GetLoadNoWithTime(string[] WagonDataLog)
        {
            try
            {
                StringBuilder LoadNumber = new StringBuilder();
                LoadNumber.Append("20" + WagonDataLog[3] + "/");//Year 2020/
                LoadNumber.Append(WagonDataLog[10].Substring(0, WagonDataLog[10].Length - 2) + "/");//Month 2020/4/
                LoadNumber.Append(WagonDataLog[10].Substring(WagonDataLog[10].Length - 2) + "-");//Day 2020/4/1-

                LoadNumber.Append(WagonDataLog[6] + ":");//HOUR 2020/4/1-15:
                LoadNumber.Append(WagonDataLog[7] + "-");//Minutes 2020/4/1-15:31-

                LoadNumber.Append(WagonDataLog[11]);//Date 2021/05/19-15:31-001 

                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC GetLoadNoWithTime() " , DateTime.Now.ToString(), LoadNumber.ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return LoadNumber.ToString();
            }
            catch (Exception exProductionDataLog)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC GetLoadNoWithTime() Error " , DateTime.Now.ToString(), exProductionDataLog.Message, null, true);
                return null;
            }
        }
        //Get date time
        private static string GetWagonEventDateTime(string[] WagonDataLog, string WagonName)
        {
            try
            {
                StringBuilder EventDateTime = new StringBuilder();
                EventDateTime.Append("20" + WagonDataLog[3] + "/");//Year 2020/
                EventDateTime.Append(WagonDataLog[4] + "/");//Month 2020/4/
                EventDateTime.Append(WagonDataLog[5] + " ");//Date 2020/4/16-
                EventDateTime.Append(WagonDataLog[6] + ":");//HH 2020/4/1 12:
                EventDateTime.Append(WagonDataLog[7] + ":");//MM 2020/4/1 12:12:
                EventDateTime.Append(WagonDataLog[8]);//MM 2020/4/1 12:12:10
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC GetWagonEventDateTime() Load DateTime () " + WagonName, DateTime.Now.ToString(), EventDateTime.ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return EventDateTime.ToString();
            }
            catch (Exception exProductionDataLog)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC GetWagonEventDateTime() " + WagonName, DateTime.Now.ToString(), exProductionDataLog.Message, null, true);
                return null;
            }
        }
        #endregion
        #region"Plc tag write"

        public static void WriteValues(string addr, string data)
        {
            try
            {

                if (IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected == true) ////added by SBS to avoid hanging when there is no PLC communication 
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValues() addr=" + addr + " data=" + data + " ", DateTime.Now.ToString(), "Details save successfully", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                    try
                    {
                        short[] arrDeviceValue = new short[1];          //Data for 'DeviceValue'
                        arrDeviceValue[0] = Convert.ToInt16(data);
                        System.String[] arrData = new System.String[] { addr };
                        int result = ReadWritePlcValue.WriteTagValueInPLC(arrDeviceValue, arrData, 1);

                        try
                        {
                            UserActivityEntity _insert = new UserActivityEntity();
                            _insert.DateTimeCol = DateTime.Now;
                            _insert.Activity = "Write Adreess : " + addr + " with Value :" + data;
                            if (IndiSCADAGlobalLibrary.UserLoginDetails.UserName == null)
                            {
                                _insert.UserName = "System";
                            }
                            else
                            {
                                _insert.UserName = IndiSCADAGlobalLibrary.UserLoginDetails.UserName;
                            }
                            //IndiSCADABusinessLogic.UserActivityLogic.InsertUserActivity(_insert);
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("LoginViewModel UserActivityInsert()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }

                        if (result == 0)
                        {
                            ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValues()", DateTime.Now.ToString(), "Details save successfully", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        else
                        {
                            ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValues()", DateTime.Now.ToString(), "Failed to save data", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValues() Error while Writting", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValues()", DateTime.Now.ToString(), "IsPLCConnected is false", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValues()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }

        public static void WriteValuesArray(string[] addresses, short[] values, int iNumberOfData)
        {
            try
            {
                if (IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected == true) ////added by SBS to avoid hanging when there is no PLC communication 
                { 
                    try
                    {
                        int result=0;
                        for (int i = 0; i < iNumberOfData; i++)
                        {
                            //result = ReadWritePlcValue.WriteTagValueInPLC(values, addresses, iNumberOfData);
                            //string TagName = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName(TaskName, index);
                            DeviceCommunication.CommunicationWithPLC.WriteValues(addresses[i], values[i].ToString());
                            ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValuesArray() addr1=" + addresses[i] + " data1=" + values[i].ToString() + " Number of values : " + iNumberOfData, DateTime.Now.ToString(), "Details save successfully", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }

                        if (result == 0)
                        {
                            ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValuesArray()", DateTime.Now.ToString(), "Details save successfully", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        else
                        {
                            ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValuesArray()", DateTime.Now.ToString(), "Failed to save data", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValuesArray() Error while writting", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValuesArray()", DateTime.Now.ToString(), "IsPLCConnected is false", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValuesArray()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }

        }
        public static void WriteValues_Real(string addr, string data)
        {
            try
            {
                if (IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected == true) ////added by SBS to avoid hanging when there is no PLC communication 
                {
                    try
                    {
                        //short[] arrDeviceValue = new short[1];		    //Data for 'DeviceValue'
                        //arrDeviceValue[0] = Convert.ToInt16(data);
                        //  ErrorLogger.LogError.ErrorLog("CommunicationWithPLC(RealWrite) Writing to " + addr + " data = " + data, DateTime.Now.ToString(), "Details save successfully", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        System.String[] arrData = new System.String[] { addr };
                        int result = ReadWritePlcValue.WriteTagValueRealInPLC(data, arrData, 2);
                        if (result == 0)
                        {
                            ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValues_Real()", DateTime.Now.ToString(), "Details save successfully", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        else
                        {
                            ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValues_Real()", DateTime.Now.ToString(), "Failed to save data", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValues_Real() Error while writting :", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValues_Real()", DateTime.Now.ToString(), "IsPLCConnected is false", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValues_Real()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        public static void WriteBoolValues(string addr, string data)
        {
            try
            {

                if (IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected == true) ////added by SBS to avoid hanging when there is no PLC communication 
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteBoolValues() addr=" + addr + " data=" + data + " ", DateTime.Now.ToString(), "Details save successfully", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                    try
                    {
                        short[] arrDeviceValue = new short[1];          //Data for 'DeviceValue'
                        arrDeviceValue[0] = Convert.ToInt16(data);
                        System.String[] arrData = new System.String[] { addr };
                        int result = ReadWritePlcValue.WriteBoolValueInPLC(arrDeviceValue, arrData, 1);

                        try
                        {
                            UserActivityEntity _insert = new UserActivityEntity();
                            _insert.DateTimeCol = DateTime.Now;
                            _insert.Activity = "Write Adreess : " + addr + " with Value :" + data;
                            if (IndiSCADAGlobalLibrary.UserLoginDetails.UserName == null)
                            {
                                _insert.UserName = "System";
                            }
                            else
                            {
                                _insert.UserName = IndiSCADAGlobalLibrary.UserLoginDetails.UserName;
                            }
                            //IndiSCADABusinessLogic.UserActivityLogic.InsertUserActivity(_insert);
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("LoginViewModel UserActivityInsert()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }

                        if (result == 0)
                        {
                            ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteBoolValues()", DateTime.Now.ToString(), "Details save successfully", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        else
                        {
                            ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteBoolValues()", DateTime.Now.ToString(), "Failed to save data", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteBoolValues() Error while Writting", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteBoolValues()", DateTime.Now.ToString(), "IsPLCConnected is false", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteBoolValues()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        public static void WriteValues_Word(string addr, string data)
        {
            try
            {
                ErrorLogger.LogError.ErrorLog("SettingLogic WriteValueToPLCWord() addr" + addr + " data" + data, DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                if (IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected == true) ////added by SBS to avoid hanging when there is no PLC communication 
                {
                    try
                    {
                        //short[] arrDeviceValue = new short[1];		    //Data for 'DeviceValue'
                        //arrDeviceValue[0] = Convert.ToInt16(data);
                        //  ErrorLogger.LogError.ErrorLog("CommunicationWithPLC(RealWrite) Writing to " + addr + " data = " + data, DateTime.Now.ToString(), "Details save successfully", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        System.String[] arrData = new System.String[] { addr };
                        int result = ReadWritePlcValue.WriteTagValueWordInPLC(data, arrData, 2);
                        if (result == 0)
                        {
                            ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValues_Word()", DateTime.Now.ToString(), "Details save successfully", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        else
                        {
                            ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValues_Word()", DateTime.Now.ToString(), "Failed to save data", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValues_Word() Error while writting :", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValues_Word()", DateTime.Now.ToString(), "IsPLCConnected is false", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC WriteValues_Word()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }

        #endregion

        #endregion
    }
}
