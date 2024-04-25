using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class UserMasterEntity :  IDataErrorInfo
    {
        private bool IsValidating = false;//flag to check validation only from button click
        public Dictionary<string, string> Errors = new Dictionary<string, string>();//adding error to this array

        public bool IsValidTextBoxData()
        {
            IsValidating = true;
            try
            {
                OnPropertyChanged("UserName");
                OnPropertyChanged("UserPassword");
                OnPropertyChanged("UserRole");
                OnPropertyChanged("EmailID");
                OnPropertyChanged("MobileNo");
            }
            catch (Exception ex)
            {
                IsValidating = false;
            }
            return (Errors.Count() == 0);
        }
        public bool IsValidTextBoxDataNewUser()
        {
            IsValidating = true;
            try
            {
                OnPropertyChanged("UserName");
                OnPropertyChanged("UserPassword");
                OnPropertyChanged("UserRole");
                OnPropertyChanged("EmailID");
                OnPropertyChanged("MobileNo");
            }
            finally
            {
                IsValidating = false;
            }
            return (Errors.Count() == 0);
        }
        public string this[string PropertyName]
        {
            get
            {
                string Result = string.Empty;
                Errors.Remove(PropertyName);
                if (!IsValidating) return Result;
                {
                    switch (PropertyName)
                    {
                        case "UserName":
                            Result = UserNameValidation();//validate user data
                            break;
                        case "UserPassword":
                            Result = PasswordValidation();//validate Password data
                            break;
                        case "UserRole":
                            Result = UserRollValidation();//validate access level data
                            break;
                        case "EmailID":
                            Result = EmailValidation();//validate EmailID data
                            break;
                        case "MobileNo":
                            Result = MobileNoValidation();//validate MobileNo data
                            break;
                        default:
                            break;
                    }
                }
               
                return Result;
               
            }
        }
        public string Error
        {
            get { return string.Empty; }
        }

        //validation rules
        private string UserNameValidation()
        {
            string result = null;

            if (string.IsNullOrEmpty(UserName))
            {
                result = "Please enter a user id."; Errors.Add("UserName", "0");
            }
            else if (UserName.Length > 10)
            {
                result = "The user id is too long."; Errors.Add("UserName", "0");
            }
            else if (UserName.Length < 5)
            {
                result = "The user id is too short."; Errors.Add("UserName", "0");
            }
            return result;
        }
        private string EmailValidation()
        {
            string result = null;
            if (string.IsNullOrEmpty(EmailID))
            {
                result = "Please enter an email id."; Errors.Add("EmailID", "0");
            }
            else if (!Regex.IsMatch(EmailID, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                result = "Please enter a valid email id."; Errors.Add("EmailID", "0");
            }
            return result;
        }
        private string MobileNoValidation()
        {
            string result = null;
            if (string.IsNullOrEmpty(MobileNo.ToString ()))
            {
                result = "Please enter an telephone no."; Errors.Add("MobileNo", "0");
            }
            //else if (!Regex.IsMatch(MobileNo.ToString (), @"^((\+){0,1}91(\s){0,1}(\-){0,1}(\s){0,1}){0,1}9[0-9](\s){0,1}(\-){0,1}(\s){0,1}[1-9]{1}[0-9]{7}$"))
            else if (!Regex.IsMatch(MobileNo.ToString(), @"^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}"))
            {
                result = "Please enter a valid telephone no."; Errors.Add("MobileNo", "0");
            }
            return result;
        }
        private string PasswordValidation()
        {
            string result = null;
            bool _IsPasswordValid = IsPasswordValid();
            if (string.IsNullOrEmpty(UserPassword))
            {
                result = "Please enter a password."; Errors.Add("UserPassword", "0");
            }
            else if (!_IsPasswordValid)
            {
                result = "Please enter a valid password."; Errors.Add("UserPassword", "0");
            }
            return result;
        }
        private bool IsPasswordValid()
        {
            if (UserPassword != null)
            {
                var hasNumber = new Regex(@"[0-9]+");
                var hasUpperChar = new Regex(@"[A-Z]+");
                var hasMinimum8Chars = new Regex(@".{8,}");
                var isValidated = hasNumber.IsMatch(UserPassword) && hasUpperChar.IsMatch(UserPassword) && hasMinimum8Chars.IsMatch(UserPassword);
                return isValidated;
            }
            else
            {
                return false;
            }
        }

        private string UserRollValidation()
        {
            string result = null;
            if (string.IsNullOrEmpty(UserRole))
            {
                result = "Please select a access level."; Errors.Add("UserRole", "0");
            }
            return result;
        }
    }
}
