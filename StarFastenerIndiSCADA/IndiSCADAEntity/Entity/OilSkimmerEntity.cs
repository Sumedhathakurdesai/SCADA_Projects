using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class OilSkimmerEntity : INotifyPropertyChanged
    {
        #region "Public/Private Property"
        private string _OilSkimmerMotorName;
        public string OilSkimmerMotorName
        {
            get
            {
                return _OilSkimmerMotorName;
            }
            set
            {
                _OilSkimmerMotorName = value;
                OnPropertyChanged("OilSkimmerMotorName");

            }
        }

        private string _OilSkimmerMotorID;
        public string OilSkimmerMotorID
        {
            get
            {
                return _OilSkimmerMotorID;
            }
            set
            {
                _OilSkimmerMotorID = value;
                OnPropertyChanged("OilSkimmerMotorID");

            }
        }

        private string _BaseOilSkimmerOnOFF;
        public string BaseOilSkimmerOnOFF
        {
            get
            {
                return _BaseOilSkimmerOnOFF;
            }
            set
            {
                _BaseOilSkimmerOnOFF = value;
                OnPropertyChanged("BaseOilSkimmerOnOFF");

            }
        }

        private string _BaseOilSkimmerTrip;
        public string BaseOilSkimmerTrip
        {
            get
            {
                return _BaseOilSkimmerTrip;
            }
            set
            {
                _BaseOilSkimmerTrip = value;
                OnPropertyChanged("BaseOilSkimmerTrip");

            }
        }

        private string _BaseOilSkimmerStatus;
        public string BaseOilSkimmerStatus
        {
            get
            {
                return _BaseOilSkimmerStatus;
            }
            set
            {
                _BaseOilSkimmerStatus = value;
                OnPropertyChanged("BaseOilSkimmerStatus");

            }
        }
        #endregion

        #region Constructor
        public OilSkimmerEntity()
        { }
        public OilSkimmerEntity(OilSkimmerEntity Obj)
        {
            _OilSkimmerMotorID = Obj.OilSkimmerMotorID;
            _OilSkimmerMotorName = Obj.OilSkimmerMotorName;
            _BaseOilSkimmerOnOFF = Obj.BaseOilSkimmerOnOFF;
            _BaseOilSkimmerTrip = Obj.BaseOilSkimmerTrip;
            _BaseOilSkimmerStatus = Obj.BaseOilSkimmerStatus;
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
