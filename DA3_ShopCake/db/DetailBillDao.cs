using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.db
{
    interface DetailBillDao
    {
        List<DetailBill> GetDetailBills();
        void insertDetailBill(DetailBill detail);
        void deleteDetailBill(DetailBill detail);
        void updateDetailBill(DetailBill detail);
    }
}
