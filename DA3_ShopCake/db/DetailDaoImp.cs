using dbforproject3.db.DbHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;

namespace ConsoleApp2.db
{
    class DetailDaoImp : DetailBillDao
    {
        private List<DetailBill> detailBills;

        public DetailDaoImp()
        {
            detailBills = new List<DetailBill>();
            
            string strConn = $"Server=localhost; Database=QLTiemBanh; Trusted_Connection=True;";
            SqlConnection sqlConnection = new SqlConnection(strConn);
            SqlCommand sqlCommand = new SqlCommand();
            String query = "select * from DETAIL_BILL";

            try
            {
                sqlCommand.CommandText = query;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;

                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    DetailBill detail = new DetailBill();
                    
                    detail.BillId = reader[0].ToString();
                    detail.CakeId = reader[1].ToString();
                    detail.Count = Int32.Parse(reader[2].ToString());

                    detailBills.Add(detail);
                }
                sqlConnection.Close();

            }
            catch (Exception ex)
            {
                sqlConnection.Close();
                throw ex;
            }
        }
        public void deleteDetailBill(DetailBill detail)
        {
            String delQuery = $"DELETE FROM DETAIL_BILL WHERE BILL_ID = '{detail.BillId}' AND CAKE_ID = '{detail.CakeId}';";
            DatabaseHelper.executeQuery(delQuery);
        }

        public List<DetailBill> GetDetailBills()
        {
            return detailBills;
        }

        public void insertDetailBill(DetailBill detail)
        {
            using (SqlConnection connection = new SqlConnection("Server=localhost; Database=QLTiemBanh; Trusted_Connection=True;"))
            {
                String query = "INSERT INTO dbo.DETAIL_BILL (BILL_ID, CAKE_ID, COUNT)" +
                " VALUES (@BillId,@CakeId,@Count)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BillId", detail.BillId);
                    command.Parameters.AddWithValue("@CakeId", detail.CakeId);
                    command.Parameters.AddWithValue("@Count", detail.Count);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    if (result < 0)
                    {
                        MessageBox.Show("Insertion: failed");
                    }
                }
            }

        }
        /*
        DETAIL_BILL: BILL_ID(FK), CAKE_ID (FK), COUNT
         */
        public void updateDetailBill(DetailBill detail)
        {
            throw new NotImplementedException();
        }
    }
}
