using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APS.DB;
using System.Windows.Input;
using APS.UI.View;
using System.Windows;

namespace APS.UI.ViewModel
{
    class RegisterViewModel : INotifyPropertyChanged
    {

        #region Field

        private string _newName;
        private string _newLogin;
        private string _newPassword;
        private string _newConfPassword;
        private UserDAL _usDal;
        private ICommand _registerCommand;

        #endregion

        #region Constructor

        public RegisterViewModel()
        {
            this._usDal = new UserDAL();

            this._registerCommand = new RegisterCommandImpl(arg => RegisterInAPS());
        }

        #endregion

        #region Properties
        
        public string NewName
        {
            get
            {
                return _newName;
            }
            set
            {
                _newName = value;
                OnPropertyChanged("NewName");
            }
        }

        public string NewLogin
        {
            get
            {
                return _newLogin;
            }
            set
            {
                _newLogin = value;
                OnPropertyChanged("NewLogin");
            }
        }

        public string NewPassword
        {
            get
            {
                return _newPassword;
            }
            set
            {
                _newPassword = value;
                OnPropertyChanged("NewPassword");
            }
        }

        public string NewConfPassword
        {
            get
            {
                return _newConfPassword;
            }
            set
            {
                _newConfPassword = value;
                OnPropertyChanged("NewConfPassword");
            }
        }

        #endregion

        #region Commands

        public ICommand RegisterCommand
        {
            get
            {
                return _registerCommand;
            }
            set
            {
                _registerCommand = value;
                OnPropertyChanged("RegisterCommand");
            }
        }

        #endregion

        #region Methods

        private void RegisterInAPS()
        {
            if(NewName == null || NewLogin == null || NewPassword == null || NewConfPassword == null)
            {
                MessageBox.Show("Please, fill in all fields", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(NewPassword != NewConfPassword)
            {
                MessageBox.Show("Your confirm password not correct", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(!_usDal.IsUserValid(NewLogin))
            {
                MessageBox.Show("A user with this login already exists", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                _usDal.InsertUsers(new User
                {
                    Name = NewName,
                    Login = NewLogin,
                    Password = NewPassword
                });
                MessageBox.Show("Your register!", "Correctly", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion INotifyPropertyChanged Members

        #region Command Implementations

        private class RegisterCommandImpl : ICommand
        {
            public RegisterCommandImpl(Action<object> action)
            {
                ExecuteDelegate = action;
            }
            public Predicate<object> CanExecuteDelegate { get; set; }
            public Action<object> ExecuteDelegate { get; set; }
 
            public bool CanExecute(object parameter)
            {
                if (CanExecuteDelegate != null)
                {
                    return CanExecuteDelegate(parameter);
                }
                return true;
            }
            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
            public void Execute(object parameter)
            {
                if (ExecuteDelegate != null)
                {
                    ExecuteDelegate(parameter);
                }
            }
        }

        #endregion Command Implementations
    }
}
