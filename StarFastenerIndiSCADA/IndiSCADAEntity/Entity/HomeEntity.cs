using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public class HomeEntity : INotifyPropertyChanged
    {
        #region"property"
        private string _ShiftNo;
        public string ShiftNo
        {
            get
            {
                return _ShiftNo;
            }
            set
            {
                _ShiftNo = value;
                OnPropertyChanged("ShiftNo");
            }
        }

        //#region downtime shift properties
        //private string _ShiftNo;
        //public string ShiftNo
        //{
        //    get
        //    {
        //        return _ShiftNo;
        //    }
        //    set
        //    {
        //        _ShiftNo = value;
        //        OnPropertyChanged("ShiftNo");
        //    }
        //}
        private int _DownTime;
        public int DownTime
        {
            get
            {
                return _DownTime;
            }
            set
            {
                _DownTime = value;
                OnPropertyChanged("DownTime");
            }
        }
        private int _SemiDownTime;
        public int SemiDownTime
        {
            get
            {
                return _SemiDownTime;
            }
            set
            {
                _SemiDownTime = value;
                OnPropertyChanged("SemiDownTime");
            }
        }
        private int _ManualDownTime;
        public int ManualDownTime
        {
            get
            {
                return _ManualDownTime;
            }
            set
            {
                _ManualDownTime = value;
                OnPropertyChanged("ManualDownTime");
            }
        }
        private int _MaintenanceDownTime;
        public int MaintenanceDownTime
        {
            get
            {
                return _MaintenanceDownTime;
            }
            set
            {
                _MaintenanceDownTime = value;
                OnPropertyChanged("MaintenanceDownTime");
            }
        }
        private int _CompleteDownTime;
        public int CompleteDownTime
        {
            get
            {
                return _CompleteDownTime;
            }
            set
            {
                _CompleteDownTime = value;
                OnPropertyChanged("CompleteDownTime");
            }
        }

        private int _AutoTotalDownTime;
        public int AutoTotalDownTime
        {
            get
            {
                return _AutoTotalDownTime;
            }
            set
            {
                _AutoTotalDownTime = value;
                OnPropertyChanged("AutoTotalDownTime");
            }
        }
        private int _SemiTotalDownTime;
        public int SemiTotalDownTime
        {
            get
            {
                return _SemiTotalDownTime;
            }
            set
            {
                _SemiTotalDownTime = value;
                OnPropertyChanged("SemiTotalDownTime");
            }
        }
        private int _ManualTotalDownTime;
        public int ManualTotalDownTime
        {
            get
            {
                return _ManualTotalDownTime;
            }
            set
            {
                _ManualTotalDownTime = value;
                OnPropertyChanged("ManualTotalDownTime");
            }
        }
        private int _MaintenanceTotalDownTime;
        public int MaintenanceTotalDownTime
        {
            get
            {
                return _MaintenanceTotalDownTime;
            }
            set
            {
                _MaintenanceTotalDownTime = value;
                OnPropertyChanged("MaintenanceTotalDownTime");
            }
        }
        private int _CompleteTotalDownTime;
        public int CompleteTotalDownTime
        {
            get
            {
                return _CompleteTotalDownTime;
            }
            set
            {
                _CompleteTotalDownTime = value;
                OnPropertyChanged("CompleteTotalDownTime");
            }
        }



        private string _AutoLiveDowntimeShift;
        public string AutoLiveDowntimeShift
        {
            get
            {
                return _AutoLiveDowntimeShift;
            }
            set
            {
                _AutoLiveDowntimeShift = value;
                OnPropertyChanged("AutoLiveDowntimeShift");
            }
        }

        private string _SemiLiveDowntimeShift;
        public string SemiLiveDowntimeShift
        {
            get
            {
                return _SemiLiveDowntimeShift;
            }
            set
            {
                _SemiLiveDowntimeShift = value;
                OnPropertyChanged("SemiLiveDowntimeShift");
            }
        }

        private string _ManualLiveDowntimeShift;
        public string ManualLiveDowntimeShift
        {
            get
            {
                return _ManualLiveDowntimeShift;
            }
            set
            {
                _ManualLiveDowntimeShift = value;
                OnPropertyChanged("ManualLiveDowntimeShift");
            }
        }
        private string _MaintainanceLiveDowntimeShift;
        public string MaintainanceLiveDowntimeShift
        {
            get
            {
                return _MaintainanceLiveDowntimeShift;
            }
            set
            {
                _MaintainanceLiveDowntimeShift = value;
                OnPropertyChanged("MaintainanceLiveDowntimeShift");
            }
        }
        private string _CompleteLiveDowntimeShift;
        public string CompleteLiveDowntimeShift
        {
            get
            {
                return _CompleteLiveDowntimeShift;
            }
            set
            {
                _CompleteLiveDowntimeShift = value;
                OnPropertyChanged("CompleteLiveDowntimeShift");
            }
        }

        private string _ShiftNum;
        public string ShiftNum
        {
            get
            {
                return _ShiftNum;
            }
            set
            {
                _ShiftNum = value;
                OnPropertyChanged("ShiftNum");
            }
        }

        private string _AutoPreviousDowntimeShift;
        public string AutoPreviousDowntimeShift
        {
            get
            {
                return _AutoPreviousDowntimeShift;
            }
            set
            {
                _AutoPreviousDowntimeShift = value;
                OnPropertyChanged("AutoPreviousDowntimeShift");
            }
        }

        private string _SemiPreviousDowntimeShift;
        public string SemiPreviousDowntimeShift
        {
            get
            {
                return _SemiPreviousDowntimeShift;
            }
            set
            {
                _SemiPreviousDowntimeShift = value;
                OnPropertyChanged("SemiPreviousDowntimeShift");
            }
        }

        private string _ManualPreviousDowntimeShift;
        public string ManualPreviousDowntimeShift
        {
            get
            {
                return _ManualPreviousDowntimeShift;
            }
            set
            {
                _ManualPreviousDowntimeShift = value;
                OnPropertyChanged("ManualPreviousDowntimeShift");
            }
        }
        private string _MaintenancePreviousDowntimeShift;
        public string MaintenancePreviousDowntimeShift
        {
            get
            {
                return _MaintenancePreviousDowntimeShift;
            }
            set
            {
                _MaintenancePreviousDowntimeShift = value;
                OnPropertyChanged("MaintenancePreviousDowntimeShift");
            }
        }
        private string _CompletePreviousDowntimeShift;
        public string CompletePreviousDowntimeShift
        {
            get
            {
                return _CompletePreviousDowntimeShift;
            }
            set
            {
                _CompletePreviousDowntimeShift = value;
                OnPropertyChanged("CompletePreviousDowntimeShift");
            }
        }


        private int _TotalDownTime;
        public int TotalDownTime
        {
            get
            {
                return _TotalDownTime;
            }
            set
            {
                _TotalDownTime = value;
                OnPropertyChanged("TotalDownTime");
            }
        }


        //#endregion


        private string _SendingDate;
        public string SendingDate
        {
            get
            {
                return _SendingDate;
            }
            set
            {
                _SendingDate = value;
                OnPropertyChanged("SendingDate");
            }
        }


        private string _HDEProcess;
        public string HDEProcess
        {
            get
            {
                return _HDEProcess;
            }
            set
            {
                _HDEProcess = value;
                OnPropertyChanged("HDEProcess");
            }
        }

        private string _AlarmName;
        public string AlarmName
        {
            get
            {
                return _AlarmName;
            }
            set
            {
                _AlarmName = value;
                OnPropertyChanged("AlarmName");
            }
        }
        private int _AlarmONCount;
        public int AlarmONCount
        {
            get
            {
                return _AlarmONCount;
            }
            set
            {
                _AlarmONCount = value;
                OnPropertyChanged("AlarmONCount");
            }
        }

        private int _AlarmCount;
        public int AlarmCount
        {
            get
            {
                return _AlarmCount;
            }
            set
            {
                _AlarmCount = value;
                OnPropertyChanged("AlarmCount");
            }
        }

        private int _AlarmNotACKCount;
        public int AlarmNotACKCount
        {
            get
            {
                return _AlarmNotACKCount;
            }
            set
            {
                _AlarmNotACKCount = value;
                OnPropertyChanged("AlarmNotACKCount");
            }
        }
        private string _PartName;
        public string PartName
        {
            get
            {
                return _PartName;
            }
            set
            {
                _PartName = value;
                OnPropertyChanged("PartName");
            }
        }
        private int _PartNameCount;
        public int PartNameCount
        {
            get
            {
                return _PartNameCount;
            }
            set
            {
                _PartNameCount = value;
                OnPropertyChanged("PartNameCount");
            }
        }
        private long _TotalQty;
        public long TotalQuantityCount
        {
            get
            {
                return _TotalQty;
            }
            set
            {
                _TotalQty = value;
                OnPropertyChanged("TotalQuantityCount");
            }
        }

        private int _TotalLoads;
        public int TotalLoadCount
        {
            get
            {
                return _TotalLoads;
            }
            set
            {
                _TotalLoads = value;
                OnPropertyChanged("TotalLoadCount");
            }
        }

        private int _Shift1QuantityCount;
        public int Shift1QuantityCount
        {
            get
            {
                return _Shift1QuantityCount;
            }
            set
            {
                _Shift1QuantityCount = value;
                OnPropertyChanged("Shift1QuantityCount");
            }
        }

        private int _Shift2QuantityCount;
        public int Shift2QuantityCount
        {
            get
            {
                return _Shift2QuantityCount;
            }
            set
            {
                _Shift2QuantityCount = value;
                OnPropertyChanged("Shift2QuantityCount");
            }
        }

        private int _Shift3QuantityCount;
        public int Shift3QuantityCount
        {
            get
            {
                return _Shift3QuantityCount;
            }
            set
            {
                _Shift3QuantityCount = value;
                OnPropertyChanged("Shift3QuantityCount");
            }
        }

        private int _Shift1LoadCount;
        public int Shift1LoadCount
        {
            get
            {
                return _Shift1LoadCount;
            }
            set
            {
                _Shift1LoadCount = value;
                OnPropertyChanged("Shift1LoadCount");
            }
        }

        private int _Shift2LoadCount;
        public int Shift2LoadCount
        {
            get
            {
                return _Shift2LoadCount;
            }
            set
            {
                _Shift2LoadCount = value;
                OnPropertyChanged("Shift2LoadCount");
            }
        }

        private int _Shift3LoadCount;
        public int Shift3LoadCount
        {
            get
            {
                return _Shift3LoadCount;
            }
            set
            {
                _Shift3LoadCount = value;
                OnPropertyChanged("Shift3LoadCount");
            }
        }

        private string _ChemicalName;
        public string ChemicalName
        {
            get
            {
                return _ChemicalName;
            }
            set
            {
                _ChemicalName = value;
                OnPropertyChanged("ChemicalName");
            }
        }
        private int _ChemicalConsumptionCount;
        public int ChemicalConsumptionCount
        {
            get
            {
                return _ChemicalConsumptionCount;
            }
            set
            {
                _ChemicalConsumptionCount = value;
                OnPropertyChanged("ChemicalConsumptionCount");
            }
        }
        private string _RectifierStnName;
        public string RectifierStnName
        {
            get
            {
                return _RectifierStnName;
            }
            set
            {
                _RectifierStnName = value;
                OnPropertyChanged("RectifierStnName");
            }
        }
        private int _CurrentConsumptionCount;
        public int CurrentConsumptionCount
        {
            get
            {
                return _CurrentConsumptionCount;
            }
            set
            {
                _CurrentConsumptionCount = value;
                OnPropertyChanged("CurrentConsumptionCount");
            }
        }





        private string _AvgCycleTime;
        public string AvgCycleTime
        {
            get
            {
                return _AvgCycleTime;
            }
            set
            {
                _AvgCycleTime = value;
                OnPropertyChanged("AvgCycleTime");
            }
        }



        private string _PlantAM;
        public string PlantAM
        {
            get
            {
                return _PlantAM;
            }
            set
            {
                _PlantAM = value;
                OnPropertyChanged("PlantAM");
            }
        }
        private string _CycleStartStop;
        public string CycleStartStop
        {
            get
            {
                return _CycleStartStop;
            }
            set
            {
                _CycleStartStop = value;
                OnPropertyChanged("CycleStartStop");
            }
        }
        private string _W1AM;
        public string W1AM
        {
            get
            {
                return _W1AM;
            }
            set
            {
                _W1AM = value;
                OnPropertyChanged("W1AM");
            }
        }
        private string _W2AM;
        public string W2AM
        {
            get
            {
                return _W2AM;
            }
            set
            {
                _W2AM = value;
                OnPropertyChanged("W2AM");
            }
        }
        private string _W3AM;
        public string W3AM
        {
            get
            {
                return _W3AM;
            }
            set
            {
                _W3AM = value;
                OnPropertyChanged("W3AM");
            }
        }
        private string _W4AM;
        public string W4AM
        {
            get
            {
                return _W4AM;
            }
            set
            {
                _W4AM = value;
                OnPropertyChanged("W4AM");
            }
        }
        private string _W5AM;
        public string W5AM
        {
            get
            {
                return _W5AM;
            }
            set
            {
                _W5AM = value;
                OnPropertyChanged("W5AM");
            }
        }
        private string _W6AM;
        public string W6AM
        {
            get
            {
                return _W6AM;
            }
            set
            {
                _W6AM = value;
                OnPropertyChanged("W6AM");
            }
        }
        private string _W7AM;
        public string W7AM
        {
            get
            {
                return _W7AM;
            }
            set
            {
                _W7AM = value;
                OnPropertyChanged("W7AM");
            }
        }
        private string _W8AM;
        public string W8AM
        {
            get
            {
                return _W8AM;
            }
            set
            {
                _W8AM = value;
                OnPropertyChanged("W8AM");
            }
        }

        private string _ActualCycleTime;
        public string ActualCycleTime
        {
            get
            {
                return _ActualCycleTime;
            }
            set
            {
                _ActualCycleTime = value;
                OnPropertyChanged("ActualCycleTime");
            }
        }
        private string _ONAlarmCount;
        public string ONAlarmCount
        {
            get
            {
                return _ONAlarmCount;
            }
            set
            {
                _ONAlarmCount = value;
                OnPropertyChanged("ONAlarmCount");
            }
        }
        private string _NotACKAlarmCount;
        public string NotACKAlarmCount
        {
            get
            {
                return _NotACKAlarmCount;
            }
            set
            {
                _NotACKAlarmCount = value;
                OnPropertyChanged("NotACKAlarmCount");
            }
        }
        private string _setCycleTime;
        public string setCycleTime
        {
            get
            {
                return _setCycleTime;
            }
            set
            {
                _setCycleTime = value;
                OnPropertyChanged("setCycleTime");
            }
        }
        private string _LastCycleTime;
        public string LastCycleTime
        {
            get
            {
                return _LastCycleTime;
            }
            set
            {
                _LastCycleTime = value;
                OnPropertyChanged("LastCycleTime");
            }
        }
        private string _UpdationDateTime;
        public string UpdationDateTime
        {
            get
            {
                return _UpdationDateTime;
            }
            set
            {
                _UpdationDateTime = value;
                OnPropertyChanged("UpdationDateTime");
            }
        }
        private string _PlantStatus;
        public string PlantStatus
        {
            get
            {
                return _PlantStatus;
            }
            set
            {
                _PlantStatus = value;
                OnPropertyChanged("PlantStatus");
            }
        }



        private long _PartArea;
        public long PartArea
        {
            get
            {
                return _PartArea;
            }
            set
            {
                _PartArea = value;
                OnPropertyChanged("PartArea");
            }
        }

        private long _Shift1PartArea;
        public long Shift1PartArea
        {
            get
            {
                return _Shift1PartArea;
            }
            set
            {
                _Shift1PartArea = value;
                OnPropertyChanged("Shift1PartArea");
            }
        }
        private long _Shift2PartArea;
        public long Shift2PartArea
        {
            get
            {
                return _Shift2PartArea;
            }
            set
            {
                _Shift2PartArea = value;
                OnPropertyChanged("Shift2PartArea");
            }
        }
        private long _Shift3PartArea;
        public long Shift3PartArea
        {
            get
            {
                return _Shift3PartArea;
            }
            set
            {
                _Shift3PartArea = value;
                OnPropertyChanged("Shift3PartArea");
            }
        }

        private string _ShiftDate;
        public string ShiftDate
        {
            get
            {
                return _ShiftDate;
            }
            set
            {
                _ShiftDate = value;
                OnPropertyChanged("ShiftDate");
            }
        }

        private string _TotalRecord;
        public string Total_Records
        {
            get
            {
                return _TotalRecord;
            }
            set
            {
                _TotalRecord = value;
                OnPropertyChanged("Total_Records");
            }
        }


        //private int _HistoricalZincPlatingParameterName;
        //public int HistoricalZincPlatingParameterName
        //{
        //    get
        //    {
        //        return _HistoricalZincPlatingParameterName;
        //    }
        //    set
        //    {
        //        _HistoricalZincPlatingParameterName = value;
        //        OnPropertyChanged("HistoricalZincPlatingParameterName");
        //    }
        //}
        //private int _HistoricalZincPlatingParameterValue;
        //public int HistoricalZincPlatingParameterValue
        //{
        //    get
        //    {
        //        return _HistoricalZincPlatingParameterValue;
        //    }
        //    set
        //    {
        //        _HistoricalZincPlatingParameterValue = value;
        //        OnPropertyChanged("HistoricalZincPlatingParameterValue");
        //    }
        //}


        #region  zinc plating parameter
        private string _DegreasingTemp;
        public string DegreasingTemp
        {
            get
            {
                return _DegreasingTemp;
            }
            set
            {
                _DegreasingTemp = value;
                OnPropertyChanged("DegreasingTemp");
            }
        }
        private string _DescalingTemp;
        public string DescalingTemp
        {
            get
            {
                return _DescalingTemp;
            }
            set
            {
                _DescalingTemp = value;
                OnPropertyChanged("DescalingTemp");
            }
        }
        private string _AnodicCleaningTemp;
        public string AnodicCleaningTemp
        {
            get
            {
                return _AnodicCleaningTemp;
            }
            set
            {
                _AnodicCleaningTemp = value;
                OnPropertyChanged("AnodicCleaningTemp");
            }
        }
        private string _AnodicCleaningCurrent;
        public string AnodicCleaningCurrent
        {
            get
            {
                return _AnodicCleaningCurrent;
            }
            set
            {
                _AnodicCleaningCurrent = value;
                OnPropertyChanged("AnodicCleaningCurrent");
            }
        }
        private string _ZincPlatingTemp;
        public string ZincPlatingTemp
        {
            get
            {
                return _ZincPlatingTemp;
            }
            set
            {
                _ZincPlatingTemp = value;
                OnPropertyChanged("ZincPlatingTemp");
            }
        }
        private string _PlatingTime;
        public string PlatingTime
        {
            get
            {
                return _PlatingTime;
            }
            set
            {
                _PlatingTime = value;
                OnPropertyChanged("PlatingTime");
            }
        }
        private string _ZincPlatingCurrent;
        public string ZincPlatingCurrent
        {
            get
            {
                return _ZincPlatingCurrent;
            }
            set
            {
                _ZincPlatingCurrent = value;
                OnPropertyChanged("ZincPlatingCurrent");
            }
        }
        private string _TrivalentPassivationPH;
        public string TrivalentPassivationPH
        {
            get
            {
                return _TrivalentPassivationPH;
            }
            set
            {
                _TrivalentPassivationPH = value;
                OnPropertyChanged("TrivalentPassivationPH");
            }
        }
        private string _DMWaterConductivity;
        public string DMWaterConductivity
        {
            get
            {
                return _DMWaterConductivity;
            }
            set
            {
                _DMWaterConductivity = value;
                OnPropertyChanged("DMWaterConductivity");
            }
        }
        private string _TriPassivationTemp;
        public string TriPassivationTemp
        {
            get
            {
                return _TriPassivationTemp;
            }
            set
            {
                _TriPassivationTemp = value;
                OnPropertyChanged("TriPassivationTemp");
            }
        }
        private string _SealantTemp;
        public string SealantTemp
        {
            get
            {
                return _SealantTemp;
            }
            set
            {
                _SealantTemp = value;
                OnPropertyChanged("SealantTemp");
            }
        }
        private string _SealerPH;
        public string SealerPH
        {
            get
            {
                return _SealerPH;
            }
            set
            {
                _SealerPH = value;
                OnPropertyChanged("SealerPH");
            }
        }
        private string _ConveyorOven;
        public string ConveyorOven
        {
            get
            {
                return _ConveyorOven;
            }
            set
            {
                _ConveyorOven = value;
                OnPropertyChanged("ConveyorOven");
            }
        }
        private string _OvenTime;
        public string OvenTime
        {
            get
            {
                return _OvenTime;
            }
            set
            {
                _OvenTime = value;
                OnPropertyChanged("OvenTime");
            }
        }

        #endregion

        //        #region pokayoke para 

        //        private string _OilSkimmer_2_Motor1;

        //        public string OilSkimmer_2_Motor1
        //        {
        //            get
        //            { return _OilSkimmer_2_Motor1; }
        //            set
        //            { _OilSkimmer_2_Motor1 = value; OnPropertyChanged("OilSkimmer_2_Motor1"); }
        //        }


        //        private string _Oil_skimmer_2_Pump;

        //        public string Oil_skimmer_2_Pump
        //        {
        //            get
        //            { return _Oil_skimmer_2_Pump; }
        //            set
        //            { _Oil_skimmer_2_Pump = value; OnPropertyChanged("Oil_skimmer_2_Pump"); }
        //        }


        //        private string _ScrubberPump;

        //        public string  ScrubberPump
        //        {
        //            get
        //            { return _ScrubberPump; }
        //    set
        //            { _ScrubberPump = value; OnPropertyChanged("ScrubberPump");
        //}
        //        }


        //private string _AlkalineZinc_21_24;

        //public string AlkalineZinc_21_24
        //        {
        //            get
        //            { return _AlkalineZinc_21_24; }
        //            set
        //            { _AlkalineZinc_21_24 = value; OnPropertyChanged("AlkalineZinc_21_24"); }
        //        }


        //private string _AlkalineZinc_25_28;

        //public string AlkalineZinc_25_28
        //        {
        //            get
        //            { return _AlkalineZinc_25_28; }
        //            set
        //            { _AlkalineZinc_25_28 = value; OnPropertyChanged("AlkalineZinc_25_28"); }
        //        }



        //private string _AlkalineZinc_29_34;

        //public string AlkalineZinc_29_34
        //        {
        //            get
        //            { return _AlkalineZinc_29_34; }
        //            set
        //            { _AlkalineZinc_29_34 = value; OnPropertyChanged("AlkalineZinc_29_34"); }
        //        }


        //private string _AlkalineZinc_35_38;

        //public string AlkalineZinc_35_38
        //        {
        //            get
        //            { return _AlkalineZinc_35_38; }
        //            set
        //            { _AlkalineZinc_35_38 = value; OnPropertyChanged("AlkalineZinc_35_38"); }
        //        }

        //        private string _Passivation_2;

        //        public string Passivation_2
        //        {
        //            get
        //            { return _Passivation_2; }
        //            set
        //            { _Passivation_2 = value; OnPropertyChanged("Passivation_2"); }
        //        }



        //        private string _TopCoat_1;

        //public string TopCoat_1
        //        {
        //            get
        //            { return _TopCoat_1; }
        //            set
        //            { _TopCoat_1 = value; OnPropertyChanged("TopCoat_1"); }
        //        }



        //private string _TopCoat_2;

        //public string TopCoat_2
        //        {
        //            get
        //            { return _TopCoat_2; }
        //            set
        //            { _TopCoat_2 = value; OnPropertyChanged("TopCoat_2"); }
        //        }



        //private string _TopCoat_3;

        //public string TopCoat_3
        //        {
        //            get
        //            { return _TopCoat_3; }
        //            set
        //            { _TopCoat_3 = value; OnPropertyChanged("TopCoat_3"); }
        //        }



        //private string _AlkalineZinc_21_28;

        //public string AlkalineZinc_21_28
        //        {
        //            get
        //            { return _AlkalineZinc_21_28; }
        //            set
        //            { _AlkalineZinc_21_28 = value; OnPropertyChanged("AlkalineZinc_21_28"); }
        //        }










        //private string _Nitric_Stn_44;

        //        public string Nitric_Stn_44
        //        {
        //            get
        //            { return _Nitric_Stn_44; }
        //            set
        //            { _Nitric_Stn_44 = value; OnPropertyChanged("Nitric_Stn_44"); }
        //        }




        //private string _Passivation_2_Stn_51;

        //public string Passivation_2_Stn_51
        //        {
        //            get
        //            { return _Passivation_2_Stn_51; }
        //            set
        //            { _Passivation_2_Stn_51 = value; OnPropertyChanged("Passivation_2_Stn_51"); }
        //        }




        //private string _TopCoat_2_Stn_64;

        //public string TopCoat_2_Stn_64
        //        {
        //            get
        //            { return _TopCoat_2_Stn_64; }
        //            set
        //            { _TopCoat_2_Stn_64 = value; OnPropertyChanged("TopCoat_2_Stn_64"); }
        //        }





        //private string _TopCoat_1_Stn_63;

        //public string TopCoat_1_Stn_63
        //        {
        //            get
        //            { return _TopCoat_1_Stn_63; }
        //            set
        //            { _TopCoat_1_Stn_63 = value; OnPropertyChanged("TopCoat_1_Stn_63"); }
        //        }







        //private string _TopCoat_3_Stn_65;

        //public string TopCoat_3_Stn_65
        //        {
        //            get
        //            { return _TopCoat_3_Stn_65; }
        //            set
        //            { _TopCoat_3_Stn_65 = value; OnPropertyChanged("TopCoat_3_Stn_65"); }
        //        }




        //private string _Anodic_1;

        //public string Anodic_1
        //        {
        //            get
        //            { return _Anodic_1; }
        //            set
        //            { _Anodic_1 = value; OnPropertyChanged("Anodic_1"); }
        //        }





        //private string _Anodic_2;

        //public string Anodic_2
        //        {
        //            get
        //            { return _Anodic_2; }
        //            set
        //            { _Anodic_2 = value; OnPropertyChanged("Anodic_2"); }
        //        }






        //private string _Anodic_3;

        //public string Anodic_3
        //        {
        //            get
        //            { return _Anodic_3; }
        //            set
        //            { _Anodic_3 = value; OnPropertyChanged("Anodic_3"); }
        //        }






        //private string _AlZn_1;

        //public string AlZn_1
        //        {
        //            get
        //            { return _AlZn_1; }
        //            set
        //            { _AlZn_1 = value; OnPropertyChanged("AlZn_1"); }
        //        }






        //private string _AlZn_2;

        //public string AlZn_2
        //        {
        //            get
        //            { return _AlZn_2; }
        //            set
        //            { _AlZn_2 = value; OnPropertyChanged("AlZn_2"); }
        //        }





        //private string _AlZn_3;

        //public string AlZn_3
        //        {
        //            get
        //            { return _AlZn_3; }
        //            set
        //            { _AlZn_2 = value; OnPropertyChanged("AlZn_3"); }
        //        }





        //private string _AlZn_4;

        //public string AlZn_4
        //        {
        //            get
        //            { return _AlZn_4; }
        //            set
        //            { _AlZn_4 = value; OnPropertyChanged("AlZn_4"); }
        //        }





        //private string _AlZn_5;

        //public string AlZn_5
        //        {
        //            get
        //            { return _AlZn_5; }
        //            set
        //            { _AlZn_5 = value; OnPropertyChanged("AlZn_5"); }
        //        }






        //private string _AlZn_6;

        //public string AlZn_6
        //        {
        //            get
        //            { return _AlZn_6; }
        //            set
        //            { _AlZn_6 = value; OnPropertyChanged("AlZn_6"); }
        //        }




        //private string _AlZn_7;

        //public string AlZn_7
        //        {
        //            get
        //            { return _AlZn_7; }
        //            set
        //            { _AlZn_7 = value; OnPropertyChanged("AlZn_7"); }
        //        }


        //private string _AlZn_8;

        //public string AlZn_8
        //        {
        //            get
        //            { return _AlZn_8; }
        //            set
        //            { _AlZn_8 = value; OnPropertyChanged("AlZn_8"); }
        //        }





        //private string _AlZn_9;

        //public string AlZn_9
        //        {
        //            get
        //            { return _AlZn_9; }
        //            set
        //            { _AlZn_9 = value; OnPropertyChanged("AlZn_9"); }
        //        }





        //private string _AlZn_10;

        //public string AlZn_10
        //        {
        //            get
        //            { return _AlZn_10; }
        //            set
        //            { _AlZn_10 = value; OnPropertyChanged("AlZn_10"); }
        //        }





        //private string _AlZn_11;

        //public string AlZn_11
        //        {
        //            get
        //            { return _AlZn_11; }
        //            set
        //            { _AlZn_11 = value; OnPropertyChanged("AlZn_11"); }
        //        }





        //private string _AlZn_12;

        //public string AlZn_12
        //        {
        //            get
        //            { return _AlZn_12; }
        //            set
        //            { _AlZn_12 = value; OnPropertyChanged("AlZn_12"); }
        //        }












        //private string _AlZn_13;

        //public string AlZn_13
        //        {
        //            get
        //            { return _AlZn_13; }
        //            set
        //            { _AlZn_13 = value; OnPropertyChanged("AlZn_13"); }
        //        }





        //private string _AlZn_14;

        //public string AlZn_14
        //        {
        //            get
        //            { return _AlZn_14; }
        //            set
        //            { _AlZn_14 = value; OnPropertyChanged("AlZn_14"); }
        //        }





        //private string _AlZn_15;

        //public string AlZn_15
        //        {
        //            get
        //            { return _AlZn_15; }
        //            set
        //            { _AlZn_15 = value; OnPropertyChanged("AlZn_15"); }
        //        }






        //private string _AlZn_16;

        //public string AlZn_16
        //        {
        //            get
        //            { return _AlZn_16; }
        //            set
        //            { _AlZn_16 = value; OnPropertyChanged("AlZn_16"); }
        //        }





        //private string _AlZn_17;

        //public string AlZn_17
        //        {
        //            get
        //            { return _AlZn_17; }
        //            set
        //            { _AlZn_17 = value; OnPropertyChanged("AlZn_17"); }
        //        }


        //private string _AlZn_18;

        //public string AlZn_18
        //        {
        //            get
        //            { return _AlZn_18; }
        //            set
        //            { _AlZn_18 = value; OnPropertyChanged("AlZn_18"); }
        //        }





        //        #endregion

        #region pokayoke para

        private string _Oil_skimmer_2_Motor_P1;

        public string Oil_skimmer_2_Motor_P1
        {
            get
            { return _Oil_skimmer_2_Motor_P1; }
            set
            { _Oil_skimmer_2_Motor_P1 = value; OnPropertyChanged("Oil_skimmer_2_Motor_P1"); }
        }


        private string _Oil_skimmer_2_Pump_P2;

        public string Oil_skimmer_2_Pump_P2
        {
            get
            { return _Oil_skimmer_2_Pump_P2; }
            set
            { _Oil_skimmer_2_Pump_P2 = value; OnPropertyChanged("Oil_skimmer_2_Pump_P2"); }
        }


        private string _ScrubberPump_P3;

        public string ScrubberPump_P3
        {
            get
            { return _ScrubberPump_P3; }
            set
            { _ScrubberPump_P3 = value; OnPropertyChanged("ScrubberPump_P3"); }
        }


        private string _FilterPump_6_P4;

        public string FilterPump_6_P4
        {
            get
            { return _FilterPump_6_P4; }
            set
            { _FilterPump_6_P4 = value; OnPropertyChanged("FilterPump_6_P4"); }
        }

        private string _FilterPump_7_P5;

        public string FilterPump_7_P5
        {
            get
            { return _FilterPump_7_P5; }
            set
            { _FilterPump_7_P5 = value; OnPropertyChanged("FilterPump_7_P5"); }
        }

        private string _FilterPump_8_P6;

        public string FilterPump_8_P6
        {
            get
            { return _FilterPump_8_P6; }
            set
            { _FilterPump_8_P6 = value; OnPropertyChanged("FilterPump_8_P6"); }
        }

        private string _FilterPump_9_P7;

        public string FilterPump_9_P7
        {
            get
            { return _FilterPump_9_P7; }
            set
            { _FilterPump_9_P7 = value; OnPropertyChanged("FilterPump_9_P7"); }
        }

        private string _FilterPump_13_P8;

        public string FilterPump_13_P8
        {
            get
            { return _FilterPump_13_P8; }
            set
            { _FilterPump_13_P8 = value; OnPropertyChanged("FilterPump_13_P8"); }
        }

        private string _FilterPump_15_P9;

        public string FilterPump_15_P9
        {
            get
            { return _FilterPump_15_P9; }
            set
            { _FilterPump_15_P9 = value; OnPropertyChanged("FilterPump_15_P9"); }
        }

        private string _FilterPump_16_P10;

        public string FilterPump_16_P10
        {
            get
            { return _FilterPump_16_P10; }
            set
            { _FilterPump_16_P10 = value; OnPropertyChanged("FilterPump_16_P10"); }
        }

        private string _FilterPump_17_P11;

        public string FilterPump_17_P11
        {
            get
            { return _FilterPump_17_P11; }
            set
            { _FilterPump_17_P11 = value; OnPropertyChanged("FilterPump_17_P11"); }
        }

        private string _DosingPump_1_P12;

        public string DosingPump_1_P12
        {
            get
            { return _DosingPump_1_P12; }
            set
            { _DosingPump_1_P12 = value; OnPropertyChanged("DosingPump_1_P12"); }
        }

        private string _DosingPump_P13;

        public string DosingPump_P13
        {
            get
            { return _DosingPump_P13; }
            set
            { _DosingPump_P13 = value; OnPropertyChanged("DosingPump_P13"); }
        }

        private string _DosingPump_3_P14;

        public string DosingPump_3_P14
        {
            get
            { return _DosingPump_3_P14; }
            set
            { _DosingPump_3_P14 = value; OnPropertyChanged("DosingPump_3_P14"); }
        }

        private string _DosingPump_4_P15;

        public string DosingPump_4_P15
        {
            get
            { return _DosingPump_4_P15; }
            set
            { _DosingPump_4_P15 = value; OnPropertyChanged("DosingPump_4_P15"); }
        }

        private string _DosingPump_5_P16;

        public string DosingPump_5_P16
        {
            get
            { return _DosingPump_5_P16; }
            set
            { _DosingPump_5_P16 = value; OnPropertyChanged("DosingPump_5_P16"); }
        }

        private string _DosingPump_6_P17;

        public string DosingPump_6_P17
        {
            get
            { return _DosingPump_6_P17; }
            set
            { _DosingPump_6_P17 = value; OnPropertyChanged("DosingPump_6_P17"); }
        }

        private string _DosingPump_7_P18;

        public string DosingPump_7_P18
        {
            get
            { return _DosingPump_7_P18; }
            set
            { _DosingPump_7_P18 = value; OnPropertyChanged("DosingPump_7_P18"); }
        }

        private string _DosingPump_8_P19;

        public string DosingPump_8_P19
        {
            get
            { return _DosingPump_8_P19; }
            set
            { _DosingPump_8_P19 = value; OnPropertyChanged("DosingPump_8_P19"); }
        }

        private string _DosingPump_9_P20;

        public string DosingPump_9_P20
        {
            get
            { return _DosingPump_9_P20; }
            set
            { _DosingPump_9_P20 = value; OnPropertyChanged("DosingPump_9_P20"); }
        }

        private string _DosingPump_10_P21;

        public string DosingPump_10_P21
        {
            get
            { return _DosingPump_10_P21; }
            set
            { _DosingPump_10_P21 = value; OnPropertyChanged("DosingPump_10_P21"); }
        }

        private string _DosingPump_11_P22;

        public string DosingPump_11_P22
        {
            get
            { return _DosingPump_11_P22; }
            set
            { _DosingPump_11_P22 = value; OnPropertyChanged("DosingPump_11_P22"); }
        }

        private string _DosingPump_12_P23;

        public string DosingPump_12_P23
        {
            get
            { return _DosingPump_12_P23; }
            set
            { _DosingPump_12_P23 = value; OnPropertyChanged("DosingPump_12_P23"); }
        }

        private string _DosingPump_13_P24;

        public string DosingPump_13_P24
        {
            get
            { return _DosingPump_13_P24; }
            set
            { _DosingPump_13_P24 = value; OnPropertyChanged("DosingPump_13_P24"); }
        }

        private string _DosingPump_17_P25;

        public string DosingPump_17_P25
        {
            get
            { return _DosingPump_17_P25; }
            set
            { _DosingPump_17_P25 = value; OnPropertyChanged("DosingPump_17_P25"); }
        }

    

        private string _DosingPump_18_P26;

        public string DosingPump_18_P26
        {
            get
            { return _DosingPump_18_P26; }
            set
            { _DosingPump_18_P26 = value; OnPropertyChanged("DosingPump_18_P26"); }
        }

        private string _DosingPump_19_P27;

        public string DosingPump_19_P27
        {
            get
            { return _DosingPump_19_P27; }
            set
            { _DosingPump_19_P27 = value; OnPropertyChanged("DosingPump_19_P27"); }
        }

        private string _DosingPump_20_P28;

        public string DosingPump_20_P28
        {
            get
            { return _DosingPump_20_P28; }
            set
            { _DosingPump_20_P28 = value; OnPropertyChanged("DosingPump_20_P28"); }
        }

        private string _DosingPump_21_P29;

        public string DosingPump_21_P29
        {
            get
            { return _DosingPump_21_P29; }
            set
            { _DosingPump_21_P29 = value; OnPropertyChanged("DosingPump_21_P29"); }
        }


        private string _DosingPump_22_P30;

        public string DosingPump_22_P30
        {
            get
            { return _DosingPump_22_P30; }
            set
            { _DosingPump_22_P30 = value; OnPropertyChanged("DosingPump_22_P30"); }
        }



        private string _DosingPump_23_P31;

        public string DosingPump_23_P31
        {
            get
            { return _DosingPump_23_P31; }
            set
            { _DosingPump_23_P31 = value; OnPropertyChanged("DosingPump_23_P31"); }
        }



        private string _DosingPump_24_P32;

        public string DosingPump_24_P32
        {
            get
            { return _DosingPump_24_P32; }
            set
            { _DosingPump_24_P32 = value; OnPropertyChanged("DosingPump_24_P32"); }
        }

        private string _DosingPump_25_P33;

        public string DosingPump_25_P33
        {
            get
            { return _DosingPump_25_P33; }
            set
            { _DosingPump_25_P33 = value; OnPropertyChanged("DosingPump_25_P33"); }
        }

        private string _Anodic_1_P34;

        public string Anodic_1_P34
        {
            get
            { return _Anodic_1_P34; }
            set
            { _Anodic_1_P34 = value; OnPropertyChanged("Anodic_1_P34"); }
        }


        private string _Anodic_2_P35;

        public string Anodic_2_P35
        {
            get
            { return _Anodic_2_P35; }
            set
            { _Anodic_2_P35 = value; OnPropertyChanged("Anodic_2_P35"); }
        }

        private string _Anodic_3_P36;

        public string Anodic_3_P36
        {
            get
            { return _Anodic_3_P36; }
            set
            { _Anodic_3_P36 = value; OnPropertyChanged("Anodic_3_P36"); }
        }


        private string _AlZn_1_P37;

        public string AlZn_1_P37
        {
            get
            { return _AlZn_1_P37; }
            set
            { _AlZn_1_P37 = value; OnPropertyChanged("AlZn_1_P37"); }
        }


        private string _AlZn_2_P38;

        public string AlZn_2_P38
        {
            get
            { return _AlZn_2_P38; }
            set
            { _AlZn_2_P38 = value; OnPropertyChanged("AlZn_2_P38"); }
        }

        private string _AlZn_3_P39;

        public string AlZn_3_P39
        {
            get
            { return _AlZn_3_P39; }
            set
            { _AlZn_3_P39 = value; OnPropertyChanged("AlZn_3_P39"); }
        }
        private string _AlZn_4_P40;

        public string AlZn_4_P40
        {
            get
            { return _AlZn_4_P40; }
            set
            { _AlZn_4_P40 = value; OnPropertyChanged("AlZn_4_P40"); }
        }


        private string _AlZn_5_P41;

        public string AlZn_5_P41
        {
            get
            { return _AlZn_5_P41; }
            set
            { _AlZn_5_P41 = value; OnPropertyChanged("AlZn_5_P41"); }
        }


        private string _AlZn_6_P42;

        public string AlZn_6_P42
        {
            get
            { return _AlZn_6_P42; }
            set
            { _AlZn_6_P42 = value; OnPropertyChanged("AlZn_6_P42"); }
        }


        private string _AlZn_7_P43;

        public string AlZn_7_P43
        {
            get
            { return _AlZn_7_P43; }
            set
            { _AlZn_7_P43 = value; OnPropertyChanged("AlZn_7_P43"); }
        }

        private string _AlZn_8_P44;

        public string AlZn_8_P44
        {
            get
            { return _AlZn_8_P44; }
            set
            { _AlZn_8_P44 = value; OnPropertyChanged("AlZn_8_P44"); }
        }

        private string _AlZn_9_P45;

        public string AlZn_9_P45
        {
            get
            { return _AlZn_9_P45; }
            set
            { _AlZn_9_P45 = value; OnPropertyChanged("AlZn_9_P45"); }
        }

        private string _AlZn_10_P46;

        public string AlZn_10_P46
        {
            get
            { return _AlZn_10_P46; }
            set
            { _AlZn_10_P46 = value; OnPropertyChanged("AlZn_10_P46"); }
        }

        private string _AlZn_11_P47;

        public string AlZn_11_P47
        {
            get
            { return _AlZn_11_P47; }
            set
            { _AlZn_11_P47 = value; OnPropertyChanged("AlZn_11_P47"); }
        }

        private string _AlZn_12_P48;

        public string AlZn_12_P48
        {
            get
            { return _AlZn_12_P48; }
            set
            { _AlZn_12_P48 = value; OnPropertyChanged("AlZn_12_P48"); }
        }

        private string _AlZn_13_P49;

        public string AlZn_13_P49
        {
            get
            { return _AlZn_13_P49; }
            set
            { _AlZn_13_P49 = value; OnPropertyChanged("AlZn_13_P49"); }
        }

        private string _AlZn_14_P50;

        public string AlZn_14_P50
        {
            get
            { return _AlZn_14_P50; }
            set
            { _AlZn_14_P50 = value; OnPropertyChanged("AlZn_14_P50"); }
        }

        private string _AlZn_15_P51;

        public string AlZn_15_P51
        {
            get
            { return _AlZn_15_P51; }
            set
            { _AlZn_15_P51 = value; OnPropertyChanged("AlZn_15_P51"); }
        }

        private string _AlZn_16_P52;

        public string AlZn_16_P52
        {
            get
            { return _AlZn_16_P52; }
            set
            { _AlZn_16_P52 = value; OnPropertyChanged("AlZn_16_P52"); }
        }

        private string _AlZn_17_P53;

        public string AlZn_17_P53
        {
            get
            { return _AlZn_17_P53; }
            set
            { _AlZn_17_P53 = value; OnPropertyChanged("AlZn_17_P53"); }
        }

        private string _AlZn_18_P54;

        public string AlZn_18_P54
        {
            get
            { return _AlZn_18_P54; }
            set
            { _AlZn_18_P54 = value; OnPropertyChanged("AlZn_18_P54"); }
        }


        #endregion

        #region PokaYokeNewparametersOld
        private string _CED_Rectifier;
        public string CED_Rectifier
        {
            get
            {
                return _CED_Rectifier;
            }
            set
            {
                _CED_Rectifier = value;
                OnPropertyChanged("CED_Rectifier");
            }
        }
        private string _Hot_Water_Temperature;
        public string Hot_Water_Temperature
        {
            get
            {
                return _Hot_Water_Temperature;
            }
            set
            {
                _Hot_Water_Temperature = value;
                OnPropertyChanged("Hot_Water_Temperature");
            }
        }
        private string _KOD_Temperature;
        public string KOD_Temperature
        {
            get
            {
                return _KOD_Temperature;
            }
            set
            {
                _KOD_Temperature = value;
                OnPropertyChanged("KOD_Temperature");
            }
        }
        private string _Degreasing_Temperature;
        public string Degreasing_Temperature
        {
            get
            {
                return _Degreasing_Temperature;
            }
            set
            {
                _Degreasing_Temperature = value;
                OnPropertyChanged("Degreasing_Temperature");
            }
        }
        private string _Phosphating_Temperature;
        public string Phosphating_Temperature
        {
            get
            {
                return _Phosphating_Temperature;
            }
            set
            {
                _Phosphating_Temperature = value;
                OnPropertyChanged("Phosphating_Temperature");
            }
        }
        private string _CED_Temperature;
        public string CED_Temperature
        {
            get
            {
                return _CED_Temperature;
            }
            set
            {
                _CED_Temperature = value;
                OnPropertyChanged("CED_Temperature");
            }
        }
        private string _North_Zone_Temperature;
        public string North_Zone_Temperature
        {
            get
            {
                return _North_Zone_Temperature;
            }
            set
            {
                _North_Zone_Temperature = value;
                OnPropertyChanged("North_Zone_Temperature");
            }
        }
        private string _South_Zone_Temperature;
        public string South_Zone_Temperature
        {
            get
            {
                return _South_Zone_Temperature;
            }
            set
            {
                _South_Zone_Temperature = value;
                OnPropertyChanged("South_Zone_Temperature");
            }
        }
        private string _East_Zone_Temperature;
        public string East_Zone_Temperature
        {
            get
            {
                return _East_Zone_Temperature;
            }
            set
            {
                _East_Zone_Temperature = value;
                OnPropertyChanged("East_Zone_Temperature");
            }
        }
        private string _West_Zone_Temperature;
        public string West_Zone_Temperature
        {
            get
            {
                return _West_Zone_Temperature;
            }
            set
            {
                _West_Zone_Temperature = value;
                OnPropertyChanged("West_Zone_Temperature");
            }
        }
        private string _Heat_Exchanger_Temperature;
        public string Heat_Exchanger_Temperature
        {
            get
            {
                return _Heat_Exchanger_Temperature;
            }
            set
            {
                _Heat_Exchanger_Temperature = value;
                OnPropertyChanged("Heat_Exchanger_Temperature");
            }
        }
        #endregion

    
        #region concentration
        //concentration
        private string _ZincConcentration;
        public string ZincConcentration
        {
            get
            {
                return _ZincConcentration;
            }
            set
            {
                _ZincConcentration = value;
                OnPropertyChanged("ZincConcentration");
            }
        }
        private string _CausticSodaConcentration;
        public string CausticSodaConcentration
        {
            get
            {
                return _CausticSodaConcentration;
            }
            set
            {
                _CausticSodaConcentration = value;
                OnPropertyChanged("CausticSodaConcentration");
            }
        }

        #endregion

        #region ERP  
        private string _ERPInwardQuantity;
        public string ERPInwardQuantity
        {
            get
            {
                return _ERPInwardQuantity;
            }
            set
            {
                _ERPInwardQuantity = value;
                OnPropertyChanged("ERPInwardQuantity");
            }
        }
        private string _ERPRunningQuantity;
        public string ERPRunningQuantity
        {
            get
            {
                return _ERPRunningQuantity;
            }
            set
            {
                _ERPRunningQuantity = value;
                OnPropertyChanged("ERPRunningQuantity");
            }
        }
        private string _ERPCompletedQuantity;
        public string ERPCompletedQuantity
        {
            get
            {
                return _ERPCompletedQuantity;
            }
            set
            {
                _ERPCompletedQuantity = value;
                OnPropertyChanged("ERPCompletedQuantity");
            }
        }
        private string _ERPBalancedQuantity;
        public string ERPBalancedQuantity
        {
            get
            {
                return _ERPBalancedQuantity;
            }
            set
            {
                _ERPBalancedQuantity = value;
                OnPropertyChanged("ERPBalancedQuantity");
            }
        }



        #endregion

        #region H2D
        private int _Quantity;
        public int Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                _Quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        #endregion

        #region passivation summary
        private string _PassivationSilver;
        public string PassivationSilver
        {
            get
            {
                return _PassivationSilver;
            }
            set
            {
                _PassivationSilver = value;
                OnPropertyChanged("PassivationSilver");
            }
        }
        private string _PassivationYellow;
        public string PassivationYellow
        {
            get
            {
                return _PassivationYellow;
            }
            set
            {
                _PassivationYellow = value;
                OnPropertyChanged("PassivationYellow");
            }
        }
        private string _PassivationBlack;
        public string PassivationBlack
        {
            get
            {
                return _PassivationBlack;
            }
            set
            {
                _PassivationBlack = value;
                OnPropertyChanged("PassivationBlack");
            }
        }
        private string _PassivationZincIron;
        public string PassivationZincIron
        {
            get
            {
                return _PassivationZincIron;
            }
            set
            {
                _PassivationZincIron = value;
                OnPropertyChanged("PassivationZincIron");
            }
        }
        #endregion

        #endregion

        #region Constructor

        public HomeEntity()
        { }

        public HomeEntity(HomeEntity Obj)
        {
            _SendingDate = Obj.SendingDate;
            _AlarmName = Obj.AlarmName;
            //_AlarmONCount = Obj.AlarmONCount;
            //_AlarmNotACKCount = Obj.AlarmNotACKCount;
            _PartName = Obj.PartName;
            _PartNameCount = Obj.PartNameCount;
            _ChemicalName = Obj.ChemicalName;
            _ChemicalConsumptionCount = Obj.ChemicalConsumptionCount;

            _RectifierStnName = Obj.RectifierStnName;
            _CurrentConsumptionCount = Obj.CurrentConsumptionCount;

            _Shift1QuantityCount = Obj.Shift1QuantityCount;
            _Shift2QuantityCount = Obj.Shift2QuantityCount;
            _Shift3QuantityCount = Obj.Shift3QuantityCount;
            _Shift1LoadCount = Obj.Shift1LoadCount;
            _Shift2LoadCount = Obj.Shift2LoadCount;
            _Shift3LoadCount = Obj.Shift3LoadCount;

            _TotalLoads = Obj.TotalLoadCount;
            _TotalQty = Obj._TotalQty;
            _AvgCycleTime = Obj.AvgCycleTime;
            _HDEProcess = Obj.HDEProcess;

            _W6AM = Obj.W6AM;
            _W5AM = Obj.W5AM;
            _W4AM = Obj.W4AM;
            _W3AM = Obj.W3AM;
            _W2AM = Obj.W2AM;
            _W1AM = Obj.W1AM;

            _PlantAM = Obj.PlantAM;
            _CycleStartStop = Obj.CycleStartStop;
            _setCycleTime = Obj.setCycleTime;
            _ActualCycleTime = Obj._ActualCycleTime;
            _NotACKAlarmCount = Obj.NotACKAlarmCount;
            _ONAlarmCount = Obj.ONAlarmCount;
            _LastCycleTime = Obj.LastCycleTime;

            _UpdationDateTime = Obj.UpdationDateTime;
            _PlantStatus = Obj.PlantStatus;



            _ShiftDate = Obj.ShiftDate;
            _PartArea = Obj.PartArea;
            _Shift1PartArea = Obj.Shift1PartArea;
            _Shift2PartArea = Obj.Shift2PartArea;
            _Shift3PartArea = Obj.Shift3PartArea;

            //downtime 
            _AutoPreviousDowntimeShift = Obj.AutoPreviousDowntimeShift;
            _SemiPreviousDowntimeShift = Obj.SemiPreviousDowntimeShift;
            _ManualPreviousDowntimeShift = Obj.ManualPreviousDowntimeShift;
            _AutoLiveDowntimeShift = Obj.AutoLiveDowntimeShift;
            _SemiLiveDowntimeShift = Obj.SemiLiveDowntimeShift;
            _ManualLiveDowntimeShift = Obj.ManualLiveDowntimeShift;
            _MaintainanceLiveDowntimeShift = Obj.MaintainanceLiveDowntimeShift;
            _CompleteLiveDowntimeShift = Obj.CompleteLiveDowntimeShift;

            //_MaintenancePreviousDowntimeShift = Obj.MaintenancePreviousDowntimeShift;
            //_CompletePreviousDowntimeShift = Obj.CompletePreviousDowntimeShift;
            //_ShiftNo = Obj.ShiftNo;
            //_DownTime = Obj.DownTime;
            //_SemiDownTime = Obj.SemiDownTime;
            //_ManualDownTime = Obj.ManualDownTime;
            //_MaintenanceDownTime = Obj.MaintenanceDownTime;
            //_CompleteDownTime = Obj.CompleteDownTime;

            //_AutoTotalDownTime = Obj.AutoTotalDownTime;
            //_SemiTotalDownTime = Obj.SemiTotalDownTime;
            //_ManualTotalDownTime = Obj.ManualTotalDownTime;
            //_MaintenanceTotalDownTime = Obj.MaintenanceTotalDownTime;
            //_CompleteTotalDownTime = Obj.CompleteTotalDownTime;


            // zinc plating 
            //_ZincPlatingParameterName = Obj.ZincPlatingParameterName;
            //_ZincPlatingParameterValue = Obj.ZincPlatingParameterValue;
            _DegreasingTemp = Obj.DegreasingTemp;
            _DescalingTemp = Obj.DescalingTemp;
            _AnodicCleaningTemp = Obj.AnodicCleaningTemp;
            _AnodicCleaningCurrent = Obj.AnodicCleaningCurrent;
            _ZincPlatingTemp = Obj.ZincPlatingTemp;
            _PlatingTime = Obj.PlatingTime;
            _ZincPlatingCurrent = Obj.ZincPlatingCurrent;
            _TrivalentPassivationPH = Obj.TrivalentPassivationPH;
            _DMWaterConductivity = Obj.DMWaterConductivity;
            _TriPassivationTemp = Obj.TriPassivationTemp;
            _SealantTemp = Obj.SealantTemp;
            _SealerPH = Obj.SealerPH;
            _ConveyorOven = Obj.ConveyorOven;
            _OvenTime = Obj.OvenTime;

            //_HistoricalZincPlatingParameterName = Obj.HistoricalZincPlatingParameterName;
            //_HistoricalZincPlatingParameterValue = Obj.HistoricalZincPlatingParameterValue;

            //poka yoke
            //_PokaYokeZincPlatingParameterName = Obj.PokaYokeZincPlatingParameterName;
            //_PokaYokeZincPlatingParameterValue = Obj.PokaYokeZincPlatingParameterValue;
            _CED_Rectifier = Obj.CED_Rectifier;
            _Hot_Water_Temperature = Obj.Hot_Water_Temperature;
            _KOD_Temperature = Obj.KOD_Temperature;
            _Degreasing_Temperature = Obj.Degreasing_Temperature;
            _Phosphating_Temperature = Obj.Phosphating_Temperature;
            _CED_Temperature = Obj.CED_Temperature;
            _North_Zone_Temperature = Obj.North_Zone_Temperature;
            _South_Zone_Temperature = Obj.South_Zone_Temperature;
            _East_Zone_Temperature = Obj.East_Zone_Temperature;
            _West_Zone_Temperature = Obj.West_Zone_Temperature;
            _Heat_Exchanger_Temperature = Obj.Heat_Exchanger_Temperature;

            //concentration
            _CausticSodaConcentration = Obj.CausticSodaConcentration;
            _ZincConcentration = Obj.ZincConcentration;

            //ERP
            _ERPInwardQuantity = Obj.ERPInwardQuantity;
            _ERPRunningQuantity = Obj.ERPRunningQuantity;
            _ERPCompletedQuantity = Obj.ERPCompletedQuantity;
            _ERPBalancedQuantity = Obj.ERPBalancedQuantity;

            //h2D part quantity
            _Quantity = Obj.Quantity;

            //passivation summary
            _PassivationSilver = Obj.PassivationSilver;
            _PassivationYellow = Obj.PassivationYellow;
            _PassivationBlack = Obj.PassivationBlack;
            _PassivationZincIron = Obj.PassivationZincIron;
            _TotalRecord = Obj.Total_Records;

            #region pokayoke 

            //_OilSkimmer_2_Motor1 = Obj.OilSkimmer_2_Motor1;
            //_Oil_skimmer_2_Pump = Obj.Oil_skimmer_2_Pump;
            //_ScrubberPump = Obj.ScrubberPump;
            //_AlkalineZinc_21_24 = Obj.AlkalineZinc_21_24;
            //_AlkalineZinc_25_28 = Obj.AlkalineZinc_25_28;
            //_AlkalineZinc_29_34 = Obj.AlkalineZinc_29_34;
            //_AlkalineZinc_35_38 = Obj.AlkalineZinc_35_38;
            //_Passivation_2 = Obj.Passivation_2;
            //_TopCoat_1 = Obj.TopCoat_1;
            //_TopCoat_2 = Obj.TopCoat_2;
            //_TopCoat_3 = Obj.TopCoat_3;
            //_AlkalineZinc_21_28 = Obj.AlkalineZinc_21_28;
            //_AlkalineZinc_21_28 = Obj.AlkalineZinc_21_28;
            //_AlkalineZinc_21_28 = Obj.AlkalineZinc_21_28;
            //_AlkalineZinc_21_28 = Obj.AlkalineZinc_21_28;
            //_AlkalineZinc_29_34 = Obj.AlkalineZinc_29_34;
            //_AlkalineZinc_29_34 = Obj.AlkalineZinc_29_34;
            //_AlkalineZinc_29_34 = Obj.AlkalineZinc_29_34;
            //_AlkalineZinc_29_34 = Obj.AlkalineZinc_29_34;
            //_AlkalineZinc_35_38 = Obj.AlkalineZinc_35_38;
            //_AlkalineZinc_35_38 = Obj.AlkalineZinc_35_38;
            //_AlkalineZinc_35_38 = Obj.AlkalineZinc_35_38;
            //_AlkalineZinc_35_38 = Obj.AlkalineZinc_35_38;
            //_Nitric_Stn_44 = Obj.Nitric_Stn_44;
            //_Passivation_2_Stn_51 = Obj.Passivation_2_Stn_51;
            //_Passivation_2_Stn_51 = Obj.Passivation_2_Stn_51;
            //_Passivation_2_Stn_51 = Obj.Passivation_2_Stn_51;
            //_TopCoat_2_Stn_64 = Obj.TopCoat_2_Stn_64;
            //_TopCoat_2_Stn_64 = Obj.TopCoat_2_Stn_64;
            //_TopCoat_1_Stn_63 = Obj.TopCoat_1_Stn_63;
            //_TopCoat_1_Stn_63 = Obj.TopCoat_1_Stn_63;
            //_TopCoat_3_Stn_65 = Obj.TopCoat_3_Stn_65;
            //_TopCoat_3_Stn_65 = Obj.TopCoat_3_Stn_65;
            //_Oil_skimmer_2_Pump = Obj.Oil_skimmer_2_Pump;
            //_Anodic_1 = Obj.Anodic_1;
            //_Anodic_2 = Obj.Anodic_2;
            //_Anodic_3 = Obj.Anodic_3;
            //_AlZn_1 = Obj.AlZn_1;
            //_AlZn_2 = Obj.AlZn_2;
            //_AlZn_3 = Obj.AlZn_3;
            //_AlZn_4 = Obj.AlZn_4;
            //_AlZn_5 = Obj.AlZn_5;
            //_AlZn_6 = Obj.AlZn_6;
            //_AlZn_7 = Obj.AlZn_7;
            //_AlZn_8 = Obj.AlZn_8;
            //_AlZn_9 = Obj.AlZn_9;
            //_AlZn_10 = Obj.AlZn_10;
            //_AlZn_11 = Obj.AlZn_11;
            //_AlZn_12 = Obj.AlZn_12;
            //_AlZn_13 = Obj.AlZn_13;
            //_AlZn_14 = Obj.AlZn_14;
            //_AlZn_15 = Obj.AlZn_15;
            //_AlZn_16 = Obj.AlZn_16;
            //_AlZn_17 = Obj.AlZn_17;
            //_AlZn_18 = Obj.AlZn_18;
            Oil_skimmer_2_Motor_P1 = Obj.Oil_skimmer_2_Motor_P1;

            _Oil_skimmer_2_Pump_P2 = Obj.Oil_skimmer_2_Pump_P2;

            _ScrubberPump_P3 = Obj.ScrubberPump_P3;



            _FilterPump_6_P4 = Obj.FilterPump_6_P4;



            _FilterPump_7_P5 = Obj.FilterPump_7_P5;


            _FilterPump_8_P6 = Obj.FilterPump_8_P6;


            _FilterPump_9_P7 = Obj.FilterPump_9_P7;


            _FilterPump_13_P8 = Obj.FilterPump_13_P8;


            _FilterPump_15_P9 = Obj.FilterPump_15_P9;


            _FilterPump_16_P10 = Obj.FilterPump_16_P10;


            _FilterPump_17_P11 = Obj.FilterPump_17_P11;



            _DosingPump_1_P12 = Obj.DosingPump_1_P12;
            _DosingPump_P13 = Obj.DosingPump_P13;
            _DosingPump_3_P14 = Obj.DosingPump_3_P14;
            _DosingPump_4_P15 = Obj.DosingPump_4_P15;


            _DosingPump_5_P16 = Obj.DosingPump_5_P16;

            _DosingPump_6_P17 = Obj.DosingPump_6_P17;

            _DosingPump_7_P18 = Obj.DosingPump_7_P18;

            _DosingPump_8_P19 = Obj.DosingPump_8_P19;



            _DosingPump_9_P20 = Obj.DosingPump_9_P20;
            _DosingPump_10_P21 = Obj.DosingPump_10_P21;
            _DosingPump_11_P22 = Obj.DosingPump_11_P22;
            _DosingPump_12_P23 = Obj.DosingPump_12_P23;


            _DosingPump_13_P24 = Obj.DosingPump_13_P24;


            _DosingPump_17_P25 = Obj.DosingPump_17_P25;
            _DosingPump_18_P26 = Obj.DosingPump_18_P26;
            _DosingPump_19_P27 = Obj.DosingPump_19_P27;


            _DosingPump_20_P28 = Obj.DosingPump_20_P28;
            _DosingPump_21_P29 = Obj.DosingPump_21_P29;


            _DosingPump_22_P30 = Obj.DosingPump_22_P30;
            _DosingPump_23_P31 = Obj.DosingPump_23_P31;


            _DosingPump_24_P32 = Obj.DosingPump_24_P32;
            _DosingPump_25_P33 = Obj.DosingPump_25_P33;


            _Anodic_1_P34 = Obj.Anodic_1_P34;



            _Anodic_2_P35 = Obj.Anodic_2_P35;
            _Anodic_3_P36 = Obj.Anodic_3_P36;
            _AlZn_1_P37 = Obj.AlZn_1_P37;


            _AlZn_2_P38 = Obj.AlZn_2_P38;
            _AlZn_3_P39 = Obj.AlZn_3_P39;
            _AlZn_4_P40 = Obj.AlZn_4_P40;
            _AlZn_5_P41 = Obj.AlZn_5_P41;
            _AlZn_6_P42 = Obj.AlZn_6_P42;
            _AlZn_7_P43 = Obj.AlZn_7_P43;
            _AlZn_8_P44 = Obj.AlZn_8_P44;
            _AlZn_9_P45 = Obj.AlZn_9_P45;
            _AlZn_10_P46 = Obj.AlZn_10_P46;
            _AlZn_11_P47 = Obj.AlZn_11_P47;
            _AlZn_12_P48 = Obj.AlZn_12_P48;
            _AlZn_13_P49 = Obj.AlZn_13_P49;
            _AlZn_14_P50 = Obj.AlZn_14_P50;
            _AlZn_15_P51 = Obj.AlZn_15_P51;
            _AlZn_16_P52 = Obj.AlZn_16_P52;
            _AlZn_17_P53 = Obj.AlZn_17_P53;
            _AlZn_18_P54 = Obj.AlZn_18_P54;






            #endregion
        }

        #endregion

        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion
    }
}
