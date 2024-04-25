using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class AlarmDataEntity : INotifyPropertyChanged
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
                OnPropertyChanged("AlarmName");
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
        private string _AlarmCondition;
        public string AlarmCondition
        {
            get
            {
                return _AlarmCondition;
            }
            set
            {
                _AlarmCondition = value;
                OnPropertyChanged("AlarmCondition");
            }
        }
        private string _AlarmDuration;
        public string AlarmDuration
        {
            get
            {
                return _AlarmDuration;
            }
            set
            {
                _AlarmDuration = value;
                OnPropertyChanged("AlarmDuration");

            }
        }
        private string _AlarmType;
        public string AlarmType
        {
            get
            {
                return _AlarmType;
            }
            set
            {
                _AlarmType = value;
                OnPropertyChanged("AlarmType");
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
        private int _AlarmID;
        public int AlarmID
        {
            get
            {
                return _AlarmID;
            }
            set
            {
                _AlarmID = value;
                OnPropertyChanged("AlarmID");

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
        private DateTime _AlarmDateTime;
        public DateTime AlarmDateTime
        {
            get
            {
                return _AlarmDateTime;
            }
            set
            {
                _AlarmDateTime = value;
                OnPropertyChanged("AlarmDateTime");
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

        #endregion
        #region Constructor

        public AlarmDataEntity()
        { }

        public AlarmDataEntity(AlarmDataEntity Obj)
        {
            _AlarmName = Obj.AlarmName;
            _AlarmText = Obj.AlarmText;
            _AlarmCondition = Obj.AlarmCondition;
            _AlarmDuration = Obj.AlarmDuration;
            _AlarmPriority = Obj.AlarmPriority;
            _AlarmID = Obj.AlarmID;
            _isOFF = Obj.isOFF;
            _isON = Obj.isON;
            _isACK = Obj.isACK;
            _CausesDownTime = Obj.CausesDownTime;
            _AlarmType = Obj.AlarmType;
            _AlarmDateTime = Obj.AlarmDateTime;
            _AlarmGroup = Obj.AlarmGroup;
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
