using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
   public class IOStatusEntity: INotifyPropertyChanged
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
        private string _W1;
        public string W1
        {
            get
            {
                return _W1;
            }
            set
            {
                _W1 = value;
                OnPropertyChanged("W1");
            }

        }
        private string _W2;
        public string W2
        {
            get
            {
                return _W2;
            }
            set
            {
                _W2 = value;
                OnPropertyChanged("W2");
            }

        }
        private string _W3;
        public string W3
        {
            get
            {
                return _W3;
            }
            set
            {
                _W3 = value;
                OnPropertyChanged("W3");
            }

        }
        private string _W4;
        public string W4
        {
            get
            {
                return _W4;
            }
            set
            {
                _W4 = value;
                OnPropertyChanged("W4");
            }

        }
        private string _W5;
        public string W5
        {
            get
            {
                return _W5;
            }
            set
            {
                _W5 = value;
                OnPropertyChanged("W5");
            }

        }
        private string _W6;
        public string W6
        {
            get
            {
                return _W6;
            }
            set
            {
                _W6 = value;
                OnPropertyChanged("W6");
            }

        }
        private string _W7;
        public string W7
        {
            get
            {
                return _W7;
            }
            set
            {
                _W7 = value;
                OnPropertyChanged("W7");
            }

        }
        private string _W8;
        public string W8
        {
            get
            {
                return _W8;
            }
            set
            {
                _W8 = value;
                OnPropertyChanged("W8");
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
        private string _ID;
        public string ID
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
        private string _D1;
        public string D1
        {
            get
            {
                return _D1;
            }
            set
            {
                _D1 = value;
                OnPropertyChanged("D1");
            }

        }
        private string _D2;
        public string D2
        {
            get
            {
                return _D2;
            }
            set
            {
                _D2 = value;
                OnPropertyChanged("D2");
            }

        }
        private string _D3;
        public string D3
        {
            get
            {
                return _D3;
            }
            set
            {
                _D3 = value;
                OnPropertyChanged("D3");
            }
        }
        private string _D4;
        public string D4
        {
            get
            {
                return _D4;
            }
            set
            {
                _D4 = value;
                OnPropertyChanged("D4");
            }
        }
        private string _CT1;
        public string CT1
        {
            get
            {
                return _CT1;
            }
            set
            {
                _CT1 = value;
                OnPropertyChanged("CT1");
            }
        }
        private string _CT2;
        public string CT2
        {
            get
            {
                return _CT2;
            }
            set
            {
                _CT2 = value;
                OnPropertyChanged("CT2");
            }
        }
        private string _CT3;
        public string CT3
        {
            get
            {
                return _CT3;
            }
            set
            {
                _CT3 = value;
                OnPropertyChanged("CT3");
            }
        }

        #endregion
        #region Constructor
        public IOStatusEntity()
        { }
        public IOStatusEntity(IOStatusEntity Obj)
        {
            _ID = Obj.ID;
            _W1 = Obj.W1;
            _W2 = Obj.W2;
            _W3 = Obj.W3;
            _W4 = Obj.W4;
            _W5 = Obj.W5;
            _W6 = Obj.W6;
            _W7 = Obj.W7;
            _W8 = Obj.W8;

            _D1 = Obj.D1;
            _D2 = Obj.D2;
            _D3 = Obj.D3;
            _D4 = Obj.D4;

            _CT1 = Obj.CT1;
            _CT2 = Obj.CT2;
            _CT3 = Obj.CT3;

            _ParameterName = Obj.ParameterName;
            _Value = Obj.Value;
        }
        #endregion
    }
}
