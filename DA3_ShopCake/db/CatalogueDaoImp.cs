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
