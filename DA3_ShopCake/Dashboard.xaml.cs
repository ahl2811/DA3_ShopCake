using DA3_ShopCake.Screens;
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
            //CategoryList.Visibility = Visibility.Collapsed;
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
            //MainScreen.Children.Clear();
            //MainScreen.Children.Add(detailScreen);
            detailScreen.ExitHandler += DetailScreen_ExitHandler;
            detailScreen.UpdateHandler += DetailScreen_UpdateHandler;
            NavigateTo(detailScreen);
        }

        private void DetailScreen_UpdateHandler(string cakeCode)
        {
            var updateScreen = new UpdatingScreen(cakeCode);
            //MainScreen.Children.Clear();
            //MainScreen.Children.Add(updateScreen);
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
            //var detailScreen = new CakeDetailScreen(cakeType, cakeCode);
            //MainScreen.Children.Clear();
            //MainScreen.Children.Add(detailScreen);
            //detailScreen.ExitHandler += DetailScreen_ExitHandler;
            //detailScreen.UpdateHandler += DetailScreen_UpdateHandler;
            NavigateBack();
        }

        private void DetailScreen_ExitHandler()
        {
            //var screen = new CakeScreen(cakeType);
            //MainScreen.Children.Clear();
            //MainScreen.Children.Add(screen);
            //screen.LearnMoreHandler += Screen_LearnMoreHandler;
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
            //MainScreen.Children.Clear();
            //MainScreen.Children.Add(screen);
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
            //MainScreen.Children.Clear();
            //MainScreen.Children.Add(screen);
            screen.LearnMoreHandler += Screen_LearnMoreHandler;
            SetRootScreen(screen);
        }

        private void BreadButton_Click(object sender, RoutedEventArgs e)
        {
            int cakeType = (int)CakeType.Bread;
            var screen = new CakeScreen(cakeType);
            //MainScreen.Children.Clear();
            //MainScreen.Children.Add(screen);
            screen.LearnMoreHandler += Screen_LearnMoreHandler;
            SetRootScreen(screen);
        }

        private void CupCakeButton_Click(object sender, RoutedEventArgs e)
        {
            int cakeType = (int)CakeType.CupCake;
            var screen = new CakeScreen(cakeType);
            //MainScreen.Children.Clear();
            //MainScreen.Children.Add(screen);
            screen.LearnMoreHandler += Screen_LearnMoreHandler;
            SetRootScreen(screen);
        }

        private void BreadSliceButton_Click(object sender, RoutedEventArgs e)
        {
            int cakeType = (int)CakeType.SlicedBread;
            var screen = new CakeScreen(cakeType);
            //MainScreen.Children.Clear();
            //MainScreen.Children.Add(screen);
            screen.LearnMoreHandler += Screen_LearnMoreHandler;
            SetRootScreen(screen);
        }

        private void createBillButton_Click(object sender, RoutedEventArgs e)
        {
            var creatBillScreen = new CreatingBillScreen();
            //MainScreen.Children.Clear();
            //MainScreen.Children.Add(creatBillScreen);
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
