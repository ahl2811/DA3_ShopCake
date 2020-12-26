using dbforproject3.db.DbHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;

namespace ConsoleApp2.db
{
    class CakeDaoImp : CakeDao
    {
        
        private List<Cake> cakes;

        public CakeDaoImp()
        {
            cakes = new List<Cake>();

            string strConn = $"Server=localhost; Database=QLTiemBanh; Trusted_Connection=True;";
            SqlConnection sqlConnection = new SqlConnection(strConn);
            SqlCommand sqlCommand = new SqlCommand();
            String query = "select * from CAKE";

            try
            {
                sqlCommand.CommandText = query;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;

                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Cake cake = new Cake();
                    
                    cake.Id = reader[0].ToString();
                    cake.CatalogueId = reader[1].ToString();
                    cake.Name = reader[2].ToString();
                    cake.Price = Int32.Parse(reader[3].ToString());
                    cake.Description = reader[4].ToString();

                    cakes.Add(cake);
                }
                sqlConnection.Close();

            }
            catch (Exception ex)
            {
                sqlConnection.Close();
                throw ex;
            }
        }
        public void deleteCake(Cake cake)
        {

            String delQuery = $"DELETE FROM CAKE WHERE CAKE_ID = '{cake.Id}';";
            DatabaseHelper.executeQuery(delQuery);
        }

        public List<Cake> GetCakes()
        {
            return cakes;
        }

        public void insertCake(Cake cake)
        {
            using (SqlConnection connection = new SqlConnection("Server=localhost; Database=QLTiemBanh; Trusted_Connection=True;"))
            {
                String query = "INSERT INTO dbo.CAKE (CAKE_ID, CATALOGUE_ID, CAKE_NAME, PRICE, DESCRIPTION)" +
                " VALUES (@Id,@CatalogueId,@Name,@Price,@Description)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", cake.Id);
                    command.Parameters.AddWithValue("@CatalogueId", cake.CatalogueId);
                    command.Parameters.AddWithValue("@Name", cake.Name);
                    command.Parameters.AddWithValue("@Price", cake.Price);
                    command.Parameters.AddWithValue("@Description", cake.Description);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    if (result < 0)
                    {
                        MessageBox.Show("Insertion: failed");
                    }
                }
            }
        }

        public void updateCake(Cake cake)
        {
            /*
CAKE: CAKE_ID (PK), CATALOGUE_ID (FK), CAKE_NAME, PRICE (INT), IMAGE (TEXT)
 */
        }

    }

}
