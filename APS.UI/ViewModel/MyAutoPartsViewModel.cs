using APS.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APS.UI.ViewModel
{
    class MyAutoPartsViewModel: INotifyPropertyChanged
    {
        #region Variable

        private AutoPartsDAL _autoPartDal;
        private CategoryDAL _categoryDal;
        private NowInfoDAL _nowInfoDal;
        private IList<string> _allParts;
        private string _selectedParts;

        private string _photo;
        private string _name;
        private string _price;
        private string _numbers;
        private string _category;

        #endregion

        #region Constructor

        public MyAutoPartsViewModel()
        {
            _autoPartDal = new AutoPartsDAL();
            _categoryDal = new CategoryDAL();
            _nowInfoDal = new NowInfoDAL();
            _allParts = new List<string>();

            List<AutoPart> list = _autoPartDal.SelectPartsFromUser(_nowInfoDal.GetIdByUser());
            foreach (var x in list)
            {
                AllParts.Add(x.Name);
            }
        }

        #endregion

        #region Properties

        public IList<string> AllParts
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

        public string SelectedParts
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

        #endregion

        #region Methods
        
        private void GetOnePart()
        {
            AutoPart ap = _autoPartDal.SelectPartsFromName(SelectedParts);
            
            Photo = ap.Photo;
            Name = ap.Name;
            Numbers = ap.Numbers.ToString();
            Price = ap.Price.ToString();
            Category = _categoryDal.SelectCategoryById(ap.CategoryId);
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
