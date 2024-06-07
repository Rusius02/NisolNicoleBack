using Domain;
using Infrastructure.SqlServer.Utils;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.SqlServer.Repository.Books
{
    public partial class BookRepository : IBookRepository
    {
        private readonly IDomainFactory<Book> _factory = new BookFactory();
        public Book Create(Book book)
        {
            /*We connect to our database*/
            using var connection = Database.GetConnection();
            List<Book> books = GetAll();
            connection.Open(); 
            if (BookExists(book.ISBN))
            {
                return null; // User already exists, return null
            }
            
            //We call our request from the UserRequest class
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqCreate
            };
            /*We pass the received data as an argument in our request*/
            command.Parameters.AddWithValue("@" + ColName, book.Name);
            command.Parameters.AddWithValue("@" + ColDescription, book.Description);
            command.Parameters.AddWithValue("@" + ColISBN, book.ISBN);
            command.Parameters.AddWithValue("@" + ColPrice, book.Price);

            book.Id = (int)command.ExecuteScalar();

            return book;
        }

        public bool Delete(Book book)
        {
            /*We connect to our database*/
            using var connection = Database.GetConnection();
            connection.Open();

            //We call our request from the UserRequest class
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqDelete
            };
            /*We pass the received data as an argument in our request*/
            command.Parameters.AddWithValue("@" + ColId, book.Id);
            return command.ExecuteNonQuery() > 0;
        }

        public List<Book> GetAll()
        {
            var books = new List<Book>();
            /*We connect to our database*/
            using var connection = Database.GetConnection();
            connection.Open();

            //We call our request from the ActivityRequest class
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqGetAll
            };

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            //We get our values and add them to the List 
            while (reader.Read())
            {
                books.Add(_factory.CreateFromSqlReader(reader));
            }

            return books;
        }

        public Book GetBook(Book book)
        {
            /*We connect to our database*/
            using var connection = Database.GetConnection();
            connection.Open();

            //We call our request from the SportRequest class
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqGetById
            };

            /*We pass the received data as an argument in our request*/
            command.Parameters.AddWithValue("@" + ColId, book.Id);

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return reader.Read() ? _factory.CreateFromSqlReader(reader) : null;
        }

        public bool Update(Book book)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            //We call our request from the ActivityRequest class
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqUpdate
            };

            /*We pass the received data as an argument in our request*/
            command.Parameters.AddWithValue("@" + ColId, book.Id);
            command.Parameters.AddWithValue("@" + ColName, book.Name);
            command.Parameters.AddWithValue("@" + ColDescription, book.Description);
            command.Parameters.AddWithValue("@" + ColISBN, book.ISBN);
            command.Parameters.AddWithValue("@" + ColPrice, book.Price);
            return command.ExecuteNonQuery() > 0;
        }

        private bool BookExists(string isbn)
        {
            List<Book> books = GetAll();

            foreach (Book u in books)
            {
                if (u.ISBN == isbn )
                {
                    return true; // User already exists
                }
            }

            return false; // User does not exist
        }
    }
}
