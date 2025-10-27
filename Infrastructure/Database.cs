using System.Data.SqlClient;

namespace Infrastructure
{
    public class Database
    {
        //Method that connects to our server and database
        private const string ConnectionString = "Server=tcp:serveradri.database.windows.net,1433;Initial Catalog=NisolNicole;Persist Security Info=False;User ID=sqladmin;Password=Ugrpouimpl258S3;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}