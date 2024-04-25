using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace IndiSCADAEntity.Entity
{
    public partial class ChemicalNameMasterEntity : INotifyPropertyChanged
    {

        #region Constructor

        public ChemicalNameMasterEntity()
        { }

        public ChemicalNameMasterEntity(ChemicalNameMasterEntity Obj)
        {
            _PumpNumber = Obj.PumpNumber;
            _PumpName = Obj.PumpName;
            _ChemicalName = Obj.ChemicalName;
            _ChemicalPercentage = Obj.ChemicalPercentage;

        }

        #endregion
        #region "Public/Private Property"
        private string _ChemicalNo;
        public string ChemicalNo
        {
            get
            {
                return _ChemicalNo;
            }
            set
            {
                _ChemicalNo = value;
                OnPropertyChanged("ChemicalNo");
            }
        }

        private string _PumpNumber;
        public string PumpNumber
        {
            get
            {
                return _PumpNumber;
            }
            set
            {
                _PumpNumber = value;
                OnPropertyChanged("PumpNumber");

            }
        }

        private string _PumpName;
        public string PumpName
        {
            get
            {
                return _PumpName;
            }
            set
            {
                _PumpName = value;
                OnPropertyChanged("PumpName");

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
                OnPropertyChanged("_ChemicalName");

            }
        }

        private string _ChemicalPercentage;
        public string ChemicalPercentage
        {
            get
            {
                return _ChemicalPercentage;
            }
            set
            {
                _ChemicalPercentage = value;
                OnPropertyChanged("ChemicalPercentage");

            }
        }
        private bool _isChemicalPercentComplete;
        public bool isChemicalPercentComplete
        {
            get
            {
                return _isChemicalPercentComplete;
            }
            set
            {
                _isChemicalPercentComplete = value;
                OnPropertyChanged("isChemicalPercentComplete");
            }
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
