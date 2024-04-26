using Infrastructure.SqlServer.Repository.Users;
using Microsoft.AspNetCore.Mvc;
using NisolNicole.Utils;
using NisolNicole.Utils.Dtos;

namespace NisolNicole.Controllers
{
    //Here we declare our User route
    [ApiController]
    [Route("api/Users")]
    public class UserController : ControllerBase
    {
        //Declare the UseCases we need
        private readonly UseCaseCreateUser _useCaseCreateUser;
        private readonly UseCaseListUser _useCaseListUser;
        private readonly UseCaseDeleteUser _useCaseDelete;

        //We call the Constructor to initialize all our UseCases
        public UserController(UseCaseCreateUser useCaseCreateUser, UseCaseListUser useCaseListUser, UseCaseDeleteUser useCaseDelete, IJwtAuthentificationManager jwtAuthentificationManager)
        {
            _useCaseCreateUser = useCaseCreateUser;
            _useCaseListUser = useCaseListUser;
            _useCaseDelete = useCaseDelete;
        }


        /*Here we have our Create method,
         We give it the type Post*/
        [HttpPost]
        [Route("Create")]
        public ActionResult<OutputDtoCreateUser> Create([FromBody] InputDtoCreateUsers user)
        {
            /*We call the Execute method of our UseCase and give it a Dto.
             And it will return an OutputDto of Comment.
            And we return the code 201 to notify that the request has been made*/
            return StatusCode(201, _useCaseCreateUser.Execute(user)); 
        }
        
        /*Here we have our GetAll method,
         We give it the type Get*/
        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(200)]
        public ActionResult<List<OutputDtoUser>> GetAll()
        {
            /*We call the Execute method of our UseCase and give it a Dto.
             And it will return a list of OutputDto's from User.
            And we return the code 200 to notify that the request has been made*/
            return StatusCode(200, _useCaseListUser.Execute());
        }
        
        /*Here we have our GetUser method,
         We give it the type Get*/
        [HttpPost]
        [Route("GetUser")]
        [ProducesResponseType(200)]
        public ActionResult<OutputDtoUser>GetUser([FromBody] InputDtoUsers inputDtoUsers) 
        {
            /*We call the Execute method of the Use Case by sending it an Input that contains an id
             And it will return an OutputDtoUser*/
            return StatusCode(200, _useCaseListUser.Execute(inputDtoUsers));
        }
        /*We have our Delete method,
         We give it the type Delete*/
        [HttpDelete]
        [ProducesResponseType(200)]
        public ActionResult<bool> Delete(int id)
        {
            /*We call the Execute method of our UseCase and give it a Dto which returns a Boolean.
            And we return the code 200 to notify that the request has been made*/
            return StatusCode(200, _useCaseDelete.Execute(new InputDtoUsers()
            {
                Id = id
            })); 
        }
        
        /*Here we have our Update method,
         We give it the type Put*/
        [HttpPut]
        [ProducesResponseType(200)]
        public ActionResult<bool> Update([FromBody]InputDtoUpdateUsers inputDtoUpdateUsers)
        {
            /*We call the Execute method of our UseCase and give it a Dto.
             And it will return an OutputDto of User.
            And we return the code 200 to notify that the request has been made*/
            return StatusCode(200, _useCaseCreateUser.Execute(inputDtoUpdateUsers)); 
        }
    }
}