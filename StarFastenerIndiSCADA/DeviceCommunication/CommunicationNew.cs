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
    public static class CommunicationNew
    {

        #region"Declaration"
        static DispatcherTimer DispatchTimerCommWithPLC = new DispatcherTimer();
  

        //public static MitsubishiComm.frmCommunication ReadWritePlcValue = new frmCommunication(3);
        public static OmronComm.frmCommunication ReadWritePlcValue1 = new OmronComm.frmCommunication();
        static System.ComponentModel.BackgroundWorker _BackgroundWorkerCommWithPLC = new System.ComponentModel.BackgroundWorker();
        public static int PreviousShift1 = 0, Shift1_StartTime_nowSec = 0;
        public static DataTable ShiftMaster = null;
        #endregion


        #region "Property"
        #endregion

        #region Public/Private Method
        private static void DoWork(object Sender, System.ComponentModel.DoWorkEventArgs e)
        {
            ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew DoWork() Started", DateTime.Now.ToString(), IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected.ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

            try
            {

                //check PLC communication
                IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected = ReadWritePlcValue1.getConnectionStatus();
                if (IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected == false)
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew DoWork() getConnectionStatus IsPLCConnected==false", DateTime.Now.ToString(), IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected.ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                    ReadWritePlcValue1.doConnect();
                }
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew DoWork() getConnectionStatus readed " + IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected.ToString(), DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
 
                ReadPLCTagValues();

            }
            catch (Exception exThreadDashBoardUpdateTrigger)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew DoWork()", DateTime.Now.ToString(), exThreadDashBoardUpdateTrigger.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew DispatcherTickEvent()", DateTime.Now.ToString(), exDispatcherTickEvent.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
       
        //Start background worker to read plc tag and data log
        public static void StartPlcComminicationAndDataLog()
        {
            try
            { 
                ReadWritePlcValue1.doConnect();
          
                _BackgroundWorkerCommWithPLC.DoWork += DoWork;
                DispatchTimerCommWithPLC.Interval = TimeSpan.FromMilliseconds(500);
                DispatchTimerCommWithPLC.Tick += DispatcherTickEvent;
                DispatchTimerCommWithPLC.Start();
            }
            catch (Exception exStartPlcComminicationAndDataLog)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew StartPlcComminicationAndDataLog()", DateTime.Now.ToString(), exStartPlcComminicationAndDataLog.Message, null, true);
            }
        }

        #region"PLC Tag Read"

        public static void ReadPLCTagValues()
        {
            try
            {
                if (IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected)
                {
                    //if (isApplicationStartUp)//if application started first time read tags which are not required to read continue
                    //{
                        //ApplicationStartUpTagRead();
                        //InitialisePartData();
                        //isApplicationStartUp = false;
                    //}
                    //read tags continue alarm, data log , temperature,events , rectifier and dosing etc
                    ContinuousTagRead();
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew ReadPLCTagValues()", DateTime.Now.ToString(), "PLC communication failed.", null, true);
                }
            }
            catch (Exception exReadPLCTagValues)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew ReadPLCTagValues()", DateTime.Now.ToString(), exReadPLCTagValues.Message, null, true);
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
            }
            catch (Exception exApplicationStartUpTagRead)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ApplicationStartUpTagRead()", DateTime.Now.ToString(), exApplicationStartUpTagRead.Message, null, true);

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
                        return ReadWritePlcValue1.ReadTagValue(TaskName);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew ReadPLCTagValue() While Writting :" + TaskName, DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        return null;
                    }
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew ReadPLCTagValue()" + TaskName, DateTime.Now.ToString(), "IsPLCConnected is false", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    return null;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew ReadPLCTagValue() " + TaskName, DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                        return ReadWritePlcValue1.ReadTag_RealValue(TaskName);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew Real_ReadPLCTagValue() while reading :" + TaskName, DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        return null;
                    }
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew Real_ReadPLCTagValue()" + TaskName, DateTime.Now.ToString(), "IsPLCConnected is false", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    return null;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew Real_ReadPLCTagValue() " + TaskName, DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                        return ReadWritePlcValue1.readDeviceRandom(TagName, Length);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew Read_BlockAddress()" + TagName[0], DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        return null;
                    }
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew Read_BlockAddress()" + TagName[0], DateTime.Now.ToString(), "IsPLCConnected is false", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    return null;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew Read_BlockAddress() " + TagName[0], DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                        return ReadWritePlcValue1.ReadTag_DoubleWordValue(TaskName);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew Word_ReadPLCTagValue() " + TaskName, DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        return null;
                    }
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew Word_ReadPLCTagValue()" + TaskName, DateTime.Now.ToString(), "IsPLCConnected is false", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    return null;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew Word_ReadPLCTagValue()" + TaskName, DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return null;
            }
        }

        //continuous tag read method
        private static void ContinuousTagRead()
        {


            try
            { 
 

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
                IndiSCADAGlobalLibrary.TagList.TemperatureActual = ReadPLCTagValue("TempControllerActual"); 
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


                IndiSCADAGlobalLibrary.TagList.RectifierAmpHr = DeviceCommunication.CommunicationWithPLC.Real_ReadPLCTagValue("RectifierAmpHr");


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
 




                IndiSCADAGlobalLibrary.TagList.WagonNextStep = ReadPLCTagValue("WagaonNextStep"); // address need to check with PLC  //????
                //Tag Id 41----------------------------------------------------------------------------------------------
                IndiSCADAGlobalLibrary.TagList.WagonNextStation = ReadPLCTagValue("WagaonNextStation");// address need to check with PLC  //????
                IndiSCADAGlobalLibrary.TagList.WagonMovment = ReadPLCTagValue("WagonMovement");
                ////Tag Id 40----------------------------------------------------------------------------------------------
                IndiSCADAGlobalLibrary.TagList.LastCycleTime = ReadPLCTagValue("CYCLETIMESETCYCLETIME");

                IndiSCADAGlobalLibrary.TagList.LoadDipTime = ReadPLCTagValue("DipTimeForDisplayDipTime");

            }
            catch (Exception exContinuousTagRead)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ContinuousTagRead()", DateTime.Now.ToString(), exContinuousTagRead.Message, null, true);
            }

            #region shiftwise Downtime logic
            //NEW LOGIC FOR DOWNTIME
            try
            {
                IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime = ReadPLCTagValue("LiveShiftWiseDownTime");
                IndiSCADAGlobalLibrary.TagList.PreviousShiftWiseDownTime = ReadPLCTagValue("PreviousShiftWiseDownTime");
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ContinuousTagRead() ReadShiftwiseDowntime", DateTime.Now.ToString(), IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime.Length.ToString(), null, true);
                if (PreviousShift1 == 0)
                {
                    PreviousShift1 = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                }
                else
                {
                    int h = DateTime.Now.Hour * 60 * 60;
                    int m = DateTime.Now.Minute * 60;
                    int s = DateTime.Now.Second;
                    int nowSec = h + m + s;

                    //calculate first shift start time in sec
                    //below will resturn result of this query = SELECT * From [dbo].[ShiftMaster] where IsNewEntry=0 order by ShiftNo asc
                    if (Shift1_StartTime_nowSec == 0)
                    {
                        ServiceResponse<DataTable> Result = IndiSCADADataAccess.DataAccessSelect.SelectShiftTimeDetails();
                        
                        ShiftMaster = Result.Response;
                      //  ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ContinuousTagRead() SelectShiftTimeDetails - ", DateTime.Now.ToString(), ShiftMaster.Rows[0]["ShiftStartTime"].ToString(), null, true);
                        DateTime Shift1_StartTime = Convert.ToDateTime(ShiftMaster.Rows[0]["ShiftStartTime"].ToString());
                        int Shift1_StartTime_h = Shift1_StartTime.Hour * 60 * 60;
                        int Shift1_StartTime_m = Shift1_StartTime.Minute * 60;
                        int Shift1_StartTime_s = Shift1_StartTime.Second;
                        Shift1_StartTime_nowSec = Shift1_StartTime_h + Shift1_StartTime_m + Shift1_StartTime_s;
                    }
                  //  ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ContinuousTagRead() Shift1_StartTime_nowSec = "+ Shift1_StartTime_nowSec, DateTime.Now.ToString(), IndiSCADAGlobalLibrary.TagList.RealShiftWiseDownTime.Length.ToString(), null, true);
                    //21900 = 6.05, 50700 = 14.05, 79500 = 22.05
                    if (nowSec >= 0 && nowSec <= Shift1_StartTime_nowSec)
                    {
                        //during 0 to 6.5 dont update anything.
                    }
                    else
                    {
                       // ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ContinuousTagRead() shift  = " + IndiSCADAGlobalLibrary.AccessConfig.Shift , DateTime.Now.ToString(), " previous shift = " + PreviousShift1, null, true);
                        if (IndiSCADAGlobalLibrary.AccessConfig.Shift != PreviousShift1)
                        {
                            PreviousShift1 = IndiSCADAGlobalLibrary.AccessConfig.Shift;
                        }

                        string Qry = "";
                        if (PreviousShift1 == 1)
                        {
                            if (ShiftMaster.Rows.Count == 3) //for 3 shifts
                            { Qry = "select * from ShiftWiseDownTimeSummary where cast(ShiftDateTime as date) = cast('" + DateTime.Now.AddDays(-1) + "' as date)  and ShiftNo='3'"; }
                            else if (ShiftMaster.Rows.Count == 2) //for 2 shifts
                            { Qry = "select * from ShiftWiseDownTimeSummary where cast(ShiftDateTime as date) = cast('" + DateTime.Now.AddDays(-1) + "' as date)  and ShiftNo='2'"; }
                        }
                        else if (PreviousShift1 == 2)
                        { Qry = "select * from ShiftWiseDownTimeSummary where cast(ShiftDateTime as date) =cast('" + DateTime.Now + "' as date)  and ShiftNo='1'"; }
                        else if (PreviousShift1 == 3)
                        { Qry = "select * from ShiftWiseDownTimeSummary where cast(ShiftDateTime as date) =cast('" + DateTime.Now + "' as date)  and ShiftNo='2'"; }

                        ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ContinuousTagRead() Query  = " + Qry, DateTime.Now.ToString(), " ", null, true);
                        ServiceResponse<DataTable> result = IndiSCADADataAccess.DataAccessSelect.SelectLog4SettingData(Qry);


                        if (result.Response.Rows.Count == 0 || result.Response == null) //if no record exist then insert
                        {
                            if (PreviousShift1 == 1)
                            {
                                if (ShiftMaster.Rows.Count == 3) //for 3 shifts
                                {
                                    IndiSCADADataAccess.DataAccessInsert.InsertShitwiseDowntimeData(DateTime.Now.AddDays(-1), 3,
                                    IndiSCADAGlobalLibrary.TagList.PreviousShiftWiseDownTime[0], IndiSCADAGlobalLibrary.TagList.PreviousShiftWiseDownTime[1],
                                    IndiSCADAGlobalLibrary.TagList.PreviousShiftWiseDownTime[2], IndiSCADAGlobalLibrary.TagList.PreviousShiftWiseDownTime[3],
                                    IndiSCADAGlobalLibrary.TagList.PreviousShiftWiseDownTime[4]);
                                }
                                else if (ShiftMaster.Rows.Count == 2) //for 2 shifts
                                {
                                    IndiSCADADataAccess.DataAccessInsert.InsertShitwiseDowntimeData(DateTime.Now.AddDays(-1), 2,
                                    IndiSCADAGlobalLibrary.TagList.PreviousShiftWiseDownTime[0], IndiSCADAGlobalLibrary.TagList.PreviousShiftWiseDownTime[1],
                                    IndiSCADAGlobalLibrary.TagList.PreviousShiftWiseDownTime[2], IndiSCADAGlobalLibrary.TagList.PreviousShiftWiseDownTime[3],
                                    IndiSCADAGlobalLibrary.TagList.PreviousShiftWiseDownTime[4]);
                                }
                            }
                            else if (PreviousShift1 == 2)
                            {
                                IndiSCADADataAccess.DataAccessInsert.InsertShitwiseDowntimeData(DateTime.Now, 1,
                                   IndiSCADAGlobalLibrary.TagList.PreviousShiftWiseDownTime[0], IndiSCADAGlobalLibrary.TagList.PreviousShiftWiseDownTime[1],
                                   IndiSCADAGlobalLibrary.TagList.PreviousShiftWiseDownTime[2], IndiSCADAGlobalLibrary.TagList.PreviousShiftWiseDownTime[3],
                                   IndiSCADAGlobalLibrary.TagList.PreviousShiftWiseDownTime[4]);
                            }
                            else if (PreviousShift1 == 3)
                            {
                                IndiSCADADataAccess.DataAccessInsert.InsertShitwiseDowntimeData(DateTime.Now, 2,
                                   IndiSCADAGlobalLibrary.TagList.PreviousShiftWiseDownTime[0], IndiSCADAGlobalLibrary.TagList.PreviousShiftWiseDownTime[1],
                                   IndiSCADAGlobalLibrary.TagList.PreviousShiftWiseDownTime[2], IndiSCADAGlobalLibrary.TagList.PreviousShiftWiseDownTime[3],
                                   IndiSCADAGlobalLibrary.TagList.PreviousShiftWiseDownTime[4]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC ContinuousTagRead() downtime error ", DateTime.Now.ToString(), ex.Message, null, true);
                // log.Error("CommunicationWithPLC ContinuousTagRead() downtime datalog" + ex.Message);
            }

            #endregion
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
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew DivideBy method", DateTime.Now.ToString(), "", null, true);
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
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew GetDuration()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew DivideBy method", DateTime.Now.ToString(), ex.ToString(), null, true);
                return null;
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
                ErrorLogger.LogError.ErrorLog("CommunicationWithPLCNew IntConvertion()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return 0;
            }
        }
        
      
    
        #endregion
    }
}
