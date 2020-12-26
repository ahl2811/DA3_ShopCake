using DA3_ShopCake.Screens;
using dbforproject3.db.DbHelper;
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

namespace DA3_ShopCake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class Dashboard : Window
    {
        private readonly Stack<UserControl> ScreenStack = new Stack<UserControl>();
        public Dashboard()
        {
            InitializeComponent();
            showDefaultScreen();
            createDatabaseIfNotExist();
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
            ;
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
                "IMAGE varchar(200)," +
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
        }

        /*
         * QLTiemBanh
         * CATALOGUE: ID (PK), CATALOGUE _NAME (NVARCHAR(50))
        CAKE: CAKE_ID (PK), CATALOGUE_ID (FK), CAKE_NAME, PRICE (INT), IMAGE (TEXT)
        BILL: BILL_ID(PK), CUSTOMER_ID (FK), SALE_DATE
        DETAIL_BILL: BILL_ID(FK), CAKE_ID (FK), COUNT
        CUSTOMER: CUSTOMER_ID (PK), CUSTOMER_NAME, PHONE
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
            return true;
        }

        private bool NavigateBack()
        {
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

            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void showDefaultScreen()
        {
            HomeButton.IsChecked = true;
            CategoryList.Visibility = Visibility.Visible;
            var screen = new CakeScreen((int)CakeType.BirthdayCake);
            screen.LearnMoreHandler += Screen_LearnMoreHandler;
            SetRootScreen(screen);
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

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            var addNewScreen = new NewAddingScreen();
            addNewScreen.SubmitHandler += AddNewScreen_SubmitHandler;
            addNewScreen.ExitHandler += AddNewScreen_ExitHandler;
            NavigateTo(addNewScreen);
        }

        private void AddNewScreen_ExitHandler()
        {
            NavigateBack();
        }

        private void AddNewScreen_SubmitHandler()
        {
            showDefaultScreen();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            showDefaultScreen();
        }

        private void Screen_LearnMoreHandler(string cakeCode)
        {
            var detailScreen = new CakeDetailScreen(cakeCode);
            detailScreen.ExitHandler += DetailScreen_ExitHandler;
            detailScreen.UpdateHandler += DetailScreen_UpdateHandler;
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
            SetRootScreen(screen);
        }
        private void StatisticButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryList.Visibility = Visibility.Collapsed;
            var screen = new StatisticScreen();
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
            SetRootScreen(screen);
        }

        private void BreadButton_Click(object sender, RoutedEventArgs e)
        {
            int cakeType = (int)CakeType.Bread;
            var screen = new CakeScreen(cakeType);
            screen.LearnMoreHandler += Screen_LearnMoreHandler;
            SetRootScreen(screen);
        }

        private void CupCakeButton_Click(object sender, RoutedEventArgs e)
        {
            int cakeType = (int)CakeType.CupCake;
            var screen = new CakeScreen(cakeType);
            screen.LearnMoreHandler += Screen_LearnMoreHandler;
            SetRootScreen(screen);
        }

        private void BreadSliceButton_Click(object sender, RoutedEventArgs e)
        {
            int cakeType = (int)CakeType.SlicedBread;
            var screen = new CakeScreen(cakeType);
            screen.LearnMoreHandler += Screen_LearnMoreHandler;
            SetRootScreen(screen);
        }

        private void createBillButton_Click(object sender, RoutedEventArgs e)
        {
            var creatBillScreen = new CreatingBillScreen();
            creatBillScreen.ExitHandler += CreatBillScreen_ExitHandler;
            creatBillScreen.SubmitHandler += CreatBillScreen_SubmitHandler;
            NavigateTo(creatBillScreen);
        }

        private void CreatBillScreen_SubmitHandler()
        {
            HistoryOrderButton.IsChecked = true;
            ShowHistoryScreen();
        }

        private void CreatBillScreen_ExitHandler()
        {
            NavigateBack();
        }
    }
}
