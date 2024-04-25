using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class TankBypassEntity: INotifyPropertyChanged
    {
        #region Public/Private Property
        private string _TankName;
        public string TankName
        {
            get
            {
                return _TankName;
            }
            set
            {
                _TankName = value;
                OnPropertyChanged("TankName");
            }

        }

        private string _BypassOnOff;
        public string BypassOnOff
        {
            get
            {
                return _BypassOnOff;
            }
            set
            {
                _BypassOnOff = value;
                OnPropertyChanged("BypassOnOff");
            }

        }
        private string _TankBypassID;
        public string TankBypassID
        {
            get
            {
                return _TankBypassID;
            }
            set
            {
                _TankBypassID = value;
                OnPropertyChanged("TankBypassID");

            }
        }

        #endregion
        #region Constructor

        public TankBypassEntity()
        { }

        public TankBypassEntity(TankBypassEntity Obj)
        {
            _TankName = Obj.TankName;
            _BypassOnOff = Obj.BypassOnOff;
            _TankBypassID = Obj.TankBypassID;
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
