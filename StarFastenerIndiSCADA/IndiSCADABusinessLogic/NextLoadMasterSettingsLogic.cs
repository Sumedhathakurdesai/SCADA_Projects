using IndiSCADAEntity.Entity;
using IndiSCADAGlobalLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace IndiSCADABusinessLogic
{
    public class NextLoadMasterSettingsLogic
    {
        #region"Declaration"
        //Read only once Nextload Master setting records
        static ServiceResponse<IList> _RectifierStationName = IndiSCADATranslation.NextLoadMasterSettingTranslation.GetparameterNameFromDB();


        #endregion
        #region Public/private methods
        public static ServiceResponse<DataTable> NextLoadSettingLogic(NextLoadSettingsEntity _nextloaddata)
        {
            ServiceResponse<DataTable> result = new ServiceResponse<DataTable>();
            try
            {
                result = IndiSCADADataAccess.DataAccessSelect.SelectNextLoadMasterSettingData();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NextLoadMasterSettingsLogic NextLoadSettingLogic()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }

        public static ObservableCollection<NextLoadSettingsEntity> SelectNextLoadSettingData()
        {
            ObservableCollection<NextLoadSettingsEntity> _result= new ObservableCollection<NextLoadSettingsEntity>();
            try
            {
                ServiceResponse<IList> _TranslatoionResult = new ServiceResponse<IList>();
                _TranslatoionResult = IndiSCADATranslation.NextLoadMasterSettingTranslation.GetparameterNameFromDB();
                IList<NextLoadSettingsEntity> lstNextLoadData = (IList<NextLoadSettingsEntity>)(_TranslatoionResult.Response);
                if (_TranslatoionResult.Status == ResponseType.S)
                {
                    if (_TranslatoionResult.Response != null)
                    {
                        _result = new ObservableCollection<NextLoadSettingsEntity>(lstNextLoadData);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NextLoadMasterSettingsLogic SelectNextLoadSettingData()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }

        public static ServiceResponse<int> AddNewParameterButton(NextLoadSettingsEntity _nextLoadSettingsEntity)
        {
            ServiceResponse<int> _result = new ServiceResponse<int>();
            try
            {
                _result = IndiSCADADataAccess.DataAccessInsert.InsertNextLoadSettingData(_nextLoadSettingsEntity);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NextloadMasterSettingLogic AddNewParameterButton ()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;       

        }

        public static void UpdateParameterButton(NextLoadSettingsEntity _nextLoadSettingsEntity)
        {
            try
            {
                ServiceResponse<int> _result = new ServiceResponse<int>();
                try
                {
                    _result = IndiSCADADataAccess.DataAccessUpdate.UpdateNextLoadSettingData(_nextLoadSettingsEntity);
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError.ErrorLog("NextloadMasterSettingLogic UpdateNextLoadSettingData ()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NextloadMasterSettingLogic UpdateParameterButton()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }

        }

        public static void CreatePartMasterTable(ObservableCollection<NextLoadSettingsEntity> _nextLoadSettingsData)
        {
            try
            {
                ServiceResponse<int> _result = new ServiceResponse<int>();
                try
                {
                    IndiSCADADataAccess.DataAccessCreate.CreateTablePartMaster(_nextLoadSettingsData);
                    //DataTable DT = IndiSCADATranslation.NextLoadMasterSettingTranslation.getDataNextLoadMasterSettingsForNextLoad();
                    //_result = IndiSCADADataAccess.DataAccessCreate.CreateTablePartMaster();

                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError.ErrorLog("NextloadMasterSettingLogic CreateTablePartMaster ()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NextloadMasterSettingLogic CreatePartMasterTable()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }

        }

        public static void DeleteParameterButton(NextLoadSettingsEntity _nextLoadSettingsEntity)
        {
            try
            {
                ServiceResponse<int> _result = new ServiceResponse<int>();
                try
                {
                    _result = IndiSCADADataAccess.DataAccessDelete.DeleteNextLoadSettingData(_nextLoadSettingsEntity);
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError.ErrorLog("NextloadMasterSettingLogic DeleteNextLoadSettingData ()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NextloadMasterSettingLogic DeleteParameterButton()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }

        }

        public static ObservableCollection<string> GetAllTagListFromXML()
        {
            ObservableCollection<string> _result = new ObservableCollection<string>();
            try
            {
                DataSet dsTagAddresslist = new DataSet();
                dsTagAddresslist.ReadXml(@"EIP.xml");
                ErrorLogger.LogError.ErrorLog("ConfigurationViewModel GetAllTagListFromXML()", DateTime.Now.ToString(), dsTagAddresslist.Tables[0].Rows.Count.ToString(), "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                if (dsTagAddresslist != null)
                {
                    if (dsTagAddresslist.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsTagAddresslist.Tables[0].Rows)
                        {
                            _result.Add(dr["Name"].ToString());
                        }
                    }
                }                           
                
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NextloadMasterSettingLogic GetAllTagListFromXML()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }

        public static ObservableCollection<string> GetAllParameterList()
        {
            ObservableCollection<string> parameterlist = new ObservableCollection<string>();
            ServiceResponse<DataTable> result = new ServiceResponse<DataTable>();            
            try
            {
                result = IndiSCADADataAccess.DataAccessSelect.SelectNextLoadMasterSettingData();
                DataTable dt = result.Response;
                foreach (DataRow dr in dt.Rows)
                {
                    parameterlist.Add(dr["ParameterName"].ToString());
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NextloadMasterSettingLogic GetAllParameterList()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return parameterlist;
        }

        public static ServiceResponse<int> UpdateFormulaInNextloadSetting(string parametername, string formula)
        {
            ServiceResponse<int> result = new ServiceResponse<int>();
            try
            {
               
                bool test = TestFormula(formula);
                if (test == true)
                {
                     result = IndiSCADADataAccess.DataAccessUpdate.UpdateNextLoadSettingFormula(parametername,formula);                    
                }
                else
                {
                    result.Response = -10;
                    result.Message = "Formula string is not proper";
                    result.Status = ResponseType.E;
                }
               
            }
            catch(Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NextloadMasterSettingLogic UpdateFormulaInNextloadSetting()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }
        private static bool TestFormula(string expr)
        {
            try
            {

                string formula = expr;
                string[] elements = formula.Split(' ');
                string calc = "";
                foreach (string s in elements)
                {
                    if ((s.StartsWith("[")) && (s.EndsWith("]")))
                    {
                        string colname = s.Substring(1, s.Length - 2);
                        calc += 12;
                    }
                    else
                    {
                        calc += s;
                    }
                }
                DataTable calDT = new DataTable();
                Object value = calDT.Compute(calc, "");
                return true;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NextloadMasterSettingLogic TestFormula()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return false;
            }
        }
        #endregion

    }
}
