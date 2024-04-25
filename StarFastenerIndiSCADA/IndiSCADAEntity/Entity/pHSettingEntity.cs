using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class pHSettingEntity : INotifyPropertyChanged
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
        private string _ActualpH;
        public string ActualpH
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
        
        private string _pHID;
        public string pHID
        {
            get
            {
                return _pHID;
            }
            set
            {
                _pHID = value;
                OnPropertyChanged("pHID");
            }

        }
        #endregion
        #region Constructor

        public pHSettingEntity()
        { }

        public pHSettingEntity(pHSettingEntity Obj)
        {
            _ParameterName = Obj.ParameterName;
            _ActualpH = Obj.ActualpH;
            _LowSP = Obj.LowSP;
            _HighSP = Obj.HighSP;            
            _pHID = Obj.pHID;
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
