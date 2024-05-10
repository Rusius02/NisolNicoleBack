using Domain;
using Infrastructure.SqlServer.Utils;
using System.Data.SqlClient;

namespace Infrastructure.SqlServer.Repository.Books
{
    public partial class BookRepository : IBookRepository
    {
        private readonly IDomainFactory<Book> _factory = new BookFactory();
        Book IBookRepository.Create(Book book)
        {
            /*We connect to our database*/
            using var connection = Database.GetConnection();
            List<Book> users = GetAll();
            connection.Open();
            if (UserExists(user.mail, user.pseudo))
            {
                return null; // User already exists, return null
            }
            // Hash the password before storing it
            string hashedPassword = HashPassword(user.Password);

            //We call our request from the UserRequest class
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqCreate
            };
            /*We pass the received data as an argument in our request*/
            command.Parameters.AddWithValue("@" + ColLastName, user.LastName);
            command.Parameters.AddWithValue("@" + ColFirstName, user.FirstName);
            command.Parameters.AddWithValue("@" + ColSexe, user.sexe);
            command.Parameters.AddWithValue("@" + ColBirthdate, user.BirthDate);
            command.Parameters.AddWithValue("@" + ColPseudo, user.pseudo);
            command.Parameters.AddWithValue("@" + ColMail, user.mail);
            command.Parameters.AddWithValue("@" + ColPassword, hashedPassword);

            user.Id = (int)command.ExecuteScalar();

            return user;
        }

        bool IBookRepository.Delete(Book book)
        {
            throw new NotImplementedException();
        }

        List<Book> IBookRepository.GetAll()
        {
            throw new NotImplementedException();
        }

        Book IBookRepository.GetUser(Book book)
        {
            throw new NotImplementedException();
        }

        bool IBookRepository.Update(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
