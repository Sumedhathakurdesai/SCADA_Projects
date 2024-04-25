using IndiSCADAEntity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IndiSCADA_ST.ViewModel
{
    public class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
            try
            {
                UserActivityEntity InsertUserActivity = new UserActivityEntity();
                InsertUserActivity.DateTimeCol = DateTime.Now;
                InsertUserActivity.Activity = this.execute.Method.ReflectedType.FullName.ToString()+"-"+this.execute.Method.Name.ToString();
                if (IndiSCADAGlobalLibrary.UserLoginDetails.UserName == null)
                {
                    InsertUserActivity.UserName = "System";
                }
                else
                {
                    InsertUserActivity.UserName = IndiSCADAGlobalLibrary.UserLoginDetails.UserName;
                }
                IndiSCADABusinessLogic.UserActivityLogic.InsertUserActivity(InsertUserActivity);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError.ErrorLog("RelayCommand Execute()", DateTime.Now.ToString(), ex.Message, null, IndiSCADAGlobalLibrary.ErrorLogEnable.IsErrorLogEnable);
            }
        }
    }

}
