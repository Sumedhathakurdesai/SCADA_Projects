using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class RectifierEntity : INotifyPropertyChanged
    {
        #region Public/Private Property
        private string _RectifierName;
        public string RectifierName
        {
            get
            {
                return _RectifierName;
            }
            set
            {
                _RectifierName = value;
                OnPropertyChanged("RectifierName");
            }

        }
        private string _AutoManual;
        public string AutoManual
        {
            get
            {
                return _AutoManual;
            }
            set
            {
                _AutoManual = value;
                OnPropertyChanged("AutoManual");
            }

        }
        private string _ManualOnOff;
        public string ManualOnOff
        {
            get
            {
                return _ManualOnOff;
            }
            set
            {
                _ManualOnOff = value;
                OnPropertyChanged("ManualOnOff");
            }

        }
        private string _Calculated;
        public string Calculated
        {
            get
            {
                return _Calculated;
            }
            set
            {
                _Calculated = value;
                OnPropertyChanged("Calculated");
            }

        }
        private string _ManualCurrent;
        public string ManualCurrent
        {
            get
            {
                return _ManualCurrent;
            }
            set
            {
                _ManualCurrent = value;
                OnPropertyChanged("ManualCurrent");
            }

        }
        private string _AppliedCurrent;
        public string AppliedCurrent
        {
            get
            {
                return _AppliedCurrent;
            }
            set
            {
                _AppliedCurrent = value;
                OnPropertyChanged("AppliedCurrent");
            }
        }
        private string _ActualVoltage;
        public string ActualVoltage
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
        private string _ActualCurrent;
        public string ActualCurrent
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
        private string _OnOffStatus;
        public string OnOffStatus
        {
            get
            {
                return _OnOffStatus;
            }
            set
            {
                _OnOffStatus = value;
                OnPropertyChanged("OnOffStatus");
            }
        }
        private string _AlarmStatus;
        public string AlarmStatus
        {
            get
            {
                return _AlarmStatus;
            }
            set
            {
                _AlarmStatus = value;
                OnPropertyChanged("AlarmStatus");
            }
        }
        private string _AmpHr;
        public string AmpHr
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
        private string _ResetAmpHr;
        public string ResetAmpHr
        {
            get
            {
                return _ResetAmpHr;
            }
            set
            {
                _ResetAmpHr = value;
                OnPropertyChanged("ResetAmpHr");
            }
        }
        private string _HighSP;
        public string HighSP
        {
            get
            {
                return _HighSP;
            }
            set
            {
                _HighSP = value;
                OnPropertyChanged("HighSP");
            }
        }
        private string _LowSP;
        public string LowSP
        {
            get
            {
                return _LowSP;
            }
            set
            {
                _LowSP = value;
                OnPropertyChanged("LowSP");
            }
        }
        private string _RectifierNo;
        public string RectifierNo
        {
            get
            {
                return _RectifierNo;
            }
            set
            {
                _RectifierNo = value;
                OnPropertyChanged("RectifierNo");
            }
        }
        private string _CuAmpHr;
        public string CuAmpHr
        {
            get
            {
                return _CuAmpHr;
            }
            set
            {
                _CuAmpHr = value;
                OnPropertyChanged("CuAmpHr");
            }
        }
        #endregion
        #region Constructor

        public RectifierEntity()
        { }

        public RectifierEntity(RectifierEntity Obj)
        {
            _RectifierName = Obj.RectifierName;
            _Calculated = Obj.Calculated;
            _ManualOnOff = Obj.ManualOnOff;
            _AutoManual = Obj.AutoManual;
            _ManualCurrent = Obj.ManualCurrent;
            _AppliedCurrent = Obj.AppliedCurrent;
            _ActualCurrent = Obj.ActualCurrent;
            _ActualVoltage = Obj.ActualVoltage;
            _ActualVoltage = Obj.ActualVoltage;
            _OnOffStatus = Obj.OnOffStatus;
            _RectifierNo = Obj.RectifierNo;
            _AlarmStatus = Obj.AlarmStatus;
            _HighSP = Obj.HighSP;
            _LowSP = Obj.LowSP;
            _AmpHr = Obj.AmpHr;
            _ResetAmpHr = Obj.ResetAmpHr;
            _CuAmpHr = Obj.CuAmpHr;
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
