using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.db
{
    interface BillDao
    {
        List<Bill> GetBills();
        void insertBill(Bill bill);
        void deleteBill(Bill bill);
    }
}
