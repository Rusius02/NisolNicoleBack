namespace Infrastructure.SqlServer.Repository.ShippingInfos
{
    public partial class ShippingInfosRepository
    {
        public const string TableName = "shipping_infos",
         ColId = "id",
         ColFullName = "full_name",
         ColAddressNumber = "address_number",
         ColAddressCity = "address_city",
         ColAddressZip = "address_zip",
         ColAddressStreet = "address_street",
         ColAddressCountry = "address_country",
         ColMail = "mail",
         ColShippingMethod = "ShippingMethod",
         ColOrderId = "order_id";

        //We have all our queries here 
        //Create query which creates a database User
        public static readonly string ReqCreate = $@"
        INSERT INTO {TableName}({ColFullName}, {ColAddressNumber}, {ColAddressCity}, {ColAddressZip}, {ColAddressStreet}, {ColAddressCountry}, {ColMail}, {ColShippingMethod}, 
        {ColOrderId})
        OUTPUT INSERTED.{ColId}
        VALUES(@{ColFullName}, @{ColAddressNumber}, @{ColAddressCity}, @{ColAddressZip}, @{ColAddressStreet}, @{ColAddressCountry}, @{ColMail}, @{ColShippingMethod}, 
        @{ColOrderId})";

        //This is the one that will send us all the User
        public static readonly string ReqGetAll = $@"
        SELECT * FROM {TableName}";

        //This is the one that will send us all the activities based on Id
        public static readonly string ReqGetById = $@"
        SELECT * FROM {TableName}
        WHERE {ColId} = @{ColId}";

    }
}
