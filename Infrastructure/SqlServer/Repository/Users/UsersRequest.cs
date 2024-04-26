namespace Infrastructure.SqlServer.Repository.Users
{
    public partial class UsersRepository
    {
        public const string TableName = "users",
            ColId = "idUser",
            ColLastName = "last_name",
            ColFirstName = "first_name",
            ColSexe = "sexe",
            ColMail = "mail",
            ColPseudo = "pseudo",
            ColPassword = "password",
            ColBirthdate = "birthdate",
            ColRole = "role";
    
        //We have all our queries here 
        //Create query which creates a database User
        public static readonly string ReqCreate = $@"
        INSERT INTO {TableName}({ColLastName}, {ColFirstName}, 
        {ColSexe},  {ColBirthdate}, {ColPseudo},{ColMail},{ColPassword})
        OUTPUT INSERTED.{ColId}
        VALUES(@{ColLastName}, @{ColFirstName}, @{ColSexe}, @{ColBirthdate}, 
        @{ColPseudo}, @{ColMail} ,@{ColPassword})";

        //This is the one that will send us all the User
        public static readonly string ReqGetAll = $@"
        SELECT * FROM {TableName}";

        //This is the one that will send us all the activities based on Id
        public static readonly string ReqGetById = $@"
        SELECT * FROM {TableName}
        WHERE {ColId} = @{ColId}";
        
        //Delete query which deletes the data based on the id
        public static readonly string ReqDelete = $@"
            DELETE FROM {TableName} WHERE {ColId} = @{ColId}";

        // The Update request which allows to modify an activity in the database
        public static readonly string ReqUpdate = $@"
            UPDATE {TableName}
            SET {ColFirstName} = @{ColFirstName}, {ColLastName} = @{ColLastName}, 
            {ColSexe} = @{ColSexe},{ColBirthdate} = @{ColBirthdate}, {ColPseudo} = @{ColPseudo},
            {ColMail} = @{ColMail}, {ColPassword} = @{ColPassword}
            WHERE {ColId} = @{ColId}";
        
        //This is the one that will send us all the activities based on Pseudo et Password
        public static readonly string ReqGetByPseudo = $@"
        SELECT * FROM {TableName}
        WHERE {ColPseudo} = @{ColPseudo} AND {ColPassword} = @{ColPassword}";
            
    }
}