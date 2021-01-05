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
using ConsoleApp2.db;
using DA3_ShopCake.ViewModels;

namespace DA3_ShopCake.Screens
{
    /// <summary>
    /// Interaction logic for CakeScreen.xaml
    /// </summary>
    public partial class CakeScreen : UserControl
    {
        public delegate void Detail_Delegate(string cakeCode);
        public event Detail_Delegate LearnMoreHandler;
        public event Detail_Delegate AddToOrderHandler;

        CakeScreenVM cakeVM;
        public CakeScreen(int cakeType)
        {
            InitializeComponent();
            cakeVM = new CakeScreenVM(cakeType + 1);
            DataContext = cakeVM;
            CakeListView.ItemsSource = cakeVM.cakeList;
        }

        //Test
        
        //private void GetCateName(int cakeType)
        //{
        //    switch (cakeType)
        //    {
        //        case (int)CakeType.BirthdayCake:
        //            CateName.Text = "BirthDayCake";
        //            break;

        //        case (int)CakeType.Bread:
        //            CateName.Text = "Bread";
        //            break;

        //        case (int)CakeType.SlicedBread:
        //            CateName.Text = "SlicedBread";
        //            break;

        //        case (int)CakeType.CupCake:
        //            CateName.Text = "CupCake";
        //            break;
        //        default:
        //            break;
        //    }
        //}

        private void LearnMoreButton_Click(object sender, RoutedEventArgs e)
        {
            Button sd = sender as Button;
            //ChuyenDi cdi = (ChuyenDi)sd.DataContext;
            //string maChuyenDi = cdi.MaChuyenDi;
            Cake cake = (Cake) sd.DataContext;
            if (LearnMoreHandler != null)
            {
                LearnMoreHandler(cake.Id);
            }
        }

        private void onPurchase(object sender, RoutedEventArgs e)
        {

        }

        private void AddToOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Button sd = sender as Button;
            Cake cake = (Cake)sd.DataContext;
            AddToOrderHandler?.Invoke(cake.Id);
        }
    }
}
