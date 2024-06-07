namespace Infrastructure.SqlServer.Repository.Books
{
    public partial class BookRepository
    {
        public const string TableName = "Book",
            ColId = "idBook",
            ColName = "name",
            ColDescription = "description",
            ColISBN = "ISBN",
            ColPrice = "price";

        //We have all our queries here 
        //Create query which creates a database User
        public static readonly string ReqCreate = $@"
        INSERT INTO {TableName}({ColName}, {ColDescription}, 
        {ColISBN},  {ColPrice})
        OUTPUT INSERTED.{ColId}
        VALUES(@{ColName}, @{ColDescription}, @{ColISBN}, @{ColPrice})";

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
            SET {ColName} = @{ColName}, {ColDescription} = @{ColDescription}, 
            {ColISBN} = @{ColISBN},{ColPrice} = @{ColPrice}
            WHERE {ColId} = @{ColId}";

        //This is the one that will send us all the activities based on Pseudo et Password
        public static readonly string ReqGetByPseudo = $@"
        SELECT * FROM {TableName}
        WHERE {ColName} = @{ColName}";

    }
}