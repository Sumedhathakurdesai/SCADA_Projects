using IndiSCADAEntity.Entity;
using IndiSCADAGlobalLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADABusinessLogic
{
     public static class LoginLogic
    {
        #region Public/private methods
        public static ServiceResponse<string> LoginUser(UserMasterEntity _userdata)
        {
            ServiceResponse<string> result = new ServiceResponse<string>();
            try
            {
                result= IndiSCADADataAccess.DataAccessSelect.LoginUserAccess(_userdata);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("LoginLogic LoginUser()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }
        #endregion
    }
}
