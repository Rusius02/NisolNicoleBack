using Application.Utils;
using Infrastructure.SqlServer.Repository.Users;
using NisolNicole.Utils.Dtos;

namespace NisolNicole.Utils
{
    public class UseCaseDeleteUser : IDelete<InputDtoUsers>
    {
        private readonly IUsersRepository _usersRepository;
        public UseCaseDeleteUser(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public bool Execute(InputDtoUsers dto)
        {
            var activityFromDto = Mapper.GetInstance().Map<Domain.Users>(dto);
            var activityFromDB = _usersRepository.Delete(activityFromDto);
            return Mapper.GetInstance().Map<bool>(activityFromDB);
        }
    }
}