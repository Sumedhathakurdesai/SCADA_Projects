using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace DeviceCommunication
{
    public static class Wagon345Datalog
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
            ErrorLogger.LogError.ErrorLog("Wagon345Datalog DoWork() Started", DateTime.Now.ToString(), IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected.ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
 
            try
            { 
                ////check PLC communication
                //IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected = ReadWritePlcValue.getConnectionStatus();
                //if (IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected == false)
                //{
                //    ErrorLogger.LogError.ErrorLog("Wagon345Datalog DoWork() getConnectionStatus IsPLCConnected==false", DateTime.Now.ToString(), IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected.ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                //    ReadWritePlcValue.doConnect();
                //}
                //ErrorLogger.LogError.ErrorLog("Wagon345Datalog DoWork() getConnectionStatus readed " + IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected.ToString(), DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                try
                {
                    if (IndiSCADAGlobalLibrary.PLCCommunication.IsPLCConnected)
                    {              
                        IndiSCADAGlobalLibrary.TagList.W3DataLog = ReadPLCTagValue("Wagon3DataLog");
                        //Tag Id 122----------------------------------------------------------------------------------------------
                        IndiSCADAGlobalLibrary.TagList.W4DataLog = ReadPLCTagValue("Wagon4DataLog");
                        //Tag Id 123----------------------------------------------------------------------------------------------
                        IndiSCADAGlobalLibrary.TagList.W5DataLog = ReadPLCTagValue("Wagon5DataLog");
                    }
                    else
                    {
                        ErrorLogger.LogError.ErrorLog("Wagon345Datalog Dowork() WagonDataLog read", DateTime.Now.ToString(), "PLC communication failed.", null, true);
                    }
                }
                catch (Exception exReadPLCTagValues)
                {
                    ErrorLogger.LogError.ErrorLog("Wagon345Datalog Dowork() WagonDataLog read", DateTime.Now.ToString(), exReadPLCTagValues.Message, null, true);
                }  
            }
            catch (Exception exThreadDashBoardUpdateTrigger)
            {
                ErrorLogger.LogError.ErrorLog("Wagon345Datalog DoWork()", DateTime.Now.ToString(), exThreadDashBoardUpdateTrigger.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                ErrorLogger.LogError.ErrorLog("Wagon345Datalog DispatcherTickEvent()", DateTime.Now.ToString(), exDispatcherTickEvent.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                        ErrorLogger.LogError.ErrorLog("Wagon345Datalog ReadPLCTagValue() While Writting :" + TaskName, DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        return null;
                    }
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("Wagon345Datalog ReadPLCTagValue()" + TaskName, DateTime.Now.ToString(), "IsPLCConnected is false", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    return null;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("Wagon345Datalog ReadPLCTagValue() " + TaskName, DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return null;
            }
        }

        //Start background worker to read plc tag and data log

        public static void StartWagon345DataLog()
        {
            try
            {
                ReadWritePlcValue.doConnect();

                _BackgroundWorkerCommWithPLC.DoWork += DoWork;
                DispatchTimerCommWithPLC.Interval = TimeSpan.FromMilliseconds(500);
                DispatchTimerCommWithPLC.Tick += DispatcherTickEvent;
                DispatchTimerCommWithPLC.Start();
            }
            catch (Exception exStartPlcComminicationAndDataLog)
            {
                ErrorLogger.LogError.ErrorLog("Wagon345Datalog StartPlcComminicationAndDataLog()", DateTime.Now.ToString(), exStartPlcComminicationAndDataLog.Message, null, true);
            }
        }

        #endregion


    }
}
