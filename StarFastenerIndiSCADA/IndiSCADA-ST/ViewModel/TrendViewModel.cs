using IndiSCADAEntity.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Wpf;
using System.ComponentModel;
using System.Windows.Data;
using System.Threading;
using LiveCharts.Configurations;
using System.Windows.Controls;
using IndiSCADAGlobalLibrary;
using System.Data;
using System.Windows;
using System.Windows.Media;
using Syncfusion.UI.Xaml.Charts;

namespace IndiSCADA_ST.ViewModel
{
    public class TrendViewModel : BaseViewModel
    {
        #region"Declaration"
        private double _axisMax;
        private double _axisMin;
        private double _trend;
        public event PropertyChangedEventHandler PropertyChanged;
        DispatcherTimer DispatchTimerView = new DispatcherTimer();
        System.ComponentModel.BackgroundWorker _BackgroundWorkerView = new System.ComponentModel.BackgroundWorker(); 
        #endregion
      
        #region ICommand
        private readonly ICommand _FilterAlarmAndEventHistoryClick;
        public ICommand FilterAlarmAndEventHistoryClick
        {
            get { return _FilterAlarmAndEventHistoryClick; }
        }
        
        //private readonly ICommand _CurrentCheckedClick;
        //public ICommand CurrentCheckedClick
        //{
        //    get { return _CurrentCheckedClick; }
        //}

        private readonly ICommand _UpdateGraph;
        public ICommand UpdateGraph
        {
            get { return _UpdateGraph; }
        }
        
        private readonly ICommand _rectHistoryClick;
        public ICommand rectHistoryClick
        {
            get { return _rectHistoryClick; }
        }
        private readonly ICommand _tempHistoryClick;
        public ICommand tempHistoryClick
        {
            get { return _tempHistoryClick; }
        }
        private readonly ICommand _Exit;//
        public ICommand Exit
        {
            get { return _Exit; }
        }
        private readonly ICommand _RectPopup;
        public ICommand RectPopup
        {
            get { return _RectPopup; }
        }
        private readonly ICommand _TempPopup;
        public ICommand TempPopup
        {
            get { return _TempPopup; }
        }

        //temperature
        private readonly ICommand _TempTrendPause;//
        public ICommand TempTrendPause
        {
            get { return _TempTrendPause; }
        }
        private readonly ICommand _TempTrendPlay;//
        public ICommand TempTrendPlay
        {
            get { return _TempTrendPlay; }
        }
        private readonly ICommand _TempTrendPrint;//
        public ICommand TempTrendPrint
        {
            get { return _TempTrendPrint; }
        }

        private bool _isOpenTempTrendBPopup = new bool();
        public bool isOpenTempTrendBPopup
        {
            get { return _isOpenTempTrendBPopup; }
            set { _isOpenTempTrendBPopup = value; OnPropertyChanged("isOpenTempTrendBPopup"); }
        }

        private bool _isOpenRectTrendBPopup = new bool();
        public bool isOpenRectTrendBPopup
        {
            get { return _isOpenRectTrendBPopup; }
            set { _isOpenRectTrendBPopup = value; OnPropertyChanged("isOpenRectTrendBPopup"); }
        }
        //
        #endregion
       
        #region"Destructor"
        ~TrendViewModel()
        {
            try
            {
                DispatchTimerView.Stop();
            }
            catch { }
        }
        #endregion

    //    public class SymbolColor : IValueConverter 
    //{ 
    //  public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
    //  { 
 
    //        var adornment = value as ChartAdornment; 
    //        var item = adornment.Item as Model; 
    //        if (item.ColorFlag) 
    //            return new SolidColorBrush(Colors.Yellow); 
    //        else 
    //            return new SolidColorBrush(Colors.Red); 
    //  } 
    //  } 

        #region "Consrtuctor"
        public TrendViewModel()
        {
            try
            {
       
                TempvisibilityValue = "Hidden";
                RectifiervisibilityValue = "Hidden";

                var mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.DateTime.Ticks)   //use DateTime.Ticks as X
                .Y(model => model.Value);           //use the value property as Y
                //lets save the mapper globally.
                Charting.For<MeasureModel>(mapper);
                //lets set how to display the X Labels
                DateTimeFormatter = value => new DateTime((long)value).ToString("mm:ss");
                //AxisStep forces the distance between each separator in the X axis
                AxisStep = TimeSpan.FromSeconds(1).Ticks;
                //AxisUnit forces lets the axis know that we are plotting seconds
                //this is not always necessary, but it can prevent wrong labeling
                AxisUnit = TimeSpan.TicksPerSecond;
                SetAxisLimits(DateTime.Now);
                ChartValuesTemperature = new ChartValues<MeasureModel>(); //new ChartValues<MeasureModel>();
                ChartValuesCurrent = new ChartValues<MeasureModel>();
                isTempTrendPlay = true;
                isCurrentTrendPlay = true;

                _BackgroundWorkerView.DoWork += DoWork;
                DispatchTimerView.Interval = TimeSpan.FromMilliseconds(1000);
                DispatchTimerView.Tick += DispatcherTickEvent;
                DispatchTimerView.Start();
                _Exit = new RelayCommand(ExitButtonCommandClick);

                _FilterAlarmAndEventHistoryClick = new RelayCommand(FilterButtonClick);
                _tempHistoryClick = new RelayCommand(FiltertempHistoryClick);
                _rectHistoryClick = new RelayCommand(FilterrectHistoryClick);

                //_CurrentCheckedClick = new RelayCommand(CurrentCheckedcmdClick);

                _TempTrendPrint = new RelayCommand(TempTrendPrintclick);
                _TempTrendPlay = new RelayCommand(TempTrendPlayclick);
                _TempTrendPause = new RelayCommand(TempTrendPauseclick);

                _TempPopup = new RelayCommand(TempPopupclick);
                _RectPopup = new RelayCommand(RectPopupclick);

                _UpdateGraph = new RelayCommand(UpdateGraphclick);

                TrendEntityItems = IndiSCADABusinessLogic.TrendLogic.ReturnStationName("Temperature"); //remove comments after completing source code
                _allItems = TrendEntityItems;
                TrendEntityCurrentItems = IndiSCADABusinessLogic.TrendLogic.ReturnStationName("Rectifier"); //remove comments after completing source code
                _allItemsCurrent = TrendEntityCurrentItems;
                
                HistoricalTempTrend = new ObservableCollection<TemperatureTrendentity>();
                TemperatureTrendentity _Da = new TemperatureTrendentity();
                _Da.DateTimeCol = DateTime.Now;
                _Da.SoakDegreasing = 10;
                _Da.Dryer2 = 17;
                TemperatureTrendentity _Da1 = new TemperatureTrendentity();
                _Da1.DateTimeCol = DateTime.Now;
                _Da1.Anodic1 = 1000;
                _Da1.Anodic2 = 1700;
                TemperatureTrendentity _Da2 = new TemperatureTrendentity();
                _Da2.DateTimeCol = DateTime.Now;
                _Da2.AlZinc1 = 103;
                _Da2.Dryer2 = 170;


        

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TrendViewModel ()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        #endregion
   
        #region "public/private methods"
        private void UpdateGraphclick(object objCommandParameter)
        {
            try
            {
                string para = objCommandParameter.ToString();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TrendViewModel UpdateGraphclick()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
         
        private void SetAxisLimits(DateTime now)
        {
            try
            {
                AxisMax = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 1 second ahead
                AxisMin = now.Ticks - TimeSpan.FromSeconds(8).Ticks; // and 8 seconds behind
            }
            catch(Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TrendViewModel SetAxisLimits()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
         
        private void FiltertempHistoryClick(object objCommandParameter)
        {
            try
            {
                isOpenTempTrendBPopup = false;
                ObservableCollection<TemperatureTrendentity> _tEMPTrendEntity = new ObservableCollection<TemperatureTrendentity>();
                NewHistoricalTempTrend = new ObservableCollection<TemperatureTrendentity>();

                int TempIndex = 0;
                if (HistoricalTempTrend != null)
                {
                    foreach (var item in HistoricalTempTrend)
                    {
                        TemperatureTrendentity _TemperatureTrendentityName = new TemperatureTrendentity();

                        if (isSoakDegressingTempSelected == true)
                        {
                            _TemperatureTrendentityName.SoakDegreasing =HistoricalTempTrend[TempIndex].SoakDegreasing;
                            
                        }
                        if (isAnodic1TempSelected == true)
                        {
                            _TemperatureTrendentityName.Anodic1 = HistoricalTempTrend[TempIndex].Anodic1;
                            
                        }
                        if (isAnodic2TempSelected == true)
                        {
                            _TemperatureTrendentityName.Anodic2 = HistoricalTempTrend[TempIndex].Anodic2;
                            
                        }
                        if (isAlkalineZinc1TempSelected == true)
                        {
                            _TemperatureTrendentityName.AlZinc1 = HistoricalTempTrend[TempIndex].AlZinc1;
                            
                        }
                        if (isAlkalineZinc2TempSelected == true)
                        {
                            _TemperatureTrendentityName.AlZinc2 = HistoricalTempTrend[TempIndex].AlZinc2;
                            
                        }
                        if (isAlkalineZinc3TempSelected == true)
                        {
                            _TemperatureTrendentityName.AlZinc3 = HistoricalTempTrend[TempIndex].AlZinc3;
                            
                        }
                        if (isAlkalineZinc4TempSelected == true)
                        {
                            _TemperatureTrendentityName.AlZinc4 = HistoricalTempTrend[TempIndex].AlZinc4;
                            
                        }
                        if (isAlkalineZinc5TempSelected == true)
                        {
                            _TemperatureTrendentityName.AlZinc5 = HistoricalTempTrend[TempIndex].AlZinc5;
                            
                        }
                        if (isAlkalineZinc6TempSelected == true)
                        {
                            _TemperatureTrendentityName.AlZinc6 = HistoricalTempTrend[TempIndex].AlZinc6;
                            
                        }
                        if (isAlkalineZinc7TempSelected == true)
                        {
                            _TemperatureTrendentityName.AlZinc7 = HistoricalTempTrend[TempIndex].AlZinc7;
                            
                        }
                        if (isAlkalineZinc8TempSelected == true)
                        {
                            _TemperatureTrendentityName.AlZinc8 = HistoricalTempTrend[TempIndex].AlZinc8;
                            
                        }
                        if (isAlkalineZinc9TempSelected == true)
                        {
                            _TemperatureTrendentityName.AlZinc9 = HistoricalTempTrend[TempIndex].AlZinc9;
                            
                        }
                        if (isPass1TempSelected == true)
                        {
                            _TemperatureTrendentityName.Pass1 = HistoricalTempTrend[TempIndex].Pass1;
                            
                        }
                        if (isPass2TempSelected == true)
                        {
                            _TemperatureTrendentityName.Pass2 = HistoricalTempTrend[TempIndex].Pass2;
                            
                        }
                        if (isPass3TempSelected == true)
                        {
                            _TemperatureTrendentityName.Pass3 = HistoricalTempTrend[TempIndex].Pass3;
                            
                        }
                        if (isPass4TempSelected == true)
                        {
                            _TemperatureTrendentityName.Pass4 = HistoricalTempTrend[TempIndex].Pass4;
                            
                        }
                        if (isPass5TempSelected == true)
                        {
                            _TemperatureTrendentityName.Pass5 = HistoricalTempTrend[TempIndex].Pass5;
                            
                        }
                        if (isDryer1TempSelected == true)
                        {
                            _TemperatureTrendentityName.Dryer1 = HistoricalTempTrend[TempIndex].Dryer1;
                            
                        }
                        if (isDryer2TempSelected == true)
                        {
                            _TemperatureTrendentityName.Dryer2 = HistoricalTempTrend[TempIndex].Dryer2;
                            
                        }
                        if (istemp20Selected == true)
                        {
                            _TemperatureTrendentityName.Dryer2 = HistoricalTempTrend[TempIndex].Temp20;
                            
                        }
                        if (istemp21Selected == true)
                        {
                            _TemperatureTrendentityName.Dryer2 = HistoricalTempTrend[TempIndex].Temp21;
                            
                        }
                        if (istemp22Selected == true)
                        {
                            _TemperatureTrendentityName.Dryer2 = HistoricalTempTrend[TempIndex].Temp22;
                            
                        }
                        if (istemp23Selected == true)
                        {
                            _TemperatureTrendentityName.Dryer2 = HistoricalTempTrend[TempIndex].Temp23;
                            
                        }
                        if (istemp24Selected == true)
                        {
                            _TemperatureTrendentityName.Dryer2 = HistoricalTempTrend[TempIndex].Temp24;
                            
                        }
                        if (istemp25Selected == true)
                        {
                            _TemperatureTrendentityName.Dryer2 = HistoricalTempTrend[TempIndex].Temp25;
                            
                        }



                        if (isAllTempSelected == true)
                        {
                            _TemperatureTrendentityName.SoakDegreasing = HistoricalTempTrend[TempIndex].SoakDegreasing;
                            _TemperatureTrendentityName.Anodic1 = HistoricalTempTrend[TempIndex].Anodic1;
                            _TemperatureTrendentityName.Anodic2 = HistoricalTempTrend[TempIndex].Anodic2;
                            _TemperatureTrendentityName.AlZinc1 = HistoricalTempTrend[TempIndex].AlZinc1;
                            _TemperatureTrendentityName.AlZinc2 = HistoricalTempTrend[TempIndex].AlZinc2;
                            _TemperatureTrendentityName.AlZinc3 = HistoricalTempTrend[TempIndex].AlZinc3;
                            _TemperatureTrendentityName.AlZinc4 = HistoricalTempTrend[TempIndex].AlZinc4;
                            _TemperatureTrendentityName.AlZinc5 = HistoricalTempTrend[TempIndex].AlZinc5;
                            _TemperatureTrendentityName.AlZinc6 = HistoricalTempTrend[TempIndex].AlZinc6;
                            _TemperatureTrendentityName.AlZinc7 = HistoricalTempTrend[TempIndex].AlZinc7;
                            _TemperatureTrendentityName.AlZinc8 = HistoricalTempTrend[TempIndex].AlZinc8;
                            _TemperatureTrendentityName.AlZinc9 = HistoricalTempTrend[TempIndex].AlZinc9;
                            _TemperatureTrendentityName.Pass1 = HistoricalTempTrend[TempIndex].Pass1;
                            _TemperatureTrendentityName.Pass2 = HistoricalTempTrend[TempIndex].Pass2;
                            _TemperatureTrendentityName.Pass3 = HistoricalTempTrend[TempIndex].Pass3;
                            _TemperatureTrendentityName.Pass4 = HistoricalTempTrend[TempIndex].Pass4;
                            _TemperatureTrendentityName.Pass5 = HistoricalTempTrend[TempIndex].Pass5;
                            _TemperatureTrendentityName.Dryer1 = HistoricalTempTrend[TempIndex].Dryer1;
                            _TemperatureTrendentityName.Dryer2 = HistoricalTempTrend[TempIndex].Dryer2;

                            _TemperatureTrendentityName.Temp20 = HistoricalTempTrend[TempIndex].Temp20;
                            _TemperatureTrendentityName.Temp21 = HistoricalTempTrend[TempIndex].Temp21;
                            _TemperatureTrendentityName.Temp22 = HistoricalTempTrend[TempIndex].Temp22;
                            _TemperatureTrendentityName.Temp23 = HistoricalTempTrend[TempIndex].Temp23;
                            _TemperatureTrendentityName.Temp24 = HistoricalTempTrend[TempIndex].Temp24;
                            _TemperatureTrendentityName.Temp25 = HistoricalTempTrend[TempIndex].Temp25;
                        }


                        _TemperatureTrendentityName.TimeCol = (HistoricalTempTrend[TempIndex].DateTimeCol).ToString("HH: mm"); 


                        TempIndex = TempIndex + 1;


                        _tEMPTrendEntity.Add(_TemperatureTrendentityName);
                        NewHistoricalTempTrend.Add(_TemperatureTrendentityName);
                    }
                }
                TempvisibilityValue = "Visible";
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TrendViewModel FiltertempHistoryClick()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }

        private void FilterrectHistoryClick(object objCommandParameter)
        {
            try
            {
                isOpenRectTrendBPopup = false;
                ObservableCollection<RectifierTrendEntity> _RectifierTrendEntity = new ObservableCollection<RectifierTrendEntity>();
                NewHistoricalCurrentTrend  = new ObservableCollection<RectifierTrendEntity>();

                int TempIndex = 0;
                if (HistoricalCurrentTrend != null)
                {
                    foreach (var item in HistoricalCurrentTrend)
                    {
                        RectifierTrendEntity _RectifierTrendEntityName = new RectifierTrendEntity();

                        if (isAnodic1CurrentSelected == true)
                        {
                            _RectifierTrendEntityName.Anodic1ActualCurrent= HistoricalCurrentTrend[TempIndex].Anodic1ActualCurrent;
                             
                        }
                        if (isAnodic2CurrentSelected == true)
                        {
                            _RectifierTrendEntityName.Anodic2ActualCurrent = HistoricalCurrentTrend[TempIndex].Anodic2ActualCurrent;
                             
                        }
                        if (isAnodic3CurrentSelected == true)
                        {
                            _RectifierTrendEntityName.Anodic3ActualCurrent = HistoricalCurrentTrend[TempIndex].Anodic3ActualCurrent;
                             
                        }
                        if (isAlZn1Selected == true)
                        {
                            _RectifierTrendEntityName.AlZn1ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn1ActualCurrent;
                             
                        }
                        if (isAlZn2Selected == true)
                        {
                            _RectifierTrendEntityName.AlZn2ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn2ActualCurrent;
                             
                        }
                        if (isAlZn3Selected == true)
                        {
                            _RectifierTrendEntityName.AlZn3ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn3ActualCurrent;
                             
                        }
                        if (isAlZn4Selected == true)
                        {
                            _RectifierTrendEntityName.AlZn4ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn4ActualCurrent;
                             
                        }
                        if (isAlZn5Selected == true)
                        {
                            _RectifierTrendEntityName.AlZn5ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn5ActualCurrent;
                             
                        }
                        if (isAlZn6Selected == true)
                        {
                            _RectifierTrendEntityName.AlZn6ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn6ActualCurrent;
                             
                        }
                        if (isAlZn7Selected == true)
                        {
                            _RectifierTrendEntityName.AlZn7ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn7ActualCurrent;
                             
                        }
                        if (isAlZn8Selected == true)
                        {
                            _RectifierTrendEntityName.AlZn8ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn8ActualCurrent;
                             
                        }
                        if (isAlZn9Selected == true)
                        {
                            _RectifierTrendEntityName.AlZn9ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn9ActualCurrent;
                             
                        }
                        if (isAlZn10Selected == true)
                        {
                            _RectifierTrendEntityName.AlZn10ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn10ActualCurrent;
                             
                        }
                        if (isAlZn11Selected == true)
                        {
                            _RectifierTrendEntityName.AlZn11ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn11ActualCurrent;
                             
                        }
                        if (isAlZn12Selected == true)
                        {
                            _RectifierTrendEntityName.AlZn12ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn12ActualCurrent;
                             
                        }
                        if (isAlZn13Selected == true)
                        {
                            _RectifierTrendEntityName.AlZn13ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn13ActualCurrent;
                             
                        }
                        if (isAlZn14Selected == true)
                        {
                            _RectifierTrendEntityName.AlZn14ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn14ActualCurrent;
                             
                        }
                        if (isAlZn15Selected == true)
                        {
                            _RectifierTrendEntityName.AlZn15ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn15ActualCurrent;
                             
                        }
                        if (isAlZn16Selected == true)
                        {
                            _RectifierTrendEntityName.AlZn16ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn16ActualCurrent;
                             
                        }
                        if (isAlZn17Selected == true)
                        {
                            _RectifierTrendEntityName.AlZn17ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn17ActualCurrent;
                             
                        }
                        if (isAlZn18Selected == true)
                        {
                            _RectifierTrendEntityName.AlZn18ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn18ActualCurrent;
                             
                        }

                        if (isAll == true)
                        {
                            _RectifierTrendEntityName.Anodic1ActualCurrent = HistoricalCurrentTrend[TempIndex].Anodic1ActualCurrent;
                            _RectifierTrendEntityName.Anodic2ActualCurrent = HistoricalCurrentTrend[TempIndex].Anodic2ActualCurrent;
                            _RectifierTrendEntityName.Anodic3ActualCurrent = HistoricalCurrentTrend[TempIndex].Anodic3ActualCurrent;
                            _RectifierTrendEntityName.AlZn1ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn1ActualCurrent;
                            _RectifierTrendEntityName.AlZn2ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn2ActualCurrent;
                            _RectifierTrendEntityName.AlZn3ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn3ActualCurrent;
                            _RectifierTrendEntityName.AlZn4ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn4ActualCurrent;
                            _RectifierTrendEntityName.AlZn5ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn5ActualCurrent;
                            _RectifierTrendEntityName.AlZn6ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn6ActualCurrent;
                            _RectifierTrendEntityName.AlZn7ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn7ActualCurrent;
                            _RectifierTrendEntityName.AlZn8ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn8ActualCurrent;
                            _RectifierTrendEntityName.AlZn9ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn9ActualCurrent;
                            _RectifierTrendEntityName.AlZn10ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn10ActualCurrent;
                            _RectifierTrendEntityName.AlZn11ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn11ActualCurrent;
                            _RectifierTrendEntityName.AlZn12ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn12ActualCurrent;
                            _RectifierTrendEntityName.AlZn13ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn13ActualCurrent;
                            _RectifierTrendEntityName.AlZn14ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn14ActualCurrent;
                            _RectifierTrendEntityName.AlZn15ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn15ActualCurrent;
                            _RectifierTrendEntityName.AlZn16ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn16ActualCurrent;
                            _RectifierTrendEntityName.AlZn17ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn17ActualCurrent;
                            _RectifierTrendEntityName.AlZn18ActualCurrent = HistoricalCurrentTrend[TempIndex].AlZn18ActualCurrent;
                        } 

                        _RectifierTrendEntityName.TimeCol = (HistoricalCurrentTrend[TempIndex].DateTimeCol).ToString("HH: mm");
                        TempIndex = TempIndex + 1;


                        _RectifierTrendEntity.Add(_RectifierTrendEntityName);
                        NewHistoricalCurrentTrend.Add(_RectifierTrendEntityName);
                    }
                }
                RectifiervisibilityValue = "Visible";
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TrendViewModel FilterrectHistoryClick()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        private void FilterButtonClick(object objCommandParameter)
        {
            try
            {


                DateTime StartDate = Convert.ToDateTime(SelectedStartDate.ToShortDateString() + " " + SelectedStartTime.ToShortTimeString());
                DateTime EndDate = Convert.ToDateTime(SelectedEndDate.ToShortDateString() + " " + SelectedEndTime.ToShortTimeString());
                if (isTemperatureSelected == true)
                {
                    HistoricalTempTrend = IndiSCADABusinessLogic.TrendLogic.GetTempratureData(StartDate, EndDate);
                    isAllTempSelected = true;
                    FiltertempHistoryClick("");
                    TempvisibilityValue = "Visible";
                }
                if (isRectifierSelected == true)
                {
                    HistoricalCurrentTrend = IndiSCADABusinessLogic.TrendLogic.GetCurrentData(StartDate, EndDate);
                    isAll = true;
                    FilterrectHistoryClick("");
                    RectifiervisibilityValue = "Visible";
                }


               

                //ObservableCollection<HistoricalCurrentTrend> TemperatureIP = IndiSCADABusinessLogic.SettingLogic.GetTemperatureInputs();
           

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TrendViewModel Generate Historical Graph FilterButtonClick()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        private void TempPopupclick(object _commandparameters)
        {
            try
            {
                //if (DispatchTimerView.IsEnabled == true)
                //{
                isOpenTempTrendBPopup = true;
                isOpenRectTrendBPopup = false;
                //}
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("TrendViewModel TempPopupclick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void RectPopupclick(object _commandparameters)
        {
            try
            {
                //if (DispatchTimerView.IsEnabled == true)
                //{
                isOpenTempTrendBPopup = false;
                isOpenRectTrendBPopup = true;
                //}
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("TrendViewModel RectPopupclick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void TempTrendPauseclick(object _commandparameters)
        {
            try
            {
                if (DispatchTimerView.IsEnabled == true)
                {
                    DispatchTimerView.Stop();
                    isTempTrendPlay = false;
                    isTempTrendpause = true;
                    isCurrentTrendpause = true;
                    isCurrentTrendPlay = false;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("TrendViewModel TempTrendPauseclick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void TempTrendPlayclick(object _commandparameters)
        {
            try
            {
                if (DispatchTimerView.IsEnabled == false)
                {
                    DispatchTimerView.Start();
                    isTempTrendPlay = true;
                    isTempTrendpause = false;
                    isCurrentTrendpause = false;
                    isCurrentTrendPlay = true;
                }
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("TrendViewModel TempTrendPlayclick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void TempTrendPrintclick(object _commandparameters)
        {
            try
            {
               // TempTrendChart
                //ScreenCapture sc = new ScreenCapture();
                //// capture entire screen, and save it to a file
                //Image img = sc.CaptureScreen();
                //// display image in a Picture control named imageDisplay
                ////this.imageDisplay.Image = img;
                //// capture this window, and save it
                //sc.CaptureWindowToFile(this.Handle, "C:\\temp2.gif", ImageFormat.Gif);
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("TrendViewModel TempTrendPrintclick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void FilterTempItems(string keyword)
        {
            try
            {
                var filteredItems =
                string.IsNullOrWhiteSpace(keyword) ?
                _allItems :
                _allItems.Where(i => i.ItemName.ToLower().Contains(keyword.ToLower()));

            TrendEntityItems = new ObservableCollection<TrendEntity>(filteredItems);
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog(" TrendViewModel FilterTempItems()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }
        private void FilterCurrentItems(string keyword)
        {
            try
            {
                var filteredItems =
                string.IsNullOrWhiteSpace(keyword) ?
                _allItemsCurrent :
                _allItemsCurrent.Where(i => i.ItemName.ToLower().Contains(keyword.ToLower()));

                TrendEntityCurrentItems = new ObservableCollection<TrendEntity>(filteredItems);
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog(" TrendViewModel FilterCurrentItems()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
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
                ErrorLogger.LogError.ErrorLog(" TrendViewModel ExitButtonCommandClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }

       //live current checkbox checked
        public void CurrentCheckedcmdClick(object objCommandParameter)
        {
            try
            {                //if user checkes on all 
                if (objCommandParameter.ToString() == "All")
                {
                    for (int i = 0; i < TrendEntityCurrentItems.Count; i++)
                    {
                        TrendEntityCurrentItems[i].isCurrentChecked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TrendViewModel CurrentCheckedcmdClick()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        public void CurrentUnCheckedcmdClick(object objCommandParameter)
        {
            try
            {
                if (objCommandParameter.ToString()!= "All") //if single station is unchecked then uncheck all also
                {
                    int index = 0;
                    TrendEntityCurrentItems[TrendEntityCurrentItems.Count-1].isCurrentChecked = false;  
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TrendViewModel CurrentUnCheckedcmdClick()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        //live current checkbox checked
        public void TempertureCheckedcmdClick(object objCommandParameter)
        {
            try
            {                //if user checkes on all 
                if (objCommandParameter.ToString() == "All")
                {
                    for (int i = 0; i < TrendEntityItems.Count; i++)
                    {
                        TrendEntityItems[i].isTempChecked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TrendViewModel TempertureCheckedcmdClick()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        public void TempertureUnCheckedcmdClick(object objCommandParameter)
        {
            try
            {
                if (objCommandParameter.ToString() != "All") //if single station is unchecked then uncheck all also
                {
                    int index = 0;
                    TrendEntityItems[TrendEntityItems.Count - 1].isTempChecked = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TrendViewModel TempertureUnCheckedcmdClick()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }

        private void DoWork(object Sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    #region live temp
                    try
                    {
                        int index = 0;

                        TemperatureTrendentity _TemperatureTrendentityNm = new TemperatureTrendentity();
                        foreach (var item in TrendEntityItems)
                        {
                            if (item.isTempChecked == true)
                            {
                                TemperatureValue = IndiSCADABusinessLogic.TrendLogic.GetTemperatureValue(item).ToString();//_trend;
                                _TemperatureTrendentityNm.LiveTime = DateTime.Now;
                                //_TemperatureTrendentityNm.TempStationName = item.ItemName;
                                if (item.ItemName == "All")
                                {
                                    for (int i = 0; i < TrendEntityItems.Count; i++)
                                    {
                                        TrendEntityItems[i].isTempChecked = true;
                                    }

                                    _TemperatureTrendentityNm.LiveTemp1 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[0]);
                                    _TemperatureTrendentityNm.LiveTemp2 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[1]);
                                    _TemperatureTrendentityNm.LiveTemp3 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[2]);
                                    _TemperatureTrendentityNm.LiveTemp4 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[3]);
                                    _TemperatureTrendentityNm.LiveTemp5 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[4]);
                                    _TemperatureTrendentityNm.LiveTemp6 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[5]);
                                    _TemperatureTrendentityNm.LiveTemp7 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[6]);
                                    _TemperatureTrendentityNm.LiveTemp8 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[7]);
                                    _TemperatureTrendentityNm.LiveTemp9 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[8]);
                                    _TemperatureTrendentityNm.LiveTemp10 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[9]);
                                    _TemperatureTrendentityNm.LiveTemp11 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[10]);
                                    _TemperatureTrendentityNm.LiveTemp12 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[11]);
                                    _TemperatureTrendentityNm.LiveTemp13 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[12]);
                                    _TemperatureTrendentityNm.LiveTemp14 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[13]);
                                    _TemperatureTrendentityNm.LiveTemp15 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[14]);
                                    _TemperatureTrendentityNm.LiveTemp16 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[15]);
                                    _TemperatureTrendentityNm.LiveTemp17 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[16]);
                                    _TemperatureTrendentityNm.LiveTemp18 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[17]);
                                    _TemperatureTrendentityNm.LiveTemp19 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[18]);
                                    _TemperatureTrendentityNm.LiveTemp20 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[19]);
                                    _TemperatureTrendentityNm.LiveTemp21 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[20]);
                                    _TemperatureTrendentityNm.LiveTemp22 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[21]);
                                    _TemperatureTrendentityNm.LiveTemp23 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[22]);
                                    _TemperatureTrendentityNm.LiveTemp24 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[23]);
                                    _TemperatureTrendentityNm.LiveTemp25 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.TemperatureActual[24]);
                                }
                                else if (index == 0)
                                {
                                    _TemperatureTrendentityNm.LiveTemp1 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 1)
                                {
                                    _TemperatureTrendentityNm.LiveTemp2 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 2)
                                {
                                    _TemperatureTrendentityNm.LiveTemp3 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 3)
                                {
                                    _TemperatureTrendentityNm.LiveTemp4 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 4)
                                {
                                    _TemperatureTrendentityNm.LiveTemp5 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 5)
                                {
                                    _TemperatureTrendentityNm.LiveTemp6 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 6)
                                {
                                    _TemperatureTrendentityNm.LiveTemp7 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 7)
                                {
                                    _TemperatureTrendentityNm.LiveTemp8 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 8)
                                {
                                    _TemperatureTrendentityNm.LiveTemp9 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 9)
                                {
                                    _TemperatureTrendentityNm.LiveTemp10 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 10)
                                {
                                    _TemperatureTrendentityNm.LiveTemp11 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 11)
                                {
                                    _TemperatureTrendentityNm.LiveTemp12 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 12)
                                {
                                    _TemperatureTrendentityNm.LiveTemp13 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 13)
                                {
                                    _TemperatureTrendentityNm.LiveTemp14 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 14)
                                {
                                    _TemperatureTrendentityNm.LiveTemp15 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 15)
                                {
                                    _TemperatureTrendentityNm.LiveTemp16 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 16)
                                {
                                    _TemperatureTrendentityNm.LiveTemp17 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 17)
                                {
                                    _TemperatureTrendentityNm.LiveTemp18 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 18)
                                {
                                    _TemperatureTrendentityNm.LiveTemp19 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 19)
                                {
                                    _TemperatureTrendentityNm.LiveTemp20 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 20)
                                {
                                    _TemperatureTrendentityNm.LiveTemp21 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 21)
                                {
                                    _TemperatureTrendentityNm.LiveTemp22 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 22)
                                {
                                    _TemperatureTrendentityNm.LiveTemp23 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 23)
                                {
                                    _TemperatureTrendentityNm.LiveTemp24 = Convert.ToDouble(TemperatureValue);
                                }
                                else if (index == 24)
                                {
                                    _TemperatureTrendentityNm.LiveTemp25 = Convert.ToDouble(TemperatureValue);
                                }
                                index++;
                            }
                        }
                        Data.Add(_TemperatureTrendentityNm);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog(" TrendViewModel GetTemperatureValue()", DateTime.Now.ToString(), ex.Message, "No", true);
                    } 

                    if (Data.Count > 10)
                    {
                        Data.RemoveAt(0);
                    }
                    #endregion


                    #region live current
                    try
                    {
                        int index = 0;
                       

                        RectifierTrendEntity _CurrentTrendentityNm = new RectifierTrendEntity();
                        int TotalCurrent = TrendEntityCurrentItems.Count-1;
                        foreach (var item in TrendEntityCurrentItems)
                        {
                            if (item.isCurrentChecked == true)
                            { 
                                CurrentValue = IndiSCADABusinessLogic.TrendLogic.GetCurrentValue(item).ToString();//_trend; 
                                _CurrentTrendentityNm.LiveTime = DateTime.Now; 
                              
                                if (item.ItemName == "All")
                                {
                                    for(int i=0;i< TrendEntityCurrentItems.Count;i++)
                                    {
                                        TrendEntityCurrentItems[i].isCurrentChecked = true; 
                                    } 

                                    _CurrentTrendentityNm.LiveRect1 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.ActualCurrent[0]);
                                    _CurrentTrendentityNm.LiveRect2 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.ActualCurrent[1]);
                                    _CurrentTrendentityNm.LiveRect3 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.ActualCurrent[2]);
                                    _CurrentTrendentityNm.LiveRect4 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.ActualCurrent[3]);
                                    _CurrentTrendentityNm.LiveRect5 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.ActualCurrent[4]);
                                    _CurrentTrendentityNm.LiveRect6 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.ActualCurrent[5]);
                                    _CurrentTrendentityNm.LiveRect7 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.ActualCurrent[6]);
                                    _CurrentTrendentityNm.LiveRect8 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.ActualCurrent[7]);
                                    _CurrentTrendentityNm.LiveRect9 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.ActualCurrent[8]);
                                    _CurrentTrendentityNm.LiveRect10 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.ActualCurrent[9]);
                                    _CurrentTrendentityNm.LiveRect11 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.ActualCurrent[10]);
                                    _CurrentTrendentityNm.LiveRect12 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.ActualCurrent[11]);
                                    _CurrentTrendentityNm.LiveRect13 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.ActualCurrent[12]);
                                    _CurrentTrendentityNm.LiveRect14 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.ActualCurrent[13]);
                                    _CurrentTrendentityNm.LiveRect15 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.ActualCurrent[14]);
                                    _CurrentTrendentityNm.LiveRect16 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.ActualCurrent[15]);
                                    _CurrentTrendentityNm.LiveRect17 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.ActualCurrent[16]);
                                    _CurrentTrendentityNm.LiveRect18 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.ActualCurrent[17]);
                                    _CurrentTrendentityNm.LiveRect19 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.ActualCurrent[18]);
                                    _CurrentTrendentityNm.LiveRect20 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.ActualCurrent[19]);
                                    _CurrentTrendentityNm.LiveRect21 = Convert.ToDouble(IndiSCADAGlobalLibrary.TagList.ActualCurrent[20]);

                                }
                                else if (index == 0 )
                                {
                                    _CurrentTrendentityNm.LiveRect1 = Convert.ToDouble(CurrentValue);
                                }
                                else if (index == 1)
                                {
                                    _CurrentTrendentityNm.LiveRect2 = Convert.ToDouble(CurrentValue);
                                }
                                else if (index == 2 )
                                {
                                    _CurrentTrendentityNm.LiveRect3 = Convert.ToDouble(CurrentValue);
                                }
                                else if (index == 3 )
                                {
                                    _CurrentTrendentityNm.LiveRect4 = Convert.ToDouble(CurrentValue);
                                }
                                else if (index == 4 )
                                {
                                    _CurrentTrendentityNm.LiveRect5 = Convert.ToDouble(CurrentValue);
                                }
                                else if (index == 5 )
                                {
                                    _CurrentTrendentityNm.LiveRect6 = Convert.ToDouble(CurrentValue);
                                }
                                else if (index == 6)
                                {
                                    _CurrentTrendentityNm.LiveRect7 = Convert.ToDouble(CurrentValue);
                                }
                                else if (index == 7 )
                                {
                                    _CurrentTrendentityNm.LiveRect8 = Convert.ToDouble(CurrentValue);
                                }
                                else if (index == 8)
                                {
                                    _CurrentTrendentityNm.LiveRect9 = Convert.ToDouble(CurrentValue);
                                }
                                else if (index == 9 )
                                {
                                    _CurrentTrendentityNm.LiveRect10 = Convert.ToDouble(CurrentValue);
                                }
                                else if (index == 10 )
                                {
                                    _CurrentTrendentityNm.LiveRect11 = Convert.ToDouble(CurrentValue);
                                }
                                else if (index == 11 )
                                {
                                    _CurrentTrendentityNm.LiveRect12 = Convert.ToDouble(CurrentValue);
                                }
                                else if (index == 12 )
                                {
                                    _CurrentTrendentityNm.LiveRect13 = Convert.ToDouble(CurrentValue);
                                }
                                else if (index == 13 )
                                {
                                    _CurrentTrendentityNm.LiveRect14 = Convert.ToDouble(CurrentValue);
                                }
                                else if (index == 14 )
                                {
                                    _CurrentTrendentityNm.LiveRect15 = Convert.ToDouble(CurrentValue);
                                }
                                else if (index == 15 )
                                {
                                    _CurrentTrendentityNm.LiveRect16 = Convert.ToDouble(CurrentValue);
                                }
                                else if (index == 16 )
                                {
                                    _CurrentTrendentityNm.LiveRect17 = Convert.ToDouble(CurrentValue);
                                }
                                else if (index == 17 )
                                {
                                    _CurrentTrendentityNm.LiveRect18 = Convert.ToDouble(CurrentValue);
                                }
                                else if (index == 18 )
                                {
                                    _CurrentTrendentityNm.LiveRect19 = Convert.ToDouble(CurrentValue);
                                }
                                else if (index == 19 )
                                {
                                    _CurrentTrendentityNm.LiveRect20 = Convert.ToDouble(CurrentValue);
                                }
                                else if (index == 20 )
                                {
                                    _CurrentTrendentityNm.LiveRect21 = Convert.ToDouble(CurrentValue);
                                }                            
                            } 

                            index++;
                            
                        }
                        CurrentData.Add(_CurrentTrendentityNm);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog(" TrendViewModel GetTemperatureValue()", DateTime.Now.ToString(), ex.Message, "No", true);
                    }

                    if (CurrentData.Count > 10)
                    {
                        CurrentData.RemoveAt(0);
                    }
                    #endregion


                    //if (SelectedItemTemperature != null)
                    //{
                    //    try
                    //    {
                    //        var r = new Random();
                    //        var now = DateTime.Now;

                    //        _trend += r.Next(0, 100); //_trend += r.Next(-8, 10);
                    //        TemperatureValue = IndiSCADABusinessLogic.TrendLogic.GetTemperatureValue(SelectedItemTemperature).ToString();//_trend;
                    //        ChartValuesTemperature.Add(new MeasureModel
                    //        {
                    //            DateTime = now,
                    //            Value = Convert.ToInt16(TemperatureValue),
                    //        });
                    //        SetAxisLimits(now);

                    //        //lets only use the last 150 values
                    //        if (ChartValuesTemperature.Count > 150) ChartValuesTemperature.RemoveAt(0);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        ErrorLogger.LogError.ErrorLog(" TrendViewModel GetTemperatureValue()", DateTime.Now.ToString(), ex.Message, "No", true);
                    //    }
                    //}

                    //if (SelectedItemCurrent != null)
                    //{
                    //    try
                    //    {
                    //        var r = new Random();
                    //        var now = DateTime.Now;

                    //        _trend += r.Next(-8, 10);
                    //        CurrentValue = IndiSCADABusinessLogic.TrendLogic.GetCurrentValue(SelectedItemCurrent).ToString();//_trend;
                    //        ChartValuesCurrent.Add(new MeasureModel
                    //        {
                    //            DateTime = now,
                    //            Value = Convert.ToInt16(CurrentValue),// IndiSCADABusinessLogic.TrendLogic.GetCurrentValue(SelectedItemCurrent)//_trend

                    //        });
                    //        SetAxisLimits(now);
                    //        //lets only use the last 150 values
                    //        if (ChartValuesCurrent.Count > 150) ChartValuesCurrent.RemoveAt(0);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        ErrorLogger.LogError.ErrorLog(" TrendViewModel GetCurrentValue()", DateTime.Now.ToString(), ex.Message, "No", true);
                    //    }
                    //}
                }));
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TrendViewModel DoWork()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        void DispatcherTickEvent(object sender, EventArgs e)
        {
            try
            {
                if (_BackgroundWorkerView.IsBusy != true)
                {
                    _BackgroundWorkerView.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TrendViewModel DispatcherTickEvent()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        #endregion

        #region Public/Private Property
        private ObservableCollection<TemperatureTrendentity> _Data = new ObservableCollection<TemperatureTrendentity>();
        public ObservableCollection<TemperatureTrendentity> Data
        {
            get => _Data;
            set
            {
                _Data = value;
                OnPropertyChanged("Data");

            }
        }
        private ObservableCollection<RectifierTrendEntity> _CurrentData = new ObservableCollection<RectifierTrendEntity>();
        public ObservableCollection<RectifierTrendEntity> CurrentData
        {
            get => _CurrentData;
            set
            {
                _CurrentData = value;
                OnPropertyChanged("CurrentData");

            }
        }


        public ChartValues<MeasureModel> ChartValuesTemperature { get; set; }
        public ChartValues<MeasureModel> ChartValuesCurrent { get; set; }
        public Func<double, string> DateTimeFormatter { get; set; }
        public double AxisStep { get; set; }
        public double AxisUnit { get; set; }
        public bool IsReading { get; set; }

        private string _RectifiervisibilityValue;
        public string RectifiervisibilityValue
        {
            get { return _RectifiervisibilityValue; }
            set { _RectifiervisibilityValue = value; OnPropertyChanged("RectifiervisibilityValue"); }
        }
        private string _TempvisibilityValue;
        public string TempvisibilityValue
        {
            get { return _TempvisibilityValue; }
            set { _TempvisibilityValue = value; OnPropertyChanged("TempvisibilityValue"); }
        }
        private string _CurrentValue ;
        public string CurrentValue
        {
            get { return _CurrentValue; }
            set { _CurrentValue = value; OnPropertyChanged("CurrentValue"); }
        }
        private string _TemperatureValue;
        public string TemperatureValue
        {
            get { return _TemperatureValue; }
            set { _TemperatureValue = value; OnPropertyChanged("TemperatureValue"); }
        }
        public double AxisMax
        {
            get { return _axisMax; }
            set
            {
                _axisMax = value;
                OnPropertyChanged("AxisMax");
            }
        }
        public double AxisMin
        {
            get { return _axisMin; }
            set
            {
                _axisMin = value;
                OnPropertyChanged("AxisMin");
            }
        }
      
        //temp
        private bool _isTempTrendPlay;
        public bool isTempTrendPlay
        {
            get => _isTempTrendPlay;
            set
            {
                _isTempTrendPlay = value;
                OnPropertyChanged("isTempTrendPlay");
            }
        }
        private bool _isTempTrendpause;
        public bool isTempTrendpause
        {
            get => _isTempTrendpause;
            set
            {
                _isTempTrendpause = value;
                OnPropertyChanged("isTempTrendpause");
            }
        }

        private ObservableCollection<TrendEntity> _allItems;
        private string _searchKeyword;
        public string SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                _searchKeyword = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TrendEntityItems)));
                FilterTempItems(_searchKeyword);
            }
        }
        private ObservableCollection<TrendEntity> _TrendEntityItems;
        public ObservableCollection<TrendEntity> TrendEntityItems
        {
            get => _TrendEntityItems;
            set
            {
                _TrendEntityItems = value;
                OnPropertyChanged("TrendEntityItems");
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TrendEntityItems)));
            }
        }
        private TrendEntity _selectedItem;
        public TrendEntity SelectedItemTemperature
        {
            get => _selectedItem;
            set
            {
                if (value == null || value.Equals(_selectedItem)) return;

                _selectedItem = value;
                OnPropertyChanged("SelectedItemTemperature");
            }
        }
        //
        //Current
        private bool _isOpenLeftDrawerCurrent;
        public bool isOpenLeftDrawerCurrent
        {
            get => _isOpenLeftDrawerCurrent;
            set
            {
                _isOpenLeftDrawerCurrent = value;
                OnPropertyChanged("isOpenLeftDrawerCurrent");
            }
        }
        private bool _isCurrentTrendPlay;
        public bool isCurrentTrendPlay
        {
            get => _isCurrentTrendPlay;
            set
            {
                _isCurrentTrendPlay = value;
                OnPropertyChanged("isCurrentTrendPlay");
            }
        }
        private bool _isCurrentTrendpause;
        public bool isCurrentTrendpause
        {
            get => _isCurrentTrendpause;
            set
            {
                _isCurrentTrendpause = value;
                OnPropertyChanged("isCurrentTrendpause");
            }
        }

        private ObservableCollection<TrendEntity> _allItemsCurrent;
        private string _searchKeywordCurrent;
        public string searchKeywordCurrent
        {
            get => _searchKeywordCurrent;
            set
            {
                _searchKeywordCurrent = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TrendEntityCurrentItems)));
                FilterCurrentItems(_searchKeywordCurrent);
            }
        }
        private ObservableCollection<TrendEntity> _TrendEntityCurrentItems;
        public ObservableCollection<TrendEntity> TrendEntityCurrentItems
        {
            get => _TrendEntityCurrentItems;
            set
            {
                _TrendEntityCurrentItems = value;
                OnPropertyChanged("TrendEntityCurrentItems");
                
            }
        }
        private TrendEntity _selectedCurrentItem;
        public TrendEntity SelectedItemCurrent
        {
            get => _selectedCurrentItem;
            set
            {
                if (value == null || value.Equals(_selectedCurrentItem)) return;

                _selectedCurrentItem = value;
                OnPropertyChanged("SelectedItemCurrent");
            }
        }
        //

        //current temp properties
        #region current properties
        private bool _isAnodic1CurrentSelected = false;
        public bool isAnodic1CurrentSelected
        {
            get => _isAnodic1CurrentSelected;
            set
            {
                _isAnodic1CurrentSelected = value;
                OnPropertyChanged("isAnodic1CurrentSelected");
                if (_isAnodic1CurrentSelected == true)
                {
                    RectifiervisibilityValue = "Visible";
                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAnodic2CurrentSelected = false;
        public bool isAnodic2CurrentSelected
        {
            get => _isAnodic2CurrentSelected;
            set
            {
                _isAnodic2CurrentSelected = value;
                OnPropertyChanged("isAnodic2CurrentSelected");
                if (_isAnodic2CurrentSelected == true)
                {
                    RectifiervisibilityValue = "Visible";

                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAnodic3CurrentSelected = false;
        public bool isAnodic3CurrentSelected
        {
            get => _isAnodic3CurrentSelected;
            set
            {
                _isAnodic3CurrentSelected = value;
                OnPropertyChanged("isAnodic3CurrentSelected");
                if (_isAnodic3CurrentSelected == true)
                {
                    RectifiervisibilityValue = "Visible";

                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }
        private bool _isAlZn1Selected = false;
        public bool isAlZn1Selected
        {
            get => _isAlZn1Selected;
            set
            {
                _isAlZn1Selected = value;
                OnPropertyChanged("isAlZn1Selected");
                if (_isAlZn1Selected == true)
                {
                    RectifiervisibilityValue = "Visible";

                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAlZn2Selected = false;
        public bool isAlZn2Selected
        {
            get => _isAlZn2Selected;
            set
            {
                _isAlZn2Selected = value;
                OnPropertyChanged("isAlZn2Selected");
                if (_isAlZn2Selected == true)
                {
                    RectifiervisibilityValue = "Visible";

                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAlZn3Selected = false;
        public bool isAlZn3Selected
        {
            get => _isAlZn3Selected;
            set
            {
                _isAlZn3Selected = value;
                OnPropertyChanged("isAlZn3Selected");
                if (_isAlZn3Selected == true)
                {
                    RectifiervisibilityValue = "Visible";

                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAlZn4Selected = false;
        public bool isAlZn4Selected
        {
            get => _isAlZn4Selected;
            set
            {
                _isAlZn4Selected = value;
                OnPropertyChanged("isAlZn4Selected");
                if (_isAlZn4Selected == true)
                {
                    RectifiervisibilityValue = "Visible";

                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAlZn5Selected = false;
        public bool isAlZn5Selected
        {
            get => _isAlZn5Selected;
            set
            {
                _isAlZn5Selected = value;
                OnPropertyChanged("isAlZn5Selected");
                if (_isAlZn5Selected == true)
                {
                    RectifiervisibilityValue = "Visible";

                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAlZn6Selected = false;
        public bool isAlZn6Selected
        {
            get => _isAlZn6Selected;
            set
            {
                _isAlZn6Selected = value;
                OnPropertyChanged("isAlZn6Selected");
                if (_isAlZn6Selected == true)
                {
                    RectifiervisibilityValue = "Visible";

                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAlZn7Selected = false;
        public bool isAlZn7Selected
        {
            get => _isAlZn7Selected;
            set
            {
                _isAlZn7Selected = value;
                OnPropertyChanged("isAlZn7Selected");
                if (_isAlZn7Selected == true)
                {
                    RectifiervisibilityValue = "Visible";

                   
                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAlZn8Selected = false;
        public bool isAlZn8Selected
        {
            get => _isAlZn8Selected;
            set
            {
                _isAlZn8Selected = value;
                OnPropertyChanged("isAlZn8Selected");
                if (_isAlZn8Selected == true)
                {
                    RectifiervisibilityValue = "Visible";

                   
                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAlZn9Selected = false;
        public bool isAlZn9Selected
        {
            get => _isAlZn9Selected;
            set
            {
                _isAlZn9Selected = value;
                OnPropertyChanged("isAlZn9Selected");
                if (_isAlZn9Selected == true)
                {
                    RectifiervisibilityValue = "Visible";

                   
                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAlZn10Selected = false;
        public bool isAlZn10Selected
        {
            get => _isAlZn10Selected;
            set
            {
                _isAlZn10Selected = value;
                OnPropertyChanged("isAlZn10Selected");
                if (_isAlZn10Selected == true)
                {
                    RectifiervisibilityValue = "Visible";


                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }
        private bool _isAlZn11Selected = false;
        public bool isAlZn11Selected
        {
            get => _isAlZn11Selected;
            set
            {
                _isAlZn11Selected = value;
                OnPropertyChanged("isAlZn11Selected");
                if (_isAlZn11Selected == true)
                {
                    RectifiervisibilityValue = "Visible";


                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }
        private bool _isAlZn12Selected = false;
        public bool isAlZn12Selected
        {
            get => _isAlZn12Selected;
            set
            {
                _isAlZn12Selected = value;
                OnPropertyChanged("isAlZn12Selected");
                if (_isAlZn12Selected == true)
                {
                    RectifiervisibilityValue = "Visible";


                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }
        private bool _isAlZn13Selected = false;
        public bool isAlZn13Selected
        {
            get => _isAlZn13Selected;
            set
            {
                _isAlZn13Selected = value;
                OnPropertyChanged("isAlZn13Selected");
                if (_isAlZn13Selected == true)
                {
                    RectifiervisibilityValue = "Visible";


                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }
        private bool _isAlZn14Selected = false;
        public bool isAlZn14Selected
        {
            get => _isAlZn14Selected;
            set
            {
                _isAlZn14Selected = value;
                OnPropertyChanged("isAlZn14Selected");
                if (_isAlZn14Selected == true)
                {
                    RectifiervisibilityValue = "Visible";


                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAlZn15Selected = false;
        public bool isAlZn15Selected
        {
            get => _isAlZn15Selected;
            set
            {
                _isAlZn15Selected = value;
                OnPropertyChanged("isAlZn15Selected");
                if (_isAlZn15Selected == true)
                {
                    RectifiervisibilityValue = "Visible";


                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAlZn16Selected = false;
        public bool isAlZn16Selected
        {
            get => _isAlZn16Selected;
            set
            {
                _isAlZn16Selected = value;
                OnPropertyChanged("isAlZn16Selected");
                if (_isAlZn16Selected == true)
                {
                    RectifiervisibilityValue = "Visible";


                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAlZn17Selected = false;
        public bool isAlZn17Selected
        {
            get => _isAlZn17Selected;
            set
            {
                _isAlZn17Selected = value;
                OnPropertyChanged("isAlZn17Selected");
                if (_isAlZn17Selected == true)
                {
                    RectifiervisibilityValue = "Visible";


                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAlZn18Selected = false;
        public bool isAlZn18Selected
        {
            get => _isAlZn18Selected;
            set
            {
                _isAlZn18Selected = value;
                OnPropertyChanged("isAlZn18Selected");
                if (_isAlZn18Selected == true)
                {
                    RectifiervisibilityValue = "Visible";


                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }


        private bool _istemp20Selected = false;
        public bool istemp20Selected
        {
            get => _istemp20Selected;
            set
            {
                _istemp20Selected = value;
                OnPropertyChanged("istemp20Selected");
                if (_istemp20Selected == true)
                {
                    RectifiervisibilityValue = "Visible";


                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }
        private bool _istemp21Selected = false;
        public bool istemp21Selected
        {
            get => _istemp21Selected;
            set
            {
                _istemp21Selected = value;
                OnPropertyChanged("istemp21Selected");
                if (_istemp21Selected == true)
                {
                    RectifiervisibilityValue = "Visible";


                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }
        private bool _istemp22Selected = false;
        public bool istemp22Selected
        {
            get => _istemp22Selected;
            set
            {
                _istemp22Selected = value;
                OnPropertyChanged("istemp22Selected");
                if (_istemp22Selected == true)
                {
                    RectifiervisibilityValue = "Visible";


                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }
        private bool _istemp23Selected = false;
        public bool istemp23Selected
        {
            get => _istemp23Selected;
            set
            {
                _istemp23Selected = value;
                OnPropertyChanged("istemp23Selected");
                if (_istemp23Selected == true)
                {
                    RectifiervisibilityValue = "Visible";


                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }
        private bool _istemp24Selected = false;
        public bool istemp24Selected
        {
            get => _istemp24Selected;
            set
            {
                _istemp24Selected = value;
                OnPropertyChanged("istemp24Selected");
                if (_istemp24Selected == true)
                {
                    RectifiervisibilityValue = "Visible";


                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }
        private bool _istemp25Selected = false;
        public bool istemp25Selected
        {
            get => _istemp25Selected;
            set
            {
                _istemp25Selected = value;
                OnPropertyChanged("istemp25Selected");
                if (_istemp25Selected == true)
                {
                    RectifiervisibilityValue = "Visible";


                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAll = false;
        public bool isAll
        {
            get => _isAll;
            set
            {
                _isAll = value;
                OnPropertyChanged("isAll");
                if (_isAll == true)
                {
                    RectifiervisibilityValue = "Visible";

                    isAnodic1CurrentSelected = false;
                    isAnodic2CurrentSelected = false;
                    isAlZn1Selected = false;
                    isAlZn2Selected = false;
                    isAlZn3Selected = false;
                    isAlZn4Selected = false;
                    isAlZn5Selected = false;
                    isAlZn6Selected = false;
                    isAlZn7Selected = false;
                    isAlZn8Selected = false;
                    isAlZn9Selected = false;
                    isAlZn10Selected = false;
                    isAlZn11Selected = false;
                    isAlZn12Selected = false;
                    isAlZn13Selected = false;
                    isAlZn14Selected = false;
                    isAlZn15Selected = false;
                    isAlZn16Selected = false;
                    isAlZn17Selected = false;
                    isAlZn18Selected = false;
                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }
        

            
        #endregion
        #region temperature properties
        private bool _isSoakDegressingTempSelected = false;
        public bool isSoakDegressingTempSelected
        {
            get => _isSoakDegressingTempSelected;
            set
            {
                _isSoakDegressingTempSelected = value;
                OnPropertyChanged("isSoakDegressingTempSelected");
                if (_isSoakDegressingTempSelected == true)
                {
                    TempvisibilityValue = "Visible";

                }
                else
                {
                    TempvisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAnodic1TempSelected = false;
        public bool isAnodic1TempSelected
        {
            get => _isAnodic1TempSelected;
            set
            {
                _isAnodic1TempSelected = value;
                OnPropertyChanged("isAnodic1TempSelected");
                if (_isAnodic1TempSelected == true)
                {
                    TempvisibilityValue = "Visible";

                }
                else
                {
                    TempvisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAnodic2TempSelected = false;
        public bool isAnodic2TempSelected
        {
            get => _isAnodic2TempSelected;
            set
            {
                _isAnodic2TempSelected = value;
                OnPropertyChanged("isAnodic2TempSelected");
                if (_isAnodic2TempSelected == true)
                {
                    TempvisibilityValue = "Visible";

                }
                else
                {
                    TempvisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAlkalineZinc1TempSelected = false;
        public bool isAlkalineZinc1TempSelected
        {
            get => _isAlkalineZinc1TempSelected;
            set
            {
                _isAlkalineZinc1TempSelected = value;
                OnPropertyChanged("isAlkalineZinc1TempSelected");
                if (_isAlkalineZinc1TempSelected == true)
                {
                    TempvisibilityValue = "Visible";

                }
                else
                {
                    TempvisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAlkalineZinc2TempSelected = false;
        public bool isAlkalineZinc2TempSelected
        {
            get => _isAlkalineZinc2TempSelected;
            set
            {
                _isAlkalineZinc2TempSelected = value;
                OnPropertyChanged("isAlkalineZinc2TempSelected");
                if (_isAlkalineZinc2TempSelected == true)
                {
                    TempvisibilityValue = "Visible";

                }
                else
                {
                    TempvisibilityValue = "Hidden";
                }
            }
        }

     
        private bool _isAlkalineZinc3TempSelected = false;
        public bool isAlkalineZinc3TempSelected
        {
            get => _isAlkalineZinc3TempSelected;
            set
            {
                _isAlkalineZinc3TempSelected = value;
                OnPropertyChanged("isAlkalineZinc3TempSelected");
                if (_isAlkalineZinc3TempSelected == true)
                {
                    TempvisibilityValue = "Visible";
                }
                else
                {
                    TempvisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAlkalineZinc4TempSelected = false;
        public bool isAlkalineZinc4TempSelected
        {
            get => _isAlkalineZinc4TempSelected;
            set
            {
                _isAlkalineZinc4TempSelected = value;
                OnPropertyChanged("isAlkalineZinc4TempSelected");
                if (_isAlkalineZinc4TempSelected == true)
                {
                    TempvisibilityValue = "Visible";
                }
                else
                {
                    TempvisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAlkalineZinc5TempSelected = false;
        public bool isAlkalineZinc5TempSelected
        {
            get => _isAlkalineZinc5TempSelected;
            set
            {
                _isAlkalineZinc5TempSelected = value;
                OnPropertyChanged("isAlkalineZinc5TempSelected");
                if (_isAlkalineZinc5TempSelected == true)
                {
                    TempvisibilityValue = "Visible";
                }
                else
                {
                    TempvisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAlkalineZinc6TempSelected = false;
        public bool isAlkalineZinc6TempSelected
        {
            get => _isAlkalineZinc6TempSelected;
            set
            {
                _isAlkalineZinc6TempSelected = value;
                OnPropertyChanged("isAlkalineZinc6TempSelected");
                if (_isAlkalineZinc6TempSelected == true)
                {
                    TempvisibilityValue = "Visible";
                }
                else
                {
                    TempvisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAlkalineZinc7TempSelected = false;
        public bool isAlkalineZinc7TempSelected
        {
            get => _isAlkalineZinc7TempSelected;
            set
            {
                _isAlkalineZinc7TempSelected = value;
                OnPropertyChanged("isAlkalineZinc7TempSelected");
                if (_isAlkalineZinc7TempSelected == true)
                {
                    TempvisibilityValue = "Visible";
                }
                else
                {
                    TempvisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAlkalineZinc8TempSelected = false;
        public bool isAlkalineZinc8TempSelected
        {
            get => _isAlkalineZinc8TempSelected;
            set
            {
                _isAlkalineZinc8TempSelected = value;
                OnPropertyChanged("isAlkalineZinc8TempSelected");
                if (_isAlkalineZinc8TempSelected == true)
                {
                    TempvisibilityValue = "Visible";

                }
                else
                {
                    TempvisibilityValue = "Hidden";
                }
            }
        }

        private bool _isAlkalineZinc9TempSelected = false;
        public bool isAlkalineZinc9TempSelected
        {
            get => _isAlkalineZinc9TempSelected;
            set
            {
                _isAlkalineZinc9TempSelected = value;
                OnPropertyChanged("isAlkalineZinc9TempSelected");
                if (_isAlkalineZinc9TempSelected == true)
                {
                    TempvisibilityValue = "Visible";

                }
                else
                {
                    TempvisibilityValue = "Hidden";
                }
            }
        }

        private bool _isPass1TempSelected = false;
        public bool isPass1TempSelected
        {
            get => _isPass1TempSelected;
            set
            {
                _isPass1TempSelected = value;
                OnPropertyChanged("isPass1TempSelected");
                if (_isPass1TempSelected == true)
                {
                    TempvisibilityValue = "Visible";

                }
                else
                {
                    TempvisibilityValue = "Hidden";
                }
            }
        }

        private bool _isPass2TempSelected = false;
        public bool isPass2TempSelected
        {
            get => _isPass2TempSelected;
            set
            {
                _isPass2TempSelected = value;
                OnPropertyChanged("isPass2TempSelected");
                if (_isPass2TempSelected == true)
                {
                    TempvisibilityValue = "Visible";

                }
                else
                {
                    TempvisibilityValue = "Hidden";
                }
            }
        }
        private bool _isPass3TempSelected = false;
        public bool isPass3TempSelected
        {
            get => _isPass3TempSelected;
            set
            {
                _isPass3TempSelected = value;
                OnPropertyChanged("isPass3TempSelected");
                if (_isAlkalineZinc9TempSelected == true)
                {
                    TempvisibilityValue = "Visible";

                }
                else
                {
                    TempvisibilityValue = "Hidden";
                }
            }
        }
        private bool _isPass4TempSelected = false;
        public bool isPass4TempSelected
        {
            get => _isPass4TempSelected;
            set
            {
                _isPass4TempSelected = value;
                OnPropertyChanged("isPass4TempSelected");
                if (_isAlkalineZinc9TempSelected == true)
                {
                    TempvisibilityValue = "Visible";

                }
                else
                {
                    TempvisibilityValue = "Hidden";
                }
            }
        }
        private bool _isPass5TempSelected = false;
        public bool isPass5TempSelected
        {
            get => _isPass5TempSelected;
            set
            {
                _isPass5TempSelected = value;
                OnPropertyChanged("isPass5TempSelected");
                if (_isPass5TempSelected == true)
                {
                    TempvisibilityValue = "Visible";

                }
                else
                {
                    TempvisibilityValue = "Hidden";
                }
            }
        }
        private bool _isDryer1TempSelected = false;
        public bool isDryer1TempSelected
        {
            get => _isDryer1TempSelected;
            set
            {
                _isDryer1TempSelected = value;
                OnPropertyChanged("isDryer1TempSelected");
                if (_isDryer1TempSelected == true)
                {
                    TempvisibilityValue = "Visible";

                }
                else
                {
                    TempvisibilityValue = "Hidden";
                }
            }
        }
        private bool _isDryer2TempSelected = false;
        public bool isDryer2TempSelected
        {
            get => _isDryer2TempSelected;
            set
            {
                _isDryer2TempSelected = value;
                OnPropertyChanged("isDryer2TempSelected");
                if (_isDryer2TempSelected == true)
                {
                    TempvisibilityValue = "Visible";

                }
                else
                {
                    TempvisibilityValue = "Hidden";
                }
            }
        }



        private bool _isAllTempSelected = false;
        public bool isAllTempSelected
        {
            get => _isAllTempSelected;
            set
            {
                _isAllTempSelected = value;
                OnPropertyChanged("isAllTempSelected");
                if (_isAllTempSelected == true)
                {
                    TempvisibilityValue = "Visible";
                    isSoakDegressingTempSelected = false;
                    isAnodic1TempSelected = false;
                    isAnodic2TempSelected = false;
                    isAlkalineZinc1TempSelected = false;
                    isAlkalineZinc2TempSelected = false;
                    isAlkalineZinc3TempSelected = false;
                    isAlkalineZinc4TempSelected = false;
                    isAlkalineZinc5TempSelected = false;
                    isAlkalineZinc6TempSelected = false;
                    isAlkalineZinc7TempSelected = false;
                    isAlkalineZinc8TempSelected = false;
                    isAlkalineZinc9TempSelected = false;
                    isPass1TempSelected = false;
                    isPass2TempSelected = false;
                    isPass3TempSelected = false;
                    isPass4TempSelected = false;
                    isPass5TempSelected = false;
                    isDryer1TempSelected = false;
                    isDryer2TempSelected = false;
                }
                else
                {
                    TempvisibilityValue = "Hidden";
                }
            }
        }

        #endregion

        private bool _isTemperatureSelected=false;
        public bool isTemperatureSelected
        {
            get => _isTemperatureSelected;
            set
            {
                _isTemperatureSelected = value;
                OnPropertyChanged("isTemperatureSelected");
                if (_isTemperatureSelected== true)
                {
                    TempvisibilityValue = "Visible";
                    isRectifierSelected = false;
                    //isOpenTempTrendBPopup = true;
                    //isOpenRectTrendBPopup = false;
                }
                else
                {
                    TempvisibilityValue = "Hidden";
                }
            }
        }



        private bool _isRectifierSelected = false;
        public bool isRectifierSelected
        {
            get => _isRectifierSelected;
            set
            {
                _isRectifierSelected = value;
                OnPropertyChanged("isRectifierSelected");
                if (_isRectifierSelected == true)
                {
                    RectifiervisibilityValue = "Visible";
                    isTemperatureSelected = false;
                    //isOpenRectTrendBPopup = true;
                    //isOpenTempTrendBPopup = false;
                }
                else
                {
                    RectifiervisibilityValue = "Hidden";
                }
            }
        }
        private DateTime _SelectedStartDate = DateTime.Now;
        public DateTime SelectedStartDate
        {
            get { return _SelectedStartDate; }
            set { _SelectedStartDate = value; OnPropertyChanged("SelectedStartDate"); }
        }
        private DateTime _SelectedStartTime = DateTime.Now;
        public DateTime SelectedStartTime
        {
            get { return _SelectedStartTime; }
            set { _SelectedStartTime = value; OnPropertyChanged("SelectedStartTime"); }
        }
        private DateTime _SelectedEndDate = DateTime.Now;
        public DateTime SelectedEndDate
        {
            get { return _SelectedEndDate; }
            set { _SelectedEndDate = value; OnPropertyChanged("SelectedEndDate"); }
        }
        private DateTime _SelectedEndTime = DateTime.Now;
        public DateTime SelectedEndTime
        {
            get { return _SelectedEndTime; }
            set { _SelectedEndTime = value; OnPropertyChanged("SelectedEndTime"); }
        }
        private ObservableCollection<TemperatureTrendentity> _HistoricalTempTrend=new ObservableCollection<TemperatureTrendentity>();
        public ObservableCollection<TemperatureTrendentity> HistoricalTempTrend
        {
            get => _HistoricalTempTrend;
            set
            {
                _HistoricalTempTrend = value;
                OnPropertyChanged("HistoricalTempTrend");
                
            }
        }
        private ObservableCollection<RectifierTrendEntity> _HistoricalCurrentTrend = new ObservableCollection<RectifierTrendEntity>();
        public ObservableCollection<RectifierTrendEntity> HistoricalCurrentTrend
        {
            get => _HistoricalCurrentTrend;
            set
            {
                _HistoricalCurrentTrend = value;
                OnPropertyChanged("HistoricalCurrentTrend");

            }
        }

        private ObservableCollection<TemperatureTrendentity> _NewHistoricalTempTrend = new ObservableCollection<TemperatureTrendentity>();
        public ObservableCollection<TemperatureTrendentity> NewHistoricalTempTrend
        {
            get => _NewHistoricalTempTrend;
            set
            {
                _NewHistoricalTempTrend = value;
                OnPropertyChanged("NewHistoricalTempTrend");

            }
        }
        private ObservableCollection<RectifierTrendEntity> _NewHistoricalCurrentTrend = new ObservableCollection<RectifierTrendEntity>();
        public ObservableCollection<RectifierTrendEntity> NewHistoricalCurrentTrend
        {
            get => _NewHistoricalCurrentTrend;
            set
            {
                _NewHistoricalCurrentTrend = value;
                OnPropertyChanged("NewHistoricalCurrentTrend");

            }
        }
        #endregion

         

    }
}
