using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
   public partial  class AlarmMasterEntity : INotifyPropertyChanged
    {
        #region Public/Private Property
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
                OnPropertyChanged("UserName");
            }

        }
        private string _AlarmText;
        public string AlarmText
        {
            get
            {
                return _AlarmText;
            }
            set
            {
                _AlarmText = value;
                OnPropertyChanged("AlarmText");
            }
        }
        private string _AlarmHelp;
        public string AlarmHelp
        {
            get
            {
                return _AlarmHelp;
            }
            set
            {
                _AlarmHelp = value;
                OnPropertyChanged("AlarmHelp");
            }
        }
        private string _AlarmGroup;
        public string AlarmGroup
        {
            get
            {
                return _AlarmGroup;
            }
            set
            {
                _AlarmGroup = value;
                OnPropertyChanged("AlarmGroup");

            }
        }

        private string _AlarmPriority;
        public string AlarmPriority
        {
            get
            {
                return _AlarmPriority;
            }
            set
            {
                _AlarmPriority = value;
                OnPropertyChanged("AlarmPriority");
            }
        }
        private int _AlarmSerialNumber;
        public int AlarmSerialNumber
        {
            get
            {
                return _AlarmSerialNumber;
            }
            set
            {
                _AlarmSerialNumber = value;
                OnPropertyChanged("AlarmSerialNumber");

            }
        }
        private bool _isACK;
        public bool isACK
        {
            get
            {
                return _isACK;
            }
            set
            {
                _isACK = value;
                OnPropertyChanged("isACK");
            }
        }
        private bool _isON;
        public bool isON
        {
            get
            {
                return _isON;
            }
            set
            {
                _isON = value;
                OnPropertyChanged("isON");
            }
        }
        private bool _isOFF;
        public bool isOFF
        {
            get
            {
                return _isOFF;
            }
            set
            {
                _isOFF = value;
                OnPropertyChanged("isOFF");
            }
        }
        private bool _CausesDownTime;
        public bool CausesDownTime
        {
            get
            {
                return _CausesDownTime;
            }
            set
            {
                _CausesDownTime = value;
                OnPropertyChanged("CausesDownTime");
            }
        }

        #endregion
        #region Constructor

        public AlarmMasterEntity()
        { }

        public AlarmMasterEntity(AlarmMasterEntity Obj)
        {
            _AlarmName = Obj.AlarmName;
            _AlarmText = Obj.AlarmText;
            _AlarmHelp = Obj.AlarmHelp;
            _AlarmGroup = Obj.AlarmGroup;
            _AlarmPriority = Obj.AlarmPriority;
            _AlarmSerialNumber = Obj.AlarmSerialNumber;
            _isON = Obj.isON;
            _isOFF = Obj.isOFF;
            _isACK = Obj.isACK;
            _CausesDownTime = Obj.CausesDownTime;
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
