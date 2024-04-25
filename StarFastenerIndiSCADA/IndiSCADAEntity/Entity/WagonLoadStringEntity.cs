using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class WagonLoadStringEntity : INotifyPropertyChanged
    {
        #region Public/Private Property
        private string _WagonNumber;
        public string WagonNumber
        {
            get
            {
                return _WagonNumber;
            }
            set
            {
                _WagonNumber = value;
                OnPropertyChanged("WagonNumber");
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

        private string _GetPut;
        public string GetPut
        {
            get
            {
                return _GetPut;
            }
            set
            {
                _GetPut = value;
                OnPropertyChanged("GetPut");
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
        private string _Year;
        public string Year
        {
            get
            {
                return _Year;
            }
            set
            {
                _Year = value;
                OnPropertyChanged("Year");
            }

        }
        private string _Month;
        public string Month
        {
            get
            {
                return _Month;
            }
            set
            {
                _Month = value;
                OnPropertyChanged("Month");
            }
        }
        private string _Day;
        public string Day
        {
            get
            {
                return _Day;
            }
            set
            {
                _Day = value;
                OnPropertyChanged("Day");
            }
        }
        private string _Hour;
        public string Hour
        {
            get
            {
                return _Hour;
            }
            set
            {
                _Hour = value;
                OnPropertyChanged("Hour");
            }
        }
        private string _Minutes;
        public string Minutes
        {
            get
            {
                return _Minutes;
            }
            set
            {
                _Minutes = value;
                OnPropertyChanged("Minutes");
            }
        }
        private string _Seconds;
        public string Seconds
        {
            get
            {
                return _Seconds;
            }
            set
            {
                _Seconds = value;
                OnPropertyChanged("Seconds");
            }
        }
        private string _MMDD;
        public string MMDD
        {
            get
            {
                return _MMDD;
            }
            set
            {
                _MMDD = value;
                OnPropertyChanged("MMDD");
            }
        }
        private bool _isLoadNoValid;
        public bool isLoadNoValid
        {
            get
            {
                return _isLoadNoValid;
            }
            set
            {
                _isLoadNoValid = value;
                OnPropertyChanged("isLoadNoValid");
            }
        }
        private bool _isMMDDValid;
        public bool isMMDDValid
        {
            get
            {
                return _isMMDDValid;
            }
            set
            {
                _isMMDDValid = value;
                OnPropertyChanged("isMMDDValid");
            }
        }
        #endregion
        #region Constructor

        public WagonLoadStringEntity()
        { }

        public WagonLoadStringEntity(WagonLoadStringEntity Obj)
        {
            _WagonNumber= Obj.WagonNumber;
            _GetPut = Obj.GetPut;
            _StationNo = Obj.StationNo;            
            _LoadType = Obj.LoadType;
            _LoadNumber = Obj.LoadNumber;
            _Year = Obj.Year;
            _Month = Obj.Month;
            _Day = Obj.Day;
            _Hour = Obj.Hour;
            _Minutes = Obj.Minutes;
            _Seconds = Obj.Seconds;
            _MMDD = Obj.MMDD;
            _isLoadNoValid = Obj.isLoadNoValid;
            _isMMDDValid = Obj.isMMDDValid;
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
