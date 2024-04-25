using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndiSCADAEntity.Entity;
using System.Collections.ObjectModel;

namespace IndiSCADAGlobalLibrary
{
    public static class TagList
    {
        #region"All Alarms"
        //Wagon 1 alarm values
        public static string[] W1AlarmValue = new string[] { };
        public static string[] W1Alarms
        {
            get
            { return W1AlarmValue; }
            set
            { W1AlarmValue = value; }
        }
        //Wagon 2 alarm values
        public static string[] W2AlarmValue = new string[] { };
        public static string[] W2Alarms
        {
            get
            { return W2AlarmValue; }
            set
            { W2AlarmValue = value; }
        }
        //Wagon 3 alarm values
        public static string[] W3AlarmValue = new string[] { };
        public static string[] W3Alarms
        {
            get
            { return W3AlarmValue; }
            set
            { W3AlarmValue = value; }
        }
        //Wagon 4 alarm values
        public static string[] W4AlarmValue = new string[] { };
        public static string[] W4Alarms
        {
            get
            { return W4AlarmValue; }
            set
            { W4AlarmValue = value; }
        }
        //Wagon 5 alarm values
        public static string[] W5AlarmValue = new string[] { };
        public static string[] W5Alarms
        {
            get
            { return W5AlarmValue; }
            set
            { W5AlarmValue = value; }
        }
        //Wagon 6 alarm values
        public static string[] W6AlarmValue = new string[] { };
        public static string[] W6Alarms
        {
            get
            { return W6AlarmValue; }
            set
            { W6AlarmValue = value; }
        }
        //Temperature Alarms
        public static string[] TemperatureAlarmValue = new string[] { };
        public static string[] TemperatureAlarms
        {
            get
            { return TemperatureAlarmValue; }
            set
            { TemperatureAlarmValue = value; }
        }
        //Dryer Alarms
        public static string[] DryerAlarmValue = new string[] { };
        public static string[] DryerAlarms
        {
            get
            { return DryerAlarmValue; }
            set
            { DryerAlarmValue = value; }
        }
        //Cross Trolley Alarms
        public static string[] CrossTrolleyAlarmValue = new string[] { };
        public static string[] CrossTrolleyAlarms
        {
            get
            { return CrossTrolleyAlarmValue; }
            set
            { CrossTrolleyAlarmValue = value; }
        }
        //Rectifier Alarms
        public static string[] RectifierAlarmValue = new string[] { };
        public static string[] RectifierAlarms1
        {
            get
            { return RectifierAlarmValue; }
            set
            { RectifierAlarmValue = value; }
        }

        public static string[] RectifierAlarm2Value = new string[] { };
        public static string[] RectifierAlarms2
        {
            get
            { return RectifierAlarm2Value; }
            set
            { RectifierAlarm2Value = value; }
        }

        //Trip Alarms
        public static string[] TripAlarmValue = new string[] { };
        public static string[] TripAlarms
        {
            get
            { return TripAlarmValue; }
            set
            { TripAlarmValue = value; }
        }

        //pH Alarms
        public static string[] pHAlarmValue = new string[] { };
        public static string[] pHAlarm
        {
            get
            { return pHAlarmValue; }
            set
            { pHAlarmValue = value; }
        }

        //pH Alarms
        public static string[] FilterPumpAlarmValue = new string[] { };
        public static string[] FilterPumpAlarm
        {
            get
            { return FilterPumpAlarmValue; }
            set
            { FilterPumpAlarmValue = value; }
        }

        //pH Alarms
        public static string[] SprayPumpAlarmValue = new string[] { };
        public static string[] SprayPumpAlarm
        {
            get
            { return SprayPumpAlarmValue; }
            set
            { SprayPumpAlarmValue = value; }
        }

        //pH Alarms
        public static string[] MechAgitatorAlarmValue = new string[] { };
        public static string[] MechAgitatorAlarm
        {
            get
            { return MechAgitatorAlarmValue; }
            set
            { MechAgitatorAlarmValue = value; }
        }

        //pH Alarms
        public static string[] UtilityAlarmValue = new string[] { };
        public static string[] UtilityAlarm
        {
            get
            { return UtilityAlarmValue; }
            set
            { UtilityAlarmValue = value; }
        }


        #endregion
        #region "Temperature values"
        //Temperature actual values
        public static string[] TemperatureActualValue = new string[] { };
        public static string[] TemperatureActual
        {
            get
            { return TemperatureActualValue; }
            set
            { TemperatureActualValue = value; }
        }
        //Temperature High SP values
        public static string[] TemperatureHighSPValue = new string[] { };
        public static string[] TemperatureHighSP
        {
            get
            { return TemperatureHighSPValue; }
            set
            { TemperatureHighSPValue = value; }
        }
        //Temperature Low SP values
        public static string[] TemperatureLowSPValue = new string[] { };
        public static string[] TemperatureLowSP
        {
            get
            { return TemperatureLowSPValue; }
            set
            { TemperatureLowSPValue = value; }
        }
        //Temperature SP values
        public static string[] TemperatureSPValue = new string[] { };
        public static string[] TemperatureSetPoint
        {
            get
            { return TemperatureSPValue; }
            set
            { TemperatureSPValue = value; }
        }
        #endregion

        #region "pH values"
        //Temperature actual values
        public static string[] pHActualValue = new string[] { };
        public static string[] pHActual
        {
            get
            { return pHActualValue; }
            set
            { pHActualValue = value; }
        }
        //pH High SP values
        public static string[] pHHighSPValue = new string[] { };
        public static string[] pHHighSP
        {
            get
            { return pHHighSPValue; }
            set
            { pHHighSPValue = value; }
        }
        //pH Low SP values
        public static string[] pHLowSPValue = new string[] { };
        public static string[] pHLowSP
        {
            get
            { return pHLowSPValue; }
            set
            { pHLowSPValue = value; }
        }

        #endregion
        #region"WagonNextStepOperations"
        //Wagon 1 values
        public static string[] WagonNextStepValue = new string[] { };
        public static string[] WagonNextStep
        {
            get
            { return WagonNextStepValue; }
            set
            { WagonNextStepValue = value; }
        }
        //Wagon 2 values
        public static string[] Wagon2NextStepValue = new string[] { };
        public static string[] Wagon2NextStep
        {
            get
            { return Wagon2NextStepValue; }
            set
            { Wagon2NextStepValue = value; }
        }
        //Wagon next station values
        public static string[] WagonNextStationValue = new string[] { };
        public static string[] WagonNextStation
        {
            get
            { return WagonNextStationValue; }
            set
            { WagonNextStationValue = value; }
        }
        #endregion
        #region"WagonMovements"
        //Wagon 1 values
        public static string[] Wagon1MovmentValue = new string[] { };
        public static string[] Wagon1Movment
        {
            get
            { return Wagon1MovmentValue; }
            set
            { Wagon1MovmentValue = value; }
        }
        //Wagon 2 values
        public static string[] Wagon2MovmentValue = new string[] { };
        public static string[] Wagon2Movment
        {
            get
            { return Wagon2MovmentValue; }
            set
            { Wagon2MovmentValue = value; }
        }
        //
        public static string[] WagonMovmentValue = new string[] { };// 
        public static string[] WagonMovment
        {
            get
            { return WagonMovmentValue; }
            set
            { WagonMovmentValue = value; }
        }
        #endregion
        #region "Rectifier values"
        //Actual Current values
        public static string[] ActualCurrentValue = new string[] { };
        public static string[] ActualCurrent
        {
            get
            { return ActualCurrentValue; }
            set
            { ActualCurrentValue = value; }
        }
        //Avg Current values
        public static string[] AvgCurrentValue = new string[] { };
        public static string[] AvgCurrentValues
        {
            get
            { return AvgCurrentValue; }
            set
            { AvgCurrentValue = value; }
        }
        //Avg Current values
        public static string[] AppliedCurrentValue = new string[] { };
        public static string[] AppliedCurrentValues
        {
            get
            { return AppliedCurrentValue; }
            set
            { AppliedCurrentValue = value; }
        }

        //ActualVolatage values
        public static string[] ActualVoltageValue = new string[] { };
        public static string[] ActualVoltage
        {
            get
            { return ActualVoltageValue; }
            set
            { ActualVoltageValue = value; }
        }
        //Manual Set Current values
        public static string[] ManualCurrentSPValue = new string[] { };
        public static string[] ManualCurrentSP
        {
            get
            { return ManualCurrentSPValue; }
            set
            { ManualCurrentSPValue = value; }
        }
        //Auto Set Current values
        public static string[] AutoCurrentSPValue = new string[] { };
        public static string[] AutoCurrentSP
        {
            get
            { return AutoCurrentSPValue; }
            set
            { AutoCurrentSPValue = value; }
        }
        #endregion
        #region"Tank Details"
        public static Boolean isScreenOpenValue;
        public static Boolean isTankDetailsScreenOpen
        {
            get
            { return isScreenOpenValue; }
            set
            { isScreenOpenValue = value; }
        }
        public static string[] LoadTypeatStationArrayLoadTypeValue = new string[69]; //{ "11","12","0","1","1","0","0", "0", "0", "0", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "1" };
        public static string[] LoadTypeatStationArrayLoadType
        {
            get
            { return LoadTypeatStationArrayLoadTypeValue; }
            set
            { LoadTypeatStationArrayLoadTypeValue = value; }
        }

        //LoadNoAtStation values we have defined array size to as per station no.
        public static string[] LoadNoAtStationValue = new string[69]; //{ "11","12","0","1","1","0","0", "0", "0", "0", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "1" };
        public static string[] LoadNoAtStation
        {
            get
            { return LoadNoAtStationValue; }
            set
            { LoadNoAtStationValue = value; }
        }

        public static string[] MMDDAtStationValue = new string[69]; //{ "11","12","0","1","1","0","0", "0", "0", "0", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "1" };        
        public static string[] MMDDAtStation
        {
            get
            { return MMDDAtStationValue; }
            set
            { MMDDAtStationValue = value; }
        }
        public static string[] YYAtStationValue = new string[69]; //{ "11","12","0","1","1","0","0", "0", "0", "0", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "1" };        
        public static string[] YYAtStation
        {
            get
            { return YYAtStationValue; }
            set
            { YYAtStationValue = value; }
        }

        //LoadNoAtStation values we have defined array size to as per station no.
        public static string[] PartNameAtStationValue = new string[69];
        public static string[] PartNameAtStation
        {
            get
            { return PartNameAtStationValue; }
            set
            { PartNameAtStationValue = value; }
        }

        public static string[] MECLnumberAtStationValue = new string[69];
        public static string[] MECLnumberAtStation
        {
            get
            { return MECLnumberAtStationValue; }
            set
            { MECLnumberAtStationValue = value; }
        }

        #endregion
        #region"Data Logging"
        //W1 data Logging values
        public static string[] W1DataLogValue = new string[] { };
        public static string[] W1DataLog
        {
            get
            { return W1DataLogValue; }
            set
            { W1DataLogValue = value; }
        }
        //W2 data Logging values
        public static string[] W2DataLogValue = new string[] { };
        public static string[] W2DataLog
        {
            get
            { return W2DataLogValue; }
            set
            { W2DataLogValue = value; }
        }
        //W3 data Logging values
        public static string[] W3DataLogValue = new string[] { };
        public static string[] W3DataLog
        {
            get
            { return W3DataLogValue; }
            set
            { W3DataLogValue = value; }
        }
        //W4 data Logging values
        public static string[] W4DataLogValue = new string[] { };
        public static string[] W4DataLog
        {
            get
            { return W4DataLogValue; }
            set
            { W4DataLogValue = value; }
        }
        //W5 data Logging values
        public static string[] W5DataLogValue = new string[] { };
        public static string[] W5DataLog
        {
            get
            { return W5DataLogValue; }
            set
            { W5DataLogValue = value; }
        }
        public static string[] W6DataLogValue = new string[] { };
        public static string[] W6DataLog
        {
            get
            { return W6DataLogValue; }
            set
            { W6DataLogValue = value; }
        }
        public static string[] W7DataLogValue = new string[] { };
        public static string[] W7DataLog
        {
            get
            { return W7DataLogValue; }
            set
            { W7DataLogValue = value; }
        }
        public static string[] W8DataLogValue = new string[] { };
        public static string[] W8DataLog
        {
            get
            { return W8DataLogValue; }
            set
            { W8DataLogValue = value; }
        }
        #endregion
        #region"Events"
        //Events
        public static string[] PlantEventValue = new string[] { };
        public static string[] PlantEventValues
        {
            get
            { return PlantEventValue; }
            set
            { PlantEventValue = value; }
        }
        public static string[] W1EventValue = new string[] { };
        public static string[] W1EventValues
        {
            get
            { return W1EventValue; }
            set
            { W1EventValue = value; }
        }
        public static string[] W2EventValue = new string[] { };
        public static string[] W2EventValues
        {
            get
            { return W2EventValue; }
            set
            { W2EventValue = value; }
        }

        public static string[] W3EventValue = new string[] { };
        public static string[] W3EventValues
        {
            get
            { return W3EventValue; }
            set
            { W3EventValue = value; }
        }

        public static string[] W4EventValue = new string[] { };
        public static string[] W4EventValues
        {
            get
            { return W4EventValue; }
            set
            { W4EventValue = value; }
        }
        public static string[] W5EventValue = new string[] { };
        public static string[] W5EventValues
        {
            get
            { return W5EventValue; }
            set
            { W5EventValue = value; }
        }
        public static string[] W6EventValue = new string[] { };
        public static string[] W6EventValues
        {
            get
            { return W6EventValue; }
            set
            { W6EventValue = value; }
        }
        public static string[] RectifierEventValue = new string[] { };
        public static string[] RectifierEventValues
        {
            get
            { return RectifierEventValue; }
            set
            { RectifierEventValue = value; }
        }
        public static string[] DosingEventValue = new string[] { };
        public static string[] DosingEventValues
        {
            get
            { return DosingEventValue; }
            set
            { DosingEventValue = value; }
        }
        #endregion

        #region shiftwise downtime 
        public static string[] _RealShiftWiseDownTime = new string[] { };
        public static string[] RealShiftWiseDownTime
        {
            get
            { return _RealShiftWiseDownTime; }
            set
            { _RealShiftWiseDownTime = value; }
        }
        public static string[] _PreviousShiftWiseDownTime = new string[] { };
        public static string[] PreviousShiftWiseDownTime
        {
            get
            { return _PreviousShiftWiseDownTime; }
            set
            { _PreviousShiftWiseDownTime = value; }
        }
        #endregion

        #region out of range
        public static string[] ORTempLowCountValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORTempLowCount
        {
            get
            { return ORTempLowCountValue; }
            set
            { ORTempLowCountValue = value; }
        }

        public static string[] ORTempHighCountValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORTempHighCount
        {
            get
            { return ORTempHighCountValue; }
            set
            { ORTempHighCountValue = value; }
        }

        public static string[] ORpHLowCountValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORpHLowCount
        {
            get
            { return ORpHLowCountValue; }
            set
            { ORpHLowCountValue = value; }
        }

        public static string[] ORpHHighCountValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORpHHighCount
        {
            get
            { return ORpHHighCountValue; }
            set
            { ORpHHighCountValue = value; }
        }

        public static string[] ORCurrentLowValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORCurrentLow
        {
            get
            { return ORCurrentLowValue; }
            set
            { ORCurrentLowValue = value; }
        }

        public static string[] ORCurrentHighValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORCurrentHigh
        {
            get
            { return ORCurrentHighValue; }
            set
            { ORCurrentHighValue = value; }
        }

        public static string[] ORDipTimeLowValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORDipTimeLow
        {
            get
            { return ORDipTimeLowValue; }
            set
            { ORDipTimeLowValue = value; }
        }

        public static string[] ORDipTimeHighValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORDipTimeHigh
        {
            get
            { return ORDipTimeHighValue; }
            set
            { ORDipTimeHighValue = value; }
        }

        public static string[] ORpHLowBypassValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORpHLowBypass
        {
            get
            { return ORpHLowBypassValue; }
            set
            { ORpHLowBypassValue = value; }
        }

        public static string[] ORpHHighBypassValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORpHHighBypass
        {
            get
            { return ORpHHighBypassValue; }
            set
            { ORpHHighBypassValue = value; }
        }

        public static string[] ORLowBypassTempValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORLowBypassTemp
        {
            get
            { return ORLowBypassTempValue; }
            set
            { ORLowBypassTempValue = value; }
        }

        public static string[] ORHighBypassTempValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORHighBypassTemp
        {
            get
            { return ORHighBypassTempValue; }
            set
            { ORHighBypassTempValue = value; }
        }

        //use in out of range datalog
        public static string[] ORTempLowSPValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORTempLowSP
        {
            get
            { return ORTempLowSPValue; }
            set
            { ORTempLowSPValue = value; }
        }

        public static string[] ORTempHighSPValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORTempHighSP
        {
            get
            { return ORTempHighSPValue; }
            set
            { ORTempHighSPValue = value; }
        }

        public static string[] ORTempAvgValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORTempAvg
        {
            get
            { return ORTempAvgValue; }
            set
            { ORTempAvgValue = value; }
        }

        public static string[] ORTempLowTimeValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORTempLowTime
        {
            get
            { return ORTempLowTimeValue; }
            set
            { ORTempLowTimeValue = value; }
        }

        public static string[] ORTempHighTimeValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORTempHighTime
        {
            get
            { return ORTempHighTimeValue; }
            set
            { ORTempHighTimeValue = value; }
        }

        public static string[] ORpHLowSPValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORpHLowSP
        {
            get
            { return ORpHLowSPValue; }
            set
            { ORpHLowSPValue = value; }
        }

        public static string[] ORpHHighSPValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORpHHighSP
        {
            get
            { return ORpHHighSPValue; }
            set
            { ORpHHighSPValue = value; }
        }

        public static string[] ORpHLowTimeValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORpHLowTime
        {
            get
            { return ORpHLowTimeValue; }
            set
            { ORpHLowTimeValue = value; }
        }

        public static string[] ORpHHighTimeValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORpHHighTime
        {
            get
            { return ORpHHighTimeValue; }
            set
            { ORpHHighTimeValue = value; }
        }

        public static string[] ORpHAvgValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORpHAvg
        {
            get
            { return ORpHAvgValue; }
            set
            { ORpHAvgValue = value; }
        }

        public static string[] ORCurrentAvgValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORCurrentAvg
        {
            get
            { return ORCurrentAvgValue; }
            set
            { ORCurrentAvgValue = value; }
        }

        public static string[] ORDipTimeValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORDipTime
        {
            get
            { return ORDipTimeValue; }
            set
            { ORDipTimeValue = value; }
        }

        public static string[] ORDiptimeLowBypassValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORDiptimeLowBypass
        {
            get
            { return ORDiptimeLowBypassValue; }
            set
            { ORDiptimeLowBypassValue = value; }
        }

        public static string[] ORDiptimeHighBypassValue = new string[] { };// {"1","10","42","61" };
        public static string[] ORDiptimeHighBypass
        {
            get
            { return ORDiptimeHighBypassValue; }
            set
            { ORDiptimeHighBypassValue = value; }
        }

        public static string[] LastCycleTimeValue = new string[] { };// {"1","10","42","61" };
        public static string[] LastCycleTime
        {
            get
            { return LastCycleTimeValue; }
            set
            { LastCycleTimeValue = value; }
        }

        #endregion

        #region "Debug Loadstring"
        //W5 data Logging values
        public static bool _DataLogDebug;
        public static bool DataLogDebug
        {
            get
            { return _DataLogDebug; }
            set
            { _DataLogDebug = value; }
        }
        #endregion

        #region"IOT"
        private static HomeEntity _PokaYokeZincPlatingLine = new HomeEntity();
        public static HomeEntity PokaYokeZincPlatingLine
        {
            get { return _PokaYokeZincPlatingLine; }
            set { _PokaYokeZincPlatingLine = value; }
        }
        private static HomeEntity _ParameterConcentrationData = new HomeEntity();
        public static HomeEntity ParameterConcentrationData
        {
            get { return _ParameterConcentrationData; }
            set { _ParameterConcentrationData = value; }
        }
        private static HomeEntity _ERPData = new HomeEntity();
        public static HomeEntity ERPData
        {
            get { return _ERPData; }
            set { _ERPData = value; }
        }
        private static HomeEntity _PassivationData = new HomeEntity();
        public static HomeEntity PassivationData
        {
            get { return _PassivationData; }
            set { _PassivationData = value; }
        }

        private static HomeEntity _TotalRecordData = new HomeEntity();
        public static HomeEntity TotalRecordData
        {
            get { return _TotalRecordData; }
            set { _TotalRecordData = value; }
        }
        private static ObservableCollection<HomeEntity> _H2DData = new ObservableCollection<HomeEntity>();
        public static ObservableCollection<HomeEntity> H2DData
        {
            get { return _H2DData; }
            set { _H2DData = value; }
        }
        //zinc plating and poka yoke
        private static ObservableCollection<TankDetailsEntity> _ZincPlatingLine = new ObservableCollection<TankDetailsEntity>();
        public static ObservableCollection<TankDetailsEntity> ZincPlatingLine
        {
            get { return _ZincPlatingLine; }
            set { _ZincPlatingLine = value; }
        }

        private static ObservableCollection<HomeEntity> _ChemicalConsumptionSummaryIOT = new ObservableCollection<HomeEntity>();
        public static ObservableCollection<HomeEntity> ChemicalConsumptionSummaryIOT
        {
            get { return _ChemicalConsumptionSummaryIOT; }
            set { _ChemicalConsumptionSummaryIOT = value; }
        }
        private static ObservableCollection<HomeEntity> _CurrentConsumptionSummaryIOT = new ObservableCollection<HomeEntity>();
        public static ObservableCollection<HomeEntity> CurrentConsumptionSummaryIOT
        {
            get { return _CurrentConsumptionSummaryIOT; }
            set { _CurrentConsumptionSummaryIOT = value; }
        }
        private static ObservableCollection<HomeEntity> _AlarmSummaryGraphIOT = new ObservableCollection<HomeEntity>();
        public static ObservableCollection<HomeEntity> AlarmSummaryGraphIOT
        {
            get { return _AlarmSummaryGraphIOT; }
            set { _AlarmSummaryGraphIOT = value; }
        }
        private static ObservableCollection<HomeEntity> _PartNameSummaryIOT = new ObservableCollection<HomeEntity>();
        public static ObservableCollection<HomeEntity> PartNameSummaryIOT
        {
            get { return _PartNameSummaryIOT; }
            set { _PartNameSummaryIOT = value; }
        }
        private static ObservableCollection<HomeEntity> _PartAreaSummaryIOT = new ObservableCollection<HomeEntity>();
        public static ObservableCollection<HomeEntity> PartAreaSummaryIOT
        {
            get { return _PartAreaSummaryIOT; }
            set { _PartAreaSummaryIOT = value; }
        }

        private static HomeEntity _ProductionDetails = new HomeEntity();
        public static HomeEntity ProductionDetails
        {
            get { return _ProductionDetails; }
            set { _ProductionDetails = value; }
        }
        private static HomeEntity _DateTimeDetails = new HomeEntity();
        public static HomeEntity DateTimeDetails
        {
            get { return _DateTimeDetails; }
            set { _DateTimeDetails = value; }
        }
        #endregion

        #region "Shift Setting"
        public static bool _IsShiftSettingChanged;
        public static bool IsShiftSettingChanged
        {
            get
            { return _IsShiftSettingChanged; }
            set
            { _IsShiftSettingChanged = value; }
        }

        #endregion
        public static string[] LoadDipTimeValue = new string[] { };// {"1","10","42","61" };
        public static string[] LoadDipTime
        {
            get
            { return LoadDipTimeValue; }
            set
            { LoadDipTimeValue = value; }
        }
        public static string[] _LivePokaYokeZincPlatingLine = new string[] { };
        public static string[] LivePokaYokeZincPlatingLine
        {
            get
            { return _LivePokaYokeZincPlatingLine; }
            set
            { _LivePokaYokeZincPlatingLine = value; }
        }
        #region "Setting screen startup"

        //W5 data Logging values
        public static bool _SettingScreenContinuousTagRead;
        public static bool SettingScreenContinuousTagRead
        {
            get
            { return _SettingScreenContinuousTagRead; }
            set
            { _SettingScreenContinuousTagRead = value; }
        }

        //W5 data Logging values
        public static bool _SettingScreenOpen;
        public static bool SettingScreenOpen
        {
            get
            { return _SettingScreenOpen; }
            set
            { _SettingScreenOpen = value; }
        }

        //temp
        public static string[] _TempHigh = new string[] { };
        public static string[] TempHigh
        {
            get
            { return _TempHigh; }
            set
            { _TempHigh = value; }
        }

        public static string[] _TempLow = new string[] { };
        public static string[] TempLow
        {
            get
            { return _TempLow; }
            set
            { _TempLow = value; }
        }

        public static string[] _TempSetpt = new string[] { };
        public static string[] TempSetpt
        {
            get
            { return _TempSetpt; }
            set
            { _TempSetpt = value; }
        }

        //dosing
        public static string[] _DosingAutoManual = new string[] { };
        public static string[] DosingAutoManual
        {
            get
            { return _DosingAutoManual; }
            set
            { _DosingAutoManual = value; }
        }

        public static string[] _DosingManualOffOn = new string[] { };
        public static string[] DosingManualOffOn
        {
            get
            { return _DosingManualOffOn; }
            set
            { _DosingManualOffOn = value; }
        }

        public static string[] _DosingTimeFlowrate = new string[] { };
        public static string[] DosingTimeFlowrate
        {
            get
            { return _DosingTimeFlowrate; }
            set
            { _DosingTimeFlowrate = value; }
        }
        public static string[] _DosingOnOrOffStatus = new string[] { };
        public static string[] DosingOnOrOffStatus
        {
            get
            { return _DosingOnOrOffStatus; }
            set
            { _DosingOnOrOffStatus = value; }
        }
        public static string[] _DosingQuantity = new string[] { };
        public static string[] DosingQuantity
        {
            get
            { return _DosingQuantity; }
            set
            { _DosingQuantity = value; }
        }

        public static string[] _DosingFlowRate = new string[] { };
        public static string[] DosingFlowRate
        {
            get
            { return _DosingFlowRate; }
            set
            { _DosingFlowRate = value; }
        }
        public static string[] _DosingSetAmpHr = new string[] { };
        public static string[] DosingSetAmpHr
        {
            get
            { return _DosingSetAmpHr; }
            set
            { _DosingSetAmpHr = value; }
        }
        public static string[] _DosingActualAmpHr = new string[] { };
        public static string[] DosingActualAmpHr
        {
            get
            { return _DosingActualAmpHr; }
            set
            { _DosingActualAmpHr = value; }
        }
        public static string[] _DosingRemainingTime = new string[] { };
        public static string[] DosingRemainingTime
        {
            get
            { return _DosingRemainingTime; }
            set
            { _DosingRemainingTime = value; }
        }
        public static string[] _DosingTimeInSec = new string[] { };
        public static string[] DosingTimeInSec
        {
            get
            { return _DosingTimeInSec; }
            set
            { _DosingTimeInSec = value; }
        }

        public static string[] _DosingCumulativeAmpHr = new string[] { };
        public static string[] DosingCumulativeAmpHr
        {
            get
            { return _DosingCumulativeAmpHr; }
            set
            { _DosingCumulativeAmpHr = value; }
        }
        public static string[] _SetPH = new string[] { };
        public static string[] SetPH
        {
            get
            { return _SetPH; }
            set
            { _SetPH = value; }
        }
        public static string[] _DosingpHActual = new string[] { };
        public static string[] DosingpHActual
        {
            get
            { return _DosingpHActual; }
            set
            { _DosingpHActual = value; }
        }

        //ph
        public static string[] _pHHigh = new string[] { };
        public static string[] pHHigh
        {
            get
            { return _pHHigh; }
            set
            { _pHHigh = value; }
        }

        public static string[] _pHLow = new string[] { };
        public static string[] pHLow
        {
            get
            { return _pHLow; }
            set
            { _pHLow = value; }
        }

        //wcs
        public static string[] _Wagon1WCSInput = new string[] { };
        public static string[] Wagon1WCSInput
        {
            get
            { return _Wagon1WCSInput; }
            set
            { _Wagon1WCSInput = value; }
        }
        public static string[] _Wagon2WCSInput = new string[] { };
        public static string[] Wagon2WCSInput
        {
            get
            { return _Wagon2WCSInput; }
            set
            { _Wagon2WCSInput = value; }
        }
        public static string[] _Wagon3WCSInput = new string[] { };
        public static string[] Wagon3WCSInput
        {
            get
            { return _Wagon3WCSInput; }
            set
            { _Wagon3WCSInput = value; }
        }
        public static string[] _Wagon4WCSInput = new string[] { };
        public static string[] Wagon4WCSInput
        {
            get
            { return _Wagon4WCSInput; }
            set
            { _Wagon4WCSInput = value; }
        }
        public static string[] _Wagon5WCSInput = new string[] { };
        public static string[] Wagon5WCSInput
        {
            get
            { return _Wagon5WCSInput; }
            set
            { _Wagon5WCSInput = value; }
        }
        public static string[] _Wagon6WCSInput = new string[] { };
        public static string[] Wagon6WCSInput
        {
            get
            { return _Wagon6WCSInput; }
            set
            { _Wagon6WCSInput = value; }
        }
        public static string[] _Wagon7WCSInput = new string[] { };
        public static string[] Wagon7WCSInput
        {
            get
            { return _Wagon7WCSInput; }
            set
            { _Wagon7WCSInput = value; }
        }


        //tank tray bypass
        
        public static string[] _TankBypass = new string[] { };
        public static string[] TankBypass
        {
            get
            { return _TankBypass; }
            set
            { _TankBypass = value; }
        }
        public static string[] _TrayBypass = new string[] { };
        public static string[] TrayBypass
        {
            get
            { return _TrayBypass; }
            set
            { _TrayBypass = value; }
        }


        //filter pump
        public static string[] _FilterPumpOnOFF = new string[] { };
        public static string[] FilterPumpOnOFF
        {
            get
            { return _FilterPumpOnOFF; }
            set
            { _FilterPumpOnOFF = value; }
        }
        public static string[] _FilterPumpInputCBTrip = new string[] { };
        public static string[] FilterPumpInputCBTrip
        {
            get
            { return _FilterPumpInputCBTrip; }
            set
            { _FilterPumpInputCBTrip = value; }
        }
        public static string[] _FilterPumpOutput = new string[] { };
        public static string[] FilterPumpOutput
        {
            get
            { return _FilterPumpOutput; }
            set
            { _FilterPumpOutput = value; }
        }

        //dryer
        public static string[] _DryerBypass = new string[] { };
        public static string[] DryerBypass
        {
            get
            { return _DryerBypass; }
            set
            { _DryerBypass = value; }
        }

        //oilkimmer
        public static string[] _OilSkimmerAutoManual = new string[] { };
        public static string[] OilSkimmerAutoManual
        {
            get
            { return _OilSkimmerAutoManual; }
            set
            { _OilSkimmerAutoManual = value; }
        }
        public static string[] _OilSkimmerTrip = new string[] { };
        public static string[] OilSkimmerTrip
        {
            get
            { return _OilSkimmerTrip; }
            set
            { _OilSkimmerTrip = value; }
        }
        public static string[] _OilSkimmerOutput = new string[] { };
        public static string[] OilSkimmerOutput
        {
            get
            { return _OilSkimmerOutput; }
            set
            { _OilSkimmerOutput = value; }
        }

        //Base Barrel Motor
        public static string[] _BaseBarrelMotorONOFF = new string[] { };
        public static string[] BaseBarrelMotorONOFF
        {
            get
            { return _BaseBarrelMotorONOFF; }
            set
            { _BaseBarrelMotorONOFF = value; }
        }
        public static string[] _BaseBarrelMotorTrip = new string[] { };
        public static string[] BaseBarrelMotorTrip
        {
            get
            { return _BaseBarrelMotorTrip; }
            set
            { _BaseBarrelMotorTrip = value; }
        }
        public static string[] _BaseBarrelMotorStatus = new string[] { };
        public static string[] BaseBarrelMotorStatus
        {
            get
            { return _BaseBarrelMotorStatus; }
            set
            { _BaseBarrelMotorStatus = value; }
        }

        //rectifier
        public static string[] _ManualCurrent = new string[] { };
        public static string[] ManualCurrent
        {
            get
            { return _ManualCurrent; }
            set
            { _ManualCurrent = value; }
        }
        //public static string[] _AppliedCurrent = new string[] { };
        //public static string[] AppliedCurrent
        //{
        //    get
        //    { return _AppliedCurrent; }
        //    set
        //    { _AppliedCurrent = value; }
        //}
        public static string[] _RectifierHighSP = new string[] { };
        public static string[] RectifierHighSP
        {
            get
            { return _RectifierHighSP; }
            set
            { _RectifierHighSP = value; }
        }
        public static string[] _RectifierLowSP = new string[] { };
        public static string[] RectifierLowSP
        {
            get
            { return _RectifierLowSP; }
            set
            { _RectifierLowSP = value; }
        }
        public static string[] _AutoManual = new string[] { };
        public static string[] AutoManual
        {
            get
            { return _AutoManual; }
            set
            { _AutoManual = value; }
        }
        public static string[] _ManulaONOFF = new string[] { };
        public static string[] ManulaONOFF
        {
            get
            { return _ManulaONOFF; }
            set
            { _ManulaONOFF = value; }
        }
        public static string[] _RectifierAlarmStatus = new string[] { };
        public static string[] RectifierAlarmStatus
        {
            get
            { return _RectifierAlarmStatus; }
            set
            { _RectifierAlarmStatus = value; }
        }
        public static string[] _ResetAmpHr = new string[] { };
        public static string[] ResetAmpHr
        {
            get
            { return _ResetAmpHr; }
            set
            { _ResetAmpHr = value; }
        }
        public static string[] _RectifierCumulativeAmpPerHr = new string[] { };
        public static string[] RectifierCumulativeAmpPerHr
        {
            get
            { return _RectifierCumulativeAmpPerHr; }
            set
            { _RectifierCumulativeAmpPerHr = value; }
        }
        public static string[] _RectifierAmpHr = new string[] { };
        public static string[] RectifierAmpHr
        {
            get
            { return _RectifierAmpHr; }
            set
            { _RectifierAmpHr = value; }
        }

        public static string[] _Calculatedcurrent = new string[] { };
        public static string[] Calculatedcurrent
        {
            get
            { return _Calculatedcurrent; }
            set
            { _Calculatedcurrent = value; }
        }
        #endregion
    }
}
