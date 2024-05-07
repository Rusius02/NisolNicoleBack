using Application.Utils;
using Infrastructure.SqlServer.Repository.Users;
using NisolNicole.Utils.Dtos;

namespace NisolNicole.Utils
{
    public class UseCaseCreateUser : IWriting<OutputDtoCreateUser, InputDtoCreateUsers>
    {
        //Initialization of our repository
        private readonly IUsersRepository _usersRepository;
        public UseCaseCreateUser(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        
        /*Method that will create an User using an InputDTO given as an argument
         and that will return an Activity OutputDto*/
        public OutputDtoCreateUser Execute(InputDtoCreateUsers dto)
        {
            var userFromDto = Mapper.GetInstance().Map<Domain.Users>(dto);

            var userFromDb = _usersRepository.Create(userFromDto);
            return Mapper.GetInstance().Map<OutputDtoCreateUser>(userFromDb);
        }

        /*Method that will modify an User using an InputDTO given to it as an argument
         and that will return a boolean to tell us if it has been modified*/
        public bool Execute(InputDtoUpdateUsers dto)
        {
            var userFromDto = Mapper.GetInstance().Map<Domain.Users>(dto);
            var userFromDB = _usersRepository.Update(userFromDto);
            return Mapper.GetInstance().Map<bool>(userFromDB);
        }
        /*Method that will modify an User using an InputDTO given to it as an argument
         and that will return a boolean to tell us if it has been modified*/
        public void Execute()
        {
            _usersRepository.HashPasswordsForAllUsers();
        }
    }
    
}