using IndiSCADAEntity.Entity;
using IndiSCADAGlobalLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace IndiSCADA_ST.ViewModel
{
    public class NewUserViewModel : BaseViewModel
    {
        #region ICommand
        private readonly ICommand _AddButtonCommand;
        public ICommand AddButtonCommand
        {
            get { return _AddButtonCommand; }
        }
        private readonly ICommand _ResetButtonCommand;
        public ICommand ResetButtonCommand
        {
            get { return _ResetButtonCommand; }
        }
        private readonly ICommand _UpdateButtonCommand;
        public ICommand UpdateButtonCommand
        {
            get { return _UpdateButtonCommand; }
        }
        private readonly ICommand _DeleteButtonCommand;
        public ICommand DeleteButtonCommand
        {
            get { return _DeleteButtonCommand; }
        }
        private readonly ICommand _Exit;
        public ICommand Exit
        {
            get { return _Exit; }
        }
        #endregion
        #region Constructor
        public NewUserViewModel()
        {
            try
            {
                _Exit = new RelayCommand(ExitButtonCommandClick);
                _AddButtonCommand = new RelayCommand(AddButtonCommandClicked);
                _UpdateButtonCommand = new RelayCommand(UpdateButtonCommandClick);
                _DeleteButtonCommand = new RelayCommand(DeleteButtonCommandClick);
                _ResetButtonCommand = new RelayCommand(ResetButtonCommandClick);
                RefreshGridView();//assign data to grid view
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("NewUserViewModel()", DateTime.Now.ToString(), ex.Message, "No",true);
            }
        }
        #endregion
        #region Public/Private Property
        private UserMasterEntity _UserMaster = new UserMasterEntity();
        public UserMasterEntity UserMaster
        {
            get { return _UserMaster; }
            set { _UserMaster = value; MessageName = ""; OnPropertyChanged("UserMaster"); }
        }
        private string _messageName = "";
        public string MessageName
        {
            get { return _messageName; }
            set { _messageName = value; OnPropertyChanged("MessageName"); }
        }
        private string _UpdatemessageName = "";
        public string UpdatemessageName
        {
            get { return _UpdatemessageName; }
            set { _UpdatemessageName = value; OnPropertyChanged("UpdatemessageName"); }
        }
        private string _DeletemessageName = "";
        public string DeletemessageName
        {
            get { return _DeletemessageName; }
            set { _DeletemessageName = value; OnPropertyChanged("DeletemessageName"); }
        }
        private ObservableCollection<UserMasterEntity> _UsarDataTable;
        public ObservableCollection<UserMasterEntity> UsarDataTable
        {
            get { return _UsarDataTable; }
            set { _UsarDataTable = value; OnPropertyChanged("UsarDataTable"); }
        }
        #endregion
        #region Public/Private Method
        private void ResetButtonCommandClick(object _commandparameters)
        {
            try
            {
                UserMasterEntity _userentity = new UserMasterEntity();
                _userentity.UserPassword = "";
                _userentity.UserName = "";
                _userentity.MobileNo = "";
                _userentity.UserRole = "";
                _userentity.EmailID = "";
                UserMaster = _userentity;
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("NewUserViewModel ResetButtonCommandClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        public override void AddButtonCommandClicked(object LogginCommandParameter)
        {
            try
            {
                MessageName = "";
                if (LogginCommandParameter != null)
                {
                    var LoginPasswordBox = LogginCommandParameter as PasswordBox;
                    UserMaster.UserPassword = LoginPasswordBox.Password.ToString();
                }
                bool _isvalidTextData = UserMaster.IsValidTextBoxData();
                if (_isvalidTextData)
                {
                    ServiceResponse<int> _result = IndiSCADABusinessLogic.UserMasterLogic.InsertUserMasterData(UserMaster);
                    if (_result.Status ==  ResponseType.S)
                    {
                        MessageName = "Successfully inserted data.";
                        RefreshGridView();//assign data to grid view
                    }
                    else
                    {
                        MessageName = "Failed to inserted data.";
                    }
                }
               
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("NewUserViewModel AddButtonCommandClicked ()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        private void UpdateButtonCommandClick(object obj)
        {
            try
            {
                MessageName = "";
                if (obj != null)
                {
                    var LoginPasswordBox = obj as PasswordBox;
                    UserMaster.UserPassword = LoginPasswordBox.Password.ToString();
                }
                bool _isvalidTextData = UserMaster.IsValidTextBoxData();
                if (_isvalidTextData)
                {
                    ServiceResponse<int> _result = IndiSCADABusinessLogic.UserMasterLogic.UpdateUserMasterData(UserMaster);
                    if (_result.Status == ResponseType.S)
                    {
                        MessageName = "Successfully updated data.";
                        RefreshGridView();//assign data to grid view
                    }
                    else
                    {
                        MessageName = "Failed to update data.";
                    }
                }

            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("NewUserViewModel UpdateButtonCommandClick" , DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        private void DeleteButtonCommandClick(object obj)
        {
            try
            {
                MessageName = "";
                if (obj != null)
                {
                    var LoginPasswordBox = obj as PasswordBox;
                    UserMaster.UserPassword = LoginPasswordBox.Password.ToString();
                    RefreshGridView();//assign data to grid view
                }
                bool _isvalidTextData = UserMaster.IsValidTextBoxData();
                if (_isvalidTextData)
                {
                    ServiceResponse<int> _result = IndiSCADABusinessLogic.UserMasterLogic.DeleteUserMasterData(UserMaster);
                    if (_result.Status == ResponseType.S)
                    {
                        MessageName = "Successfully deleted data.";
                        RefreshGridView();//assign data to grid view
                    }
                    else
                    {
                        MessageName = "Failed to delete data.";
                    }
                }

            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("NewUserViewModel DeleteButtonCommandClick ()()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        private void ExitButtonCommandClick(object _commandparameters)
        {
            try
            {
                

            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("NewUserViewModel ExitButtonCommandClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        private void RefreshGridView()
        {
            try
            {
                UsarDataTable = IndiSCADABusinessLogic.UserMasterLogic.SelectUserMasterData();
            }
            catch (Exception exExitButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("NewUserViewModel RefreshGridView()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        #endregion
    }
}
