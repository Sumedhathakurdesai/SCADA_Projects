using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using IndiSCADA_ST.View;
using MahApps.Metro.Controls.Dialogs;

namespace IndiSCADA_ST.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region "Icommand"
        private readonly ICommand _AddButtonCommand;
        public ICommand AddButtonCommand
        {
            get { return _AddButtonCommand; }
        }
        private readonly ICommand _SaveButtonCommand;
        public ICommand SaveButtonCommand
        {
            get { return _SaveButtonCommand; }
        }
        private readonly ICommand _CloseButtonCommand;
        public ICommand CloseButtonCommand
        {
            get { return _CloseButtonCommand; }
        }
        private readonly ICommand _CancelButtonCommand;
        public ICommand CancelButtonCommand
        {
            get { return _CancelButtonCommand; }
        }
        private readonly ICommand _HomeButtonCommand;
        public ICommand HomeButtonCommand
        {
            get { return _HomeButtonCommand; }
        }
        private readonly ICommand _EditButtonCommand;
        public ICommand EditButtonCommand
        {
            get { return _EditButtonCommand; }
        }
        #endregion

        public BaseViewModel()
        {
            _AddButtonCommand = new RelayCommand(AddButtonCommandClick);
            _SaveButtonCommand = new RelayCommand(SaveButtonCommandClick);
            _CloseButtonCommand = new RelayCommand(CloseButtonCommandClick);
            _CancelButtonCommand = new RelayCommand(CancelButtonCommandClick);
            _HomeButtonCommand = new RelayCommand(HomeButtonCommandClick);
            _EditButtonCommand = new RelayCommand(EditButtonCommandClick);
        }

        private void AddButtonCommandClick(object obj)
        {
            AddButtonCommandClicked(obj);
        }
        private void SaveButtonCommandClick(object obj)
        {
            SaveButtonCommandClicked(obj);
        }
        private void CloseButtonCommandClick(object obj)
        {
            CloseButtonCommandClicked(obj);
        }
        private void CancelButtonCommandClick(object obj)
        {
            CancelButtonCommandClicked(obj);
        }
        private void HomeButtonCommandClick(object obj)
        {
            HomeButtonCommandClicked(obj);
        }
        private void EditButtonCommandClick(object obj)
        {
            EditButtonCommandClicked(obj);
        }

        public virtual void AddButtonCommandClicked(object obj)
        {
            //throw new NotImplementedException();
        }
        public virtual void SaveButtonCommandClicked(object obj)
        {
            //throw new NotImplementedException();
        }
        public virtual void CloseButtonCommandClicked(object obj)
        {
            //throw new NotImplementedException();
        }
        public virtual void CancelButtonCommandClicked(object obj)
        {
            //throw new NotImplementedException();
        }
        public virtual void HomeButtonCommandClicked(object obj)
        {
            //throw new NotImplementedException();
        }
        public virtual void EditButtonCommandClicked(object obj)
        {
            //throw new NotImplementedException();
        }
        
        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
        #endregion
    }
}
