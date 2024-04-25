using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IndiSCADAEntity.Entity
{
    public partial class OutOfRangesSettingsEntitypH:INotifyPropertyChanged
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
        private string _LowLevelpH;
        public string LowLevelpH
        {
            get
            {
                return _LowLevelpH;
            }
            set
            {
                _LowLevelpH = value;
                OnPropertyChanged("LowLevelpH");
            }

        }
        private string _HighLevelpH;
        public string HighLevelpH
        {
            get
            {
                return _HighLevelpH;
            }
            set
            {
                _HighLevelpH = value;
                OnPropertyChanged("HighLevelpH");
            }

        }
        private string _LowBypasspH;
        public string LowBypasspH
        {
            get
            {
                return _LowBypasspH;
            }
            set
            {
                _LowBypasspH = value;
                OnPropertyChanged("LowBypasspH");
            }

        }
        private string _HighBypasspH;
        public string HighBypasspH
        {
            get
            {
                return _HighBypasspH;
            }
            set
            {
                _HighBypasspH = value;
                OnPropertyChanged("HighBypasspH");
            }

        }
        private string _LowSPpH;
        public string LowSPpH
        {
            get
            {
                return _LowSPpH;
            }
            set
            {
                _LowSPpH = value;
                OnPropertyChanged("LowSPpH");
            }

        }
        private string _HighSPpH;
        public string HighSPpH
        {
            get
            {
                return _HighSPpH;
            }
            set
            {
                _HighSPpH = value;
                OnPropertyChanged("HighSPpH");
            }

        }
        private string _DelaypH;
        public string DelaypH
        {
            get
            {
                return _DelaypH;
            }
            set
            {
                _DelaypH = value;
                OnPropertyChanged("DelaypH");
            }

        }
        private string _LowCountpH;
        public string LowCountpH
        {
            get
            {
                return _LowCountpH;
            }
            set
            {
                _LowCountpH = value;
                OnPropertyChanged("LowCountpH");
            }

        }
        private string _LowTimepH;
        public string LowTimepH
        {
            get
            {
                return _LowTimepH;
            }
            set
            {
                _LowTimepH = value;
                OnPropertyChanged("LowTimepH");
            }

        }
        private string _HighCountpH;
        public string HighCountpH
        {
            get
            {
                return _HighCountpH;
            }
            set
            {
                _HighCountpH = value;
                OnPropertyChanged("HighCountpH");
            }

        }
        private string _HighTimepH;
        public string HighTimepH
        {
            get
            {
                return _HighTimepH;
            }
            set
            {
                _HighTimepH = value;
                OnPropertyChanged("HighTimepH");
            }

        }
        private string _AvgpH;
        public string AvgpH
        {
            get
            {
                return _AvgpH;
            }
            set
            {
                _AvgpH = value;
                OnPropertyChanged("AvgpH");
            }

        }
        private int _pHID;
        public int pHID
        {
            get
            {
                return _pHID;
            }
            set
            {
                _pHID = value;
                OnPropertyChanged("pHID");

            }
        }
        #endregion
        #region Constructor

        public OutOfRangesSettingsEntitypH()
        { }

        public OutOfRangesSettingsEntitypH(OutOfRangesSettingsEntitypH Obj)
        {
            _ParameterName = Obj._ParameterName;
            _pHID = Obj.pHID;
            _LowLevelpH = Obj.LowLevelpH;
            _LowLevelpH = Obj.LowLevelpH;
            _HighLevelpH = Obj.HighLevelpH;
            _LowBypasspH = Obj.LowBypasspH;
            _HighBypasspH = Obj.HighBypasspH;
            _LowSPpH = Obj.LowSPpH;
            _HighSPpH = Obj.HighSPpH;
            _DelaypH = Obj.DelaypH;
            _LowCountpH = Obj.LowCountpH;
            _LowTimepH = Obj.LowTimepH;
            _HighCountpH = Obj.HighCountpH;
            _HighTimepH = Obj.HighTimepH;
            _AvgpH = Obj.AvgpH;
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
