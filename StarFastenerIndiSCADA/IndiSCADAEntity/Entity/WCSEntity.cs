 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class WCSEntity : INotifyPropertyChanged
    {
        #region Public/Private Property

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
        #endregion
        #region Constructor

        public WCSEntity()
        { }

        public WCSEntity(WCSEntity Obj)
        {
            _ParameterName = Obj.ParameterName;
            _ID = Obj.ID;
            _Value = Obj.Value;
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

