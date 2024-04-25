using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class UserActivityEntity : INotifyPropertyChanged
    {
        #region Constructor

        public UserActivityEntity()
        { }

        public UserActivityEntity(UserActivityEntity Obj)
        {
            _UserName = Obj.UserName;
            _Activity = Obj.Activity;
            _DateTimeCol = Obj.DateTimeCol;
        }

        #endregion
        #region "Public/Private Property"
        private string _UserName;
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
                OnPropertyChanged("UserName");

            }
        }
        private string _Activity;
        public string Activity
        {
            get
            {
                return _Activity;
            }
            set
            {
                _Activity = value;
                OnPropertyChanged("Activity");

            }
        }
        private DateTime _DateTimeCol;
        public DateTime DateTimeCol
        {
            get
            {
                return _DateTimeCol;
            }
            set
            {
                _DateTimeCol = value;
                OnPropertyChanged("DateTimeCol");
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
