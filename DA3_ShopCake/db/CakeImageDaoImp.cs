using dbforproject3.db.DbHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DA3_ShopCake.db
{
    class CakeImageDaoImp : CakeImageDao
    {

        private List<CakeImage> cakeImages;

        public CakeImageDaoImp()
        {
            cakeImages = new List<CakeImage>();

            string strConn = $"Server=localhost; Database=QLTiemBanh; Trusted_Connection=True;";
            SqlConnection sqlConnection = new SqlConnection(strConn);
            SqlCommand sqlCommand = new SqlCommand();
            String query = "select * from CAKEIMAGE";

            try
            {
                sqlCommand.CommandText = query;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;

                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    CakeImage cakeImage = new CakeImage();

                    var currentFolder = AppDomain.CurrentDomain.BaseDirectory;
                    cakeImage.CakeId = reader[0].ToString();
                    cakeImage.Image = $"{currentFolder}Assets\\Images\\{reader[1].ToString()}";

                    cakeImages.Add(cakeImage);
                }
                sqlConnection.Close();

            }
            catch (Exception ex)
            {
                sqlConnection.Close();
                throw ex;
            }
        }
        public void deleteCakeImage(CakeImage cakeImage)
        {
            String delQuery = $"DELETE FROM CAKEIMAGE WHERE CAKE_ID = '{cakeImage.CakeId}';";
            DatabaseHelper.executeQuery(delQuery);
        }

        public List<CakeImage> GetCakeImages()
        {
            return cakeImages;
        }

        public void insertCakeImage(CakeImage cakeImage)
        {
            using (SqlConnection connection = new SqlConnection("Server=localhost; Database=QLTiemBanh; Trusted_Connection=True;"))
            {
                String query = "INSERT INTO dbo.CAKEIMAGE (CAKE_ID, IMAGE)" +
                " VALUES (@CakeId,@Image)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CakeId", cakeImage.CakeId);
                    command.Parameters.AddWithValue("@Image", cakeImage.Image);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    if (result < 0)
                    {
                        MessageBox.Show("Insertion: failed");
                    }
                }
            }
        }

        public CakeImage getImagebyCakeID(String id)
        {
            CakeImage image = null;
            foreach (CakeImage img in cakeImages)
            {
                if (img.CakeId == id)
                {
                    image = new CakeImage(img.CakeId, img.Image);
                    break;
                }
            }

            return image;
        }
        
    }
}
