using Application.Utils;
using Infrastructure.SqlServer.Repository.Users;
using NisolNicole.Utils.Dtos;

namespace NisolNicole.Utils
{
    public class UseCaseListUser : IQuery<List<OutputDtoUser>>
    {
        //Initialization of our repository
        private readonly IUsersRepository _usersRepository;

        public UseCaseListUser(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        
        /*Method that will return an User List*/
        public List<OutputDtoUser> Execute()
        {
            List<Domain.Users> users = _usersRepository.GetAll();
            return Mapper.GetInstance().Map<List<OutputDtoUser>>(users);
        }
        
        /*Method that will return a User
         according to a dto passed to it as an argument*/
        public OutputDtoUser Execute(InputDtoUsers dto)
        {
            var userFromDto = Mapper.GetInstance().Map<Domain.Users>(dto);
            Domain.Users users = _usersRepository.GetUser(userFromDto);
            return Mapper.GetInstance().Map<OutputDtoUser>(users);
        }
    }
}