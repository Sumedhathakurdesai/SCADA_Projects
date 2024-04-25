using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class TrendEntity : INotifyPropertyChanged
    {
        #region "Public/Private Property"

        private string _ItemName;
        public string ItemName
        {
            get
            {
                return _ItemName;
            }
            set
            {
                _ItemName = value;
                OnPropertyChanged("ItemName");

            }
        }
        private bool _isTempChecked;
        public bool isTempChecked
        {
            get
            {
                return _isTempChecked;
            }
            set
            {
                _isTempChecked = value;
                OnPropertyChanged("isTempChecked");

            }
        }

        private bool _isCurrentChecked;
        public bool isCurrentChecked
        {
            get
            {
                return _isCurrentChecked;
            }
            set
            {
                _isCurrentChecked = value;
                OnPropertyChanged("isCurrentChecked");

            }
        }

        #endregion
        #region Constructor
        public TrendEntity()
        { }
        public TrendEntity(TrendEntity Obj)
        {
            _ItemName = Obj.ItemName;
            _isTempChecked = Obj.isTempChecked;
            _isCurrentChecked = Obj.isCurrentChecked;
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
