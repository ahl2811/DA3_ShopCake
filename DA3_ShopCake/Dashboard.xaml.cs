using ConsoleApp2.db;
using DA3_ShopCake.Screens;
using DA3_ShopCake.utils;
using dbforproject3.db.DbHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DA3_ShopCake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class Dashboard : Window
    {
        private readonly Stack<UserControl> ScreenStack = new Stack<UserControl>();
        private List<KeyValuePair<String, int>> numPurchases;
        private ObservableCollection<Cake> selectedCakes;
        private CakeDaoImp cakeDao;
        public Dashboard()
        {
            InitializeComponent();
            showDefaultScreen();
            createDatabaseIfNotExist();
            rightTapDefaultWidth = rightTab.Width;

            cakeDao = new CakeDaoImp();
            numPurchases = new List<KeyValuePair<string, int>>();
            selectedCakes = new ObservableCollection<Cake>();

            //following code for testing function
            //foreach (Cake cake in cakeDao.GetCakes())
            //{
            //    selectedCakes.Add(cake);
            //    numPurchases.Add(new KeyValuePair<string, int>(cake.Id, 1));
            //}

            lbSelectedPurchases.ItemsSource = selectedCakes;
            updateTmpCostTextBlock();
            updateLastCostTextBlock();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
        }

        private void OnIncreasingItemCount(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel stackPanel = (StackPanel)button.Parent;
            Grid grid = (Grid)stackPanel.Parent;

            Grid firstItem = grid.Children.OfType<Grid>().FirstOrDefault();

            TextBox currentCount = firstItem.Children.OfType<TextBox>().FirstOrDefault();

            int currentCountInt = Int32.Parse(currentCount.Text);
            currentCountInt += 1;
            currentCount.Text = currentCountInt.ToString();

            String cakeId = button.Tag.ToString();
            changeSelectedItemNumInList(cakeId, currentCountInt);
            updateTmpCostTextBlock();
            updateLastCostTextBlock();

            Grid container = (Grid)grid.Parent;
            Grid priceContainer = (Grid)container.Children[3];

            TextBlock itemCountPrice = priceContainer.Children.OfType<TextBlock>().FirstOrDefault();
            Cake cake = cakeDao.getCakeById(cakeId);
            itemCountPrice.Text = (currentCountInt * cake.Price).ToString();
        }

        private void changeSelectedItemNumInList(String itemId, int newValue)
        {
            foreach (KeyValuePair<String, int> selectedItem in numPurchases)
            {
                if (selectedItem.Key.Equals(itemId))
                {
                    KeyValuePair<String, int> newKeyValue = new KeyValuePair<string, int>(itemId, newValue);
                    numPurchases[numPurchases.IndexOf(selectedItem)] = newKeyValue;
                    break;
                }
            }
        }

        private void OnDecreasingItemCount(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel stackPanel = (StackPanel)button.Parent;
            Grid grid = (Grid)stackPanel.Parent;

            Grid firstItem = grid.Children.OfType<Grid>().FirstOrDefault();

            TextBox currentCount = firstItem.Children.OfType<TextBox>().FirstOrDefault();

            int currentCountInt = Int32.Parse(currentCount.Text);

            if (currentCountInt == 0)
            {
                return;
            }
            else
            {
                currentCountInt -= 1;
                currentCount.Text = currentCountInt.ToString();

                String cakeId = button.Tag.ToString();
                changeSelectedItemNumInList(cakeId, currentCountInt);
                updateTmpCostTextBlock();
                updateLastCostTextBlock();

                Grid container = (Grid)grid.Parent;
                Grid priceContainer = (Grid)container.Children[3];

                TextBlock itemCountPrice = priceContainer.Children.OfType<TextBlock>().FirstOrDefault();
                Cake cake = cakeDao.getCakeById(cakeId);
                itemCountPrice.Text = (currentCountInt * cake.Price).ToString();
            }

        }

        private void OnRemoveItem(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            String itemId = button.Tag.ToString();

            removeItemInSelectedListBox(itemId);
            removeItemInSeletedList(itemId);
            updateTmpCostTextBlock();
            updateLastCostTextBlock();
        }

        private void removeItemInSeletedList(String itemId)
        {
            foreach (KeyValuePair<String, int> selectedItem in numPurchases)
            {
                if (selectedItem.Key.Equals(itemId))
                {
                    numPurchases.Remove(selectedItem);
                    break;
                }
            }
        }

        private void removeItemInSelectedListBox(String itemId)
        {
            foreach (Cake cake in selectedCakes)
            {
                if (cake.Id.Equals(itemId))
                {
                    selectedCakes.Remove(cake);
                    break;
                }
            }
        }

        private void createDatabaseIfNotExist()
        {

            DatabaseHelper.Database = "master";
            String db = "QLTiemBanh";
            String query = "";
            String con = $"Server=localhost; Database= master; Trusted_Connection=True;";
            bool isCreated = DatabaseHelper.isDatabaseExists(con, db);

            if (!isCreated)
            {
                query = "create database QLTiemBanh";
                DatabaseHelper.executeQuery(query);

                DatabaseHelper.Database = db;

                query = "use QLTiemBanh";
                DatabaseHelper.executeQuery(query);

                createDefaultTable();
                addForeignKey();
                insertDefaultDatabase();
            }

            DatabaseHelper.Database = db;
        }

        private void insertDefaultDatabase()
        {
            CatalogueDaoImp catalogueDao = new CatalogueDaoImp();
            catalogueDao.insertCatalogue(new Catalogue("1", "Bánh sinh nhật"));
            catalogueDao.insertCatalogue(new Catalogue("2", "Bánh mì"));
            catalogueDao.insertCatalogue(new Catalogue("3", "Bánh cupcake"));
            catalogueDao.insertCatalogue(new Catalogue("4", "Bánh sandwich"));
        }

        private void createDefaultTable()
        {
            String query = "";

            query = "create table CATALOGUE(" +
                                "ID varchar(6)," +
                                "CATALOGUE_NAME nvarchar(50)," +
                                "primary key(ID))";
            DatabaseHelper.executeQuery(query);

            query = "create table CAKE(" +
                "CAKE_ID varchar(6)," +
                "CATALOGUE_ID varchar(6)," +
                "CAKE_NAME nvarchar(100)," +
                "PRICE int," +
                "DESCRIPTION nvarchar(200)," +
                "primary key(CAKE_ID))";
            DatabaseHelper.executeQuery(query);

            query = "create table BILL(" +
                "BILL_ID varchar(6)," +
                "CUSTOMER_ID varchar(6)," +
                "SALE_DATE varchar(200)," +
                "primary key(BILL_ID))";
            DatabaseHelper.executeQuery(query);

            query = "create table DETAIL_BILL(" +
                "BILL_ID varchar(6)," +
                "CAKE_ID varchar(6)," +
                "COUNT int," +
                "primary key(BILL_ID, CAKE_ID))";
            DatabaseHelper.executeQuery(query);

            query = "create table CUSTOMER(" +
                "CUSTOMER_ID varchar(6)," +
                "CUSTOMER_NAME nvarchar(200)," +
                "PHONE varchar(11)," +
                "primary key(CUSTOMER_ID))";
            DatabaseHelper.executeQuery(query);
            /*CAKEIMAGE: CAKE_ID (PK), IMAGE(PK)*/

            query = "create table CAKEIMAGE(" +
                "CAKE_ID varchar(6)," +
                "IMAGE varchar(200)," +
                "primary key(CAKE_ID, IMAGE))";
            DatabaseHelper.executeQuery(query);
        }

        /*
         * QLTiemBanh
         * CATALOGUE: ID (PK), CATALOGUE _NAME (NVARCHAR(50))
        CAKE: CAKE_ID (PK), CATALOGUE_ID (FK), CAKE_NAME, PRICE (INT), DESCRIPTION (TEXT)
        BILL: BILL_ID(PK), CUSTOMER_ID (FK), SALE_DATE
        DETAIL_BILL: BILL_ID(FK), CAKE_ID (FK), COUNT
        CUSTOMER: CUSTOMER_ID (PK), CUSTOMER_NAME, PHONE
        CAKEIMAGE: CAKE_ID (PK, FK), IMAGE(PK)

        */
        private void addForeignKey()
        {
            String query = "";

            query = "alter table CAKE add constraint fk_catalogue foreign key(CATALOGUE_ID) references CATALOGUE(ID)";
            DatabaseHelper.executeQuery(query);

            query = "alter table BILL add constraint fk_customer_id foreign key(CUSTOMER_ID) references CUSTOMER(CUSTOMER_ID)";
            DatabaseHelper.executeQuery(query);

            query = "alter table DETAIL_BILL add constraint fk_bill_id foreign key(BILL_ID) references BILL(BILL_ID)";
            DatabaseHelper.executeQuery(query);

            query = "alter table DETAIL_BILL add constraint fk_cake_id foreign key(CAKE_ID) references CAKE(CAKE_ID)";
            DatabaseHelper.executeQuery(query);
            
            query = "alter table CAKEIMAGE add constraint fk_cake_id_2 foreign key(CAKE_ID) references CAKE(CAKE_ID)";
            DatabaseHelper.executeQuery(query);
        }

        private void SetRootScreen(UserControl screen)
        {
            if (ScreenStack.Count >= 1)
            {
                ScreenStack.Clear();
            }

            ScreenStack.Push(screen);
            MainScreen.Children.Clear();
            MainScreen.Children.Add(screen);
        }

        private void NavigateTo(UserControl screen)
        {

            ScreenStack.Push(screen);
            MainScreen.Children.Clear();
            MainScreen.Children.Add(screen);
        }

        private bool NavigateBackAndUpdateData(UserControl screen)
        {
            if (ScreenStack.Count <= 1)
            {
                return false;
            }
            ScreenStack.Pop();
            ScreenStack.Pop();
            NavigateTo(screen);
            HiddenOrderScreen();
            return true;
        }

        private bool NavigateBack()
        {
            rightTab.Visibility = Visibility.Visible;

            if (ScreenStack.Count <= 1)
            {
                return false;
            }
            ScreenStack.Pop();
            var screen = ScreenStack.Peek();
            MainScreen.Children.Clear();
            MainScreen.Children.Add(screen);
            return true;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
                rightTapDefaultWidth = rightTab.Width;
            }
            else
            {
                this.WindowState = WindowState.Normal;
                rightTapDefaultWidth = rightTab.Width;
            }
        }

        private void showDefaultScreen()
        {
            HomeButton.IsChecked = true;
            CategoryList.Visibility = Visibility.Visible;
            var screen = new CakeScreen((int)CakeType.BirthdayCake);
            screen.LearnMoreHandler += Screen_LearnMoreHandler;
            screen.AddToOrderHandler += Screen_AddToOrderHandler;
            ShowOrderScreen();
            SetRootScreen(screen);
        }

        private void Screen_AddToOrderHandler(string cakeCode)
        {
            CakeDaoImp cakeDao = new CakeDaoImp();
            Cake cake = cakeDao.getCakeById(cakeCode);
            AddToOrderList(cake);
        }

        private void AddToOrderList(Cake cake)
        {
            foreach(Cake c in selectedCakes)
            {
                if(c.Id == cake.Id)
                {
                    MessageBox.Show("Sản phẩm đã tồn tại trong giỏ hàng");
                    return;
                }
            }
            selectedCakes.Add(cake);
            numPurchases.Add(new KeyValuePair<string, int>(cake.Id, 1));
            updateTmpCostTextBlock();
            updateLastCostTextBlock();
        }
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            keywordPlaceholderTextBlock.Visibility = Visibility.Hidden;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (keywordTextBox.Text.Length == 0)
            {
                keywordPlaceholderTextBlock.Visibility = Visibility.Visible;
            }
        }

        double rightTapDefaultWidth;

        private void HiddenOrderScreen()
        {
            rightTab.Visibility = Visibility.Collapsed;
        }

        private void ShowOrderScreen()
        {
            rightTab.Visibility = Visibility.Visible;
        }
        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryList.Visibility = Visibility.Hidden;
            var addNewScreen = new NewAddingScreen();
            addNewScreen.SubmitHandler += AddNewScreen_SubmitHandler;
            addNewScreen.ExitHandler += AddNewScreen_ExitHandler;
            HiddenOrderScreen();
            SetRootScreen(addNewScreen);
        }

        private void AddNewScreen_ExitHandler()
        {
            ShowOrderScreen();
            NavigateBack();
        }

        private void AddNewScreen_SubmitHandler()
        {
            ShowOrderScreen();
            showDefaultScreen();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            showDefaultScreen();
        }

        private void Screen_LearnMoreHandler(string cakeCode)
        {
            Cake cake = cakeDao.getCakeById(cakeCode);

            if(cake == null)
            {
                MessageBox.Show("Mã sản phẩm không hợp lệ");
                return;
            }

            var detailScreen = new CakeDetailScreen(cake.Id);

            detailScreen.ExitHandler += DetailScreen_ExitHandler;
            detailScreen.UpdateHandler += DetailScreen_UpdateHandler;
            HiddenOrderScreen();
            NavigateTo(detailScreen);
        }

        private void DetailScreen_UpdateHandler(string cakeCode)
        {
            var updateScreen = new UpdatingScreen(cakeCode);
            updateScreen.ExitHandler += UpdateScreen_ExitHandler;
            updateScreen.SubmitHandler += UpdateScreen_SubmitHandler;
            NavigateTo(updateScreen);
        }

        private void UpdateScreen_SubmitHandler(string cakeCode)
        {
            var detailScreen = new CakeDetailScreen(cakeCode);
            detailScreen.ExitHandler += DetailScreen_ExitHandler;
            detailScreen.UpdateHandler += DetailScreen_UpdateHandler;
            
            NavigateBackAndUpdateData(detailScreen);
        }

        private void UpdateScreen_ExitHandler()
        {
            NavigateBack();
        }

        private void DetailScreen_ExitHandler()
        {
            ShowOrderScreen();
            NavigateBack();
        }

        private void HistoryOrderButton_Click(object sender, RoutedEventArgs e)
        {
            ShowHistoryScreen();
        }

        private void ShowHistoryScreen()
        {
            CategoryList.Visibility = Visibility.Collapsed;
            var screen = new HistoryScreen();
            HiddenOrderScreen();
            SetRootScreen(screen);
        }
        private void StatisticButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryList.Visibility = Visibility.Collapsed;
            var screen = new StatisticScreen();
            HiddenOrderScreen();
            SetRootScreen(screen);
        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryList.Visibility = Visibility.Collapsed;
        }

        private void SupportButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryList.Visibility = Visibility.Collapsed;
        }

        private void RowDefinition_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BirthdayCakeButton_Click(object sender, RoutedEventArgs e)
        {
            int cakeType = (int)CakeType.BirthdayCake;
            var screen = new CakeScreen(cakeType);
            screen.LearnMoreHandler += Screen_LearnMoreHandler;
            screen.AddToOrderHandler += Screen_AddToOrderHandler;
            ShowOrderScreen();
            SetRootScreen(screen);
        }

        private void BreadButton_Click(object sender, RoutedEventArgs e)
        {
            int cakeType = (int)CakeType.Bread;
            var screen = new CakeScreen(cakeType);
            screen.LearnMoreHandler += Screen_LearnMoreHandler;
            screen.AddToOrderHandler += Screen_AddToOrderHandler;
            ShowOrderScreen();
            SetRootScreen(screen);
        }

        private void CupCakeButton_Click(object sender, RoutedEventArgs e)
        {
            int cakeType = (int)CakeType.CupCake;
            var screen = new CakeScreen(cakeType);
            screen.LearnMoreHandler += Screen_LearnMoreHandler;
            screen.AddToOrderHandler += Screen_AddToOrderHandler;
            ShowOrderScreen();
            SetRootScreen(screen);
        }

        private void BreadSliceButton_Click(object sender, RoutedEventArgs e)
        {
            int cakeType = (int)CakeType.SlicedBread;
            var screen = new CakeScreen(cakeType);
            screen.LearnMoreHandler += Screen_LearnMoreHandler;
            screen.AddToOrderHandler += Screen_AddToOrderHandler;
            ShowOrderScreen();
            SetRootScreen(screen);
        }

        private void createBillButton_Click(object sender, RoutedEventArgs e)
        {
            if(numPurchases.Count() == 0)
            {
                MessageBox.Show("Giỏ hàng rỗng");
                return;
            }

            var creatBillScreen = new CreatingBillScreen(numPurchases, Double.Parse(txtLastCost.Text));
            creatBillScreen.ExitHandler += CreatBillScreen_ExitHandler;
            creatBillScreen.SubmitHandler += CreatBillScreen_SubmitHandler;
            HiddenOrderScreen();
            NavigateTo(creatBillScreen);
        }

        private void CreatBillScreen_SubmitHandler()
        {
            CategoryList.Visibility = Visibility.Hidden;
            HistoryOrderButton.IsChecked = true;
          
            ShowHistoryScreen();
        }

        private void CreatBillScreen_ExitHandler()
        {
            NavigateBack();
        }

        private void RefreshBillButton_Click(object sender, RoutedEventArgs e)
        {
            selectedCakes.Clear();
            numPurchases.Clear();
            updateTmpCostTextBlock();
            updateLastCostTextBlock();
        }

        private int calCulateTmpCost()
        {
            List<KeyValuePair<int, int>> serialNums = getSerialNums();
            return Calculation.sumOfSerialNumbers(serialNums);
        }

        private void updateTmpCostTextBlock()
        {
            int tmpCost = calCulateTmpCost();
            txtTmpCost.Text = tmpCost.ToString();
        }  
        
        private void updateLastCostTextBlock()
        {
            int tmpCost = calCulateTmpCost();
            double lastCost = tmpCost;

            if(cbDiscount.IsChecked == true)
            {
                lastCost = ((double)tmpCost) - (Calculation.divide(tmpCost, 10));
            }
            txtLastCost.Text = lastCost.ToString();
        }


        private List<KeyValuePair<int, int>> getSerialNums()
        {
            List<KeyValuePair<int, int>> serialNums = new List<KeyValuePair<int, int>>();
            foreach (KeyValuePair<String, int> item in numPurchases)
            {
                Cake cake = cakeDao.getCakeById(item.Key);
                serialNums.Add(new KeyValuePair<int, int>(cake.Price, item.Value));
            }

            return serialNums;
        }

        private void OnDiscountChange(object sender, RoutedEventArgs e)
        {
            updateLastCostTextBlock();
        }
    }
}
