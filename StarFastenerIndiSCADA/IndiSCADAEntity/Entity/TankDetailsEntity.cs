using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class TankDetailsEntity : INotifyPropertyChanged
    {
        #region Public/Private Property
        private bool _SetLoadTypeReadOnly;
        public bool SetLoadTypeReadOnly
        {
            get
            {
                return _SetLoadTypeReadOnly;
            }
            set
            {
                _SetLoadTypeReadOnly = value;
                OnPropertyChanged("SetLoadTypeReadOnly");
            }
        }
        private bool _SetLoadNoReadOnly;
        public bool SetLoadNoReadOnly
        {
            get
            {
                return _SetLoadNoReadOnly;
            }
            set
            {
                _SetLoadNoReadOnly = value;
                OnPropertyChanged("SetLoadNoReadOnly");
            }
        }

        private string _StationName;
        public string StationName
        {
            get
            {
                return _StationName;
            }
            set
            {
                _StationName = value;
                OnPropertyChanged("StationName");
            }

        }
        private string _StationNo;
        public string StationNo
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
        private string _MECLNumber;
        public string MECLNumber
        {
            get
            {
                return _MECLNumber;
            }
            set
            {
                _MECLNumber = value;
                OnPropertyChanged("MECLNumber");
            }

        }

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
        private string _LoadType;
        public string LoadType
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
        private string _Duration;
        public string Duration
        {
            get
            {
                return _Duration;
            }
            set
            {
                _Duration = value;
                OnPropertyChanged("Duration");
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
        private string _pH;
        public string pH
        {
            get
            {
                return _pH;
            }
            set
            {
                _pH = value;
                OnPropertyChanged("pH");
            }
        }
        private string _StationID;
        public string StationID
        {
            get
            {
                return _StationID;
            }
            set
            {
                _StationID = value;
                OnPropertyChanged("StationID");
            }
        }
        #endregion
        #region Constructor

        public TankDetailsEntity()
        { }

        public TankDetailsEntity(TankDetailsEntity Obj)
        {
            _StationNo = Obj.StationNo;
            _StationName = Obj.StationName;
            _LoadType = Obj.LoadType;
            _LoadNumber = Obj.LoadNumber;
            _PartName = Obj.PartName;
            _MECLNumber = Obj.MECLNumber;
            _Duration = Obj.Duration;
            _ActualTemperature = Obj.ActualTemperature;
            _ActualCurrent = Obj.ActualCurrent;
            _ActualVoltage = Obj.ActualVoltage;
            _pH = Obj.pH;
            _SetLoadTypeReadOnly = Obj.SetLoadTypeReadOnly;
            _SetLoadNoReadOnly = Obj.SetLoadNoReadOnly;
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
