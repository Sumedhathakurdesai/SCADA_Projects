using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class LoadDipTimeEntity : INotifyPropertyChanged
    {
        #region Public/Private Property
        private string _LoadNumber;
        public string LoadNumber
        {
            get
            {
                return _LoadNumber;
            }
            set
            {
                _LoadNumber = value;
                OnPropertyChanged("LoadNumber");
            }

        }
        private int _LoadType;
        public int LoadType
        {
            get
            {
                return _LoadType;
            }
            set
            {
                _LoadType = value;
                OnPropertyChanged("LoadType");
            }
        }
        private DateTime _DipInTime;
        public DateTime DipInTime
        {
            get
            {
                return _DipInTime;
            }
            set
            {
                _DipInTime = value;
                OnPropertyChanged("DipdInTime");
            }
        }
        private DateTime _DipOutTime;
        public DateTime DipOutTime
        {
            get
            {
                return _DipOutTime;
            }
            set
            {
                _DipOutTime = value;
                OnPropertyChanged("DipOutTime");
            }
        }
        private int _DipOutShift;
        public int DipOutShift
        {
            get
            {
                return _DipOutShift;
            }
            set
            {
                _DipOutShift = value;
                OnPropertyChanged("DipOutShift");

            }
        }
        private int _DipInShift;
        public int DipInShift
        {
            get
            {
                return _DipInShift;
            }
            set
            {
                _DipInShift = value;
                OnPropertyChanged("DipInShift");

            }
        }
        private string _Operator;
        public string Operator
        {
            get
            {
                return _Operator;
            }
            set
            {
                _Operator = value;
                OnPropertyChanged("Operator");
            }
        }
        private bool _Status;
        public bool Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
                OnPropertyChanged("Status");
            }
        }
        private int _StationNo;
        public int StationNo
        {
            get
            {
                return _StationNo;
            }
            set
            {
                _StationNo = value;
                OnPropertyChanged("StationNo");

            }
        }
        private int _SetCurrent;
        public int SetCurrent
        {
            get
            {
                return _SetCurrent;
            }
            set
            {
                _SetCurrent = value;
                OnPropertyChanged("SetCurrent");

            }
        }

        private float _AmpHr;
        public float AmpHr
        {
            get
            {
                return _AmpHr;
            }
            set
            {
                _AmpHr = value;
                OnPropertyChanged("AmpHr");

            }
        }


        private int _TemperatureSetLow;
        public int TemperatureSetLow
        {
            get
            {
                return _TemperatureSetLow;
            }
            set
            {
                _TemperatureSetLow = value;
                OnPropertyChanged("TemperatureSetLow");

            }
        }
        private int _TemperatureSetHigh;
        public int TemperatureSetHigh
        {
            get
            {
                return _TemperatureSetHigh;
            }
            set
            {
                _TemperatureSetHigh = value;
                OnPropertyChanged("TemperatureSetHigh");

            }
        }
        private int _TemperatureActual;
        public int TemperatureActual
        {
            get
            {
                return _TemperatureActual;
            }
            set
            {
                _TemperatureActual = value;
                OnPropertyChanged("TemperatureActual");

            }
        }
        private int _ActualVoltage;
        public int ActualVoltage
        {
            get
            {
                return _ActualVoltage;
            }
            set
            {
                _ActualVoltage = value;
                OnPropertyChanged("ActualVoltage");

            }
        }
        private int _ActualCurrent;
        public int ActualCurrent
        {
            get
            {
                return _ActualCurrent;
            }
            set
            {
                _ActualCurrent = value;
                OnPropertyChanged("ActualCurrent");

            }
        }
        private float _ActualpH;
        public float ActualpH
        {
            get
            {
                return _ActualpH;
            }
            set
            {
                _ActualpH = value;
                OnPropertyChanged("ActualpH");

            }
        }
        private int _AvgCurrent;
        public int AvgCurrent
        {
            get
            {
                return _AvgCurrent;
            }
            set
            {
                _AvgCurrent = value;
                OnPropertyChanged("AvgCurrent");

            }
        }
        private bool _isStationDipTimeExceed;
        public bool isStationDipTimeExceed
        {
            get
            {
                return _isStationDipTimeExceed;
            }
            set
            {
                _isStationDipTimeExceed = value;
                OnPropertyChanged("isStationDipTimeExceed");
            }
        }

        #region out of range
        private int _ORTempLowSP;
        public int ORTempLowSP
        {
            get
            {
                return _ORTempLowSP;
            }
            set
            {
                _ORTempLowSP = value;
                OnPropertyChanged("ORTempLowSP");

            }
        }

        private int _ORTempHighSP;
        public int ORTempHighSP
        {
            get
            {
                return _ORTempHighSP;
            }
            set
            {
                _ORTempHighSP = value;
                OnPropertyChanged("ORTempHighSP");

            }
        }

        private int _ORTempAvg;
        public int ORTempAvg
        {
            get
            {
                return _ORTempAvg;
            }
            set
            {
                _ORTempAvg = value;
                OnPropertyChanged("ORTempAvg");

            }
        }

        private int _ORTempLowTime;
        public int ORTempLowTime
        {
            get
            {
                return _ORTempLowTime;
            }
            set
            {
                _ORTempLowTime = value;
                OnPropertyChanged("ORTempLowTime");

            }
        }

        private int _ORTempHighTime;
        public int ORTempHighTime
        {
            get
            {
                return _ORTempHighTime;
            }
            set
            {
                _ORTempHighTime = value;
                OnPropertyChanged("ORTempHighTime");

            }
        }

        private int _ORpHLowSP;
        public int ORpHLowSP
        {
            get
            {
                return _ORpHLowSP;
            }
            set
            {
                _ORpHLowSP = value;
                OnPropertyChanged("ORpHLowSP");

            }
        }

        private int _ORpHHighSP;
        public int ORpHHighSP
        {
            get
            {
                return _ORpHHighSP;
            }
            set
            {
                _ORpHHighSP = value;
                OnPropertyChanged("ORpHHighSP");

            }
        }

        private int _ORpHLowTime;
        public int ORpHLowTime
        {
            get
            {
                return _ORpHLowTime;
            }
            set
            {
                _ORpHLowTime = value;
                OnPropertyChanged("ORpHLowTime");

            }
        }

        private int _ORpHHighTime;
        public int ORpHHighTime
        {
            get
            {
                return _ORpHHighTime;
            }
            set
            {
                _ORpHHighTime = value;
                OnPropertyChanged("ORpHHighTime");

            }
        }

        private int _ORpHAvg;
        public int ORpHAvg
        {
            get
            {
                return _ORpHAvg;
            }
            set
            {
                _ORpHAvg = value;
                OnPropertyChanged("ORpHAvg");

            }
        }

        private int _ORCurrent;
        public int ORCurrent
        {
            get
            {
                return _ORCurrent;
            }
            set
            {
                _ORCurrent = value;
                OnPropertyChanged("ORCurrent");

            }
        }

        private int _ORDipTime;
        public int ORDipTime
        {
            get
            {
                return _ORDipTime;
            }
            set
            {
                _ORDipTime = value;
                OnPropertyChanged("ORDipTime");

            }
        }
        

        #endregion



        #endregion
        #region Constructor

        public LoadDipTimeEntity()
        { }

        public LoadDipTimeEntity(LoadDipTimeEntity Obj)
        {
            _LoadNumber = Obj.LoadNumber;
            _LoadType = Obj.LoadType;
            _DipInTime = Obj.DipInTime;
            _DipOutTime = Obj.DipOutTime;
            _DipOutShift = Obj.DipOutShift;
            _DipInShift = Obj.DipInShift;
            _Operator = Obj.Operator;
            _Status = Obj.Status;
            _StationNo = Obj.StationNo;
            _SetCurrent = Obj.SetCurrent;
            _AmpHr = Obj.AmpHr;
            _TemperatureSetLow = Obj.TemperatureSetLow;
            _TemperatureSetHigh = Obj.TemperatureSetHigh;
            _TemperatureActual = Obj.TemperatureActual;
            _ActualVoltage = Obj.ActualVoltage;
            _ActualCurrent = Obj.ActualCurrent;
            _ActualpH = Obj.ActualpH;
            _AvgCurrent = Obj.AvgCurrent;
            _isStationDipTimeExceed = Obj.isStationDipTimeExceed;

            _ORTempLowSP = Obj.ORTempLowSP;
            _ORTempHighSP = Obj.ORTempHighSP;
            _ORTempAvg = Obj.ORTempAvg;
            _ORTempLowTime = Obj.ORTempLowTime;
            _ORTempHighTime = Obj.ORTempHighTime;
            _ORpHLowSP = Obj.ORpHLowSP;
            _ORpHHighSP = Obj.ORpHHighSP;
            _ORpHLowTime = Obj.ORpHLowTime;
            _ORpHHighTime = Obj.ORpHHighTime;
            _ORpHAvg = Obj.ORpHAvg;
            _ORCurrent = Obj.ORCurrent;
            _ORDipTime = Obj.ORDipTime; 
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
