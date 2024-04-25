using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class EventLogEntity : INotifyPropertyChanged
    {
        #region Constructor

        public EventLogEntity()
        { }

        public EventLogEntity(EventLogEntity Obj)
        {
            _Description = Obj.Description;
            _GroupName = Obj.GroupName;
            _StartDateTime = Obj.StartDateTime;
            _EndDateTime = Obj.EndDateTime;
            _isComplete = Obj.isComplete;
        }

        #endregion
        #region "Public/Private Property"
        private string _Description;
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
                OnPropertyChanged("Description");

            }
        }

        private string _EventText;
        public string EventText
        {
            get
            {
                return _EventText;
            }
            set
            {
                _EventText = value;
                OnPropertyChanged("EventText");

            }
        }

        private string _GroupName;
        public string GroupName
        {
            get
            {
                return _GroupName;
            }
            set
            {
                _GroupName = value;
                OnPropertyChanged("GroupName");

            }
        }
        private DateTime _StartDateTime;
        public DateTime StartDateTime
        {
            get
            {
                return _StartDateTime;
            }
            set
            {
                _StartDateTime = value;
                OnPropertyChanged("StartDateTime");
            }
        }
        private DateTime _EndDateTime;
        public DateTime EndDateTime
        {
            get
            {
                return _EndDateTime;
            }
            set
            {
                _EndDateTime = value;
                OnPropertyChanged("EndDateTime");
            }
        }
        private bool _isComplete;
        public bool isComplete
        {
            get
            {
                return _isComplete;

            }
            set
            {
                _isComplete = value;
                OnPropertyChanged("isComplete");

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
