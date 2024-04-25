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
    public static class IOStatusLogic
    {
        #region"Declaration"
        //Read only once wagon input paramters
        static ServiceResponse<IList> _WagonInputParameterList = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Wagon Input");
        //Read only once wagon output paramters
        static ServiceResponse<IList> _WagonOutputParameterList = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Wagon Output");
        //Read only once CT outut paramters
        static ServiceResponse<IList> _CTOutputParameterList = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Cross Trolley Output");
        //Read only once CT Input paramters
        static ServiceResponse<IList> _CTInputParameterList = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Cross Trolley Input");

        //Read only once Dryer Output paramters
        static ServiceResponse<IList> _DryerOutputParameterList = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("DryerOutput");
        //Read only once Dryer Input paramters
        static ServiceResponse<IList> _DryerInputParameterList = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Dryer Input");
        //Read only once system input paramters
        static ServiceResponse<IList> _SystemInputParameterList = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("System Input");
        //Read only once systen output paramters
        static ServiceResponse<IList> _SystemOutputParameterList = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("System Output");

        static ServiceResponse<IList> _BarrelMotorOnOffParameterList = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("BaseBarrelSetting");
        static ServiceResponse<IList> _LoaderParameterList = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("loader Input");
        static ServiceResponse<IList> _LoaderOutputParameterList = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("loader Output");

        static ServiceResponse<IList> _UnloaderInputParameterList = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Unloader Input");
        static ServiceResponse<IList> _UnloaderOutPutParameterList = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("unloader Output");

        //Read only once Barrel Wagon Input paramters
        static ServiceResponse<IList> _WagonBarrelInPutParameter = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Wagon Basket Input");
        //Read only once Barrel Wagon OutPut paramters
        static ServiceResponse<IList> _WagonBarrelOutPutParameter = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Wagon Basket Output");



        //Read only once CT Input paramters
        //static ServiceResponse<IList> _TripStatusInputParameterList = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("TripStatus");
        //Station Number details
        static string[] StationName = new string[]{
            "LOAD/ UNLOAD STATION",
            "SOAK CLEANING 1",
            "SOAK CLEANING 2",
            "ULTRASONIC CLEANING",
            "HOT WATER RINSE",
            "ANODIC CLEANING 1",
            "WATER RINSE 1",
            "WATER RINSE 2",
            "ACID PICKLING",
            "WATER RINSE 3",
            "WATER RINSE 4",
            "ACID ACTIVATION",
            "CROSS TANK",
            "WARM WATER RINSE 8",
            "ELECTROLESS NI 1",
            "ELECTROLESS NI 2",
            "ELECTROLESS NI 3",
            "ELECTROLESS NI 4",
            "NI DRAG OUT",
            "WATER RINSE 9",
            "WATER RINSE10",
            "HOT WATER RINSE",
            "HOT AIR DRYING-CUSTOMER SCOPE",
            "UNLOADING"};
        #endregion
        #region Public/private methods
        public static string ValueFromArray(string[] InputArray,int index)
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
        public static ObservableCollection<IOStatusEntity> GetWagonInputs()
        {
            ObservableCollection<IOStatusEntity> _WagonInputs = new ObservableCollection<IOStatusEntity>();
            try
            {
                //string[] Wagon1Inputs = new string[] { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };
                //string[] Wagon2Inputs = new string[] { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };
                //string[] Wagon3Inputs = new string[] { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };
                //string[] Wagon4Inputs = new string[] { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };
                //string[] Wagon5Inputs = new string[] { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };
                //string[] Wagon6Inputs = new string[] { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };
                //string[] Wagon7Inputs = new string[] { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };




                //Get W1Inputs
                string[] Wagon1Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon1Input");//22
                //Get W2Inputs
                string[] Wagon2Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon2Input");//22
                //Get W3Inputs
                string[] Wagon3Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon3Input");//22
                //Get W4Inputs
                string[] Wagon4Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon4Input");//22
                //Get W5Inputs
                string[] Wagon5Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon5Input");//34
                //Get W6Inputs
                string[] Wagon6Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon6Input");//34
                //Get W7Inputs
                string[] Wagon7Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon7Input");//34




                int index = 0;
                if (_WagonInputParameterList.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_WagonInputParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        IOStatusEntity _IOStatusEntity = new IOStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == Wagon1Inputs.Length && lstIOStatus.Count == Wagon2Inputs.Length)
                            {


                                if (index<22)
                                {
                                    _IOStatusEntity.W1 = ValueFromArray(Wagon1Inputs, index);
                                    _IOStatusEntity.W2 = ValueFromArray(Wagon2Inputs, index);
                                    _IOStatusEntity.W3 = ValueFromArray(Wagon3Inputs, index);
                                    _IOStatusEntity.W4 = ValueFromArray(Wagon4Inputs, index);

                                    _IOStatusEntity.W5 = ValueFromArray(Wagon5Inputs, index);
                                    _IOStatusEntity.W6 = ValueFromArray(Wagon6Inputs, index);
                                    _IOStatusEntity.W7 = ValueFromArray(Wagon7Inputs, index);
                                     
                                }
                                else
                                {
                                    _IOStatusEntity.W5 = ValueFromArray(Wagon5Inputs, index);
                                    _IOStatusEntity.W6 = ValueFromArray(Wagon6Inputs, index);
                                    _IOStatusEntity.W7 = ValueFromArray(Wagon7Inputs, index);
                                }
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
        public static ObservableCollection<IOStatusEntity> GetWagonBasketInputs()
        {
            ObservableCollection<IOStatusEntity> _WagonInputs = new ObservableCollection<IOStatusEntity>();
            try
            {

                //Get W1Inputs
                string[] Wagon3Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("W6BasketInput");
                //Get W2Inputs
                string[] Wagon4Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("W7BasketInput");
               // Get W3Inputs
                string[] Wagon6Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("W8BasketInput");
                ////Get W4Inputs
                //string[] Wagon4Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("W4Inputs");
                ////Get W5Inputs
                //string[] Wagon5Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("W5Inputs");
                //ServiceResponse<IList> _result = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("WagonInput");
                int index = 0;
                if (_WagonBarrelInPutParameter.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_WagonBarrelInPutParameter.Response);
                    foreach (var item in lstIOStatus)
                    {
                        IOStatusEntity _IOStatusEntity = new IOStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == Wagon3Inputs.Length && lstIOStatus.Count == Wagon4Inputs.Length && lstIOStatus.Count == Wagon6Inputs.Length)
                            {
                                _IOStatusEntity.W6 = ValueFromArray(Wagon3Inputs, index);
                                _IOStatusEntity.W7 = ValueFromArray(Wagon4Inputs, index);
                                _IOStatusEntity.W8 = ValueFromArray(Wagon6Inputs, index);
                                //_IOStatusEntity.W4 = ValueFromArray(Wagon2Inputs, index);
                                //_IOStatusEntity.W5 = ValueFromArray(Wagon2Inputs, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagonBasketInputs() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _WagonInputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _WagonInputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagonBasketInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _WagonInputs;
            }
        }  
        public static ObservableCollection<IOStatusEntity> GetWagonOutPut()
        {
            ObservableCollection<IOStatusEntity> _WagonOutPuts = new ObservableCollection<IOStatusEntity>();
            try
            {
                //string[] Wagon1OutPuts = new string[] { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };
                //string[] Wagon2OutPuts = new string[] { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };
                //string[] Wagon3OutPuts = new string[] { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };
                //string[] Wagon4OutPuts = new string[] { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };
                //string[] Wagon5OutPuts = new string[] { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };
                //string[] Wagon6OutPuts = new string[] { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };
                //string[] Wagon7OutPuts = new string[] { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };


                //Get W1Inputs
                string[] Wagon1OutPuts = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon1Output");
                //Get W2Inputs
                string[] Wagon2OutPuts = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon2Output");
                //Get W3Inputs
                string[] Wagon3OutPuts = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon3Output");
                //Get W4Inputs
                string[] Wagon4OutPuts = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon4Output");
                //Get W5Inputs
                string[] Wagon5OutPuts = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon5Output");
                //Get W5Inputs
                string[] Wagon6OutPuts = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon6Output");
                //Get W5Inputs
                string[] Wagon7OutPuts = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Wagon7Output");


                int index = 0;
                if (_WagonOutputParameterList.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_WagonOutputParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        IOStatusEntity _IOStatusEntity = new IOStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == Wagon1OutPuts.Length && lstIOStatus.Count == Wagon2OutPuts.Length)
                            {
                                //for wagon 1,2,3,4,5    Rotation C.c.wise status is not present ...for address no 10
                                if(index<=10)
                                {
                                    _IOStatusEntity.W1 = ValueFromArray(Wagon1OutPuts, index);
                                    _IOStatusEntity.W2 = ValueFromArray(Wagon2OutPuts, index);
                                    _IOStatusEntity.W3 = ValueFromArray(Wagon3OutPuts, index);
                                    _IOStatusEntity.W4 = ValueFromArray(Wagon4OutPuts, index);
                                    _IOStatusEntity.W5 = ValueFromArray(Wagon5OutPuts, index);
                                    _IOStatusEntity.W6 = ValueFromArray(Wagon6OutPuts, index);
                                    _IOStatusEntity.W7 = ValueFromArray(Wagon7OutPuts, index);
                                }
                                else
                                {
                                    _IOStatusEntity.W5 = ValueFromArray(Wagon5OutPuts, index);
                                    _IOStatusEntity.W6 = ValueFromArray(Wagon6OutPuts, index);
                                    _IOStatusEntity.W7 = ValueFromArray(Wagon7OutPuts, index);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagonOutPut() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _WagonOutPuts.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _WagonOutPuts;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagonInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _WagonOutPuts;
            }
        }   //11 1
        public static ObservableCollection<IOStatusEntity> GetWagonBasketOutPut()  //5 ok
        {
            ObservableCollection<IOStatusEntity> _WagonInputs = new ObservableCollection<IOStatusEntity>();
            try
            {

                //Get W1Inputs
                string[] Wagon3Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("W6BasketOutPut");
                //Get W2Inputs
                string[] Wagon4Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("W7BasketOutPut");
               // Get W3Inputs
                string[] Wagon6Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("W8BasketOutPut");
                ////Get W4Inputs
                //string[] Wagon4Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("W4Inputs");
                ////Get W5Inputs
                //string[] Wagon5Inputs = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("W5Inputs");
                //ServiceResponse<IList> _result = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("WagonInput");
                int index = 0;
                if (_WagonBarrelOutPutParameter.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_WagonBarrelOutPutParameter.Response);
                    foreach (var item in lstIOStatus)
                    {
                        IOStatusEntity _IOStatusEntity = new IOStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == Wagon3Inputs.Length && lstIOStatus.Count == Wagon4Inputs.Length && lstIOStatus.Count == Wagon6Inputs.Length)
                            {
                                _IOStatusEntity.W6 = ValueFromArray(Wagon3Inputs, index);
                                _IOStatusEntity.W7 = ValueFromArray(Wagon4Inputs, index);
                                _IOStatusEntity.W8 = ValueFromArray(Wagon6Inputs, index);
                                //_IOStatusEntity.W4 = ValueFromArray(Wagon2Inputs, index);
                                //_IOStatusEntity.W5 = ValueFromArray(Wagon2Inputs, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagonBasketOutPut() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _WagonInputs.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _WagonInputs;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetWagonBasketOutPut()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _WagonInputs;
            }
        }
        public static ObservableCollection<IOStatusEntity> GetSystemOutPut() //3 Load Ready Indication address is not there in icd
        {
            ObservableCollection<IOStatusEntity> _SystemOutPut = new ObservableCollection<IOStatusEntity>();
            try
            {
                //string[] SystemOutPut = new string[] { "1", "1", "1", "1"  };

                string[] SystemOutPut = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("SystemOutput");

                int index = 0;
                if (_SystemOutputParameterList.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_SystemOutputParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        IOStatusEntity _IOStatusEntity = new IOStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == SystemOutPut.Length)
                            {
                                _IOStatusEntity.Value = ValueFromArray(SystemOutPut, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetSystemOutPut() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _SystemOutPut.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _SystemOutPut;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetSystemOutPut()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _SystemOutPut;
            }
        }
        public static ObservableCollection<IOStatusEntity> GetSystemInPut()
        {
            ObservableCollection<IOStatusEntity> _SystemInPut = new ObservableCollection<IOStatusEntity>();
            try
            {
                //string[] SystemInPut = new string[] { "1", "1", "1", "1", "1", "1", "1" };

                string[] SystemInPut = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("SystemInputs");

                int index = 0;
                if (_SystemInputParameterList.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_SystemInputParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        IOStatusEntity _IOStatusEntity = new IOStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetSystemInPut() lstIOStatus lstIOStatus.Count "+ lstIOStatus.Count + " SystemInPut.Length"+ SystemInPut.Length, DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                            if (lstIOStatus.Count == SystemInPut.Length)
                            {
                                _IOStatusEntity.Value = ValueFromArray(SystemInPut, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetSystemInPut() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _SystemInPut.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _SystemInPut;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetSystemInPut()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _SystemInPut;
            }
        } //plc addresses pending
        public static ObservableCollection<IOStatusEntity> GetCTOutPut()//3 CT in ICD but only 1 in use
        {
            ObservableCollection<IOStatusEntity> _CTOutPut = new ObservableCollection<IOStatusEntity>();
            try
            {
                //Get CTOutPut
                //string[] CTOutPut = new string[] { "1", "1", "1", "1" };


                string[] CTOutPut = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("CrossTrolleyOutput");




                int index = 0;
                if (_CTOutputParameterList.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_CTOutputParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        IOStatusEntity _IOStatusEntity = new IOStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == CTOutPut.Length)
                            {

                                _IOStatusEntity.CT1 = ValueFromArray(CTOutPut, index);
                            }
                        }
                        catch (Exception ex) 
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetCTOutPut() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _CTOutPut.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _CTOutPut;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetCTOutPut()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _CTOutPut;
            }
        }
        public static ObservableCollection<IOStatusEntity> GetCTInPut() //emergency input address is pending
        {
            ObservableCollection<IOStatusEntity> _CTInPut = new ObservableCollection<IOStatusEntity>();
            try
            {
                ////Get CTInPut
                //string[] CT1InPut = new string[] { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1"  };

                string[] CT1InPut = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("CrossTrolleyL1toL2Trolley");



                int index = 0;
                if (_CTInputParameterList.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_CTInputParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        IOStatusEntity _IOStatusEntity = new IOStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == CT1InPut.Length)
                            {
                                _IOStatusEntity.CT1 = ValueFromArray(CT1InPut, index);                                                           
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetCTInPut() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _CTInPut.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _CTInPut;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetCTInPut()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _CTInPut;
            }
        }
        public static ObservableCollection<IOStatusEntity> GetDryerOutPut() //ok   
        {
            ObservableCollection<IOStatusEntity> _DryerOutPut = new ObservableCollection<IOStatusEntity>();
            try
            {
                //string[] Dryer1OutPut = new string[] { "1", "1", "1", "1" };
                //string[] Dryer2OutPut = new string[] { "1", "1", "1", "1" };
                //string[] Dryer3OutPut = new string[] { "1", "1", "1", "1" };
                //string[] Dryer4OutPut = new string[] { "1", "1", "1", "1" };


                string[] Dryer1OutPut = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Dryer1Output");
                string[] Dryer2OutPut = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Dryer2Output");
                string[] Dryer3OutPut = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Dryer3Output");
                string[] Dryer4OutPut = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Dryer4Output");

                //ServiceResponse<IList> _result = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("DryerOutPut");
                int index = 0;
                if (_DryerOutputParameterList.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_DryerOutputParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        IOStatusEntity _IOStatusEntity = new IOStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == Dryer1OutPut.Length)
                            {
                                _IOStatusEntity.D1 = ValueFromArray(Dryer1OutPut, index);
                                _IOStatusEntity.D2 = ValueFromArray(Dryer2OutPut, index);
                                _IOStatusEntity.D3 = ValueFromArray(Dryer3OutPut, index);

                                //Dryer Blower On is not present for dryer4/dehydrator
                                if (index == 2)
                                {
                                     
                                }
                                else
                                {
                                    _IOStatusEntity.D4 = ValueFromArray(Dryer4OutPut, index);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetDryerOutPut() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _DryerOutPut.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _DryerOutPut;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetDryerOutPut()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _DryerOutPut;
            }
        }
        public static ObservableCollection<IOStatusEntity> GetDryerInut() //emergency input address pending
        {
            ObservableCollection<IOStatusEntity> _DryerInPut = new ObservableCollection<IOStatusEntity>();
            try
            {
                //string[] Dryer1InPut = new string[] { "1", "1", "1", "1", "1", "1", "1", "1" };
                //string[] Dryer2InPut = new string[] { "1", "1", "1", "1", "1", "1", "1", "1" };
                //string[] Dryer3InPut = new string[] { "1", "1", "1", "1", "1", "1", "1", "1" };
                //string[] Dryer4InPut = new string[] { "1", "1", "1", "1", "1", "1", "1", "1" };

                //////Get CTInPut
                string[] Dryer1InPut = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Dryer1Input");//8
                string[] Dryer2InPut = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Dryer2Input");//8
                string[] Dryer3InPut = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Dryer3Input");//8
                string[] Dryer4InPut = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Dryer4Input");//7

                 
                int index = 0;
                if (_DryerInputParameterList.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_DryerInputParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        IOStatusEntity _IOStatusEntity = new IOStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == Dryer1InPut.Length)
                            {
                                _IOStatusEntity.D1 = ValueFromArray(Dryer1InPut, index);
                                _IOStatusEntity.D2 = ValueFromArray(Dryer2InPut, index);
                                _IOStatusEntity.D3 = ValueFromArray(Dryer3InPut, index);

                                // Blower C/B Healthy is not present for dryer 3
                                if (index==7)
                                {
                                     
                                }
                                else
                                {
                                    _IOStatusEntity.D4 = ValueFromArray(Dryer4InPut, index);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetDryerInut() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _DryerInPut.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _DryerInPut;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetDryerInut()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _DryerInPut;
            }
        }

        //public static ObservableCollection<IOStatusEntity> GetTripStatusInputs()
        //{
        //    ObservableCollection<IOStatusEntity> _TripInPut = new ObservableCollection<IOStatusEntity>();
        //    try
        //    {
        //        //Get CTInPut
        //        string[] TripStatusInPut = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("TripStatus");
        //        //ServiceResponse<IList> _result = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("DryerInPut");
        //        //int index = 0;
        //        //if (_TripStatusInputParameterList.Response != null)
        //        //{
        //        //    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_TripStatusInputParameterList.Response);
        //        //    foreach (var item in lstIOStatus)
        //        //    {
        //        //        IOStatusEntity _IOStatusEntity = new IOStatusEntity();
        //        //        _IOStatusEntity.ParameterName = item.ParameterName;
        //        //        try
        //        //        {
        //        //            if (lstIOStatus.Count == TripStatusInPut.Length)
        //        //            {
        //        //                _IOStatusEntity.Value = ValueFromArray(TripStatusInPut, index);
        //        //            }
        //        //        }
        //        //        catch { }
        //        //        _TripInPut.Add(_IOStatusEntity);
        //        //        index = index + 1;
        //        //    }
        //        //}
        //        return _TripInPut;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.LogError.ErrorLog("IOStatusLogic GetTripStatusInut()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
        //        return _TripInPut;
        //    }
        //}

        public static ObservableCollection<IOStatusEntity> GetLoaderInput() //ok
        {
            ObservableCollection<IOStatusEntity> _BarrelMotorOnOff = new ObservableCollection<IOStatusEntity>();
            try
            {
                //string[] BarrelMotorOnOff = { "0", "0", "1", "0", "1", "0", "0", "1", "0", "1", "0", "0", "1", "0", "1", "1" };

                string[] BarrelMotorOnOff = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("loaderInput");

                int index = 0;
                if (_LoaderParameterList.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_LoaderParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        IOStatusEntity _IOStatusEntity = new IOStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == BarrelMotorOnOff.Length)
                            {
                                _IOStatusEntity.Value = ValueFromArray(BarrelMotorOnOff, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetLoaderInput() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _BarrelMotorOnOff.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _BarrelMotorOnOff;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetLoaderInput()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _BarrelMotorOnOff;
            }
        }
        public static ObservableCollection<IOStatusEntity> GetLoaderOutput() //ok
        {
            ObservableCollection<IOStatusEntity> _BarrelMotorOnOff = new ObservableCollection<IOStatusEntity>();
            try
            {
                //string[] BarrelMotorOnOff = { "0", "0", "1", "0", "1", "0", "0"};

                string[] BarrelMotorOnOff = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("LoaderOutput");

                int index = 0;
                if (_LoaderOutputParameterList.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_LoaderOutputParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        IOStatusEntity _IOStatusEntity = new IOStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == BarrelMotorOnOff.Length)
                            {
                                _IOStatusEntity.Value = ValueFromArray(BarrelMotorOnOff, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetLoaderOutput() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _BarrelMotorOnOff.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _BarrelMotorOnOff;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetLoaderOutput()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _BarrelMotorOnOff;
            }
        }
        
        //ok
        public static ObservableCollection<IOStatusEntity> GetBarrelMotorStatus()
        {
            ObservableCollection<IOStatusEntity> _BarrelMotorStatus = new ObservableCollection<IOStatusEntity>();
            try
            {
                //string[] BarrelMotorStatus = { "0", "0", "1", "0", "1", "0", "0", "1", "0", "1", "0", "0", "1", "0", "1", "0", "0", "1", "0", "1", "0", "0", "1", "0", "1", "0", "0", "1", "0", "1", "0", "0", "1", "0", "0", "0", "1", "0" };

                string[] BarrelMotorStatus = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("BarrelMotorOutput");

                int index = 0;
                if (_BarrelMotorOnOffParameterList.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_BarrelMotorOnOffParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        IOStatusEntity _IOStatusEntity = new IOStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == BarrelMotorStatus.Length)
                            {
                                _IOStatusEntity.Value = ValueFromArray(BarrelMotorStatus, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetBarrelMotorStatus() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _BarrelMotorStatus.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _BarrelMotorStatus;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetBarrelMotorStatus()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _BarrelMotorStatus;
            }
        }

        public static ObservableCollection<IOStatusEntity> GetUnloaderInputStatusInputs()
        {
            ObservableCollection<IOStatusEntity> _UnloadInPut = new ObservableCollection<IOStatusEntity>();
            try
            {
                //string[] UNLOADERInputInPut = { "0", "0", "1", "0", "1", "0", "0", "1", "0", "1" };


                string[] UNLOADERInputInPut = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("UnloaderInput");

                int index = 0;
                if (_UnloaderInputParameterList.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_UnloaderInputParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        IOStatusEntity _IOStatusEntity = new IOStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == UNLOADERInputInPut.Length)
                            {
                                _IOStatusEntity.Value = ValueFromArray(UNLOADERInputInPut, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetUnloaderInputStatusInputs() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _UnloadInPut.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _UnloadInPut;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetUnloaderInputStatusInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _UnloadInPut;
            }
        }


        public static ObservableCollection<IOStatusEntity> GetUnloaderOutPutStatusInputs()
        {
            ObservableCollection<IOStatusEntity> _UnloadInPut = new ObservableCollection<IOStatusEntity>();
            try
            {
                //string[] UNLOADERInputInPut = { "0", "0", "1", "0", "0", "1", "0" };

                string[] UNLOADERInputInPut = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("UnloaderOutput");

                int index = 0;
                if (_UnloaderOutPutParameterList.Response != null)
                {
                    IList<IOStatusEntity> lstIOStatus = (IList<IOStatusEntity>)(_UnloaderOutPutParameterList.Response);
                    foreach (var item in lstIOStatus)
                    {
                        IOStatusEntity _IOStatusEntity = new IOStatusEntity();
                        _IOStatusEntity.ParameterName = item.ParameterName;
                        try
                        {
                            if (lstIOStatus.Count == UNLOADERInputInPut.Length)
                            {
                                _IOStatusEntity.Value = ValueFromArray(UNLOADERInputInPut, index);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("IOStatusLogic GetUnloaderOutPutStatusInputs() lstIOStatus", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _UnloadInPut.Add(_IOStatusEntity);
                        index = index + 1;
                    }
                }
                return _UnloadInPut;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("IOStatusLogic GetUnloaderOutPutStatusInputs()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _UnloadInPut;
            }
        }
         
        #endregion
    }
}
