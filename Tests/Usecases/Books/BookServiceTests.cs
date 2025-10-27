using Application.UseCases.Books;
using Domain;
using Infrastructure.SqlServer.Repository.Books;
using Moq;

namespace Tests.Usecases.Books
{
    public class BookServiceTests
    {
        [Fact]
        public void GetAllBooks_ReturnsAllBooks()
        {
            // Arrange
            var books = new List<Book>
        {
            new Book { Id = 1, Title = "Livre 1" },
            new Book { Id = 2, Title = "Livre 2" }
        };
            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(r => r.GetAll()).Returns(books);
            var service = new UseCaseListBook(mockRepo.Object);

            // Act
            var result = service.Execute();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, b => b.Title == "Livre 1");
        }

        [Fact]
        public void GetAllBooks_WhenNoBooks_ReturnsEmptyList()
        {
            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(r => r.GetAll()).Returns(new List<Book>());
            var service = new UseCaseListBook(mockRepo.Object);

            var result = service.Execute();

            Assert.Empty(result);
        }
    }
}
