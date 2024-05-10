using Application.UseCases.Books.Dtos;
using Application.Utils;
using Infrastructure.SqlServer.Repository.Books;
using Infrastructure.SqlServer.Repository.Users;
using NisolNicole.Utils.Dtos;

namespace Application.UseCases.Books
{
    public class UseCaseCreateBook : IWriting<OutputDtoCreateBook, InputDtoCreateBook>
    {
        //Initialization of our repository
        private readonly IBookRepository _bookRepository;
        public UseCaseCreateBook(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public OutputDtoCreateBook Execute(InputDtoCreateBook dto)
        {
            var bookFromDto = Mapper.GetInstance().Map<Domain.Book>(dto);

            var bookFromDb = _bookRepository.Create(bookFromDto);
            return Mapper.GetInstance().Map<OutputDtoCreateBook>(bookFromDb);
        }

        public bool Execute(InputDtoUpdateBook dto)
        {
            var bookFromDto = Mapper.GetInstance().Map<Domain.Book>(dto);
            var bookFromDB = _bookRepository.Update(bookFromDto);
            return Mapper.GetInstance().Map<bool>(bookFromDB);
        }        
    }
}
