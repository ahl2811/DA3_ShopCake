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
    /// Interaction logic for UpdatingScreen.xaml
    /// </summary>
    public partial class UpdatingScreen : UserControl
    {
        public delegate void DelegateExit();
        public event DelegateExit ExitHandler;

        public delegate void DelegateSubmit(string cakeCode);
        public event DelegateSubmit SubmitHandler;

        private string cakeCode;
        public UpdatingScreen(string cakeCode)
        {
            InitializeComponent();
            this.cakeCode = cakeCode;
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            SubmitHandler?.Invoke(cakeCode);
        }

        private void exitUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            ExitHandler?.Invoke();
        }
    }
}
