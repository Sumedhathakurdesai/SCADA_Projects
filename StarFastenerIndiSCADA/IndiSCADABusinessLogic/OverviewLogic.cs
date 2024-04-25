using IndiSCADAEntity.Entity;
using IndiSCADAGlobalLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADABusinessLogic
{

    public static class OverviewLogic
    {
        static ServiceResponse<IList> _WagonPrerequisitesParameterList = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("WagonPrerequisites");
        //static ServiceResponse<IList> _WagonBasketPrerequisitesParameterList = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("WagonBasketPrerequisites");
        static ServiceResponse<IList> _CTPrerequisitesParameterList = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("CTPrerequisites");
        static ServiceResponse<IList> _UnLoaderPrerequisitesParameterList = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("UnloaderPrerequisites");
        static ServiceResponse<IList> _DryerPrerequisitesParameterList = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("DryerPrerequisites");
        static ServiceResponse<IList> _BarrelPrerequisitesParameterList = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("SystemPrerequisites");

        #region Public/private methods

        public static ServiceResponse<DataTable> GetStationNameIndex(int StationNo)
        {
            ServiceResponse<DataTable> result = new ServiceResponse<DataTable>();
            try
            {
                result = IndiSCADADataAccess.DataAccessSelect.ReturnDataTableForStation("StationMasterTankDetailsStation", "SP_StationMaster", StationNo);
                return result;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OverviewLogic SelectStationName()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }

        public static ServiceResponse<IList> SelectStationName()
        {
            ServiceResponse<IList>  result = new ServiceResponse<IList>();
            try
            {
                result = IndiSCADATranslation.OverviewTranslation.GetStationNameNameFromDB();
                if(result.Status == ResponseType.S)
                {
                    if (result.Response != null )
                    {
                        IList<OverViewEntity> _IListresult = (IList<OverViewEntity>)(result.Response);
                        foreach (var item in _IListresult)
                        {
                            if (IndiSCADAGlobalLibrary.TagList.LoadNoAtStation[Convert.ToInt32(item.StationNo)] != null)
                            {
                                if (IndiSCADAGlobalLibrary.TagList.LoadNoAtStation[Convert.ToInt32(item.StationNo)] != "" && IndiSCADAGlobalLibrary.TagList.LoadNoAtStation[Convert.ToInt32(item.StationNo)] != "0")
                                {
                                    item.LoadVisibility = "Visible";
                                }
                                else
                                {
                                    item.LoadVisibility = "Hidden";
                                }
                            }
                            else
                            {
                                item.LoadVisibility = "Hidden";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OverviewLogic SelectStationName()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }

        //to control load visibility
        public static ServiceResponse<IList> SelectStationName(int lineno)
        {
            ServiceResponse<IList> result = new ServiceResponse<IList>();
            try
            {
                result = IndiSCADATranslation.OverviewTranslation.GetStationNameForLineFromDB(lineno);
                if (result.Status == ResponseType.S)
                {
                    if (result.Response != null)
                    {
                        IList<OverViewEntity> _IListresult = (IList<OverViewEntity>)(result.Response);
                        
                        foreach (var item in _IListresult)
                        { 
                           
                            if (IndiSCADAGlobalLibrary.TagList.LoadNoAtStation[Convert.ToInt32(item.StationNo)-1] != null)
                            {
                                if (IndiSCADAGlobalLibrary.TagList.LoadNoAtStation[Convert.ToInt32(item.StationNo)-1] != "" && IndiSCADAGlobalLibrary.TagList.LoadNoAtStation[Convert.ToInt32(item.StationNo)-1] != "0")
                                {
                                    item.LoadVisibility = "Visible";
                                }
                                else
                                {
                                    item.LoadVisibility = "Hidden";
                                }
                            }
                            else
                            {
                                item.LoadVisibility = "Hidden";
                            }
                        }
                    }
                }
                else
                {
                    ErrorLogger.LogError.ErrorLog("OverviewLogic while calling GetStationNameForLineFromDB()", DateTime.Now.ToString(), "" +ResponseType.S, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OverviewLogic SelectStationName()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }

        public static string GetWagonCurrentOperation(int wagonNo)
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
                        else if (w1Operation == "5")
                        {
                            WagonOperations = "W1 Put On Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[0]; ;
                        }
                        else
                        {
                            WagonOperations = "Waiting for instruction";
                        }
                    }
                    else
                    {
                        ErrorLogger.LogError.ErrorLog("OverviewLogic getWagonCurrentOperation(W1)", DateTime.Now.ToString(), "Wagon1 Next step values null records.", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                        else if (w1Operation == "5")
                        {
                            WagonOperations = "W2 Put On Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[1]; ;
                        }
                        else
                        {
                            WagonOperations = "Waiting for instruction";
                        }
                    }
                    else
                    {
                        ErrorLogger.LogError.ErrorLog("OverviewLogic getWagonCurrentOperation(W2)", DateTime.Now.ToString(), "Wagon2 Next step values null records.", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                        else if (w1Operation == "5")
                        {
                            WagonOperations = "W3 Put On Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[2]; ;
                        }
                        else
                        {
                            WagonOperations = "Waiting for instruction";
                        }
                    }
                    else
                    {
                        ErrorLogger.LogError.ErrorLog("OverviewLogic getWagonCurrentOperation(W3)", DateTime.Now.ToString(), "Wagon2 Next step values null records.", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                        else if (w1Operation == "5")
                        {
                            WagonOperations = "W4 Put On Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[3]; ;
                        }
                        else
                        {
                            WagonOperations = "Waiting for instruction";
                        }
                    }
                    else
                    {
                        ErrorLogger.LogError.ErrorLog("OverviewLogic getWagonCurrentOperation(W4)", DateTime.Now.ToString(), "Wagon2 Next step values null records.", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                            WagonOperations = "W5 Put On Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[4]; ;
                        }
                        else
                        {
                            WagonOperations = "Waiting for instruction";
                        }
                    }
                    else
                    {
                        ErrorLogger.LogError.ErrorLog("OverviewLogic getWagonCurrentOperation(W5)", DateTime.Now.ToString(), "Wagon2 Next step values null records.", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                            WagonOperations = "W6 Put On Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[5]; ;
                        }
                        else
                        {
                            WagonOperations = "Waiting for instruction";
                        }
                    }
                    else
                    {
                        ErrorLogger.LogError.ErrorLog("OverviewLogic getWagonCurrentOperation(W6)", DateTime.Now.ToString(), "Wagon6 Next step values null records.", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                            WagonOperations = "W7 Put On Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[6]; ;
                        }
                        else
                        {
                            WagonOperations = "Waiting for instruction";
                        }
                    }
                    else
                    {
                        ErrorLogger.LogError.ErrorLog("OverviewLogic getWagonCurrentOperation(W7)", DateTime.Now.ToString(), "Wagon7 Next step values null records.", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                //if (wagonNo == 8)
                //{
                //    if (IndiSCADAGlobalLibrary.TagList.WagonNextStep != null && IndiSCADAGlobalLibrary.TagList.WagonNextStation.Length != 0)
                //    {
                //        string w1Operation = IndiSCADAGlobalLibrary.TagList.WagonNextStep[7];
                //        if (w1Operation == "4")
                //        {
                //            WagonOperations = "W8 Get From Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[7];
                //        }
                //        if (w1Operation == "5")
                //        {
                //            WagonOperations = "W8 Put On Stn : " + IndiSCADAGlobalLibrary.TagList.WagonNextStation[7]; ;
                //        }
                //        else
                //        {
                //            WagonOperations = "Waiting for instruction";
                //        }
                //    }
                //    else
                //    {
                //        ErrorLogger.LogError.ErrorLog("OverviewLogic getWagonCurrentOperation(W8)", DateTime.Now.ToString(), "Wagon8 Next step values null records.", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                //    }
                //}
                return WagonOperations;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OverviewLogic" +" getWagonCurrentOperation()", DateTime.Now.ToString(), ex.Message, null, true);
                return null;
            }
        }
        
        public static string GetWagonStatus(int wagonNo)
        {
            try
            {
                string WagonStatus = " ";
                //Get Wagon AM
                string[] WagonAutoMan = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("WagonAutoMan");
                if (wagonNo == 1)
                {
                    WagonStatus = WagonAutoMan[0];
                }
                if (wagonNo == 2)
                {
                    WagonStatus = WagonAutoMan[1];
                }
                if (wagonNo == 3)
                {
                    WagonStatus = WagonAutoMan[2];
                }
                if (wagonNo == 4)
                {
                    WagonStatus = WagonAutoMan[3];
                }
                if (wagonNo == 5)
                {
                    WagonStatus = WagonAutoMan[4];
                }
                if (wagonNo == 6)
                {
                    WagonStatus = WagonAutoMan[5];
                }
                if (wagonNo == 7)
                {
                    WagonStatus = WagonAutoMan[6];
                }
                //if (wagonNo == 8)
                //{
                //    WagonStatus = WagonAutoMan[7];
                //}
                return WagonStatus;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OverviewLogic" + " GetWagonStatus()", DateTime.Now.ToString(), ex.Message, null, true);
                return null;
            }
        }

        public static string GetPlantStatus(string Parameter)  //need to check
        {
            try
            {
                string PlantStatus = " ";

                string[] SystemInPut =  DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("SystemInput");   //{ "1","1","1","1","1"};
                if (Parameter == "AM")
                {
                    PlantStatus = SystemInPut[0];//auto manual
                }
                if (Parameter == "Service") 
                {
                    PlantStatus = SystemInPut[4];//emergency
                }
                if (Parameter == "CycleON")
                {
                    PlantStatus = SystemInPut[1];  //cycle start
                }
                if (Parameter == "Reset")
                {
                    PlantStatus = SystemInPut[2];  //cycle reset
                }
                if (Parameter == "PowerON")
                {
                    PlantStatus = SystemInPut[3];//power on
                }
                return PlantStatus;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OverviewLogic" + " PlantStatus()", DateTime.Now.ToString(), ex.Message, null, true);
                return null;
            }
        }

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

        public static ObservableCollection<OverViewEntity> GetWagonPrerequisites()
        {
            ObservableCollection<OverViewEntity> _WagonInputs = new ObservableCollection<OverViewEntity>();
            try
            {
                //string[] Wagon1Inputs = new string[] { "1", "1", "1", "1", "1", "1" };
                //string[] Wagon2Inputs = new string[] { "1", "1", "1", "1", "1", "1" };
                //string[] Wagon3Inputs = new string[] { "1", "1", "1", "1", "1", "1" };
                //string[] Wagon4Inputs = new string[] { "1", "1", "1", "1", "1", "1" };
                //string[] Wagon5Inputs = new string[] { "1", "1", "1", "1", "1", "1" };
                //string[] Wagon6Inputs = new string[] { "1", "1", "1", "1", "1", "1" };
                //string[] Wagon7Inputs = new string[] { "1", "1", "1", "1", "1", "1" };


                string[] Wagon1Inputs = new string[] { }; string[] Wagon2Inputs = new string[] { }; string[] Wagon3Inputs = new string[] { };
                string[] Wagon4Inputs = new string[] { }; string[] Wagon5Inputs = new string[] { }; string[] Wagon6Inputs = new string[] { };
                string[] Wagon7Inputs = new string[] { }; string[] Wagon8Inputs = new string[] { };

                try
                {
                    //Get W1Inputs

                    //string[] Wagon1Inputs1 = new string[] { "1", "1", "1", "1", "1", "1" };
                    string[] Wagon1Inputs1 = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon1Prereqisites");
                    var list = new List<string>();
                    for (int i = 0; i < Wagon1Inputs1.Length; i++)
                    {
                        if (i >= 4)
                        {
                            list.Add("2");
                        }
                        else
                        {
                            list.Add(Wagon1Inputs1[i]);
                        }
                    }
                    Wagon1Inputs = list.ToArray();

                    //string[] Wagon2Inputs2 = new string[] { "1", "1", "1", "1", "1", "1" };
                    string[] Wagon2Inputs2 = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon2Prereqisites");
                    var list1 = new List<string>();
                    for (int i = 0; i < Wagon1Inputs1.Length; i++)
                    {
                        if (i >= 4)
                        {
                            list1.Add("2");
                        }
                        else
                        {
                            list1.Add(Wagon2Inputs2[i]);
                        }
                    }
                    Wagon2Inputs = list1.ToArray();


                    //Get W3Inputs
                    //string[] Wagon3Inputs1 = new string[] { "1", "1", "1", "1", "1", "1" };
                    string[] Wagon3Inputs1 = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon3Prereqisites");
                    var list2 = new List<string>();
                    for (int i = 0; i < Wagon1Inputs1.Length; i++)
                    {
                        if (i >= 4)
                        {
                            list2.Add("2");
                        }
                        else
                        {
                            list2.Add(Wagon3Inputs1[i]);
                        }
                    }
                    Wagon3Inputs = list2.ToArray();

                    ////Get W4Inputs
                    //string[] Wagon4Inputs1 = new string[] { "1", "1", "1", "1", "1", "1"};
                    string[] Wagon4Inputs1 = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon4Prereqisites");
                    var list3 = new List<string>();
                    for (int i = 0; i < Wagon1Inputs1.Length; i++)
                    {
                        if (i >= 4)
                        {
                            list3.Add("2");
                        }
                        else
                        {
                            list3.Add(Wagon4Inputs1[i]);
                        }
                    }
                    Wagon4Inputs = list3.ToArray();

                    //Get W5Inputs
                    //Wagon5Inputs = new string[]  { "1", "1", "1", "1", "1", "1"};
                    Wagon5Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon5Prereqisites");


                    ////Get W5Inputs
                    //Wagon6Inputs = new string[] { "1", "1", "1", "1", "1", "1" };
                    Wagon6Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon6Prereqisites");


                    //Get W5Inputs
                    //Wagon7Inputs =new string[] { "1", "1", "1", "1", "1", "1" };
                    Wagon7Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon7Prereqisites");



                }
                catch (Exception ex)
                { ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagonInputs() ", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable); }

             
                int index = 0;
                if (_WagonPrerequisitesParameterList.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_WagonPrerequisitesParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        OverViewEntity _IOStatusEntity = new OverViewEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == Wagon1Inputs.Length)
                            {
                                _IOStatusEntity.W1 = ValueFromArray(Wagon1Inputs, index);
                                _IOStatusEntity.W2 = ValueFromArray(Wagon2Inputs, index);
                                _IOStatusEntity.W3 = ValueFromArray(Wagon3Inputs, index);
                                _IOStatusEntity.W4 = ValueFromArray(Wagon4Inputs, index);
                                _IOStatusEntity.W5 = ValueFromArray(Wagon5Inputs, index);
                                _IOStatusEntity.W6 = ValueFromArray(Wagon6Inputs, index);
                                _IOStatusEntity.W7 = ValueFromArray(Wagon7Inputs, index); 
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagonInputs() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _WagonInputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _WagonInputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagonInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _WagonInputs;
            }
        }

        public static ObservableCollection<OverViewEntity> GetCTPrerequisites()
        {
            ObservableCollection<OverViewEntity> _WagonInputs = new ObservableCollection<OverViewEntity>();
            try
            {
                //string[] CT1Inputs = new string[] { "1", "1", "1" };
                string[] CT1Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("CT1Prerequisites");

                int index = 0;
                if (_CTPrerequisitesParameterList.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_CTPrerequisitesParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        OverViewEntity _IOStatusEntity = new OverViewEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == CT1Inputs.Length)
                            {
                                _IOStatusEntity.CT1 = ValueFromArray(CT1Inputs, index); 
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetCTPrerequisites() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _WagonInputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _WagonInputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetCTPrerequisites()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _WagonInputs;
            }
        }

        public static ObservableCollection<OverViewEntity> GetDryerPrerequisites()
        {
            ObservableCollection<OverViewEntity> _WagonInputs = new ObservableCollection<OverViewEntity>();
            try
            {

                //Get W1Inputs
                //string[] Dryer1Inputs = new string[] { "1", "1" };
                //string[] Dryer2Inputs = new string[] { "1", "1" };
                //string[] Dryer3Inputs = new string[] { "1", "1" };
                //string[] Dryer4Inputs = new string[] { "1", "1" };

                string[] Dryer1Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Dryer1Prerequisites");
                string[] Dryer2Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Dryer2Prerequisites");
                string[] Dryer3Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Dryer3Prerequisites");
                string[] Dryer4Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Dryer4Prerequisites");


                int index = 0;
                if (_DryerPrerequisitesParameterList.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_DryerPrerequisitesParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        OverViewEntity _IOStatusEntity = new OverViewEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == Dryer1Inputs.Length )
                            {
                                _IOStatusEntity.D1 = ValueFromArray(Dryer1Inputs, index);
                                _IOStatusEntity.D2 = ValueFromArray(Dryer2Inputs, index);
                                _IOStatusEntity.D3 = ValueFromArray(Dryer3Inputs, index);
                                _IOStatusEntity.D4 = ValueFromArray(Dryer4Inputs, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetDryerPrerequisites() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _WagonInputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _WagonInputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetDryerPrerequisites()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _WagonInputs;
            }
        }

        public static ObservableCollection<OverViewEntity> GetUnloaderPrerequisites()
        {
            ObservableCollection<OverViewEntity> _WagonInputs = new ObservableCollection<OverViewEntity>();
            try
            {
                //Get W1Inputs
                //string[] Wagon1Inputs = new string[] { "1", "1" };

                string[] Wagon1Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("UnloaderPrerequisites");

                int index = 0;
                if (_UnLoaderPrerequisitesParameterList.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_UnLoaderPrerequisitesParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        OverViewEntity _IOStatusEntity = new OverViewEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == Wagon1Inputs.Length)
                            {
                                _IOStatusEntity.Unloader = ValueFromArray(Wagon1Inputs, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetUnloaderPrerequisites() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _WagonInputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _WagonInputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetUnloaderPrerequisites()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _WagonInputs;
            }
        }

        public static ObservableCollection<OverViewEntity> GetBarrelPrerequisites()
        {
            ObservableCollection<OverViewEntity> _WagonInputs = new ObservableCollection<OverViewEntity>();
            try
            {
                //Get W1Inputs
                //string[] Wagon1Inputs = new string[] { "1", "1", "1", "1" };

                string[] Wagon1Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("SystemPrerequisites");

                int index = 0;
                if (_BarrelPrerequisitesParameterList.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_BarrelPrerequisitesParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        OverViewEntity _IOStatusEntity = new OverViewEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == Wagon1Inputs.Length)
                            {
                                _IOStatusEntity.Unloader = ValueFromArray(Wagon1Inputs, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetUnloaderPrerequisites() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _WagonInputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _WagonInputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetUnloaderPrerequisites()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _WagonInputs;
            }
        }


        
        public static string GetNetworkData(string NetworkData)
        {
            string NetworkInfo = ""; 
            try
            {
                if (NetworkData.Contains("Temperature"))
                {
                    #region Temperature
                    if (NetworkData == "Temperature 1")
                    {
                        NetworkInfo = "Temperature 1"
                            + System.Environment.NewLine + "Modbus Card 1"
                            + System.Environment.NewLine + "Node Address : 2";
                    }
                    if (NetworkData == "Temperature 2")
                    {
                        NetworkInfo = "Temperature 2"
                            + System.Environment.NewLine + "Modbus Card 1"
                            + System.Environment.NewLine + "Node Address : 3";
                    }
                    if (NetworkData == "Temperature 3")
                    {
                        NetworkInfo = "Temperature 3"
                            + System.Environment.NewLine + "Modbus Card 1"
                            + System.Environment.NewLine + "Node Address : 4";
                    }
                    if (NetworkData == "Temperature 4")
                    {
                        NetworkInfo = "Temperature 4"
                            + System.Environment.NewLine + "Modbus Card 1"
                            + System.Environment.NewLine + "Node Address : 5";
                    }
                    if (NetworkData == "Temperature 5")
                    {
                        NetworkInfo = "Temperature 5"
                            + System.Environment.NewLine + "Modbus Card 1"
                            + System.Environment.NewLine + "Node Address : 6";
                    }
                    if (NetworkData == "Temperature 6")
                    {
                        NetworkInfo = "Temperature 6"
                            + System.Environment.NewLine + "Modbus Card 1"
                            + System.Environment.NewLine + "Node Address : 7";
                    }
                    if (NetworkData == "Temperature 7")
                    {
                        NetworkInfo = "Temperature 7"
                            + System.Environment.NewLine + "Modbus Card 1"
                            + System.Environment.NewLine + "Node Address : 8";
                    }
                    if (NetworkData == "Temperature 8")
                    {
                        NetworkInfo = "Temperature 8"
                            + System.Environment.NewLine + "Modbus Card 1"
                            + System.Environment.NewLine + "Node Address : 9";
                    }
                    if (NetworkData == "Temperature 9")
                    {
                        NetworkInfo = "Temperature 9"
                            + System.Environment.NewLine + "Modbus Card 1"
                            + System.Environment.NewLine + "Node Address : 10";
                    }
                    if (NetworkData == "Temperature 10")
                    {
                        NetworkInfo = "Temperature 10"
                            + System.Environment.NewLine + "Modbus Card 1"
                            + System.Environment.NewLine + "Node Address : 11";
                    }
                    if (NetworkData == "Temperature 11")
                    {
                        NetworkInfo = "Temperature 11"
                            + System.Environment.NewLine + "Modbus Card 1"
                            + System.Environment.NewLine + "Node Address : 12";
                    }
                    if (NetworkData == "Temperature 12")
                    {
                        NetworkInfo = "Temperature 12"
                            + System.Environment.NewLine + "Modbus Card 1"
                            + System.Environment.NewLine + "Node Address : 13";
                    }
                    if (NetworkData == "Temperature 13")
                    {
                        NetworkInfo = "Temperature 13"
                            + System.Environment.NewLine + "Modbus Card 1"
                            + System.Environment.NewLine + "Node Address : 14";
                    }
                    if (NetworkData == "Temperature 14")
                    {
                        NetworkInfo = "Temperature 14"
                            + System.Environment.NewLine + "Modbus Card 1"
                            + System.Environment.NewLine + "Node Address : 15";
                    }
                    if (NetworkData == "Temperature 15")
                    {
                        NetworkInfo = "Temperature 15"
                            + System.Environment.NewLine + "Modbus Card 1"
                            + System.Environment.NewLine + "Node Address : 16";
                    }



                    if (NetworkData == "Temperature 16")
                    {
                        NetworkInfo = "Temperature 16"
                            + System.Environment.NewLine + "Modbus Card 1"
                            + System.Environment.NewLine + "Node Address : 1";
                    }
                    if (NetworkData == "Temperature 17")
                    {
                        NetworkInfo = "Temperature 17"
                            + System.Environment.NewLine + "Modbus Card 2"
                        + System.Environment.NewLine + "Node Address : 2";
                    }
                    if (NetworkData == "Temperature 18")
                    {
                        NetworkInfo = "Temperature 18"
                            + System.Environment.NewLine + "Modbus Card 2"
                        + System.Environment.NewLine + "Node Address : 3";
                    }
                    if (NetworkData == "Temperature 19")
                    {
                        NetworkInfo = "Temperature 19"
                            + System.Environment.NewLine + "Modbus Card 2"
                        + System.Environment.NewLine + "Node Address : 4";
                    }
                    if (NetworkData == "Temperature 20")
                    {
                        NetworkInfo = "Temperature 20"
                            + System.Environment.NewLine + "Modbus Card 2"
                        + System.Environment.NewLine + "Node Address : 5";
                    }
                    if (NetworkData == "Temperature 21")
                    {
                        NetworkInfo = "Temperature 21"
                            + System.Environment.NewLine + "Modbus Card 2"
                        + System.Environment.NewLine + "Node Address : 6";
                    }
                    if (NetworkData == "Temperature 22")
                    {
                        NetworkInfo = "Temperature 22"
                            + System.Environment.NewLine + "Modbus Card 2"
                        + System.Environment.NewLine + "Node Address : 7";
                    }
                    if (NetworkData == "Temperature 23")
                    {
                        NetworkInfo = "Temperature 23"
                            + System.Environment.NewLine + "Modbus Card 2"
                        + System.Environment.NewLine + "Node Address : 8";
                    }
                    if (NetworkData == "Temperature 24")
                    {
                        NetworkInfo = "Temperature 24"
                            + System.Environment.NewLine + "Modbus Card 2"
                        + System.Environment.NewLine + "Node Address : 9";
                    }
                    if (NetworkData == "Temperature 25")
                    {
                        NetworkInfo = "Temperature 25"
                            + System.Environment.NewLine + "Modbus Card 2"
                        + System.Environment.NewLine + "Node Address : 10";
                    }
                    #endregion
                }
                else if (NetworkData.Contains("Rectifier"))
                {
                    #region Rectifier
                    if (NetworkData == "Rectifier 1")
                    {
                        NetworkInfo = "Rectifier 1"
                            + System.Environment.NewLine + "Modbus Card 1"
                            + System.Environment.NewLine + "Node Address : Node 1"
                            + System.Environment.NewLine + "Current Rating : 1500Amp";
                    }
                    else if (NetworkData == "Rectifier 2")
                    {
                        NetworkInfo = "Rectifier 2"
                            + System.Environment.NewLine + "Modbus Card 1"
                            + System.Environment.NewLine + "Node Address : Node 2"
                            + System.Environment.NewLine + "Current Rating : 1500Amp";
                    }
                    else if (NetworkData == "Rectifier 3")
                    {
                        NetworkInfo = "Rectifier 3"
                            + System.Environment.NewLine + "Modbus Card 1"
                            + System.Environment.NewLine + "Node Address : Node 3"
                            + System.Environment.NewLine + "Current Rating : 1500Amp";
                    }
                    else if (NetworkData == "Rectifier 4")
                    {
                        NetworkInfo = "Rectifier 4"
                            + System.Environment.NewLine + "Modbus Card 1"
                            + System.Environment.NewLine + "Node Address : Node 4"
                            + System.Environment.NewLine + "Current Rating : 1500Amp";
                    }
                    else if (NetworkData == "Rectifier 5")
                    {
                        NetworkInfo = "Rectifier 5"
                            + System.Environment.NewLine + "Modbus Card 1"
                            + System.Environment.NewLine + "Node Address : Node 5"
                            + System.Environment.NewLine + "Current Rating : 1500Amp";
                    }
                    else if (NetworkData == "Rectifier 6")
                    {
                        NetworkInfo = "Rectifier 6"
                            + System.Environment.NewLine + "Modbus Card 1"
                            + System.Environment.NewLine + "Node Address : Node 6"
                            + System.Environment.NewLine + "Current Rating : 1500Amp";
                    }


                    else if (NetworkData == "Rectifier 7")
                    {
                        NetworkInfo = "Rectifier 7"
                            + System.Environment.NewLine + "Modbus Card 2"
                            + System.Environment.NewLine + "Node Address : Node 1"
                            + System.Environment.NewLine + "Current Rating : 1500Amp";
                    }
                    else if (NetworkData == "Rectifier 8")
                    {
                        NetworkInfo = "Rectifier 8"
                            + System.Environment.NewLine + "Modbus Card 2"
                            + System.Environment.NewLine + "Node Address : Node 2"
                            + System.Environment.NewLine + "Current Rating : 1500Amp";
                    }
                    else if (NetworkData == "Rectifier 9")
                    {
                        NetworkInfo = "Rectifier 9"
                            + System.Environment.NewLine + "Modbus Card 2"
                            + System.Environment.NewLine + "Node Address : Node 3"
                            + System.Environment.NewLine + "Current Rating : 1500Amp";
                    }
                    else if (NetworkData == "Rectifier 10")
                    {
                        NetworkInfo = "Rectifier 10"
                            + System.Environment.NewLine + "Modbus Card 2"
                            + System.Environment.NewLine + "Node Address : Node 4"
                            + System.Environment.NewLine + "Current Rating : 1500Amp";
                    }
                    else if (NetworkData == "Rectifier 11")
                    {
                        NetworkInfo = "Rectifier 11"
                            + System.Environment.NewLine + "Modbus Card 2"
                            + System.Environment.NewLine + "Node Address : Node 5"
                            + System.Environment.NewLine + "Current Rating : 1500Amp";
                    }
                    else if (NetworkData == "Rectifier 12")
                    {
                        NetworkInfo = "Rectifier 12"
                            + System.Environment.NewLine + "Modbus Card 2"
                            + System.Environment.NewLine + "Node Address : Node 5"
                            + System.Environment.NewLine + "Current Rating : 1500Amp";
                    }
                    else if (NetworkData == "Rectifier 13")
                    {
                        NetworkInfo = "Rectifier 13"
                            + System.Environment.NewLine + "Modbus Card 2"
                            + System.Environment.NewLine + "Node Address : Node 5"
                            + System.Environment.NewLine + "Current Rating : 1500Amp";
                    }
                    #endregion
                }
                else if (NetworkData.Contains("pH"))
                {
                    #region pH
                    if (NetworkData == "pH 1")
                    {
                        NetworkInfo = "pH 1"
                            + System.Environment.NewLine + "MOdbus Card 2";
                    }
                    else if (NetworkData == "pH 2")
                    {
                        NetworkInfo = "pH 2"
                            + System.Environment.NewLine + "MOdbus Card 2";
                    }
                    else if (NetworkData == "pH 3")
                    {
                        NetworkInfo = "pH 3"
                            + System.Environment.NewLine + "MOdbus Card 2";
                    }
                    else if (NetworkData == "pH 4")
                    {
                        NetworkInfo = "pH 4"
                            + System.Environment.NewLine + "MOdbus Card 2";
                    }
                    #endregion
                }
                else if (NetworkData.Contains("WCS"))
                {
                    #region WCS
                    if (NetworkData == "WCS 1")
                    {
                        NetworkInfo = "WCS 1"
                           + System.Environment.NewLine + "Device 1 Node 0"
                           + System.Environment.NewLine + "From Station 1 to 8"
                           + System.Environment.NewLine + "Communication Type : Ethernet";
                    }
                    else if (NetworkData == "WCS 2")
                    {
                        NetworkInfo = "WCS 2"
                           + System.Environment.NewLine + "Device 1 Node 1"
                           + System.Environment.NewLine + "From Station 5 to 15"
                           + System.Environment.NewLine + "Communication Type : Ethernet";
                    }
                    else if (NetworkData == "WCS 3")
                    {
                        NetworkInfo = "WCS 3"
                           + System.Environment.NewLine + "Device 1 Node 2"
                           + System.Environment.NewLine + "From Station 11 to 22"
                           + System.Environment.NewLine + "Communication Type : Ethernet";
                    }
                    else if (NetworkData == "WCS 4")
                    {
                        NetworkInfo = "WCS 4"
                           + System.Environment.NewLine + "Device 1 Node 3"
                           + System.Environment.NewLine + "From Station 18 to 40"
                           + System.Environment.NewLine + "Communication Type : Ethernet";
                    }
                    else if (NetworkData == "WCS 5")
                    {
                        NetworkInfo = "WCS 5"
                           + System.Environment.NewLine + "Device 1 Node 0"
                           + System.Environment.NewLine + "From Station 41 to 48"
                           + System.Environment.NewLine + "Communication Type : Ethernet";
                    }
                    else if (NetworkData == "WCS 6")
                    {
                        NetworkInfo = "WCS 6"
                           + System.Environment.NewLine + "Device 1 Node 1"
                           + System.Environment.NewLine + "From Station 46 to 63"
                           + System.Environment.NewLine + "Communication Type : Ethernet";
                    }
                    else if (NetworkData == "WCS 7")
                    {
                        NetworkInfo = "WCS 7"
                           + System.Environment.NewLine + "Device 1 Node 2"
                           + System.Environment.NewLine + "From Station 59 to 69"
                           + System.Environment.NewLine + "Communication Type : Ethernet";
                    }
                    #endregion
                }
                else
                {
                    if (NetworkData == "HMI")
                    {
                        NetworkInfo = "HMI"
                        + System.Environment.NewLine + "Plc Name : Omron"
                        + System.Environment.NewLine + "PLC communication address details : 192.168.250.2";
                    }

                    else if (NetworkData == "Main PLC 1")
                    {
                        NetworkInfo = "Main PLC 1"
                           + System.Environment.NewLine + "Plc Name : Omron"
                           + System.Environment.NewLine + "Station/PLC Number : PLC 1"
                           + System.Environment.NewLine + "PLC Model Number : NJ301-1100"
                           + System.Environment.NewLine + "Communication Type : Ethernet"
                           + System.Environment.NewLine + "PLC communication address details : 192.168.250.1";
                    }

                    else if (NetworkData == "SCADA PC")
                    {
                        NetworkInfo = "SCADA PC"
                           + System.Environment.NewLine + "IP Address : 192.168.250.20";
                    }
                    else if (NetworkData == "Ethernet Switch")
                    {
                        NetworkInfo = "Ethernet Switch"
                           + System.Environment.NewLine + "Connected device list :"
                           + System.Environment.NewLine + "1) Main PLC IP Address : 192.168.250.1"
                           + System.Environment.NewLine + "2) HMI : WCS-EIG310, IP Address : 192.168.250.2"
                           + System.Environment.NewLine + "3) Scada PC : IP Address : 192.168.250.20"
                           + System.Environment.NewLine + "4) Device 1 : WCS-EIG310, IP Address : 192.168.250.11"
                           + System.Environment.NewLine + "5) Device 2 : WCS-EIG310, IP Address : 192.168.250.12";
                    }

                    else if (NetworkData == "Device 1")
                    {
                        NetworkInfo = "Device 1"
                           + System.Environment.NewLine + "IP Address : 192.168.250.11"
                           + System.Environment.NewLine + "Device Name : WCS-EIG310";
                    }
                    else if (NetworkData == "Device 2")
                    {
                        NetworkInfo = "Device 2"
                           + System.Environment.NewLine + "IP Address : 192.168.250.12"
                           + System.Environment.NewLine + "Device Name : WCS-EIG310";
                    } 
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetNetworkData()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return "Null";
            }
            return NetworkInfo;
        }

        public static string GetImageName(string ImageName)
        {
            string ImageInfo = "";
            try
            {
                if(ImageName.Contains("Temperature"))
                {
                    ImageInfo = "/Image/Temperature.jpg";
                }
                else if(ImageName.Contains("Rectifier"))
                {
                    ImageInfo = "/Image/Temperature.jpg";
                }
                else if (ImageName.Contains("pH"))
                {
                    ImageInfo = "/Image/Temperature.jpg";
                }
                else if (ImageName.Contains("HMI"))
                {
                    ImageInfo = "/Image/Temperature.jpg";
                }
                else if (ImageName.Contains("Main PLC"))
                {
                    ImageInfo = "/Image/Temperature.jpg";
                }
                else if (ImageName.Contains("SCADA PC"))
                {
                    ImageInfo = "/Image/Temperature.jpg";
                }
                else if (ImageName.Contains("Device"))
                {
                    ImageInfo = "/Image/Temperature.jpg";
                }
                else if (ImageName.Contains("WCS"))
                {
                    ImageInfo = "/Image/Temperature.jpg";
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("HomeBusinessLogic GetImageName()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return "Null";
            }
            return ImageInfo;
        }

        public static string GetNetworkCommunication(string Name)
        {
            string Value="Red";
            try
            {

                if (Name.Contains("WCS"))
                {
                    string[] WCS =DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("NetworkDesignWCS"); // { "1", "1", "1", "1", "1", "1", "1" }; // 
                    if (Name == "WCS1")
                    {
                        if (WCS[0] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "WCS2")
                    {
                        if (WCS[1] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "WCS3")
                    {
                        if (WCS[2] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "WCS4")
                    {
                        if (WCS[3] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "WCS5")
                    {
                        if (WCS[4] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "WCS6")
                    {
                        if (WCS[5] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "WCS7")
                    {
                        if (WCS[6] == "1")
                        {
                            Value = "Green";
                        }
                    }
                }

                else if (Name.Contains("Temp"))
                {
                    string[] Temp = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("NetworkDesignTemp");// { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", }; 
                    if (Name == "Temp1")
                    {
                        if (Temp[0] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp2")
                    {
                        if (Temp[1] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp3")
                    {
                        if (Temp[2] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp4")
                    {
                        if (Temp[3] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp5")
                    {
                        if (Temp[4] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp6")
                    {
                        if (Temp[5] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp7")
                    {
                        if (Temp[6] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp8")
                    {
                        if (Temp[7] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp9")
                    {
                        if (Temp[8] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp10")
                    {
                        if (Temp[9] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp11")
                    {
                        if (Temp[10] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp12")
                    {
                        if (Temp[11] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp13")
                    {
                        if (Temp[12] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp14")
                    {
                        if (Temp[13] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp15")
                    {
                        if (Temp[14] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp16")
                    {
                        if (Temp[15] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp17")
                    {
                        if (Temp[16] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp18")
                    {
                        if (Temp[17] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp19")
                    {
                        if (Temp[18] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp20")
                    {
                        if (Temp[19] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp21")
                    {
                        if (Temp[20] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp22")
                    {
                        if (Temp[21] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp23")
                    {
                        if (Temp[22] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp24")
                    {
                        if (Temp[23] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Temp25")
                    {
                        if (Temp[24] == "1")
                        {
                            Value = "Green";
                        }
                    }
                }

                else if (Name.Contains("Rect"))
                {
                    string[] Rect =  DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("NetworkDesignRect"); //{ "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1"};
                    if (Name == "Rect1")
                    {
                        if (Rect[0] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Rect2")
                    {
                        if (Rect[1] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Rect3")
                    {
                        if (Rect[2] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Rect4")
                    {
                        if (Rect[3] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Rect5")
                    {
                        if (Rect[4] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Rect6")
                    {
                        if (Rect[5] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Rect7")
                    {
                        if (Rect[6] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Rect8")
                    {
                        if (Rect[7] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Rect9")
                    {
                        if (Rect[8] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Rect10")
                    {
                        if (Rect[9] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Rect11")
                    {
                        if (Rect[10] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Rect12")
                    {
                        if (Rect[11] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "Rect13")
                    {
                        if (Rect[12] == "1")
                        {
                            Value = "Green";
                        }
                    }
                }

                else if (Name.Contains("pH"))
                {
                    string[] pH =  DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("NetworkDesignpH");//{ "1", "1", "1", "1" };//
                    if (Name == "pH1")
                    {
                        if (pH[0] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "pH2")
                    {
                        if (pH[1] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "pH3")
                    {
                        if (pH[2] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "pH4")
                    {
                        if (pH[3] == "1")
                        {
                            Value = "Green";
                        }
                    }
                }

                else
                {
                    string[] Values = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("NetworkDesignMainPLC");  //{ "1", "1", "1", "1", "1", "1", "1" };
                    if (Name == "Device1")
                    {
                        if (Values[0] == "1")
                        {
                            Value = "Green";
                        }
                    }
                   else if (Name == "Device2")
                    {
                        if (Values[1] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "ScadaPc")
                    {
                        if (Values[2] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "HMI")
                    {
                        if (Values[3] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "MainPLC1")
                    {
                        if (Values[4] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "MainPLC1_TO_ECAT1")
                    {
                        if (Values[5] == "1")
                        {
                            Value = "Green";
                        }
                    }
                    else if (Name == "ECAT1_TO_ECAT2")
                    {
                        if (Values[6] == "1")
                        {
                            Value = "Green";
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetUnloaderPrerequisites()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return "Red";
            }
            return Value;
        }

        #endregion
    }
}
