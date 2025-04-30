namespace Infrastructure.SqlServer.Repository.Books
{
    public partial class BookRepository
    {
        public const string TableName = "book",
            ColId = "idBook",
            ColTitle = "name",
            ColDescription = "description",
            ColISBN = "ISBN",
            ColPrice = "price",
            ColCoverImagePath = "CoverImagePath",
            ColStripeProductId = "StripeProductId";

        //We have all our queries here 
        //Create query which creates a database User
        public static readonly string ReqCreate = $@"
        INSERT INTO {TableName}({ColTitle}, {ColDescription}, 
        {ColISBN},  {ColPrice}, {ColCoverImagePath}, {ColStripeProductId})
        OUTPUT INSERTED.{ColId}
        VALUES(@{ColTitle}, @{ColDescription}, @{ColISBN}, @{ColPrice}, @{ColCoverImagePath}, @{ColStripeProductId})";

        //This is the one that will send us all the User
        public static readonly string ReqGetAll = $@"
        SELECT * FROM {TableName}";

        //This is the one that will send us all the activities based on Id
        public static readonly string ReqGetById = $@"
        SELECT * FROM {TableName}
        WHERE {ColId} = @{ColId}";

        public static readonly string ReqGetByStripeId = $@"
        SELECT * FROM {TableName}
        WHERE {ColStripeProductId} = @{ColStripeProductId}";

        //Delete query which deletes the data based on the id
        public static readonly string ReqDelete = $@"
            DELETE FROM {TableName} WHERE {ColId} = @{ColId}";

        // The Update request which allows to modify an activity in the database
        public static readonly string ReqUpdate = $@"
            UPDATE {TableName}
            SET {ColTitle} = @{ColTitle}, {ColDescription} = @{ColDescription}, 
            {ColISBN} = @{ColISBN},{ColPrice} = @{ColPrice}, {ColCoverImagePath} = @{ColCoverImagePath}, {ColStripeProductId} = @{ColStripeProductId}
            WHERE {ColId} = @{ColId}";

        //This is the one that will send us all the activities based on Pseudo et Password
        public static readonly string ReqGetByPseudo = $@"
        SELECT * FROM {TableName}
        WHERE {ColTitle} = @{ColTitle}";

    }
}