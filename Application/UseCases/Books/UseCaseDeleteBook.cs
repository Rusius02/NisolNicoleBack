using Application.UseCases.Books.Dtos;
using Application.Utils;
using Infrastructure.SqlServer.Repository.Books;

namespace Application.UseCases.Books
{
    public class UseCaseDeleteBook
    {
        private readonly IBookRepository _bookRepository;
        public UseCaseDeleteBook(IBookRepository usersRepository)
        {
            _bookRepository = usersRepository;
        }
        public bool Execute(InputDtoDeleteBook dto)
        {
            var bookFromDto = Mapper.GetInstance().Map<Domain.Book>(dto);
            var bookFromDB = _bookRepository.Delete(bookFromDto);
            return Mapper.GetInstance().Map<bool>(bookFromDB);
        }
    }
}
