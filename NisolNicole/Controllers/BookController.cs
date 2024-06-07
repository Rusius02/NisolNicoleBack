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
        private readonly UseCaseListBook _useCaseListBook;

        public BookController(UseCaseCreateBook useCaseCreateBook, UseCaseDeleteBook useCaseDeleteBook, UseCaseListBook useCaseListBook)
        {
            _useCaseCreateBook = useCaseCreateBook;
            _useCaseDeleteBook = useCaseDeleteBook;
            _useCaseListBook = useCaseListBook;
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult<OutputDtoCreateBook> Create([FromBody] InputDtoCreateBook book)
        {
            return StatusCode(201, _useCaseCreateBook.Execute(book));
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        public ActionResult<bool> Delete(int id)
        {
           return StatusCode(200, _useCaseDeleteBook.Execute(new InputDtoDeleteBook()
            {
                Id = id
            }));
        }
        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(200)]
        public ActionResult<List<OutputDtoBook>> GetAll()
        {
            return StatusCode(200, _useCaseListBook.Execute());
        }

        [HttpPost]
        [Route("GetBook")]
        [ProducesResponseType(200)]
        public ActionResult<OutputDtoBook> GetBook([FromBody] InputDtoBook inputDtoBook)
        {
            return StatusCode(200, _useCaseListBook.Execute(inputDtoBook));
        }
    }
}
