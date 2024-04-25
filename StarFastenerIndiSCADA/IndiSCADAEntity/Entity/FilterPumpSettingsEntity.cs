using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class FilterPumpSettingsEntity : INotifyPropertyChanged
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
        private string _Status;
        public string Status
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
        private string _TripStatus;
        public string TripStatus
        {
            get
            {
                return _TripStatus;
            }
            set
            {
                _TripStatus = value;
                OnPropertyChanged("TripStatus");
            }

        }
        private string _PumpOnOffID;
        public string PumpOnOffID
        {
            get
            {
                return _PumpOnOffID;
            }
            set
            {
                _PumpOnOffID = value;
                OnPropertyChanged("PumpOnOffID");
            }

        }
        #endregion
        #region Constructor

        public FilterPumpSettingsEntity()
        { }

        public FilterPumpSettingsEntity(FilterPumpSettingsEntity Obj)
        {
            _ParameterName = Obj.ParameterName;
            _ManualOnOff = Obj.ManualOnOff;
            _Status = Obj.Status;
            _TripStatus = Obj.TripStatus;
            _PumpOnOffID = Obj.PumpOnOffID;
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
