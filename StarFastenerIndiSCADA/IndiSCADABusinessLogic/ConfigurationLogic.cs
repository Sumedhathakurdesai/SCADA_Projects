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
    public static class ConfigurationLogic
    {
        #region chemical name setting
        public static ServiceResponse<DataTable> SelectChemicalMasterData()
        {
            ServiceResponse<DataTable> result = new ServiceResponse<DataTable>();
            try
            {
                result = IndiSCADADataAccess.DataAccessSelect.SelectHagerTypeMasterData("Select * from ChemicalMaster");
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("PartMasterLogic getHangerDetails()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }
        public static ServiceResponse<int> AddNewChemicalName(ChemicalNameMasterEntity ChemicalEntity)
        {

            ServiceResponse<int> _result = new ServiceResponse<int>();
            ServiceResponse<DataTable> ResultShiftData = IndiSCADADataAccess.DataAccessSelect.getChemicalNameData(ChemicalEntity);

            ServiceResponse<DataTable> ResultTotalChemical = IndiSCADADataAccess.DataAccessSelect.getTotalPercentageEnteredForChemical(ChemicalEntity);

            try
            {

                if (ResultTotalChemical.Status == ResponseType.S)
                {
                    DataTable dtTotal = ResultTotalChemical.Response;
                    if (dtTotal.Rows.Count > 0)
                    {
                        Single totalpercent = 0;
                        try
                        {
                            totalpercent = Convert.ToSingle(dtTotal.Rows[0]["TotalChemical"].ToString()) + Convert.ToSingle(ChemicalEntity.ChemicalPercentage);
                        }
                        catch { }

                        if (totalpercent <= 100)
                        {
                            if (ResultShiftData.Status == ResponseType.S)
                            {
                                DataTable dtShiftData = ResultShiftData.Response;
                                if (dtShiftData != null)
                                {
                                    if (dtShiftData.Rows.Count == 0)
                                    {
                                        try
                                        {
                                            _result = IndiSCADADataAccess.DataAccessInsert.InsertChemicalNameMasterData(ChemicalEntity);
                                        }
                                        catch (Exception ex)
                                        {
                                            ErrorLogger.LogError.ErrorLog("ConfigurationLogic AddNewChemicalName ()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            _result.Response = 2;
                        }
                    }

                }
            }
            catch { }
            return _result;
        }
        public static ServiceResponse<int> DeleteSelectedChemical(ChemicalNameMasterEntity ChemicalEntity)
        {
            ServiceResponse<int> _result = new ServiceResponse<int>();
            try
            {
                _result = IndiSCADADataAccess.DataAccessDelete.DeleteChemicalNameMasterData(ChemicalEntity);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationLogic DeleteSelectedChemical ()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }

        public static ObservableCollection<ChemicalNameMasterEntity> GetChemicalNameMasterData()
        {
            ObservableCollection<ChemicalNameMasterEntity> _result = new ObservableCollection<ChemicalNameMasterEntity>();
            try
            {
                ServiceResponse<IList> _TranslatoionResult = new ServiceResponse<IList>();
                _TranslatoionResult = IndiSCADATranslation.NextLoadMasterSettingTranslation.getChemicalNameMasterData();
                IList<ChemicalNameMasterEntity> lstShiftData = (IList<ChemicalNameMasterEntity>)(_TranslatoionResult.Response);
                if (_TranslatoionResult.Status == ResponseType.S)
                {
                    if (_TranslatoionResult.Response != null)
                    {
                        try
                        {
                            ObservableCollection<ChemicalNameMasterEntity> ChemicalDataColl = new ObservableCollection<ChemicalNameMasterEntity>(lstShiftData);
                            for (int i = 0; i < ChemicalDataColl.Count; i++)
                            {
                                ChemicalNameMasterEntity chmEntity = new ChemicalNameMasterEntity();
                                chmEntity = ChemicalDataColl[i];
                                ServiceResponse<DataTable> ResultTotalChemical = IndiSCADADataAccess.DataAccessSelect.getTotalPercentageEnteredForChemical(chmEntity);
                                DataTable dtTotal = ResultTotalChemical.Response;
                                if (dtTotal.Rows.Count > 0)
                                {
                                    Single totalpercent = Convert.ToSingle(dtTotal.Rows[0]["TotalChemical"].ToString());
                                    if (totalpercent < 100)
                                    {
                                        ChemicalDataColl[i].isChemicalPercentComplete = false;
                                    }
                                    else if (totalpercent == 100)
                                    {
                                        ChemicalDataColl[i].isChemicalPercentComplete = true;
                                    }
                                }
                            }
                            _result = ChemicalDataColl;
                        }
                        catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NextLoadMasterSettingsLogic SelectNextLoadSettingData()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }

        public static ObservableCollection<string> GetRemainingPumpData()
        {
            ObservableCollection<string> _result = new ObservableCollection<string>();
            try
            {
                ServiceResponse<DataTable> ResultPumpData = IndiSCADADataAccess.DataAccessSelect.getRemainingPumpData();
                DataTable dtPumpdata = ResultPumpData.Response;

                if (dtPumpdata != null)
                {
                    if (dtPumpdata.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtPumpdata.Rows)
                        {
                            _result.Add(dr["PumpName"].ToString());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NextloadMasterSettingLogic GetAllTagListFromXML()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }
        public static ServiceResponse<int> UpdateSelectedChemical(ChemicalNameMasterEntity ChemicalEntity)
        {
            ServiceResponse<int> _result = new ServiceResponse<int>();
            try
            {
                _result = IndiSCADADataAccess.DataAccessUpdate.UpdateChemicalNameMasterData(ChemicalEntity);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationLogic UpdateSelectedChemical ()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }

        #endregion


        public static ObservableCollection<WagonLoadStringEntity> GetWagonDetails()
        {
            ObservableCollection<WagonLoadStringEntity> _WagonDetails = new ObservableCollection<WagonLoadStringEntity>();
            try
            {
                // ServiceResponse<DataTable> _WagonList = IndiSCADADataAccess.DataAccessSelect.ReturnDataTable("StationMasterTankDetails", "SP_StationMaster");

                //string[] Wagon1LoadString = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("LoadTypeatStationArrayLoadType"); //??addresss plc 
                // Duration               
                _WagonDetails = DeviceCommunication.CommunicationWithPLC.WagonDetails;                
                
                return (_WagonDetails);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationLogic GetWagonDetails() Error in foreach loop ", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return null;
            }
        }

        public static ServiceResponse<int> CreateDatabase(String DatabaseName, String ServerName)
        {
            ServiceResponse<int> _result = new ServiceResponse<int>();
            try
            {
                string CreateDatabaseQuery = "CREATE DATABASE " + DatabaseName;

                _result = IndiSCADADataAccess.DataAccessCreate.CreateNewDatabase(ServerName, DatabaseName, CreateDatabaseQuery); 
                return _result;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationLogic CreateDatabase()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }

        public static ServiceResponse<int> ClearShiftData()
        {
            ServiceResponse<int> _result = new ServiceResponse<int>();
            try
            {
                _result = IndiSCADADataAccess.DataAccessDelete.DeleteAllShiftMasterData();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationLogic AddNewShiftTiming ()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }
        public static ObservableCollection<ShiftMasterEntity> SelectShiftMasterData()
        {
            ObservableCollection<ShiftMasterEntity> _result = new ObservableCollection<ShiftMasterEntity>();
            try
            {
                ServiceResponse<IList> _TranslatoionResult = new ServiceResponse<IList>();
                _TranslatoionResult = IndiSCADATranslation.NextLoadMasterSettingTranslation.getShiftMasterData();
                IList<ShiftMasterEntity> lstShiftData = (IList<ShiftMasterEntity>)(_TranslatoionResult.Response);
                if (_TranslatoionResult.Status == ResponseType.S)
                {
                    if (_TranslatoionResult.Response != null)
                    {
                        _result = new ObservableCollection<ShiftMasterEntity>(lstShiftData);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NextLoadMasterSettingsLogic SelectNextLoadSettingData()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }

        public static ServiceResponse<int> AddNewShiftTiming(ShiftMasterEntity shiftentity)
        {
            ServiceResponse<int> _result = new ServiceResponse<int>();
            ServiceResponse<DataTable> ResultShiftData = IndiSCADADataAccess.DataAccessSelect.getShiftMasterData(shiftentity.ShiftNumber);

            if (ResultShiftData.Status == ResponseType.S)
            {
                DataTable dtShiftData = ResultShiftData.Response;
                if (dtShiftData != null)
                {
                    if (dtShiftData.Rows.Count == 0)
                    {
                        try
                        {
                            _result = IndiSCADADataAccess.DataAccessInsert.InsertShiftMasterData(shiftentity);
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("ConfigurationLogic AddNewShiftTiming ()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }

                    }
                    else
                    {
                        try
                        {
                            // ServiceResponse<int> res = IndiSCADADataAccess.DataAccessDelete.DeleteShiftMasterData(shiftentity.ShiftNumber);

                            _result = IndiSCADADataAccess.DataAccessInsert.InsertShiftMasterData(shiftentity);
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("ConfigurationLogic AddNewShiftTiming ()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }

                    }

                }
            }
            return _result;
        }

        public static int CreateTables(String DatabaseName, String ServerName)
        { 
            try
            {
                String CreateTable = ""; int _result=0;

                CreateTable = "CREATE TABLE [dbo].[AlarmData](    [AlarmID][float] NULL,    [AlarmDateTime] [datetime] NULL,	[AlarmName]        [nvarchar]        (max) NULL,    [AlarmText] [nvarchar]        (max) NULL,    [AlarmCondition] [nvarchar]        (max) NULL,    [AlarmDuration] [nvarchar]        (max) NULL,    [isOFF] [bit] NULL,	[isACK] [bit] NULL,	[CausesDownTime] [bit] NULL,	[AlarmGroup]        [nvarchar]        (max) NULL) ON[PRIMARY]";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateTables(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE TABLE [dbo].[AlarmMaster](	[AlarmName] [varchar](100) NULL,	[AlarmText] [varchar](100) NULL,	[AlarmHelp] [varchar](3000) NULL,	[isACK] [bit] NULL,	[isON] [bit] NULL,	[isOFF] [bit] NULL,	[AlarmPriority] [varchar](50) NULL,	[CausesDownTime] [bit] NULL,	[AlarmSerialNumber] [int] NULL,	[AlarmGroup] [varchar](50) NULL,	[AlarmAddress] [varchar](50) NULL,	[AlarmOnCondition] [varchar](50) NULL,	[AlarmOFFCondition] [varchar](50) NULL) ON [PRIMARY]";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateTables(ServerName, DatabaseName, CreateTable);
                 
                CreateTable = "CREATE TABLE [dbo].[ChemicalMaster](	[ID] [int] NULL,	[ChemicalName] [varchar](3000) NULL,	[PumpNo] [varchar](20) NULL,	[PumpName] [varchar](200) NULL,	[ModeOfOperation] [varchar](50) NULL,	[SetTime] [varchar](50) NULL,	[SetQuantity] [varchar](50) NULL,	[SetFlowRate] [varchar](50) NULL,	[DosingOP] [varchar](50) NULL,	[OnOffCondition] [bit] NULL) ON [PRIMARY]";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateTables(ServerName, DatabaseName, CreateTable);
                 
                CreateTable = "CREATE TABLE [dbo].[DosingOperationData](	[DosingDateTime] [datetime] NULL,	[ChemicalName] [nvarchar](max) NULL,	[ModeOfOperation] [nvarchar](max) NULL,	[SetQuantity] [float] NULL,	[SetFlowRate] [float] NULL,	[SetTime] [float] NULL,	[PumpNo] [nvarchar](max) NULL) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateTables(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE TABLE [dbo].[Event_Log](	[StartDate] [datetime] NULL,	[EndDate] [datetime] NULL,	[Description] [nvarchar](max) NULL,	[GroupName] [nvarchar](max) NULL,	[isComplete] [bit] NULL,	[EventText] [nvarchar](max) NULL) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateTables(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE TABLE [dbo].[EventMaster](	[EventName] [nvarchar](50) NULL,	[EventText] [nvarchar](225) NULL,	[EventAddress] [nvarchar](225) NULL,	[EventGroup] [nvarchar](225) NULL,	[EventOnCondition] [nvarchar](50) NULL,	[EventSerialNo] [int] NULL,	[EventOFFCondition] [nvarchar](50) NULL,	[WagonOperation] [nvarchar](50) NULL,	[StationNo] [nvarchar](50) NULL) ON [PRIMARY]";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateTables(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE TABLE [dbo].[LoadData](	[LoadNumber] [nvarchar](max) NULL,	[LoadInTime] [datetime] NULL,	[LoadOutTime] [datetime] NULL,	[Operator] [nvarchar](max) NULL,	[Status] [bit] NULL,	[isStart] [bit] NULL,	[isEnd] [bit] NULL,	[LoadType] [float] NULL,	[LoadInShift] [float] NULL,	[LoadOutShift] [float] NULL,	[isStationExceedTime] [bit] NULL,	[CycleTime] [int] NULL,	[isStationOutOfRange] [bit] NULL,	[TotalWeight] [float] NULL) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateTables(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE TABLE [dbo].[LoadDipTime](	[ID] [float] NULL,	[LoadNumber] [nvarchar](max) NULL,	[LoadType] [nvarchar](max) NULL,	[StationNo] [float] NULL,	[DipInTime] [datetime] NULL,	[DipOutTime] [datetime] NULL,	[DipInShift] [nvarchar](max) NULL,	[DipOutShift] [nvarchar](max) NULL,	[DipTimeSetting] [nvarchar](max) NULL,	[Status] [bit] NULL,	[isStationDipTimeExceed] [bit] NULL,	[PartNumber] [nvarchar](max) NULL,	[SetCurrent] [float] NULL,	[TemperatureSetLow] [float] NULL,	[TemperatureSetHigh] [float] NULL,	[TemperatureActual] [float] NULL,	[Actual_Voltage] [float] NULL,	[Actual_Current] [float] NULL,	[Actual_pH] [float] NULL,	[AvgCurrent] [float] NULL,	[isStationExceedTime] [bit] NULL,	[ORTempLowSP] [float] NULL,	[ORTempHighSP] [float] NULL,	[ORTempAvg] [float] NULL,	[ORTempLowTime] [float] NULL,	[ORTempHighTime] [float] NULL,	[ORpHLowSP] [float] NULL,	[ORpHHighSP] [float] NULL,	[ORpHLowTime] [float] NULL,	[ORpHHighTime] [float] NULL,	[ORpHAvg] [float] NULL,	[ORDipTime] [float] NULL,	[isStationTimeOutOfRange] [bit] NULL,	[isStationTempOutOfRange] [bit] NULL,	[isStationCurrentOutOfRange] [bit] NULL,	[isStationpHOutOfRange] [bit] NULL) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateTables(ServerName, DatabaseName, CreateTable);

                //CreateTable = "CREATE TABLE [dbo].[LoadPartDetails](	[PartNumber] [varchar](50) NULL,	[PartName] [varchar](50) NULL,	[ClientName] [varchar](50) NULL,	[Description] [varchar](50) NULL,	[WeightPerPart] [float] NULL,	[AreaPerPart] [float] NULL,	[TotalWeight] [float] NULL,	[Anodic1CurrentDensity] [float] NULL,	[Anodic1Current] [float] NULL,	[Anodic2CurrentDensity] [float] NULL,	[Anodic2Current] [float] NULL,	[AlkalineZincCurrentDensity] [float] NULL,	[AlkalineZincCurrent] [float] NULL,	[PassivationSelection] [int] NULL,	[BarrelNo] [varchar](50) NULL,	[LoadNumber] [text] NULL,	[DateTimeCol] [datetime] NULL) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
                CreateTable = "CREATE TABLE [dbo].[LoadPartDetails](	[PartNumber] [varchar](50) NULL,	[PartName] [varchar](50) NULL,	[CustomerName] [varchar](50) NULL,	[Description] [varchar](50) NULL,	[WeightPerPart] [float] NULL,	[SurfaceAreaPerPart] [float] NULL,	[TotalWeight] [float] NULL,	[Quantity] [float] NULL,	[Anodic1CD] [float] NULL,	[Anodic1CDmm2] [float] NULL,	[Anodic1Current] [float] NULL,	[Anodic2CD] [float] NULL,	[Anodic2CDmm2] [float] NULL,	[Anodic2Current] [float] NULL,	[AlkalineZincCD] [float] NULL,	[AlkalineZincCDmm2] [float] NULL,	[AlkalineZincCurrent] [float] NULL,	[isSelectedForNextLoad] [bit] NULL,	[PassivationSelection] [int] NULL,	[LoadNumber] [text] NULL,	[DateTimeCol] [datetime] NULL) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateTables(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE TABLE [dbo].[NextLoadMasterSettings](	[ParameterName] [nvarchar](max) NULL,	[ColumnName] [nvarchar](max) NULL,	[DataType] [nvarchar](max) NULL,	[Unit] [nvarchar](max) NULL,	[MinValue] [float] NULL,	[MaxValue] [float] NULL,	[isPrimaryKey] [bit] NULL,	[TableName] [nvarchar](max) NULL,	[isDownloadToPLC] [bit] NULL,	[TaskName] [nvarchar](max) NULL,	[isCalculationRequired] [bit] NULL,	[IsReadOnlyForNextLoad] [bit] NULL,	[IsInLoadData] [bit] NULL,	[IsInReport] [bit] NULL,	[CalculationFormula] [nvarchar](max) NULL) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateTables(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE TABLE [dbo].[ORDipTime](	[ProgramNo] [int] NULL,	[StationNo] [int] NULL,	[StationName] [varchar](50) NULL,	[DipTimeToleranceHigh] [numeric](18, 0) NULL,	[DipTimeToleranceLow] [numeric](18, 0) NULL,	[DipLowBypass] [bit] NULL,	[DipHighBypass] [bit] NULL) ON [PRIMARY]";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateTables(ServerName, DatabaseName, CreateTable);

                //CreateTable = "CREATE TABLE [dbo].[PartMaster](	[PartNumber] [varchar](255) NULL,	[PartName] [varchar](255) NULL,	[ClientName] [varchar](255) NULL,	[Description] [varchar](255) NULL,	[WeightPerPart] [float] NULL,	[AreaPerPart] [float] NULL,	[TotalWeight] [float] NULL,	[Anodic1CurrentDensity] [float] NULL,	[Anodic1Current] [float] NULL,	[Anodic2CurrentDensity] [float] NULL,	[Anodic2Current] [float] NULL,	[AlkalineZincCurrentDensity] [float] NULL,	[AlkalineZincCurrent] [int] NULL,	[PassivationSelection] [nvarchar](255) NULL,	[isSelectedForNextLoad] [bit] NULL,	[BarrelNo] [varchar](50) NULL) ON [PRIMARY]";
                CreateTable = "CREATE TABLE [dbo].[PartMaster](	[PartNumber] [varchar](50) NULL,	[PartName] [varchar](50) NULL,	[CustomerName] [varchar](50) NULL,	[Description] [varchar](50) NULL,[WeightPerPart] [float] NULL,	[SurfaceAreaPerPart] [float] NULL,	[TotalWeight] [float] NULL,	[Quantity] [float] NULL,	[Anodic1CD] [float] NULL,	[Anodic1CDmm2] [float] NULL,	[Anodic1CDCurrent] [float] NULL,[Anodic2CD] [float] NULL,[Anodic2CDmm2] [float] NULL,	[Anodic2Current] [float] NULL,	[AlkalineZincCD] [float] NULL,	[AlkalineZincCDmm2] [float] NULL,	[AlkalineZincCurrent] [float] NULL,	[isSelectedForNextLoad] [bit] NULL,	[PassivationSelection] [int] NULL) ON [PRIMARY]";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateTables(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE TABLE [dbo].[ScreenParameter](	[ParameterName] [varchar](100) NULL,	[ParameterType] [varchar](100) NULL,	[ParameterID] [int] NULL) ON [PRIMARY]";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateTables(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE TABLE [dbo].[StationMaster](	[StationNumber] [int] NOT NULL,	[ShortStnName] [nchar](10) NULL,	[StationName] [varchar](50) NOT NULL,	[SequenceNumber] [int] NULL,	[StationDimension] [varchar](50) NULL,	[ConstructionMaterial] [varchar](50) NULL,	[OtherInfoOfStation] [varchar](50) NULL,	[StnDipTimeRange] [varchar](50) NULL,	[MaintainenceScheduleDate] [datetime] NULL,	[CurrentLoadNumber] [varchar](50) NOT NULL,	[PartNumber] [varchar](50) NOT NULL,	[TempLowSetValue] [varchar](20) NULL,	[TempHighSetValue] [varchar](20) NULL,	[ActualTemp] [varchar](20) NULL,	[TimeAboveSetTemp_SP] [varchar](20) NULL,	[TimeAboveSetTemp_PV] [varchar](20) NULL,	[SetCurrentValue] [varchar](20) NULL,	[ActualCurrent_Amp] [varchar](20) NULL,	[ActualVoltage_PV] [varchar](20) NULL,	[ActualAmpHr] [varchar](20) NULL,	[SetPointVoltage] [varchar](20) NULL,	[pHLowSetValue] [varchar](20) NULL,	[pHHighSetValue] [varchar](20) NULL,	[pHActualValue] [varchar](20) NULL,	[DosingPumpDetails] [varchar](50) NULL,	[LastDosingDateTime] [datetime] NULL,	[DosingQty] [varchar](20) NULL,	[DosingSetPoints] [varchar](20) NULL,	[DosingActual_AmpHr] [varchar](20) NULL,	[EnergyConsumedKW] [varchar](20) NULL,	[ActualTempMinLimit] [varchar](20) NULL,	[ActualTempMaxLimit] [varchar](20) NULL,	[SetTempMinLimit] [varchar](20) NULL,	[SetTempMaxLimit] [varchar](20) NULL,	[SetTemp] [varchar](20) NULL,	[isTempControllerPresent] [bit] NULL,	[ispHMeterPresent] [bit] NULL,	[isRectifierPresent] [bit] NULL,	[TempStnName] [varchar](50) NULL,	[TemperatureValueIndex] [numeric](18, 0) NULL,	[CurrentValueIndex] [numeric](18, 0) NULL,	[pHValueIndex] [numeric](18, 0) NULL,	[VoltageValueIndex] [numeric](18, 0) NULL) ON [PRIMARY]";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateTables(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE TABLE [dbo].[TrendCurrentData](	[DateTimeCol] [datetime] NULL,	[Anodic1SPCurrent] [float] NULL,	[Anodic1Current] [float] NULL,	[Anodic2SPCurrent] [float] NULL,	[Anodic2Current] [float] NULL,	[AlkalineZinc1SPCurrent] [float] NULL,	[AlkalineZinc1Current] [float] NULL,	[AlkalineZinc2SPCurrent] [float] NULL,	[AlkalineZinc2Current] [float] NULL,	[AlkalineZinc3SPCurrent] [float] NULL,	[AlkalineZinc3Current] [float] NULL,	[AlkalineZinc4SPCurrent] [float] NULL,	[AlkalineZinc4Current] [float] NULL,	[AlkalineZinc5SPCurrent] [float] NULL,	[AlkalineZinc5Current] [float] NULL,	[AlkalineZinc6SPCurrent] [float] NULL,	[AlkalineZinc6Current] [float] NULL,	[AlkalineZinc7SPCurrent] [float] NULL,	[AlkalineZinc7Current] [float] NULL,	[AlkalineZinc8SPCurrent] [float] NULL,	[AlkalineZinc8Current] [float] NULL,	[AlkalineZinc9SPCurrent] [float] NULL,	[AlkalineZinc9Current] [float] NULL,	[AlkalineZinc10SPCurrent] [float] NULL,	[AlkalineZinc10Current] [float] NULL,	[AlkalineZinc11SPCurrent] [float] NULL,[AlkalineZinc11Current] [float] NULL,	[AlkalineZinc12SPCurrent] [float] NULL,	[AlkalineZinc12Current] [float] NULL) ON [PRIMARY]";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateTables(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE TABLE [dbo].[TrendTempData](	[DateTimeCol] [datetime] NULL,	[SoakDegressingTemp] [float] NULL,	[Anodic1Temp] [float] NULL,	[Anodic2Temp] [float] NULL,	[AlkalineZinc1Temp] [float] NULL,	[AlkalineZinc2Temp] [float] NULL,	[AlkalineZinc3Temp] [float] NULL,	[AlkalineZinc4Temp] [float] NULL,	[AlkalineZinc5Temp] [float] NULL,	[AlkalineZinc6Temp] [float] NULL,	[Passivation1Temp] [float] NULL,	[Passivation2Temp] [float] NULL,	[TopCoatTemp] [nchar](10) NULL,	[DryerTemp] [nchar](10) NULL) ON [PRIMARY]";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateTables(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE TABLE [dbo].[UserActivityData](	[DateTimeCol] [datetime] NULL,	[UserName] [nvarchar](max) NULL,	[Activity] [nvarchar](max) NULL) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateTables(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE TABLE [dbo].[UserMaster](	[UserName] [nvarchar](max) NULL,	[UserPassword] [nvarchar](max) NULL,	[UserRole] [nvarchar](max) NULL,	[EmailID] [nvarchar](max) NULL,	[MobileNo] [nvarchar](max) NULL) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateTables(ServerName, DatabaseName, CreateTable);
                 

                return _result;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationLogic CreateTables()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return 0;
            }
        }

        public static int CreateProcedures(String DatabaseName, String ServerName)
        { 
            try
            {
                String CreateTable = "";  int _result = 0;
                CreateTable = "CREATE PROCEDURE [dbo].[EventsDataLog] \n 	@Selection varchar(20) = NULL, \n 	@StartDate datetime = NULL, \n 	@EndDate datetime= NULL, \n 	@Description varchar(50) = NULL, \n 	@GroupName varchar(50) = NULL,   \n 	@EventText varchar(100) = NULL, \n 	@isComplete bit = 0 \n AS \n BEGIN \n 	 \n 	if(@Selection='InsertData') \n 	begin \n 		INSERT INTO Event_Log(StartDate,EndDate,Description,GroupName,isComplete,EventText) VALUES(@StartDate,@EndDate,@Description,@GroupName,@isComplete,@EventText); \n 	end \n 	 \n 	if(@Selection='UpdateData') \n 	begin \n 		Update Event_Log set EndDate = @EndDate, isComplete = 1 where Description=@Description and isComplete = 0; \n 	end \n 	 \n 	if(@Selection='Select') \n 	begin \n 		Select * From Event_Log ; \n 	end \n   	if(@Selection='AlarmEventHistory') \n 	begin \n 		 \n 		SELECT AlarmDateTime,'AL' AS Type,b.AlarmGroup,a.AlarmText,b.AlarmPriority,AlarmCondition FROM AlarmData a,AlarmMaster b where a.AlarmName = b.AlarmText and AlarmDateTime between (@StartDate) and (@EndDate) Union Select StartDate,'EV',GroupName,Description,null,null from Event_Log where StartDate between(@StartDate) and (@EndDate) ; \n 	end \n  \n END \n ";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE PROCEDURE [dbo].[SelectScreenwiseNextLoadMasterSettingData]  \n 	@screenname nvarchar(50)=null \n AS \n BEGIN  \n 	SET NOCOUNT ON;  \n     if (@screenname = 'All') \n     Begin \n 		SELECT * from NextLoadMasterSettings; \n 	End \n 	if (@screenname = 'AllExceptNxtLd') \n     Begin \n 		SELECT * from NextLoadMasterSettings where ParameterName != 'isSelectedforNextLoad'; \n 	End \n 	else if (@screenname = 'DownloadToPLC') \n 				Begin \n 					SELECT * from NextLoadMasterSettings where isDownloadToPLC=1; \n 				End \n 		else \n 			Begin \n 				SELECT * from NextLoadMasterSettings where TableName=@screenname and ParameterName != 'isSelectedforNextLoad'; \n 			End  \n END \n ";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);

                CreateTable = "create PROCEDURE [dbo].[SP_ChemicalMaster]  \n 	@Selection varchar(20) = NULL, \n 	@ID nvarchar(200)= NULL, \n 	@OnOffCondition nvarchar(200) = NULL  \n  AS \n BEGIN \n 	if(@Selection='UpdateCondition') \n 	begin \n 	         Update ChemicalMaster set OnOffCondition=@OnOffCondition where ID=@ID; \n 	end  \n END  \n ";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);

                CreateTable = "create proc [dbo].[SP_LoadData] \n ( \n @Selection varchar(20) = NULL, \n @LoadNumber varchar(50), \n @LoadType varchar(50) = NULL, \n @LoadInTime datetime = NULL, \n @LoadOutTime datetime = NULL,@LoadInShift varchar(10) = NULL, \n @LoadOutShift varchar(10) = NULL, \n @CycleTime varchar(10)= NULL, \n @Operator varchar(30) = NULL, \n @Status bit = 0, \n @isStart bit = 0, \n @isEnd bit = 0, \n @LastCycleTime varchar(10)= NULL, \n @isStationExceedTime bit = 0, \n @ReturnValue varchar(30) = NULL OUTPUT \n ) \n as \n begin \n  		declare @pname varchar(30) = NULL \n 	if(@Selection='Insert') \n 	begin \n 		INSERT INTO LoadData(LoadNumber,LoadType,LoadInTime,LoadOutTime,LoadInShift,LoadOutShift,Operator,Status,isStart,isEnd,isStationExceedTime,CycleTime,isStationOutOfRange)VALUES \n 		(@LoadNumber,@LoadType,@LoadInTime,@LoadOutTime,@LoadInShift,@LoadOutShift,@Operator,@Status,@isStart,@isEnd,@isStationExceedTime,@LastCycleTime,0); \n 	end \n  \n 	if(@Selection='Update') \n 	begin \n 		Update LoadData set LoadOutTime = @LoadOutTime, LoadOutShift = @LoadOutShift, Status = 1, isEnd = 1, CycleTime=@CycleTime where LoadNumber=@LoadNumber and Status = 0; \n 	end \n 	 \n 	if(@Selection = 'IsPresentLoadNo') \n 	begin \n 		SELECT * From LoadData where LoadNumber=@LoadNumber; \n 	end \n 	 \n 	if(@Selection = 'UpdateStnOR') \n 	begin \n 		Update LoadData set  isStationOutOfRange = 1 where LoadNumber=@LoadNumber and isStationOutOfRange = 0 \n 	end \n end \n ";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE proc [dbo].[SP_LoadDipTime] \n @Selection varchar(100) = NULL, \n @LoadNumber varchar(50), \n @LoadType varchar(50) = NULL, \n @StationNo int, \n @DipInTime datetime = NULL, \n @DipOutTime datetime = NULL, \n @DipInShift varchar(10) = NULL, \n @DipOutShift varchar(10) = NULL, \n @Status bit = 0, \n @DipTimeSetting varchar(50) = NULL, \n @TemperatureSetLow float = NULL, \n @TemperatureSetHigh float = NULL, \n @TemperatureActual float = NULL, \n @isStationExceedTime bit = 0, \n @ActualCurrent float = NULL, \n @SetCurrent float = NULL, \n @ActualVoltage float = NULL, \n @AvgCurrent float = NULL, \n @ActualPH float = NULL, \n  \n @ORTempLowSP float = NULL, \n @ORTempHighSP float = NULL, \n @ORTempAvg float = NULL, \n @ORTempLowTime float = NULL, \n @ORTempHighTime float = NULL, \n  \n @ORpHLowSP float = NULL, \n @ORpHHighSP float = NULL, \n @ORpHLowTime float = NULL, \n @ORpHHighTime float = NULL, \n @ORpHAvg float = NULL, \n  \n @ORDipTime float = NULL, \n  \n @istempOR bit = NULL, \n @isphOR bit = NULL, \n @iscurrOR bit = NULL, \n @istimeOR bit = NULL \n  \n as \n begin \n  	if(@Selection='Insert') \n 	begin \n 		INSERT INTO LoadDipTime(LoadNumber,LoadType,StationNo,DipInTime,DipOutTime,DipInShift,DipOutShift,Status, DipTimeSetting) VALUES(@LoadNumber,@LoadType,@StationNo,@DipInTime,@DipOutTime,@DipInShift,@DipOutShift,@Status,@DipTimeSetting); \n 	end \n  \n 	if(@Selection='WithTemperature') \n 	begin \n 		Update LoadDipTime set DipOutTime=@DipOutTime, DipOutShift=@DipOutShift, Status = 1,ORDipTime=@ORDipTime, ORTempLowSP=@ORTempLowSP,ORTempHighSP=@ORTempHighSP,ORTempAvg=@ORTempAvg,ORTempLowTime=@ORTempLowTime,ORTempHighTime=@ORTempHighTime ,TemperatureSetLow=@TemperatureSetLow, TemperatureSetHigh=@TemperatureSetHigh, TemperatureActual=@TemperatureActual, isStationExceedTime=@isStationExceedTime where LoadNumber=@LoadNumber and Status = 0 and StationNo=@StationNo; \n 	end \n 	if(@Selection='WithRectifier') \n 	begin \n 		Update LoadDipTime set AvgCurrent=@AvgCurrent ,DipOutTime=@DipOutTime,ORDipTime=@ORDipTime, DipOutShift=@DipOutShift, Status = 1, isStationExceedTime=@isStationExceedTime,Actual_Current=@ActualCurrent,SetCurrent=@SetCurrent,Actual_Voltage=@ActualVoltage where LoadNumber=@LoadNumber and Status = 0 and StationNo=@StationNo; \n 	end \n 	if(@Selection='WithPH') \n 	begin \n 		Update LoadDipTime set DipOutTime=@DipOutTime, DipOutShift=@DipOutShift, Status = 1,ORDipTime=@ORDipTime,ORpHLowSP=ORpHLowSP,ORpHHighSP=@ORpHHighSP,ORpHLowTime=@ORpHLowTime,ORpHHighTime=@ORpHHighTime,ORpHAvg=@ORpHAvg,Actual_pH=@ActualPH,isStationExceedTime=@isStationExceedTime where LoadNumber=@LoadNumber and Status = 0 and StationNo=@StationNo; \n 	end \n 	if(@Selection='WithTemperatureRectifier') \n 	begin \n 		Update LoadDipTime set AvgCurrent=@AvgCurrent,DipOutTime=@DipOutTime,ORDipTime=@ORDipTime, ORTempLowSP=@ORTempLowSP,ORTempHighSP=@ORTempHighSP,ORTempAvg=@ORTempAvg,ORTempLowTime=@ORTempLowTime,ORTempHighTime=@ORTempHighTime, DipOutShift=@DipOutShift, Status = 1, TemperatureSetLow=@TemperatureSetLow, TemperatureSetHigh=@TemperatureSetHigh, TemperatureActual=@TemperatureActual, Actual_Current=@ActualCurrent,SetCurrent=@SetCurrent, Actual_Voltage=@ActualVoltage, isStationExceedTime=@isStationExceedTime where LoadNumber=@LoadNumber and Status = 0 and StationNo=@StationNo; \n 	end	 \n 	if(@Selection='WithTemperaturePh') \n 	begin \n 		Update LoadDipTime set DipOutTime=@DipOutTime, DipOutShift=@DipOutShift, Status = 1, ORDipTime=@ORDipTime, ORpHLowSP=ORpHLowSP,ORpHHighSP=@ORpHHighSP,ORpHLowTime=@ORpHLowTime,ORpHHighTime=@ORpHHighTime,ORpHAvg=@ORpHAvg,ORTempLowSP=@ORTempLowSP,ORTempHighSP=@ORTempHighSP,ORTempAvg=@ORTempAvg,ORTempLowTime=@ORTempLowTime,ORTempHighTime=@ORTempHighTime,TemperatureSetLow=@TemperatureSetLow, TemperatureSetHigh=@TemperatureSetHigh, TemperatureActual=@TemperatureActual,Actual_pH=@ActualPH,isStationExceedTime=@isStationExceedTime where LoadNumber=@LoadNumber and Status = 0 and StationNo=@StationNo; \n 	end \n 	if(@Selection='WithDipTime') \n 	begin \n 		Update LoadDipTime set DipOutTime=@DipOutTime, DipOutShift=@DipOutShift, Status = 1, ORDipTime=@ORDipTime,isStationExceedTime=@isStationExceedTime where LoadNumber=@LoadNumber and Status = 0 and StationNo=@StationNo; \n 	end \n 	 \n 	if(@Selection='updateORpara') \n 	begin \n 		Update LoadDipTime set isStationTempOutOfRange=@istempOR, isStationpHOutOfRange=@isphOR, isStationCurrentOutOfRange=@iscurrOR, isStationTimeOutOfRange=@istimeOR where LoadNumber=@LoadNumber and StationNo=@StationNo; \n 	end \n  \n 	if(@Selection = 'IsPresentLoadNo') \n 	begin \n 		SELECT * From LoadDipTime where LoadNumber=@LoadNumber and StationNo=@StationNo; \n 	end \n  \n end \n ";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE PROCEDURE [dbo].[sp_NextLoadMasterSettingData]   \n @ParameterName nvarchar(50)=null, \n @ColumnName nvarchar(50)=null, \n @DataType nvarchar(50)=null, \n @Unit nvarchar(50)=null, \n @MinVal int=0, \n @MaxVal int=0, \n @isPrimaryKey bit = 0, \n @TableName nvarchar(50)=null, \n @isDownloadToPLC bit=0, \n @TaskName nvarchar(50)=null, \n @isCalculationRequired bit=0, \n @IsReadOnlyForNextLoad bit=0, \n @IsInLoadData bit=0, \n @IsInReport bit=0, \n @status nvarchar(50)=null \n AS \n BEGIN  \n 	SET NOCOUNT ON;  \n 	 \n 	if (@status='INSERT') \n 	BEGIN \n 		Insert into NextLoadMasterSettings (ParameterName,ColumnName,DataType,Unit,MinValue,MaxValue,isPrimaryKey,TableName,IsDownloadToPLC,TaskName,IsCalculationRequired,IsReadOnlyForNextLoad,IsInLoadData,IsInReport) Values (@ParameterName,@ColumnName,@DataType,@Unit,@MinVal,@MaxVal,@isPrimaryKey,@TableName,@isDownloadToPLC,@TaskName,@isCalculationRequired,@IsReadOnlyForNextLoad,@IsInLoadData,@IsInReport) \n     END \n      \n     if (@status='SELECT') \n     BEGIN \n 		SELECT * from NextLoadMasterSettings; \n 	END \n 	 \n 	if (@status='DELETE') \n 	BEGIN \n 	DELETE FROM NextLoadMasterSettings WHERE ParameterName=@ParameterName \n     END \n      \n     if (@status='UPDATE') \n 	BEGIN \n 		update NextLoadMasterSettings set ColumnName=@ColumnName,DataType=@DataType,Unit=@Unit,MinValue=@MinVal,MaxValue=@MaxVal,isPrimaryKey=@isPrimaryKey,TableName=@TableName,IsDownloadToPLC=@isDownloadToPLC,TaskName=@TaskName,IsCalculationRequired=@isCalculationRequired,IsReadOnlyForNextLoad=@IsReadOnlyForNextLoad,IsInLoadData=@IsInLoadData,IsInReport=@IsInReport where ParameterName = @ParameterName; \n     END \n      \n     if (@status='DATALOG') \n     BEGIN \n 		SELECT * from NextLoadMasterSettings WHERE IsInReport = 1; \n 	END \n 	 \n END";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE proc [dbo].[SP_ORdipTime] \n ( \n @Selection varchar(50) = NULL, \n @ProgramNo varchar(20) = NULL, \n @StationName varchar(100)= NULL, \n @DipTimeToleranceHigh float = NULL, \n @DipTimeToleranceLow float = NULL, \n @DipLowBypass bit = NULL, \n @DipHighBypass bit = NULL, \n @StationNO varchar(20) = NULL, \n @ReturnValue varchar(100) = NULL OUTPUT \n ) \n as \n begin  \n 	if(@Selection='Insert') \n 	begin \n 		INSERT INTO ORDipTime(ProgramNo,StationName,DipTimeToleranceHigh,DipTimeToleranceLow,DipLowBypass,DipHighBypass,StationNo)VALUES \n 		(@ProgramNo,@StationName,@DipTimeToleranceHigh,@DipTimeToleranceLow,@DipLowBypass,@DipHighBypass,@StationNO); \n 	end \n  \n 	if(@Selection='Update') \n 	begin \n 		Update ORDipTime set DipTimeToleranceHigh = @DipTimeToleranceHigh, DipTimeToleranceLow = @DipTimeToleranceLow, DipLowBypass = @DipLowBypass, DipHighBypass=@DipHighBypass where ProgramNo=@ProgramNo and StationName = @StationName; \n 	end \n 	 \n 	if(@Selection = 'delete') \n 	begin \n 		delete From ORDipTime where ProgramNo=@ProgramNo; \n 	end \n 	 \n 	if(@Selection = 'select') \n 	begin \n 		Select * From ORDipTime  order by StationNo asc; \n 	end \n 	 \n     if(@Selection = 'selectByProgram') \n 	begin \n 		Select * From ORDipTime where ProgramNo=@ProgramNo order by StationNo asc; \n 	end \n  \n 		if(@Selection = 'selectDistinctProgram') \n 	begin \n 		SELECT DISTINCT ProgramNo FROM ORDipTime; \n 	end \n end \n ";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);

                //CreateTable = "CREATE PROCEDURE [dbo].[sp_PartMasterData]  \n @PartNumber nvarchar(50)=null, \n @status nvarchar(70)=null, \n @Description nvarchar(200)=null \n AS \n BEGIN  \n 	SET NOCOUNT ON;  \n 	 \n 		SET NOCOUNT ON;  \n 	 \n 	if (@status='SelectPart') \n 	BEGIN \n 		select * from PartMaster where PartNumber=@PartNumber; \n     END \n      \n     if (@status='SelectDescription') \n 	BEGIN \n 		select * from PartMaster where Description=@Description; \n     END \n      \n     if (@status='SelectAll') \n     BEGIN \n 		SELECT * from PartMaster; \n 	END \n  \n 	if (@status='SelectAllPart') \n     BEGIN \n 		SELECT PartNumber,PartName,ClientName,Description,WeightPerPart,AreaPerPart,Anodic1CurrentDensity,Anodic2CurrentDensity,AlkalineZincCurrentDensity from PartMaster; \n 	END \n  \n 	if (@status='DATALOG') \n     BEGIN \n 		SELECT * from PartMaster where isSelectedforNextLoad=1; \n 	END \n 	 \n END \n ";
                CreateTable = "CREATE PROCEDURE [dbo].[sp_PartMasterData]\n@PartNumber nvarchar(50)=null,\n@status nvarchar(70)=null,\n@Description nvarchar(200)=null\nAS\nBEGIN\n	SET NOCOUNT ON;\n		SET NOCOUNT ON;\n	\n	if (@status='SelectPart')\n	BEGIN\n		select * from PartMaster where PartNumber=@PartNumber;\n    END\n    \n    if (@status='SelectDescription')\n	BEGIN\n		select * from PartMaster where Description=@Description;\n    END\n    \n    if (@status='SelectAll')\n    BEGIN\n		SELECT * from PartMaster;\n	END\n\n	if (@status='SelectAllPart')\n    BEGIN\n		SELECT PartNumber,PartName,CustomerName,Description,WeightPerPart,SurfaceAreaPerPart,Anodic1CD,Anodic2CD,AlkalineZincCD,Anodic1CDmm2,Anodic2CDmm2,AlkalineZincCDmm2 from PartMaster;\n	END\n\n	if (@status='DATALOG')\n    BEGIN\n		SELECT * from PartMaster where isSelectedforNextLoad=1;\n	END\n	\nEND\n";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE PROCEDURE [dbo].[SP_SelectPartNameInLoadPartDetails] \n @Selection varchar(20) = NULL, \n @LoadNumber varchar(50) \n AS \n BEGIN  \n 	SET NOCOUNT ON; \n 	if(@Selection = 'SelectPart') \n 	begin \n 		SELECT * From [LoadPartDetails] where LoadNumber like @LoadNumber \n 	end \n END \n ";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE PROCEDURE [dbo].[SP_StationMaster]  \n 	@Selection varchar(40) = NULL \n AS \n BEGIN \n 		 \n 	if(@Selection='StationMasterTankDetails') \n 	begin \n 		Select * From StationMaster ; \n 	end \n END \n ";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE PROCEDURE [dbo].[sp_TableCreateUsingQuery]  \n @sqlQuery nvarchar(Max)=null \n AS \n BEGIN   \n 	SET NOCOUNT ON;   \n     Exec (@sqlQuery) \n 	END \n ";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE PROCEDURE [dbo].[SPAlarmData]  \n 	@Selection varchar(20) = NULL, \n 	@AlarmName nvarchar(250)= NULL, \n 	@AlarmText nvarchar(250) = NULL, \n 	@AlarmCondition nvarchar(20)= NULL, \n 	@AlarmGroup varchar(50) = NULL, \n 	@AlarmDuration varchar(50) = NULL, \n 	@AlarmPriority nvarchar(50) = NULL, \n 	@isACK bit=0, \n 	@isOFF bit=0, \n 	@CausesDownTime bit=0, \n 	@AlarmDateTime DateTime=null \n AS \n BEGIN \n 	 \n 	if(@Selection='InsertData') \n 	begin \n 		INSERT INTO AlarmData(AlarmDuration,AlarmName,AlarmText,AlarmDateTime,AlarmGroup,AlarmCondition) VALUES(@AlarmDuration,@AlarmName,@AlarmText,@AlarmDateTime,@AlarmGroup,@AlarmCondition); \n 	end \n 	 \n 	if(@Selection='Select') \n 	begin \n 		Select * From AlarmData ; \n 	end \n 	if(@Selection='SelectStartUpAlarm') \n 	begin \n 		Select TOP 1 * From AlarmData where AlarmName= @AlarmName and AlarmCondition='ON' ORDER BY AlarmDateTime DESC  ; \n 	end \n END \n ";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE PROCEDURE [dbo].[SPAlarmHelp] \n  	@AlarmName nvarchar(250)= NULL, \n 	@ReturnValuestring nvarchar(1000) out \n AS \n BEGIN \n 	 \n 	begin \n 		select @ReturnValuestring=AlarmHelp from AlarmMaster where AlarmName =@AlarmName; \n 			end \n END \n ";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE PROCEDURE [dbo].[SPAlarmMaster]  \n 	@Selection varchar(20) = NULL, \n 	@AlarmName nvarchar(150)= NULL, \n 	@AlarmText nvarchar(150) = NULL, \n 	@AlarmHelp nvarchar(20)= NULL, \n 	@AlarmGroup varchar(50) = NULL, \n 	@AlarmSerialNumber varchar(50) = NULL, \n 	@AlarmPriority nvarchar(50) = NULL, \n 	@isACK bit=0, \n 	@isON bit=0, \n 	@isOFF bit=0, \n 	@CausesDownTime bit=0 \n AS \n BEGIN \n 		if(@Selection='InsertData') \n 	begin \n 		INSERT INTO AlarmMaster(AlarmName,AlarmText,AlarmHelp,AlarmGroup,AlarmPriority,AlarmSerialNumber) VALUES(@AlarmName,@AlarmText,@AlarmHelp,@AlarmGroup,@AlarmPriority,@AlarmSerialNumber); \n 	end \n 	 \n 	if(@Selection='Select') \n 	begin \n 		Select * From AlarmMaster order by AlarmSerialNumber ASC; \n 	end \n 	 \n 	if(@Selection='UpdateDataAlarm') \n 	begin \n 		Update AlarmMaster set isON = @isON,isOFF = @isOFF,isACK = @isACK where AlarmName=@AlarmName ; \n 	end \n 	 \n 	if(@Selection='UpdateDataAlarmACK') \n 	begin \n 		Update AlarmMaster set isACK = @isACK where AlarmName=@AlarmName ; \n 	end \n 	 \n 	if(@Selection='UpdateMasterOff') \n 	begin \n 		Update AlarmMaster set isON = @isON,isOFF = @isOFF,isACK = @isACK where AlarmText=@AlarmText ; \n 	end \n  \n 	if(@Selection='SelectDataByGroup') \n 	begin \n 		Select * From AlarmMaster where AlarmGroup=@AlarmGroup order by AlarmSerialNumber ASC; \n 	end \n END \n ";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE PROCEDURE [dbo].[SPChemicalConsumption]  \n 	@ChemicalName nvarchar(200)= NULL, \n 	@DateTimeCol DateTime = NULL, \n 	@ModeOfOperation nvarchar(20)= NULL, \n 	@SetQuantity numeric(18, 0)=null, \n 	@SetFlowRate numeric(18, 0)=null, \n 	@SetTime numeric(18, 0)=null, \n 	@PumpNo nvarchar(50)=null, \n 	@Selection varchar(20) = NULL \n AS \n BEGIN \n 	if(@Selection='InsertData') \n 	begin \n 		INSERT INTO DosingOperationData(ChemicalName,DosingDateTime,ModeOfOperation,SetQuantity,SetFlowRate,SetTime,PumpNo) VALUES(@ChemicalName,@DateTimeCol,@ModeOfOperation,@SetQuantity,@SetFlowRate,@SetTime,@PumpNo); \n 	end \n END \n ";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE PROCEDURE  [dbo].[SPCurrentTrendDataLog]  \n 	@Selection varchar(20) = NULL, \n 	@SPValueList nvarchar(1000)= NULL, \n 	@ValueList nvarchar(1000)= NULL, \n 	@InTime datetime = NULL \n AS \n BEGIN \n 	if(@Selection='InsertData') \n 	begin  \n 		Declare @Current1val as float \n 		SELECT @Current1val=CAST(Name as float) FROM dbo.SplitStingFromIN(@SPValueList,1) where position=1 \n 		Declare @Current2val as float \n 		SELECT @Current2val=CAST(Name as float) FROM dbo.SplitStingFromIN(@ValueList,1) where position=1 \n 		Declare @Current3val as float \n 		SELECT @Current3val=CAST(Name as float) FROM dbo.SplitStingFromIN(@SPValueList,1) where position=2  \n 		Declare @Current4val as float \n 		SELECT @Current4val=CAST(Name as float) FROM dbo.SplitStingFromIN(@ValueList,1) where position=2   \n 		Declare @Current5val as float  \n 		SELECT @Current5val=CAST(Name as float) FROM dbo.SplitStingFromIN(@SPValueList,1) where position=3  \n 		Declare @Current6val as float  \n 		SELECT @Current6val=CAST(Name as float) FROM dbo.SplitStingFromIN(@ValueList,1) where position=3  \n 		Declare @Current7val as float  \n 		SELECT @Current7val=CAST(Name as float) FROM dbo.SplitStingFromIN(@SPValueList,1) where position=4  \n 		Declare @Current8val as float  \n 		SELECT @Current8val=CAST(Name as float) FROM dbo.SplitStingFromIN(@ValueList,1) where position=4  \n 		Declare @Current9val as float  \n 		SELECT @Current9val=CAST(Name as float) FROM dbo.SplitStingFromIN(@SPValueList,1) where position=5  \n 		Declare @Current10val as float  \n 		SELECT @Current10val=CAST(Name as float) FROM dbo.SplitStingFromIN(@ValueList,1) where position=5  \n 		Declare @Current11val as float  \n 		SELECT @Current11val=CAST(Name as float) FROM dbo.SplitStingFromIN(@SPValueList,1) where position=6  \n 		Declare @Current12val as float  \n 		SELECT @Current12val=CAST(Name as float) FROM dbo.SplitStingFromIN(@ValueList,1) where position=6  \n  \n 		Declare @Current13val as float  \n 		SELECT @Current13val=CAST(Name as float) FROM dbo.SplitStingFromIN(@SPValueList,1) where position=7  \n 		Declare @Current14val as float  \n 		SELECT @Current14val=CAST(Name as float) FROM dbo.SplitStingFromIN(@ValueList,1) where position=7  \n  \n 		Declare @Current15val as float \n 		SELECT @Current15val=CAST(Name as float) FROM dbo.SplitStingFromIN(@SPValueList,1) where position=8  \n 		Declare @Current16val as float \n 		SELECT @Current16val=CAST(Name as float) FROM dbo.SplitStingFromIN(@ValueList,1) where position=8  	 \n  			   \n 		Declare @Current17val as float \n 		SELECT @Current17val=CAST(Name as float) FROM dbo.SplitStingFromIN(@SPValueList,1) where position=9  \n 		Declare @Current18val as float \n 		SELECT @Current18val=CAST(Name as float) FROM dbo.SplitStingFromIN(@ValueList,1) where position=9  	 \n  			   \n 		Declare @Current19val as float		SELECT @Current19val=CAST(Name as float) FROM dbo.SplitStingFromIN(@SPValueList,1) where position=10  \n 		Declare @Current20val as float		SELECT @Current20val=CAST(Name as float) FROM dbo.SplitStingFromIN(@ValueList,1) where position=10   \n 	 			   \n 		Declare @Current21val as float \n 		SELECT @Current21val=CAST(Name as float) FROM dbo.SplitStingFromIN(@SPValueList,1) where position=11  \n 		Declare @Current22val as float \n 		SELECT @Current22val=CAST(Name as float) FROM dbo.SplitStingFromIN(@ValueList,1) where position=11   \n 	 			   \n 		Declare @Current23val as float \n 		SELECT @Current23val=CAST(Name as float) FROM dbo.SplitStingFromIN(@SPValueList,1) where position=12  \n 		Declare @Current24val as float \n 		SELECT @Current24val=CAST(Name as float) FROM dbo.SplitStingFromIN(@ValueList,1) where position=12   \n 	 			   \n 		Declare @Current25val as float \n 		SELECT @Current25val=CAST(Name as float) FROM dbo.SplitStingFromIN(@SPValueList,1) where position=13  \n 		Declare @Current26val as float \n 		SELECT @Current26val=CAST(Name as float) FROM dbo.SplitStingFromIN(@ValueList,1) where position=13 	  \n 			   \n 		Declare @Current27val as float \n 		SELECT @Current27val=CAST(Name as float) FROM dbo.SplitStingFromIN(@SPValueList,1) where position=14  \n 		Declare @Current28val as float \n 		SELECT @Current28val=CAST(Name as float) FROM dbo.SplitStingFromIN(@ValueList,1) where position=14   \n 	 			   \n  \n 		INSERT INTO TrendCurrentData ([DateTimeCol] \n          ,[Anodic1SPCurrent] ,[Anodic1Current] \n            ,[Anodic2SPCurrent] ,[Anodic2Current] \n            ,[AlkalineZinc1SPCurrent] ,[AlkalineZinc1Current] \n            ,[AlkalineZinc2SPCurrent] ,[AlkalineZinc2Current] \n            ,[AlkalineZinc3SPCurrent] ,[AlkalineZinc3Current] \n            ,[AlkalineZinc4SPCurrent] ,[AlkalineZinc4Current] \n            ,[AlkalineZinc5SPCurrent] ,[AlkalineZinc5Current] \n            ,[AlkalineZinc6SPCurrent] ,[AlkalineZinc6Current] \n            ,[AlkalineZinc7SPCurrent] ,[AlkalineZinc7Current] \n            ,[AlkalineZinc8SPCurrent] ,[AlkalineZinc8Current] \n            ,[AlkalineZinc9SPCurrent] ,[AlkalineZinc9Current] \n            ,[AlkalineZinc10SPCurrent] ,[AlkalineZinc10Current] \n            ,[AlkalineZinc11SPCurrent] ,[AlkalineZinc11Current] \n            ,[AlkalineZinc12SPCurrent] ,[AlkalineZinc12Current] \n           )  \n            VALUES \n            (@InTime, \n 		   @Current1val,@Current2val, \n 		   @Current3val,@Current4val, \n 		   @Current5val,@Current6val, \n 		   @Current7val,@Current8val, \n 		   @Current9val,@Current10val, \n 		   @Current11val,@Current12val, \n 		   @Current13val,@Current14val, \n 		   @Current15val,@Current16val, \n 		   @Current17val,@Current18val, \n 		   @Current19val,@Current20val, \n 		   @Current21val,@Current22val, \n 		   @Current23val,@Current24val, \n 		   @Current25val,@Current26val, \n 		   @Current27val,@Current28val); \n 	end \n 	END \n ";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);

                CreateTable = "Create PROCEDURE [dbo].[SPEventMaster]  \n 	@Selection varchar(20) = NULL, \n 	@EventName nvarchar(50)= NULL, \n 	@EventText nvarchar(50) = NULL, \n 	@EventGroup nvarchar(20)= NULL, \n 	@EventOnCondition varchar(50) = NULL, \n 	@EventSerialNo varchar(50) = NULL, \n 	@EventOFFCondition nvarchar(50) = NULL \n AS \n BEGIN \n 	if(@Selection='InsertData') \n 	begin \n 		INSERT INTO EventMaster(EventName,EventText,EventGroup,EventOnCondition,EventSerialNo,EventOFFCondition) VALUES(@EventName,@EventText,@EventGroup,@EventOnCondition,@EventSerialNo,@EventOFFCondition); \n 	end \n 	 \n 	if(@Selection='Select') \n 	begin \n 		Select * From EventMaster ; \n 	end  \n END \n ";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);

                //CreateTable = "CREATE PROCEDURE [dbo].[SPHomeView]  \n 	@StartDate DateTime= null, \n 	@EndDate DateTime= null, \n 	@ShiftNumber int=null, \n 	@ParameterType varchar(50) = NULL	 \n AS \n BEGIN \n 	 \n 	if(@ParameterType='AlarmGraph') \n 	begin \n 		select AlarmGroup ,COUNT(AlarmName) as AlarmCount from AlarmData where AlarmCondition='ON' And AlarmDateTime between @StartDate and @EndDate group by AlarmGroup; \n 	end \n 	if(@ParameterType='ChemicalConsumptionSummary') \n 	begin  \n 		select ChemicalName ,Sum(SetQuantity) as ChemicalConsumptionCount from DosingOperationData where DosingDateTime between @StartDate and @EndDate group by ChemicalName; \n 	end \n 	if(@ParameterType='PartNumberSummary') \n 	begin  \n 		select PartNumber ,CAST(Sum(TotalWeight/WeightPerPart)AS integer) as PartNameCount from LoadPartDetails where DateTimeCol between @StartDate and @EndDate group by PartNumber; \n 	end \n 	if(@ParameterType='AlarmTotalCount') \n 	begin \n 		select * from AlarmMaster where isON = 1; \n 	end \n 	if(@ParameterType='TotalQuantity') \n 	begin  \n 		select  CAST(sum(LP.TotalWeight/WeightPerPart)AS integer)  as TotalQuantity from LoadData LD, LoadPartDetails LP where LD.LoadNumber  like LP.LoadNumber and LoadInTime between @StartDate and @EndDate \n 	end \n 	if(@ParameterType='TotalQuantity_Shift') \n 	begin \n 		 select CAST(sum(LP.TotalWeight/WeightPerPart)AS integer)  as TotalQuantity_Shift from LoadData LD, LoadPartDetails LP where LD.LoadNumber  like LP.LoadNumber and LoadInTime between @StartDate and @EndDate and LoadInShift=@ShiftNumber \n 	end \n 	 \n 	if(@ParameterType='TotalLoads_Shift') \n 	begin \n 		select COUNT(*) as TotalLoads_Shift from LoadData LD where LoadInTime between @StartDate and @EndDate and LoadInShift=@ShiftNumber \n 	end \n 	 \n 	if(@ParameterType='TotalLoads') \n 	begin \n 		select COUNT(*) as TotalLoads from LoadData LD where LoadInTime between @StartDate and @EndDate \n 	end \n 	 \n 	if(@ParameterType='AvgCycleTime') \n 	begin \n 		select Avg(CycleTime) as AvgCycleTime from LoadData LD where LD.LoadInTime between @StartDate and @EndDate \n 	end \n 	 \n END \n ";
                CreateTable = "CREATE PROCEDURE [dbo].[SPHomeView] \n	@StartDate DateTime= null,\n	@EndDate DateTime= null,\n	@ShiftNumber int=null,\n	@ParameterType varchar(50) = NULL	\nAS\nBEGIN\n	\n		if(@ParameterType='AlarmGraph')\n	begin\n		select AlarmGroup ,COUNT(AlarmName) as AlarmCount from AlarmData where AlarmCondition='ON' And AlarmDateTime between @StartDate and @EndDate group by AlarmGroup;\n	end\n	if(@ParameterType='ChemicalConsumptionSummary')\n	begin \n		select ChemicalName ,Sum(SetQuantity) as ChemicalConsumptionCount from DosingOperationData where DosingDateTime between @StartDate and @EndDate group by ChemicalName;\n	end\n	if(@ParameterType='PartNumberSummary')\n	begin\n		select PartNumber ,Sum(Quantity) as PartNameCount from LoadPartDetails where DateTimeCol between @StartDate and @EndDate group by PartNumber;\n	end\n	if(@ParameterType='AlarmTotalCount')\n	begin\n		select * from AlarmMaster where isON = 1;\n	end\n	if(@ParameterType='TotalQuantity')\n	begin\n		select sum(LP.Quantity) as TotalQuantity from LoadData LD, LoadPartDetails LP where LD.LoadNumber  like LP.LoadNumber and LoadInTime between @StartDate and @EndDate\n	end\n	if(@ParameterType='TotalQuantity_Shift')\n	begin\n		select sum(LP.Quantity) as TotalQuantity_Shift from LoadData LD, LoadPartDetails LP where LD.LoadNumber  like LP.LoadNumber and LoadInTime between @StartDate and @EndDate and LoadInShift=@ShiftNumber\n	end\n		if(@ParameterType='TotalLoads_Shift')\n	begin\n		select COUNT(*) as TotalLoads_Shift from LoadData LD where LoadInTime between @StartDate and @EndDate and LoadInShift=@ShiftNumber\n	end\n	\n	if(@ParameterType='TotalLoads')\n	begin\n		select COUNT(*) as TotalLoads from LoadData LD where LoadInTime between @StartDate and @EndDate\n	end\n	\n	if(@ParameterType='AvgCycleTime')\n	begin\n		select Avg(CycleTime) as AvgCycleTime from LoadData LD where LD.LoadInTime between @StartDate and @EndDate\n	end \n	\n	\n	if(@ParameterType='AlarmTotalCountDetails')\n	begin\n		select AlarmGroup,AlarmName from AlarmMaster where isON = 1;\n	end\n	if(@ParameterType='ChemicalConsumptionDetails')\n	begin  \n		SELECT DosingOperationData.ChemicalName, CASE WHEN DosingOperationData.ModeOfOperation = '1' THEN 'Auto' WHEN DosingOperationData.ModeOfOperation = '0' THEN 'Manual' ELSE '' END AS ModeOfOperation, DosingOperationData.ChemicalName,DosingOperationData.PumpNo,DosingOperationData.SetQuantity FROM  DosingOperationData where DosingDateTime between @StartDate and @EndDate ;\n	end\n	if(@ParameterType='PartNumberSummaryDetails')\n	begin\n		select LoadNumber,PartNumber,PartName,Quantity from LoadPartDetails where DateTimeCol between @StartDate and @EndDate;\n	end\n	if(@ParameterType='AlarmSummaryDetails')\n	begin\n	    select AlarmGroup,AlarmName, COUNT(AlarmName) as AlarmCount from AlarmData where AlarmCondition='ON' And AlarmDateTime between @StartDate and @EndDate group by AlarmName,AlarmGroup; \n	end	\n	if(@ParameterType='ACKAlarmSummaryDetails')\n	begin \n		select AlarmGroup,AlarmName from AlarmMaster where isON = 1 and isACK=1;\n	end\n	if(@ParameterType='W1AlarmSummaryDetails')\n	begin\n		select AlarmGroup ,AlarmName  from AlarmData where AlarmCondition='ON' and  AlarmGroup='W1 Alarms' And AlarmDateTime between @StartDate and @EndDate; \n	end\n	if(@ParameterType='W2AlarmSummaryDetails')\n	begin\n		select AlarmGroup ,AlarmName  from AlarmData where AlarmCondition='ON' and  AlarmGroup='W2 Alarms' And AlarmDateTime between @StartDate and @EndDate; \n	end\n	if(@ParameterType='W3AlarmSummaryDetails')\n	begin\n		select AlarmGroup ,AlarmName  from AlarmData where AlarmCondition='ON' and  AlarmGroup='W3 Alarms' And AlarmDateTime between @StartDate and @EndDate; \n	end\n	if(@ParameterType='W4AlarmSummaryDetails')\n	begin\n		select AlarmGroup ,AlarmName  from AlarmData where AlarmCondition='ON' and  AlarmGroup='W4 Alarms' And AlarmDateTime between @StartDate and @EndDate; \n	end\n	if(@ParameterType='W5AlarmSummaryDetails')\n	begin\n		select AlarmGroup ,AlarmName  from AlarmData where AlarmCondition='ON' and  AlarmGroup='W5 Alarms' And AlarmDateTime between @StartDate and @EndDate; \n	end\n	if(@ParameterType='W6AlarmSummaryDetails')\n	begin\n		select AlarmGroup ,AlarmName  from AlarmData where AlarmCondition='ON' and  AlarmGroup='W6 Alarms' And AlarmDateTime between @StartDate and @EndDate; \n	end\n	if(@ParameterType='W7AlarmSummaryDetails')\n	begin\n		select AlarmGroup ,AlarmName  from AlarmData where AlarmCondition='ON' and  AlarmGroup='W7 Alarms' And AlarmDateTime between @StartDate and @EndDate; \n	end\n	if(@ParameterType='W8AlarmSummaryDetails')\n	begin\n		select AlarmGroup ,AlarmName  from AlarmData where AlarmCondition='ON' and  AlarmGroup='W8 Alarms' And AlarmDateTime between @StartDate and @EndDate; \n	end\nEND\n";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE PROCEDURE [dbo].[SPOverview]  \n 	@LineNo float = NULL, \n 	@Selection varchar(20) = NULL \n AS \n BEGIN \n 	if(@Selection='Select') \n 	begin \n 		Select * From StationMaster ; \n 	end \n 	 \n 	if(@Selection='SelectLine') \n 	begin \n 		Select * From StationMaster where SequenceNumber = @LineNo ; \n 	end \n END \n ";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE PROCEDURE [dbo].[SPScreenParameter]  \n 	@ParameterType nvarchar(30) = NULL \n AS \n BEGIN \n 	 \n 	begin \n 		Select * from ScreenParameter where ParameterType=@ParameterType order by ParameterID ASC \n 	end \n 	 \n END \n ";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);

                CreateTable = "Create PROCEDURE [dbo].[SPTrendHistorical] \n 	@StartDate DateTime= null, \n 	@EndDate DateTime= null, \n 	@Selection varchar(20) = NULL \n AS \n BEGIN \n 	if(@Selection='Temperature') \n 	begin \n 		select * from TrendTempData where  DateTimeCol between @StartDate and @EndDate ; \n 	end \n 	if(@Selection='Current') \n 	begin \n 		select * from TrendCurrentData where DateTimeCol between @StartDate and @EndDate; \n 	end \n 	 \n END \n ";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE PROCEDURE [dbo].[SPUserActivity] \n 	@UserName nvarchar(20)= NULL, \n 	@DateTimeCol DateTime = NULL, \n 	@Activity nvarchar(Max)= NULL, \n 	@Selection varchar(20) = NULL \n AS \n BEGIN \n 	if(@Selection='InsertData') \n 	begin \n 		INSERT INTO UserActivityData(UserName,DateTimeCol,Activity) VALUES(@UserName,@DateTimeCol,@Activity); \n 	end \n 	 \n END \n ";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE PROCEDURE  [dbo].[TempTrendDataLog] \n  	@Selection varchar(20) = NULL, \n 	@ValueList nvarchar(1000)= NULL, \n 	@InTime datetime = NULL \n AS \n BEGIN \n 	if(@Selection='InsertData') \n 	begin \n 		Declare @Temp1val as int \n 		SELECT @Temp1val=CAST(Name as int) FROM dbo.SplitStingFromIN(@ValueList,1) where position=1 \n 		Declare @Temp2val as int \n 		SELECT @Temp2val=CAST(Name as int) FROM dbo.SplitStingFromIN(@ValueList,1) where position=2 \n 		Declare @Temp3val as int \n 		SELECT @Temp3val=CAST(Name as int) FROM dbo.SplitStingFromIN(@ValueList,1) where position=3  \n 		Declare @Temp4val as int \n 		SELECT @Temp4val=CAST(Name as int) FROM dbo.SplitStingFromIN(@ValueList,1) where position=4   \n 		Declare @Temp5val as int  \n 		SELECT @Temp5val=CAST(Name as int) FROM dbo.SplitStingFromIN(@ValueList,1) where position=5  \n 		Declare @Temp6val as int  \n 		SELECT @Temp6val=CAST(Name as int) FROM dbo.SplitStingFromIN(@ValueList,1) where position=6  \n 		Declare @Temp7val as int  \n 		SELECT @Temp7val=CAST(Name as int) FROM dbo.SplitStingFromIN(@ValueList,1) where position=7  \n 		Declare @Temp8val as int  \n 		SELECT @Temp8val=CAST(Name as int) FROM dbo.SplitStingFromIN(@ValueList,1) where position=8  \n 		Declare @Temp9val as int  \n 		SELECT @Temp9val=CAST(Name as int) FROM dbo.SplitStingFromIN(@ValueList,1) where position=9 \n 		Declare @Temp10val as int  \n 		SELECT @Temp10val=CAST(Name as int) FROM dbo.SplitStingFromIN(@ValueList,1) where position=10 \n 	    Declare @Temp11val as int  \n 		SELECT @Temp11val=CAST(Name as int) FROM dbo.SplitStingFromIN(@ValueList,1) where position=11 \n 	    Declare @Temp12val as int  \n 		SELECT @Temp12val=CAST(Name as int) FROM dbo.SplitStingFromIN(@ValueList,1) where position=12 \n 	    Declare @Temp13val as int  \n 		SELECT @Temp13val=CAST(Name as int) FROM dbo.SplitStingFromIN(@ValueList,1) where position=13 \n  \n 	INSERT INTO TrendTempData([DateTimeCol] \n            ,[SoakDegressingTemp] \n            ,[Anodic1Temp] \n            ,[Anodic2Temp] \n            ,[AlkalineZinc1Temp] \n 		   ,[AlkalineZinc2Temp] \n 		   ,[AlkalineZinc3Temp] \n 		   ,[AlkalineZinc4Temp] \n 		   ,[AlkalineZinc5Temp] \n            ,[AlkalineZinc6Temp] \n            ,[Passivation1Temp] \n            ,[Passivation2Temp] \n            ,[TopCoatTemp] \n            ,[DryerTemp]) \n            VALUES \n            (@InTime,@Temp1val \n            ,@Temp2val \n            ,@Temp3val \n            ,@Temp4val,@Temp5val,@Temp6val,@Temp7val,@Temp8val,@Temp9val,@Temp10val,@Temp11val,@Temp12val,@Temp13val); \n 	end \n 	 \n END \n ";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);

                CreateTable = "CREATE PROCEDURE [dbo].[UserMasterSP] \n 	@Selection varchar(20) = NULL, \n 	@UserName nvarchar(50)= NULL, \n 	@UserPassword nvarchar(50) = NULL, \n 	@MobileNo nvarchar(15)= NULL, \n 	@UserRole varchar(50) = NULL, \n 	@ReturnValue int out, \n 	@EmailID nvarchar(50) = NULL \n AS \n BEGIN \n 	 \n 	if(@Selection='InsertData') \n 	begin \n 		INSERT INTO UserMaster(UserName,UserPassword,MobileNo,UserRole,EmailID) VALUES(@UserName,@UserPassword,@MobileNo,@UserRole,@EmailID); \n 	end \n 	 \n 	if(@Selection='Select') \n 	begin \n 		Select * From UserMaster ; \n 	end \n 	 \n 	if(@Selection='DeleteUserData') \n 	begin \n 		Delete  From UserMaster where UserName =@UserName; \n 	end \n 	 \n 	if(@Selection='UpdateData') \n 	begin \n 		Update UserMaster set UserPassword=@UserPassword, MobileNo=@MobileNo, UserRole=@UserRole, EmailID=@EmailID where UserName=@UserName ; \n 	end \n 	 \n 	if(@Selection='Login') \n 	begin \n 		declare @passwrd nvarchar(50)		select @passwrd=UserPassword from UserMaster where UserName =@UserName and UserRole=@UserRole ; \n 		if @passwrd is null \n 			set @ReturnValue=-1 \n 		else \n 		if @passwrd=@UserPassword \n 			set @ReturnValue=1 \n 		else \n 			set @ReturnValue=-2 \n 	end \n  \n END \n  ";
                _result = IndiSCADADataAccess.DataAccessCreate.CreateProcedures(ServerName, DatabaseName, CreateTable);
                 

                return _result;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationLogic CreateProcedures()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return 0;
            } 
        }
    }
}
