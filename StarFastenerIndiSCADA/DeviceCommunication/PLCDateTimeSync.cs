using IndiSCADAEntity.Entity;
using IndiSCADAGlobalLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace DeviceCommunication
{
    public static class PLCDateTimeSync
    {
        #region "Declaration"
        static DispatcherTimer DispatchTimerAlarmEvent = new DispatcherTimer();
        static System.ComponentModel.BackgroundWorker _BackgroundWorkerAlarmEvent = new System.ComponentModel.BackgroundWorker();
        public static string[] arrAddress, arrControlBits;
        public static bool isSCADAOpen = false;
        #endregion

        #region method

        private static void DoWork(object Sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                //App.Current.Dispatcher.BeginInvoke(new Action(() => 
                //{
                PLCDateTimeSynchEvent();
                //}));

            }
            catch (Exception exThreadDashBoardUpdateTrigger)
            {
                ErrorLogger.LogError.ErrorLog("PLCDateTimeSync DoWork()", DateTime.Now.ToString(), exThreadDashBoardUpdateTrigger.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                ErrorLogger.LogError.ErrorLog("PLCDateTimeSync DispatcherTickEvent()", DateTime.Now.ToString(), exDispatcherTickEvent.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        //Start background worker to read plc tag and data log
        public static void StartPLCDateTimeSyncTracking()
        {
            try
            {
                //_BackgroundWorkerAlarmEvent.DoWork += DoWork;
                //DispatchTimerAlarmEvent.Interval = TimeSpan.FromMilliseconds(1);
                //DispatchTimerAlarmEvent.Tick += DispatcherTickEvent;
                //DispatchTimerAlarmEvent.Start();
                isSCADAOpen = true;
                new System.Threading.Thread(delegate ()
                {
                    PLCDateTimeSynchEvent();
                }).Start();
            }
            catch (Exception exStartPlcComminicationAndDataLog)
            {
                ErrorLogger.LogError.ErrorLog("PLCDateTimeSync StartAlarmAndEventTracking()", DateTime.Now.ToString(), exStartPlcComminicationAndDataLog.Message, null, true);
            }
        }
        private static void PLCDateTimeSynchEvent()
        {

            do
            {
                try
                {
                    DateTime CurrentDateTime = DateTime.Now;
                    string day = "", month = "", year = "", hours = "", minutes = "", seconds = "", dayofweek = "";
                    TimeSpan time1 = new TimeSpan(23, 58, 0); //10 o'clock
                    TimeSpan time2 = new TimeSpan(0, 1, 0); //12 o'clock
                    TimeSpan now = DateTime.Now.TimeOfDay;

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
                        # region "Assign datetime values"
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

                        if (seconds == "0")
                        {
                            try
                            {
                                short[] arrDateTimeValue = new short[7];		    //Data for datetime
                                arrDateTimeValue[0] = (Convert.ToInt16(year));
                                arrDateTimeValue[1] = (Convert.ToInt16(month));
                                arrDateTimeValue[2] = (Convert.ToInt16(day));
                                arrDateTimeValue[3] = (Convert.ToInt16(hours));
                                arrDateTimeValue[4] = (Convert.ToInt16(minutes));
                                arrDateTimeValue[5] = (Convert.ToInt16(seconds));
                                arrDateTimeValue[6] = (Convert.ToInt16(dayofweek));

                                // objLogging.LogError(DateTime.Now, "PLCDateTimeSynch() Write Values 1 -", "" , 0);
                                //arrDateTimeValue[7] = (Convert.ToInt16(1)); // Confirmation bit                        

                                short[] arrDeviceValue = new short[1];		    //Data for 'DeviceValue'
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
                                                CommunicationWithPLC.WriteValuesArray(arrData, arrDeviceValue, 1);
                                            }
                                            catch { }

                                            try
                                            {
                                                //  objLogging.LogError(DateTime.Now, "PLCDateTimeSynch() Write Values -", arrAddress[1], 0);
                                                arrDeviceValue[0] = Convert.ToInt16(arrDateTimeValue[1]);
                                                arrData = new System.String[] { arrAddress[1] };
                                                CommunicationWithPLC.WriteValuesArray(arrData, arrDeviceValue, 1);
                                            }
                                            catch { }

                                            try
                                            {
                                                // objLogging.LogError(DateTime.Now, "PLCDateTimeSynch() Write Values -", arrAddress[2], 0);
                                                arrDeviceValue[0] = Convert.ToInt16(arrDateTimeValue[2]);
                                                arrData = new System.String[] { arrAddress[2] };
                                                CommunicationWithPLC.WriteValuesArray(arrData, arrDeviceValue, 1);
                                            }
                                            catch { }

                                            try
                                            {
                                                //  objLogging.LogError(DateTime.Now, "PLCDateTimeSynch() Write Values -", arrAddress[3], 0);
                                                arrDeviceValue[0] = Convert.ToInt16(arrDateTimeValue[3]);
                                                arrData = new System.String[] { arrAddress[3] };
                                                CommunicationWithPLC.WriteValuesArray(arrData, arrDeviceValue, 1);
                                            }
                                            catch { }
                                            try
                                            {
                                                //  objLogging.LogError(DateTime.Now, "PLCDateTimeSynch() Write Values -", arrAddress[4], 0);
                                                arrDeviceValue[0] = Convert.ToInt16(arrDateTimeValue[4]);
                                                arrData = new System.String[] { arrAddress[4] };
                                                CommunicationWithPLC.WriteValuesArray(arrData, arrDeviceValue, 1);
                                            }
                                            catch { }

                                            try
                                            {
                                                //  objLogging.LogError(DateTime.Now, "PLCDateTimeSynch() Write Values -", arrAddress[5], 0);
                                                arrDeviceValue[0] = Convert.ToInt16(arrDateTimeValue[5]);
                                                arrData = new System.String[] { arrAddress[5] };
                                                CommunicationWithPLC.WriteValuesArray(arrData, arrDeviceValue, 1);
                                            }
                                            catch { }

                                            try
                                            {
                                                //  objLogging.LogError(DateTime.Now, "PLCDateTimeSynch() Write Values -", arrAddress[6], 0);
                                                arrDeviceValue[0] = Convert.ToInt16(arrDateTimeValue[6]);
                                                arrData = new System.String[] { arrAddress[6] };
                                                CommunicationWithPLC.WriteValuesArray(arrData, arrDeviceValue, 1);
                                            }
                                            catch { }

                                            try
                                            {
                                                arrDeviceValue = new short[1];
                                                arrDeviceValue[0] = Convert.ToInt16(1);
                                                arrData = new System.String[] { arrControlBits[0] }; // Confirmation Bit
                                               CommunicationWithPLC.WriteValuesArray(arrData, arrDeviceValue, 1);

                                            }
                                            catch { }

                                            //int result = objMainScreen.WriteValueINPLC(arrDateTimeValue, arrAddress, 7);

                                            ErrorLogger.LogError.ErrorLog("CommunicationWithPLC PLCDateTimeSynch()", DateTime.Now.ToString(), "", null, true);

                                        }
                                        catch (Exception ex) { }

                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        arrAddress = new System.String[7];
                                        for (int i = 0; i < 7; i++)
                                        {
                                            arrAddress[i] = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName("DateTimeSynch", i);
                                        }

                                        for (int i = 0; i < 2; i++)
                                        {
                                            arrControlBits[i] = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName("DateTimeCorrection", i);
                                        }

                                    }
                                    catch (Exception ex) { }

                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("CommunicationWithPLC PLCDateTimeSynch()", DateTime.Now.ToString(), ex.Message, null, true);
                            }

                            #endregion

                            Thread.Sleep(60000);
                        }
                    }

                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError.ErrorLog(" PLCDateTimeSynch()", DateTime.Now.ToString(), ex.Message, null, true);
                }
            }
            while (isSCADAOpen == true);
        }
        #endregion
    }
}
