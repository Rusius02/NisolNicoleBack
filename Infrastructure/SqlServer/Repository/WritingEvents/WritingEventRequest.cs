namespace Infrastructure.SqlServer.Repository.WritingEvents
{
    public partial class WritingEventRepository
    {
        public const string TableName = "WritingEvent",
            ColId = "id_writing_event",
            ColName = "name",
            ColTheme = "theme",
            ColDescription = "description",
            ColStartDate = "start_date",
            ColEndDate = "end_date";

        public static readonly string ReqCreate = $@"
        INSERT INTO {TableName}({ColName}, {ColTheme},{ColDescription}, 
        {ColStartDate},  {ColEndDate})
        OUTPUT INSERTED.{ColId}
        VALUES(@{ColName}, @{ColDescription}, {ColTheme}, @{ColStartDate}, @{ColEndDate})";

        public static readonly string ReqGetAll = $@"
        SELECT * FROM {TableName}";

        public static readonly string ReqGetById = $@"
        SELECT * FROM {TableName}
        WHERE {ColId} = @{ColId}";

        public static readonly string ReqDelete = $@"
            DELETE FROM {TableName} WHERE {ColId} = @{ColId}";

        public static readonly string ReqUpdate = $@"
            UPDATE {TableName}
            SET {ColName} = @{ColName}, {ColDescription} = @{ColDescription},
            {ColTheme} = @{ColTheme},
            {ColStartDate} = @{ColStartDate},{ColEndDate} = @{ColEndDate}
            WHERE {ColId} = @{ColId}";

        public static readonly string ReqGetByPseudo = $@"
        SELECT * FROM {TableName}
        WHERE {ColName} = @{ColName}";

    }
}
