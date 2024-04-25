using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class UserMasterEntity : INotifyPropertyChanged
    {
        #region Public/Private Property
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
        private string _Password;
        public string UserPassword
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
                OnPropertyChanged("UserPassword");
            }
        }
        private string _UserRole;
        public string UserRole
        {
            get
            {
                return _UserRole;
            }
            set
            {
                _UserRole = value;
                OnPropertyChanged("UserRole");
            }
        }
        private string _EmailID;
        public string EmailID
        {
            get
            {
                return _EmailID;
            }
            set
            {
                _EmailID = value;
                OnPropertyChanged("EmailID");

            }
        }

        private string _MobileNo;
        public string MobileNo
        {
            get
            {
                return _MobileNo;
            }
            set
            {
                _MobileNo = value;
                OnPropertyChanged("MobileNo");
            }
        }


        #endregion

        #region Constructor

        public UserMasterEntity()
        { }

        public UserMasterEntity(UserMasterEntity Obj)
        {
            _UserName = Obj.UserName;
            _UserRole = Obj.UserRole;
            _EmailID = Obj.EmailID;
            _Password = Obj.UserPassword;
            _MobileNo = Obj.MobileNo;
        }

        #endregion

        #region Public/Private Method

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
