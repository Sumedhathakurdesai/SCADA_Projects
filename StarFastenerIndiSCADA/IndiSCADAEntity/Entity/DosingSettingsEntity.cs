using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class DosingSettingsEntity: INotifyPropertyChanged
    {
        #region "Public/Private Property"
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
        private string _ChemicalName1;
        public string ChemicalName1
        {
            get
            {
                return _ChemicalName1;
            }
            set
            {
                _ChemicalName1 = value;
                OnPropertyChanged("ChemicalName1");

            }
        }
        private string _ChemicalName2;
        public string ChemicalName2
        {
            get
            {
                return _ChemicalName2;
            }
            set
            {
                _ChemicalName2 = value;
                OnPropertyChanged("ChemicalName2");

            }
        }
        private string _ChemicalName3;
        public string ChemicalName3
        {
            get
            {
                return _ChemicalName3;
            }
            set
            {
                _ChemicalName3 = value;
                OnPropertyChanged("ChemicalName3");

            }
        }
        private string _ChemicalName4;
        public string ChemicalName4
        {
            get
            {
                return _ChemicalName4;
            }
            set
            {
                _ChemicalName4 = value;
                OnPropertyChanged("ChemicalName4");

            }
        }
        private string _ChemicalName5;
        public string ChemicalName5
        {
            get
            {
                return _ChemicalName5;
            }
            set
            {
                _ChemicalName5 = value;
                OnPropertyChanged("ChemicalName5");

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
        private string _ManualONOFF;
        public string ManualONOFF
        {
            get
            {
                return _ManualONOFF;
            }
            set
            {
                _ManualONOFF = value;
                OnPropertyChanged("ManualONOFF");

            }
        }
        private string _FlowRateTimerBased;
        public string FlowRateTimerBased
        {
            get
            {
                return _FlowRateTimerBased;
            }
            set
            {
                _FlowRateTimerBased = value;
                OnPropertyChanged("FlowRateTimerBased");

            }
        }
        private string _Quantity;
        public string Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                _Quantity = value;
                OnPropertyChanged("Quantity");

            }
        }
        private string _Time;
        public string SetTime
        {
            get
            {
                return _Time;
            }
            set
            {
                _Time = value;
                OnPropertyChanged("SetTime");

            }
        }
        private string _SetAmp;
        public string SetAmp
        {
            get
            {
                return _SetAmp;
            }
            set
            {
                _SetAmp = value;
                OnPropertyChanged("SetAmp");

            }
        }
        private string _ActualAmp;
        public string ActualAmp
        {
            get
            {
                return _ActualAmp;
            }
            set
            {
                _ActualAmp = value;
                OnPropertyChanged("ActualAmp");

            }
        }
        private string _setPH;
        public string setPH
        {
            get
            {
                return _setPH;
            }
            set
            {
                _setPH = value;
                OnPropertyChanged("setPH");

            }
        }
        private string _ActualpH;
        public string ActualpH
        {
            get
            {
                return _ActualpH;
            }
            set
            {
                _ActualpH = value;
                OnPropertyChanged("ActualpH");

            }
        }
        

        private string _RemainingTime;
        public string RemainingTime
        {
            get
            {
                return _RemainingTime;
            }
            set
            {
                _RemainingTime = value;
                OnPropertyChanged("RemainingTime");

            }
        }
        private string _CumulativeAmphr;
        public string CumulativeAmphr
        {
            get
            {
                return _CumulativeAmphr;
            }
            set
            {
                _CumulativeAmphr = value;
                OnPropertyChanged("CumulativeAmphr");

            }
        }
        
        private string _FlowRate;
        public string FlowRate
        {
            get
            {
                return _FlowRate;
            }
            set
            {
                _FlowRate = value;
                OnPropertyChanged("FlowRate");

            }
        }
        private string _ONOFFStatus;
        public string ONOFFStatus
        {
            get
            {
                return _ONOFFStatus;
            }
            set
            {
                _ONOFFStatus = value;
                OnPropertyChanged("ONOFFStatus");

            }
        }
        private string _DosingID;
        public string DosingID
        {
            get
            {
                return _DosingID;
            }
            set
            {
                _DosingID = value;
                OnPropertyChanged("DosingID");

            }
        }
        #endregion
        #region Constructor
        public DosingSettingsEntity()
        { }
        public DosingSettingsEntity(DosingSettingsEntity Obj)
        {
            _StationName = Obj.StationName;
            _AutoManual = Obj.AutoManual;
            _ManualONOFF = Obj.ManualONOFF;
            _FlowRateTimerBased = Obj.FlowRateTimerBased;
            _Quantity = Obj.Quantity;
            _FlowRate = Obj.FlowRate;
            _Time = Obj.SetTime;
            _SetAmp = Obj.SetAmp;
            _setPH = Obj.setPH;
            _ActualpH = Obj.ActualpH;
            _ActualAmp = Obj.ActualAmp;
            _RemainingTime = Obj.RemainingTime; 
            _ONOFFStatus = Obj.ONOFFStatus;
            _DosingID = Obj.DosingID;
            _CumulativeAmphr = Obj.CumulativeAmphr;
            _ChemicalName = Obj.ChemicalName;
            _ChemicalName1 = Obj.ChemicalName1;
            _ChemicalName2 = Obj.ChemicalName2;
            _ChemicalName3 = Obj.ChemicalName3;
            _ChemicalName4 = Obj.ChemicalName4;
            _ChemicalName5 = Obj.ChemicalName5; 
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
