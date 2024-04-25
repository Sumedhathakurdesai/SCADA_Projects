using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class DosingOperationDataEntity : INotifyPropertyChanged
    {
        #region "Public/Private Property"
        private double _SetQuantity;
        public double SetQuantity
        {
            get
            {
                return _SetQuantity;
            }
            set
            {
                _SetQuantity = value;
                OnPropertyChanged("SetQuantity");

            }
        }
        private double _SetFlowRate;
        public double SetFlowRate
        {
            get
            {
                return _SetFlowRate;
            }
            set
            {
                _SetFlowRate = value;
                OnPropertyChanged("SetFlowRate");

            }
        }
        private int _SetTime;
        public int SetTime
        {
            get
            {
                return _SetTime;
            }
            set
            {
                _SetTime = value;
                OnPropertyChanged("SetTime");

            }
        }
        private string _PumpNo;
        public string PumpNo
        {
            get
            {
                return _PumpNo;
            }
            set
            {
                _PumpNo = value;
                OnPropertyChanged("PumpNo");

            }
        }
        private int _ID;
        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
                OnPropertyChanged("ID");

            }
        }
        private string _ModeOfOperation;
        public string ModeOfOperation
        {
            get
            {
                return _ModeOfOperation;
            }
            set
            {
                _ModeOfOperation = value;
                OnPropertyChanged("ModeOfOperation");
            }

        }
        private string _ChemicalName;
        public string ChemicalName
        {
            get
            {
                return _ChemicalName;
            }
            set
            {
                _ChemicalName = value;
                OnPropertyChanged("ChemicalName");
            }
        }
        private DateTime _DosingDateTime;
        public DateTime DosingDateTime
        {
            get
            {
                return _DosingDateTime;
            }
            set
            {
                _DosingDateTime = value;
                OnPropertyChanged("DosingDateTime");
            }
        }
        #endregion 
        #region Constructor

        public DosingOperationDataEntity()
        { }

        public DosingOperationDataEntity(DosingOperationDataEntity Obj)
        {
            _DosingDateTime = Obj.DosingDateTime;
            _ChemicalName = Obj.ChemicalName;
            _ModeOfOperation = Obj.ModeOfOperation;
            _ID = Obj.ID;
            _PumpNo = Obj.PumpNo;
            _SetQuantity = Obj.SetQuantity;
            _SetFlowRate = Obj.SetFlowRate;
            _SetTime = Obj.SetTime;
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
