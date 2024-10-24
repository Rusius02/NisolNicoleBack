using Application.UseCases.Books.Dtos;
using Application.Utils;
using Infrastructure.SqlServer.Repository.Books;
using Microsoft.Extensions.Configuration;

namespace Application.UseCases.Books
{
    public class UseCaseDeleteBook
    {
        private readonly IBookRepository _bookRepository;
        private readonly string _baseImagePath;
        public UseCaseDeleteBook(IBookRepository usersRepository, IConfiguration configuration)
        {
            _bookRepository = usersRepository;
            _baseImagePath = configuration["ImageSettings:BaseImagePath"];
        }
        public bool Execute(InputDtoDeleteBook dto)
        {
            var bookFromDto = Mapper.GetInstance().Map<Domain.Book>(dto);
            var book = _bookRepository.GetBook(bookFromDto);
            if (book == null)
            {
                // Si le livre n'existe pas, retourner false ou une exception
                return false;
            }

            // Étape 2 : Supprimer physiquement l'image si elle existe
            if (!string.IsNullOrEmpty(book.CoverImagePath))
            {
                var baseImagePath = Path.Combine("wwwroot", "images", "covers");

                // Create the absolute path to the image using only the file name
                var imagePath = Path.Combine(baseImagePath, book.CoverImagePath.Split('/').Last());

                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath); // Supprimer l'image du disque
                }
            }

            // Étape 3 : Supprimer le livre de la base de données
            return _bookRepository.Delete(book);
        }
    }
}
