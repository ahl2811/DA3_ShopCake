using ConsoleApp2.db;
using DA3_ShopCake.db;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        private CakeDaoImp cakeDao;
        private ObservableCollection<CakeImage> cakeImages;
        private List<CakeImage> newCakeImages;

        public UpdatingScreen(string cakeCode)
        {
            InitializeComponent();
            this.cakeCode = cakeCode;
            cakeDao = new CakeDaoImp();
            newCakeImages = new List<CakeImage>();

            foreach (Cake cake in cakeDao.GetCakes())
            {
                if (cake.Id.Equals(cakeCode))
                {
                    txtCakeName.Text = cake.Name;
                    txtId.Text = cake.Id;
                    txtPrice.Text = cake.Price.ToString();
                    txtDescription.Text = cake.Description;

                    cakeImages = new ObservableCollection<CakeImage>();
                    CakeImageDaoImp cakeImageDao = new CakeImageDaoImp();
                    foreach (CakeImage cakeImage in cakeImageDao.GetCakeImages())
                    {
                        if (cakeImage.CakeId.Equals(cakeCode))
                        {
                            cakeImages.Add(cakeImage);
                        }
                    }
                    lbImages.ItemsSource = cakeImages;
                    break;
                }
            }
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if (txtCakeName.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên bánh kem");
                return;
            }
            else if (txtPrice.Text == "")
            {
                MessageBox.Show("Bạn phải nhập giá sản phẩm");
                return;
            }

            Cake updateCake = cakeDao.getCakeById(cakeCode);
            if (updateCake == null)
            {
                MessageBox.Show("Lỗi: không tìm thấy sản phẩm");
                return;
            }

            updateCake.Name = txtCakeName.Text;
            updateCake.Price = Int32.Parse(txtPrice.Text);
            updateCake.Description = txtDescription.Text;

            cakeDao.updateCake(updateCake);

            if (newCakeImages.Count() > 0)
            {
                CakeImageDaoImp cakeImageDao = new CakeImageDaoImp();
                foreach (CakeImage cakeImage in newCakeImages)
                {
                    cakeImageDao.insertCakeImage(cakeImage);
                }
            }

            MessageBox.Show("Đã hoàn thành");

            SubmitHandler?.Invoke(cakeCode);
        }

        private void exitUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            ExitHandler?.Invoke();
        }
        private void PlaceholdersListBox_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void onAddNewImage(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            screen.Multiselect = true;

            if (screen.ShowDialog() == true)
            {
                var files = screen.FileNames;

                foreach (var file in files)
                {

                    var info = new FileInfo(file);

                    var newName = $"{Guid.NewGuid()}{info.Extension}";

                    var currentFolder = AppDomain.CurrentDomain.BaseDirectory;
                    File.Copy(file, $"{currentFolder}Assets\\Images\\{newName}");

                    CakeImage newCakeImage = new CakeImage(cakeCode, $"{currentFolder}Assets\\Images\\{newName}");
                    newCakeImages.Add(newCakeImage);
                    cakeImages.Add(newCakeImage);
                }
            }
        }
    }
}
