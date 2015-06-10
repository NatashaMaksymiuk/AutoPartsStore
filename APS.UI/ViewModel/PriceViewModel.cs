using APS.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace APS.UI.ViewModel
{
    class PriceViewModel: INotifyPropertyChanged
    {
        #region Variable

        private AutoPartsDAL _autoPartDal;
        private CategoryDAL _categoryDal;
        private IList<AutoPart> _allParts;
        private AutoPart _selectedParts;

        private string _photo;
        private string _name;
        private string _price;
        private string _numbers;
        private string _category;
        private string _maxPrice;
        private string _minPrice;

        private ICommand _selectPartsFromPrice;

        #endregion

        #region Constructor

        public PriceViewModel()
        {
            _autoPartDal = new AutoPartsDAL();
            _categoryDal = new CategoryDAL();
            _allParts = new List<AutoPart>();
            _selectedParts = new AutoPart();

            this._selectPartsFromPrice = new SelectPartsFromPriceCommandImpl(arg => DoSelectPartsFromPrice());
        }

        #endregion

        #region Properties

        public IList<AutoPart> AllParts
        {
            get
            {
                return _allParts;
            }
            set
            {
                _allParts = value;
                OnPropertyChanged("AllParts");
            }
        }

        public AutoPart SelectedParts
        {
            get
            {
                return _selectedParts;
            }
            set
            {
                _selectedParts = value;
                GetOnePart();
            }
        }

        public string Photo
        {
            get
            {
                return _photo;
            }
            set
            {
                _photo = value;
                OnPropertyChanged("Photo");
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }

        public string Numbers
        {
            get
            {
                return _numbers;
            }
            set
            {
                _numbers = value;
                OnPropertyChanged("Numbers");
            }
        }

        public string Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
                OnPropertyChanged("Category");
            }
        }

        public string MinPrice
        {
            get 
            {
                return _minPrice;
            }
            set
            {
                _minPrice = value;
                OnPropertyChanged("MinPrice");
            }
        }

        public string MaxPrice
        {
            get
            {
                return _maxPrice;
            }
            set
            {
                _maxPrice = value;
                OnPropertyChanged("MaxPrice");
            }
        }

        #endregion

        #region Commands

        public ICommand SelectPartsFromPrice
        {
            get
            {
                return _selectPartsFromPrice;
            }
            set
            {
                _selectPartsFromPrice = value;
                OnPropertyChanged("SelectPartsFromPrice");
            }
        }

        #endregion

        #region Methods

        private void GetOnePart()
        {
            AutoPart ap = _autoPartDal.SelectPartsFromName(SelectedParts.Name);
            
            Photo = ap.Photo;
            Name = ap.Name;
            Numbers = ap.Numbers.ToString();
            Price = ap.Price.ToString();
            Category = _categoryDal.SelectCategoryById(ap.CategoryId);
        }

        private void DoSelectPartsFromPrice()
        {
            List<AutoPart> list = _autoPartDal.SelectPartsFromPrice(Int32.Parse(MinPrice), Int32.Parse(MaxPrice));
            AllParts = list;
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

        private class SelectPartsFromPriceCommandImpl : ICommand
        {
            public SelectPartsFromPriceCommandImpl(Action<object> action)
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
