namespace Infrastructure.SqlServer.Repository.SiteTraffic
{
    partial class SiteTrafficRepository
    {
        public const string TableName = "siteVisit",
           ColId = "idSiteVisit",
           ColDate = "visitDate",
           ColIP= "IPAdress";

        //We have all our queries here 
        //Create query which creates a database User
        public static readonly string ReqCreate = $@"
        INSERT INTO {TableName}({ColDate}, {ColIP})
        OUTPUT INSERTED.{ColId}
        VALUES(@{ColDate}, @{ColIP})";

        //This is the one that will send us all the User
        public static readonly string ReqGetAll = $@"
        SELECT * FROM {TableName}";

        //This is the one that will send us all the activities based on Id
        public static readonly string ReqGetById = $@"
        SELECT * FROM {TableName}
        WHERE {ColId} = @{ColId}";

        public static readonly string ReqGetByIPAdress = $@"
        SELECT * FROM {TableName}
        WHERE {ColIP} = @{ColIP}";

    }
}
