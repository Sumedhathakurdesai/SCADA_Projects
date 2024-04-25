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
    public class UserMasterLogic
    {
        public static ServiceResponse<int> InsertUserMasterData(UserMasterEntity _insertUserData)
        {
            ServiceResponse<int> _result = new ServiceResponse<int>();
            try
            {
                _result = IndiSCADADataAccess.DataAccessInsert.InsertNewUserData(_insertUserData);
            }
            catch(Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("UserMasterLogic InsertUserMasterData ()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }
        public static ServiceResponse<int> UpdateUserMasterData(UserMasterEntity _updateUserData)
        {
            ServiceResponse<int> _result = new ServiceResponse<int>();
            try
            {
                _result = IndiSCADADataAccess.DataAccessUpdate.UpdateNewUserData(_updateUserData);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("UserMasterLogic UpdateUserMasterData ()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }
        public static ServiceResponse<int> DeleteUserMasterData(UserMasterEntity _updateUserData)
        {
            ServiceResponse<int> _result = new ServiceResponse<int>();
            try
            {
                _result = IndiSCADADataAccess.DataAccessDelete.DeleteNewUserData(_updateUserData);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("UserMasterLogic DeleteUserMasterData()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }
        public static ObservableCollection<UserMasterEntity> SelectUserMasterData()
        {
            ObservableCollection<UserMasterEntity> _result = new ObservableCollection<UserMasterEntity>();
            try
            {
                ServiceResponse<IList> _TranslatoionResult = new ServiceResponse<IList>();
                _TranslatoionResult = IndiSCADATranslation.UserMasterTranslation.GetUserMasterDataTranslation();
                if (_TranslatoionResult.Status == ResponseType.S)
                {
                    if (_TranslatoionResult.Response != null)
                    {
                        IList<UserMasterEntity> _Ilistresult=(IList<UserMasterEntity>)(_TranslatoionResult.Response);
                        _result = new ObservableCollection<UserMasterEntity> (_Ilistresult); 
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("UserMasterLogic SelectUserMasterData()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return _result;
        }
    }
}
