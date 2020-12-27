using ConsoleApp2.db;
using DA3_ShopCake.db;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class NewAddingScreen : UserControl
    {
        public delegate void DelegateExit();
        public event DelegateExit ExitHandler;

        public event DelegateExit SubmitHandler;
        Cake newCake;
        CakeDaoImp cakeDaoImp;
        ObservableCollection<CakeImage> cakeImages;


        public NewAddingScreen()
        {
            InitializeComponent();

            cakeDaoImp = new CakeDaoImp();

            newCake = new Cake();
            newCake.Id = cakeDaoImp.getNextId();
            cakeImages = new ObservableCollection<CakeImage>();
            lbImages.ItemsSource = cakeImages;
            txtId.Text = newCake.Id;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            ExitHandler?.Invoke();
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {

            if (txtCakeName.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên bánh kem");
                return;
            } else if (txtPrice.Text == "")
            {
                MessageBox.Show("Bạn phải nhập giá sản phẩm");
                return;
            }

            newCake.Name = txtCakeName.Text;
            newCake.Price = Int32.Parse(txtPrice.Text);
            newCake.Description = txtDescription.Text;
            newCake.CatalogueId = (cbCatalogue.SelectedIndex + 1).ToString();
            cakeDaoImp.insertCake(newCake);

            if (cakeImages.Count() > 0)
            {
                CakeImageDaoImp cakeImageDao = new CakeImageDaoImp();
                foreach(CakeImage cakeImage in cakeImages)
                {
                    var info = new FileInfo(cakeImage.Image);

                    var newName = $"{Guid.NewGuid()}{info.Extension}";

                    var currentFolder = AppDomain.CurrentDomain.BaseDirectory;
                    File.Copy(cakeImage.Image, $"{currentFolder}Assets\\Images\\{newName}");
                    cakeImage.Image = $"{currentFolder}Assets\\Images\\{newName}";

                    cakeImageDao.insertCakeImage(cakeImage);
                }
            }

            SubmitHandler?.Invoke();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void OnAddNewImage(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            screen.Multiselect = true;

            if (screen.ShowDialog() == true)
            {
                var files = screen.FileNames;

                foreach (var file in files)
                { 
                    cakeImages.Add(new CakeImage(newCake.Id, file));
                }
            }
        }
    }
}
