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

namespace DA3_ShopCake.Screens
{
    /// <summary>
    /// Interaction logic for CakeScreen.xaml
    /// </summary>
    public partial class CakeScreen : UserControl
    {
        public delegate void Detail_Delegate(string cakeCode);
        public event Detail_Delegate LearnMoreHandler;

        public CakeScreen(int cakeType)
        {
            InitializeComponent();
            GetCateName(cakeType);
        }

        //Test
        private void GetCateName(int cakeType)
        {
            switch (cakeType)
            {
                case (int)CakeType.BirthdayCake:
                    CateName.Text = "BirthDayCake";
                    break;

                case (int)CakeType.Bread:
                    CateName.Text = "Bread";
                    break;

                case (int)CakeType.SlicedBread:
                    CateName.Text = "SlicedBread";
                    break;

                case (int)CakeType.CupCake:
                    CateName.Text = "CupCake";
                    break;
                default:
                    break;
            }
        }
        private void ChooseItemButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ChooseItemButton_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void LearnMoreButton_Click(object sender, RoutedEventArgs e)
        {
            //Button sd = sender as Button;
            //ChuyenDi cdi = (ChuyenDi)sd.DataContext;
            //string maChuyenDi = cdi.MaChuyenDi;
            string cakeCode = "";
            if (LearnMoreHandler != null)
            {
                LearnMoreHandler(cakeCode);
            }
        }
    }
}
