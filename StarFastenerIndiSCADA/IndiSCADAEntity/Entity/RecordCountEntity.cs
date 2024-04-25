using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public class RecordCountEntity : INotifyPropertyChanged
    {
        #region"property"
                    

        private string _SendingDate;
        public string SendingDate
        {
            get
            {
                return _SendingDate;
            }
            set
            {
                _SendingDate = value;
                OnPropertyChanged("SendingDate");
            }
        }


        private string _TotalRecord;
        public string Total_Records
        {
            get
            {
                return _TotalRecord;
            }
            set
            {
                _TotalRecord = value;
                OnPropertyChanged("Total_Records");
            }
        }

        #endregion

        #region Constructor

        public RecordCountEntity()
        { }

        public RecordCountEntity(RecordCountEntity Obj)
        {
            _SendingDate = Obj.SendingDate;
           
            _TotalRecord = Obj.Total_Records;
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
