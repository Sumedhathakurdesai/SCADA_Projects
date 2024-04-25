using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class TopSpraySettingsEntity : INotifyPropertyChanged
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
        private string _ServiceOnOff;
        public string ServiceOnOff
        {
            get
            {
                return _ServiceOnOff;
            }
            set
            {
                _ServiceOnOff = value;
                OnPropertyChanged("ServiceOnOff");
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
        private string _TopSprayID;
        public string TopSprayID
        {
            get
            {
                return _TopSprayID;
            }
            set
            {
                _TopSprayID = value;
                OnPropertyChanged("TopSprayID");
            }

        }
        #endregion
        #region Constructor

        public TopSpraySettingsEntity()
        { }

        public TopSpraySettingsEntity(TopSpraySettingsEntity Obj)
        {
            _ParameterName = Obj.ParameterName;
            _ManualOnOff = Obj.ManualOnOff;
            _Status = Obj.Status;
            _TripStatus = Obj.TripStatus;
            _TopSprayID = Obj.TopSprayID;
            _ServiceOnOff = Obj.ServiceOnOff;
            _AutoManual = Obj.AutoManual;
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
