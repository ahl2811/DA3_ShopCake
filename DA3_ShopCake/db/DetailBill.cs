using System;
using System.Collections.Generic;
using System.Text;

/*
DETAIL_BILL: BILL_ID(FK), CAKE_ID (FK), COUNT
 */
namespace ConsoleApp2.db
{
    class DetailBill
    {
        public String BillId { get; set; }
        public String CakeId { get; set; }
        public int Count { get; set; }

        public DetailBill(string billId, string cakeId, int count)
        {
            BillId = billId;
            CakeId = cakeId;
            Count = count;
        }

        public DetailBill()
        {
            //do nothing
        }
    }
}
