using System.Data.SqlClient;

namespace Infrastructure
{
    public class Database
    {
        //Method that connects to our server and database
        private const string ConnectionString = "Server=MSI;DataBase=NisolNicole;Integrated Security=SSPI";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}