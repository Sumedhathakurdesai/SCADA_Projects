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
   public class NextloadEntryLogic
    {
        #region"Declaration"
        //Read only once Nextload Master setting records
        //static ServiceResponse<IList> _RectifierStationName = IndiSCADATranslation.NextLoadMasterSettingTranslation.GetparameterNameFromDB();        
        #endregion

        #region Public/private methods



        public static ServiceResponse<int> insertLNInToLoadPartData(string lno, DataTable dtPartColumns, DataTable dtPartData)
        {
            ServiceResponse<int> result = new ServiceResponse<int>();
            try
            {
                result = IndiSCADADataAccess.DataAccessInsert.insertLNInToLoadPartData(lno, dtPartColumns, dtPartData);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NExtLoadEntryLogic() insertLNInToLoadPartData", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }

        public static ServiceResponse<DataTable> getDataPartMasterForDataLogging1()
        {

            ServiceResponse<DataTable> result = new ServiceResponse<DataTable>();
            try
            {
                result = IndiSCADADataAccess.DataAccessSelect.getDataPartMasterForDataLogging1();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NExtLoadEntryLogic getDataPartMasterForDataLogging1()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }

        public static ServiceResponse<DataTable> getDataNextLoadMasterSettingsForDataLogging()
        {

            ServiceResponse<DataTable> result = new ServiceResponse<DataTable>();
            try
            {
                result = IndiSCADADataAccess.DataAccessSelect.getDataNextLoadMasterSettings();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NExtLoadEntryLogic getDataNextLoadMasterSettingsForDataLogging()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }

        public static ServiceResponse<int> DeleteFromLoadPartDetails(string query)
        {
            ServiceResponse<int> result = new ServiceResponse<int>();
            try
            {
                result = IndiSCADADataAccess.DataAccessInsert.DeleteFromLoadPartDetails(query);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NExtLoadEntryLogic() DeleteFromLoadPartDetails", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }

        public static ServiceResponse<DataTable> getPartDetails(string partnumber, string selection,string Description)
        {
            ServiceResponse<DataTable> result = new ServiceResponse<DataTable>();
            try
            {
                result = IndiSCADADataAccess.DataAccessSelect.SelectPartMasterData(partnumber, selection, Description);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NExtLoadEntryLogic getPartDetails()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }
        public static ServiceResponse<DataTable> DTCheckLoadisPResent(string lno)
        {

            ServiceResponse<DataTable> result = new ServiceResponse<DataTable>();
            try
            {
                result = IndiSCADADataAccess.DataAccessSelect.DTCheckLoadisPResent(lno);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NExtLoadEntryLogic DTCheckLoadisPResent()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }
        public static ServiceResponse<DataTable> GetLoadNumber(int StationNo)
        {

            ServiceResponse<DataTable> result = new ServiceResponse<DataTable>();
            try
            {
                result = IndiSCADADataAccess.DataAccessSelect.getLoadNumber(StationNo);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NExtLoadEntryLogic GetLoadNumber()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }

        public static ServiceResponse<DataTable> getDataPartMaster(string colname, string colvalue)
        {

            ServiceResponse<DataTable> result = new ServiceResponse<DataTable>();
            try
            {
                result = IndiSCADADataAccess.DataAccessSelect.SelectPartMasterDataforColumn(colname, colvalue);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NExtLoadEntryLogic getDataPartMaster()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }

        public static ServiceResponse<DataTable> getPartList()
        {

            ServiceResponse<DataTable> result = new ServiceResponse<DataTable>();
            try
            {
                result = IndiSCADADataAccess.DataAccessSelect.SelectPartNumberListData();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NExtLoadEntryLogic getPartList()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }

        public static ServiceResponse<DataTable> getPartDescriptionList()
        {

            ServiceResponse<DataTable> result = new ServiceResponse<DataTable>();
            try
            {
                result = IndiSCADADataAccess.DataAccessSelect.SelectPartDesciptionListData();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NExtLoadEntryLogic getPartList()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }


        public static ServiceResponse<DataTable> getNextloadSettingDetails(string tablename)
        {
            ServiceResponse<DataTable> result = new ServiceResponse<DataTable>();
            try
            {
                result = IndiSCADADataAccess.DataAccessSelect.SelectScreenwiseNextLoadMasterSettingData(tablename);

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NExtLoadEntryLogic getNextloadSettingDetails()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }


        public static ServiceResponse<DataTable> getORDipTimeDetails(string ProgramNO)
        {
            ServiceResponse<DataTable> result = new ServiceResponse<DataTable>();
            try
            {
                result = IndiSCADADataAccess.DataAccessSelect.SelectORDipTimeDataFromprogramName(ProgramNO);

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NExtLoadEntryLogic getORDipTimeDetails()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }

        public static ServiceResponse<int> SaveNewPart(string query)
        {
            ServiceResponse<int> result = new ServiceResponse<int>();
            try
            {
                result = IndiSCADADataAccess.DataAccessInsert.InsertPartMasterData(query);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NExtLoadEntryLogic SaveNewPart()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }

        public static ServiceResponse<int> UpdateSelectedPart(string query)
        {
            ServiceResponse<int> result = new ServiceResponse<int>();
            try
            {
                result = IndiSCADADataAccess.DataAccessUpdate.UpdatePartMasterData(query);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NExtLoadEntryLogic UpdateSelectedPart()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }

        public static ServiceResponse<int> ResetPartforNextLoad(string Query)
        {
            ServiceResponse<int> result = new ServiceResponse<int>();
            try
            {
                result = (IndiSCADADataAccess.DataAccessUpdate.UpdatePartMasterData(Query));
                
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NExtLoadEntryLogic ResetPartforNextLoad()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

            }
            return result;
        }


        public static ServiceResponse<int> UpdateSelectedPartforNextLoad(DataRow dr)
        {
            ServiceResponse<int> result = new ServiceResponse<int>();
            try
            {
                string partnumber = dr["Part Number"].ToString();
                ServiceResponse<DataTable> response = IndiSCADADataAccess.DataAccessSelect.SelectPartMasterData("", "SelectAll","");
                DataTable dt = response.Response;

                //string partName = "";
                //try
                //{
                //    DataRow[] results = dt.Select(" ParameterName='" + colname.ToString() + "'");
                //    partName = results[0]["ColumnName"].ToString();
                //}
                //catch (Exception ex)
                //{

                //}

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string colname = dt.Columns[i].ToString();                   
                    string query = "Update PartMaster set isSelectedforNextLoad=1 ,[" + colname + "]='" + dr[i].ToString() + "' where PartNumber='" + partnumber + "'";
                    result = IndiSCADADataAccess.DataAccessUpdate.UpdatePartMasterData(query);
                }                
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NExtLoadEntryLogic UpdateSelectedPartforNextLoad()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

            }
            return result;
        }


        public static ServiceResponse<DataTable> EditPartDetails(string partnumber)
        {
            ServiceResponse<DataTable> result = new ServiceResponse<DataTable>();
            //try
            //{
            //    result = IndiSCADADataAccess.DataAccessSelect.SelectPartMasterData(partnumber);
            //}
            //catch (Exception ex)
            //{
            //    ErrorLogger.LogError.ErrorLog("LoginLogic LoginUser()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            //}
            return result;
        }

        public static ServiceResponse<int> DeleteSelectedPart(string partnumber)
        {
            ServiceResponse<int> result = new ServiceResponse<int>();
            try
            {
                result = IndiSCADADataAccess.DataAccessDelete.DeletePartMasterData(partnumber);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NExtLoadEntryLogic DeleteSelectedPart()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }
        #endregion
    }
}
