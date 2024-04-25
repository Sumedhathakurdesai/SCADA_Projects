
using IndiSCADAEntity.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Syncfusion.Windows.Shared;
using System.Windows.Controls;
using System.Data;
using IndiSCADAGlobalLibrary;
using System.Windows.Threading;
using MahApps.Metro.Controls.Dialogs;
using System.Configuration;
using System;
using System.Data;
using System.Data.Sql;
using Microsoft.Win32;
using IndiSCADA_ST.View; 

namespace IndiSCADA_ST.ViewModel
{
    public class ConfigurationViewModel : BaseViewModel
    {
        #region"Declaration"
        DispatcherTimer DispatchTimerView = new DispatcherTimer();
        System.ComponentModel.BackgroundWorker _BackgroundWorkerView = new System.ComponentModel.BackgroundWorker();
        #endregion
        #region ICommand     
        #region "Chemical names command"
        private readonly ICommand _AddChemicalNameCommand;
        public ICommand AddChemicalNameCommand
        {
            get { return _AddChemicalNameCommand; }
        }
        private readonly ICommand _UpdateChemicalNameCommand;
        public ICommand UpdateChemicalNameCommand
        {
            get { return _UpdateChemicalNameCommand; }
        }
        private readonly ICommand _DeleteChemicalNameCommand;
        public ICommand DeleteChemicalNameCommand
        {
            get { return _DeleteChemicalNameCommand; }
        }
        private readonly ICommand _SelectionChangeCommand;
        public ICommand SelectionChangeCommand
        {
            get { return _SelectionChangeCommand; }
        }
        private readonly ICommand _StopCH;//
        public ICommand StopCH
        {
            get { return _StopCH; }
        }
        private readonly ICommand _NameChangeCommand;//
        public ICommand NameChangeCommand
        {
            get { return _NameChangeCommand; }
        }

        #endregion

        private readonly ICommand _ClearShiftTimingCommand;
        public ICommand ClearShiftTimingCommand
        {
            get { return _ClearShiftTimingCommand; }
        }
        private readonly ICommand _SetShiftTimingCommand;
        public ICommand SetShiftTimingCommand
        {
            get { return _SetShiftTimingCommand; }
        }

        private readonly ICommand _GenerateDatabase;//
        public ICommand GenerateDatabase
        {
            get { return _GenerateDatabase; }
        }
        
        private readonly ICommand _StopWagonStatus;//
        public ICommand StopWagonStatus
        {
            get { return _StopWagonStatus; }
        }
        private readonly ICommand _Exit;//
        public ICommand Exit
        {
            get { return _Exit; }
        }
        private readonly ICommand _AddNewParameter;
        public ICommand SaveParameter
        {
            get { return _AddNewParameter; }
        }
        private readonly ICommand _UpdateParameter;
        public ICommand UpdateParameter
        {
            get { return _UpdateParameter; }
        }
        private readonly ICommand _DeleteParameter;
        public ICommand DeleteParameter
        {
            get { return _DeleteParameter; }
        }
        private readonly ICommand _CreateTableCommand;
        public ICommand CreateTableCommand
        {
            get { return _CreateTableCommand; }
        }
        private readonly ICommand _AddParameter;
        public ICommand AddParameter
        {
            get { return _AddParameter; }
        }
        private readonly ICommand _TagSelectedCommand;
        public ICommand TagSelectedCommand
        {
            get { return _TagSelectedCommand; }
        }
        private readonly ICommand _TagValueToWriteCommand;
        public ICommand TagValueToWrite
        {
            get { return _TagValueToWriteCommand; }
        }
        private readonly ICommand _FirstShiftTimeChangeCommand;
        public ICommand FirstShiftTimeChangeCommand
        {
            get { return _FirstShiftTimeChangeCommand; }
        }
        private readonly ICommand _CalculateFormulaCommand;
        public ICommand CalculateFormulaCommand
        {
            get { return _CalculateFormulaCommand; }
        }
        
        private readonly ICommand _ParameterSelectCommand;
        public ICommand ParameterSelectCommand
        {
            get { return _ParameterSelectCommand; }
        }

        private readonly ICommand _ParameterChangeCommand;
        public ICommand ParameterChangeCommand
        {
            get { return _ParameterChangeCommand; }
        }
        
        private readonly ICommand _UpdateFormulaCommand;
        public ICommand UpdateFormulaCommand
        {
            get { return _UpdateFormulaCommand; }
        }
        
        #endregion


        #region"Destructor"
        ~ConfigurationViewModel()
        {
            try
            {
                DispatchTimerView.Stop();
            }
            catch { }
        }
        #endregion
        #region "Consrtuctor"
        public ConfigurationViewModel()
        {
            try
            {
                _BackgroundWorkerView.DoWork += DoWork;
                DispatchTimerView.Interval = TimeSpan.FromSeconds(1);
                DispatchTimerView.Tick += DispatcherTickEvent;
                DispatchTimerView.Start();
                _Exit = new RelayCommand(ExitButtonCommandClick);
                _AddNewParameter = new RelayCommand(AddNewParameterClick);
                _UpdateParameter = new RelayCommand(UpdateParameterClick);
                _DeleteParameter = new RelayCommand(DeleteParameterClick);
                _CreateTableCommand = new RelayCommand(CreateTableClick);
                _AddParameter = new RelayCommand(AddParameterClick);
                _TagSelectedCommand = new RelayCommand(TagSelectedCommandClick);
                _TagValueToWriteCommand = new RelayCommand(TagWriteCommandClick);
                _FirstShiftTimeChangeCommand = new RelayCommand(ShiftTimeChangeCommandClick);
                _CalculateFormulaCommand = new RelayCommand(CalculateFormulaClick);
                _ParameterSelectCommand = new RelayCommand(ParameterSelectClick);
                _ParameterChangeCommand = new RelayCommand(ParameterChangeClick);
                _UpdateFormulaCommand = new RelayCommand(UpdateFormulaClick); 
                _StopWagonStatus = new RelayCommand(WagonStatusStopClick); 

                //database generatn
                _GenerateDatabase = new RelayCommand(GenerateDatabaseClick);

                //shift setting
                _SetShiftTimingCommand = new RelayCommand(AddShiftTimingCommandClick);
                _ClearShiftTimingCommand = new RelayCommand(ClearShiftTimingCommandClick);
                 
                //chemical name settting
                _AddChemicalNameCommand = new RelayCommand(AddChemicalNameCommandClick);
                _UpdateChemicalNameCommand = new RelayCommand(UpdateChemicalNameCommandClick);
                _DeleteChemicalNameCommand = new RelayCommand(DeleteChemicalNameCommandClick);
                _StopCH = new RelayCommand(StopCHRefreshClick);
                _NameChangeCommand = new RelayCommand(NameChangeCommandClick);
                PumpMasterData = IndiSCADABusinessLogic.ConfigurationLogic.SelectChemicalMasterData().Response;
                ChemicalNameMasterCollection = IndiSCADABusinessLogic.ConfigurationLogic.GetChemicalNameMasterData();
                RemainingPumpData = IndiSCADABusinessLogic.ConfigurationLogic.GetRemainingPumpData();



                try
                {
                    AllServerList = new ObservableCollection<string>();
                    SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
                    DataTable dtServerList = instance.GetDataSources();
                    for (int indexRow = 0; indexRow < dtServerList.Rows.Count; indexRow++)
                    {
                        AllServerList.Add(dtServerList.Rows[indexRow][0].ToString() + @"\" + dtServerList.Rows[indexRow][1].ToString());
                    }
                }
                catch(Exception ex)
                {
                    ErrorLogger.LogError.ErrorLog("ConfigurationViewModel() exception while fetching sql server names", DateTime.Now.ToString(), ex.Message, "No", true);
                }

                try
                {
                    IndiSCADAGlobalLibrary.ConfigurationUpdate.StopWagonStatusDatalog = false;
                    StartStopBTNcontent = "Start";
                    IndiSCADAGlobalLibrary.TagList.DataLogDebug = false; // start debug mode
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError.ErrorLog("ConfigurationViewModel() StopWagonStatusDatalog", DateTime.Now.ToString(), ex.Message, "No", true);
                }

            //shift time setting
            try
                {
                    DateTime shift1Time = Convert.ToDateTime(IndiSCADA_ST.Properties.Settings.Default.FirstShiftTime);
                    DateTime shift2Time = shift1Time.AddHours(8);
                    DateTime shift3Time = shift2Time.AddHours(8);

                    FirstShiftTime = shift1Time.ToString("hh:mm:ss");
                    SecondShiftTime = shift2Time.ToString("hh:mm:ss");
                    ThirdShiftTime = shift3Time.ToString("hh:mm:ss");
                }
                catch(Exception ex)

                { ErrorLogger.LogError.ErrorLog("ConfigurationViewModel ()", DateTime.Now.ToString(), ex.Message, "No", true); }



                #region  "Test Connection tab initialise"
                try
                {
                    AllTagsList = IndiSCADABusinessLogic.NextLoadMasterSettingsLogic.GetAllTagListFromXML();
                }
                catch { }
                #endregion

                // nextload setting tab
                try
                {
                    NextLoadData = IndiSCADABusinessLogic.NextLoadMasterSettingsLogic.SelectNextLoadSettingData();
                    UpdateParameterList();
                }
                catch (Exception ex)
                { ErrorLogger.LogError.ErrorLog("ConfigurationViewModel  ()", DateTime.Now.ToString(), ex.Message, "No", true); }

                // Wagon loadstring initialisation               
                try
                {
                    WagonLoadstringData = IndiSCADABusinessLogic.ConfigurationLogic.GetWagonDetails();
                    ShiftTimeData = IndiSCADABusinessLogic.ConfigurationLogic.SelectShiftMasterData();
                }
                catch (Exception ex)
                { ErrorLogger.LogError.ErrorLog("ConfigurationViewModel  ()", DateTime.Now.ToString(), ex.Message, "No", true); }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationViewModel ()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }


        #endregion
        #region "public/private methods"
        #region Chemical Name Setting methods

        private void AddChemicalNameCommandClick(object _Index)
        {
            try
            {
                if (PumpMasterData != null)
                {
                    //  ChemicalEntity.PumpName = SelectedPumpName;
                    foreach (DataRow dr in PumpMasterData.Rows)
                    {
                        if (dr["PumpName"].ToString() == ChemicalEntity.PumpName)
                        {
                            ChemicalEntity.PumpNumber = dr["PumpNo"].ToString();
                        }
                    }
                }

                try
                {
                    ServiceResponse<int> result = IndiSCADABusinessLogic.ConfigurationLogic.AddNewChemicalName(ChemicalEntity);
                    if (result.Response == 1)
                    {
                        System.Windows.MessageBox.Show("Data saved successfully.", "Saving", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information); 
                    }
                    else if (result.Response == 2)
                    {
                        System.Windows.MessageBox.Show("Data not saved. The total percentage for the selected station exceeds 100.", "Warning", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                    }
                }
                catch { }



                ChemicalNameMasterCollection = IndiSCADABusinessLogic.ConfigurationLogic.GetChemicalNameMasterData();

                RemainingPumpData = IndiSCADABusinessLogic.ConfigurationLogic.GetRemainingPumpData();

                if (ChemicalNameMasterCollection.Count > 0)
                    ChemicalEntity = ChemicalNameMasterCollection[0];
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationViewModel_AddNewParameterClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }

        private void UpdateChemicalNameCommandClick(object _Index)
        {
            try
            {
                // add validations to check data
                IndiSCADABusinessLogic.ConfigurationLogic.UpdateSelectedChemical(ChemicalEntity);
                ChemicalNameMasterCollection = IndiSCADABusinessLogic.ConfigurationLogic.GetChemicalNameMasterData();

                if (ChemicalNameMasterCollection.Count > 0)
                    ChemicalEntity = ChemicalNameMasterCollection[0];
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationViewModel_UpdateChemicalNameCommandClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }

        private void DeleteChemicalNameCommandClick(object _Index)
        {

            try
            {
                // add validations to check data
                IndiSCADABusinessLogic.ConfigurationLogic.DeleteSelectedChemical(ChemicalEntity);
                ChemicalNameMasterCollection = IndiSCADABusinessLogic.ConfigurationLogic.GetChemicalNameMasterData();

                RemainingPumpData = IndiSCADABusinessLogic.ConfigurationLogic.GetRemainingPumpData();

                if (ChemicalNameMasterCollection.Count > 0)
                    ChemicalEntity = ChemicalNameMasterCollection[0];
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationViewModel_DeleteParameterClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }

        private void SelectionChangeCommandClick(object _Index)
        {

            try
            {
                SelectedPumpName = ChemicalEntity.PumpName;


            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationViewModel_DeleteParameterClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        private void StopCHRefreshClick(object _commandparameters)
        {
            try
            {
                //isCHEdit = true;
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OutOfRange StoppHRefreshClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }

        }

        private void NameChangeCommandClick(object _commandparameters)
        {
            try
            {
                SelectedPumpName = ChemicalEntity.PumpName;
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("OutOfRange StoppHRefreshClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }

        }
        #endregion

        private void GenerateDatabaseClick(object _Index)
        {
            try
            {
                ServiceResponse<int> DBcreatedSuccessfully = IndiSCADABusinessLogic.ConfigurationLogic.CreateDatabase(NewDatabaseName, SelectedServerName);
                int TablescreatedSuccessfully = IndiSCADABusinessLogic.ConfigurationLogic.CreateTables(NewDatabaseName, SelectedServerName);
                int ProcedurescreatedSuccessfully = IndiSCADABusinessLogic.ConfigurationLogic.CreateProcedures(NewDatabaseName, SelectedServerName);
                if(DBcreatedSuccessfully.Response==1 && TablescreatedSuccessfully==1 && ProcedurescreatedSuccessfully == 1)
                {
                   System.Windows.MessageBox.Show("Database Created Successfully");
                }
                else
                {
                    System.Windows.MessageBox.Show("Fail to create Datbase");
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationViewModel GenerateDatabaseClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        private void AddShiftTimingCommandClick(object _Index)
        {
            try
            {
                // add validations to check data
                if ((System.Windows.MessageBox.Show("Do you want to change the Settings?", "Warning", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question, System.Windows.MessageBoxResult.Yes) == System.Windows.MessageBoxResult.Yes))
                {
                    if (NoOfShift == "1" || NoOfShift == "2" || NoOfShift == "3")
                    {
                        int totalshifts = Convert.ToInt32(NoOfShift);
                        int shiftno = Convert.ToInt32(ShiftEntity.ShiftNumber);
                        ShiftMasterEntity SEntity = ShiftEntity;
                        if (shiftno > 0)
                        {
                            //  IndiSCADABusinessLogic.ConfigurationLogic.ClearShiftData();
                            for (int i = 1; i <= totalshifts; i++)
                            {
                                IndiSCADABusinessLogic.ConfigurationLogic.AddNewShiftTiming(SEntity);
                                SEntity.ShiftNumber = (++shiftno).ToString();
                                SEntity.ShiftStartTime = SEntity.ShiftEndTime;

                                DateTime shift1Time = Convert.ToDateTime(SEntity.ShiftStartTime.ToString());
                                int shifthrs = Convert.ToInt32(ShiftHours);
                                DateTime shiftTime = shift1Time.AddHours(shifthrs);
                                SEntity.ShiftEndTime = shiftTime.ToString("HH:mm:ss");
                            }
                            ShiftEntity = new ShiftMasterEntity();
                            ShiftTimeData = IndiSCADABusinessLogic.ConfigurationLogic.SelectShiftMasterData();
                            System.Windows.MessageBox.Show("The shift changes will be applicable on next day first shift");
                            TagList.IsShiftSettingChanged = true;
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Invalid Shift Number");
                        }
                    }

                    else if (NoOfShift == "Custom")
                    {
                        int shiftno = Convert.ToInt32(ShiftEntity.ShiftNumber);
                        if (shiftno > 0)
                        {
                            IndiSCADABusinessLogic.ConfigurationLogic.AddNewShiftTiming(ShiftEntity);
                            ShiftTimeData = IndiSCADABusinessLogic.ConfigurationLogic.SelectShiftMasterData();
                            System.Windows.MessageBox.Show("The shift changes will be applicable on next day first shift");
                            TagList.IsShiftSettingChanged = true;
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Invalid Shift Number");
                        }

                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Invalid Shift Number");
                    }

                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationViewModel_AddNewParameterClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }

        private void ClearShiftTimingCommandClick(object _Index)
        {
            try
            {
                // add validations to check data
                IndiSCADABusinessLogic.ConfigurationLogic.ClearShiftData();
                ShiftTimeData = IndiSCADABusinessLogic.ConfigurationLogic.SelectShiftMasterData();

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationViewModel_AddNewParameterClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }


        private void ExitButtonCommandClick(object _commandparameters)
        {
            try
            {
                DispatchTimerView.Stop();
                IndiSCADAGlobalLibrary.TagList.DataLogDebug = false;
                
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationViewModel ExitButtonCommandClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", true);
            }
        }

        private void AddNewParameterClick(object _Index)
        {
            try
            {
                // add validations to check data

                IndiSCADABusinessLogic.NextLoadMasterSettingsLogic.AddNewParameterButton(NextloadSetting);
                NextLoadData = IndiSCADABusinessLogic.NextLoadMasterSettingsLogic.SelectNextLoadSettingData();
                UpdateParameterList();

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationViewModel_AddNewParameterClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }


        private void AddParameterClick(object _Index)
        {
            try
            {
                // add validations to check data

                NextloadSetting = new NextLoadSettingsEntity();
                //ClearAllNextloadSettingValues();
                //NextLoadData = IndiSCADABusinessLogic.NextLoadMasterSettingsLogic.SelectNextLoadSettingData();


            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationViewModel_AddParameterClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }

        private void TagSelectedCommandClick(object _Index)
        {
            try
            {
                if (SelectedTagToDisplay != null)
                {
                    SelectedTagValues = new ObservableCollection<string>();
                    try
                    {
                        string[] tagvalues = DeviceCommunication.CommunicationWithPLC.ReadPLCTagValue(SelectedTagToDisplay);

                        if (tagvalues.Length > 0)
                        {

                            SelectedTagValues.Clear();
                            for (int index = 0; index < tagvalues.Length; index++)
                                SelectedTagValues.Add(tagvalues[index]);


                        }
                        else
                        {
                            SelectedTagValues.Add("No of values : 0");
                        }
                    }
                    catch { SelectedTagValues.Add("Unable to read value"); }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationViewModel_TagSelectedClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }

        private void TagWriteCommandClick(object obj)
        {
            try
            {
                if ((TagAddresstoWrite != null) && (TagValueToWrite != null))
                {
                    try
                    {
                        DeviceCommunication.CommunicationWithPLC.WriteValues(TagAddresstoWrite, TagValuetoWrite);                       
                    }
                    catch { TagValuetoWrite = "Unable to Write value"; }
                }
            }
            catch (Exception ex) { ErrorLogger.LogError.ErrorLog("ConfigurationViewModel TagWriteCommandClick()", DateTime.Now.ToString(), ex.Message, "No", true); }
            
        }

        private void ShiftTimeChangeCommandClick(object obj)
        {
            try
            {
                DateTime shift1Time =Convert.ToDateTime(FirstShiftTime.ToString());
                DateTime shift2Time = shift1Time.AddHours(8);
                DateTime shift3Time = shift2Time.AddHours(8);

                FirstShiftTime = shift1Time.ToString("hh:mm:ss");
                SecondShiftTime = shift2Time.ToString("hh:mm:ss");
                ThirdShiftTime = shift3Time.ToString("hh:mm:ss");

                IndiSCADA_ST.Properties.Settings.Default.FirstShiftTime = FirstShiftTime;

                IndiSCADA_ST.Properties.Settings.Default.Save();
            }
            catch (Exception ex) { ErrorLogger.LogError.ErrorLog("ConfigurationViewModel ShiftTimeChangeCommandClick()", DateTime.Now.ToString(), ex.Message, "No", true); }

        }

        private void CalculateFormulaClick(object paraname)
        {
            try
            {
                //   isOpenFormulaPopup = true;
                FormulaParameter = paraname.ToString();
                FormulaView objFormulaView = new FormulaView(paraname.ToString());
                IndiSCADA_ST.Properties.Settings.Default.ParameterName = paraname.ToString(); 
                objFormulaView.ShowDialog();
            }
            catch (Exception ex) { ErrorLogger.LogError.ErrorLog("ConfigurationViewModel CalculateFormulaClick()", DateTime.Now.ToString(), ex.Message, "No", true); }

        }

        private void ParameterSelectClick(object obj)
        {
            try
            {
                
                FormulaString +=" [" + SelectedParameter + "] ";
            }
            catch (Exception ex) { ErrorLogger.LogError.ErrorLog("ConfigurationViewModel ParameterSelectClick()", DateTime.Now.ToString(), ex.Message, "No", true); }

        }
        private void ParameterChangeClick(object obj)
        {
            try
            {
                IndiSCADA_ST.Properties.Settings.Default.ParameterName = FormulaParameter;
            }
            catch (Exception ex) { ErrorLogger.LogError.ErrorLog("ConfigurationViewModel ParameterChangeClick()", DateTime.Now.ToString(), ex.Message, "No", true); }

        }

        private void UpdateParameterClick(object _Index)
        {
            try
            {
                // add validations to check data
                IndiSCADABusinessLogic.NextLoadMasterSettingsLogic.UpdateParameterButton(NextloadSetting);
                NextLoadData = IndiSCADABusinessLogic.NextLoadMasterSettingsLogic.SelectNextLoadSettingData();
                UpdateParameterList();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationViewModel_UpdateParameterClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        private void WagonStatusStopClick(object _Index)
        {
            try
            {
                if (IndiSCADAGlobalLibrary.ConfigurationUpdate.StopWagonStatusDatalog == false)
                {
                    DispatchTimerView.Start();
                    IndiSCADAGlobalLibrary.ConfigurationUpdate.StopWagonStatusDatalog = true;
                    StartStopBTNcontent = "Stop";
                }
                else
                {
                    DispatchTimerView.Stop();
                    IndiSCADAGlobalLibrary.ConfigurationUpdate.StopWagonStatusDatalog = false;
                    StartStopBTNcontent = "Start";
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationViewModel WagonStatusStopClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        private void UpdateFormulaClick(object _Index)
        {
            try
            {
                // add validations to check data
                FormulaParameter = IndiSCADA_ST.Properties.Settings.Default.ParameterName;
                ServiceResponse<int> result = IndiSCADABusinessLogic.NextLoadMasterSettingsLogic.UpdateFormulaInNextloadSetting(FormulaParameter, FormulaString);
                if (result.Message != "")
                {
                    UpdateResult = result.Message;
                }
                else
                    if (result.Status == ResponseType.S)
                { UpdateResult = "Formula updated successfully."; }
                else
                { UpdateResult = "Unable to update formula."; }
                
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationViewModel_UpdateFormulaClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        private  DataTable ConvertListToDataTable(IList<NextLoadSettingsEntity> datalist)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            //int columns = 0;
            //foreach (var array in datalist)
            //{
            //    if (array. > columns)
            //    {
            //        columns = array.Length;
            //    }
            //}

            //// Add columns.
            //for (int i = 0; i < columns; i++)
            //{
            //    table.Columns.Add();
            //}

            //// Add rows.
            //foreach (var array in datalist)
            //{
            //    table.Rows.Add(array);
            //}

            return table;
        }
        private void UpdateParameterList()
        {
            try
            {
                
                if (NextLoadData != null)
                {
                    if (NextLoadData.Count > 0)
                    {
                        ParameterList = new ObservableCollection<string>();
                        int count = 0;
                        foreach (NextLoadSettingsEntity objnextload in NextLoadData)
                        {
                            ParameterList.Add(objnextload.ParameterName.ToString());
                            count++;
                        }
                    }
            }
            }
            catch(Exception ex) { }
        }
        private void CreateTableClick(object _Index)
        {
            try
            {
                // add validations to check data
                DataTable dtNextloadData = new DataTable();

                IndiSCADABusinessLogic.NextLoadMasterSettingsLogic.CreatePartMasterTable(NextLoadData);
               // NextLoadData = IndiSCADABusinessLogic.NextLoadMasterSettingsLogic.SelectNextLoadSettingData();


            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("SettingsViewModel RectifierOnOFFClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        private void DeleteParameterClick(object _Index)
        {
            try
            {
                // add validations to check data

                IndiSCADABusinessLogic.NextLoadMasterSettingsLogic.DeleteParameterButton(NextloadSetting);
                NextLoadData = IndiSCADABusinessLogic.NextLoadMasterSettingsLogic.SelectNextLoadSettingData();
                UpdateParameterList();

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationViewModel_DeleteParameterClick()", DateTime.Now.ToString(), ex.Message, "No", true);
            }
        }
        private void DoWork(object Sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                WagonLoadstringData = IndiSCADABusinessLogic.ConfigurationLogic.GetWagonDetails();                

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationViewModel DoWork()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        void DispatcherTickEvent(object sender, EventArgs e)
        {
            try
            {
                if (_BackgroundWorkerView.IsBusy != true)
                {
                    _BackgroundWorkerView.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("ConfigurationViewModel DispatcherTickEvent()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        #endregion

        #region Public/Private Property

        #region chemical name setting Public/Private Property
        private DataTable _PumpMasterData = new DataTable();
        public DataTable PumpMasterData
        {
            get { return _PumpMasterData; }
            set { _PumpMasterData = value; OnPropertyChanged("PumpMasterData"); }
        }
        private ChemicalNameMasterEntity _ChemicalEntity = new ChemicalNameMasterEntity();
        public ChemicalNameMasterEntity ChemicalEntity
        {
            get { return _ChemicalEntity; }
            set { _ChemicalEntity = value; OnPropertyChanged("ChemicalEntity"); }
        }
        private string _SelectedPumpName;
        public string SelectedPumpName
        {
            get { return _SelectedPumpName; }
            set { _SelectedPumpName = value; OnPropertyChanged("SelectedPumpName"); }
        }
        private ObservableCollection<ChemicalNameMasterEntity> _ChemicalNameMasterCollection;
        public ObservableCollection<ChemicalNameMasterEntity> ChemicalNameMasterCollection
        {
            get { return _ChemicalNameMasterCollection; }
            set { _ChemicalNameMasterCollection = value; OnPropertyChanged("ChemicalNameMasterCollection"); }
        }
        private ObservableCollection<string> _RemainingPumpData;
        public ObservableCollection<string> RemainingPumpData
        {
            get { return _RemainingPumpData; }
            set { _RemainingPumpData = value; OnPropertyChanged("RemainingPumpData"); }
        }

        #endregion


        private string _ShiftHours;
        public string ShiftHours
        {
            get { return _ShiftHours; }
            set { _ShiftHours = value; OnPropertyChanged("ShiftHours"); }
        }
        private ObservableCollection<ShiftMasterEntity> _ShiftTimeData;
        public ObservableCollection<ShiftMasterEntity> ShiftTimeData
        {
            get { return _ShiftTimeData; }
            set { _ShiftTimeData = value; OnPropertyChanged("ShiftTimeData"); }
        }

        private ShiftMasterEntity _ShiftEntity = new ShiftMasterEntity();
        public ShiftMasterEntity ShiftEntity
        {
            get { return _ShiftEntity; }
            set { _ShiftEntity = value; OnPropertyChanged("ShiftEntity"); }
        }
        private string _NoOfShift;
        public string NoOfShift
        {
            get { return _NoOfShift; }
            set { _NoOfShift = value; OnPropertyChanged("NoOfShift"); }
        }

        private ObservableCollection<string> _AllServerList;
        public ObservableCollection<string> AllServerList
        {
            get { return _AllServerList; }
            set { _AllServerList = value; OnPropertyChanged("AllServerList"); }
        }
        private string _NewDatabaseName;
        public string NewDatabaseName
        {
            get { return _NewDatabaseName; }
            set { _NewDatabaseName = value; OnPropertyChanged("NewDatabaseName"); }
        }
        private string _SelectedServerName;
        public string SelectedServerName
        {
            get { return _SelectedServerName; }
            set { _SelectedServerName = value; OnPropertyChanged("SelectedServerName"); }
        }
        

        private string _StartStopBTNcontent;
        public string StartStopBTNcontent
        {
            get { return _StartStopBTNcontent; }
            set { _StartStopBTNcontent = value; OnPropertyChanged("StartStopBTNcontent"); }
        }
        private ObservableCollection<string> _ParameterName = new ObservableCollection<string>();
        public ObservableCollection<string> ParameterList
        {
            get { return _ParameterName; }
            set { _ParameterName = value; OnPropertyChanged("ParameterList"); }
        }

        private ObservableCollection<NextLoadSettingsEntity> _NextLoadData;
        public ObservableCollection<NextLoadSettingsEntity> NextLoadData
        {
            get { return _NextLoadData; }
            set { _NextLoadData = value; OnPropertyChanged("NextLoadData"); }
        }

        private ObservableCollection<string> _AllTagsList;
        public ObservableCollection<string> AllTagsList
        {
            get { return _AllTagsList; }
            set { _AllTagsList = value; OnPropertyChanged("AllTagsList"); }
        }

        private ObservableCollection<string> _SelectedTagValues;
        public ObservableCollection<string> SelectedTagValues
        {
            get { return _SelectedTagValues; }
            set { _SelectedTagValues = value; OnPropertyChanged("SelectedTagValues"); }
        }

        private string _SelectedTagToDisplay;
        public string SelectedTagToDisplay
        {
            get { return _SelectedTagToDisplay; }
            set { _SelectedTagToDisplay = value; OnPropertyChanged("SelectedTagToDisplay"); }
        }

        private string _TagAddresstoWrite;
        public string TagAddresstoWrite
        {
            get { return _TagAddresstoWrite; }
            set { _TagAddresstoWrite = value; OnPropertyChanged("TagAddresstoWrite"); }
        }

        private string _TagValuetoWrite;
        public string TagValuetoWrite
        {
            get { return _TagValuetoWrite; }
            set { _TagValuetoWrite = value; OnPropertyChanged("TagValuetoWrite"); }
        }
        private string _FirstShiftTime;
        public string FirstShiftTime
        {
            get { return _FirstShiftTime; }
            set { _FirstShiftTime = value; OnPropertyChanged("FirstShiftTime"); }
        }
        private string _SecondShiftTime;
        public string SecondShiftTime
        {
            get { return _SecondShiftTime; }
            set { _SecondShiftTime = value; OnPropertyChanged("SecondShiftTime"); }
        }
        private string _ThirdShiftTime;
        public string ThirdShiftTime
        {
            get { return _ThirdShiftTime; }
            set { _ThirdShiftTime = value; OnPropertyChanged("ThirdShiftTime"); }
        }
        private bool _isOpenFormulaPopup;
        public bool isOpenFormulaPopup
        {
            get { return _isOpenFormulaPopup; }
            set { _isOpenFormulaPopup = value; OnPropertyChanged("isOpenFormulaPopup"); }
        }
        private NextLoadSettingsEntity _NextloadSetting = new NextLoadSettingsEntity();
        public NextLoadSettingsEntity NextloadSetting
        {
            get { return _NextloadSetting; }
            set { _NextloadSetting = value; OnPropertyChanged("NextloadSetting"); }
        }

        private string _FormulaString;
        public string FormulaString
        {
            get { return _FormulaString; }
            set { _FormulaString = value; OnPropertyChanged("FormulaString"); }
        }

        private string _FormulaParameter;
        public string FormulaParameter
        {
            get { return _FormulaParameter; }
            set { _FormulaParameter = value; OnPropertyChanged("FormulaParameter"); }
        }
        private string _SelectedParameter;
        public string SelectedParameter
        {
            get { return _SelectedParameter; }
            set { _SelectedParameter = value; OnPropertyChanged("SelectedParameter"); }
        }

        private string _UpdateResult;
        public string UpdateResult
        {
            get { return _UpdateResult; }
            set { _UpdateResult = value; OnPropertyChanged("UpdateResult"); }
        }

        private ObservableCollection<WagonLoadStringEntity> _WagonLoadstringData = new ObservableCollection<WagonLoadStringEntity>();
        public ObservableCollection<WagonLoadStringEntity> WagonLoadstringData
        {
            get { return _WagonLoadstringData; }
            set { _WagonLoadstringData = value; OnPropertyChanged("WagonLoadstringData"); }
        }
        private bool _isLoadNoValid;
        public bool isLoadNoValid
        {
            get
            {
                return _isLoadNoValid;
            }
            set
            {
                _isLoadNoValid = value;
                OnPropertyChanged("isLoadNoValid");
            }
        }
        private bool _isMMDDValid;
        public bool isMMDDValid
        {
            get
            {
                return _isMMDDValid;
            }
            set
            {
                _isMMDDValid = value;
                OnPropertyChanged("isMMDDValid");
            }
        }
        #endregion

        #region methods
        private void ClearAllNextloadSettingValues()
        {
            NextloadSetting.ParameterName = "";
            NextloadSetting.DataType = "";
            NextloadSetting.CalculationFormula = "";
            NextloadSetting.MinValue = "";
            NextloadSetting.MaxValue = "";
            NextloadSetting.isCalculationRequired = false;
            NextloadSetting.isDownloadToPlc =false;
            NextloadSetting.isInReport = false;
            NextloadSetting.isPrimaryKey =false;
            NextloadSetting.isReadOnly = false;
            NextloadSetting.ClickToAddFormula = false;
        }


        #endregion
    }
}
