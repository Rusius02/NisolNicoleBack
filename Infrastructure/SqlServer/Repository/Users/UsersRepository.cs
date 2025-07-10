using System.Data;
using System.Data.SqlClient;
using Infrastructure.SqlServer.Utils;

namespace Infrastructure.SqlServer.Repository.Users
{
    public partial class UsersRepository : IUsersRepository
    {
        private readonly IDomainFactory<Domain.Users> _factory = new UsersFactory();
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        private bool UserExists(string email, string username)
        {
            List<Domain.Users> users = GetAll();

            foreach (Domain.Users u in users)
            {
                if (u.Mail == email || u.Pseudo == username)
                {
                    return true; // User already exists
                }
            }

            return false; // User does not exist
        }
        //The Create method creates and returns an User
        public Domain.Users? Create(Domain.Users user)
        {
            /*We connect to our database*/
            using var connection = Database.GetConnection();
            List<Domain.Users> users = GetAll();
            connection.Open();
            if (UserExists(user.Mail, user.Pseudo))
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
            command.Parameters.AddWithValue("@" + ColSexe, user.Sexe);
            command.Parameters.AddWithValue("@" + ColBirthdate, user.BirthDate);
            command.Parameters.AddWithValue("@" + ColPseudo, user.Pseudo);
            command.Parameters.AddWithValue("@" + ColMail, user.Mail);
            command.Parameters.AddWithValue("@" + ColPassword, hashedPassword);
            command.Parameters.AddWithValue("@" + ColAddressStreet, user.AddressStreet);
            command.Parameters.AddWithValue("@" + ColAddressNumber, user.AddressNumber);
            command.Parameters.AddWithValue("@" + ColAddressCity, user.AddressCity);
            command.Parameters.AddWithValue("@" + ColAddressZip, user.AddressZip);
            command.Parameters.AddWithValue("@" + ColAddressCountry, user.AddressCountry);

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
        public Domain.Users? GetUser(Domain.Users users)
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
            command.Parameters.AddWithValue("@" + ColSexe, users.Sexe);
            command.Parameters.AddWithValue("@" + ColBirthdate, users.BirthDate);
            command.Parameters.AddWithValue("@" + ColPseudo, users.Pseudo);
            command.Parameters.AddWithValue("@" + ColMail, users.Mail);
            command.Parameters.AddWithValue("@" + ColPassword, users.Password);
            command.Parameters.AddWithValue("@" + ColAddressStreet, users.AddressStreet);
            command.Parameters.AddWithValue("@" + ColAddressNumber, users.AddressNumber);
            command.Parameters.AddWithValue("@" + ColAddressCity, users.AddressCity);
            command.Parameters.AddWithValue("@" + ColAddressZip, users.AddressZip);
            command.Parameters.AddWithValue("@" + ColAddressCountry, users.AddressCountry);
            return command.ExecuteNonQuery()>0;
        }

        //The GetUserByPseudo method will return an activity based on its pseudo and password
        public Domain.Users? GetUserByPseudo(string pseudo, string password)
        {
            // Connect to the database
            using var connection = Database.GetConnection();
            connection.Open();

            // Prepare SQL command to retrieve the user by username
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqGetByPseudo
            };

            // Set parameters for the SQL command
            command.Parameters.AddWithValue("@" + ColPseudo, pseudo);

            // Execute SQL command and retrieve the user's data
            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            if (reader.Read())
            {
                // If user found, retrieve hashed password from the database
                string hashedPasswordFromDb = reader["password"] as string
                    ?? throw new InvalidOperationException("Le mot de passe est null dans la base.");

                // Verify if the given password matches the hashed password from the database
                if (BCrypt.Net.BCrypt.Verify(password, hashedPasswordFromDb))
                {
                    // Passwords match, return the user
                    return _factory.CreateFromSqlReader(reader);
                }
            }

            // If user not found or passwords do not match, return null
            return null;
        }



        public void HashPasswordsForAllUsers()
        {
            // Connect to the database
            using var connection = Database.GetConnection();
            connection.Open();

            // Retrieve all user records from the database
            var selectCommand = new SqlCommand
            {
                Connection = connection,
                CommandText = "SELECT idUser, Password FROM Users"
            };

            var reader = selectCommand.ExecuteReader();

            while (reader.Read())
            {
                // Retrieve user information
                int userId = (int)reader["idUser"];
                string currentPassword = reader["Password"] as String
                    ?? throw new InvalidOperationException("Mot de passe null dans la DB");

                // Check if the current password is already hashed
                if (!IsHashedPassword(currentPassword))
                {
                    // Close the existing DataReader before executing the update command
                    reader.Close();

                    // Hash the current password using bcrypt
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(currentPassword, BCrypt.Net.BCrypt.GenerateSalt());

                    // Update the user record in the database with the hashed password
                    var updateCommand = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "UPDATE Users SET Password = @HashedPassword WHERE idUser = @UserId"
                    };

                    updateCommand.Parameters.AddWithValue("@HashedPassword", hashedPassword);
                    updateCommand.Parameters.AddWithValue("@UserId", userId);

                    updateCommand.ExecuteNonQuery();

                    // Re-execute the select command to continue reading user records
                    reader = selectCommand.ExecuteReader();
                }
            }

            // Close the DataReader after processing
            reader.Close();
        }

        // Method to check if a password is already hashed
        private bool IsHashedPassword(string password)
        {
            // Check if the password starts with the bcrypt identifier
            return password.StartsWith("$2a$");
        }

    }
}