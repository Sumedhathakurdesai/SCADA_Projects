using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using IndiSCADAEntity.Entity;
using IndiSCADAGlobalLibrary;
using System;
using System.Collections;

namespace IndiSCADATranslation
{
    public class SystemStatusTranslation
    {
        #region Public Private  method
        public static ServiceResponse<IList> GetparameterNameFromDB(string ParameterType)
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.SelectIOStatusParameterDataDataTable(ParameterType);
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
                                      select new SystemStatusEntity()
                                      {
                                          ParameterName = (dr["ParameterName"].ToString()),
                                      });
                        _FinalTranslationResponse.Response = _Query.ToList<SystemStatusEntity>();
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


        public static ServiceResponse<IList> GetTempAddressFromDB(string ParameterType)
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.SelectAddressFromAlarmMasterUsingGroupName(ParameterType);
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
                                      select new SystemStatusEntity()
                                      {
                                          ParameterName = (dr["AlarmAddress"].ToString()),
                                      });
                        _FinalTranslationResponse.Response = _Query.ToList<SystemStatusEntity>();
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

        public static ServiceResponse<IList> GetPHAddressFromDB(string ParameterType)
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.SelectAddressFromAlarmMasterUsingGroupName(ParameterType);
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
                                      select new SystemStatusEntity()
                                      {
                                          ParameterName = (dr["AlarmAddress"].ToString()),
                                      });
                        _FinalTranslationResponse.Response = _Query.ToList<SystemStatusEntity>();
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
