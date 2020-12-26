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
    class BillDaoImp : BillDao
    {
        private List<Bill> bills;

        public BillDaoImp()
        {
            bills = new List<Bill>();

            string strConn = $"Server=localhost; Database=QLTiemBanh; Trusted_Connection=True;";
            SqlConnection sqlConnection = new SqlConnection(strConn);
            SqlCommand sqlCommand = new SqlCommand();
            String query = "select * from BILL";

            try
            {
                sqlCommand.CommandText = query;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;

                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Bill bill = new Bill();
                    
                    bill.Id = reader[0].ToString();
                    bill.CustomerId = reader[1].ToString();
                    bill.SaleDate = reader[2].ToString();

                    bills.Add(bill);
                }
                sqlConnection.Close();

            }
            catch (Exception ex)
            {
                sqlConnection.Close();
                throw ex;
            }
        }

        public void deleteBill(Bill bill)
        {
            String delQuery = $"DELETE FROM BILL WHERE BILL_ID = '{bill.Id}';";
            DatabaseHelper.executeQuery(delQuery);
        }

        public List<Bill> GetBills()
        {
            return bills;
        }

        public void insertBill(Bill bill)
        {
            using (SqlConnection connection = new SqlConnection("Server=localhost; Database=QLTiemBanh; Trusted_Connection=True;"))
            {
                String query = "INSERT INTO dbo.BILL (BILL_ID, CUSTOMER_ID, SALE_DATE)" +
                " VALUES (@Id,@CustomerId,@SaleDate)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", bill.Id);
                    command.Parameters.AddWithValue("@CustomerId", bill.CustomerId);
                    command.Parameters.AddWithValue("@SaleDate", bill.SaleDate);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    if (result < 0)
                    {
                        MessageBox.Show("Insertion: failed");
                    }
                }
            }
        }


        public String getNextId()
        {
            String newId = "";
            bool isFoundNewId = false;

            while (!isFoundNewId)
            {
                newId = StringHelper.RandomString(6);
                if (bills.Count() == 0)
                {
                    isFoundNewId = true;
                }
                for (int i = 0; i < bills.Count(); i++)
                {
                    if (bills[i].Id.Equals(newId))
                    {
                        break;
                    }
                    if (i == bills.Count() - 1)
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
