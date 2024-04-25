using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class OutOfRangeSettingsDipTime : INotifyPropertyChanged
    {
        #region Public/Private Property
        private string _ProgramNo;
        public string ProgramNo
        {
            get
            {
                return _ProgramNo;
            }
            set
            {
                _ProgramNo = value;
                OnPropertyChanged("ProgramNo");
            }
        }
        private string _ParameterName;
        public string ParameterName
        {
            get
            {
                return _ParameterName;
            }
            set
            {
                _ParameterName = value;
                OnPropertyChanged("ParameterName");
            }

        }
        private string _LowSPDipTime;
        public string LowSPDipTime
        {
            get
            {
                return _LowSPDipTime;
            }
            set
            {
                _LowSPDipTime = value;
                OnPropertyChanged("LowSPDipTime");
            }

        }
        private string _HighSPDipTime;
        public string HighSPDipTime
        {
            get
            {
                return _HighSPDipTime;
            }
            set
            {
                _HighSPDipTime = value;
                OnPropertyChanged("HighSPDipTime");
            }

        }
        private string _LowBypass;
        public string LowBypass
        {
            get
            {
                return _LowBypass;
            }
            set
            {
                _LowBypass = value;
                OnPropertyChanged("LowBypass");
            }

        }
        private string _HighBypass;
        public string HighBypass
        {
            get
            {
                return _HighBypass;
            }
            set
            {
                _HighBypass = value;
                OnPropertyChanged("HighBypass");
            }

        }
        private int _DipTimeID;
        public int DipTimeID
        {
            get
            {
                return _DipTimeID;
            }
            set
            {
                _DipTimeID = value;
                OnPropertyChanged("DipTimeID");
            }

        }
        private String _StationNO;
        public String StationNO
        {
            get
            {
                return _StationNO;
            }
            set
            {
                _StationNO = value;
                OnPropertyChanged("StationNO");
            }

        }
        #endregion
        #region Constructor

        public OutOfRangeSettingsDipTime()
        { }

        public OutOfRangeSettingsDipTime(OutOfRangeSettingsDipTime Obj)
        {
            _ParameterName = Obj._ParameterName;
            _DipTimeID = Obj.DipTimeID;
            _HighBypass = Obj.HighBypass;
            _LowBypass = Obj.LowBypass;
            _HighSPDipTime = Obj.HighSPDipTime;
            _LowSPDipTime = Obj.LowSPDipTime;
            _ProgramNo = Obj.ProgramNo;
            _StationNO = Obj.StationNO;
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
