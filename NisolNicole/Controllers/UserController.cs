using Infrastructure.SqlServer.Repository.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NisolNicole.Utils;
using NisolNicole.Utils.Dtos;

namespace NisolNicole.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UserController : ControllerBase
    {
        private readonly UseCaseCreateUser _useCaseCreateUser;
        private readonly UseCaseListUser _useCaseListUser;
        private readonly UseCaseDeleteUser _useCaseDelete;
        private readonly IJwtAuthentificationManager _jwtAuthentificationManager;

        public UserController(UseCaseCreateUser useCaseCreateUser, UseCaseListUser useCaseListUser, UseCaseDeleteUser useCaseDelete, IJwtAuthentificationManager jwtAuthentificationManager)
        {
            _useCaseCreateUser = useCaseCreateUser;
            _useCaseListUser = useCaseListUser;
            _useCaseDelete = useCaseDelete;
            _jwtAuthentificationManager = jwtAuthentificationManager;
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult<OutputDtoCreateUser> Create([FromBody] InputDtoCreateUsers user)
        {
            return StatusCode(201, _useCaseCreateUser.Execute(user)); 
        }

        [HttpPost]
        [Route("SignIn")]
        public ActionResult SignIn([FromBody] InputDtoCreateUsers user)
        {
            _useCaseCreateUser.Execute(user);
            UserProxy userProxy = _jwtAuthentificationManager.Authentificate(user.Pseudo, user.Password);
            if (!userProxy.token.IsNullOrEmpty())
            {
                return Ok(new { userProxy });
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] InputDtoLoginUser inputDtoLoginUser)
        {
            UserProxy userProxy = _jwtAuthentificationManager.Authentificate(inputDtoLoginUser.Pseudo, inputDtoLoginUser.Password);

            if (!userProxy.token.IsNullOrEmpty())
            {
                return Ok(new { userProxy });
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(200)]
        public ActionResult<List<OutputDtoUser>> GetAll()
        {
            return StatusCode(200, _useCaseListUser.Execute());
        }
        
        [HttpPost]
        [Route("GetUser")]
        [ProducesResponseType(200)]
        public ActionResult<OutputDtoUser>GetUser([FromBody] InputDtoUsers inputDtoUsers) 
        {
            return StatusCode(200, _useCaseListUser.Execute(inputDtoUsers));
        }
        [HttpDelete]
        [ProducesResponseType(200)]
        public ActionResult<bool> Delete(int id)
        {
            return StatusCode(200, _useCaseDelete.Execute(new InputDtoUsers()
            {
                Id = id
            })); 
        }
        
        [HttpPut]
        [ProducesResponseType(200)]
        [Route("updateUser")]
        public ActionResult<bool> Update([FromBody]InputDtoUpdateUsers inputDtoUpdateUsers)
        {
            return StatusCode(200, _useCaseCreateUser.Execute(inputDtoUpdateUsers)); 
        }


        [HttpPut]
        [ProducesResponseType(200)]
        [Route("hashPasswords")]
        public ActionResult<bool> hashPasswords()
        {
            _useCaseCreateUser.Execute();
            return StatusCode(200);
        }
    }
}