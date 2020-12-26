using ConsoleApp2.db;
using DA3_ShopCake.db;
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

namespace DA3_ShopCake.Screens
{
    enum CakeType {BirthdayCake, Bread, CupCake, SlicedBread}
    public partial class CakeDetailScreen : UserControl
    {
        
        public delegate void DelegateExit();
        public event DelegateExit ExitHandler;

        public delegate void DelegateUpdate(string cakeCode);
        public event DelegateUpdate UpdateHandler;

        private string cakeCode;
        private CakeDaoImp cakeDao;
        private ObservableCollection<CakeImage> cakeImages;

        public CakeDetailScreen(string cakeCode)
        {
            InitializeComponent();
            this.cakeCode = cakeCode;

            cakeDao = new CakeDaoImp();

            foreach (Cake cake in cakeDao.GetCakes())
            {
                if(cake.Id.Equals(cakeCode))
                {
                    txtCakeName.Text = cake.Name;
                    txtId.Text = cake.Id;
                    txtPrice.Text = cake.Price.ToString();
                    txtDescription.Text = cake.Description;

                    cakeImages = new ObservableCollection<CakeImage>();
                    CakeImageDaoImp cakeImageDao = new CakeImageDaoImp();
                    foreach(CakeImage cakeImage in cakeImageDao.GetCakeImages())
                    {
                        if(cakeImage.CakeId.Equals(cakeCode))
                        {
                            cakeImages.Add(cakeImage);
                        }
                    }
                    lbImages.ItemsSource = cakeImages;

                    if(cakeImages.Count() > 0)
                    {
                        Uri uri = new Uri(cakeImages[0].Image, UriKind.RelativeOrAbsolute);
                        imgCake.Source = new BitmapImage(uri);
                    }
                    else
                    {
                        Uri uri = new Uri("../Assets/Images/bread.jpg", UriKind.RelativeOrAbsolute);
                        imgCake.Source = new BitmapImage(uri);
                    }
                    break;
                }
            }


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

        private void PlaceholdersListBox_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null)
            {
                CakeImage cakeImage = (CakeImage)item.DataContext;
                if(cakeImage != null)
                {
                    Uri uri = new Uri(cakeImage.Image, UriKind.RelativeOrAbsolute);
                    imgCake.Source = new BitmapImage(uri);
                }
            }
        }
    }
}
