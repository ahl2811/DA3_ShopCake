using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.db
{
    interface CustomerDao
    {
        List<Customer> GetCustomers();
        void insertCustomer(Customer customer);
        void deleteCustomer(Customer customer);
        void updateCustomer(Customer customer);
    }
}
