using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class BaseBarrelMotorEntity : INotifyPropertyChanged
    {
        #region "Public/Private Property"
        private string _BaseBarrelMotorName;
        public string BaseBarrelMotorName
        {
            get
            {
                return _BaseBarrelMotorName;
            }
            set
            {
                _BaseBarrelMotorName = value;
                OnPropertyChanged("BaseBarrelMotorName");

            }
        }

        private string _BaseBarrelMotorID;
        public string BaseBarrelMotorID
        {
            get
            {
                return _BaseBarrelMotorID;
            }
            set
            {
                _BaseBarrelMotorID = value;
                OnPropertyChanged("BaseBarrelMotorID");

            }
        }

        private string _BaseBarrelMotorOnOFF;
        public string BaseBarrelMotorOnOFF
        {
            get
            {
                return _BaseBarrelMotorOnOFF;
            }
            set
            {
                _BaseBarrelMotorOnOFF = value;
                OnPropertyChanged("BaseBarrelMotorOnOFF");

            }
        }
        private string _BaseBarrelMotorTrip;
        public string BaseBarrelMotorTrip
        {
            get
            {
                return _BaseBarrelMotorTrip;
            }
            set
            {
                _BaseBarrelMotorTrip = value;
                OnPropertyChanged("BaseBarrelMotorTrip");

            }
        }
        private string _BaseBarrelMotorStatus;
        public string BaseBarrelMotorStatus
        {
            get
            {
                return _BaseBarrelMotorStatus;
            }
            set
            {
                _BaseBarrelMotorStatus = value;
                OnPropertyChanged("BaseBarrelMotorStatus");

            }
        }
        #endregion

        #region Constructor
        public BaseBarrelMotorEntity()
        { }
        public BaseBarrelMotorEntity(BaseBarrelMotorEntity Obj)
        {
            _BaseBarrelMotorID = Obj.BaseBarrelMotorID;
            _BaseBarrelMotorName = Obj.BaseBarrelMotorName;
            _BaseBarrelMotorOnOFF = Obj.BaseBarrelMotorOnOFF;
            _BaseBarrelMotorTrip = Obj.BaseBarrelMotorTrip;
            _BaseBarrelMotorStatus = Obj.BaseBarrelMotorStatus;
        }
       #endregion
        
        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
new PropertyChangedEventArgs(PropertyName));
        }
        #endregion
    }

}
