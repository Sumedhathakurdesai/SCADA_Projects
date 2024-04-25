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
    public static class ORDipTimeTranslation
    {
        #region Public Private  method
        public static ServiceResponse<IList> GetProgramName()
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.SelectDistinctprogramName();
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
                                      select new OutOfRangeSettingsDipTime()
                                      {
                                          ProgramNo = (dr["ProgramNo"].ToString()),
                                      });
                        _FinalTranslationResponse.Response = _Query.ToList<OutOfRangeSettingsDipTime>();
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
        public static ServiceResponse<IList> GetDipTimeFromProgramName(string ProgramNo)
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                ServiceResponse<DataTable> _LocalData = IndiSCADADataAccess.DataAccessSelect.SelectORDipTimeDataFromprogramName(ProgramNo);
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
                                      select new OutOfRangeSettingsDipTime()
                                      {
                                          ParameterName = (dr["StationName"].ToString()),
                                          ProgramNo = (dr["ProgramNo"].ToString()),
                                          HighBypass = (dr["DipHighBypass"].ToString()),
                                          LowBypass = (dr["DipLowBypass"].ToString()),
                                          HighSPDipTime = (dr["DipTimeToleranceHigh"].ToString()),
                                          LowSPDipTime = (dr["DipTimeToleranceLow"].ToString()),
                                      });
                        _FinalTranslationResponse.Response = _Query.ToList<OutOfRangeSettingsDipTime>();
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
