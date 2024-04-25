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
using IndiSCADAGlobalLibrary;
using System.Windows;

namespace IndiSCADA_ST.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        #region ICommand
        private readonly ICommand _OkButtonCommand;
        public ICommand OkButtonCommand
        {
            get { return _OkButtonCommand; }
        }
        private readonly ICommand _Exit;
        public ICommand Exit
        {
            get { return _Exit; }
        }
        private DelegateCommand _LoginCommand;
        public DelegateCommand LoginCommand => _LoginCommand ?? (_LoginCommand = new DelegateCommand(LoginButtonClick, CanLoginUser));


        #endregion

        #region Public/Private Property
        public interface IHavePassword
        {
            System.Security.SecureString Password { get; }
        }
        
        private UserMasterEntity _UserMaster =new UserMasterEntity();
        public UserMasterEntity UserMaster
        {
            get { return _UserMaster; }
            set { _UserMaster = value; OnPropertyChanged("UserMaster"); }
        }
        private string _messageName = "";
        public string MessageName
        {
            get { return _messageName; }
            set { _messageName = value; OnPropertyChanged("MessageName"); }
        }
        #endregion

        #region Constructor
        public LoginViewModel()
        {
            try
            {
                _Exit = new RelayCommand(ExitButtonCommandClick);
                //_OkButtonCommand = new RelayCommand(OkButtonCommandClick);
                UserMaster.PropertyChanged += UserMaster_PropertyChanged;
            }
            catch (Exception exLoginViewModel)
            {
                ErrorLogger.LogError.ErrorLog("LoginViewModel()", DateTime.Now.ToString(), exLoginViewModel.Message, "No",true);
            }
        }
        #endregion

        #region Public/Private Method

        private bool CanLoginUser(Object obj)
        {
            return true;
        }

        private void LoginButtonClick(object LogginCommandParameter)
        {
            try
            {
                MessageName = "";
                
                bool _isvalidTextData = UserMaster.IsValidTextBoxData();
                if (_isvalidTextData)
                {
                    ServiceResponse<string> result= IndiSCADABusinessLogic.LoginLogic.LoginUser(UserMaster);
                    if (result != null)
                    {
                        if (result.Response != null)
                        {
                            if (result.Response.Contains("1"))
                            {
                                MessageName = "Login Successfull";
                                UserActivityInsert(MessageName);//user activity
                                if (LogginCommandParameter != null)
                                {
                                    IndiSCADAGlobalLibrary.UserLoginDetails.UserName = UserMaster.UserName;
                                    IndiSCADAGlobalLibrary.UserLoginDetails.AccessLevel = UserMaster.UserRole;
                                    Window ObjLogin = (Window)LogginCommandParameter;
                                    ObjLogin.Close();
                                }
                            }
                            else
                            {
                                MessageName = "Login falied.";
                                UserActivityInsert(MessageName);//user activity
                            }
                        }
                        else
                        {
                            MessageName = "Login falied.";
                            UserActivityInsert(MessageName);//user activity
                        }
                    }
                    else
                    {
                        MessageName = "Login falied.";
                        UserActivityInsert(MessageName);//user activity
                    }
                }


            }
            catch (Exception exLoginButtonCommandClick)
            {
                ErrorLogger.LogError.ErrorLog("LoginViewModel LoginButtonClick()", DateTime.Now.ToString(), exLoginButtonCommandClick.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        private void UserMaster_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //If a property of our new Customer changed, we want to check the CanExecute of the Add button
            LoginCommand.CanExecute(null);
        }
        public override void AddButtonCommandClicked(object obj)
        {

        }
        private void UserActivityInsert(string Activity)
        {
            try
            {
                UserActivityEntity _insert = new UserActivityEntity();
                _insert.DateTimeCol = DateTime.Now;
                _insert.Activity = Activity;
                if (IndiSCADAGlobalLibrary.UserLoginDetails.UserName == null)
                {
                    _insert.UserName = "System";
                }
                else
                {
                    _insert.UserName = IndiSCADAGlobalLibrary.UserLoginDetails.UserName;
                }
                IndiSCADABusinessLogic.UserActivityLogic.InsertUserActivity(_insert);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("LoginViewModel UserActivityInsert()", DateTime.Now.ToString(), ex.Message, "No", IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
        
        private void ExitButtonCommandClick(object _commandparameters)
        {
            try
            {
               

            }
            catch (Exception exExitButtonCommandClick)
            {
                //ErrorLogger.LogError.ErrorLog("ExitButtonCommandClick()", DateTime.Now.ToString(), exExitButtonCommandClick.Message, "No", 1);
            }
        }
        #endregion
    }
}
