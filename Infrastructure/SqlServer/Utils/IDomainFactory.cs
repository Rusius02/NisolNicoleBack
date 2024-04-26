using System.Data.SqlClient;

namespace Infrastructure.SqlServer.Utils
{
    //Interface that will define the method that will take the data from the repository
    public interface IDomainFactory<out T>
    {
        T CreateFromSqlReader(SqlDataReader reader);
    }
}