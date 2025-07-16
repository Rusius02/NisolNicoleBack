using Application.Utils;
using Infrastructure.SqlServer.Repository.Users;
using NisolNicole.Utils.Dtos;

namespace NisolNicole.Utils
{
    public class UseCaseCreateUser : IWriting<OutputDtoCreateUser, InputDtoCreateUsers>
    {
        
        private readonly IUsersRepository _usersRepository;
        public UseCaseCreateUser(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        
       
        public OutputDtoCreateUser Execute(InputDtoCreateUsers dto)
        {
            var userFromDto = Mapper.GetInstance().Map<Domain.Users>(dto);

            var userFromDb = _usersRepository.Create(userFromDto);
            return Mapper.GetInstance().Map<OutputDtoCreateUser>(userFromDb);
        }

        
        public bool Execute(InputDtoUpdateUsers dto)
        {
            var userFromDto = Mapper.GetInstance().Map<Domain.Users>(dto);
            var userFromDB = _usersRepository.Update(userFromDto);
            return Mapper.GetInstance().Map<bool>(userFromDB);
        }
        
        public void Execute()
        {
            _usersRepository.HashPasswordsForAllUsers();
        }
    }
    
}