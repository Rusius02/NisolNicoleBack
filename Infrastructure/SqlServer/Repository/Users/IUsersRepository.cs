using System.Collections.Generic;

namespace Infrastructure.SqlServer.Repository.Users
{
    //This is where we declare all our User Repository methods
    public interface IUsersRepository
    {
        Domain.Users Create(Domain.Users user);

        List<Domain.Users> GetAll();
        
        Domain.Users GetUser(Domain.Users users);

        bool Delete(Domain.Users users);

        bool Update(Domain.Users users);
        
        Domain.Users GetUserByPseudo(string pseudo, string password);
        
        
    }
}