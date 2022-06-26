using System.Data.SqlClient;

namespace SharedFunctionalities.Retrieve
{
    public class SQLHelper
    {
        #region fields
        #endregion

        #region constructors

        #endregion

        #region methods
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
        #endregion
    }
}
