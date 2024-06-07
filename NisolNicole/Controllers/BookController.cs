using Application.UseCases.Books;
using Application.UseCases.Books.Dtos;
using Infrastructure.SqlServer.Repository.Users;
using Microsoft.AspNetCore.Mvc;
using NisolNicole.Utils;
using NisolNicole.Utils.Dtos;

namespace NisolNicole.Controllers
{
    [ApiController]
    [Route("api/Books")]
    public class BookController : ControllerBase
    {
        private readonly UseCaseCreateBook _useCaseCreateBook;
        private readonly UseCaseDeleteBook _useCaseDeleteBook;

        public BookController(UseCaseCreateBook useCaseCreateBook, UseCaseDeleteBook useCaseDeleteBook)
        {
            _useCaseCreateBook = useCaseCreateBook;
            _useCaseDeleteBook = useCaseDeleteBook;
        }

        /*Here we have our Create method,
        We give it the type Post*/
        [HttpPost]
        [Route("Create")]
        public ActionResult<OutputDtoCreateBook> Create([FromBody] InputDtoCreateBook book)
        {
            /*We call the Execute method of our UseCase and give it a Dto.
             And it will return an OutputDto of Comment.
            And we return the code 201 to notify that the request has been made*/
            return StatusCode(201, _useCaseCreateBook.Execute(book));
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        public ActionResult<bool> Delete(int id)
        {
            /*We call the Execute method of our UseCase and give it a Dto which returns a Boolean.
            And we return the code 200 to notify that the request has been made*/
            return StatusCode(200, _useCaseDeleteBook.Execute(new InputDtoDeleteBook()
            {
                Id = id
            }));
        }
    }
}
