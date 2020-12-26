using System;
using System.Collections.Generic;
using System.Text;


/*
CUSTOMER: CUSTOMER_ID (PK), CUSTOMER_NAME, PHONE
 */
namespace ConsoleApp2.db
{
    class Customer
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }

        public Customer(string id, string name, string phone)
        {
            Id = id;
            Name = name;
            Phone = phone;
        }

        public Customer()
        {
            //do nothing
        }
    }
}
