using Infrastructure.SqlServer.Utils;
using System.Data.SqlClient;

namespace Infrastructure.SqlServer.Repository.Users
{
    public class UsersFactory : IDomainFactory<Domain.Users>
    {
        /*Here we have our class that will load our data from the data table into a new User*/
        public Domain.Users CreateFromSqlReader(SqlDataReader reader)
        {
            return new Domain.Users()
            {
                Id = reader.GetInt32(reader.GetOrdinal(UsersRepository.ColId)),
                LastName = reader.GetString(reader.GetOrdinal(UsersRepository.ColLastName)),
                FirstName = reader.GetString(reader.GetOrdinal(UsersRepository.ColFirstName)),
                sexe = reader.GetString(reader.GetOrdinal(UsersRepository.ColSexe)),
                BirthDate = reader.GetDateTime(reader.GetOrdinal(UsersRepository.ColBirthdate)),
                mail = reader.GetString(reader.GetOrdinal(UsersRepository.ColMail)),
                pseudo = reader.GetString(reader.GetOrdinal(UsersRepository.ColPseudo)),
                Password = reader.GetString(reader.GetOrdinal(UsersRepository.ColPassword)),
                Role = reader.GetString(reader.GetOrdinal(UsersRepository.ColRole))
                    
            };
        }
    }
}