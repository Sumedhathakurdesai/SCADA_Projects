using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    
    public class StationDetails : INotifyPropertyChanged
    {
        private ObservableCollection<OverViewEntity> _StationData1;
        public ObservableCollection<OverViewEntity> StationData1
        {
            get { return _StationData1; }
            set { _StationData1 = value; OnPropertyChanged("StationData1"); }
        }

        private ObservableCollection<OverViewEntity> _StationData2;
        public ObservableCollection<OverViewEntity> StationData2
        {
            get { return _StationData2; }
            set { _StationData2 = value; OnPropertyChanged("StationData2"); }
        }

        private ObservableCollection<OverViewEntity> _StationData3;
        public ObservableCollection<OverViewEntity> StationData3
        {
            get { return _StationData3; }
            set { _StationData3 = value; OnPropertyChanged("StationData3"); }
        }
        
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        #endregion
    }
}
