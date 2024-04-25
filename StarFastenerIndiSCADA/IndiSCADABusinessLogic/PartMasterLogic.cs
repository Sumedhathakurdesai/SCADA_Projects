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
    public class PartMasterLogic
    {
        #region"Declaration"
        //Read only once Nextload Master setting records
        //static ServiceResponse<IList> _RectifierStationName = IndiSCADATranslation.NextLoadMasterSettingTranslation.GetparameterNameFromDB();        
        #endregion
        
        #region Public/private methods
        public static ServiceResponse<DataTable> getPartDetails(string partnumber,string selection)
        {
            ServiceResponse<DataTable> result = new ServiceResponse<DataTable>();
            try
            {
                result = IndiSCADADataAccess.DataAccessSelect.SelectPartMasterData(partnumber,selection, "");
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("PartMasterLogic getPartDetails()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }

        

        public static ServiceResponse<DataTable> getDataPartMaster(string colname, string colvalue)
        {
            
            ServiceResponse<DataTable> result = new ServiceResponse<DataTable>();
            try
            {
                result = IndiSCADADataAccess.DataAccessSelect.SelectPartMasterDataforColumn(colname,colvalue);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("PartMasterLogic getDataPartMaster()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                ErrorLogger.LogError.ErrorLog("PartMasterLogic getPartList()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                ErrorLogger.LogError.ErrorLog("PartMasterLogic getNextloadSettingDetails()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                ErrorLogger.LogError.ErrorLog("PartMasterLogic SaveNewPart()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                ErrorLogger.LogError.ErrorLog("PartMasterLogic UpdateSelectedPart()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }

        public static ServiceResponse<DataTable> getSelectedPartDetails(string query)
        {
            ServiceResponse<DataTable> result = new ServiceResponse<DataTable>();
            try
            {
                result = IndiSCADADataAccess.DataAccessSelect.SelectPartMasterWithQuery(query);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("PartMasterLogic getSelectedPartDetails()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
            //    ErrorLogger.LogError.ErrorLog("PartMasterLogic LoginUser()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
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
                ErrorLogger.LogError.ErrorLog("PartMasterLogic DeleteSelectedPart()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }

        public static ServiceResponse<int> DeleteSelectedHangerType(string htype)
        {
            ServiceResponse<int> result = new ServiceResponse<int>();
            try
            {
                result = IndiSCADADataAccess.DataAccessDelete.DeleteHangerMasterData(htype);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("PartMasterLogic DeleteSelectedHangerType()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }

        public static ServiceResponse<DataTable> getHangerDetails()
        {
            ServiceResponse<DataTable> result = new ServiceResponse<DataTable>();
            try
            {
                result = IndiSCADADataAccess.DataAccessSelect.SelectHagerTypeMasterData("Select * from HangerTypeMaster");
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("PartMasterLogic getHangerDetails()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }

        public static ServiceResponse<DataTable> isHangerTypeExists(string htype)
        {
            ServiceResponse<DataTable> result = new ServiceResponse<DataTable>();
            try
            {
                result = IndiSCADADataAccess.DataAccessSelect.SelectHagerTypeMasterData("Select * from HangerTypeMaster where HangerType='"+htype + "'");
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("PartMasterLogic isHangerTypeExists()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }

        public static ServiceResponse<int> UpdateHangerType(DataRow dr)
        {
            ServiceResponse<int> result = new ServiceResponse<int>();
            try
            {
                result = IndiSCADADataAccess.DataAccessUpdate.UpdateHangerTypeMasterData(dr);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("PartMasterLogic UpdateHangerType()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }

        public static ServiceResponse<int> InsertHangerType(DataRow dr)
        {
            ServiceResponse<int> result = new ServiceResponse<int>();
            try
            {
                string query = "Insert into HangerTypeMaster Values('" + dr["HangerType"].ToString() + "', " + Convert.ToInt32(dr["PartQuantity"].ToString()) + ")";
                result = IndiSCADADataAccess.DataAccessInsert.InsertPartMasterData(query);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("PartMasterLogic InsertHangerType()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }
        
            
        #endregion

    }
}
