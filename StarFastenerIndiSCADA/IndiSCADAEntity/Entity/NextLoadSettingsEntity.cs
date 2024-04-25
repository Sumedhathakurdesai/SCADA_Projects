using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class NextLoadSettingsEntity : INotifyPropertyChanged
    {
        #region Public/Private Property
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
        private string _ColumnName;
        public string ColumnName
        {
            get
            {
                return _ColumnName;
            }
            set
            {
                _ColumnName = value;
                OnPropertyChanged("ColumnName");
            }

        }
        private string _DataType;
        public string DataType
        {
            get
            {
                return _DataType;
            }
            set
            {
                _DataType = value;
                OnPropertyChanged("DataType");
            }

        }
        private string _Unit;
        public string Unit
        {
            get
            {
                return _Unit;
            }
            set
            {
                _Unit = value;
                OnPropertyChanged("Unit");
            }

        }
        private string _MinValue;
        public string MinValue
        {
            get
            {
                return _MinValue;
            }
            set
            {
                _MinValue = value;
                OnPropertyChanged("MinValue");
            }

        }
        private string _MaxValue;
        public string MaxValue
        {
            get
            {
                return _MaxValue;
            }
            set
            {
                _MaxValue = value;
                OnPropertyChanged("MaxValue");
            }

        }
        private string _TaskName;
        public string TaskName
        {
            get
            {
                return _TaskName;
            }
            set
            {
                _TaskName = value;
                OnPropertyChanged("TaskName");
            }

        }
        private string _ScreenName;
        public string ScreenName
        {
            get
            {
                return _ScreenName;
            }
            set
            {
                _ScreenName = value;
                OnPropertyChanged("ScreenName");
            }

        }
        private string _CalculationFormula;
        public string CalculationFormula
        {
            get
            {
                return _CalculationFormula;
            }
            set
            {
                _CalculationFormula = value;
                OnPropertyChanged("CalculationFormula");
            }

        }
        private string _Formula;
        public string Formula
        {
            get
            {
                return _Formula;
            }
            set
            {
                _Formula = value;
                OnPropertyChanged("Formula");
            }

        }
        private bool _isPrimaryKey;
        public bool isPrimaryKey
        {
            get
            {
                return _isPrimaryKey;
            }
            set
            {
                _isPrimaryKey = value;
                OnPropertyChanged("isPrimaryKey");
            }

        }
        private bool _isDownloadToPlc;
        public bool isDownloadToPlc
        {
            get
            {
                return _isDownloadToPlc;
            }
            set
            {
                _isDownloadToPlc = value;
                OnPropertyChanged("isDownloadToPlc");
            }

        }
        private bool _isReadOnly;
        public bool isReadOnly
        {
            get
            {
                return _isReadOnly;
            }
            set
            {
                _isReadOnly = value;
                OnPropertyChanged("isReadOnly");
            }

        }
        private bool _isInReport;
        public bool isInReport
        {
            get
            {
                return _isInReport;
            }
            set
            {
                _isInReport = value;
                OnPropertyChanged("isInReport");
            }

        }

        private bool _isCalculationRequired;
        public bool isCalculationRequired
        {
            get
            {
                return _isCalculationRequired;
            }
            set
            {
                _isCalculationRequired = value;
                OnPropertyChanged("isCalculationRequired");
            }


        }
        private bool _ClickToAddFormula;
        public bool ClickToAddFormula
        {
            get
            {
                return _ClickToAddFormula;
            }
            set
            {
                _ClickToAddFormula = value;
                OnPropertyChanged("ClickToAddFormula");
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
        #region Constructor

        public NextLoadSettingsEntity()
        {
            _ParameterName = "";
            _DataType = "";
            _Unit = "";
            _MinValue = "";
            _MaxValue = "";
            _TaskName = "";
            _ScreenName = "";
            _CalculationFormula = "";
            _isPrimaryKey = false;
            _isCalculationRequired = false;
            _isDownloadToPlc = false;
            _isReadOnly = false;
            _Formula = "";
            _ClickToAddFormula = false;
        }

        public NextLoadSettingsEntity(NextLoadSettingsEntity Obj)
        {
            _ParameterName = Obj.ParameterName;
            _DataType = Obj.DataType;
            _Unit = Obj.Unit;
            _MinValue = Obj.MinValue;
            _MaxValue = Obj.MaxValue;
            _TaskName = Obj.TaskName;
            _ScreenName = Obj.ScreenName ;
            _CalculationFormula = Obj.CalculationFormula;
            _isPrimaryKey = Obj.isPrimaryKey;
            _isCalculationRequired = Obj.isCalculationRequired;
            _isDownloadToPlc = Obj.isDownloadToPlc;
            _isReadOnly = Obj.isReadOnly;
            _Formula= Obj.Formula;
            _ClickToAddFormula = Obj.ClickToAddFormula;
        }

        #endregion
    }
}
