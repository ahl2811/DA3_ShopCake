using ConsoleApp2.db;
using DA3_ShopCake.utils;
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
    public partial class HistoryScreen : UserControl
    {
        private List<HistoryBillItem> historyBillItems;
        private BillDaoImp billDaoImp;
        private CustomerDaoImp customerDaoImp;
        private DetailDaoImp detailDaoImp;

        public HistoryScreen()
        {
            InitializeComponent();
            historyBillItems = new List<HistoryBillItem>();
            
            billDaoImp = new BillDaoImp();
            customerDaoImp = new CustomerDaoImp();
            detailDaoImp = new DetailDaoImp();

            foreach(Bill bill in billDaoImp.GetBills())
            {
                Customer customer = customerDaoImp.getCustomerById(bill.CustomerId);

                historyBillItems.Add(new HistoryBillItem(customer.Name, customer.Phone, bill.Id, bill.SaleDate.Substring(0,10), billCost(bill.Id).ToString()));
            }
            lbHistoryBills.ItemsSource = historyBillItems;
        }

        private int billCost(String billId)
        {
            int result = 0;

            List<KeyValuePair<int, int>> elements = new List<KeyValuePair<int, int>>();
            CakeDaoImp cakeDaoImp = new CakeDaoImp();

            foreach(DetailBill detail in detailDaoImp.GetDetailBills())
            {
                if(detail.BillId.Equals(billId))
                {
                    Cake cake = cakeDaoImp.getCakeById(detail.CakeId);
                    elements.Add(new KeyValuePair<int, int>(cake.Price, detail.Count));
                }
            }

            result = Calculation.sumOfSerialNumbers(elements);

            return result;
        }
    }

    class HistoryBillItem
    {
        public String CustomerName { get; set; }
        public String Phone { get; set; }
        public String BillId { get; set; }
        public String SaleDate { get; set; }
        public String Count { get; set; }

        public HistoryBillItem(string customerName, string phone, string billId, string saleDate, string count)
        {
            CustomerName = customerName;
            Phone = phone;
            BillId = billId;
            SaleDate = saleDate;
            Count = count;
        }
    }
}
