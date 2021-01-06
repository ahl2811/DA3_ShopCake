using dbforproject3.db.DbHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;

namespace ConsoleApp2.db
{
    class CatalogueDaoImp : CatalogueDao
    {
        private List<Catalogue> catalogues;

        public CatalogueDaoImp()
        {
            catalogues = new List<Catalogue>();

            string strConn = $"Server=localhost; Database=QLTiemBanh; Trusted_Connection=True;";
            SqlConnection sqlConnection = new SqlConnection(strConn);
            SqlCommand sqlCommand = new SqlCommand();
            String query = "select * from CATALOGUE";

            try
            {
                sqlCommand.CommandText = query;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;

                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Catalogue catalogue = new Catalogue();

                    catalogue.Id = reader[0].ToString();
                    catalogue.CatalogueName = reader[1].ToString();

                    catalogues.Add(catalogue);
                }
                sqlConnection.Close();

            }
            catch (Exception ex)
            {
                sqlConnection.Close();
                throw ex;
            }
        }
        public void deleteCatalogue(Catalogue catalogue)
        {
            String delQuery = $"DELETE FROM CATALOGUE WHERE ID = '{catalogue.Id}';";
            DatabaseHelper.executeQuery(delQuery);
        }

        public List<Catalogue> GetCatalogues()
        {
            return catalogues;
        }

        private List<TotalRevunue> cats;
        public List<TotalRevunue> GetTotalRevunues()
        {
            cats = new List<TotalRevunue>();

            string strConn = $"Server=localhost; Database=QLTiemBanh; Trusted_Connection=True;";
            SqlConnection sqlConnection = new SqlConnection(strConn);
            SqlCommand sqlCommand = new SqlCommand();
            String query = "select CATALOGUE.CATALOGUE_NAME, sum(A.TIEN) from (select CAKE.CAKE_ID, CAKE.CAKE_NAME, CAKE.CATALOGUE_ID, sum(DETAIL_BILL.COUNT)*CAKE.PRICE as TIEN FROM CAKE  join DETAIL_BILL on DETAIL_BILL.CAKE_ID = CAKE.CAKE_ID group by CAKE.CAKE_ID, CAKE.PRICE, CAKE.CAKE_NAME, CAKE.CATALOGUE_ID) as A join CATALOGUE on A.CATALOGUE_ID = CATALOGUE.ID group by CATALOGUE.CATALOGUE_NAME";

            try
            {
                sqlCommand.CommandText = query;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;

                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    TotalRevunue totalRevunue = new TotalRevunue(reader[0].ToString(), Int32.Parse(reader[1].ToString()));
                    cats.Add(totalRevunue);
                }
                sqlConnection.Close();

            }
            catch (Exception ex)
            {
                sqlConnection.Close();
                throw ex;
            }

            return cats;
        }

        public List<TotalRevunue> GetTotalRevunuesByMonth(string month)
        {
            cats = new List<TotalRevunue>();

            string strConn = $"Server=localhost; Database=QLTiemBanh; Trusted_Connection=True;";
            SqlConnection sqlConnection = new SqlConnection(strConn);
            SqlCommand sqlCommand = new SqlCommand();
            String query = $"select CATALOGUE.CATALOGUE_NAME, sum(A.TIEN) from(select CAKE.CAKE_ID, CAKE.CAKE_NAME, CAKE.CATALOGUE_ID, sum(D.COUNT) * CAKE.PRICE as TIEN FROM CAKE  join (select BILL.BILL_ID, BILL.CUSTOMER_ID, BILL.SALE_DATE, DETAIL_BILL.CAKE_ID, DETAIL_BILL.COUNT from BILL join DETAIL_BILL on BILL.BILL_ID = DETAIL_BILL.BILL_ID where SALE_DATE like '%/{month}/%') as D on D.CAKE_ID = CAKE.CAKE_ID group by CAKE.CAKE_ID, CAKE.PRICE, CAKE.CAKE_NAME, CAKE.CATALOGUE_ID) as A right outer join CATALOGUE on A.CATALOGUE_ID = CATALOGUE.ID group by CATALOGUE.CATALOGUE_NAME";

            try
            {
                sqlCommand.CommandText = query;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;

                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    int b = 0;
                    if (reader[1] == null || reader[1].ToString() == "") { b = 0; } else { b = Int32.Parse(reader[1].ToString()); }
                    TotalRevunue totalRevunue = new TotalRevunue(reader[0].ToString(), b);
                    cats.Add(totalRevunue);
                }
                sqlConnection.Close();

            }
            catch (Exception ex)
            {
                sqlConnection.Close();
                throw ex;
            }

            return cats;
        }

        public void insertCatalogue(Catalogue catalogue)
        {
            using (SqlConnection connection = new SqlConnection("Server=localhost; Database=QLTiemBanh; Trusted_Connection=True;"))
            {
                String query = "INSERT INTO dbo.CATALOGUE (ID, CATALOGUE_NAME)" +
                " VALUES (@Id,@CatalogueName)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", catalogue.Id);
                    command.Parameters.AddWithValue("@CatalogueName", catalogue.CatalogueName);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    if (result < 0)
                    {
                        MessageBox.Show("Insertion: failed");
                    }
                }
            }
        }

        public void updateCatalogue(Catalogue catalogue)
        {
            /*
 CATALOGUE: ID (PK), CATALOGUE _NAME (NVARCHAR(50))
 */
        }
    }
}
