using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
   public class SystemStatusEntity : INotifyPropertyChanged
    {
        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion
        
        #region Public/Private Property
        private string _RunningStatus;
        public string RunningStatus
        {
            get
            {
                return _RunningStatus;
            }
            set
            {
                _RunningStatus = value;
                OnPropertyChanged("RunningStatus");
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
        private string _Value;
        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
                OnPropertyChanged("Value");
            }
        }

        #endregion

        #region Constructor
        public SystemStatusEntity()
        { }
        public SystemStatusEntity(SystemStatusEntity Obj)
        {
            try
            {
                _RunningStatus = Obj.RunningStatus;
                _TripStatus = Obj.TripStatus;
                _ParameterName = Obj.ParameterName;
                _Value = Obj.Value;
            }
            catch(Exception ex)
            { }
        }
        #endregion
    }
}
