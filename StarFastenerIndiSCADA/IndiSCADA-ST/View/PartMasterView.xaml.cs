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
using System.Text.RegularExpressions;

namespace IndiSCADA_ST.View
{
    /// <summary>
    /// Interaction logic for PartMasterView.xaml
    /// </summary>
    public partial class PartMasterView : ChromelessWindow, INotifyPropertyChanged
    {
        #region "Declaration"            
        DataTable DTPartMaster = null;
        #endregion
        #region properties
        private ObservableCollection<string> _PartDescriptionList;
        public ObservableCollection<string> PartDescriptionList
        {
            get { return _PartDescriptionList; }
            set { _PartDescriptionList = value; OnPropertyChanged("PartDescriptionList"); }
        }

        private DataTable _dtNewPart = new DataTable();
        public DataTable dtNewPart
        {
            get { return _dtNewPart; }
            set { _dtNewPart = value; OnPropertyChanged("dtNewPart"); }
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

        private DataTable _dtHangerTypes = new DataTable();
        public DataTable dtHangerTypes
        {
            get { return _dtHangerTypes; }
            set { _dtHangerTypes = value; OnPropertyChanged("dtHangerTypes"); }
        }

        private string _SelectedPart;
        public string SelectedPart
        {
            get { return _SelectedPart; }
            set { _SelectedPart = value; OnPropertyChanged("SelectedPart"); }
        }
        #endregion
        #region constructor
        public PartMasterView()
        {
            //lblDeletePartMessage.Content = "";
            //lblSavePartMessage.Content = "";
            //lblUpdatePartMessage.Content = "";

            InitializeComponent();
          //  IndiSCADAGlobalLibrary.AccessConfig.GetConnectionString = ConfigurationManager.ConnectionStrings["GetConnectionString"].ConnectionString;
            DataContext = this;
            FillAllGrid();
            
            if (dtDisplayParts != null)
            {
                if (dtDisplayParts.Rows.Count > 0)
                {
                    dgDisplayParts.SelectedIndex = 0;
                    SelectedPart = dtDisplayParts.Rows[dgDisplayParts.SelectedIndex]["PartNumber"].ToString();
                }
            }
            this.dgAddNewPart.AutoGeneratingColumn += detailsViewGrid_AutoGeneratingColumn;
            this.dgEditPart.AutoGeneratingColumn += detailsViewGrid_AutoGeneratingColumn;
        }
        #endregion
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
                lblDeletePartMessage.Content = "";
                lblSavePartMessage.Content = "";
                lblUpdatePartMessage.Content = "";

                // Fill datatable for new part
                try
                {
                    ServiceResponse<DataTable> ResultPartMasterSetting = IndiSCADABusinessLogic.PartMasterLogic.getNextloadSettingDetails("PartMaster");
                    DTPartMaster = ResultPartMasterSetting.Response;

                    if (DTPartMaster != null)
                    {
                        dtNewPart = new DataTable(); //dtNewPart = new DataTable();
                        dtNewPart.Columns.Add(new DataColumn("Part Details", typeof(string)));
                        dtNewPart.Columns.Add(new DataColumn("Values", typeof(string)));
                        dtNewPart.Columns.Add(new DataColumn("Unit", typeof(string)));
                        dtNewPart.Columns.Add(new DataColumn("DataType", typeof(string)));
                        dtNewPart.Columns[0].ReadOnly = true;
                        dtNewPart.Columns[2].ReadOnly = true;
                        dtNewPart.Columns[3].ReadOnly = true;
                       // dtNewPart.Columns[3].MaxWidth = 0; dtNewPart.Columns[3].IsHidden = true; 


                        dtEditPart = new DataTable(); //dtNewPart = new DataTable();
                        dtEditPart.Columns.Add(new DataColumn("Part Details", typeof(string)));
                        dtEditPart.Columns.Add(new DataColumn("Values", typeof(string)));
                        dtEditPart.Columns.Add(new DataColumn("Unit", typeof(string)));
                        dtEditPart.Columns.Add(new DataColumn("DataType", typeof(string)));
                        dtEditPart.Columns[0].ReadOnly = true;
                        dtEditPart.Columns[2].ReadOnly = true;
                        dtEditPart.Columns[3].ReadOnly = true;



                        for (int indexRow = 0; indexRow <= DTPartMaster.Rows.Count - 1; indexRow++)
                        {                           
                                dtNewPart.Rows.Add(DTPartMaster.Rows[indexRow]["ParameterName"], "", DTPartMaster.Rows[indexRow]["Unit"].ToString(), DTPartMaster.Rows[indexRow]["DataType"].ToString());
                                dtEditPart.Rows.Add(DTPartMaster.Rows[indexRow]["ParameterName"], "", DTPartMaster.Rows[indexRow]["Unit"].ToString(), DTPartMaster.Rows[indexRow]["DataType"].ToString());
                                // this.dgvNewPartMaster.Rows.Add(DT.Rows[indexRow]["ColumnName"].ToString(), "", DT.Rows[indexRow]["Unit"].ToString(), DT.Rows[indexRow]["DataType"].ToString());                         
                        }
                    }

                }
                catch (Exception ex) { }

                #region  Fill Combobox for edit part
                try
                {
                    DataTable dtParts = new DataTable();
                    ServiceResponse<DataTable> ResultPartMaster = IndiSCADABusinessLogic.PartMasterLogic.getPartList();

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
                        //dgAllParts.ItemsSource = dtDisplayParts.DefaultView;
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
                    ErrorLogger.LogError.ErrorLog("PartMaster fillGrid() Fill Combobox for  partDescription", DateTime.Now.ToString(), ex.ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
                }
                #endregion

                // Fill datatable to show all parts
                try
                {

                    dtDisplayParts = new DataTable();
                    ServiceResponse<DataTable> ResultPartMaster = IndiSCADABusinessLogic.PartMasterLogic.getPartDetails("", "SelectAllPart");
                    if (ResultPartMaster.Status == ResponseType.S)
                    {
                        dtDisplayParts = ResultPartMaster.Response;
                        // dgAllParts.ItemsSource = dtDisplayParts.DefaultView;                        
                    }
                }
                catch { }

                // Fill HangerType table to show all types
                try
                {

                    dtHangerTypes = new DataTable();
                    ServiceResponse<DataTable> ResultHangerMaster = IndiSCADABusinessLogic.PartMasterLogic.getHangerDetails();
                    if (ResultHangerMaster.Status == ResponseType.S)
                    {
                        dtHangerTypes = ResultHangerMaster.Response;
                        // dgAllParts.ItemsSource = dtDisplayParts.DefaultView;                        
                    }
                }
                catch { }


            }
            catch { }

        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        public void getpartlist()
        {
            try
            {
                DataTable dtParts = new DataTable();
                ServiceResponse<DataTable> ResultPartMaster = IndiSCADABusinessLogic.PartMasterLogic.getPartList();

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
                    //dgAllParts.ItemsSource = dtDisplayParts.DefaultView;
                }
            }
            catch (Exception ex) { }
        }
    public void getPartDescription()
        {
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
                ErrorLogger.LogError.ErrorLog("PartMaster getPartDescription() Fill Combobox for  partDescription", DateTime.Now.ToString(), ex.ToString(), null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to delete this part?","Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    SelectedPart = dtDisplayParts.Rows[dgDisplayParts.SelectedIndex]["PartNumber"].ToString();

                    ServiceResponse<int> ResultPartMaster = IndiSCADABusinessLogic.PartMasterLogic.DeleteSelectedPart(SelectedPart);
                    RefreshPartList();
                    lblDeletePartMessage.Content = "Part Deleted Sucessfully !";
                    lblSavePartMessage.Content = "";
                    lblUpdatePartMessage.Content = ""; getPartDescription(); getpartlist();
                }
                else
                {
                    return;  
                }

                
            }
            catch (Exception ex) { lblDeletePartMessage.Content = "Part Deleted Sucessfully !"; 
                lblSavePartMessage.Content = "";
                lblUpdatePartMessage.Content = "";
            }

        }

        private void BtnAddNewPart_Click(object sender, RoutedEventArgs e)
        {
           try
            {
                lblDeletePartMessage.Content = "";
                lblSavePartMessage.Content = "";
                lblUpdatePartMessage.Content = "";

                for (int indexRow = 0; indexRow <= DTPartMaster.Rows.Count - 1; indexRow++)
                {                  
                     dtNewPart.Rows[indexRow][1]="";                   

                }
            }
            catch { }

        }
        private void RefreshPartList()
        {
            try
            {
                dtDisplayParts = new DataTable();
                ServiceResponse<DataTable> ResultPartMaster = IndiSCADABusinessLogic.PartMasterLogic.getPartDetails("", "SelectAllPart");
                if (ResultPartMaster.Status == ResponseType.S)
                {
                    dtDisplayParts = ResultPartMaster.Response;
                    // dgAllParts.ItemsSource = dtDisplayParts.DefaultView;                       

                }

            }
            catch { }
            getPartDescription();
            #region Fill Combobox for edit partdetails
            try
            {
                DataTable dtParts = new DataTable();
                ServiceResponse<DataTable> ResultPartMaster = IndiSCADABusinessLogic.PartMasterLogic.getPartList();

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
            #region Fill Combobox for edit part no
            try
            {
                DataTable dtParts = new DataTable();
                ServiceResponse<DataTable> ResultPartMaster = IndiSCADABusinessLogic.PartMasterLogic.getPartList();

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
            #endregion getpartlist();
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (CheckAllParamertersAreFilled())//&& txtArea.TextLength != 0 )
                {
                   // string a = dtNewPart.Rows[0][0].ToString().Trim();
                  //  string b = Regex.Replace(dtNewPart.Rows[0][0].ToString(), @"\s+", "");
                   // string c = dtNewPart.Rows[0][0].ToString().Replace(" ", string.Empty);

                    DataTable isdtPartNumberavailable = new DataTable();

                    string partName = "";
                    try
                    {
                        DataRow[] results = DTPartMaster.Select(" ParameterName='" + dtNewPart.Rows[0][0].ToString() + "'");
                        partName = results[0]["ColumnName"].ToString();
                    }
                    catch (Exception ex)
                    {

                    }

                    ServiceResponse<DataTable> dtpartdata = IndiSCADABusinessLogic.PartMasterLogic.getDataPartMaster(dtNewPart.Rows[0][0].ToString().Replace(" ", string.Empty).Trim(), dtNewPart.Rows[0][1].ToString());
                    isdtPartNumberavailable = dtpartdata.Response;

                    if (isdtPartNumberavailable.Rows.Count == 0)
                    {

                        string SelectQuery = "Insert into PartMaster (";
                        string Values = "( ";
                        for (int indexRow = 0; indexRow <= DTPartMaster.Rows.Count - 2; indexRow++)
                        {

                            if ((dtNewPart.Rows[indexRow][3].ToString().StartsWith("text")) || (dtNewPart.Rows[indexRow][3].ToString().StartsWith("var")))
                            {
                                SelectQuery = SelectQuery + "[" + DTPartMaster.Rows[indexRow]["ColumnName"].ToString().Replace(" ", string.Empty).Trim() + "],";
                                Values = Values + "'" + dtNewPart.Rows[indexRow][1].ToString() + "',";
                            }
                            else
                            {
                                SelectQuery = SelectQuery + DTPartMaster.Rows[indexRow]["ColumnName"].ToString().Replace(" ", string.Empty).Trim() + ",";
                                Values = Values + "" + dtNewPart.Rows[indexRow][1].ToString() + ",";
                            }
                        }
                        if ((dtNewPart.Rows[DTPartMaster.Rows.Count - 1][3].ToString() == "text") || (dtNewPart.Rows[DTPartMaster.Rows.Count - 1][3].ToString().StartsWith("var")))
                        {
                            SelectQuery = SelectQuery + DTPartMaster.Rows[DTPartMaster.Rows.Count - 1]["ColumnName"].ToString().Replace(" ", string.Empty).Trim() + ") ";
                            Values = Values + "'" + dtNewPart.Rows[DTPartMaster.Rows.Count - 1][1].ToString() + "')";
                        }
                        else
                        {
                            SelectQuery = SelectQuery + DTPartMaster.Rows[DTPartMaster.Rows.Count - 1]["ColumnName"].ToString().Replace(" ", string.Empty).Trim() + ") ";
                            Values = Values + "" + dtNewPart.Rows[DTPartMaster.Rows.Count - 1][1].ToString() + ")";
                        }
                        SelectQuery = SelectQuery + "Values" + Values;

                        ServiceResponse<int> ResultPartMaster = IndiSCADABusinessLogic.PartMasterLogic.SaveNewPart(SelectQuery);
                        RefreshPartList();

                        lblSavePartMessage.Content = "Part Saved Sucessfully !";
                        lblDeletePartMessage.Content = "";
                        lblUpdatePartMessage.Content = ""; getPartDescription(); getpartlist();
                        // FillAllGrid();getPartDescription()
                    }
                }
            }
            catch (Exception ex)
            {
                lblSavePartMessage.Content = "Fail to Save Part !";
                lblDeletePartMessage.Content = "";
                lblUpdatePartMessage.Content = "";
            }

    }
        public bool CheckAllParamertersAreFilled()
        {
            try
            {
                for (int indexRow = 0; indexRow <= dtNewPart.Rows.Count - 2; indexRow++)
                {
                    if (dtNewPart.Rows[indexRow][1].ToString().Length > 0)
                    {

                    }
                    else
                    {
                        lblSavePartMessage.Content = "Please Fill All Information !";
                        return false; 
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckEditPartDetailsAreFilled()
        {
            try
            {
                for (int indexRow = 0; indexRow <= dtEditPart.Rows.Count - 2; indexRow++)
                {
                    if (dtEditPart.Rows[indexRow][1].ToString().Length > 0)
                    {
                       
                    }
                    else
                    {
                        lblUpdatePartMessage.Content = "Please Fill All Information !";
                        return false;
                    }
                }

                if(cmbPartlist.SelectedItem.ToString()=="")
                {
                    lblUpdatePartMessage.Content = "Please Select Part to Update !";
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        private void CmbPartlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string partName = "";
            lblDeletePartMessage.Content = "";
            lblSavePartMessage.Content = "";
            lblUpdatePartMessage.Content = "";
            try
            {
                dtSelectedPart = new DataTable();
                string SelectQuery = "Select ";
                for (int indexRow = 0; indexRow <= dtEditPart.Rows.Count - 2; indexRow++)
                {
                    //ADDED BY SBS for ParameterName ColumnName logic
                    //try
                    //{
                    //    DataRow[] results = DTPartMaster.Select(" ParameterName='" + dtNewPart.Rows[0][0].ToString() + "'");
                    //    partName = results[0]["ColumnName"].ToString();
                    //}
                    //catch (Exception ex)
                    //{

                    //}
                    //SelectQuery = SelectQuery + "[" + partName + "],";
                    //-------------------------------------

                    SelectQuery = SelectQuery + "[" + dtEditPart.Rows[indexRow]["Part Details"].ToString().Replace(" ", string.Empty).Trim() + "],";
                    
                }
                //ADDED BY SBS for ParameterName ColumnName logic
                //try
                //{
                //    DataRow[] results = DTPartMaster.Select(" ParameterName='" + dtEditPart.Rows[dtEditPart.Rows.Count - 1]["Part Details"].ToString() + "'");
                //    partName = results[0]["ColumnName"].ToString();
                //}
                //catch (Exception ex)
                //{

                //}
                //SelectQuery = SelectQuery + partName + " From PartMaster where PartNumber like '" + cmbPartlist.SelectedItem.ToString() + "'";
                //-------------------------------------


                SelectQuery = SelectQuery + dtEditPart.Rows[dtEditPart.Rows.Count - 1]["Part Details"].ToString().Replace(" ", string.Empty).Trim() + " From PartMaster where PartNumber like '" + cmbPartlist.SelectedItem.ToString() + "'";
                DataTable dtWhereData = new DataTable();

                ServiceResponse<DataTable> ResultPartMaster = IndiSCADABusinessLogic.PartMasterLogic.getSelectedPartDetails(SelectQuery);
                dtSelectedPart = ResultPartMaster.Response;
                if (ResultPartMaster.Status == ResponseType.S)
                {
                    dtSelectedPart = ResultPartMaster.Response;
                    if (dtSelectedPart != null)
                    {
                        if (dtSelectedPart.Rows.Count > 0)
                        {
                            for (int indexRow = 0; indexRow < dtSelectedPart.Columns.Count; indexRow++)
                            {
                                dtEditPart.Rows[indexRow][1] = dtSelectedPart.Rows[0][indexRow].ToString();
                            }
                            // dgAllParts.ItemsSource = dtDisplayParts.DefaultView;                        
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {      
                //ADDED BY SBS for ParameterName ColumnName logic
                string partName = "";
                try
                {
                    DataRow[] results = DTPartMaster.Select(" ParameterName='" + dtNewPart.Rows[0][0].ToString() + "'");
                    partName = results[0]["ColumnName"].ToString();
                }
                catch (Exception ex)
                {

                }
                //-------------------------------------------------

                if (MessageBox.Show("Do you want to update this part?","Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (CheckEditPartDetailsAreFilled())
                    {
                        DataTable isdtPartNumberavailable = new DataTable();

                        //ADDED BY SBS for ParameterName ColumnName logic
                        ServiceResponse<DataTable> dtpartdata = IndiSCADABusinessLogic.PartMasterLogic.getDataPartMaster(partName, dtEditPart.Rows[0][1].ToString());
                        //-------------------------------

                        //ServiceResponse<DataTable> dtpartdata = IndiSCADABusinessLogic.PartMasterLogic.getDataPartMaster(dtNewPart.Rows[0][0].ToString().Replace(" ", string.Empty).Trim(), dtEditPart.Rows[0][1].ToString());
                        isdtPartNumberavailable = dtpartdata.Response;


                        string SelectQuery = "Update PartMaster Set ";
                        for (int indexRow = 0; indexRow <= DTPartMaster.Rows.Count - 2; indexRow++)
                        {
                            if ((dtEditPart.Rows[indexRow][3].ToString().StartsWith("text")) || (dtEditPart.Rows[indexRow][3].ToString().StartsWith("var")))
                            {
                                SelectQuery = SelectQuery + "[" + DTPartMaster.Rows[indexRow]["ColumnName"].ToString().Replace(" ", string.Empty).Trim() + "]= '" + dtEditPart.Rows[indexRow][1].ToString() + "',";
                            }
                            else
                            {
                                SelectQuery = SelectQuery + "[" + DTPartMaster.Rows[indexRow]["ColumnName"].ToString().Replace(" ", string.Empty).Trim() + "]=" + dtEditPart.Rows[indexRow][1].ToString() + ",";
                            }

                        }
                        if ((dtEditPart.Rows[DTPartMaster.Rows.Count - 1][3].ToString().StartsWith("text")) || (dtEditPart.Rows[DTPartMaster.Rows.Count - 1][3].ToString().StartsWith("var")))
                        {
                            SelectQuery = SelectQuery + "[" + DTPartMaster.Rows[DTPartMaster.Rows.Count - 1]["ColumnName"].ToString().Replace(" ", string.Empty).Trim() + "]='" + dtEditPart.Rows[DTPartMaster.Rows.Count - 1][1].ToString() + "'" + " where PartNumber='" + (cmbPartlist.SelectedItem.ToString()) + "'";
                        }
                        else
                        {
                            SelectQuery = SelectQuery + "[" + DTPartMaster.Rows[DTPartMaster.Rows.Count - 1]["ColumnName"].ToString().Replace(" ", string.Empty).Trim() + "]=" + dtEditPart.Rows[DTPartMaster.Rows.Count - 1][1].ToString() + "" + " where PartNumber='" + (cmbPartlist.SelectedItem.ToString()) + "'";
                        }
                        ServiceResponse<int> ResultPartMaster = IndiSCADABusinessLogic.PartMasterLogic.UpdateSelectedPart(SelectQuery);
                        RefreshPartList();
                        RefreshPartListCombobox();
                        lblUpdatePartMessage.Content = "Part Updated Sucessfully !";
                        lblDeletePartMessage.Content = "";
                        lblSavePartMessage.Content = "";

                        #region  Fill Combobox for edit part
                        try
                        {
                            DataTable dtParts = new DataTable();
                            ServiceResponse<DataTable> ResultPartMaster1 = IndiSCADABusinessLogic.PartMasterLogic.getPartList();

                            if (ResultPartMaster1.Status == ResponseType.S)
                            {
                                dtParts = ResultPartMaster1.Response;
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
                                //dgAllParts.ItemsSource = dtDisplayParts.DefaultView;
                            }
                        }
                        catch (Exception ex) { }
                        #endregion
                        getPartDescription();
                    }

                    else
                    {
                        MessageBox.Show("Pleae fill all data");
                        lblDeletePartMessage.Content = "";
                        lblSavePartMessage.Content = "";
                    }
                }
                else
                {
                    return;
                }


              


            }
            catch (Exception ex) { lblUpdatePartMessage.Content = "Fail to Update !"; }
        }

       
         private void RefreshPartListCombobox()
        {
            try
            {
                DataTable dtParts = new DataTable();
                ServiceResponse<DataTable> ResultPartMaster = IndiSCADABusinessLogic.PartMasterLogic.getPartList();

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
        }

        #region validations by datatype and range
        private void DgEditPart_CurrentCellValidating(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellValidatingEventArgs e)
        {
            try
            {
                DataRowView dr = (DataRowView)e.RowData;

                string newvalue = e.NewValue.ToString();

                if (dr[3].ToString() == "int" || dr[3].ToString() == "number")
                {
                    newvalue = e.NewValue.ToString();
                    if (!newvalue.All(char.IsDigit))
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "Only numbers are allowed";
                    }
                }
                if (dr[3].ToString() == "float")
                {
                    newvalue = e.NewValue.ToString();
                    float val = 0;
                    bool valid = float.TryParse(newvalue, out val);
                    if (!valid)
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "Only real numbers are allowed";
                    }

                }
                if (dr[0].ToString() == "HangerType")
                {
                    string validHangers = "";
                    bool checkValue = false;
                    int count = 0;
                    newvalue = e.NewValue.ToString();
                    foreach (DataRow drHangers in dtHangerTypes.Rows)
                    {

                        if (newvalue == drHangers["HangerType"].ToString())
                        {
                            checkValue = true;
                        }
                        validHangers += drHangers["HangerType"].ToString() + " / ";
                    }
                    if (checkValue == false)
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "Allowed HangerType are :" + validHangers.Substring(0,validHangers.Length-1).ToString();

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
                        for (int j = 0; j < dtNewPart.Rows.Count; j++)
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


                if (dr[0].ToString() == "Anodic 1 mm2")
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
                        for (int j = 0; j < dtNewPart.Rows.Count; j++)
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
            }
            catch (Exception ex) { }

        }

        private void DgAddNewPart_CurrentCellValidating(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellValidatingEventArgs e)
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
                    //string newvalue = e.NewValue.ToString();
                    float val = 0;
                    bool valid = float.TryParse(newvalue, out val);
                    if (!valid)
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "Only real numbers are allowed";
                    }

                }
                if (dr[0].ToString() == "HangerType")
                {
                    string validHangers = "";
                    bool checkValue = false;
                    int count = 0;
                    //string newvalue = e.NewValue.ToString();
                    foreach (DataRow drHangers in dtHangerTypes.Rows)
                    {

                        if (newvalue == drHangers["HangerType"].ToString())
                        {
                            checkValue = true;
                        }
                        validHangers += " " + drHangers["HangerType"].ToString() + " /";
                    }
                    if (checkValue == false)
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "Allowed HangerType are :" + validHangers.Substring(0, validHangers.Length - 1).ToString();

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

                if (dr[0].ToString() == "Anodic 1 CD")//dm2
                {
                    if (Convert.ToDouble(newvalue) <= 10 && Convert.ToDouble(newvalue) >= 0)
                    {
                        for (int j = 0; j < dtNewPart.Rows.Count; j++)
                        {
                            if (dtNewPart.Rows[j][0].ToString() == "Anodic 1 CD mm2")
                            {
                                dtNewPart.Rows[j][1] = Convert.ToDouble(newvalue) * 10000;
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
                        for (int j = 0; j < dtNewPart.Rows.Count; j++)
                        {
                            if (dtNewPart.Rows[j][0].ToString() == "Anodic 2 CD mm2")
                            {
                                dtNewPart.Rows[j][1] = Convert.ToDouble(newvalue) * 10000;
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
                        for (int j = 0; j < dtNewPart.Rows.Count; j++)
                        {
                            if (dtNewPart.Rows[j][0].ToString() == "Alkaline Zinc CD mm2")
                            {
                                dtNewPart.Rows[j][1] = Convert.ToDouble(newvalue) * 10000;
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
                        for (int j = 0; j < dtNewPart.Rows.Count; j++)
                        {
                            if (dtNewPart.Rows[j][0].ToString() == "Zinc Nickel CD mm2")
                            {
                                dtNewPart.Rows[j][1] = Convert.ToDouble(newvalue) * 10000;
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
                        for (int j = 0; j < dtNewPart.Rows.Count; j++)
                        {
                            if (dtNewPart.Rows[j][0].ToString() == "Anodic 1 CD")
                            {
                                dtNewPart.Rows[j][1] = Convert.ToDouble(newvalue) / 10000;
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
                        for (int j = 0; j < dtNewPart.Rows.Count; j++)
                        {
                            if (dtNewPart.Rows[j][0].ToString() == "Anodic 2 CD")
                            {
                                dtNewPart.Rows[j][1] = Convert.ToDouble(newvalue) / 10000;
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
                        for (int j = 0; j < dtNewPart.Rows.Count; j++)
                        {
                            if (dtNewPart.Rows[j][0].ToString() == "Alkaline Zinc CD")
                            {
                                dtNewPart.Rows[j][1] = Convert.ToDouble(newvalue) / 10000;
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
                        for (int j = 0; j < dtNewPart.Rows.Count; j++)
                        {
                            if (dtNewPart.Rows[j][0].ToString() == "Zinc Nickel CD")
                            {
                                dtNewPart.Rows[j][1] = Convert.ToDouble(newvalue) / 10000;
                            }
                        }
                    }
                    else
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "Must be in range 0 to 100000 mm2";
                    }
                }
            }
            catch (Exception ex) { }

        }

        #endregion

        private void CmbHangerlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnPopupClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Close popup window  date selection
                HangerTypePopUp.IsOpen = false;
                //Report selected label content
              
            }
            catch (Exception ex)
            {
              
            }
        }

        private void BtnUpdateHanger_Click(object sender, RoutedEventArgs e)
        {
            try
            {               
                foreach (DataRow dr in dtHangerTypes.Rows)
                {
                   ServiceResponse<DataTable> dtExists = IndiSCADABusinessLogic.PartMasterLogic.isHangerTypeExists(dr["HangerType"].ToString());
                    DataTable dt = dtExists.Response;
                    if (dt != null)
                    {
                        if (dt.Rows.Count == 0)
                        {
                            ServiceResponse<int> result = IndiSCADABusinessLogic.PartMasterLogic.InsertHangerType(dr);
                        }
                        else
                        {
                            ServiceResponse<int> result = IndiSCADABusinessLogic.PartMasterLogic.UpdateHangerType(dr);
                        }
                    }
                }
            }
            catch  {}

            // Fill HangerType table to show all types
            try
            {

                dtHangerTypes = new DataTable();
                ServiceResponse<DataTable> ResultHangerMaster = IndiSCADABusinessLogic.PartMasterLogic.getHangerDetails();
                if (ResultHangerMaster.Status == ResponseType.S)
                {
                    dtHangerTypes = ResultHangerMaster.Response;
                    // dgAllParts.ItemsSource = dtDisplayParts.DefaultView;                        
                }
            }
            catch { }

        }

        private void BtnDeleteHangerType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblDeletePartMessage.Content = "";
                lblSavePartMessage.Content = "";
                lblUpdatePartMessage.Content = "";

                string SelectedType = dtHangerTypes.Rows[dgHangerTypes.SelectedIndex]["HangerType"].ToString();

                ServiceResponse<int> ResultPartMaster = IndiSCADABusinessLogic.PartMasterLogic.DeleteSelectedHangerType(SelectedType);
                // Fill HangerType table to show all types
                try
                {

                    dtHangerTypes = new DataTable();
                    ServiceResponse<DataTable> ResultHangerMaster = IndiSCADABusinessLogic.PartMasterLogic.getHangerDetails();
                    if (ResultHangerMaster.Status == ResponseType.S)
                    {
                        dtHangerTypes = ResultHangerMaster.Response;
                        // dgAllParts.ItemsSource = dtDisplayParts.DefaultView;                        
                    }
                }
                catch { }

            }
            catch { }
        }        

        private void BtnHangerType_Click(object sender, RoutedEventArgs e)
        {
          
            try
            {
                HangerTypePopUp.IsOpen = true;
            }
            catch { }
        }     

        private void DgHangerTypes_CurrentCellValidated(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellValidatedEventArgs e)
        {
            try
            {
                DataRowView dr = (DataRowView)e.RowData;
                
                //{
                //    string newvalue = e.NewValue.ToString();
                //    if (!newvalue.All(char.IsDigit))
                //    {
                //        e.IsValid = false;
                //        e.ErrorMessage = "Only numbers are allowed";
                //    }
                //}
            }
            catch { }
        }

        private void DgAddNewPart_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                //ToolTip toolTip = new ToolTip();
                //var visualContainer = dgAddNewPart.GetVisualContainer();
                //var point = e.GetPosition(visualContainer);
                //var rowcolumnindex = visualContainer.PointToCellRowColumnIndex(point);
                //var rowindex = rowcolumnindex.RowIndex;
                //if (rowindex != rowColumnIndex.RowIndex)
                //    toolTip.IsOpen = false;
                //var columnindex = rowcolumnindex.ColumnIndex;
                //rowColumnIndex = rowcolumnindex;
                //// Get the resolved current record index
                //var recordIndex = this.dataGrid.ResolveToRecordIndex(rowindex);

                //if (rowindex == 2 && columnindex == 1) // EmployeeName
                //{
                //    // Get the current row record
                //    var mappingName = this.dgAddNewPart.Columns[columnindex].MappingName;
                //    var record = this.dgAddNewPart.View.Records.GetItemAt(recordIndex);
                //    var cellvalue = record.GetType().GetProperty(mappingName).GetValue(record, null).ToString();
                //    toolTip.Content = cellvalue;
                //    var dataColumnBase = SelectionHelper.GetDataColumnBase(this.dataGrid, new Syncfusion.UI.Xaml.ScrollAxis.RowColumnIndex(rowindex, columnindex));
                //    if (dataColumnBase != null)
                //    {
                //        GridCell gridCell = dataColumnBase.ColumnElement as GridCell;
                //        if (gridCell != null)
                //        {
                //            ToolTipService.SetToolTip(gridCell, toolTip);
                //            toolTip.IsOpen = true;
                //        }
                //    }
                //}
            }
            catch (Exception ex) { }
        }

        private void CmbPartDescriptionlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                dtSelectedPart = new DataTable();
                string SelectQuery;
                SelectQuery = "select * from PartMaster where Description='" + cmbPartDescriptionlist.SelectedItem.ToString() + "'";
                DataTable dtdescriptionData = new DataTable();

                ServiceResponse<DataTable> ResultPartMaster = IndiSCADABusinessLogic.PartMasterLogic.getSelectedPartDetails(SelectQuery);
                dtSelectedPart = ResultPartMaster.Response;
                if (ResultPartMaster.Status == ResponseType.S)
                {
                    dtdescriptionData = ResultPartMaster.Response;
                    if (dtdescriptionData != null)
                    {
                        if (dtdescriptionData.Rows.Count > 0)
                        {
                            PartList = new ObservableCollection<string>();
                            for (int indexRow = 0; indexRow < dtdescriptionData.Rows.Count; indexRow++)
                            {
                                PartList.Add(dtdescriptionData.Rows[indexRow][0].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void CmbPartDescriptionlist_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                CollectionView itemsViewOriginal = (CollectionView)CollectionViewSource.GetDefaultView(cmbPartDescriptionlist.ItemsSource);

                itemsViewOriginal.Filter = ((o) =>
                {
                    if (String.IsNullOrEmpty(cmbPartDescriptionlist.Text)) return true;
                    else
                    {
                        if (((string)o).Contains(cmbPartDescriptionlist.Text)) return true;
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
    }
}
