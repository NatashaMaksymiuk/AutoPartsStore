using APS.DB;
using APS.UI.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace APS.UI.ViewModel
{
    class ConnectViewModel : INotifyPropertyChanged
    {
        #region Field

        private string _myLogin;
        private string _myPassword;
        private UserDAL _usDal;
        private NowInfoDAL _nowInfoDal;
        private ICommand _connectCommand;

        #endregion

        #region Constructor

        public ConnectViewModel()
        {
            this._usDal = new UserDAL();
            this._nowInfoDal = new NowInfoDAL();

            this._connectCommand = new ConnectCommandImpl(arg => ConnectToAPS());
        }

        #endregion

        #region Properties

        public string MyLogin
        {
            get
            {
                return _myLogin;
            }
            set
            {
                _myLogin = value;
                OnPropertyChanged("MyLogin");
            }
        }

        public string MyPassword
        {
            get
            {
                return _myPassword;
            }
            set
            {
                _myPassword = value;
                OnPropertyChanged("MyPassword");
            }
        }

        #endregion

        #region Commands

        public ICommand ConnectCommand
        {
            get
            {
                return _connectCommand;
            }
            set
            {
                _connectCommand = value;
                OnPropertyChanged("ConnectCommand");
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

        #region Methods

        private void ConnectToAPS()
        {
            if(MyLogin == null || MyPassword == null)
            {
                MessageBox.Show("Please, fill in all fields", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(_usDal.IsUserLoginValid(MyLogin, MyPassword))
            {
                _nowInfoDal.InsertNew(new NowInfo()
                {
                    UserId = _usDal.GetUserIdByLoggin(MyLogin)
                });
                MessageBox.Show("Your login valid. You are logged in!", "Correctly", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("This user does not exist", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Command Implementations
            
        private class ConnectCommandImpl : ICommand
        {
            public ConnectCommandImpl(Action<object> action)
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
