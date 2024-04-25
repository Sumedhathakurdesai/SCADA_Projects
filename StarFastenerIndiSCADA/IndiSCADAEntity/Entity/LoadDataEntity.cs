using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
   public partial class LoadDataEntity : INotifyPropertyChanged
    {
        #region Public/Private Property
        private string _LastCycleTime;
        public string LastCycleTime
        {
            get
            {
                return _LastCycleTime;
            }
            set
            {
                _LastCycleTime = value;
                OnPropertyChanged("LastCycleTime");
            }
        }
        private string _LoadNumber;
        public string LoadNumber
        {
            get
            {
                return _LoadNumber;
            }
            set
            {
                _LoadNumber = value;
                OnPropertyChanged("LoadNumber");
            }

        }
        private string _LoadNumberWithTime;
        public string LoadNumberWithTime
        {
            get
            {
                return _LoadNumberWithTime;
            }
            set
            {
                _LoadNumberWithTime = value;
                OnPropertyChanged("LoadNumberWithTime");
            }

        }
        
        private int _LoadType;
        public int LoadType
        {
            get
            {
                return _LoadType;
            }
            set
            {
                _LoadType = value;
                OnPropertyChanged("LoadType");
            }
        }
        private DateTime _LoadInTime;
        public DateTime LoadInTime
        {
            get
            {
                return _LoadInTime;
            }
            set
            {
                _LoadInTime = value;
                OnPropertyChanged("LoadInTime");
            }
        }
        private DateTime _LoadOutTime;
        public DateTime LoadOutTime
        {
            get
            {
                return _LoadOutTime;
            }
            set
            {
                _LoadOutTime = value;
                OnPropertyChanged("LoadOutTime");
            }
        }
        private int _LoadOutShift;
        public int LoadOutShift
        {
            get
            {
                return _LoadOutShift;
            }
            set
            {
                _LoadOutShift = value;
                OnPropertyChanged("LoadOutShift");

            }
        }
        private int _LoadInShift;
        public int LoadInShift
        {
            get
            {
                return _LoadInShift;
            }
            set
            {
                _LoadInShift = value;
                OnPropertyChanged("LoadInShift");

            }
        }
        private string _Operator;
        public string Operator
        {
            get
            {
                return _Operator;
            }
            set
            {
                _Operator = value;
                OnPropertyChanged("Operator");
            }
        }
        private bool _isStart;
        public bool isStart
        {
            get
            {
                return _isStart;
            }
            set
            {
                _isStart = value;
                OnPropertyChanged("isStart");
            }
        }
        private bool _isEnd;
        public bool isEnd
        {
            get
            {
                return _isEnd;
            }
            set
            {
                _isEnd = value;
                OnPropertyChanged("isEnd");
            }
        }
        private bool _isStationExceedTime;
        public bool isStationExceedTime
        {
            get
            {
                return _isStationExceedTime;
            }
            set
            {
                _isStationExceedTime = value;
                OnPropertyChanged("isStationExceedTime");
            }
        }

        #endregion
        #region Constructor

        public LoadDataEntity()
        { }

        public LoadDataEntity(LoadDataEntity Obj)
        {
            _LoadNumber = Obj.LoadNumber;
            _LoadNumberWithTime = Obj.LoadNumberWithTime;
            _LoadType = Obj.LoadType;
            _LoadInTime = Obj.LoadInTime;
            _LoadOutTime = Obj.LoadOutTime;
            _LoadOutShift = Obj.LoadOutShift;
            _LoadInShift = Obj.LoadInShift;
            _Operator = Obj.Operator;
            _isStart = Obj.isStart;
            _isEnd = Obj.isEnd;
            _isStationExceedTime = Obj.isStationExceedTime;
            _LastCycleTime = Obj.LastCycleTime;
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
