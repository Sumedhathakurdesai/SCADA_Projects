using IndiSCADAEntity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADABusinessLogic
{
     public static class UserActivityLogic
    {
        #region Public/private methods
        public static void InsertUserActivity(UserActivityEntity _userActivity)
        {
            try
            {
                IndiSCADADataAccess.DataAccessInsert.InsertUserActivity(_userActivity);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("UserActivityLogic InsertUserActivity ", DateTime.Now.ToString(), ex.Message , null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        #endregion
    }
}
