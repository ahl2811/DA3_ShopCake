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
    enum CakeType {BirthdayCake, Bread, CupCake, SlicedBread}
    /// <summary>
    /// Interaction logic for CakeDetailScreen.xaml
    /// </summary>
    public partial class CakeDetailScreen : UserControl
    {
        
        public delegate void DelegateExit();
        public event DelegateExit ExitHandler;

        public delegate void DelegateUpdate(string cakeCode);
        public event DelegateUpdate UpdateHandler;

        private string cakeCode;
        public CakeDetailScreen(string cakeCode)
        {
            InitializeComponent();
            this.cakeCode = cakeCode;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            if (ExitHandler != null)
            {
                ExitHandler();
            }
        }

        private void exitDetailButton_Click(object sender, RoutedEventArgs e)
        {
            ExitHandler?.Invoke();
        }

        private void updateDetailButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateHandler?.Invoke(cakeCode);
        }
    }
}
