using APS.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.UI.ViewModel
{
    class CategoryViewModel: INotifyPropertyChanged
    {
        #region Variable

        private AutoPartsDAL _autoPartDal;
        private CategoryDAL _categoryDal;
        private IList<AutoPart> _allParts;
        private IList<string> _myCategory;
        private AutoPart _selectedParts;
        private string _selectedCategory;

        private string _photo;
        private string _name;
        private string _price;
        private string _numbers;
        private string _category;

        #endregion

        #region Constructor

        public CategoryViewModel()
        {
            _autoPartDal = new AutoPartsDAL();
            _categoryDal = new CategoryDAL();
            _allParts = new List<AutoPart>();
            _myCategory = new List<string>();
            _selectedParts = new AutoPart();

            foreach(var x in _categoryDal.SelectCategory())
            {
                MyCategory.Add(x.Name);
            }
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

        public string SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                MyAllParts();
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

        public IList<string> MyCategory
        {
            get
            {
                return _myCategory;
            }
            set
            {
                _myCategory = value;
                OnPropertyChanged("MyCategory");
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

        private void MyAllParts()
        {
            AllParts.Clear();

            AllParts = _autoPartDal.SelectPartsFromCategory(_categoryDal.SelectCategoryIdByName(SelectedCategory));        
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
    }
}
