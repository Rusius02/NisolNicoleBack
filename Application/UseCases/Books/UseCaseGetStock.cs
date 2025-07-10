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
        public void Execute(InputDtoStock dto)
        {
            _bookRepository.UpdateStock(dto.Id, dto.quantity);
        }
    }
}
