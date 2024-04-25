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
    public static class IOStatusTranslation
    {
        #region Public Private  method
        public static ServiceResponse<IList> GetparameterNameFromDB(string ParameterType)
        {
            ServiceResponse<IList> _FinalTranslationResponse = new ServiceResponse<IList>();
            try
            {
                ServiceResponse<DataTable> _LocalData =IndiSCADADataAccess.DataAccessSelect.SelectIOStatusParameterDataDataTable(ParameterType);
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
                                      select new IOStatusEntity()
                                      {
                                          ParameterName =(dr["ParameterName"].ToString()),
                                      });
                        _FinalTranslationResponse.Response = _Query.ToList<IOStatusEntity>();
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
        public static DataTable getchemicalList()
        {

            DataTable result = new DataTable();
            try
            {
                result = IndiSCADADataAccess.DataAccessSelect.SelectChemicalconsumptionData();
            }
            catch (Exception ex)
            {
               // ErrorLogger.LogError.ErrorLog("PartMasterLogic getPartList()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }
        #endregion
    }
}
