using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbforproject3.db.DbHelper
{
    class DatabaseHelper
    {
        public static String Database;
        public static bool isDatabaseExists(string connectionString, string databaseName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand($"SELECT db_id('{databaseName}')", connection))
                {
                    connection.Open();
                    return (command.ExecuteScalar() != DBNull.Value);
                }
            }
        }
        public static int executeQuery(string query)
        {
            int rowCount = 0;
            string strConn = $"Server=localhost; Database={Database}; Trusted_Connection=True;";
            SqlConnection sqlConnection = new SqlConnection(strConn);
            SqlCommand sqlCommand = new SqlCommand();

            try
            {
                sqlCommand.CommandText = query;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandTimeout = 12 * 3600;

                sqlConnection.Open();
                rowCount = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                sqlConnection.Close();
                throw ex;
            }

            return rowCount;
        }
        public static SqlDataReader executeReader(String query)
        {
            SqlDataReader result;

            string strConn = $"Server=localhost; Database=QLChuyenDi; Trusted_Connection=True;";
            SqlConnection sqlConnection = new SqlConnection(strConn);
            SqlCommand sqlCommand = new SqlCommand();

            sqlCommand.CommandText = query;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Connection = sqlConnection;

            try
            {
                sqlConnection.Open();
                result = sqlCommand.ExecuteReader();
                sqlConnection.Close();
                return result;
            }
            catch (Exception)
            {
                sqlConnection.Close();
            }
            return null;
        }
    }
}
