using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Microondas.API.DataBase
{
    public class Db
    {
        private static SqlConnection? conn = null;
        private static IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        public static SqlConnection GetConnection()
        {
            if (conn == null)
            {
                try
                {
                    var typeDatabase = configuration["TypeDatabase"];
                    if (string.IsNullOrEmpty(typeDatabase))
                    {
                        throw new ArgumentException("TypeDatabase configuration key is missing or empty.");
                    }

                    var connectionString = configuration.GetConnectionString(typeDatabase);
                    if (string.IsNullOrEmpty(connectionString))
                    {
                        throw new ArgumentException($"Connection string for '{typeDatabase}' is missing or empty.");
                    }

                    conn = new SqlConnection(connectionString);
                    conn.Open();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error connecting to database: {ex.Message}");
                    throw new DbException(ex.Message);
                }
            }
            return conn;
        }

        public static void CloseConnection()
        {
            try
            {
                if (conn != null && conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                    conn = null;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error closing database connection: {ex.Message}");
                throw new DbException(ex.Message);
            }
        }

        public static void closeReader(SqlDataReader reader)
        {
            try
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error closing SqlDataReader: {ex.Message}");
                throw new DbException(ex.Message);
            }
        }

    }
}
