using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class TemperatureSettingEntity : INotifyPropertyChanged
    {
        #region Public/Private Property
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
        private string _ActualTemperature;
        public string ActualTemperature
        {
            get
            {
                return _ActualTemperature;
            }
            set
            {
                _ActualTemperature = value;
                OnPropertyChanged("ActualTemperature");
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
        private string _ActualSP;
        public string ActualSP
        {
            get
            {
                return _ActualSP;
            }
            set
            {
                _ActualSP = value;
                OnPropertyChanged("ActualSP");
            }

        }
        private string _TemperatureID;
        public string TemperatureID
        {
            get
            {
                return _TemperatureID;
            }
            set
            {
                _TemperatureID = value;
                OnPropertyChanged("TemperatureID");
            }

        }
        #endregion
        #region Constructor

        public TemperatureSettingEntity()
        { }

        public TemperatureSettingEntity(TemperatureSettingEntity Obj)
        {
            _ParameterName = Obj.ParameterName;
            _ActualTemperature = Obj.ActualTemperature;
            _LowSP = Obj.LowSP;
            _HighSP = Obj.HighSP;
            _ActualSP = Obj.ActualSP;
            _TemperatureID = Obj.TemperatureID;
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
