using Application.UseCases.Contact.dtos;
using Application.UseCases.NewFolder;
using Microsoft.AspNetCore.Mvc;

namespace NisolNicole.Controllers
{
    [ApiController]
    [Route("api/Contact")]
    public class ContactController : ControllerBase
    {
        private readonly UseCaseContactAuthorByMail _useCaseContactAuthorByMail;

        public ContactController(UseCaseContactAuthorByMail useCaseContactAuthorByMail)
        {
            _useCaseContactAuthorByMail = useCaseContactAuthorByMail;
        }

        [HttpPost]
        public async Task<IActionResult> ContactAuthor([FromBody] InputContactFormDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _useCaseContactAuthorByMail.Execute(input);
            return Ok();
        }
    }
}
