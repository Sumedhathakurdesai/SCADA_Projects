using IndiSCADAEntity.Entity;
using IndiSCADAGlobalLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace DeviceCommunication
{
    public static class TrendDataLog
    {
        #region"Declaration"
        static string[] TempArrayDosing = new string[20];
        static string pumpno, chemicalname, dosingtime, dosingqty, mode, dosingFlowRate;
        static DispatcherTimer DispatchTimerTrendDataLog = new DispatcherTimer();
        static System.ComponentModel.BackgroundWorker _BackgroundWorkerTrendDataLogger = new System.ComponentModel.BackgroundWorker();
        static ServiceResponse<DataTable> result = IndiSCADADataAccess.DataAccessSelect.getAllFromChemicalMaster();
        static DataTable chemicalDT = result.Response;
       
        static string[] PumpNo = new string[chemicalDT.Rows.Count];
        static string[] ChemicalName = new string[chemicalDT.Rows.Count];
        static string[] DosingOP = new string[chemicalDT.Rows.Count];
        static string[] DosingAM = new string[chemicalDT.Rows.Count];
        static string[] DosingTimeSP = new string[chemicalDT.Rows.Count];
        static string[] DosingQuantity = new string[chemicalDT.Rows.Count];
        static string[] DosingFlowRate = new string[chemicalDT.Rows.Count];
        #endregion
        #region Public/Private Method
        private static void DoWork(object Sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                TemperatureDataLog();
                RectifierDataLog();
                ChemicalConsumption();
            }
            catch (Exception exThreadDashBoardUpdateTrigger)
            {
                ErrorLogger.LogError.ErrorLog("TrendDataLog DoWork()", DateTime.Now.ToString(), exThreadDashBoardUpdateTrigger.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        private static void DispatcherTickEvent(object sender, EventArgs e)
        {
            try
            {
                if (_BackgroundWorkerTrendDataLogger.IsBusy != true)
                {
                    _BackgroundWorkerTrendDataLogger.RunWorkerAsync();
                }
            }
            catch (Exception exDispatcherTickEvent)
            {
                ErrorLogger.LogError.ErrorLog("TrendDataLog DispatcherTickEvent()", DateTime.Now.ToString(), exDispatcherTickEvent.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        //Start background worker to read plc tag and data log
        public static void StartTrendDataLog()
        {
            try
            { 
                _BackgroundWorkerTrendDataLogger.DoWork += DoWork;
                DispatchTimerTrendDataLog.Interval = TimeSpan.FromSeconds(5);
                DispatchTimerTrendDataLog.Tick += DispatcherTickEvent;
                DispatchTimerTrendDataLog.Start();
            }
            catch (Exception exStartPlcComminicationAndDataLog)
            {
                ErrorLogger.LogError.ErrorLog("TrendDataLog StartTrendDataLog()", DateTime.Now.ToString(), exStartPlcComminicationAndDataLog.Message, null, true);
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
                ErrorLogger.LogError.ErrorLog("TrendDataLog IntConvertion()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return 0;
            }
        }
        private static void TemperatureDataLog()
        {
            try
            {
                TemperatureTrendentity InsertTrendTemperatureData = new TemperatureTrendentity();

                //IndiSCADAGlobalLibrary.TagList.TemperatureActual = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25" };

                if (IndiSCADAGlobalLibrary.TagList.TemperatureActual != null )
                {
                    if (IndiSCADAGlobalLibrary.TagList.TemperatureActual.Length > 0)
                    {
                        InsertTrendTemperatureData.DateTimeCol = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss tt")); 
                        ServiceResponse<int> QueryResult = IndiSCADADataAccess.DataAccessInsert.InsertTrendTemperatureData(InsertTrendTemperatureData, IndiSCADAGlobalLibrary.TagList.TemperatureActual);//25
                        if (QueryResult.Status == ResponseType.E)//query excuted with some erros
                        {
                            ErrorLogger.LogError.ErrorLog("TrendDataLog InsertTrendTemperatureData()", DateTime.Now.ToString(), QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TrendDataLog TemperatureDataLog()", DateTime.Now.ToString(), ex.Message, null, true);
            }
        }
        private static void RectifierDataLog()
        {
            try
            {
                RectifierTrendEntity InsertTrendRectifierData = new RectifierTrendEntity();

                //IndiSCADAGlobalLibrary.TagList.ActualCurrent = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" ,"21" }; //, "31", "32", "33", "30", "31", "32", "33" };
                //IndiSCADAGlobalLibrary.TagList.AppliedCurrentValues = new string[] { "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };//, "31", "32", "33", "30", "31", "32", "33" };
                
                if (IndiSCADAGlobalLibrary.TagList.ActualCurrent != null && IndiSCADAGlobalLibrary.TagList.AppliedCurrentValues != null)
                {
                    if (IndiSCADAGlobalLibrary.TagList.ActualCurrent.Length > 0 && IndiSCADAGlobalLibrary.TagList.AppliedCurrentValues.Length > 0)
                    {
                        InsertTrendRectifierData.DateTimeCol = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss tt"));
                        ServiceResponse<int> QueryResult = IndiSCADADataAccess.DataAccessInsert.InsertTrendRectifier(InsertTrendRectifierData, IndiSCADAGlobalLibrary.TagList.ActualCurrent, IndiSCADAGlobalLibrary.TagList.AppliedCurrentValues);//20
                        if (QueryResult.Status == ResponseType.E)//query excuted with some erros
                        {
                            ErrorLogger.LogError.ErrorLog("TrendDataLog RectifierDataLog() error while InsertTrendRectifier execution", DateTime.Now.ToString(), QueryResult.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                    }
                }

                //string[] AutoCurrentSP = IndiSCADAGlobalLibrary.TagList.AutoCurrentSP;

                //ErrorLogger.LogError.ErrorLog("TrendDataLog RectifierDataLog() station 7 rect1 AutoCurrentSP=" + AutoCurrentSP[0], DateTime.Now.ToString(), "", null, true);
                //ErrorLogger.LogError.ErrorLog("TrendDataLog RectifierDataLog() station 8 rect1 AutoCurrentSP=" + AutoCurrentSP[1], DateTime.Now.ToString(), "", null, true);

                //ErrorLogger.LogError.ErrorLog("TrendDataLog RectifierDataLog() station 14 rect2 AutoCurrentSP=" + AutoCurrentSP[2], DateTime.Now.ToString(), "", null, true);

                //ErrorLogger.LogError.ErrorLog("TrendDataLog RectifierDataLog() station 21 rect3 AutoCurrentSP=" + AutoCurrentSP[3], DateTime.Now.ToString(), "", null, true);
                //ErrorLogger.LogError.ErrorLog("TrendDataLog RectifierDataLog() station 22 rect3 AutoCurrentSP=" + AutoCurrentSP[4], DateTime.Now.ToString(), "", null, true);
                //ErrorLogger.LogError.ErrorLog("TrendDataLog RectifierDataLog() station 23 rect3 AutoCurrentSP=" + AutoCurrentSP[5], DateTime.Now.ToString(), "", null, true);
                //ErrorLogger.LogError.ErrorLog("TrendDataLog RectifierDataLog() station 24 rect3 AutoCurrentSP=" + AutoCurrentSP[6], DateTime.Now.ToString(), "", null, true);
                //ErrorLogger.LogError.ErrorLog("TrendDataLog RectifierDataLog() station 25 rect3 AutoCurrentSP=" + AutoCurrentSP[7], DateTime.Now.ToString(), "", null, true);
                //ErrorLogger.LogError.ErrorLog("TrendDataLog RectifierDataLog() station 26 rect3 AutoCurrentSP=" + AutoCurrentSP[8], DateTime.Now.ToString(), "", null, true);
                //ErrorLogger.LogError.ErrorLog("TrendDataLog RectifierDataLog() station 27 rect3 AutoCurrentSP=" + AutoCurrentSP[9], DateTime.Now.ToString(), "", null, true);
                //ErrorLogger.LogError.ErrorLog("TrendDataLog RectifierDataLog() station 28 rect3 AutoCurrentSP=" + AutoCurrentSP[10], DateTime.Now.ToString(), "", null, true);
                //ErrorLogger.LogError.ErrorLog("TrendDataLog RectifierDataLog() station 29 rect3 AutoCurrentSP=" + AutoCurrentSP[11], DateTime.Now.ToString(), "", null, true);
                //ErrorLogger.LogError.ErrorLog("TrendDataLog RectifierDataLog() station 30 rect3 AutoCurrentSP=" + AutoCurrentSP[12], DateTime.Now.ToString(), "", null, true);
                //ErrorLogger.LogError.ErrorLog("TrendDataLog RectifierDataLog() station 31 rect3 AutoCurrentSP=" + AutoCurrentSP[13], DateTime.Now.ToString(), "", null, true);
                //ErrorLogger.LogError.ErrorLog("TrendDataLog RectifierDataLog() station 32 rect3 AutoCurrentSP=" + AutoCurrentSP[14], DateTime.Now.ToString(), "", null, true);
                //ErrorLogger.LogError.ErrorLog("TrendDataLog RectifierDataLog() station 33 rect3 AutoCurrentSP=" + AutoCurrentSP[15], DateTime.Now.ToString(), "", null, true);
                //ErrorLogger.LogError.ErrorLog("TrendDataLog RectifierDataLog() station 34 rect3 AutoCurrentSP=" + AutoCurrentSP[16], DateTime.Now.ToString(), "", null, true);
                //ErrorLogger.LogError.ErrorLog("TrendDataLog RectifierDataLog() station 35 rect3 AutoCurrentSP=" + AutoCurrentSP[17], DateTime.Now.ToString(), "", null, true);
                //ErrorLogger.LogError.ErrorLog("TrendDataLog RectifierDataLog() station 36 rect3 AutoCurrentSP=" + AutoCurrentSP[18], DateTime.Now.ToString(), "", null, true);
                //ErrorLogger.LogError.ErrorLog("TrendDataLog RectifierDataLog() station 37 rect3 AutoCurrentSP=" + AutoCurrentSP[19], DateTime.Now.ToString(), "", null, true);
                //ErrorLogger.LogError.ErrorLog("TrendDataLog RectifierDataLog() station 38 rect3 AutoCurrentSP=" + AutoCurrentSP[20], DateTime.Now.ToString(), "", null, true);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TrendDataLog RectifierDataLog()", DateTime.Now.ToString(), ex.Message, null, true);
            }
        }

         

        private static void ChemicalConsumption()
        {
            #region form strings
            try
            {
                for (int i = 0; i < chemicalDT.Rows.Count; i++)
                {
                    PumpNo[i] = chemicalDT.Rows[i]["PumpNo"].ToString();
                    ChemicalName[i] = chemicalDT.Rows[i]["ChemicalName"].ToString();

                    DosingAM[i] = chemicalDT.Rows[i]["ModeOfOperation"].ToString();
                    DosingTimeSP[i] = chemicalDT.Rows[i]["SetTime"].ToString();
                    DosingQuantity[i] = chemicalDT.Rows[i]["SetQuantity"].ToString();
                    DosingFlowRate[i] = chemicalDT.Rows[i]["SetFlowRate"].ToString();
                    DosingOP[i] = chemicalDT.Rows[i]["DosingOP"].ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalConsumption()", DateTime.Now.ToString(), ex.Message, null, true);
            }
            #endregion



            #region Previous chemical datalog previous code COMMENTED
            //try
            //{
            //    string OnOffCondition;
            //    //fetch on off condition of  dosing output from database
            //    for (int i = 0; i < chemicalDT.Rows.Count; i++)
            //    {
            //        String[] Address = new String[] { chemicalDT.Rows[i]["DosingOP"].ToString() };
            //        string[] DosingOutput = CommunicationWithPLC.Read_BlockAddress(Address, 1);
            //        ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalConsumption() Read_BlockAddress: " + Address[0] + "=" + DosingOutput[0], DateTime.Now.ToString(), "", null, true);

            //        if (DosingOutput[0] != null && DosingOutput[0].Length > 0)
            //        {
            //            #region fetch previous on off condition of dosing output from database
            //            DataTable chemicalConditionDT = new DataTable();
            //            try
            //            {
            //                ServiceResponse<DataTable> result = IndiSCADADataAccess.DataAccessSelect.getConditionFromChemicalMaster();

            //                if (result.Response != null)
            //                {
            //                    if (result.Response.Rows.Count > 0)
            //                    {
            //                        chemicalConditionDT = result.Response;
            //                        ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalConsumption() getConditionFromChemicalMaster() result is not null", DateTime.Now.ToString(),"", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

            //                    }
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalConsumption() getConditionFromChemicalMaster()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            //            }
            //            #endregion

            //            OnOffCondition = chemicalConditionDT.Rows[i]["OnOffCondition"].ToString();

            //            if (DosingOutput[0] == "1")//if output is 1
            //            {
            //                ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalConsumption() DosingOutput is 1", DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

            //                if (OnOffCondition == "0" || OnOffCondition == "false" || OnOffCondition == "False") //if not already 1 then insert new record
            //                {
            //                    DosingOperationDataEntity _DataChemicalConsumption = new DosingOperationDataEntity();
            //                    _DataChemicalConsumption.PumpNo = chemicalDT.Rows[i]["PumpNo"].ToString();
            //                    _DataChemicalConsumption.ChemicalName = chemicalDT.Rows[i]["ChemicalName"].ToString();

            //                    #region mode of operation
            //                    try
            //                    {
            //                        Address = new String[] { chemicalDT.Rows[i]["ModeOfOperation"].ToString() };
            //                        string[] ModeOfOperation = CommunicationWithPLC.Read_BlockAddress(Address, 1);
            //                        _DataChemicalConsumption.ModeOfOperation = ModeOfOperation[0].ToString();
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalConsumption() mode of operation", DateTime.Now.ToString(), ex.Message, null, true);
            //                    }
            //                    #endregion

            //                    #region SetTime
            //                    try
            //                    {
            //                        Address = new String[] { chemicalDT.Rows[i]["SetTime"].ToString() };
            //                        string[] SetTime = CommunicationWithPLC.Read_BlockAddress(Address, 1);
            //                        _DataChemicalConsumption.SetTime = Convert.ToInt16(SetTime[0].ToString());
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalConsumption() SetTime", DateTime.Now.ToString(), ex.Message, null, true);
            //                    }
            //                    #endregion

            //                    #region SetQuantity
            //                    try
            //                    {
            //                        //Address = new String[] { chemicalDT.Rows[i]["SetQuantity"].ToString() };
            //                        string[] SetQuantity = CommunicationWithPLC.Real_ReadPLCTagValue("DosingQuantityInml");  // real value for quantity

            //                        _DataChemicalConsumption.SetQuantity = Convert.ToDouble(SetQuantity[i].ToString());
            //                        ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalConsumption() DosingQuantityInmlCircular setqunatity=" + SetQuantity[i].ToString() + " for Dosing" + i, DateTime.Now.ToString(), "", null, true);

            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalConsumption() SetQuantity", DateTime.Now.ToString(), ex.Message, null, true);
            //                    }
            //                    #endregion

            //                    #region SetFlowRate
            //                    try
            //                    {                                   
            //                        string[] SetFlowRate = CommunicationWithPLC.Real_ReadPLCTagValue("DosingFlowRatemlperSec"); // real value of flow rate

            //                        _DataChemicalConsumption.SetFlowRate = Convert.ToDouble(SetFlowRate[i].ToString());
            //                        ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalConsumption() DosingFlowRatemlperSec setqunatity=" + SetFlowRate[i].ToString() + " for Dosing" + i, DateTime.Now.ToString(), "", null, true);

            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalConsumption() SetFlowRate", DateTime.Now.ToString(), ex.Message, null, true);
            //                    }
            //                    #endregion

            //                    IndiSCADADataAccess.DataAccessInsert.InsertChemicalConsumptionData(_DataChemicalConsumption);
            //                    IndiSCADADataAccess.DataAccessUpdate.UpdateChemicalMaster(i + 1, true);  //here we have to update OnOffCondition=1 in database
            //                }
            //            }
            //            else if (DosingOutput[0] == "0") //if it is 0
            //            {
            //                ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalConsumption() DosingOutput is 0", DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

            //                if (OnOffCondition == "1" || OnOffCondition == "true" || OnOffCondition == "True")  //if not already zero  then update in database
            //                {
            //                    IndiSCADADataAccess.DataAccessUpdate.UpdateChemicalMaster(i + 1, false); //here we have to update OnOffCondition=1 in database
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex) { ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalConsumption()", DateTime.Now.ToString(), ex.Message, null, true); }
            #endregion

            try
            {
                string OnOffCondition; DataTable ChemicalNamesWithPercentageDT = new DataTable();
                //fetch on off condition of  dosing output from database
                for (int i = 0; i < chemicalDT.Rows.Count; i++)
                {
                    String[] Address = new String[] { chemicalDT.Rows[i]["DosingOP"].ToString() };
                    string[] DosingOutput = CommunicationWithPLC.Read_BlockAddress(Address, 1); //new string[] { "1" , "2" , "3" , "4" };  //

                    //ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalConsumption() Read_BlockAddress: " + Address[0] + "=" + DosingOutput[0], DateTime.Now.ToString(), "", null, true);

                    if (DosingOutput[0] != null && DosingOutput[0].Length > 0)
                    {
                        #region fetch previous on off condition of dosing output from database and percentage from chemical name master
                        DataTable chemicalConditionDT = new DataTable();
                        try
                        {
                            ServiceResponse<DataTable> result = IndiSCADADataAccess.DataAccessSelect.getConditionFromChemicalMaster();

                            if (result.Response != null)
                            {
                                if (result.Response.Rows.Count > 0)
                                {
                                    chemicalConditionDT = result.Response;
                                    ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalConsumption() getConditionFromChemicalMaster() result is not null", DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                                }
                            }

                            //fetch chemicals percentage from chemical name master
                            ServiceResponse<DataTable> ResultShiftData = IndiSCADADataAccess.DataAccessSelect.getAllChemicalNameData();
                            if (ResultShiftData.Status == ResponseType.S)
                            {
                                ChemicalNamesWithPercentageDT = ResultShiftData.Response;
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalConsumption() getConditionFromChemicalMaster()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        #endregion

                        OnOffCondition = chemicalConditionDT.Rows[i]["OnOffCondition"].ToString();//"0";//

                        if (DosingOutput[0] == "1")//if output is 1
                        {
                          //  ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalConsumption() DosingOutput is 1", DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                            if (OnOffCondition == "0" || OnOffCondition == "false" || OnOffCondition == "False") //if not already 1 then insert new record
                            {
                                DosingOperationDataEntity _DataChemicalConsumption = new DosingOperationDataEntity();
                                _DataChemicalConsumption.PumpNo = chemicalDT.Rows[i]["PumpName"].ToString();

                                #region mode of operation
                                try
                                {
                                    Address = new String[] { chemicalDT.Rows[i]["ModeOfOperation"].ToString() };
                                    string[] ModeOfOperation = CommunicationWithPLC.Read_BlockAddress(Address, 1);
                                    _DataChemicalConsumption.ModeOfOperation = ModeOfOperation[0].ToString();
                                }
                                catch (Exception ex)
                                {
                                    ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalConsumption() mode of operation", DateTime.Now.ToString(), ex.Message, null, true);
                                }
                                #endregion

                                #region SetTime
                                try
                                {
                                    Address = new String[] { chemicalDT.Rows[i]["SetTime"].ToString() };
                                    string[] SetTime = CommunicationWithPLC.Read_BlockAddress(Address, 1);
                                    _DataChemicalConsumption.SetTime = Convert.ToInt16(SetTime[0].ToString());
                                }
                                catch (Exception ex)
                                {
                                    ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalConsumption() SetTime", DateTime.Now.ToString(), ex.Message, null, true);
                                }
                                #endregion

                                #region SetFlowRate
                                try
                                {
                                    string[] SetFlowRate = CommunicationWithPLC.Real_ReadPLCTagValue("DosingFlowRatemlperSec"); // real value of flow rate
                                    _DataChemicalConsumption.SetFlowRate = Convert.ToDouble(SetFlowRate[i].ToString());
                                }
                                catch (Exception ex)
                                {
                                    ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalConsumption() SetFlowRate", DateTime.Now.ToString(), ex.Message, null, true);
                                }
                                #endregion
                                 
                                #region SetQuantity
                                try
                                {

                                    double Quantity = 00;
                                    string[] SetQuantity = CommunicationWithPLC.Real_ReadPLCTagValue("DosingQuantityInml");//new string[] { "200", "100", "250", "1000" }; ///
                                    Quantity = Convert.ToDouble(SetQuantity[i].ToString());

                                    //if table is empty then insert quanity which is readed as 100% for pump
                                    if (ChemicalNamesWithPercentageDT.Rows.Count == 0 || ChemicalNamesWithPercentageDT == null)
                                    {
                                        ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalNamesWithPercentageDT is null/having 0 record", DateTime.Now.ToString(), "", null, true);
                                        _DataChemicalConsumption.ChemicalName = chemicalDT.Rows[i]["ChemicalName"].ToString(); 
                                        _DataChemicalConsumption.SetQuantity = Convert.ToDouble(SetQuantity[i].ToString()); 
                                    }
                                    else
                                    {
                                        DataRow[] results = ChemicalNamesWithPercentageDT.Select("PumpNo='" + chemicalDT.Rows[i]["PumpNo"].ToString() + "'");

                                        ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalNamesWithPercentageDT result Count is "+results.Count().ToString()+" for pump =" + chemicalDT.Rows[i]["PumpNo"].ToString(), DateTime.Now.ToString(), "", null, true);

                                        if (results.Count() > 0)
                                        {
                                            for (int record = 0; record < results.Length; record++)
                                            {
                                                _DataChemicalConsumption.ChemicalName = results[record]["ChemicalName"].ToString();
                                                _DataChemicalConsumption.SetQuantity = Quantity * Convert.ToDouble(results[record]["ChemicalPercentage"].ToString()) / 100; 
                                            }
                                        }
                                        else //if record is not avalible for pump then insert quanity 100% for pump
                                        { 
                                            _DataChemicalConsumption.ChemicalName = chemicalDT.Rows[i]["ChemicalName"].ToString();
                                            _DataChemicalConsumption.SetQuantity = Convert.ToDouble(SetQuantity[i].ToString());
                                        }
                                    }
                                    IndiSCADADataAccess.DataAccessInsert.InsertChemicalConsumptionData(_DataChemicalConsumption);
                                }
                                catch (Exception ex)
                                {
                                    ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalConsumption() SetQuantity", DateTime.Now.ToString(), ex.Message, null, true);
                                }
                                #endregion 

                                IndiSCADADataAccess.DataAccessUpdate.UpdateChemicalMaster(i + 1, true);  //here we have to update OnOffCondition=1 in database
                            }
                        }
                        else if (DosingOutput[0] == "0") //if it is 0
                        {
                          //  ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalConsumption() DosingOutput is 0", DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                            if (OnOffCondition == "1" || OnOffCondition == "true" || OnOffCondition == "True")  //if not already zero  then update in database
                            {
                                IndiSCADADataAccess.DataAccessUpdate.UpdateChemicalMaster(i + 1, false); //here we have to update OnOffCondition=0 in database
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { ErrorLogger.LogError.ErrorLog("TrendDataLog ChemicalConsumption()", DateTime.Now.ToString(), ex.Message, null, true); }

        }


        #endregion
    }
}
