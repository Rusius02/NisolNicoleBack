using Application.Utils;
using Infrastructure.SqlServer.Repository.Users;
using NisolNicole.Utils.Dtos;

namespace NisolNicole.Utils
{
    public class UseCaseDeleteUser : IDelete<InputDtoUsers>
    {
        //Initialization of our repository
        private readonly IUsersRepository _usersRepository;
        public UseCaseDeleteUser(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        /*Method that will delete an User using a given InputDTO as argument
         and returns a boolean to tell us whether it has been deleted*/
        public bool Execute(InputDtoUsers dto)
        {
            var activityFromDto = Mapper.GetInstance().Map<Domain.Users>(dto);
            var activityFromDB = _usersRepository.Delete(activityFromDto);
            return Mapper.GetInstance().Map<bool>(activityFromDB);
        }
    }
}