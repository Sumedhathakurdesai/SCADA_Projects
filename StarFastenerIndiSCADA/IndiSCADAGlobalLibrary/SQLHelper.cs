using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MySql.Data.MySqlClient;


namespace IndiSCADAGlobalLibrary
{
    public static class SQLHelper
    {
        static SqlConnection conn = null;
        static MySqlConnection myConnection = new MySqlConnection();
        static bool connected = false;
        private static void GetConnection()
        {
            try
            {
                if (conn == null && !string.IsNullOrEmpty(IndiSCADAGlobalLibrary.AccessConfig.GetConnectionString))
                {
                    conn = new SqlConnection(IndiSCADAGlobalLibrary.AccessConfig.GetConnectionString);
                }

                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("GetConnection() Error:-" + ex.Message);
                //objLogging.LogError(DateTime.Now, "DBConnection_getConnection()", ex.Message, 0); 
            }
        }
        public static DataTable serverExcecuteSelectQuery(string Query)
        {
            try
            {
                if (myConnection.State == ConnectionState.Closed)
                {
                    myConnection.Open();
                }
                //objLogging.LogError(DateTime.Now, "serverExcecuteSelectQuery()", Query, 0);
                ErrorLogger.LogError.ErrorLog("serverExcecuteSelectQuery() ", DateTime.Now.ToString(), Query, "", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                DataSet DS = new DataSet();
                DataTable DT = new DataTable();
                string StrCommand = Query;
                MySqlCommand olecmdserverExcecuteNonQuery = new MySqlCommand(Query, myConnection);
                MySqlDataAdapter DA = new MySqlDataAdapter(olecmdserverExcecuteNonQuery);
                DA.Fill(DS);
                DT = DS.Tables[0];
                return DT;
            }
            catch (Exception ex)
            {
                myConnection.Close();
                //objLogging.LogError(DateTime.Now, "serverExcecuteSelectQuery()_Exception", ex.Message, 0);
                return null;
            }
        }
        public static string serverExcecuteNonQuery(string Query)
        {
            try
            {
                myConnection.Close();
                CreateConnection();
              //  ErrorLogger.LogError.ErrorLog("serverExcecuteNonQuery() before", DateTime.Now.ToString(), Query, "", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                //try
                //{
                //    if (myConnection.State != ConnectionState.Open)
                //    {
                //        myConnection.Open();
                //    }
                //    //  rror(DateTime.Now, "serverExcecuteNonQuery()", Query, 0);
                //}
                //catch { }

                MySqlCommand olecmdserverExcecuteNonQuery = new MySqlCommand(Query, myConnection);
                olecmdserverExcecuteNonQuery.ExecuteNonQuery();

                ErrorLogger.LogError.ErrorLog("serverExcecuteNonQuery() after", DateTime.Now.ToString(), Query, "", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return "true";
            }
            catch (Exception ex)
            {
                myConnection.Close();
                ErrorLogger.LogError.ErrorLog("serverExcecuteNonQuery()", DateTime.Now.ToString(), ex.Message, "", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                //objLogging.LogError(DateTime.Now, "serverExcecuteNonQuery()Exception", ex.Message, 0);
                return ex.Message;
            }
        }
        private static bool OpenConnection()
        {
            try
            {
                if (connected == true)
                {
                    myConnection.Close();
                    myConnection.Open();
                }
                else
                {
                    myConnection.Open();
                }
                //objLogging.LogError(DateTime.Now, "SeverDatabase connection Successfull()", "", 0);
                ErrorLogger.LogError.ErrorLog("OpenConnection() connection state ", DateTime.Now.ToString(), myConnection.State.ToString(), "", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                return true;

            }
            catch (MySqlException ex)
            {
                //switch (ex.Number)
                //{
                //    case 0:
                //        objLogging.LogError(DateTime.Now, "Cannot connect to server. Please Contact administrator", "", 0);
                //        break;

                //    case 1045:
                //        objLogging.LogError(DateTime.Now, "Invalid username/password, please try again", "", 0);
                //        break;

                //    case 1042:
                //        objLogging.LogError(DateTime.Now, "Please Check the Internet Connection and try again. .", "", 0);
                //        break;

                //    default:
                //        objLogging.LogError(DateTime.Now, "Cannot connect to server. Please Contact administrator.", "", 0);
                //        break;
                //}
                return false;
            }
        }

        public static void CreateConnection()
        {
            try
            {
                //sas
                //myConnection.ConnectionString = "Data Source=103.21.58.6;Initial Catalog=StarDB;Integrated Security=False;User ID=StarDB;Password=StarDB@123;";
                //myConnection.ConnectionString = "Data Source=103.21.58.6;Initial Catalog=StarDB;Integrated Security=False;User ID=StarDB;Password=StarDB@123;";

                // myConnection.ConnectionString = "Data Source=103.21.58.6;Initial Catalog=nirankariDB;Integrated Security=False;User ID=nirankariDB;Password=nirankariDB@123;";

                //myConnection.ConnectionString = IndiSCADAGlobalLibrary.UserLoginDetails.ConnectionString;
              //  myConnection = new MySqlConnection();
                myConnection.ConnectionString = "Data Source=103.195.185.168;Initial Catalog=indiscpx_starDB;User ID=indiscpx_starDB; Password=indiscpx_starDB@123;";

              //  myConnection.ConnectionString = IndiSCADAGlobalLibrary.AccessConfig.GetMySQLConnectionString;

                connected = OpenConnection();

            }
            catch (Exception ex)
            {
                //objLogging.LogError(DateTime.Now, "CreateConnection()_Exception", ex.Message, 0);
                //MessageBox.Show("Something went Wrong");
                ErrorLogger.LogError.ErrorLog("OpenConnection()  ", DateTime.Now.ToString(), ex.Message, ex.Message, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }

        }


        #region"Linqinsert pattern"
        public static int InsertDataSql(String Query)
        {
            try
            {
                SqlConnection oConnection = new SqlConnection(IndiSCADAGlobalLibrary.AccessConfig.GetConnectionString);
                SqlCommand oCommand = new SqlCommand(Query, oConnection);
                oConnection.Open();
                int iReturnValue;

                using (SqlTransaction oTransaction = oConnection.BeginTransaction())
                {
                    try
                    {
                        oCommand.Transaction = oTransaction;
                        iReturnValue = oCommand.ExecuteNonQuery();
                        oTransaction.Commit();
                    }
                    catch
                    {
                        oTransaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        if (oConnection.State == ConnectionState.Open)
                            oConnection.Close();
                        oConnection.Dispose();
                        oCommand.Dispose();
                    }
                }
                return iReturnValue;
            }
            catch
            {
                return 0;
            }
        }
        public static int DeleteDataSql(String Query)
        {
            try
            {
                SqlConnection oConnection = new SqlConnection(IndiSCADAGlobalLibrary.AccessConfig.GetConnectionString);
                SqlCommand oCommand = new SqlCommand(Query, oConnection);
                oConnection.Open();
                int iReturnValue;

                using (SqlTransaction oTransaction = oConnection.BeginTransaction())
                {
                    try
                    {
                        oCommand.Transaction = oTransaction;
                        iReturnValue = oCommand.ExecuteNonQuery();
                        oTransaction.Commit();
                    }
                    catch
                    {
                        oTransaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        if (oConnection.State == ConnectionState.Open)
                            oConnection.Close();
                        oConnection.Dispose();
                        oCommand.Dispose();
                    }
                }
                return iReturnValue;
            }
            catch
            {
                return 0;
            }
        }
        #endregion
        #region "FILL DATA TABLE"

        public static void Fill(DataTable dataTable, String procedureName)
        {
            SqlConnection oConnection = new SqlConnection(IndiSCADAGlobalLibrary.AccessConfig.GetConnectionString);
            SqlCommand oCommand = new SqlCommand(procedureName, oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter oAdapter = new SqlDataAdapter();

            oAdapter.SelectCommand = oCommand;
            oConnection.Open();
            using (SqlTransaction oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    oAdapter.SelectCommand.Transaction = oTransaction;
                    oAdapter.Fill(dataTable);
                    oTransaction.Commit();
                }
                catch
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oAdapter.Dispose();
                }
            }
        }

        public static void FillDT(DataTable dataTable, string sqlQuery)
        {
            SqlConnection oConnection = new SqlConnection(IndiSCADAGlobalLibrary.AccessConfig.GetConnectionString);
            SqlCommand oCommand = new SqlCommand(sqlQuery, oConnection);
            //oCommand.CommandType = CommandType.Text;

            SqlDataAdapter oAdapter = new SqlDataAdapter();

            oAdapter.SelectCommand = oCommand;
            oConnection.Open();
            using (SqlTransaction oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    oAdapter.SelectCommand.Transaction = oTransaction;
                    oAdapter.Fill(dataTable);
                    oTransaction.Commit();
                }
                catch
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oAdapter.Dispose();
                }
            }
        }
        public static ServiceResponse<string> ReturnValueSP(String procedureName, SqlParameter[] parameters)
        {
            ServiceResponse<string> _FinalSqlHelperResponse = new ServiceResponse<string>();
            SqlConnection oConnection = new SqlConnection(IndiSCADAGlobalLibrary.AccessConfig.GetConnectionString);
            SqlCommand oCommand = new SqlCommand(procedureName, oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
                oCommand.Parameters.AddRange(parameters);

            SqlDataAdapter oAdapter = new SqlDataAdapter();
            oCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;

            oAdapter.SelectCommand = oCommand;
            oConnection.Open();
            using (SqlTransaction oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    oAdapter.SelectCommand.Transaction = oTransaction;
                    int result = oCommand.ExecuteNonQuery();
                    _FinalSqlHelperResponse.Response = ((int)oCommand.Parameters["@ReturnValue"].Value).ToString ();
                    _FinalSqlHelperResponse.Message = "Data recived Successfuly";
                    _FinalSqlHelperResponse.Status = ResponseType.S;
                    oTransaction.Commit();
                    return _FinalSqlHelperResponse;
                }
                catch (Exception ex)
                {
                    oTransaction.Rollback();
                    _FinalSqlHelperResponse.Response = null;
                    _FinalSqlHelperResponse.Message = ex.Message;
                    _FinalSqlHelperResponse.Status = ResponseType.E;
                    _FinalSqlHelperResponse.ErrorLevel = ErrorLevel.A;
                    return _FinalSqlHelperResponse;
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oAdapter.Dispose();
                }
            }
        }
        public static ServiceResponse<string> ReturnValueSPString(String procedureName, SqlParameter[] parameters)
        {
            ServiceResponse<string> _FinalSqlHelperResponse = new ServiceResponse<string>();
            SqlConnection oConnection = new SqlConnection(IndiSCADAGlobalLibrary.AccessConfig.GetConnectionString);
            SqlCommand oCommand = new SqlCommand(procedureName, oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
                oCommand.Parameters.AddRange(parameters);

            SqlDataAdapter oAdapter = new SqlDataAdapter();
            oCommand.Parameters["@ReturnValuestring"].Direction = ParameterDirection.Output;

            oAdapter.SelectCommand = oCommand;
            oConnection.Open();
            using (SqlTransaction oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    oAdapter.SelectCommand.Transaction = oTransaction;
                    int result = oCommand.ExecuteNonQuery();
                    _FinalSqlHelperResponse.Response = ((string)oCommand.Parameters["@ReturnValuestring"].Value).ToString();
                    _FinalSqlHelperResponse.Message = "Data recived Successfuly";
                    _FinalSqlHelperResponse.Status = ResponseType.S;
                    oTransaction.Commit();
                    return _FinalSqlHelperResponse;
                }
                catch (Exception ex)
                {
                    oTransaction.Rollback();
                    _FinalSqlHelperResponse.Response = null;
                    _FinalSqlHelperResponse.Message = ex.Message;
                    _FinalSqlHelperResponse.Status = ResponseType.E;
                    _FinalSqlHelperResponse.ErrorLevel = ErrorLevel.A;
                    return _FinalSqlHelperResponse;
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oAdapter.Dispose();
                }
            }
        }
        public static void Fill(DataTable dataTable, String procedureName, SqlParameter[] parameters)
        {
            SqlConnection oConnection = new SqlConnection(IndiSCADAGlobalLibrary.AccessConfig.GetConnectionString);
            SqlCommand oCommand = new SqlCommand(procedureName, oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
                oCommand.Parameters.AddRange(parameters);

            SqlDataAdapter oAdapter = new SqlDataAdapter();

            oAdapter.SelectCommand = oCommand;
            oConnection.Open();
            using (SqlTransaction oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    oAdapter.SelectCommand.Transaction = oTransaction;
                    oAdapter.Fill(dataTable);
                    oTransaction.Commit();
                }
                catch (Exception ex)
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oAdapter.Dispose();
                }
            }
        }

        #endregion

        #region "FILL DATASET"

        public static void Fill(DataSet dataSet, String procedureName)
        {
            SqlConnection oConnection = new SqlConnection(IndiSCADAGlobalLibrary.AccessConfig.GetConnectionString);
            SqlCommand oCommand = new SqlCommand(procedureName, oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter oAdapter = new SqlDataAdapter();

            oAdapter.SelectCommand = oCommand;
            oConnection.Open();
            using (SqlTransaction oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    oAdapter.SelectCommand.Transaction = oTransaction;
                    oAdapter.Fill(dataSet);
                    oTransaction.Commit();
                }
                catch
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oAdapter.Dispose();
                }
            }
        }

        public static void Fill(DataSet dataSet, String procedureName, SqlParameter[] parameters)
        {
            SqlConnection oConnection = new SqlConnection(IndiSCADAGlobalLibrary.AccessConfig.GetConnectionString);
            SqlCommand oCommand = new SqlCommand(procedureName, oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
                oCommand.Parameters.AddRange(parameters);

            SqlDataAdapter oAdapter = new SqlDataAdapter();

            oAdapter.SelectCommand = oCommand;
            oConnection.Open();
            using (SqlTransaction oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    oAdapter.SelectCommand.Transaction = oTransaction;
                    oAdapter.Fill(dataSet);
                    oTransaction.Commit();
                }
                catch
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oAdapter.Dispose();
                }
            }
        }

        #endregion

        #region "EXECUTE SCALAR"

        public static object ExecuteScalar(String procedureName)
        {
            SqlConnection oConnection = new SqlConnection(IndiSCADAGlobalLibrary.AccessConfig.GetConnectionString);
            SqlCommand oCommand = new SqlCommand(procedureName, oConnection);

            oCommand.CommandType = CommandType.StoredProcedure;
            object oReturnValue;
            oConnection.Open();
            using (SqlTransaction oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    oCommand.Transaction = oTransaction;
                    oReturnValue = oCommand.ExecuteScalar();
                    oTransaction.Commit();
                }
                catch
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oCommand.Dispose();
                }
            }
            return oReturnValue;
        }

        public static object ExecuteScalar(String procedureName, SqlParameter[] parameters)
        {
            SqlConnection oConnection = new SqlConnection(IndiSCADAGlobalLibrary.AccessConfig.GetConnectionString);
            SqlCommand oCommand = new SqlCommand(procedureName, oConnection);

            oCommand.CommandType = CommandType.StoredProcedure;
            object oReturnValue;
            oConnection.Open();
            using (SqlTransaction oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    if (parameters != null)
                        oCommand.Parameters.AddRange(parameters);

                    oCommand.Transaction = oTransaction;
                    oReturnValue = oCommand.ExecuteScalar();
                    oTransaction.Commit();
                }
                catch
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oCommand.Dispose();
                }
            }
            return oReturnValue;
        }

        #endregion

        #region "EXECUTE NON QUERY"

        public static int ExecuteNonQuery(string procedureName)
        {
            SqlConnection oConnection = new SqlConnection(IndiSCADAGlobalLibrary.AccessConfig.GetConnectionString);
            SqlCommand oCommand = new SqlCommand(procedureName, oConnection);

            oCommand.CommandType = CommandType.StoredProcedure;
            int iReturnValue;
            oConnection.Open();
            using (SqlTransaction oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    oCommand.Transaction = oTransaction;
                    iReturnValue = oCommand.ExecuteNonQuery();
                    oTransaction.Commit();
                }
                catch
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oCommand.Dispose();
                }
            }
            return iReturnValue;
        }

        public static int ExecuteCreateProcedureTableQuery(string ServerName, string DatabaseName, string CreateTableQuery)
        {
            try
            {
                SqlConnection oConnection = new SqlConnection("Data Source=" + ServerName + ";Initial Catalog=" + DatabaseName + ";Integrated Security=True");

                oConnection.Open();
                SqlCommand oCommand = new SqlCommand(CreateTableQuery, oConnection);

                oCommand.ExecuteNonQuery();
                oConnection.Close();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

         

        public static int ExecuteNonQuery(string procedureName, SqlParameter[] parameters)
        {
            SqlConnection oConnection = new SqlConnection(IndiSCADAGlobalLibrary.AccessConfig.GetConnectionString);
            SqlCommand oCommand = new SqlCommand(procedureName, oConnection);

            oCommand.CommandType = CommandType.StoredProcedure;
            int iReturnValue;
            oConnection.Open();
            using (SqlTransaction oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    if (parameters != null)
                        oCommand.Parameters.AddRange(parameters);

                    oCommand.Transaction = oTransaction;
                    iReturnValue = oCommand.ExecuteNonQuery();
                    oTransaction.Commit();
                }
                catch (Exception Ex)
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oCommand.Dispose();
                }
            }
            return iReturnValue;
        }

        #endregion

        #region ObservableCollection Conversion
        public static ObservableCollection<LineMasterEntity> ToObservableCollection<LineMasterEntity>(this IList enumerable)
        {
            var col = new ObservableCollection<LineMasterEntity>();
            foreach (var cur in enumerable)
            {
                col.Add((LineMasterEntity)cur);
            }
            return col;
        }
        #endregion
    }

    #region ServiceResponse
    public enum ResponseType
    {
        E,//Error
        S,//Success
        W,//Warning
    }

    public enum ErrorLevel
    {
        A,//Access
        B,//BusinessLogic
        T,//Trnslation
        DC,//Device Comminication
    }

    public class ServiceResponse<T>
    {
        private string _Message;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        private T _Response;
        public T Response
        {
            get { return _Response; }
            set { _Response = value; }
        }

        private ResponseType _Status;
        public ResponseType Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private ErrorLevel _ErrorLevel;
        public ErrorLevel ErrorLevel
        {
            get { return _ErrorLevel; }
            set { _ErrorLevel = value; }
        }
    }

    public static class ExtensionMethod
    {
        public static bool HasError<T>(this ServiceResponse<T> Object)
            where T : class
        {
            if (Object.Response == null && Object.Status == ResponseType.E)
                return true;
            else
                return false;
        }
    }
    #endregion
  
    public static class AccessConfig
    {
        private static string _GetConnectionString;
        public static string GetConnectionString
        {
            get { return _GetConnectionString; }
            set { _GetConnectionString = value; }
        }
        private static int _Shift;
        public static int Shift
        {
            get { return _Shift; }
            set { _Shift = value; }
        }
        private static string _GetMySQLConnectionString;
        public static string GetMySQLConnectionString
        {
            get { return _GetMySQLConnectionString; }
            set { _GetMySQLConnectionString = value; }
        }
    }
    public static class ErrorLogEnable
    {
        private static bool _IsErrorLogEnable;
        public static bool IsErrorLogEnable
        {
            get { return _IsErrorLogEnable; }
            set { _IsErrorLogEnable = value; }
        }

    }
   
    public static class PLCCommunication
    {
        private static bool _IsPLCConnected;
        public static bool IsPLCConnected
        {
            get { return _IsPLCConnected; }
            set { _IsPLCConnected = value; }
        }

    }
    public static class UserLoginDetails
    {
        private static string _UserName;
        public static string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private static string _AccessLevel;
        public static string AccessLevel
        {
            get { return _AccessLevel; }
            set { _AccessLevel = value; }
        }
        private static int _AccessCode;
        public static int AccessCode
        {
            get { return _AccessCode; }
            set { _AccessCode = value; }
        }

    }
}
