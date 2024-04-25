using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IndiSCADAEntity.Entity
{
    public partial class OutOfRangesSettingsEntityTemp:INotifyPropertyChanged
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
        private string _SetPointTemp;
        public string SetPointTemp
        {
            get
            {
                return _SetPointTemp;
            }
            set
            {
                _SetPointTemp = value;
                OnPropertyChanged("SetPointTemp");
            }

        }
        private string _LowLevelTemp;
        public string LowLevelTemp
        {
            get
            {
                return _LowLevelTemp;
            }
            set
            {
                _LowLevelTemp = value;
                OnPropertyChanged("LowLevelTemp");
            }

        }
        private string _HighLevelTemp;
        public string HighLevelTemp
        {
            get
            {
                return _HighLevelTemp;
            }
            set
            {
                _HighLevelTemp = value;
                OnPropertyChanged("HighLevelTemp");
            }

        }
        private string _LowBypassTemp;
        public string LowBypassTemp
        {
            get
            {
                return _LowBypassTemp;
            }
            set
            {
                _LowBypassTemp = value;
                OnPropertyChanged("LowBypassTemp");
            }

        }
        private string _HighBypassTemp;
        public string HighBypassTemp
        {
            get
            {
                return _HighBypassTemp;
            }
            set
            {
                _HighBypassTemp = value;
                OnPropertyChanged("HighBypassTemp");
            }

        }
        private string _LowSPTemp;
        public string LowSPTemp
        {
            get
            {
                return _LowSPTemp;
            }
            set
            {
                _LowSPTemp = value;
                OnPropertyChanged("LowSPTemp");
            }

        }
        private string _HighSPTemp;
        public string HighSPTemp
        {
            get
            {
                return _HighSPTemp;
            }
            set
            {
                _HighSPTemp = value;
                OnPropertyChanged("HighSPTemp");
            }

        }
        private string _DelayTemp;
        public string DelayTemp
        {
            get
            {
                return _DelayTemp;
            }
            set
            {
                _DelayTemp = value;
                OnPropertyChanged("DelayTemp");
            }

        }
        private string _LowCountTemp;
        public string LowCountTemp
        {
            get
            {
                return _LowCountTemp;
            }
            set
            {
                _LowCountTemp = value;
                OnPropertyChanged("LowCountTemp");
            }

        }
        private string _LowTimeTemp;
        public string LowTimeTemp
        {
            get
            {
                return _LowTimeTemp;
            }
            set
            {
                _LowTimeTemp = value;
                OnPropertyChanged("LowTimeTemp");
            }

        }
        private string _HighCountTemp;
        public string HighCountTemp
        {
            get
            {
                return _HighCountTemp;
            }
            set
            {
                _HighCountTemp = value;
                OnPropertyChanged("HighCountTemp");
            }

        }
        private string _HighTimeTemp;
        public string HighTimeTemp
        {
            get
            {
                return _HighTimeTemp;
            }
            set
            {
                _HighTimeTemp = value;
                OnPropertyChanged("HighTimeTemp");
            }

        }
        private string _AvgTemp;
        public string AvgTemp
        {
            get
            {
                return _AvgTemp;
            }
            set
            {
                _AvgTemp = value;
                OnPropertyChanged("AvgTemp");
            }

        }
        private int _TempID;
        public int TempID
        {
            get
            {
                return _TempID;
            }
            set
            {
                _TempID = value;
                OnPropertyChanged("TempID");

            }
        }
        #endregion
        #region Constructor

        public OutOfRangesSettingsEntityTemp()
        { }

        public OutOfRangesSettingsEntityTemp(OutOfRangesSettingsEntityTemp Obj)
        {
            _ParameterName = Obj._ParameterName;
            _TempID = Obj.TempID;
            _LowLevelTemp = Obj.LowLevelTemp;
            _LowLevelTemp = Obj.LowLevelTemp;
            _HighLevelTemp = Obj.HighLevelTemp;
            _LowBypassTemp = Obj.LowBypassTemp;
            _HighBypassTemp = Obj.HighBypassTemp;
            _LowSPTemp = Obj.LowSPTemp;
            _HighSPTemp = Obj.HighSPTemp;
            _DelayTemp = Obj.DelayTemp;
            _LowCountTemp = Obj.LowCountTemp;
            _LowTimeTemp = Obj.LowTimeTemp;
            _HighCountTemp = Obj.HighCountTemp;
            _HighTimeTemp = Obj.HighTimeTemp;
            _AvgTemp = Obj.AvgTemp;
            _SetPointTemp = Obj.SetPointTemp;
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
