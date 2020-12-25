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
    /// Interaction logic for NewAddingScreen.xaml
    /// </summary>
    public partial class NewAddingScreen : UserControl
    {
        public delegate void DelegateExit();
        public event DelegateExit ExitHandler;

        public event DelegateExit SubmitHandler;

        public NewAddingScreen()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            ExitHandler?.Invoke();
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            SubmitHandler?.Invoke();
        }
    }
}
