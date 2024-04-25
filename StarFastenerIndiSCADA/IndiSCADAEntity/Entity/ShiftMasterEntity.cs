using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class ShiftMasterEntity : INotifyPropertyChanged
    {
        #region Constructor

        public ShiftMasterEntity()
        { }

        public ShiftMasterEntity(ShiftMasterEntity Obj)
        {
            _ShiftNumber = Obj.ShiftNumber;
            _ShiftStartTime = Obj.ShiftStartTime;
            _ShiftEndTime = Obj.ShiftEndTime;
            _ModifiedDate = Obj.ModifiedDate;

        }

        #endregion
        #region "Public/Private Property"
        private string _ShiftNumber;
        public string ShiftNumber
        {
            get
            {
                return _ShiftNumber;
            }
            set
            {
                _ShiftNumber = value;
                OnPropertyChanged("ShiftNumber");

            }
        }

        private string _ShiftStartTime;
        public string ShiftStartTime
        {
            get
            {
                return _ShiftStartTime;
            }
            set
            {
                _ShiftStartTime = value;
                OnPropertyChanged("ShiftStartTime");

            }
        }

        private string _ShiftEndTime;
        public string ShiftEndTime
        {
            get
            {
                return _ShiftEndTime;
            }
            set
            {
                _ShiftEndTime = value;
                OnPropertyChanged("ShiftEndTime");

            }
        }

        private string _ModifiedDate;
        public string ModifiedDate
        {
            get
            {
                return _ModifiedDate;
            }
            set
            {
                _ModifiedDate = value;
                OnPropertyChanged("ModifiedDate");

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
