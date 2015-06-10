using APS.DB;
using APS.UI.View;
using APS.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace APS.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NowInfoDAL _nowInfoDal;
        private UserDAL _usDal;

        public MainWindow()
        {
            InitializeComponent();

            this._nowInfoDal = new NowInfoDAL();
            this._usDal = new UserDAL();

            if(_nowInfoDal.SelectInfo().Count > 0)
            {
                LoginName.Text = _usDal.GetUserLoginById(_nowInfoDal.GetIdByUser());
            }
            else
            {
                this.MyContent.Content = this.GetConnectToAPS();
            }
        }

        private void ConnectToAPS_Click(object sender, RoutedEventArgs e)
        {
            if (_nowInfoDal.SelectInfo().Count != 0)
            {
                LoginName.Text = _usDal.GetUserLoginById(_nowInfoDal.GetIdByUser());
                MessageBox.Show("You do not have access to this page. You are logged in.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                this.MyContent.Content = this.GetConnectToAPS();
            }
        }

        private void RegisterInAPS_Click(object sender, RoutedEventArgs e)
        {
            if (_nowInfoDal.SelectInfo().Count != 0)
            {
                LoginName.Text = _usDal.GetUserLoginById(_nowInfoDal.GetIdByUser());
                MessageBox.Show("You do not have access to this page. You are logged in.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                this.MyContent.Content = this.GetRegisterInAPS();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            _nowInfoDal.DeleteNowInfo();
            LoginName.Text = "";
            this.MyContent.Content = this.GetConnectToAPS();
        }

        private void GetAll_Click(object sender, RoutedEventArgs e)
        {
            if (_nowInfoDal.SelectInfo().Count != 0)
            {
                LoginName.Text = _usDal.GetUserLoginById(_nowInfoDal.GetIdByUser());
                this.MyContent.Content = this.GetAllParts();
            }
            else
            {
                MessageBox.Show("You do not have access to this page. You are not logged in.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);        
            }
        }

        private void AddNew_Click(object sender, RoutedEventArgs e)
        {
            if (_nowInfoDal.SelectInfo().Count != 0)
            {
                LoginName.Text = _usDal.GetUserLoginById(_nowInfoDal.GetIdByUser());
                this.MyContent.Content = this.GetAddNew();
            }
            else
            {
                MessageBox.Show("You do not have access to this page. You are not logged in.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);  
            }
        }

        private void FavCategory_Click(object sender, RoutedEventArgs e)
        {
            if (_nowInfoDal.SelectInfo().Count != 0)
            {
                LoginName.Text = _usDal.GetUserLoginById(_nowInfoDal.GetIdByUser());
                this.MyContent.Content = this.GetFavouriteCategory();
            }
            else
            {
                MessageBox.Show("You do not have access to this page. You are not logged in.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            }  
        }

        private void FavPrice_Click(object sender, RoutedEventArgs e)
        {
            if (_nowInfoDal.SelectInfo().Count != 0)
            {
                LoginName.Text = _usDal.GetUserLoginById(_nowInfoDal.GetIdByUser());
                this.MyContent.Content = GetFavouritePrice();
            }
            else
            {
                MessageBox.Show("You do not have access to this page. You are not logged in.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MyAP_Click(object sender, RoutedEventArgs e)
        {
            if (_nowInfoDal.SelectInfo().Count != 0)
            {
                LoginName.Text = _usDal.GetUserLoginById(_nowInfoDal.GetIdByUser());
                this.MyContent.Content = GetMyAP();
            }
            else
            {
                MessageBox.Show("You do not have access to this page. You are not logged in.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void LangEn_Click(object sender, RoutedEventArgs e)
        {
            this.EnMenu();
        }

        private void LangUk_Click(object sender, RoutedEventArgs e)
        {
            this.UkMenu();
        }



        private UserControl GetConnectToAPS()
        {
            ConnectViewModel connViewModel = new ConnectViewModel();
            ConnectView cv = new ConnectView();
            cv.DataContext = connViewModel;
            return cv;
        }

        private UserControl GetAddNew()
        {
            AddNewPartsViewModel anpViewModel = new AddNewPartsViewModel();
            AddNewPartsView anp = new AddNewPartsView();
            anp.DataContext = anpViewModel;
            return anp;
        }

        private UserControl GetAllParts()
        {
            AllPartsViewModel apViewModel = new AllPartsViewModel();
            AllPartsView apv = new AllPartsView();
            apv.DataContext = apViewModel;
            return apv;
        }

        private UserControl GetRegisterInAPS()
        {
            RegisterViewModel regViewModel = new RegisterViewModel();
            RegisterView rv = new RegisterView();
            rv.DataContext = regViewModel;
            return rv;
        }

        private UserControl GetFavouriteCategory()
        {
            CategoryViewModel categViewModel = new CategoryViewModel();
            CategoryView cv = new CategoryView();
            cv.DataContext = categViewModel;
            return cv;
        }

        private UserControl GetFavouritePrice()
        {
            PriceViewModel priceViewModel = new PriceViewModel();
            PriceView pv = new PriceView();
            pv.DataContext = priceViewModel;
            return pv;
        }

        private UserControl GetMyAP()
        {
            MyAutoPartsViewModel myAPViewModel = new MyAutoPartsViewModel();
            MyAutoPartsView mapv = new MyAutoPartsView();
            mapv.DataContext = myAPViewModel;
            return mapv;
        }

        private void EnMenu()
        {
            MenuFile.Header = "_File";
            ConnectToAPS.Header = "_Connect to APS";
            RegisterInAPS.Header = "_Register in APS";
            Exit.Header = "_Exit";
            MenuEdit.Header = "_Edit";
            GetAll.Header = "_Get All";
            AddNew.Header = "_Add New";
            MenuEFavor.Header = "_Favourite";
            FavCategory.Header = "_Category";
            FavPrice.Header = "_Price";
            MyAP.Header = "_My Auto Parts";
            MenuLang.Header = "_Language";
        }

        private void UkMenu()
        {
            MenuFile.Header = "_Файл";
            ConnectToAPS.Header = "_Авторизація в APS";
            RegisterInAPS.Header = "_Реєстрація в APS";
            Exit.Header = "_Вихід";
            MenuEdit.Header = "_Основні функції";
            GetAll.Header = "_Всі автозапчастини";
            AddNew.Header = "_Додати автозапчастину";
            MenuEFavor.Header = "_Вибірка";
            FavCategory.Header = "_По категорії";
            FavPrice.Header = "_По ціні";
            MyAP.Header = "_Мої автозапчастини";
            MenuLang.Header = "_Мова";
        }
    }
}
