using APS.DB;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace APS.UI.ViewModel
{
    class AddNewPartsViewModel : INotifyPropertyChanged
    {
        #region Field

        private string _partsPhoto;
        private string _newPartsPhoto;
        private string _newPartsName;
        private string _newPartsPrice;
        private string _newPartsNumber;
        private string _selectedCategory;

        private ICommand _addNewPartsCommand;
        private ICommand _getImage;

        private List<string> _newPartsCategory;

        private CategoryDAL _categoryDal;
        private AutoPartsDAL _apDal;
        private NowInfoDAL _nowInfoDal;

        #endregion

        #region Constructor

        public AddNewPartsViewModel()
        {
            this._apDal = new AutoPartsDAL();
            this._categoryDal = new CategoryDAL();
            this._newPartsCategory = new List<string>();
            this._nowInfoDal = new NowInfoDAL();

            foreach(var x in _categoryDal.SelectCategory())
            {
                _newPartsCategory.Add(x.Name.ToString());
            }

            this._addNewPartsCommand = new AddNewPartsCommandImpl(arg => DoAddNewParts());
            this._getImage = new AddNewPartsCommandImpl(arg => DoGetImage());
        }

        #endregion

        #region Properties

        public string PartsPhoto
        {
            get
            {
                return _partsPhoto;
            }
            set
            {
                _partsPhoto = value;
            }
        }

        public string NewPartsPhoto
        {
            get
            {
                return _newPartsPhoto;
            }
            set
            {
                _newPartsPhoto = value;
            }
        }

        public string NewPartsName
        {
            get
            {
                return _newPartsName;
            }
            set
            {
                _newPartsName = value;
                OnPropertyChanged("NewPartsName");
            }
        }

        public string NewPartsPrice
        {
            get
            {
                return _newPartsPrice;
            }
            set
            {
                _newPartsPrice = value;
                OnPropertyChanged("NewPartsPrice");
            }
        }

        public string NewPartsNumber
        {
            get
            {
                return _newPartsNumber;
            }
            set
            {
                _newPartsNumber = value;
                OnPropertyChanged("NewPartsNumber");
            }
        }

        public string SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }

        public List<String> NewPartsCategory
        {
            get 
            {
                return _newPartsCategory;
            }
            set
            {
                _newPartsCategory = value;
                OnPropertyChanged("NewPartsCategory");
            }
        }

        #endregion

        #region Commands

        public ICommand AddNewPartsCommand
        {
            get
            {
                return _addNewPartsCommand;
            }
            set
            {
                _addNewPartsCommand = value;
                OnPropertyChanged("ConnectCommand");
            }
        }

        public ICommand GetImage
        {
            get
            {
                return _getImage;
            }
            set
            {
                _getImage = value;
                OnPropertyChanged("GetImage");
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

        private void DoGetImage()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            ofd.Title = "Auto Parts Photo";

            Nullable<bool> result = ofd.ShowDialog();

            if(result == true)
            {
                PartsPhoto = ofd.FileName;
            }
        }

        private void DoAddNewParts()
        {
            NewPartsPhoto = @"C:\Users\Znak\Desktop\Курсач Наташі Максимюк\AutoPartsPhoto\" + NewPartsName + ".jpg";
            File.Copy(PartsPhoto, NewPartsPhoto);

            if (NewPartsPhoto == null || NewPartsName == null || NewPartsNumber == null || NewPartsPrice == null || SelectedCategory == null)
            {
                MessageBox.Show("Please, fill in all fields", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                _apDal.InsertNewParts(new AutoPart
                {
                    Photo = NewPartsPhoto,
                    Name = NewPartsName,
                    Numbers = Int32.Parse(NewPartsNumber),
                    Price = Int32.Parse(NewPartsPrice),
                    CategoryId = _categoryDal.SelectCategoryIdByName(SelectedCategory),
                    UserId = _nowInfoDal.GetIdByUser()
                });
                MessageBox.Show("Your item added!", "Correctly", MessageBoxButton.OK, MessageBoxImage.Information);
            }            
        }

        #endregion

        #region Command Implementations
            
        private class AddNewPartsCommandImpl : ICommand
        {
            public AddNewPartsCommandImpl(Action<object> action)
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
