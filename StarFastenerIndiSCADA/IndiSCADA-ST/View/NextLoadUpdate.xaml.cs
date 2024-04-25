using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using IndiSCADABusinessLogic;
using IndiSCADAGlobalLibrary;
using IndiSCADA_ST.View;
using System.Configuration;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace IndiSCADA_ST.View
{
    /// <summary>
    /// Interaction logic for NextLoadUpdate.xaml
    /// </summary>
    public partial class NextLoadUpdate : ChromelessWindow, INotifyPropertyChanged
    {
        #region General Declarations
        DataTable DTPartMaster = null;
        string[] addrList = null;
        string[] newaddrList = null;
        string[] ColumnNames = null;
        string Quantity = "0", WeightPerPart = "0.0", TotalWeight = "0"; int programNo = 0;

        #endregion

        #region Properties
        private ObservableCollection<string> _PartDescriptionList;
        public ObservableCollection<string> PartDescriptionList
        {
            get { return _PartDescriptionList; }
            set { _PartDescriptionList = value; OnPropertyChanged("PartDescriptionList"); }
        }

        private DataTable _dtEditPart = new DataTable();
        public DataTable dtEditPart
        {
            get { return _dtEditPart; }
            set { _dtEditPart = value; OnPropertyChanged("dtEditPart"); }
        }

        private ObservableCollection<string> _PartList;
        public ObservableCollection<string> PartList
        {
            get { return _PartList; }
            set { _PartList = value; OnPropertyChanged("PartList"); }
        }

        private DataTable _dtDisplayParts = new DataTable();
        public DataTable dtDisplayParts
        {
            get { return _dtDisplayParts; }
            set { _dtDisplayParts = value; OnPropertyChanged("dtDisplayParts"); }
        }

        private DataTable _dtSelectedPart = new DataTable();
        public DataTable dtSelectedPart
        {
            get { return _dtSelectedPart; }
            set { _dtSelectedPart = value; OnPropertyChanged("dtSelectedPart"); }
        }

        private DataTable _dtTotalCurrentValues = new DataTable();
        public DataTable dtTotalCurrentValues
        {
            get { return _dtTotalCurrentValues; }
            set { _dtTotalCurrentValues = value; OnPropertyChanged("dtTotalCurrentValues"); }
        }

        private string _SelectedPart;
        public string SelectedPart
        {
            get { return _SelectedPart; }
            set { _SelectedPart = value; OnPropertyChanged("SelectedPart"); }
        }

        #endregion

        public NextLoadUpdate(int stationno)
        {
            try
            {
                InitializeComponent();
                DataContext = this;
                FillAllGrid();
                txtStationNo.Text = Convert.ToString(stationno);
                this.dgEditPart.AutoGeneratingColumn += detailsViewGrid_AutoGeneratingColumn;
            }
            catch
            { }

        }

        #region methods    
        void detailsViewGrid_AutoGeneratingColumn(object sender, Syncfusion.UI.Xaml.Grid.AutoGeneratingColumnArgs e)
        {
            if (e.Column.MappingName == "DataType")
                e.Column.IsHidden = true;
        }

        private void FillAllGrid()
        {
            try
            {
                // Fill datatable for new part
                try
                {
                    ServiceResponse<DataTable> ResultPartMasterSetting = IndiSCADABusinessLogic.NextloadEntryLogic.getNextloadSettingDetails("AllExceptNxtLd");
                    DTPartMaster = ResultPartMasterSetting.Response;

                    if (DTPartMaster != null)
                    {
                        dtEditPart = new DataTable(); //dtNewPart = new DataTable();
                        dtEditPart.Columns.Add(new DataColumn("Part Details", typeof(string)));
                        dtEditPart.Columns.Add(new DataColumn("Values", typeof(string)));
                        dtEditPart.Columns.Add(new DataColumn("Unit", typeof(string)));
                        dtEditPart.Columns.Add(new DataColumn("DataType", typeof(string)));
                        dtEditPart.Columns[0].ReadOnly = true;
                        dtEditPart.Columns[2].ReadOnly = true;
                        dtEditPart.Columns[3].ReadOnly = true;



                        dtSelectedPart = new DataTable();

                        for (int indexRow = 0; indexRow <= DTPartMaster.Rows.Count - 1; indexRow++)
                        {
                            dtSelectedPart.Columns.Add(new DataColumn(DTPartMaster.Rows[indexRow]["ParameterName"].ToString(), typeof(string)));
                            dtEditPart.Rows.Add(DTPartMaster.Rows[indexRow]["ParameterName"].ToString(), "", DTPartMaster.Rows[indexRow]["Unit"].ToString(), DTPartMaster.Rows[indexRow]["DataType"].ToString());
                        }
                    }

                }
                catch (Exception ex) { }

            
                #region Fill Combobox for edit part
                try
                {
                        DataTable dtParts = new DataTable();
                        ServiceResponse<DataTable> ResultPartMaster = IndiSCADABusinessLogic.NextloadEntryLogic.getPartList();

                        if (ResultPartMaster.Status == ResponseType.S)
                        {
                            dtParts = ResultPartMaster.Response;
                            if (dtParts != null)
                            {
                                if (dtParts.Rows.Count > 0)
                                {
                                    PartList = new ObservableCollection<string>();
                                    for (int indexRow = 0; indexRow < dtParts.Rows.Count; indexRow++)
                                    {
                                        PartList.Add(dtParts.Rows[indexRow][0].ToString());
                                    }
                                }
                            }
                            // dgAllParts.ItemsSource = dtDisplayParts.DefaultView;                        

                        }
                    }
                    catch (Exception ex) { }
                #endregion

                #region Fill Combobox for edit partDescription
                try
                {
                    DataTable dtPartsDesc = new DataTable();
                    ServiceResponse<DataTable> ResultPartMaster = IndiSCADABusinessLogic.NextloadEntryLogic.getPartDescriptionList();

                    if (ResultPartMaster.Status == ResponseType.S)
                    {
                        dtPartsDesc = ResultPartMaster.Response;
                        if (dtPartsDesc != null)
                        {
                            if (dtPartsDesc.Rows.Count > 0)
                            {
                                PartDescriptionList = new ObservableCollection<string>();
                                for (int indexRow = 0; indexRow < dtPartsDesc.Rows.Count; indexRow++)
                                {
                                    PartDescriptionList.Add(dtPartsDesc.Rows[indexRow][0].ToString());
                                }
                            }
                        }
                        // dgAllParts.ItemsSource = dtDisplayParts.DefaultView;                        

                    }
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError.ErrorLog("Nextload fillGrid() Fill Combobox for  partDescription", DateTime.Now.ToString(), ex.ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
                #endregion


                // Fill Total current grid with default columns using nextloadmastersetting
                try
                {
                    DataTable dt = (IndiSCADABusinessLogic.NextloadEntryLogic.getNextloadSettingDetails("All")).Response;
                    foreach (DataRow dr in dt.Rows) //travel all record present in dt
                    {
                        if (dr["IsCalculationRequired"].ToString() == "True")  //if that row contains formula
                        {
                            string colName = dr["ParameterName"].ToString();  //here we are adding that rows parametername 
                            dtTotalCurrentValues.Columns.Add("" + colName); //add new col
                            ////dtTotalCurrentValues.AutoResizeColumns();
                            ////dtTotalCurrentValues.Refresh();
                        }
                    }
                }
                catch (Exception ex) { }
            }
            catch { }

        }
        private void CalculateCurrent()
        {
            try
            {
                //lblErrorMessage.Content = "";

                ServiceResponse<DataTable> ResultPartMasterSetting = IndiSCADABusinessLogic.NextloadEntryLogic.getNextloadSettingDetails("All");
                DataTable dt = ResultPartMasterSetting.Response;
                ServiceResponse<DataTable> Resultdownload = IndiSCADABusinessLogic.NextloadEntryLogic.getNextloadSettingDetails("DownloadToPLC");
                DataTable dtDownload = Resultdownload.Response;
                newaddrList = new string[3];
                addrList = new string[dtDownload.Rows.Count];
                ColumnNames = new string[dtDownload.Rows.Count];
                int index = 0;
                
                try
                {
                    // addrList_new = new string[4];
                    for (int rows = 0; rows < 4; rows++)
                    {
                        newaddrList[index] = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName("LoadEntryEditCurrent1", rows);
                        index++;
                    }
                }
                catch (Exception ex) { ErrorLogger.LogError.ErrorLog("nextloadupd while writting LoadEntryEdit()", DateTime.Now.ToString(), ex.Message, "No", true); }

                //ErrorLogger.LogError.ErrorLog("Nextload CalculateCurrent() 1 ", DateTime.Now.ToString(), dtDownload.Rows.Count.ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                // Read the addresses and columns for calculation
               index = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if ((dr["IsDownloadToPLC"].ToString() == "True") && (dr["IsCalculationRequired"].ToString() == "True"))
                    {
                        addrList[index] = dr["TaskName"].ToString();
                        ColumnNames[index] = dr["ParameterName"].ToString();
                        index++;
                    }

                }
            

                #region CurrentCalculation// Current calculation logic using the formula in setting table
                for (int i = 0; i < dtSelectedPart.Rows.Count; i++)
                {
                    TotalWeight = dtSelectedPart.Rows[i]["Total Weight Per Barrel"].ToString();
                    WeightPerPart = dtSelectedPart.Rows[i]["Weight Per Part"].ToString();
                    index = 0;
                    if ((TotalWeight != "") && (WeightPerPart != ""))
                    {
                        int a = 0; int b = 0; int c = 0;
                        // dtSelectedPart.Rows[i]["Quantity"] = (((Convert.ToInt32(TotalWeight)) *1000)/ Convert.ToDouble(WeightPerPart)).ToString();

                        //dtSelectedPart.Rows[i]["Quantity"] = ((Convert.ToInt32(PiecesPerHanger)*1000) / Convert.ToInt32(NoOfHangers)).ToString();
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["IsCalculationRequired"].ToString() == "True")
                            {
                                string s1;

                                string formula = dr["CalculationFormula"].ToString();
                                string[] elements = formula.Split('.');
                                string calc = "";
                                foreach (string s in elements)
                                {
                                    if (s.StartsWith("("))
                                    {
                                        s1 = s.Replace("(", String.Empty);
                                        if (s1.StartsWith("["))
                                        {
                                            string colname = s1.Substring(1, s1.Length - 2);
                                            calc += "(" + dtSelectedPart.Rows[i][colname].ToString();
                                        }
                                        else
                                        {
                                            calc += s1;
                                        }
                                    }
                                    else
                                    {
                                        if (s.StartsWith("["))
                                        {
                                            string colname = s.Substring(1, s.Length - 2);
                                            calc += dtSelectedPart.Rows[i][colname].ToString();
                                        }
                                        else
                                        {
                                            calc += s;
                                        }
                                    }

                                }
                                DataTable calDT = new DataTable();
                                Object value = calDT.Compute(calc, "");

                                //if (Convert.ToInt64(value) > 600)
                                //{
                                //    lblErrorMessage.Content = "Calculated Current must be less than 600";
                                //}
                                value = Math.Round(Convert.ToDouble(value), 2);
                                dtSelectedPart.Rows[i][ColumnNames[index]] = value.ToString();



                                for (int RIndex = 0; RIndex < dtEditPart.Rows.Count; RIndex++)
                                {
                                    if (dtEditPart.Rows[RIndex][0].ToString() == ColumnNames[index].ToString())
                                    {
                                        dtEditPart.Rows[RIndex][1] = value.ToString();
                                    }
                                }

                                index++;
                            }
                        }
                    }
                    else
                    {

                    }


                }
            }
            catch (Exception ex)
            {
                // objLogging.LogError(DateTime.Now, "frmNextLoad_btnCalculateCurrent_Click", ex.Message, 0);
                ErrorLogger.LogError.ErrorLog("Nextload CalculateCurrent() Calculation using Formula : ", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            #endregion

            #region //Total Current Display logic
            try
            {
                //lblMessage.Content = "";
                string Current1ExtendLimit = ""; string Current2ExtendLimit = ""; string Current3ExtendLimit = ""; string Current4ExtendLimit = "";
                for (int records = 0; records < dtTotalCurrentValues.Columns.Count; records++) //1st 
                {
                    double totalcurrent = 0; int Noofrecords; double column1 = 0;
                    string colname = this.dtTotalCurrentValues.Columns[records].ColumnName;
                    if (dtTotalCurrentValues.Rows.Count == 0)
                    {
                        dtTotalCurrentValues.Rows.Add();
                    }

                    try
                    {
                        for (Noofrecords = 0; Noofrecords < dtSelectedPart.Rows.Count; Noofrecords++)//sum all curr
                        {
                            try
                            {
                                column1 = Convert.ToDouble(dtSelectedPart.Rows[Noofrecords][colname].ToString());
                                totalcurrent = totalcurrent + column1;
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("Nextload CalculateCurrent() TotalCurrent 1 :", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }
                        }
                        dtTotalCurrentValues.Rows[0][colname] = totalcurrent.ToString();

                        //current exceed then show message

                        string current = dtTotalCurrentValues.Rows[0][records].ToString();
                        if (Convert.ToDouble(dtTotalCurrentValues.Rows[0][records]) > 1200 && colname == "Anodic 1 Current")
                        {
                            dtTotalCurrentValues.Rows[0][records] = "";
                            Current1ExtendLimit = " 'Anodic 1' ";
                        }
                        else if (Convert.ToDouble(dtTotalCurrentValues.Rows[0][records]) > 1200 && colname == "Anodic 2 Current")
                        {
                            dtTotalCurrentValues.Rows[0][records] = "";
                            Current2ExtendLimit = " 'Anodic 2' ";
                        }
                        else if (Convert.ToDouble(dtTotalCurrentValues.Rows[0][records]) > 1200 && colname == "Alkaline Zinc Current")
                        {
                            dtTotalCurrentValues.Rows[0][records] = "";
                            Current3ExtendLimit = " 'Alkaline zinc' ";
                        }
                        //else if (Convert.ToDouble(dtTotalCurrentValues.Rows[0][records]) > 1500 && colname == "Zinc Nickel Current")
                        //{
                        //    dtTotalCurrentValues.Rows[0][records] = "";
                        //    Current4ExtendLimit = " 'Zinc Nickel' ";
                        //}

                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("Nextload CalculateCurrent() TotalCurrent 2 :", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                    }
                }
                //if (Current1ExtendLimit != "" || Current2ExtendLimit != "" || Current3ExtendLimit != "" || Current4ExtendLimit != "")
                if (Current1ExtendLimit != "" || Current2ExtendLimit != "" || Current3ExtendLimit != "" )
                {
                    System.Windows.MessageBox.Show(Current1ExtendLimit + Current2ExtendLimit + Current3ExtendLimit + "Current Extends its Limit! current must be less than 1500" + " !");
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("Nextload CalculateCurrent() TotalCurrent 3 :", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
            #endregion
        }

        private void DownloadDataToPLC(string Lno)
        {
            try
            {

                int StationNumber = Convert.ToInt16(txtStationNo.Text);
                ErrorLogger.LogError.ErrorLog("NextLoadUpdate DownloadDataToPLC() StationNumber" + StationNumber, DateTime.Now.ToString(), "", "No", true);

                if (dtSelectedPart.Rows.Count > 0)
                {
                    try
                    {
                        // Load No array
                        string[] LoadNoAtStation = IndiSCADAGlobalLibrary.TagList.LoadNoAtStation;

                        if (LoadNoAtStation[StationNumber - 1] != null)
                        {
                            if (Convert.ToInt16(LoadNoAtStation[StationNumber - 1]) > 0)
                            {
                                CalculateCurrent(); //Calculate Current

                                #region Delete_Records_From _LoadPartDetails

                                try
                                {

                                    ServiceResponse<DataTable> getData = IndiSCADABusinessLogic.NextloadEntryLogic.DTCheckLoadisPResent(Lno);
                                    DataTable DTCheckLoadisPResent = getData.Response;

                                    if (DTCheckLoadisPResent != null)
                                    {
                                        if (DTCheckLoadisPResent.Rows.Count != 0)
                                        {
                                            string QueryDelete = "Delete from LoadPartDetails where LoadNumber like '" + Lno + "'";
                                            ServiceResponse<int> result = IndiSCADABusinessLogic.NextloadEntryLogic.DeleteFromLoadPartDetails(QueryDelete);

                                            if (result.Response > 0)
                                            { }
                                            else
                                            {

                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                { }
                                #endregion Delete_Records_From _LoadPartDetails

                                #region update Partmaster as part selected 
                                //    try
                                //    {
                                //        try
                                //        {
                                //            string query = "Update PartMaster set isSelectedforNextLoad=0 where isSelectedforNextLoad = 1";
                                //            ServiceResponse<int> _UpdateResult = (NextloadEntryLogic.ResetPartforNextLoad(query));
                                //        }
                                //        catch (Exception ex) { }
                                //        foreach (DataRow dr in dtSelectedPart.Rows)
                                //        {
                                //            ServiceResponse<int> result = NextloadEntryLogic.UpdateSelectedPartforNextLoad(dr);
                                //        }
                                //    }
                                //    catch (Exception ex)
                                //    { }

                                #endregion update Partmaster as part selected 

                                #region insert_into_LoadPartData
                                try
                                {

                                    ServiceResponse<DataTable> getData = IndiSCADABusinessLogic.NextloadEntryLogic.DTCheckLoadisPResent(Lno);
                                    DataTable dtParts = new DataTable();
                                    dtParts = getData.Response;
                                    if (dtParts.Rows.Count == 0)
                                    {

                                        ServiceResponse<DataTable> getDataNextLoadMasterSettings = IndiSCADABusinessLogic.NextloadEntryLogic.getDataNextLoadMasterSettingsForDataLogging();
                                        DataTable dt = getDataNextLoadMasterSettings.Response;
                                        ServiceResponse<DataTable> getDataPartMasterForDataLogging = IndiSCADABusinessLogic.NextloadEntryLogic.getDataPartMasterForDataLogging1();
                                        DataTable dtPartData = getDataPartMasterForDataLogging.Response;

                                        ServiceResponse<int> res = IndiSCADABusinessLogic.NextloadEntryLogic.insertLNInToLoadPartData(Lno, dt, dtPartData);

                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                                #endregion insert_into_LoadPartData

                                #region Enter the entry (set allow to edit 1) (Edit Load Entry) commented
                                //try
                                //{
                                //    int res = 0;
                                //    string address = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName("Enter", 0);
                                //    short[] arrDeviceValue1 = new short[1];//Data for 'DeviceValue'
                                //    arrDeviceValue1[0] = 1;
                                //    System.String[] arrData1 = new System.String[] { address };
                                //    DeviceCommunication.CommunicationWithPLC.WriteValuesArray(arrData1, arrDeviceValue1, 1);
                                //    ErrorLogger.LogError.ErrorLog("Write Error ResetNextLoad/Enter", DateTime.Now.ToString(), "Write Error ResetNextLoad" + arrDeviceValue1 + arrData1, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                                //}
                                //catch (Exception ex)
                                //{
                                //    ErrorLogger.LogError.ErrorLog("next load update screen : ResetNextLoad/Enter", DateTime.Now.ToString(), "next load update screen : ResetNextLoad", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                                //}

                                #endregion


                                //here we writting program no
                                try
                                {
                                    string programNo = "";
                                    try
                                    {
                                        string a = "Passivation Selection";
                                        DataRow[] results = dtEditPart.Select(" [Part Details]='" + a + "'");
                                        programNo = results[0][1].ToString();
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                    IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("LoadtypeEdit", StationNumber - 1, programNo);
                                    ErrorLogger.LogError.ErrorLog("NextLoadUpdate DownloadDataToPLC() LoadtypeEdit" + programNo+" Index="+ (StationNumber - 1).ToString(), DateTime.Now.ToString(), "", "No", true);
                                }
                                catch (Exception exExitButtonCommandClick)
                                {
                                    ErrorLogger.LogError.ErrorLog("nextloadupd LoadtypeEdit()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
                                }


                                try
                                {
                                    #region here we are downloading dipTime values in PLC(commented now)
                                    //if (comboBox_programNo.Text != "")
                                    //{
                                    //    int ProgramNumber = Convert.ToInt32(comboBox_programNo.Text);
                                    //    int saved = objMainScreen.WriteValueForDipTime(ProgramNumber);
                                    //    if (saved == 1)
                                    //    {
                                    //        MessageBox.Show("DipTime values are saved succefully");
                                    //    }
                                    //    else
                                    //    {
                                    //        MessageBox.Show("Error while saving DipTime values");
                                    //    }

                                    //}
                                    //else
                                    //{
                                    //    MessageBox.Show("Please select program number");
                                    //}
                                    #endregion

                                    // Write values in PLC Read address from xml and use it to write
                                 if (newaddrList.Length > 0 && ColumnNames.Length > 0)
                                    {
                                        short[] arrDeviceValue = new short[newaddrList.Length];            //Data for 'DeviceValue'
                                        double[] CurrentValues = new double[newaddrList.Length];
                                        System.String[] arrData = new string[newaddrList.Length];
                                        //Get address from xml
                                        for (int index = 0; index < addrList.Length; index++)
                                        {
                                            if (addrList[index] != null)
                                            { 
                                                arrData[index] = newaddrList[index];
                                            }
                                        }

                                        //Summation of the current for all selected parts
                                        for (int index = 0; index < ColumnNames.Length; index++)
                                        {
                                            if (ColumnNames[index] != null)
                                            {
                                                for (int rowno = 0; rowno < dtSelectedPart.Rows.Count; rowno++)
                                                {
                                                    string colname = ColumnNames[index].ToString();
                                                    CurrentValues[index] += Convert.ToDouble(dtSelectedPart.Rows[rowno][colname].ToString());
                                                }
                                            }
                                        }


                                        #region Write StationNo in plc  commented
                                        //Write Station Num in plc
                                        //try
                                        //{ 
                                        //    int res = 0;
                                        //    short StationNo = Convert.ToInt16(txtStationNo.Text);
                                        //    string address = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName("StationToBeEdit", 0);
                                        //    ErrorLogger.LogError.ErrorLog("Nextload WriteValues() 1 ", DateTime.Now.ToString(), "Write Error StationSelection/StationToBeEdit" + address, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                                        //    short[] arrDeviceValue1 = new short[1];         //Data for 'DeviceValue'
                                        //    arrDeviceValue1[0] = StationNo;
                                        //    System.String[] arrData1 = new System.String[] { address };
                                        //    DeviceCommunication.CommunicationWithPLC.WriteValuesArray(arrData1, arrDeviceValue1, 1); //(arrDeviceValue1, arrData1, 1);
                                        //    ErrorLogger.LogError.ErrorLog("Nextload WriteValues() 1 ", DateTime.Now.ToString(), "Write Error StationSelection/StationToBeEdit" + arrDeviceValue1 + arrData1, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                                        //}
                                        //catch (Exception ex)
                                        //{
                                        //    ErrorLogger.LogError.ErrorLog("Nextload WriteValues() 1 ", DateTime.Now.ToString(), "Write Error StationSelection/StationToBeEdit", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                                        //}

                                        #endregion


                                        // assign selected values to write in plc
                                        for (int index = 0; index < ColumnNames.Length; index++)
                                        {
                                            if (ColumnNames[index] != null)
                                            {
                                                arrDeviceValue[index] = (Convert.ToInt16(CurrentValues[index]));
                                            }
                                        }

                                        //ErrorLogger.LogError.ErrorLog("Nextload WriteValues() 1 ", DateTime.Now.ToString(), CurrentValues.Length + " : " + arrDeviceValue.Length + " : " + arrData.Length, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                                        //DeviceCommunication.CommunicationWithPLC.WriteValuesArray(arrData, arrDeviceValue, newaddrList.Length);

                                        try
                                        {

                                            IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("LoadEntryEditCurrent1", StationNumber - 1, arrDeviceValue[0].ToString());
                                            ErrorLogger.LogError.ErrorLog("NextLoadUpdate DownloadDataToPLC() LoadEntryEditCurrent1" + arrDeviceValue[0].ToString() + " Index=" + (StationNumber - 1).ToString(), DateTime.Now.ToString(), "", "No", true);

                                            IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("LoadEntryEditCurrent2", StationNumber - 1, arrDeviceValue[1].ToString());
                                            ErrorLogger.LogError.ErrorLog("NextLoadUpdate DownloadDataToPLC() LoadEntryEditCurrent2" + arrDeviceValue[1].ToString() + " Index=" + (StationNumber - 1).ToString(), DateTime.Now.ToString(), "", "No", true);

                                            IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("LoadEntryEditCurrent3", StationNumber - 1, arrDeviceValue[2].ToString());
                                            ErrorLogger.LogError.ErrorLog("NextLoadUpdate DownloadDataToPLC() LoadEntryEditCurrent3" + arrDeviceValue[2].ToString() + " Index=" + (StationNumber - 1).ToString(), DateTime.Now.ToString(), "", "No", true);

                                            lblDownloadStatus.Content = "Download Status : SUCCESS ";
                                        }
                                        catch(Exception ex)
                                        {

                                            lblDownloadStatus.Content = "Download Status : Fail ";

                                            ErrorLogger.LogError.ErrorLog("NextLoadUpdate DownloadDataToPLC() StationNumber" + StationNumber, DateTime.Now.ToString(), ex.Message, "No", true);
                                        }
                                        





                                        #region Fill Combobox for edit part
                                        try
                                        {
                                            DataTable dtParts = new DataTable();
                                            ServiceResponse<DataTable> ResultPartMaster = IndiSCADABusinessLogic.NextloadEntryLogic.getPartList();

                                            if (ResultPartMaster.Status == ResponseType.S)
                                            {
                                                dtParts = ResultPartMaster.Response;
                                                if (dtParts != null)
                                                {
                                                    if (dtParts.Rows.Count > 0)
                                                    {
                                                        PartList = new ObservableCollection<string>();
                                                        for (int indexRow = 0; indexRow < dtParts.Rows.Count; indexRow++)
                                                        {
                                                            PartList.Add(dtParts.Rows[indexRow][0].ToString());
                                                        }
                                                    }
                                                }
                                                // dgAllParts.ItemsSource = dtDisplayParts.DefaultView;                        

                                            }
                                        }
                                        catch (Exception ex) { }
                                        #endregion
                                    }
                                    else
                                    {
                                        lblDownloadStatus.Content = "Download : FAIL (No parts selected) ";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    lblDownloadStatus.Content = "Download Status : Fail to save data ";
                                }


                            }//end of loLoadNoAtStationad >0
                            else
                            {
                                lblDownloadStatus.Content = "Download : FAIL (Tank is empty at Station no "+ StationNumber + ") ";
                            }
                        }//end of loLoadNoAtStationad != null
                        else
                        {
                            lblDownloadStatus.Content = "Download : FAIL (Tank is empty at Station no " + StationNumber + ") ";
                        }
                    }
                    catch (Exception ex) { }
                }
                else
                {

                }
            }
            catch (Exception ex) { }

        }


        #endregion
        #region events
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }


        private void CmbPartlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataTable dtpartdetails = new DataTable(); int colIndex = -1; int isSelectedForNextLoadCol = 0;
                ServiceResponse<DataTable> ResultPartMaster = IndiSCADABusinessLogic.NextloadEntryLogic.getPartDetails(cmbPartlist.SelectedItem.ToString(), "SelectPart", "");
                if (ResultPartMaster.Status == ResponseType.S)
                {
                    dtpartdetails = ResultPartMaster.Response;
                    for (int i = 0; i < dtpartdetails.Columns.Count; i++)  //added by sbs to avoid paste for is selected from nextload
                    {
                        string temp2 = dtpartdetails.Columns[i].ColumnName.ToString();
                        if (temp2 == "isSelectedForNextLoad")
                        {
                            colIndex = i;
                        }
                    }

                    for (int indexRow = 0; indexRow <= dtpartdetails.Columns.Count - 1; indexRow++)
                    {
                        if (indexRow == colIndex)
                        {
                            isSelectedForNextLoadCol = 1;
                        }
                        else
                        {
                            if (isSelectedForNextLoadCol == 0)
                            {
                                dtEditPart.Rows[indexRow][1] = dtpartdetails.Rows[0][indexRow].ToString();
                            }
                            else
                            {
                                dtEditPart.Rows[indexRow - 1][1] = dtpartdetails.Rows[0][indexRow].ToString();
                            }
                        }

                        if (dtEditPart.Rows[indexRow][0].ToString() == "Passivation Selection" || dtEditPart.Rows[indexRow][0].ToString() == "Total Weight Per Barrel" || dtEditPart.Rows[indexRow][0].ToString() == "Quantity")//|| dtEditPart.Rows[indexRow][0].ToString() == "DF Number" 
                        {
                            dtEditPart.Rows[indexRow][1] = "";
                        }
                    }
                }
            }
            catch { }
        }

        private void cmbDescriptionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
           {
            //    DataTable dtpartdescription = new DataTable();
            //    ServiceResponse<DataTable> ResultPartMaster = IndiSCADABusinessLogic.NextloadEntryLogic.getPartDetails("", "SelectDescription", cmbDescriptionList.SelectedItem.ToString());
            //    if (ResultPartMaster.Status == ResponseType.S)
            //    {
            //        dtpartdescription = ResultPartMaster.Response;

            //        dtpartdescription = ResultPartMaster.Response;
            //        if (dtpartdescription != null)
            //        {
            //            if (dtpartdescription.Rows.Count > 0)
            //            {
            //                PartList = new ObservableCollection<string>();
            //                for (int indexRow = 0; indexRow < dtpartdescription.Rows.Count; indexRow++)
            //                {
            //                    PartList.Add(dtpartdescription.Rows[indexRow][0].ToString());
            //                }
            //            }
            //        }

                    //for (int indexRow = 0; indexRow <= dtpartdescription.Columns.Count - 1; indexRow++)
                    //{
                    //    dtEditPart.Rows[indexRow][1] = dtpartdescription.Rows[0][indexRow].ToString();
                    //    if (dtEditPart.Rows[indexRow][0].ToString() == "Passivation Selection" || dtEditPart.Rows[indexRow][0].ToString() == "Total Weight" || dtEditPart.Rows[indexRow][0].ToString() == "Quantity")
                    //    {
                    //        dtEditPart.Rows[indexRow][1] = "";
                    //    }
                    //}
              //  }

            }
            catch { }
        }

        //validation
        private void DgEditPart_CurrentCellValidating(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellValidatingEventArgs e)
        {
            try
            {
                string newvalue = e.NewValue.ToString();

                DataRowView dr = (DataRowView)e.RowData;

                //if (dr[3].ToString() == "")
                //{
                //    //string newvalue = e.NewValue.ToString();

                //        e.IsValid = false;
                //        e.ErrorMessage = "Can not be empty";

                //}
                if (dr[3].ToString() == "int" || dr[3].ToString() == "number")
                {
                    //string newvalue = e.NewValue.ToString();
                    if (!newvalue.All(char.IsDigit))
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "Only numbers are allowed";
                    }
                }
                if (dr[3].ToString() == "float")
                {
                    //string newvalue = e.NewValue.ToString();
                    float val = 0;
                    bool valid = float.TryParse(newvalue, out val);
                    if (!valid)
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "Only real numbers are allowed";
                    }
                }
                string MinValue = "", MaxValue = "", datatype = "";
                try
                {
                    DataRow[] results = DTPartMaster.Select(" ParameterName='" + dr[0].ToString() + "'");
                    MinValue = results[0]["MinValue"].ToString();
                    MaxValue = results[0]["MaxValue"].ToString();
                    datatype = results[0]["DataType"].ToString();
                }
                catch (Exception ex)
                {
                }

                //added by sbs to directly validate min max limit from database
                if (MinValue != "" && MaxValue != "" && datatype != "")
                {
                    if (datatype == "int" || datatype == "Int" || datatype == "INT")
                    {
                        if (Convert.ToInt64(newvalue) >= Convert.ToInt64(MinValue) && Convert.ToInt64(newvalue) <= Convert.ToInt64(MaxValue))
                        {

                        }
                        else
                        {
                            e.IsValid = false;
                            e.ErrorMessage = "Must be in range " + MinValue + " to " + MaxValue;
                        }
                    }
                    if (datatype == "float" || datatype == "Float" || datatype == "FLOAT")
                    {
                        if (Convert.ToDouble(newvalue) >= Convert.ToDouble(MinValue) && Convert.ToDouble(newvalue) <= Convert.ToDouble(MaxValue))
                        {

                        }
                        else
                        {
                            e.IsValid = false;
                            e.ErrorMessage = "Must be in range " + MinValue + " to " + MaxValue;
                        }
                    }
                }
                if (dr[0].ToString() == "Total Weight Per Barrel")
                {
                    double wtperpart = 0;
                    if (Convert.ToInt64(newvalue) <= 100 && Convert.ToInt64(newvalue) >= 0)
                    {
                        for (int j = 0; j < dtEditPart.Rows.Count; j++)
                        {
                            if (dtEditPart.Rows[j][0].ToString() == "Weight Per Part")
                            {
                                wtperpart = Convert.ToDouble(dtEditPart.Rows[j][1].ToString());
                            }
                        }
                        for (int j = 0; j < dtEditPart.Rows.Count; j++)
                        {
                            if (dtEditPart.Rows[j][0].ToString() == "Quantity")
                            {
                                dtEditPart.Rows[j][1] = Convert.ToInt32((Convert.ToDouble(newvalue) * 1000) / wtperpart);
                            }
                        }
                    }
                    else
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "Must be in range 0 to 100 kg";
                    }
                }

                if (dr[0].ToString() == "Anodic 1 CD")//dm2
                {
                    if (Convert.ToDouble(newvalue) <= 10 && Convert.ToDouble(newvalue) >= 0)
                    {
                        for (int j = 0; j < dtEditPart.Rows.Count; j++)
                        {
                            if (dtEditPart.Rows[j][0].ToString() == "Anodic 1 CD mm2")
                            {
                                dtEditPart.Rows[j][1] = Convert.ToDouble(newvalue) * 10000;
                            }
                        }
                    }
                    else
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "Must be in range 0 to 10 dm2";
                    }
                }

                if (dr[0].ToString() == "Anodic 2 CD")
                {
                    if (Convert.ToDouble(newvalue) <= 10 && Convert.ToDouble(newvalue) >= 0)
                    {
                        for (int j = 0; j < dtEditPart.Rows.Count; j++)
                        {
                            if (dtEditPart.Rows[j][0].ToString() == "Anodic 2 CD mm2")
                            {
                                dtEditPart.Rows[j][1] = Convert.ToDouble(newvalue) * 10000;
                            }
                        }
                    }
                    else
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "Must be in range 0 to 10 dm2";
                    }
                }

                if (dr[0].ToString() == "Alkaline Zinc CD")
                {
                    if (Convert.ToDouble(newvalue) <= 10 && Convert.ToDouble(newvalue) >= 0)
                    {
                        for (int j = 0; j < dtEditPart.Rows.Count; j++)
                        {
                            if (dtEditPart.Rows[j][0].ToString() == "Alkaline Zinc CD mm2")
                            {
                                dtEditPart.Rows[j][1] = Convert.ToDouble(newvalue) * 10000;
                            }
                        }
                    }
                    else
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "Must be in range 0 to 10 dm2";
                    }
                }
                if (dr[0].ToString() == "Zinc Nickel CD")
                {
                    if (Convert.ToDouble(newvalue) <= 10 && Convert.ToDouble(newvalue) >= 0)
                    {
                        for (int j = 0; j < dtEditPart.Rows.Count; j++)
                        {
                            if (dtEditPart.Rows[j][0].ToString() == "Zinc Nickel CD mm2")
                            {
                                dtEditPart.Rows[j][1] = Convert.ToDouble(newvalue) * 10000;
                            }
                        }
                    }
                    else
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "Must be in range 0 to 10 dm2";
                    }
                }
                if (dr[0].ToString() == "Anodic 1 CD mm2")
                {
                    if (Convert.ToDouble(newvalue) <= 100000 && Convert.ToDouble(newvalue) >= 0)
                    {
                        for (int j = 0; j < dtEditPart.Rows.Count; j++)
                        {
                            if (dtEditPart.Rows[j][0].ToString() == "Anodic 1 CD")
                            {
                                dtEditPart.Rows[j][1] = Convert.ToDouble(newvalue) / 10000;
                            }
                        }
                    }
                    else
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "Must be in range 0 to 100000 mm2";
                    }
                }
                if (dr[0].ToString() == "Anodic 2 CD mm2")
                {
                    if (Convert.ToDouble(newvalue) <= 100000 && Convert.ToDouble(newvalue) >= 0)
                    {
                        for (int j = 0; j < dtEditPart.Rows.Count; j++)
                        {
                            if (dtEditPart.Rows[j][0].ToString() == "Anodic 2 CD")
                            {
                                dtEditPart.Rows[j][1] = Convert.ToDouble(newvalue) / 10000;
                            }
                        }
                    }
                    else
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "Must be in range 0 to 100000 mm2";
                    }
                }
                if (dr[0].ToString() == "Alkaline Zinc CD mm2")
                {
                    if (Convert.ToDouble(newvalue) <= 100000 && Convert.ToDouble(newvalue) >= 0)
                    {
                        for (int j = 0; j < dtEditPart.Rows.Count; j++)
                        {
                            if (dtEditPart.Rows[j][0].ToString() == "Alkaline Zinc CD")
                            {
                                dtEditPart.Rows[j][1] = Convert.ToDouble(newvalue) / 10000;
                            }
                        }
                    }
                    else
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "Must be in range 0 to 100000 mm2";
                    }
                }
                if (dr[0].ToString() == "Zinc Nickel CD mm2")
                {
                    if (Convert.ToDouble(newvalue) <= 100000 && Convert.ToDouble(newvalue) >= 0)
                    {
                        for (int j = 0; j < dtEditPart.Rows.Count; j++)
                        {
                            if (dtEditPart.Rows[j][0].ToString() == "Zinc Nickel CD")
                            {
                                dtEditPart.Rows[j][1] = Convert.ToDouble(newvalue) / 10000;
                            }
                        }
                    }
                    else
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "Must be in range 0 to 100000 mm2";
                    }
                }
                if (dr[0].ToString() == "Passivation Selection")
                {
                    if (Convert.ToInt64(newvalue) <= 6 && Convert.ToInt64(newvalue) >= 1)
                    {
                    }
                    else
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "Must be in range 0 to 4";
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void BtnAddPart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                programNo = 0;
                {
                    string RecipeNumber = cmbPartlist.Text;
                    int ifExist = 0;
                    for (int row = 0; row < dtSelectedPart.Rows.Count; row++)
                    {
                        string PartNumber = Convert.ToString(dtSelectedPart.Rows[row]["Part Number"].ToString());

                        if (dtSelectedPart.Rows[row][0].ToString() != null && (PartNumber == RecipeNumber))
                        {
                            ifExist = ifExist + 1;
                        }
                    }
                    if (ifExist == 0)
                    {
                        ////allow single part for nextload
                        //int SelectedParts = dtSelectedPart.Rows.Count;
                        //if (SelectedParts == 0)
                        //{
                            string[] newrow = new string[dtSelectedPart.Columns.Count];
                            DataTable dataTable = dtSelectedPart;
                            DataGridViewRow _DTRowAdd = new DataGridViewRow();
                            for (int index = 0; index <= DTPartMaster.Rows.Count - 1; index++)
                            {
                                if (dtEditPart.Rows[index][0].ToString() == "Total Weight Per Barrel")
                                {
                                    if (dtEditPart.Rows[index][1].ToString() == "")
                                    {
                                        //lblMessage.Content = "TotalWeight is Empty";
                                        System.Windows.MessageBox.Show("TotalWeight is Empty");
                                        return;
                                    }
                                    //programNo = Convert.ToInt16(dtEditPart.Rows[index][1].ToString());
                                }
                                if (dtEditPart.Rows[index][0].ToString() == "Passivation Selection")
                                {
                                    if (dtEditPart.Rows[index][1].ToString() == "")
                                    {
                                        //lblMessage.Content = "Passivation Selection Empty";
                                        System.Windows.MessageBox.Show("Passivation Selection is Empty");
                                        return;
                                    }
                                }
                                //if (dtEditPart.Rows[index][0].ToString() != "Anodic Cleaning 1 Current" && dtEditPart.Rows[index][0].ToString() != "Anodic Cleaning 2 Current" && dtEditPart.Rows[index][0].ToString() != "Alkaline Zinc Current" && dtEditPart.Rows[index][1].ToString() == "")
                                //{
                                //    System.Windows.MessageBox.Show("Please fill value for " + dtEditPart.Rows[index][0].ToString());
                                //    return;
                                //}
                                //if (DTPartMaster.Rows[index][0].ToString() != "isSelectedForNextLoad")
                                //{

                                string name = DTPartMaster.Rows[index][1].ToString();
                                newrow[index] = dtEditPart.Rows[index][1].ToString();
                                //}                           
                            }
                            dtSelectedPart.Rows.Add(newrow);
                            lblMessage.Content = "";
                        //}
                        //else//more than 1 part
                        //{
                        //    System.Windows.MessageBox.Show("Muliparts are not Allowed!");
                        //}
                    }
                    else
                    {
                        lblMessage.Content = "Part Already exists";
                    }


                }

            }
            catch (Exception ex) { }

        }

        private void BtnRemovePart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgSelectedPart.SelectedIndex >= 0)
                {
                    dtSelectedPart.Rows.RemoveAt(dgSelectedPart.SelectedIndex);
                }
                else
                {
                    lblMessage.Content = "Select part to remove";
                    
                    
                }
            }
            catch (Exception ex) { }

        }
        //shubbham
      

        private void DgEditPart_CurrentCellBeginEdit(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellBeginEditEventArgs e)
        {
            var index = e.RowColumnIndex;
            try
            {
                //allow pasivation selection and total weight and cd's to edit 
                if ((index.RowIndex == 1) || (index.RowIndex == 2) || (index.RowIndex == 3) || (index.RowIndex == 4) || (index.RowIndex == 5) || (index.RowIndex == 6) || (index.RowIndex == 8) || (index.RowIndex == 11) || (index.RowIndex == 14) || (index.RowIndex == 17) || (index.RowIndex == 20))
                {
                    e.Cancel = true;
                }


                //if ((index.RowIndex == 7) || (index.RowIndex == 18)) //allow pasivation selection and total weight to edit
                //{
                //    e.Cancel = false;
                //}
                //else
                //{
                //    e.Cancel = true;
                //}
            }
            catch { }
        }

      


        //for range validation    
        //  private void DgEditPart_RowValidating(object sender, Syncfusion.UI.Xaml.Grid.RowValidatingEventArgs e)
        //{
        //try
        //{
        //    DataRowView drForRange = (DataRowView)e.RowData;
        //    if (drForRange[0].ToString() == "PassivationSelection")
        //    {
        //        if (Convert.ToInt64(drForRange[1].ToString()) <= 4 && Convert.ToInt64(drForRange[1].ToString()) >= 1)
        //        {
        //        }
        //        else
        //        {
        //            e.IsValid = false;
        //            e.ErrorMessages.Add(drForRange[1].ToString(), "Customer AROUT cannot be passed");
        //        }
        //    }
        //}
        //catch (Exception ex) { }

        //DataRowView dr = (DataRowView)e.RowData;
        //string a = dr[0].ToString();
        //string b = dr[1].ToString();

        //e.IsValid = false;
        // e.ErrorMessages.Add(a, "Customer AROUT cannot be passed");

        //if (e.Cell.Column.UniqueName == "CountryId")
        //{
        //    int newValue = Int32.Parse(e.NewValue.ToString());
        //    if (newValue < 0 || newValue > 12)
        //    {

        // e.ErrorMessages = "The entered value must be between 0 and 12";

        //    }
        //}
        //if (Convert.ToInt16(dr[1]) == "int" || dr[1].ToString() == "number")
        //{
        //    string newvalue = e.NewValue.ToString();
        //    if (!newvalue.All(char.IsDigit))
        //    {
        //        e.IsValid = false;
        //        e.ErrorMessage = "Only numbers are allowed";
        //    }            
        //}

        //string value = dr.Row.ItemArray[1].ToString();
        //var data = e.RowData.GetType().GetProperty("Values").GetValue(e.RowData);
        //if (value.ToString().Equals("21"))
        //{
        //    e.IsValid = false;
        //    e.ErrorMessages.Add("CustomerID", "Customer AROUT cannot be passed");
        //    //e.ErrorMessage("Customer AROUT cannot be passed");
        //}
        //}



        private void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            //DownloadDataToPLC("20209");
            //LoginView loginView1 = new LoginView();
            //loginView1.ShowDialog();
            //DownloadDataToPLC("2020/8/09-16");
            try
            {
                if (txtStationNo.Text != null)
                {


                    string[] LoadNoAtStation = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("LoadNumberatStationArrayLoadNumber");
                    int stationNo = Convert.ToInt32(txtStationNo.Text);

                    string LoadNo_at_station = LoadNoAtStation[stationNo - 1];
                    ErrorLogger.LogError.ErrorLog("NextLoadUpdate BtnDownload_Click() LoadNo_at_station=" + LoadNoAtStation[stationNo - 1].ToString(), DateTime.Now.ToString(), "", "No", true);

                    int load_no = Convert.ToInt32(LoadNo_at_station);


                    if (load_no == 0)
                    {
                        if (System.Windows.MessageBox.Show("There is no load present in this station. Do You Want To still OverWrite current calculations?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            DownloadPL(stationNo, load_no);
                        }
                        else
                        {
                            return;
                            //System.Windows.MessageBox.Show("There is no load present in this station !");
                        }
                    }
                    else
                    {
                        DownloadPL(stationNo, load_no);
                        #region commented
                        ////string GetLoadNumber = "select LoadNumber from LoadDipTime where StationNumber=" + stationNo + " and Status=0 order by desc";
                        //ServiceResponse<DataTable> GetLoadNumber1 = IndiSCADABusinessLogic.NextloadEntryLogic.GetLoadNumber(stationNo);
                        //DataTable Dt = GetLoadNumber1.Response;
                        //string LoadNumber=Dt.Rows[0]["LoadNumber"].ToString();
                        //string[] arr = LoadNumber.Split('-');
                        ////string lno = DateTime.Now.ToString("yyyy/MM//DD") + "-" + load_no;

                        //if (Convert.ToInt32(arr[1]) != load_no)
                        //{
                        //    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Load Number are Mismatch. Do You Want To OverWrite?", "NextLoadUpdate", MessageBoxButton.YesNoCancel);
                        //    if (messageBoxResult == MessageBoxResult.Yes)
                        //    {
                        //        if (stationNo <= length && stationNo > 0)
                        //        {
                        //            string[] CycleStartStop = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Cycleinprocess");// SystemInputsInput");{ "0"}
                        //            if (CycleStartStop.Length != 0)
                        //            {
                        //                if (CycleStartStop[0] == "0" || CycleStartStop[0] == "False")
                        //                {
                        //                    int TotalCurrentValue = 0;
                        //                    // Add user role validation in this
                        //                    string username = IndiSCADAGlobalLibrary.UserLoginDetails.UserName; // commented for testing purpose
                        //                    string useraccesslevel = IndiSCADAGlobalLibrary.UserLoginDetails.AccessLevel;

                        //                    if (username != null)
                        //                    {
                        //                        for (int j = 0; j < dtTotalCurrentValues.Columns.Count; j++)
                        //                        {
                        //                            string q = dtTotalCurrentValues.Rows[0][j].ToString();
                        //                            if (dtTotalCurrentValues.Rows[0][j].ToString() == "")
                        //                            {
                        //                                TotalCurrentValue = 1;
                        //                            }
                        //                        }

                        //                        if (TotalCurrentValue == 0)
                        //                        {
                        //                            if (System.Windows.MessageBox.Show("Do you want to Download in PLC?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        //                            {
                        //                                DownloadDataToPLC(LoadNumber);
                        //                            }
                        //                            else
                        //                            {
                        //                                return;
                        //                            }
                        //                        }
                        //                        else
                        //                        {
                        //                            System.Windows.MessageBox.Show("Not able to download in PLC! Total Current value is Out of Limits !");
                        //                        }


                        //                    }
                        //                    else
                        //                    {
                        //                        LoginView loginView = new LoginView();
                        //                        loginView.ShowDialog();
                        //                    }
                        //                }
                        //                else
                        //                {
                        //                    System.Windows.MessageBox.Show("Please Stop Cycle before doing Next Load entry Updation.");
                        //                }
                        //            }
                        //        }
                        //    }
                        //    else if (messageBoxResult == MessageBoxResult.No)
                        //    {

                        //    }
                        //}
                        //else
                        //{
                        //    if (stationNo <= length && stationNo > 0)
                        //    {
                        //        string[] CycleStartStop = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Cycleinprocess");
                        //        if (CycleStartStop.Length != 0)
                        //        {
                        //            if (CycleStartStop[0] == "0" || CycleStartStop[0] == "False")
                        //            {
                        //                int TotalCurrentValue = 0;
                        //                // Add user role validation in this
                        //                string username = IndiSCADAGlobalLibrary.UserLoginDetails.UserName; // commented for testing purpose
                        //                string useraccesslevel = IndiSCADAGlobalLibrary.UserLoginDetails.AccessLevel;

                        //                if (username != null)
                        //                {
                        //                    for (int j = 0; j < dtTotalCurrentValues.Columns.Count; j++)
                        //                    {
                        //                        string q = dtTotalCurrentValues.Rows[0][j].ToString();
                        //                        if (dtTotalCurrentValues.Rows[0][j].ToString() == "")
                        //                        {
                        //                            TotalCurrentValue = 1;
                        //                        }
                        //                    }

                        //                    if (TotalCurrentValue == 0)
                        //                    {
                        //                        if (System.Windows.MessageBox.Show("Do you want to Download in PLC?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        //                        {
                        //                            DownloadDataToPLC(LoadNumber);
                        //                        }
                        //                        else
                        //                        {
                        //                            return;
                        //                        }
                        //                    }
                        //                    else
                        //                    {
                        //                        System.Windows.MessageBox.Show("Not able to download in PLC! Total Current value is Out of Limits !");
                        //                    }


                        //                }
                        //                else
                        //                {
                        //                    LoginView loginView = new LoginView();
                        //                    loginView.ShowDialog();
                        //                }
                        //            }
                        //            else
                        //            {
                        //                System.Windows.MessageBox.Show("Please Stop Cycle before doing Next Load Updation");
                        //            }
                        //        }
                        //    }
                        //}                       



                        //}
                        //else
                        //{
                        //    System.Windows.MessageBox.Show("There is no load present in this station !");
                        //}
                        #endregion
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Please enter Station No First");
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NextLoadUpdate BtnDownload_Click()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }

        public void DownloadPL(int stationNo,int load_no)
        {
            try
            {
                int length = 61;
                //string GetLoadNumber = "select LoadNumber from LoadDipTime where StationNumber=" + stationNo + " and Status=0 order by desc";
                ServiceResponse<DataTable> GetLoadNumber1 = IndiSCADABusinessLogic.NextloadEntryLogic.GetLoadNumber(stationNo);
                DataTable Dt = GetLoadNumber1.Response;
                string LoadNumber = Dt.Rows[0]["LoadNumber"].ToString();
                string[] arr = LoadNumber.Split('-');

                ErrorLogger.LogError.ErrorLog("NextLoadUpdate DownloadPL() LoadNumber" + LoadNumber, DateTime.Now.ToString(), "", "No", true);

                if (Convert.ToInt32(arr[2]) != load_no)
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Load Number are Mismatch. Do You Want To OverWrite?", "NextLoadUpdate", MessageBoxButton.YesNoCancel);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        if (stationNo <= length && stationNo > 0)
                        {
                            string[] CycleStartStop = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Cycleinprocess");// SystemInputsInput");{ "0"}
                            ErrorLogger.LogError.ErrorLog("NextLoadUpdate DownloadPL() CycleStartStop" + CycleStartStop, DateTime.Now.ToString(), "", "No", true);


                            if (CycleStartStop.Length != 0)
                            {
                                if (CycleStartStop[0] == "0" || CycleStartStop[0] == "False")
                                {
                                    int TotalCurrentValue = 0;
                                    // Add user role validation in this
                                    string username = IndiSCADAGlobalLibrary.UserLoginDetails.UserName; // commented for testing purpose
                                    string useraccesslevel = IndiSCADAGlobalLibrary.UserLoginDetails.AccessLevel;

                                    if (username != null)
                                    {
                                        for (int j = 0; j < dtTotalCurrentValues.Columns.Count; j++)
                                        {
                                            string q = dtTotalCurrentValues.Rows[0][j].ToString();
                                            if (dtTotalCurrentValues.Rows[0][j].ToString() == "")
                                            {
                                                TotalCurrentValue = 1;
                                            }
                                        }

                                        if (TotalCurrentValue == 0)
                                        {
                                            if (System.Windows.MessageBox.Show("Do you want to Download in PLC?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                                            {
                                                DownloadDataToPLC(LoadNumber);
                                            }
                                            else
                                            {
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            System.Windows.MessageBox.Show("Not able to download in PLC! Total Current value is Out of Limits !");
                                        }


                                    }
                                    else
                                    {
                                        LoginView loginView = new LoginView();
                                        loginView.ShowDialog();
                                    }
                                }
                                else
                                {
                                    System.Windows.MessageBox.Show("Please Stop Cycle before doing Next Load entry Updation.");
                                }
                            }
                        }
                    }
                    else if (messageBoxResult == MessageBoxResult.No)
                    {

                    }
                }
                else
                {
                    if (stationNo <= length && stationNo > 0)
                    {
                        string[] CycleStartStop = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue("Cycleinprocess");
                        ErrorLogger.LogError.ErrorLog("NextLoadUpdate DownloadPL() CycleStartStop" + CycleStartStop, DateTime.Now.ToString(), "", "No", true);

                        if (CycleStartStop.Length != 0)
                        {
                            if (CycleStartStop[0] == "0" || CycleStartStop[0] == "False")
                            {
                                int TotalCurrentValue = 0;
                                // Add user role validation in this
                                string username = IndiSCADAGlobalLibrary.UserLoginDetails.UserName; // commented for testing purpose
                                string useraccesslevel = IndiSCADAGlobalLibrary.UserLoginDetails.AccessLevel;

                                if (username != null)
                                {
                                    for (int j = 0; j < dtTotalCurrentValues.Columns.Count; j++)
                                    {
                                        string q = dtTotalCurrentValues.Rows[0][j].ToString();
                                        if (dtTotalCurrentValues.Rows[0][j].ToString() == "")
                                        {
                                            TotalCurrentValue = 1;
                                        }
                                    }

                                    if (TotalCurrentValue == 0)
                                    {
                                        if (System.Windows.MessageBox.Show("Do you want to Download in PLC?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                                        {
                                            DownloadDataToPLC(LoadNumber);
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        System.Windows.MessageBox.Show("Not able to download in PLC! Total Current value is Out of Limits !");
                                    }


                                }
                                else
                                {
                                    LoginView loginView = new LoginView();
                                    loginView.ShowDialog();
                                }
                            }
                            else
                            {
                                System.Windows.MessageBox.Show("Please Stop Cycle before doing Next Load Updation");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NextLoadUpdate DownloadPL()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }

        private void TxtStationNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtStationNo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CalculateCurrent();

            }
            catch (Exception ex) { }

        }
        public void overWrite()
        {

        }
        #endregion

    }
}
