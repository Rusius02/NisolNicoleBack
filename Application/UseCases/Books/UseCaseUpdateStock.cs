using Application.UseCases.Books.Dtos;
using Infrastructure.SqlServer.Repository.Books;

namespace Application.UseCases.Books
{
    public class UseCaseUpdateStock
    {
        private readonly IBookRepository _bookRepository;
        public UseCaseUpdateStock(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public void Execute(InputDtoStock dto)
        {
            _bookRepository.UpdateStock(dto.Id, (int)dto.quantity);
        }
    }
}
