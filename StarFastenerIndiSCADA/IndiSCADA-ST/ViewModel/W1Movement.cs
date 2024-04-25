using IndiSCADAEntity.Entity;
using IndiSCADAGlobalLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace IndiSCADA_ST.ViewModel
{
    public class W1Movement : BaseViewModel
    {
        #region"Declaration"
        DispatcherTimer DispatchTimerView = new DispatcherTimer();
        System.ComponentModel.BackgroundWorker _BackgroundWorkerView = new System.ComponentModel.BackgroundWorker();
        //DoubleAnimation animW5=new DoubleAnimation ();
        #endregion
        #region ICommand
        private readonly ICommand _Exit;//
        public ICommand Exit
        {
            get { return _Exit; }
        }
        #endregion
        #region"Destructor"
        ~W1Movement()
        {
            try
            {
                DispatchTimerView.Stop();
            }
            catch { }
        }
        #endregion
        #region "Consrtuctor"
        public W1Movement()
        {
            try
            {
                isStartUp = true;
                WagonMovements();

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel ()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        #endregion
        #region "public/private methods"
        private void animW_Completed(object sender, EventArgs e)
        {
            try
            {
                WagonMovements();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel animW_Completed()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
       
        public void WagonMovements()
        {
            #region "Wagon "
            try
            {
                if (IndiSCADAGlobalLibrary.TagList.WagonMovment != null)
                {
                    int WagonPosition =  Convert.ToInt16(IndiSCADAGlobalLibrary.TagList.WagonMovment[0]);
                    //WagonPosition = 5;
                    int TimeToTravel = 3;
                    int xpo = Convert.ToInt32(W1.X / 47);
                    if (WagonPosition != 0)
                    {
                        if (WagonPosition > xpo)
                        {
                            TimeToTravel = (WagonPosition - xpo) * 1;
                        }
                        else
                        {
                            TimeToTravel = (xpo - WagonPosition) * 1;
                        }
                    }
                    int wagonPositionXValue = (WagonPosition - 1) * 47;
                    WagonMovement(wagonPositionXValue, TimeToTravel, Convert.ToInt32(W1.X));
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel DoWork(Wagon 1) W1Movement WagonMovements()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            #endregion
        }
        private void WagonMovement(int position, int TimeTOReach, int fromPosition)
        {
            try
            {
                if (position > 0)
                {
                    if (TimeTOReach > 0)
                    {
                        DoubleAnimation animW = new DoubleAnimation(Convert.ToInt32(position), TimeSpan.FromSeconds(TimeTOReach));
                        animW.Completed += new EventHandler(animW_Completed);
                        W1.BeginAnimation(TranslateTransform.XProperty, animW);
                    }

                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel Wagon1Movement() W1Movement WagonMovements()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void ExitButtonCommandClick(object _commandparameters)
        {
            try
            {
                DispatchTimerView.Stop();
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OverviewViewModel ExitButtonCommandClick() W1Movement ExitButtonCommandClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }

        #endregion
        #region Public/Private Property
        private bool _isStartUp;
        public bool isStartUp
        {
            get { return _isStartUp; }
            set { _isStartUp = value; OnPropertyChanged("isStartUp"); }
        }
        private ObservableCollection<RectifierEntity> _RectifierIntputs = new ObservableCollection<RectifierEntity>();
        public ObservableCollection<RectifierEntity> RectifierIntputs
        {
            get { return _RectifierIntputs; }
            set { _RectifierIntputs = value; OnPropertyChanged("RectifierIntputs"); }
        }
        private ObservableCollection<DosingSettingsEntity> _DosingIntputs = new ObservableCollection<DosingSettingsEntity>();
        public ObservableCollection<DosingSettingsEntity> DosingIntputs
        {
            get { return _DosingIntputs; }
            set { _DosingIntputs = value; OnPropertyChanged("DosingIntputs"); }
        }
        private ObservableCollection<TemperatureSettingEntity> _TemperatureIntputs = new ObservableCollection<TemperatureSettingEntity>();
        public ObservableCollection<TemperatureSettingEntity> TemperatureIntputs
        {
            get { return _TemperatureIntputs; }
            set { _TemperatureIntputs = value; OnPropertyChanged("TemperatureIntputs"); }
        }
        private string _Position1;
        public string Position1
        {
            get
            {
                return _Position1;
            }
            set
            {
                _Position1 = value;
                OnPropertyChanged("Position1");
            }
        }
        private string _PlantAM;
        public string PlantAM
        {
            get
            {
                return _PlantAM;
            }
            set
            {
                _PlantAM = value;
                OnPropertyChanged("PlantAM");
            }
        }
        private string _PlantService;
        public string PlantService
        {
            get
            {
                return _PlantService;
            }
            set
            {
                _PlantService = value;
                OnPropertyChanged("PlantService");
            }
        }
        private string _PlantCycleON;
        public string PlantCycleON
        {
            get
            {
                return _PlantCycleON;
            }
            set
            {
                _PlantCycleON = value;
                OnPropertyChanged("PlantCycleON");
            }
        }
        private string _PlantReset;
        public string PlantReset
        {
            get
            {
                return _PlantReset;
            }
            set
            {
                _PlantReset = value;
                OnPropertyChanged("PlantReset");
            }
        }
        private string _PlantPowerON;
        public string PlantPowerON
        {
            get
            {
                return _PlantPowerON;
            }
            set
            {
                _PlantPowerON = value;
                OnPropertyChanged("PlantPowerON");
            }
        }
        private string _W1position = "0";
        public string W1position
        {
            get
            {
                return _W1position;
            }
            set
            {
                _W1position = value;
                OnPropertyChanged("W1position");
                DoubleAnimation anim1 = new DoubleAnimation(Convert.ToInt32(W1position), TimeSpan.FromSeconds(5));
                W1.BeginAnimation(TranslateTransform.XProperty, anim1);
            }
        }
        private string _W1NextStep;
        public string W1NextStep
        {
            get
            {
                return _W1NextStep;
            }
            set
            {
                _W1NextStep = value;
                OnPropertyChanged("W1NextStep");
            }
        }
        private string _W2NextStep;
        public string W2NextStep
        {
            get
            {
                return _W2NextStep;
            }
            set
            {
                _W2NextStep = value;
                OnPropertyChanged("W2NextStep");
            }
        }
        private string _W3NextStep;
        public string W3NextStep
        {
            get
            {
                return _W3NextStep;
            }
            set
            {
                _W3NextStep = value;
                OnPropertyChanged("W3NextStep");
            }
        }
        private string _W4NextStep;
        public string W4NextStep
        {
            get
            {
                return _W4NextStep;
            }
            set
            {
                _W4NextStep = value;
                OnPropertyChanged("W4NextStep");
            }
        }
        private string _W5NextStep;
        public string W5NextStep
        {
            get
            {
                return _W5NextStep;
            }
            set
            {
                _W5NextStep = value;
                OnPropertyChanged("W5NextStep");
            }
        }
        private string _W1AutoMan;
        public string W1AutoMan
        {
            get
            {
                return _W1AutoMan;
            }
            set
            {
                _W1AutoMan = value;
                OnPropertyChanged("W1AutoMan");
            }
        }
        private string _W2AutoMan;
        public string W2AutoMan
        {
            get
            {
                return _W2AutoMan;
            }
            set
            {
                _W2AutoMan = value;
                OnPropertyChanged("W2AutoMan");
            }
        }
        private string _W3AutoMan;
        public string W3AutoMan
        {
            get
            {
                return _W3AutoMan;
            }
            set
            {
                _W3AutoMan = value;
                OnPropertyChanged("W3AutoMan");
            }
        }
        private string _W4AutoMan;
        public string W4AutoMan
        {
            get
            {
                return _W4AutoMan;
            }
            set
            {
                _W4AutoMan = value;
                OnPropertyChanged("W4AutoMan");
            }
        }
        private string _W5AutoMan;
        public string W5AutoMan
        {
            get
            {
                return _W5AutoMan;
            }
            set
            {
                _W5AutoMan = value;
                OnPropertyChanged("W5AutoMan");
            }
        }
        private Brush _colorr = Brushes.Red;
        public Brush W1AutoManualColor
        {
            get
            {
                return _colorr;
            }
            set
            {
                _colorr = value;
                OnPropertyChanged("W1AutoManualColor");
            }
        }
        private string _W1XPositionFrom;
        public string W1XPositionFrom
        {
            get { return _W1XPositionFrom; }
            set { _W1XPositionFrom = value; OnPropertyChanged("W1XPositionFrom"); }
        }
        private string _W1XPositionTo;
        public string W1XPositionTo
        {
            get { return _W1XPositionTo; }
            set { _W1XPositionTo = value; OnPropertyChanged("W1XPositionTo"); }
        }
        private TranslateTransform _W1 = new TranslateTransform();
        public TranslateTransform W1
        {
            get { return _W1; }
            set { _W1 = value; OnPropertyChanged("W1"); }
        }
        private TranslateTransform _W2 = new TranslateTransform();
        public TranslateTransform W2
        {
            get { return _W2; }
            set { _W2 = value; OnPropertyChanged("W2"); }
        }
        private TranslateTransform _W3 = new TranslateTransform();
        public TranslateTransform W3
        {
            get { return _W3; }
            set { _W3 = value; OnPropertyChanged("W3"); }
        }
        private TranslateTransform _W4 = new TranslateTransform();
        public TranslateTransform W4
        {
            get { return _W4; }
            set { _W4 = value; OnPropertyChanged("W4"); }
        }
        private TranslateTransform _W5 = new TranslateTransform();
        public TranslateTransform W5
        {
            get { return _W5; }
            set { _W5 = value; OnPropertyChanged("W5"); }
        }
        private ObservableCollection<StationDetails> _StationsData = new ObservableCollection<StationDetails>();
        public ObservableCollection<StationDetails> StationsData
        {
            get { return _StationsData; }
            set { _StationsData = value; OnPropertyChanged("StationsData"); }
        }
        #endregion
    }
}
