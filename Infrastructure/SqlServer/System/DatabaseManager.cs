using System.Data.SqlClient;
using System.IO;

namespace Infrastructure.SqlServer.System
{
    public class DatabaseManager : IDatabaseManager
    {
        //Method that will call the init file that initializes the database tables
        public void CreateDatabaseAndTables()
        {
            var script =
                File.ReadAllText(
                    @"D:\RiderProjects\NisolNicole\Infrastructure\SqlServer\Ressources\Init.sql");

            var connection = Database.GetConnection();
            connection.Open();
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = script
            };

            command.ExecuteNonQuery();
        }

        //Method that will call the data file that create the database tables
        public void FillTables()
        {
            var script =
                File.ReadAllText(
                    @"D:\RiderProjects\NisolNicole\Infrastructure\SqlServer\Ressources\Data.sql");

            //Connection to the database
            var connection = Database.GetConnection();
            connection.Open();
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = script
            };

            command.ExecuteNonQuery();
        }
    }
}