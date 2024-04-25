using IndiSCADAEntity.Entity;
using IndiSCADAGlobalLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADATranslation
{
    public class TrendViewTranslation
    {
        #region Public Private  method
        public static ServiceResponse<IList> GetTemperatureHistoricalData(DateTime StartDate, DateTime EndDate)
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.TrendHistoricalData(StartDate, EndDate, "Temperature");
                if (_LocalData.HasError())
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = _LocalData.Message;
                    _FinalTranslationResponse.Status = ResponseType.E;
                    _FinalTranslationResponse.ErrorLevel = ErrorLevel.T;
                    return _FinalTranslationResponse;
                }
                //Translation
                if (_LocalData.Response != null)
                {
                    if (_LocalData.Response.Rows.Count > 0)
                    {
                        if (_LocalData.Response.Rows.Count > 0)
                        {
                            var _Query = (from DataRow dr in _LocalData.Response.Rows
                                          select new TemperatureTrendentity()
                                          {
                                              DateTimeCol = Convert.ToDateTime((dr["DateTimeCol"].ToString())),

                                              SoakDegreasing = Convert.ToInt32((dr["SoakCleaning1Temp"].ToString())),
                                              Anodic1 = Convert.ToInt32((dr["SoakCleaning23Temp"].ToString())),
                                              Anodic2 = Convert.ToInt32((dr["CascadeRinse2Temp"].ToString())),
                                              AlZinc1 = Convert.ToInt32((dr["AnodicCleaning1Temp"].ToString())),
                                              AlZinc2 = Convert.ToInt32((dr["AnodicCleaning2Temp"].ToString())),
                                              AlZinc3 = Convert.ToInt32((dr["AnodicCleaning3Temp"].ToString())),
                                              AlZinc4 = Convert.ToInt32((dr["AlkalineZinc12Temp"].ToString())),
                                              AlZinc5 = Convert.ToInt32((dr["AlkalineZinc34Temp"].ToString())),
                                              AlZinc6 = Convert.ToInt32((dr["AlkalineZinc56Temp"].ToString())),
                                              AlZinc7 = Convert.ToInt32((dr["AlkalineZinc78Temp"].ToString())),
                                              AlZinc8 = Convert.ToInt32((dr["AlkalineZinc910Temp"].ToString())),
                                              AlZinc9 = Convert.ToInt32((dr["AlkalineZinc1112Temp"].ToString())),
                                              Pass1 = Convert.ToInt32((dr["AlkalineZinc1314Temp"].ToString())),
                                              Pass2 = Convert.ToInt32((dr["AlkalineZinc1516Temp"].ToString())),
                                              Pass3 = Convert.ToInt32((dr["AlkalineZinc1718Temp"].ToString())),
                                              Pass4 = Convert.ToInt32((dr["Passivation1Temp"].ToString())),
                                              Pass5 = Convert.ToInt32((dr["Passivation2Temp"].ToString())),
                                              Dryer1 = Convert.ToInt32((dr["Passivation3Temp"].ToString())),
                                              Dryer2 = Convert.ToInt32((dr["WaterRinseTemp"].ToString())),


                                              Temp20 = Convert.ToInt32((dr["TopCoat1Temp"].ToString())),
                                              Temp21 = Convert.ToInt32((dr["TopCoat2Temp"].ToString())),
                                              Temp22 = Convert.ToInt32((dr["TopCoat3Temp"].ToString())),
                                              Temp23 = Convert.ToInt32((dr["Dryer1Temp"].ToString())),
                                              Temp24 = Convert.ToInt32((dr["Dryer2Temp"].ToString())),
                                              Temp25 = Convert.ToInt32((dr["Dryer3Temp"].ToString())),
                                          });
                            _FinalTranslationResponse.Response = _Query.ToList<TemperatureTrendentity>();
                            //}
                            _FinalTranslationResponse.Message = "Data Received Successfuly";
                            _FinalTranslationResponse.Status = ResponseType.S;
                        }
                        else
                        {
                            _FinalTranslationResponse.Response = null;
                            _FinalTranslationResponse.Message = "Data Received Successfuly";
                            _FinalTranslationResponse.Status = ResponseType.S;
                        }
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
            }
            catch (Exception Ex)
            {
                _FinalTranslationResponse.Response = null;
                _FinalTranslationResponse.Message = Ex.Message;
                _FinalTranslationResponse.Status = ResponseType.E;
                _FinalTranslationResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalTranslationResponse;
        }
        public static ServiceResponse<IList> GetCurrentHistoricalData(DateTime StartDate, DateTime EndDate)
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.TrendHistoricalData(StartDate, EndDate, "Current");
                if (_LocalData.HasError())
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = _LocalData.Message;
                    _FinalTranslationResponse.Status = ResponseType.E;
                    _FinalTranslationResponse.ErrorLevel = ErrorLevel.T;
                    return _FinalTranslationResponse;
                }
                //Translation
                if (_LocalData.Response != null)
                {
                    if (_LocalData.Response.Rows.Count > 0)
                    {
                        
                            var _Query = (from DataRow dr in _LocalData.Response.Rows
                                          select new RectifierTrendEntity()
                                          {


                                              DateTimeCol = Convert.ToDateTime((dr["DateTimeCol"].ToString())),

                                              Anodic1SetCurrent = Convert.ToInt32((dr["Anodic1SPCurrent"].ToString())),
                                              Anodic1ActualCurrent = Convert.ToInt32((dr["Anodic1Current"].ToString())),

                                              Anodic2SetCurrent = Convert.ToInt32((dr["Anodic2SPCurrent"].ToString())),
                                              Anodic2ActualCurrent = Convert.ToInt32((dr["Anodic2Current"].ToString())),
                                              
                                              Anodic3SetCurrent = Convert.ToInt32((dr["Anodic3SPCurrent"].ToString())),
                                              Anodic3ActualCurrent = Convert.ToInt32((dr["Anodic3Current"].ToString())),

                                              AlZn1SetCurrent = Convert.ToInt32((dr["AlkalineZinc1SPCurrent"].ToString())),
                                              AlZn1ActualCurrent = Convert.ToInt32((dr["AlkalineZinc1Current"].ToString())),

                                              AlZn2SetCurrent = Convert.ToInt32((dr["AlkalineZinc2SPCurrent"].ToString())),
                                              AlZn2ActualCurrent = Convert.ToInt32((dr["AlkalineZinc2Current"].ToString())),

                                              AlZn3SetCurrent = Convert.ToInt32((dr["AlkalineZinc3SPCurrent"].ToString())),
                                              AlZn3ActualCurrent = Convert.ToInt32((dr["AlkalineZinc3Current"].ToString())),

                                              AlZn4SetCurrent = Convert.ToInt32((dr["AlkalineZinc4SPCurrent"].ToString())),
                                              AlZn4ActualCurrent = Convert.ToInt32((dr["AlkalineZinc4Current"].ToString())),

                                              AlZn5SetCurrent = Convert.ToInt32((dr["AlkalineZinc5SPCurrent"].ToString())),
                                              AlZn5ActualCurrent = Convert.ToInt32((dr["AlkalineZinc5Current"].ToString())),

                                              AlZn6SetCurrent = Convert.ToInt32((dr["AlkalineZinc6SPCurrent"].ToString())),
                                              AlZn6ActualCurrent = Convert.ToInt32((dr["AlkalineZinc6Current"].ToString())),

                                              AlZn7SetCurrent = Convert.ToInt32((dr["AlkalineZinc7SPCurrent"].ToString())),
                                              AlZn7ActualCurrent = Convert.ToInt32((dr["AlkalineZinc7Current"].ToString())),

                                              AlZn8SetCurrent = Convert.ToInt32((dr["AlkalineZinc8SPCurrent"].ToString())),
                                              AlZn8ActualCurrent = Convert.ToInt32((dr["AlkalineZinc8Current"].ToString())),

                                              AlZn9SetCurrent = Convert.ToInt32((dr["AlkalineZinc9SPCurrent"].ToString())),
                                              AlZn9ActualCurrent = Convert.ToInt32((dr["AlkalineZinc9Current"].ToString())),

                                              AlZn10SetCurrent = Convert.ToInt32((dr["AlkalineZinc10SPCurrent"].ToString())),
                                              AlZn10ActualCurrent = Convert.ToInt32((dr["AlkalineZinc10Current"].ToString())),

                                              AlZn11SetCurrent = Convert.ToInt32((dr["AlkalineZinc11SPCurrent"].ToString())),
                                              AlZn11ActualCurrent = Convert.ToInt32((dr["AlkalineZinc11Current"].ToString())),

                                              AlZn12SetCurrent = Convert.ToInt32((dr["AlkalineZinc12SPCurrent"].ToString())),
                                              AlZn12ActualCurrent = Convert.ToInt32((dr["AlkalineZinc12Current"].ToString())),

                                              AlZn13SetCurrent = Convert.ToInt32((dr["AlkalineZinc13SPCurrent"].ToString())),
                                              AlZn13ActualCurrent = Convert.ToInt32((dr["AlkalineZinc13Current"].ToString())),

                                              AlZn14SetCurrent = Convert.ToInt32((dr["AlkalineZinc14SPCurrent"].ToString())),
                                              AlZn14ActualCurrent = Convert.ToInt32((dr["AlkalineZinc14Current"].ToString())),

                                              AlZn15SetCurrent = Convert.ToInt32((dr["AlkalineZinc15SPCurrent"].ToString())),
                                              AlZn15ActualCurrent = Convert.ToInt32((dr["AlkalineZinc15Current"].ToString())),

                                              AlZn16SetCurrent = Convert.ToInt32((dr["AlkalineZinc16SPCurrent"].ToString())),
                                              AlZn16ActualCurrent = Convert.ToInt32((dr["AlkalineZinc16Current"].ToString())),

                                              AlZn17SetCurrent = Convert.ToInt32((dr["AlkalineZinc17SPCurrent"].ToString())),
                                              AlZn17ActualCurrent = Convert.ToInt32((dr["AlkalineZinc17Current"].ToString())),

                                              AlZn18SetCurrent = Convert.ToInt32((dr["AlkalineZinc18SPCurrent"].ToString())),
                                              AlZn18ActualCurrent = Convert.ToInt32((dr["AlkalineZinc18Current"].ToString())),

                                          });
                        //}
                        _FinalTranslationResponse.Response = _Query.ToList<RectifierTrendEntity>();
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                    else
                    {
                        _FinalTranslationResponse.Response = null;
                        _FinalTranslationResponse.Message = "Data Received Successfuly";
                        _FinalTranslationResponse.Status = ResponseType.S;
                    }
                }
                else
                {
                    _FinalTranslationResponse.Response = null;
                    _FinalTranslationResponse.Message = "Data Received Successfuly";
                    _FinalTranslationResponse.Status = ResponseType.S;
                }
            }
            catch (Exception Ex)
            {
                _FinalTranslationResponse.Response = null;
                _FinalTranslationResponse.Message = Ex.Message;
                _FinalTranslationResponse.Status = ResponseType.E;
                _FinalTranslationResponse.ErrorLevel = ErrorLevel.A;
            }
            return _FinalTranslationResponse;
        }
        #endregion
    }
}
