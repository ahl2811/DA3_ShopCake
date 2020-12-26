using System;
using System.Collections.Generic;
using System.Text;


/*
BILL: BILL_ID(PK), CUSTOMER_ID (FK), SALE_DATE
 */
namespace ConsoleApp2.db
{
    class Bill
    {
        public String Id { get; set; }
        public String CustomerId { get; set; }
        public String SaleDate { get; set; }

        public Bill(string id, string customerId, String saleDate)
        {
            Id = id;
            CustomerId = customerId;
            SaleDate = saleDate;
        }

        public Bill()
        {
            //do thing
        }
    }
}
