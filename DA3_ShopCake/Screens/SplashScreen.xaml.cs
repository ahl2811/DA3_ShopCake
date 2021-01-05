using dbforproject3.db.DbHelper;
using ConsoleApp2.db;
using DA3_ShopCake.db;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace DA3_ShopCake.Screens
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var value = ConfigurationManager.AppSettings["ShowSplashScreen"];
            value = "true";
            var showSplash = bool.Parse(value);

            if (showSplash == false)
            {
                this.Hide();
                var screen = new Dashboard();
                screen.Show();
                this.Close();
            }
            else
            {
                LoadUI();
                timer = new System.Timers.Timer();
                timer.Elapsed += Timer_Elapsed;
                timer.Interval = 1000;
                timer.Start();
            }
        }

        private void LoadUI()
        {
            String db = "QLTiemBanh";
            String con = $"Server=localhost; Database= master; Trusted_Connection=True;";
            bool isHasDatabase = DatabaseHelper.isDatabaseExists(con, db);
            if (!isHasDatabase)
            {
                return;
            }

            CakeDaoImp cakeDaoImp = new CakeDaoImp();
            if (cakeDaoImp.GetCakes().Count() > 0)
            {
                Random random = new Random();
                int index = random.Next(cakeDaoImp.GetCakes().Count());

                Cake cake = cakeDaoImp.GetCakes()[index];
                txtCakeName.Text = cake.Name;
                txtDescription.Text = cake.Description;

                CakeImageDaoImp cakeImageDaoImp = new CakeImageDaoImp();
                CakeImage cakeImage = cakeImageDaoImp.getImagebyCakeID(cake.Id);

                if (cakeImage == null || cakeImage.Image == null || cakeImage.Image == "")
                {

                }
                else
                {
                    Uri uri = new Uri(cakeImage.Image, UriKind.RelativeOrAbsolute);
                    try
                    {
                        imgCake.Source = new BitmapImage(uri);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }


        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            count++;
            if (count == target)
            {
                timer.Stop();
                Dispatcher.Invoke(() => {
                    var screen = new Dashboard();
                    screen.Show();
                    if (btnUnSubcript.IsChecked == true)
                    {
                        var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        config.AppSettings.Settings["ShowSplashScreen"].Value = "false";
                        config.Save(ConfigurationSaveMode.Minimal);
                      
                    }
                    this.Close();
                });
            }
            Dispatcher.Invoke(() =>
            {
                progress.Value = count;
            });
        }

        int count = 0;
        int target = 8;
        System.Timers.Timer timer;
        
    }
    
}
