using IndiSCADAEntity.Entity;
using IndiSCADAGlobalLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace IndiSCADADataAccess
{
    public static class DataAccessCreate
    {
        #region Public/Private Property
        #endregion
        #region Public Private  method

        public static ServiceResponse<int> CreateTablePartMaster(ObservableCollection<NextLoadSettingsEntity> NextloadSettingColl)
        {
            ServiceResponse<int> _FinalCreateResponse = new ServiceResponse<int>();
            //DTNextLoadSettings = DT;
           // DataTable NextloadSettingData =new DataTable(NextloadSettingColl.Where(a => a.ScreenName == "PartMaster").ToList());
            
            if (NextloadSettingColl != null)
            {
                // create partmaster table
                try
                {
                    string sqlquery = "Create table PartMaster (";
                    string columns = "";// "PartNumber Text, PartName Text, ClientName Text, Description Text,";
                    for (int indexRow = 0; indexRow <= NextloadSettingColl.Count - 2; indexRow++)
                    {
                        columns = columns + "[" + NextloadSettingColl[indexRow].ColumnName.ToString() + "] " + NextloadSettingColl[indexRow].DataType + ",";
                    }
                    columns = columns + "[" + NextloadSettingColl[NextloadSettingColl.Count - 1].ColumnName + "] " + NextloadSettingColl[NextloadSettingColl.Count - 1].DataType;

                    sqlquery = sqlquery + columns + ")";
                    try
                    {
                        string dropCommand = "DROP TABLE PartMaster;";
                        DataTable _DTRecords = new DataTable();
                        List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",dropCommand),
                        };
                        int res = SQLHelper.ExecuteNonQuery("sp_TableCreateUsingQuery", sqlParameters.ToArray());
                    }
                    catch { }

                    List<SqlParameter> sqlParameters1 = new List<SqlParameter>()
                    {
                        new SqlParameter("@sqlQuery",sqlquery),
                    };
                    int result = SQLHelper.ExecuteNonQuery("sp_TableCreateUsingQuery", sqlParameters1.ToArray());
                    _FinalCreateResponse.Response = result;
                    _FinalCreateResponse.Message = "";
                    _FinalCreateResponse.Status = ResponseType.S;                                        
                    
                }
                catch (Exception ex)
                {
                    //objLogging.LogError(DateTime.Now, "Create PartMaster Error :", ex.Message, 0);
                }
                // create LoadPartDetails table
                try
                {
                    string sqlquery = "Create table LoadPartDetails (";
                    string columns = "";// "PartNumber Text, PartName Text, ClientName Text, Description Text,";
                    for (int indexRow = 0; indexRow <= NextloadSettingColl.Count - 2; indexRow++)
                    {
                        //if (NextloadSettingColl[indexRow].isInReport == true)//commented by sbs
                        {
                            columns = columns + "[" + NextloadSettingColl[indexRow].ColumnName + "] " + NextloadSettingColl[indexRow].DataType + ",";
                        }
                    }
                    // columns = columns + "[" + DT.Rows[DT.Rows.Count - 1]["ColumnName"].ToString() + "] " + DT.Rows[DT.Rows.Count - 1]["DataType"].ToString();
                    columns = columns + "[LoadNumber] " + "Text," + "[DateTimeCol] " + "datetime";

                    sqlquery = sqlquery + columns + ")";

                    try
                    {
                        string dropCommand = "DROP TABLE LoadPartDetails;";                   
                        DataTable _DTRecords = new DataTable();
                        List<SqlParameter> sqlParameters = new List<SqlParameter>()
                        {
                            new SqlParameter("@sqlQuery",dropCommand),
                        };
                        int res = SQLHelper.ExecuteNonQuery("sp_TableCreateUsingQuery", sqlParameters.ToArray());
                    }
                    catch { }

                    List<SqlParameter> sqlParameters1 = new List<SqlParameter>()
                    {
                        new SqlParameter("@sqlQuery",sqlquery),
                    };
                    int result = SQLHelper.ExecuteNonQuery("sp_TableCreateUsingQuery", sqlParameters1.ToArray());
                    _FinalCreateResponse.Response = result;
                    _FinalCreateResponse.Message = "";
                    _FinalCreateResponse.Status = ResponseType.S;                   
                }
                catch (Exception ex)
                {
                   // objLogging.LogError(DateTime.Now, "Create PartMaster Error :", ex.Message, 0);
                }
            }
            return _FinalCreateResponse;

        }

        public static ServiceResponse<int> CreateNewDatabase(string ServerName, string DatabaseName, string CreateDatabaseQuery)
        {
            ServiceResponse<int> _FinalCreateResponse = new ServiceResponse<int>();
            try
            { 
                SqlConnection NewSqlConn = new SqlConnection("Server="+ServerName+ ";Integrated security=SSPI;database= master" );
                SqlCommand NewSqlCommand = new SqlCommand(CreateDatabaseQuery, NewSqlConn);
                NewSqlConn.Open();
                NewSqlCommand.ExecuteNonQuery();
                NewSqlConn.Close();
                _FinalCreateResponse.Response = 1;
                return _FinalCreateResponse;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationDataAccess CreateDatabase()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                _FinalCreateResponse.Response = 0;
                return _FinalCreateResponse;
            }
        }


        public static int CreateTables(string ServerName, string DatabaseName, string CreateTableQuery)
        { 
            try
            {
                int result = SQLHelper.ExecuteCreateProcedureTableQuery(ServerName, DatabaseName, CreateTableQuery);
                return result;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationDataAccess CreateTables()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return 0;
            } 
        }


        public static int CreateProcedures(string ServerName, string DatabaseName, string CreateProcedureQuery)
        { 
            try
            {
                int result = SQLHelper.ExecuteCreateProcedureTableQuery(ServerName, DatabaseName, CreateProcedureQuery);
                return result; 
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationDataAccess CreateProcedures()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return 0;
            } 
        }

        #endregion
    }
}



//    int resultdrop = objDBConnection.updateDynamicPartMaster(dropCommand);
//    int result = objDBConnection.updateDynamicPartMaster(sqlquery);

//    if (result == 1)
//    {
//        MessageBox.Show("PartMaster Table created successfully", "Saving", MessageBoxButtons.OK, MessageBoxIcon.Information);

//    }
//    else
//    {
//        MessageBox.Show("Try again later", "Saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
//    }
//}
//catch (Exception ex)
//{
//    objLogging.LogError(DateTime.Now, "Create PartMaster Error :", ex.Message, 0);
//}
//// create LoadPartDetails table
//try
//{
//    string sqlquery = "Create table LoadPartDetails (";
//    string columns = "";// "PartNumber Text, PartName Text, ClientName Text, Description Text,";
//    for (int indexRow = 0; indexRow <= DT.Rows.Count - 2; indexRow++)
//    {
//        if (DT.Rows[indexRow]["IsInReport"].ToString() == "True")
//        {
//            columns = columns + "[" + DT.Rows[indexRow]["ColumnName"].ToString() + "] " + DT.Rows[indexRow]["DataType"].ToString() + ",";
//        }
//    }
//    // columns = columns + "[" + DT.Rows[DT.Rows.Count - 1]["ColumnName"].ToString() + "] " + DT.Rows[DT.Rows.Count - 1]["DataType"].ToString();
//    columns = columns + "[LoadNumber] " + "Text";

//    sqlquery = sqlquery + columns + ")";
//    string dropCommand = "DROP TABLE LoadPartDetails;";
//    int resultdrop = objDBConnection.updateDynamicPartMaster(dropCommand);
//    int result = objDBConnection.updateDynamicPartMaster(sqlquery);

//    if (result == 1)
//    {
//        MessageBox.Show("LoadPartDetails Table created successfully", "Saving", MessageBoxButtons.OK, MessageBoxIcon.Information);

//    }
//    else
//    {
//        MessageBox.Show("Try again later", "Saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
//    }
//}
//catch (Exception ex)
//{
//    objLogging.LogError(DateTime.Now, "Create PartMaster Error :", ex.Message, 0);
//}

////alter LoadData table
//try
//{
//    string sqlquery = "alter table LoadData add ";
//    string columns = "";// "PartNumber Text, PartName Text, ClientName Text, Description Text,";
//    for (int indexRow = 0; indexRow <= DT.Rows.Count - 2; indexRow++)
//    {
//        bool ColumnFound = false;
//        if (DT.Rows[indexRow]["IsInLoadData"].ToString() == "True")
//        {
//            // search for existing column in LoadData
//            DataTable dtLoadData = objDBConnection.getLoadDataColumns();
//            for (int i = 0; i < dtLoadData.Columns.Count; i++)
//            {
//                string colname = DT.Rows[indexRow]["ColumnName"].ToString();
//                if (dtLoadData.Columns[i].ColumnName == colname)
//                {
//                    ColumnFound = true;
//                }
//            }
//            if (!ColumnFound)
//            {
//                columns = columns + "[" + DT.Rows[indexRow]["ColumnName"].ToString() + "] " + DT.Rows[indexRow]["DataType"].ToString() + ",";
//            }
//        }
//    }
//    //   columns = columns + "[" + DT.Rows[DT.Rows.Count - 1]["ColumnName"].ToString() + "] " + DT.Rows[DT.Rows.Count - 1]["DataType"].ToString();

//    sqlquery = sqlquery + columns.Substring(0, columns.Length - 1) + "";

//    int result = objDBConnection.updateDynamicPartMaster(sqlquery);

//    if (result == 1)
//    {
//        MessageBox.Show("LoadData Table updated successfully", "Saving", MessageBoxButtons.OK, MessageBoxIcon.Information);

//    }
//    else
//    {
//        MessageBox.Show("Try again later", "Saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
//    }
