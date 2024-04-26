using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Infrastructure.SqlServer.Utils;
using NotImplementedException = System.NotImplementedException;

namespace Infrastructure.SqlServer.Repository.Users
{
    public partial class UsersRepository : IUsersRepository
    {
        private readonly IDomainFactory<Domain.Users> _factory = new UsersFactory();
        
        //The Create method creates and returns an User
        public Domain.Users Create(Domain.Users user)
        {
            /*We connect to our database*/
            using var connection = Database.GetConnection();
            List<Domain.Users> users = GetAll();
            connection.Open();

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
            command.Parameters.AddWithValue("@" + ColPassword, user.Password);
            

            
            //We do a check to see if the Activity exists based on pseudo, mail and password
            foreach(Domain.Users u in users) {
                if (u.mail==user.mail && u.pseudo==user.pseudo && u.Password == user.Password)
                {
                    return null;
                }
            }
            user.Id = (int) command.ExecuteScalar();
            
            return user;
        }

        /*The GetAll method will return a List of Users*/
        public List<Domain.Users> GetAll()
        {
            var users = new List<Domain.Users>();
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
                users.Add(_factory.CreateFromSqlReader(reader));
            }

            return users;
        }
        
        //The GetUser method will return an User based on its idUser
        public Domain.Users GetUser(Domain.Users users)
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
            command.Parameters.AddWithValue("@" + ColId, users.Id);
            
            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return reader.Read() ? _factory.CreateFromSqlReader(reader) : null;
        }

        //The Delete method deletes an User based on the id
        public bool Delete(Domain.Users users)
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
            command.Parameters.AddWithValue("@" + ColId, users.Id);
            return command.ExecuteNonQuery()>0;
        }

        //The Update method allows you to modify an activity
        public bool Update(Domain.Users users)
        {
            /*We connect to our database*/
            using var connection = Database.GetConnection();
            connection.Open();

            //We call our request from the ActivityRequest class
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqUpdate
            };
            
            /*We pass the received data as an argument in our request*/
            command.Parameters.AddWithValue("@" + ColId, users.Id);
            command.Parameters.AddWithValue("@" + ColFirstName, users.FirstName);
            command.Parameters.AddWithValue("@" + ColLastName, users.LastName);
            command.Parameters.AddWithValue("@" + ColSexe, users.sexe);
            command.Parameters.AddWithValue("@" + ColBirthdate, users.BirthDate);
            command.Parameters.AddWithValue("@" + ColPseudo, users.pseudo);
            command.Parameters.AddWithValue("@" + ColMail, users.mail);
            command.Parameters.AddWithValue("@" + ColPassword, users.Password);
            return command.ExecuteNonQuery()>0;
        }

        //The GetUserByPseudo method will return an activity based on its pseudo and password
        public Domain.Users  GetUserByPseudo(string pseudo, string password)
        {
            /*We connect to our database*/
            using var connection = Database.GetConnection();
            connection.Open();

            //We call our request from the UserRequest class
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqGetByPseudo
            };

            /*We pass the received data as an argument in our request*/
            command.Parameters.AddWithValue("@" + ColPseudo, pseudo);
            command.Parameters.AddWithValue("@" + ColPassword, password);
            
            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return reader.Read() ? _factory.CreateFromSqlReader(reader) : null;
        }
    }
}