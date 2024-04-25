using IndiSCADAEntity.Entity;
using IndiSCADAGlobalLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADATranslation
{
   public static class NextLoadMasterSettingTranslation
    {
        #region Public Private  method
        #region chemical name editble
        public static ServiceResponse<IList> getChemicalNameMasterData()
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.SelectAllChemicalNameMasterData();
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
                                      select new ChemicalNameMasterEntity()
                                      {
                                          PumpNumber = (dr["PumpNo"].ToString()),
                                          PumpName = (dr["PumpName"].ToString()),
                                          ChemicalName = (dr["ChemicalName"].ToString()),
                                          ChemicalPercentage = (dr["ChemicalPercentage"].ToString()),
                                      });
                        //_FinalTranslationResponse.Response = _Query.ToList<NextLoadSettingsEntity>();
                        _FinalTranslationResponse.Response = _Query.ToList<ChemicalNameMasterEntity>(); ;
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

        public static ServiceResponse<IList> getShiftMasterData()
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.SelectAllShiftMasterData();
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
                                      select new ShiftMasterEntity()
                                      {
                                          ShiftNumber = (dr["ShiftNo"].ToString()),
                                          ShiftStartTime = (dr["ShiftStartTime"].ToString()),
                                          ShiftEndTime = (dr["ShiftEndTime"].ToString())
                                      });
                        //_FinalTranslationResponse.Response = _Query.ToList<NextLoadSettingsEntity>();
                        _FinalTranslationResponse.Response = _Query.ToList<ShiftMasterEntity>(); ;
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

        public static ServiceResponse<IList> GetparameterNameFromDB()
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.SelectNextLoadMasterSettingData();
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
                                      select new NextLoadSettingsEntity()
                                      {
                                          ParameterName = (dr["ParameterName"].ToString()),
                                          ColumnName = (dr["ColumnName"].ToString()),
                                          DataType = (dr["DataType"].ToString()),
                                          Unit = (dr["Unit"].ToString()),
                                          ScreenName = (dr["TableName"].ToString()),
                                          CalculationFormula = (dr["CalculationFormula"].ToString()),
                                          isPrimaryKey = Convert.ToBoolean((dr["isPrimaryKey"].ToString())),
                                          isInReport = Convert.ToBoolean((dr["IsInReport"].ToString())),
                                          isDownloadToPlc = Convert.ToBoolean((dr["isDownloadToPLC"].ToString())),
                                          isReadOnly = Convert.ToBoolean((dr["IsReadOnlyForNextLoad"].ToString())),
                                          isCalculationRequired = Convert.ToBoolean((dr["IsCalculationRequired"].ToString())),
                                          MinValue = (dr["MinValue"].ToString()),
                                          MaxValue = (dr["MaxValue"].ToString())

                                      });
                        _FinalTranslationResponse.Response = _Query.ToList<NextLoadSettingsEntity>();
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
        public static ServiceResponse<DataTable> getNextloatSettingData()
        {
            ServiceResponse<DataTable> _FinalTranslationResponse = new ServiceResponse<DataTable>();
            try
            {
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.SelectNextLoadMasterSettingData();
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
                                      select new NextLoadSettingsEntity()
                                      {
                                          ParameterName = (dr["ParameterName"].ToString()),
                                          DataType = (dr["DataType"].ToString()),
                                          Unit = (dr["Unit"].ToString()),
                                          ScreenName = (dr["TableName"].ToString()),
                                          isPrimaryKey = Convert.ToBoolean((dr["isPrimaryKey"].ToString())),
                                          isInReport = Convert.ToBoolean((dr["IsInReport"].ToString())),
                                          isDownloadToPlc = Convert.ToBoolean((dr["isDownloadToPLC"].ToString())),
                                          isReadOnly = Convert.ToBoolean((dr["IsReadOnlyForNextLoad"].ToString())),
                                          isCalculationRequired = Convert.ToBoolean((dr["IsCalculationRequired"].ToString())),
                                          MinValue = (dr["MinValue"].ToString()),
                                          MaxValue = (dr["MaxValue"].ToString())
                                      });
                        //_FinalTranslationResponse.Response = _Query.ToList<NextLoadSettingsEntity>();
                        _FinalTranslationResponse.Response = _LocalData.Response;
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
