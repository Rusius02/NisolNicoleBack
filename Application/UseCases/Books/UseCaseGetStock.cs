using Application.UseCases.Books.Dtos;
using Infrastructure.SqlServer.Repository.Books;

namespace Application.UseCases.Books
{
    public class UseCaseGetStock
    {
        private readonly IBookRepository _bookRepository;
        public UseCaseGetStock(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public int Execute(InputDtoStock dto)
        {
            return _bookRepository.GetStock(dto.Id);
        }
    }
}
