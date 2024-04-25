using IndiSCADAEntity.Entity;
using IndiSCADAGlobalLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADABusinessLogic
{
    public class TrendLogic
    {
        #region"Declaration"
        //Read only once Rectifier Station Names
        static ServiceResponse<IList> _RectifierStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("Rectifier");
        //Read only once Temperature Station Names
        static ServiceResponse<IList> _TemperatureStationName = IndiSCADATranslation.IOStatusTranslation.GetparameterNameFromDB("TemperatureSetting");

        #endregion


        #region Public/private methods
        public static ObservableCollection<TrendEntity> ReturnStationName(string parameter)
        {
            ObservableCollection<TrendEntity> _Result = new ObservableCollection<TrendEntity>();
            try
            {
               if (parameter == "Temperature")
                {
                    if (_TemperatureStationName.Response != null)
                    {
                        IList<IOStatusEntity> _ListParameterName = (IList<IOStatusEntity>)(_TemperatureStationName.Response);
                        foreach (var item in _ListParameterName)
                        {
                            TrendEntity _itemtoAdd = new TrendEntity();
                            _itemtoAdd.ItemName = item.ParameterName;
                            _itemtoAdd.isTempChecked = false;
                            _Result.Add(_itemtoAdd);
                        }
                        TrendEntity _itemtoAddAll = new TrendEntity();
                        _itemtoAddAll.ItemName = "All";
                        _itemtoAddAll.isTempChecked = true;
                        _Result.Add(_itemtoAddAll);
                    }
                }
               if (parameter == "Rectifier")
                {
                    if (_RectifierStationName.Response != null)
                    {
                        IList<IOStatusEntity> _ListParameterName = (IList<IOStatusEntity>)(_RectifierStationName.Response);
                        foreach (var item in _ListParameterName)
                        {
                            TrendEntity _itemtoAdd = new TrendEntity();
                            _itemtoAdd.ItemName = item.ParameterName;
                            _itemtoAdd.isCurrentChecked = false;
                            _Result.Add(_itemtoAdd);
                        }
                        TrendEntity _itemtoAddAll = new TrendEntity();
                        _itemtoAddAll.ItemName = "All";
                        _itemtoAddAll.isCurrentChecked = true;
                        _Result.Add(_itemtoAddAll); 
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TrendLogic ReturnStationName()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _Result;
        }
        public static double ConvertToDouble(string Value)
        {
            try
            {
                return  Convert.ToDouble(Value);
            }
            catch(Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("Error TrendLogic ConvertToDouble() value:"+Value+" ", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return 0;
            }
        }
        public static double GetCurrentValue(TrendEntity _TrendEntity)
        {
            try
            {
                ///Actual Current
                string[] ActualCurrent =IndiSCADAGlobalLibrary.TagList.ActualCurrent; //{ "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21"};  //
                if (_RectifierStationName.Response != null)
                {
                    if (ActualCurrent.Length > 0)
                    {
                        IList<IOStatusEntity> lstRectifier = (IList<IOStatusEntity>)(_RectifierStationName.Response);
                        var index = lstRectifier.IndexOf(lstRectifier.Where(X => X.ParameterName == _TrendEntity.ItemName).FirstOrDefault());
                        return ConvertToDouble(ActualCurrent[index]);
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TrendLogic GetCurrentValue()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return 0;
            }
        }
        public static double GetTemperatureValue(TrendEntity _TrendEntity)
        {
            try
            {
                ///Temperature    
                string[] Temperature = IndiSCADAGlobalLibrary.TagList.TemperatureActual; // { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25" };  //
                if (_TemperatureStationName.Response != null)
                {
                    if (Temperature.Length > 0)
                    {
                        IList<IOStatusEntity> lstTemperature = (IList<IOStatusEntity>)(_TemperatureStationName.Response);
                        var index = lstTemperature.IndexOf(lstTemperature.Where(X => X.ParameterName == _TrendEntity.ItemName).FirstOrDefault());
                        return ConvertToDouble(Temperature[index]);
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TrendLogic GetTemperatureValue()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return 0;
            }
        }
        public static double GetVoltageValue(TrendEntity _TrendEntity)
        {
            try
            {
                ///Voltage
                string[] Voltage = IndiSCADAGlobalLibrary.TagList.TemperatureActual;
                if (_RectifierStationName.Response != null)
                {
                    IList<IOStatusEntity> lstRectifier = (IList<IOStatusEntity>)(_RectifierStationName.Response);
                    var index = lstRectifier.IndexOf(lstRectifier.Where(X => X.ParameterName == _TrendEntity.ItemName).FirstOrDefault());
                    return ConvertToDouble(Voltage[index]);
                }
                return 0;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TrendLogic GetVoltageValue()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return 0;
            }
        }
       public static ObservableCollection<TemperatureTrendentity> GetTempratureData(DateTime StartDate,DateTime EndDate)
        {
            ObservableCollection<TemperatureTrendentity> _resultOP = new ObservableCollection<TemperatureTrendentity>();
            try
            {
                ServiceResponse<IList> _result = IndiSCADATranslation.TrendViewTranslation.GetTemperatureHistoricalData(StartDate, EndDate);
                if (_result.Response !=null)
                {
                    if (_result.Status== ResponseType.S)
                    {
                        IList<TemperatureTrendentity> _List =(IList<TemperatureTrendentity>) ( _result.Response);
                        _resultOP = new ObservableCollection<TemperatureTrendentity>(_List);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TrendLogic GetTempratureData()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _resultOP;
        }
        public static ObservableCollection<RectifierTrendEntity> GetCurrentData(DateTime StartDate, DateTime EndDate)
        {
            ObservableCollection<RectifierTrendEntity> _resultOP = new ObservableCollection<RectifierTrendEntity>();
            try
            {
                ServiceResponse<IList> _result = IndiSCADATranslation.TrendViewTranslation.GetCurrentHistoricalData(StartDate, EndDate);
                if (_result.Response != null)
                {
                    if (_result.Status == ResponseType.S)
                    {
                        IList<RectifierTrendEntity> _List = (IList<RectifierTrendEntity>)(_result.Response);


                        //foreach (var p in _List)
                        //{
                        //    p.ToString();
                        //}

                        _resultOP = new ObservableCollection<RectifierTrendEntity>(_List);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TrendLogic GetCurrentData()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _resultOP;
        }
        #endregion
    }
}
