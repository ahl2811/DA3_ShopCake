using DA3_ShopCake.utils;
using dbforproject3.db.DbHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;

namespace ConsoleApp2.db
{
    class CustomerDaoImp : CustomerDao
    {
        private List<Customer> customers;

        public CustomerDaoImp()
        {
            customers = new List<Customer>();

            string strConn = $"Server=localhost; Database=QLTiemBanh; Trusted_Connection=True;";
            SqlConnection sqlConnection = new SqlConnection(strConn);
            SqlCommand sqlCommand = new SqlCommand();
            String query = "select * from CUSTOMER";

            try
            {
                sqlCommand.CommandText = query;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;

                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Customer customer = new Customer();

                    customer.Id = reader[0].ToString();
                    customer.Name = reader[1].ToString();
                    customer.Phone = reader[2].ToString();

                    customers.Add(customer);
                }
                sqlConnection.Close();

            }
            catch (Exception ex)
            {
                sqlConnection.Close();
                throw ex;
            }
        }
        public void deleteCustomer(Customer customer)
        {
            String delQuery = $"DELETE FROM CUSTOMER WHERE CUSTOMER_ID = '{customer.Id}';";
            DatabaseHelper.executeQuery(delQuery);
        }

        public List<Customer> GetCustomers()
        {
            return customers;
        }

        public void insertCustomer(Customer customer)
        {

            using (SqlConnection connection = new SqlConnection("Server=localhost; Database=QLTiemBanh; Trusted_Connection=True;"))
            {
                String query = "INSERT INTO dbo.CUSTOMER (CUSTOMER_ID, CUSTOMER_NAME, PHONE)" +
                " VALUES (@Id,@Name,@Phone)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", customer.Id);
                    command.Parameters.AddWithValue("@Name", customer.Name);
                    command.Parameters.AddWithValue("@Phone", customer.Phone);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    if (result < 0)
                    {
                        MessageBox.Show("Insertion: failed");
                    }
                }
            }
        }

        public void updateCustomer(Customer customer)
        {
            /*
            CUSTOMER: CUSTOMER_ID (PK), CUSTOMER_NAME, PHONE
             */
        }
        public String getNextId()
        {
            String newId = "";
            bool isFoundNewId = false;

            while (!isFoundNewId)
            {
                newId = StringHelper.RandomString(6);
                if (customers.Count() == 0)
                {
                    isFoundNewId = true;
                }
                for (int i = 0; i < customers.Count(); i++)
                {
                    if (customers[i].Id.Equals(newId))
                    {
                        break;
                    }
                    if (i == customers.Count() - 1)
                    {
                        isFoundNewId = true;
                        break;
                    }
                }
            }

            return newId;
        }
    }
}
