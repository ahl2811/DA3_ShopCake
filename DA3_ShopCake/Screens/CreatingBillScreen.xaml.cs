using ConsoleApp2.db;
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
    /// Interaction logic for CreatingBillScreen.xaml
    /// </summary>
    public partial class CreatingBillScreen : UserControl
    {
        public delegate void DelegateExit();
        public event DelegateExit ExitHandler;
        public event DelegateExit SubmitHandler;
       
        private Bill newBill;
        private BillDaoImp billDao;

        private Customer newCustomer;
        private CustomerDaoImp customerDao;

        private double cost;
        private List<Item> lbContent;
        List<KeyValuePair<String, int>> Purchases;

        public CreatingBillScreen(List<KeyValuePair<String, int>> selectedPurchases, double COST)
        {
            InitializeComponent();
            billDao = new BillDaoImp();
            newBill = new Bill();
            newBill.Id = billDao.getNextId();

            Purchases = selectedPurchases;
            cost = COST;

            customerDao = new CustomerDaoImp();
            newCustomer = new Customer();
            newCustomer.Id = customerDao.getNextId();

            setUpUI(selectedPurchases);
        }
        private void setUpUI(List<KeyValuePair<String, int>> selectedPurchases)
        {
            txtBillId.Text = newBill.Id;
            txtCustomerId.Text = newCustomer.Id;

            DateTime now = DateTime.Now;
            txtSaleDate.Text = now.ToString();

            lbContent = new List<Item>();
            CakeDaoImp cakeDao = new CakeDaoImp();

            foreach(KeyValuePair<String, int> item in selectedPurchases)
            {
                Cake cake = cakeDao.getCakeById(item.Key);
                lbContent.Add(new Item(cake.Name, item.Value.ToString()));
            }

            lbSelectedCake.ItemsSource = lbContent;
            txtCost.Text = cost.ToString();
        }
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            ExitHandler?.Invoke();
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if(txtCustomerName.Text == "")
            {
                MessageBox.Show("Bạn chưa điền tên khách hàng");
                return;
            }

            newCustomer.Name = txtCustomerName.Text;
            newCustomer.Phone = txtPhone.Text;

            customerDao.insertCustomer(newCustomer);

            newBill.CustomerId = newCustomer.Id;
            newBill.SaleDate = txtSaleDate.Text;

            billDao.insertBill(newBill);

            DetailDaoImp detailDaoImp = new DetailDaoImp();
            foreach(KeyValuePair<String, int> purchase in Purchases)
            {
                detailDaoImp.insertDetailBill(new DetailBill(newBill.Id, purchase.Key, purchase.Value));
            }

            MessageBox.Show("Đã tạo bill thành công");

            SubmitHandler?.Invoke();
        }

        private void TextBoxCustomer_GotFocus(object sender, RoutedEventArgs e)
        {
            keyWordCustomer.Visibility = Visibility.Hidden;
        }

        private void TextBoxCustomer_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtCustomerName.Text.Length == 0)
            {
                keyWordCustomer.Visibility = Visibility.Visible;
            }
        }

        private void TextBoxPhone_GotFocus(object sender, RoutedEventArgs e)
        {
            keyWordPhone.Visibility = Visibility.Hidden;
        }

        private void TextBoxPhone_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtPhone.Text.Length == 0)
            {
                keyWordPhone.Visibility = Visibility.Visible;
            }
        }

    }

    class Item
    {
        public String Name { get; set; }
        public String Count { get; set; }

        public Item(string name, string count)
        {
            Name = name;
            Count = count;
        }
    }
}
