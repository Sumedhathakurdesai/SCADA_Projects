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

namespace IndiSCADABusinessLogic
{
    public static class TankDetailsLogic
    {
        #region Public/private methods
        public static string ValueFromArray(string[] InputArray, int index)
        {
            try
            {
                return InputArray[index];
            }
            catch
            {
                return "0";
            }
        }


        public static ServiceResponse<DataTable> DisplayNextloadPartSelection()
        {
            ServiceResponse<DataTable> result = new ServiceResponse<DataTable>();
            try
            {
                result = IndiSCADADataAccess.DataAccessSelect.getDataPartMasterForDataLogging1();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NExtLoadEntryLogic() DisplayNextloadPartSelection", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            return result;
        }

        public static string displayNextLoadPart()
        {
            try
            {
                DataTable dtPartData = new DataTable(); string NextLoadSelectedPart = "";
                try
                {
                    ServiceResponse<DataTable> ResultPartMasterSetting = DisplayNextloadPartSelection();
                    dtPartData = ResultPartMasterSetting.Response;
                }
                catch (Exception ex) { ErrorLogger.LogError.ErrorLog("Error while fetching isSelectedforNextLoad=1 in NextloadView", DateTime.Now.ToString(), ex.Message, "No", true); }
                for (int i = 0; i < dtPartData.Rows.Count; i++)
                {
                    NextLoadSelectedPart = dtPartData.Rows[0]["PartName"].ToString();
                }
                if(NextLoadSelectedPart.ToString()=="" || NextLoadSelectedPart.ToString()==null)
                {
                    return "";
                }
                else
                {
                    return NextLoadSelectedPart;
                }
            }
            catch (Exception ex)
            { return ""; }
        }

        public static ObservableCollection<TankDetailsEntity> GetTanlDetails()
        {
            ObservableCollection<TankDetailsEntity> _TankDetails = new ObservableCollection<TankDetailsEntity>();
            try
            {
                ServiceResponse<DataTable> _StationList = IndiSCADADataAccess.DataAccessSelect.ReturnDataTable("StationMasterTankDetails","SP_StationMaster");
                //Actual Current
                string[] ActualCurrent = IndiSCADAGlobalLibrary.TagList.ActualCurrent;
                //Actual Voltage
                string[] ActualVoltage = IndiSCADAGlobalLibrary.TagList.ActualVoltage;
                //Actual Temperature
                string[] Temperature = IndiSCADAGlobalLibrary.TagList.TemperatureActual;
                //Actual pH
                string[] ActualpH = IndiSCADAGlobalLibrary.TagList.pHActual;
                // PartName
                string[] PartName = IndiSCADAGlobalLibrary.TagList.PartNameAtStation;

                //MECL number
               // string[] MECLNumber = IndiSCADAGlobalLibrary.TagList.MECLnumberAtStation;

                // Load No
                string[] LoadNoAtStation = IndiSCADAGlobalLibrary.TagList.LoadNoAtStation; //D2201
                // LoadType
                string[] LoadType = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("LoadTypeatStationArrayLoadType"); //D2101
                // Duration
                string[] Duration =   DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("DipTimeForDisplayDipTime"); //D2501
                string[] DurationHHMM=new string[] { };
                try
                {
                     DurationHHMM = getDurMinSec(Duration);
                }
                catch(Exception ex)
                {
                    ErrorLogger.LogError.ErrorLog("TankDetailsLogic DurationHHMM getDurMinSec()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }

                int index = 0;
                if (_StationList.Response != null)
                {
                    DataTable _DTStationList = _StationList.Response;
                    foreach (DataRow row in _DTStationList.Rows)
                    {
                        TankDetailsEntity _TankDetailsEntity = new TankDetailsEntity();


                        _TankDetailsEntity.StationID = index.ToString();
                        _TankDetailsEntity.StationNo = row["StationNumber"].ToString();//station name
                        _TankDetailsEntity.StationName = row["StationName"].ToString ();//station name
                        _TankDetailsEntity.LoadNumber = ValueFromArray(LoadNoAtStation,index);
                        _TankDetailsEntity.LoadType = ValueFromArray(LoadType, index);
                        _TankDetailsEntity.Duration = ValueFromArray(DurationHHMM, index);
                        _TankDetailsEntity.PartName = ValueFromArray(PartName, index);
                        //_TankDetailsEntity.MECLNumber = ValueFromArray(MECLNumber, index);

                        //_TankDetailsEntity.SetLoadTypeReadOnly = ValueFromArray(, index);


                        try
                        {
                            if (row["TemperatureValueIndex"].ToString().Length > 0)
                            {
                                try
                                {
                                    int Tempindex = Convert.ToInt16(row["TemperatureValueIndex"].ToString());
                                    _TankDetailsEntity.ActualTemperature = ValueFromArray(Temperature, Tempindex)  +"  °C";
                                }
                                catch { }
                            }
                            if (row["CurrentValueIndex"].ToString().Length > 0)
                            {
                                try
                                {
                                    int Tempindex = Convert.ToInt16(row["CurrentValueIndex"].ToString());
                                    _TankDetailsEntity.ActualCurrent = ValueFromArray(ActualCurrent, Tempindex) + "  A";
                                }
                                catch { }
                            }
                            if (row["VoltageValueIndex"].ToString().Length > 0)
                            {
                                try
                                {
                                    int Tempindex = Convert.ToInt16(row["VoltageValueIndex"].ToString());
                                    _TankDetailsEntity.ActualVoltage = ValueFromArray(ActualVoltage, Tempindex) + "  V";
                                }
                                catch { }
                            }
                            if (row["pHValueIndex"].ToString().Length > 0)
                            {
                                try
                                {
                                    int pHindex = Convert.ToInt16(row["pHValueIndex"].ToString());
                                    _TankDetailsEntity.pH = ValueFromArray(ActualpH, pHindex) + "  pH";
                                }
                                catch { }
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("TankDetailsLogic GetTanlDetails() Error in foreach loop ", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        }
                        _TankDetails.Add(_TankDetailsEntity);
                        index = index + 1;
                    }


                }
                return _TankDetails;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TankDetailsLogic GetTanlDetails()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return _TankDetails;
            }
        }
        public static string[] getDurMinSec(string [] duration)
        {
            try
            {
                string[] result = new string[duration.Length];
                int index = 0;
                foreach (string dur in duration)
                {
                    int totalSeconds = Convert.ToInt32(dur);
                    int seconds = totalSeconds % 60;
                    int minutes = (totalSeconds / 60) % 60;
                    int hour = totalSeconds / 3600;
                    result[index] = hour + ":" + minutes + ":" + seconds;
                    index++;
                }
                return result;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("TankDetailsLogic GetDuration()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                return null;
            }
        }
        #endregion
    }
}
