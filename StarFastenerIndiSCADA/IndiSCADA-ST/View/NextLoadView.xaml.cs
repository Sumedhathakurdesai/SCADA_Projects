
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
using System.Windows.Threading; 

namespace IndiSCADA_ST.View
{
    /// <summary>
    /// Interaction logic for NextLoadView.xaml
    /// </summary>
    public partial class NextLoadView : ChromelessWindow, INotifyPropertyChanged
    {
        #region General Declarations
        DataTable DTPartMaster = null;
        DataTable DTORDipTime = null;
        string[] addrList = null;
        string[] ColumnNames = null;
        string Quantity = "0", WeightPerPart = "0.0", TotalWeight = "0"; int programNo = 0;

        #endregion

        #region Properties
        private string _ActualWeight;
        public string ActualWeight
        {
            get { return _ActualWeight; }
            set { _ActualWeight = value; OnPropertyChanged("ActualWeight"); }
        }
        private string _SetWeight;
        public string SetWeight
        {
            get { return _SetWeight; }
            set { _SetWeight = value; OnPropertyChanged("SetWeight"); }
        }
        private bool _isOpenSetWeightPopup;
        public bool isOpenSetWeightPopup
        {
            get { return _isOpenSetWeightPopup; }
            set { _isOpenSetWeightPopup = value; OnPropertyChanged("isOpenSetWeightPopup"); }
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

        private ObservableCollection<string> _PartDescriptionList;
        public ObservableCollection<string> PartDescriptionList
        {
            get { return _PartDescriptionList; }
            set { _PartDescriptionList = value; OnPropertyChanged("PartDescriptionList"); }
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

        public NextLoadView()
        {
            InitializeComponent();
            try
            {
                DataContext = this;
                FillAllGrid();
                this.dgEditPart.AutoGeneratingColumn += detailsViewGrid_AutoGeneratingColumn;
                isOpenSetWeightPopup = false;
            }
            catch (Exception ex) { }
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
                        }
                    }
                }
                catch(Exception ex) { }
            }
            catch { }

        }
        private void CalculateCurrent()
        {
           
            try
            {
                ServiceResponse<DataTable> ResultPartMasterSetting = IndiSCADABusinessLogic.NextloadEntryLogic.getNextloadSettingDetails("All");
                DataTable dt = ResultPartMasterSetting.Response;
                ServiceResponse<DataTable> Resultdownload = IndiSCADABusinessLogic.NextloadEntryLogic.getNextloadSettingDetails("DownloadToPLC");
                DataTable dtDownload = Resultdownload.Response;
                addrList = new string[dtDownload.Rows.Count];
                ColumnNames = new string[dtDownload.Rows.Count];
                int index = 0;
                // Read the addresses and columns for calculation
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
                    try
                    {
                        //Take Actual total Wt from load cell  
                        if (chkAllowTotalWtFromLoadCell.IsChecked == true)
                        {
                            //read total weight from plc and calculate currents ....fill total weight in grid automatically.  
                            string[] PLCtotalwt = DeviceCommunication.CommunicationWithPLC.Real_ReadPLCTagValue("NextLoadActualTotalWeight");

                            TotalWeight = PLCtotalwt[0].ToString();

                            dtEditPart.Rows[6][1] = TotalWeight;
                            dtSelectedPart.Rows[i]["Total Weight Per Barrel"] = TotalWeight;

                            //calculate quantity
                            if (Convert.ToInt64(TotalWeight) <= 100 && Convert.ToInt64(TotalWeight) >= 0)
                            {
                                Double wtperpart = 0;

                                wtperpart = Convert.ToDouble(dtSelectedPart.Rows[i]["Weight Per Part"].ToString());
                                dtEditPart.Rows[7][1] = Convert.ToInt32((Convert.ToDouble(TotalWeight) * 1000) / wtperpart);
                                dtSelectedPart.Rows[i]["Quantity"] = Convert.ToInt32((Convert.ToDouble(TotalWeight) * 1000) / wtperpart);

                            }
                            else
                            {
                                System.Windows.MessageBox.Show("Total Weight Per Barrel Must be in range 0 to 100 kg");
                            } 
                        }
                        else
                        {
                            TotalWeight = dtSelectedPart.Rows[i]["Total Weight Per Barrel"].ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError.ErrorLog("Nextload CalculateCurrent() NextLoadACtualTotalWeight", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        TotalWeight = dtSelectedPart.Rows[i]["Total Weight Per Barrel"].ToString();
                    }


                    WeightPerPart = dtSelectedPart.Rows[i]["Weight Per Part"].ToString();
                    index = 0; string partName = "";
                    if ((TotalWeight != "") && (WeightPerPart != ""))
                    {
                        int a = 0; int b= 0; int c = 0;
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
                                            calc += "("+dtSelectedPart.Rows[i][colname].ToString();
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

                                        //----------added by sbs for column name and parameter name seperate
                                        //if (s.StartsWith("["))
                                        //{
                                        //    string colname = s.Substring(1, s.Length - 2);

                                        //    try
                                        //    {
                                        //        DataRow[] results = DTPartMaster.Select(" ParameterName='" + colname + "'");
                                        //        partName = results[0]["ColumnName"].ToString();
                                        //    }
                                        //    catch (Exception ex) { }

                                        //    calc += dtSelectedPart.Rows[i][partName].ToString();
                                        //}
                                        //else
                                        //{
                                        //    calc += s;
                                        //}
                                        //--------------
                                    }

                                }
                                DataTable calDT = new DataTable();
                                Object value = calDT.Compute(calc, "");

                                //----------added by sbs for column name and parameter name seperate
                                //try
                                //{
                                //    DataRow[] results = DTPartMaster.Select(" ColumnName='" + ColumnNames[index].ToString() + "'");
                                //    partName = results[0]["ParameterName"].ToString();
                                //}
                                //catch (Exception ex) { }

                                //dtSelectedPart.Rows[i][partName] = value.ToString();
                                //index++;
                                //--------------
                                
                                value = Math.Round(Convert.ToDouble(value), 2);
                                if (value.ToString() != "NaN")
                                {
                                    dtSelectedPart.Rows[i][ColumnNames[index]] = value.ToString();
                                }
                                else
                                {
                                    dtSelectedPart.Rows[i][ColumnNames[index]] = 0;  // division by zero gives NaN as value.
                                }

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
                        if (Convert.ToDouble(dtTotalCurrentValues.Rows[0][records]) > 1500 && colname == "Anodic 1 Current")
                        {
                            dtTotalCurrentValues.Rows[0][records] = "";
                            Current1ExtendLimit = " 'Anodic 1' ";
                        }
                        else if (Convert.ToDouble(dtTotalCurrentValues.Rows[0][records]) > 1500 && colname == "Anodic 2 Current")
                        {
                            dtTotalCurrentValues.Rows[0][records] = "";
                            Current2ExtendLimit = " 'Anodic 2' ";
                        }
                        else if (Convert.ToDouble(dtTotalCurrentValues.Rows[0][records]) > 1500 && colname == "Alkaline Zinc Current")
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
           
        private void DownloadDataToPLC()
        {
            try
            {
                if (dtSelectedPart.Rows.Count > 0)
                {
                    try
                    {
                        CalculateCurrent();//Calculate Current

                         //update Partmaster as part selected
                        try
                        {
                            string query="";
                            try
                            {
                               query = "Update PartMaster set isSelectedforNextLoad=0 where isSelectedforNextLoad = 1";
                                ServiceResponse<int> _UpdateResult = (NextloadEntryLogic.ResetPartforNextLoad(query));
                            }
                            catch (Exception ex) { ErrorLogger.LogError.ErrorLog("Error nextload "+ query, DateTime.Now.ToString(), ex.Message, "No", true); }
                            foreach (DataRow dr in dtSelectedPart.Rows)
                            {
                                ServiceResponse<int> result = NextloadEntryLogic.UpdateSelectedPartforNextLoad(dr);
                            }
                        }
                        catch (Exception ex)
                        { }
                    }
                    catch (Exception ex) { }


                    //here we writting program no
                    try
                    {
                        string programNum=programNo.ToString();
                        foreach (DataRow dr in dtSelectedPart.Rows)
                        {
                            programNum = dr["Passivation Selection"].ToString();
                        }
                        IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("ProgramNOselection", 0, programNum.ToString());
                    }
                    catch (Exception exExitButtonCommandClick)
                    {
                        ErrorLogger.LogError.ErrorLog("OverviewViewModel UpdateDryerAlarmTimeClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
                    }


                    //here we writting TOTAL areA,total wt,cd, for only 1 part
                    try
                    {
                        double totalArea = 0;
                        int totalAreaint = 0;

                        string AreaPerPart = "";
                        try
                        {
                            foreach (DataRow dr in dtSelectedPart.Rows)
                            {
                                //            string Anodic1CD = dr["Anodic 1 CD"].ToString();
                                //            IndiSCADABusinessLogic.SettingLogic.WriteValueToPLCReal("Anodic1CD", 0, Anodic1CD.ToString());
                                //            ErrorLogger.LogError.ErrorLog("NextLoadView Anodic1CD="+ Anodic1CD, DateTime.Now.ToString(), "", "No", true);

                                //            string Anodic2CD = dr["Anodic 2 CD"].ToString();
                                //            IndiSCADABusinessLogic.SettingLogic.WriteValueToPLCReal("Anodic2CD", 0, Anodic2CD.ToString());
                                //            ErrorLogger.LogError.ErrorLog("NextLoadView Anodic2CD=" + Anodic2CD, DateTime.Now.ToString(), "", "No", true);

                                //            string AlkalineZincCD = dr["Alkaline Zinc CD"].ToString();
                                //            IndiSCADABusinessLogic.SettingLogic.WriteValueToPLCReal("AlkalineZincCD", 0, AlkalineZincCD.ToString());
                                //            ErrorLogger.LogError.ErrorLog("NextLoadView AlkalineZincCD=" + AlkalineZincCD, DateTime.Now.ToString(), "", "No", true);

                                //            string ZincNickelCD = dr["Zinc Nickel CD"].ToString();
                                //            IndiSCADABusinessLogic.SettingLogic.WriteValueToPLCReal("ZincNickelCD", 0, ZincNickelCD.ToString());
                                //            ErrorLogger.LogError.ErrorLog("NextLoadView ZincNickelCD=" + ZincNickelCD, DateTime.Now.ToString(), "", "No", true);



                                 
                                //string setWeight = dr["Total Weight Per Barrel"].ToString();
                                //IndiSCADABusinessLogic.SettingLogic.WriteValueToPLCReal("WeightPerPart", 0, setWeight.ToString());
                                //ErrorLogger.LogError.ErrorLog("NextLoadView set Total Weight=" + setWeight, DateTime.Now.ToString(), "", "No", true);




                                //string WeightPerPart = dr["Weight Per Part"].ToString();
                                //IndiSCADABusinessLogic.SettingLogic.WriteValueToPLCReal("WeightPerPart", 0, WeightPerPart.ToString());
                                //ErrorLogger.LogError.ErrorLog("NextLoadView WeightPerPart=" + WeightPerPart, DateTime.Now.ToString(), "", "No", true);

                                //string TotalWeight = dr["Total Weight Per Barrel"].ToString();
                                //IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("TotalWeight", 0, TotalWeight.ToString());
                                //ErrorLogger.LogError.ErrorLog("NextLoadView TotalWeight=" + TotalWeight, DateTime.Now.ToString(), "", "No", true);


                                //            string AreaPrPart = dr["Surface Area Per Part"].ToString();
                                //            IndiSCADABusinessLogic.AlarmTimeLogic.WriteValueToPLC("TotalArea", 0, AreaPrPart.ToString());
                                //            ErrorLogger.LogError.ErrorLog("NextLoadView AreaPrPart=" + AreaPrPart, DateTime.Now.ToString(), "", "No", true);

                                //            //string quanity = dr["Quantity"].ToString();

                                //            //string AreaPrPart = dr["Surface Area Per Part"].ToString();

                                //            //totalArea = (Convert.ToInt16(quanity) * Convert.ToDouble(AreaPrPart)) / 10000;

                                //            //totalAreaint = Convert.ToInt32(totalArea);

                                //            //IndiSCADABusinessLogic.SettingLogic.WriteValueToPLCReal("TotalArea", 0, totalAreaint.ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError.ErrorLog("NextLoadView while writting TotalArea and CD" + totalAreaint.ToString(), DateTime.Now.ToString(), ex.Message, "No", true);
                        }
                    }
                    catch (Exception exExitButtonCommandClick)
                    {
                        ErrorLogger.LogError.ErrorLog("NextLoadView while writting TotalArea", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
                    }

                    //here we are writting total current
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
                      //  ErrorLogger.LogError.ErrorLog("Nextload WriteValues() Step 1:  ", DateTime.Now.ToString(), addrList.Length + " : " + ColumnNames.Length, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                        if (addrList.Length > 0 && ColumnNames.Length > 0)
                        {
                            short[] arrDeviceValue = new short[addrList.Length];            //Data for 'DeviceValue'
                            double[] CurrentValues = new double[addrList.Length];
                            System.String[] arrData = new string[addrList.Length];
                            //Get address from xml
                            for (int index = 0; index < addrList.Length; index++)
                            {
                                if (addrList[index] != null)
                                {
                                    string address = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName(addrList[index], 0);
                                    arrData[index] = address;
                                }
                            }

                            //Summation of the current for all selected parts
                            for (int index = 0; index < ColumnNames.Length; index++)
                            {
                                //--------------added by sbs to seperately display parameter and column name
                                //string partName = "";
                                //try
                                //{
                                //    DataRow[] results = DTPartMaster.Select(" ColumnName='" + ColumnNames[index].ToString() + "'");
                                //    partName = results[0]["ParameterName"].ToString();
                                //}
                                //catch (Exception ex) { }


                                //if (ColumnNames[index] != null)
                                //{
                                //    for (int rowno = 0; rowno < dtSelectedPart.Rows.Count; rowno++)
                                //    {
                                //        string colname = partName;//ColumnNames[index].ToString();
                                //        CurrentValues[index] += Convert.ToDouble(dtSelectedPart.Rows[rowno][colname].ToString());
                                //    }
                                //}
                                //------------------------------------------


                                if (ColumnNames[index] != null)
                                {
                                    for (int rowno = 0; rowno < dtSelectedPart.Rows.Count; rowno++)
                                    {
                                        string colname = ColumnNames[index].ToString();
                                        CurrentValues[index] += Convert.ToDouble(dtSelectedPart.Rows[rowno][colname].ToString());
                                    }
                                }
                            }
                            // assign selected values to write in plc
                            for (int index = 0; index < ColumnNames.Length; index++)
                            {
                                if (ColumnNames[index] != null)
                                {
                                    arrDeviceValue[index] = (Convert.ToInt16(CurrentValues[index]));
                                }
                            }
                            // int result = .WriteValueINPLC(arrDeviceValue, arrData, addrList.Length);
                            ErrorLogger.LogError.ErrorLog("Nextload WriteValues() 1 ", DateTime.Now.ToString(), CurrentValues.Length + " : " + arrDeviceValue.Length + " : " + arrData.Length, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                             
                                DeviceCommunication.CommunicationWithPLC.WriteValuesArray(arrData, arrDeviceValue, addrList.Length);

                            System.Threading.Thread.Sleep(1000);

                            WritePartEntryBit("NextLoadEnter", "1");

                            lblDownloadStatus.Content = "Download Status : SUCCESS ";

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
                }
                else
                {

                }
         
            }
            catch (Exception ex) { }
                     
        }

        public static void WritePartEntryBit(string TaskName, string value)
        {
            try
            {
                //Get AutoManual from PLC
              //  string[] ValueToRead = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue(TaskName);
                string TagName = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName(TaskName, 0);
                ErrorLogger.LogError.ErrorLog(" WritePartEntryBit(TagName)", DateTime.Now.ToString(), TagName, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
              //  ErrorLogger.LogError.ErrorLog("SettingLogic ONOFFButton(Value)", DateTime.Now.ToString(), ValueToRead[index].ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                if (TagName != null)
                {
                    if (TagName.Length > 0)
                    {                     
                            int LT1 = 1;                                              
                            DeviceCommunication.CommunicationWithPLC.WriteBoolValues(TagName, LT1.ToString());
                        
                    }                   
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("WritePartEntryBit ()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }

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
                string wtPerpart=""; string TotalWt = ""; Int32 CalQuanitity=0; double Current1 =0; double current2 = 0; double current3 = 0;

                DataTable dtpartdetails = new DataTable();int colIndex=-1; int isSelectedForNextLoadCol=0;
                ServiceResponse<DataTable> ResultPartMaster = IndiSCADABusinessLogic.NextloadEntryLogic.getPartDetails(cmbPartlist.SelectedItem.ToString(), "SelectPart","");
                if (ResultPartMaster.Status == ResponseType.S)
                {
                    dtpartdetails = ResultPartMaster.Response;
                    for (int i = 0; i < dtpartdetails.Columns.Count; i++) //added by sbs to avoid paste for is selected from nextload
                    {
                        string temp2 = dtpartdetails.Columns[i].ColumnName.ToString();
                        if (temp2 == "isSelectedForNextLoad")
                        {
                            colIndex = i;
                        }
                    }
                    try
                    {
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

                            if (dtEditPart.Rows[indexRow][0].ToString() == "Weight Per Part")
                            {
                                wtPerpart = dtEditPart.Rows[indexRow][1].ToString();
                            }
                            else if (dtEditPart.Rows[indexRow][0].ToString() == "Total Weight Per Barrel")
                            {
                                TotalWt = dtEditPart.Rows[indexRow][1].ToString();
                            }
                            else if (dtEditPart.Rows[indexRow][0].ToString() == "Quantity" && TotalWt != "" && wtPerpart != "")
                            {
                                try
                                {
                                    double Quntity = Convert.ToInt32((Convert.ToDouble(TotalWt) * 1000) / Convert.ToDouble(wtPerpart));
                                    dtEditPart.Rows[indexRow][1] = Quntity.ToString();
                                }
                                catch (Exception ex)
                                {
                                    double Quntity = 0;
                                    dtEditPart.Rows[indexRow][1] = Quntity.ToString();
                                }
                            }



                            //commented by sbs in star
                            //if(dtEditPart.Rows[indexRow][0].ToString()== "Passivation Selection" || dtEditPart.Rows[indexRow][0].ToString() == "Total Weight Per Barrel" || dtEditPart.Rows[indexRow][0].ToString() == "Quantity")//|| dtEditPart.Rows[indexRow][0].ToString() == "DF Number" 
                            //{
                            //    dtEditPart.Rows[indexRow][1] = "";
                            //}
                        }

                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch(Exception EX)
            { 
            }
        }

        //validation
        private void DgEditPart_CurrentCellValidating(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellValidatingEventArgs e)
        {
            try
            {
                string newvalue = e.NewValue.ToString();

                DataRowView dr = (DataRowView)e.RowData;
                
                if (dr[3].ToString() == "int" || dr[3].ToString() == "number")
                {
                    if (!newvalue.All(char.IsDigit))
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "Only numbers are allowed";
                    }
                }
                if (dr[3].ToString() == "float")
                {
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
                //-----------------------------------
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
            
            catch(Exception ex) { }
        }

        private void BtnAddPart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                programNo =0;
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
                        //allow single part for nextload
                        //int SelectedParts = dtSelectedPart.Rows.Count;
                        //if (SelectedParts == 0)
                        //{
                            string[] newrow = new string[dtSelectedPart.Columns.Count];
                            DataTable dataTable = dtSelectedPart;
                            DataGridViewRow _DTRowAdd = new DataGridViewRow();
                            for (int index = 0; index <= DTPartMaster.Rows.Count - 1; index++)
                            {
                                //Take Actual total Wt from load cell is false i.e. take manual wt
                                if (chkAllowTotalWtFromLoadCell.IsChecked==false)
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
                                //if (dtEditPart.Rows[index][0].ToString() != "Descaling Current" && dtEditPart.Rows[index][0].ToString() != "Anodic Current" && dtEditPart.Rows[index][0].ToString() != "Alkaline Zinc Current" && dtEditPart.Rows[index][1].ToString() == "")
                                //{
                                //    System.Windows.MessageBox.Show("Please fill value for " + dtEditPart.Rows[index][0].ToString());
                                //    return;
                                //}
                               

                                string name = DTPartMaster.Rows[index][1].ToString();
                                newrow[index] = dtEditPart.Rows[index][1].ToString();
                                                        
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

        private void DgEditPart_CurrentCellBeginEdit(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellBeginEditEventArgs e)
        {
            var index = e.RowColumnIndex;
            try
            {
                //allow pasivation selection and total weight and cd's to edit 
                if ((index.RowIndex == 1) || (index.RowIndex == 2) || (index.RowIndex == 3) || (index.RowIndex == 4) || (index.RowIndex == 5) || (index.RowIndex == 6) || (index.RowIndex == 8) || (index.RowIndex == 9) || (index.RowIndex == 11) || (index.RowIndex == 12) || (index.RowIndex == 14) || (index.RowIndex == 15)||(index.RowIndex == 17) || (index.RowIndex == 18)||(index.RowIndex == 20))
                {
                    e.Cancel = true;
                }


                if ((index.RowIndex == 8) || (index.RowIndex == 9)|| (index.RowIndex == 12)|| (index.RowIndex == 15) ) //allow pasivation selection and total weight to edit
                {

                    IndiSCADAGlobalLibrary.UserLoginDetails.UserName = null;
                    IndiSCADAGlobalLibrary.UserLoginDetails.AccessLevel = null;
                    if (IndiSCADAGlobalLibrary.UserLoginDetails.UserName == null)
                    {
                        LoginView loginView = new LoginView();
                        loginView.ShowDialog();

                    }
                    if ((IndiSCADAGlobalLibrary.UserLoginDetails.UserName != null))
                    {
                        e.Cancel = false;


                    }

                      


                   
                }
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
            try
            {
                int TotalCurrentValue=0;
                // Add user role validation in this
                string username = IndiSCADAGlobalLibrary.UserLoginDetails.UserName; // commented for testing purpose
                string useraccesslevel = IndiSCADAGlobalLibrary.UserLoginDetails.AccessLevel;
                
                if (username != null)
                {
                    for (int j = 0; j < dtTotalCurrentValues.Columns.Count; j++)
                    {
                        string q = dtTotalCurrentValues.Rows[0][j].ToString();
                        if (dtTotalCurrentValues.Rows[0][j].ToString() =="")
                        {
                            TotalCurrentValue = 1;
                        }
                    }

                    if (TotalCurrentValue == 0)
                    {
                        if (System.Windows.MessageBox.Show("Do you want to Download in PLC?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            try
                            {
                                #region write out of range dip time value
                                string programNo ="";
                                for (int i=0;i<dtEditPart.Rows.Count;i++)
                                {
                                    if (dtEditPart.Rows[i][0].ToString() == "Passivation Selection")
                                    {
                                         programNo = dtEditPart.Rows[i][1].ToString();
                                    }
                                }
                              
                                ServiceResponse<DataTable> ResultORDipSetting = IndiSCADABusinessLogic.NextloadEntryLogic.getORDipTimeDetails(programNo);
                                if (ResultORDipSetting.Response != null)
                                {
                                    DTORDipTime = ResultORDipSetting.Response;

                                    for (int indexRow = 0; indexRow <= DTORDipTime.Rows.Count - 1; indexRow++)
                                    {
                                        //if(DTORDipTime.Rows[0]["StationName"].ToString()!="")
                                        //{

                                        //}
                                        if (DTORDipTime.Rows[indexRow]["DipTimeToleranceHigh"].ToString() != "")
                                        {
                                            try
                                            {
                                                WriteValueToPLC("ORDiptimeEnterHigh", indexRow, DTORDipTime.Rows[indexRow]["DipTimeToleranceHigh"].ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorLogger.LogError.ErrorLog("DipTimeToleranceHigh WriteValueToPLC()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                                            }
                                        }
                                        if (DTORDipTime.Rows[indexRow]["DipTimeToleranceLow"].ToString() != "")
                                        {
                                            try
                                            {
                                                WriteValueToPLC("ORDiptimeEnterLow", indexRow, DTORDipTime.Rows[indexRow]["DipTimeToleranceLow"].ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorLogger.LogError.ErrorLog("DipTimeToleranceLow WriteValueToPLC()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                                            }
                                        }
                                        if (DTORDipTime.Rows[indexRow]["DipLowBypass"].ToString() != "")
                                        {
                                            try
                                            {
                                                WriteValueToPLC("ORDiptimeLowBypass", indexRow, DTORDipTime.Rows[indexRow]["DipLowBypass"].ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorLogger.LogError.ErrorLog("DipLowBypass WriteValueToPLC()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                                            }
                                        }
                                        if (DTORDipTime.Rows[indexRow]["DipHighBypass"].ToString() != "")
                                        {
                                            try
                                            {
                                                WriteValueToPLC("ORDiptimeHighBypass", indexRow, DTORDipTime.Rows[indexRow]["DipHighBypass"].ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorLogger.LogError.ErrorLog("DipHighBypass WriteValueToPLC()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    ErrorLogger.LogError.ErrorLog("Nextload DTORDipTime is return null", DateTime.Now.ToString(), "", null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);

                                }
                                #endregion
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError.ErrorLog("Nextload OR writting by programNO", DateTime.Now.ToString(), ex.ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                            }

                            //dtSelectedPart.Columns.Add(new DataColumn(DTPartMaster.Rows[indexRow]["ParameterName"].ToString(), typeof(string)));
                            //dtEditPart.Rows.Add(DTPartMaster.Rows[indexRow]["ParameterName"].ToString(), "", DTPartMaster.Rows[indexRow]["Unit"].ToString(), DTPartMaster.Rows[indexRow]["DataType"].ToString());
                            

                            DownloadDataToPLC();
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
                    // lblMessage.Content = "";

                }
                else
                {
                    LoginView loginView = new LoginView();
                    loginView.ShowDialog();
                }

            }
            catch (Exception ex) { }
        }

        private void CmbDescriptionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //DataTable dtpartdescription = new DataTable();
                //ServiceResponse<DataTable> ResultPartMaster = IndiSCADABusinessLogic.NextloadEntryLogic.getPartDetails("", "SelectDescription",cmbDescriptionList.SelectedItem.ToString());
                //if (ResultPartMaster.Status == ResponseType.S)
                //{
                //    dtpartdescription = ResultPartMaster.Response;

                //    dtpartdescription = ResultPartMaster.Response;
                //    if (dtpartdescription != null)
                //    {
                //        if (dtpartdescription.Rows.Count > 0)
                //        {
                //            PartList = new ObservableCollection<string>();
                //            for (int indexRow = 0; indexRow < dtpartdescription.Rows.Count; indexRow++)
                //            {
                //                PartList.Add(dtpartdescription.Rows[indexRow][0].ToString());
                //            }
                //        }
                //    }

                //    //for (int indexRow = 0; indexRow <= dtpartdescription.Columns.Count - 1; indexRow++)
                //    //{
                //    //    dtEditPart.Rows[indexRow][1] = dtpartdescription.Rows[0][indexRow].ToString();
                //    //    if (dtEditPart.Rows[indexRow][0].ToString() == "Passivation Selection" || dtEditPart.Rows[indexRow][0].ToString() == "DF Number" || dtEditPart.Rows[indexRow][0].ToString() == "Total Weight" || dtEditPart.Rows[indexRow][0].ToString() == "Quantity")
                //    //    {
                //    //        dtEditPart.Rows[indexRow][1] = "";
                //    //    }
                //    //}
                //}
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("Nextload CmbDescriptionList_SelectionChanged()", DateTime.Now.ToString(), ex.ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }

        private void CmbDescriptionList_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
           try
            {
            //    CollectionView itemsViewOriginal = (CollectionView)CollectionViewSource.GetDefaultView(cmbDescriptionList.ItemsSource);

            //    itemsViewOriginal.Filter = ((o) =>
            //    {
            //        if (String.IsNullOrEmpty(cmbDescriptionList.Text)) return true;
            //        else
            //        {
            //            if (((string)o).Contains(cmbDescriptionList.Text)) return true;
            //            else return false;
            //        }
            //    });

            //    itemsViewOriginal.Refresh();

                // if datasource is a DataView, then apply RowFilter as below and replace above logic with below one
                /* 
                 DataView view = (DataView) Cmb.ItemsSource; 
                 view.RowFilter = ("Name like '*" + Cmb.Text + "*'"); 
                */
            }
            catch { }
        }

        private void CmbPartlist_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                CollectionView itemsViewOriginal = (CollectionView)CollectionViewSource.GetDefaultView(cmbPartlist.ItemsSource);

                itemsViewOriginal.Filter = ((o) =>
                {
                    if (String.IsNullOrEmpty(cmbPartlist.Text)) return true;
                    else
                    {
                        if (((string)o).Contains(cmbPartlist.Text)) return true;
                        else return false;
                    }
                });

                itemsViewOriginal.Refresh();

                // if datasource is a DataView, then apply RowFilter as below and replace above logic with below one
                /* 
                 DataView view = (DataView) Cmb.ItemsSource; 
                 view.RowFilter = ("Name like '*" + Cmb.Text + "*'"); 
                */
            }
            catch { }
        }



        public static void WriteValueToPLC(string TaskName, int index, string value)
        {
            try
            {
                string TagName = IndiSCADAGlobalLibrary.XmlFileReadTagName.ReadTagName(TaskName, index);
                DeviceCommunication.CommunicationWithPLC.WriteValues(TagName, value);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("nextLoadView WriteValueToPLC()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }


        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CalculateCurrent();

            }
            catch (Exception ex) { }

        }



        private void BtnSetWeight_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                isOpenSetWeightPopup = true;
                SetWeightStatus.Content = "";

                string[] PLCtotalwt = DeviceCommunication.CommunicationWithPLC.Real_ReadPLCTagValue("NextLoadActualTotalWeight"); 
                ActualWeight = PLCtotalwt[0].ToString();
            }
            catch (Exception ex) { }
        }
        private void SetWeightPopupExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                isOpenSetWeightPopup = false;
            }
            catch (Exception ex) { }
        }
        private void Write_SetWeight_InPLC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                float setWeight = float.Parse(SetWeight);
                if (setWeight >= 40 && setWeight <= 100)
                {
                    IndiSCADABusinessLogic.SettingLogic.WriteValueToPLCReal("NextLoadSetTotalWeight", 0, SetWeight);
                    SetWeightStatus.Content = "Weight Set successfully"; 
                }
                else
                {
                    SetWeightStatus.Content = "Enter Set weight in range 40-100";
                }
            }
            catch (Exception ex) { SetWeightStatus.Content = "Fail to Set Weight in plc"; }
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[^0-9]+");
                e.Handled = regex.IsMatch(e.Text);
            }
            catch (Exception ex) { }
        }
        #endregion
    }
}
