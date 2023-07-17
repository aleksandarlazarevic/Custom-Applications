using System.Data.SqlClient;

namespace Utilities
{
    public class SQLManager
    {
        public SqlDataReader ExecuteQuery(string connectionString, string sqlCommand)
        {
            SqlDataReader reader;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    reader = command.ExecuteReader();
                }
            }

            return reader;
        }
    }
}