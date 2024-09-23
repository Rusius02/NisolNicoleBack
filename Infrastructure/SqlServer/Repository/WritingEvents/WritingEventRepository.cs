using Domain;
using Infrastructure.SqlServer.Utils;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.SqlServer.Repository.WritingEvents
{
    public partial class WritingEventRepository : IWritingEventRepository
    {
        private readonly IDomainFactory<WritingEvent> _factory = new WritingEventFactory();
        public WritingEvent? Create(WritingEvent writingEvent)
        {
            using var connection = Database.GetConnection();
            List<WritingEvent> writingEvents = GetAll();
            connection.Open();
            if (WritingEventExists(writingEvent.Id))
            {
                return null; 
            }

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqCreate
            };
            command.Parameters.AddWithValue("@" + ColName, writingEvent.Name);
            command.Parameters.AddWithValue("@" + ColDescription, writingEvent.Description);
            if (writingEvent.Theme != null)
            {
                command.Parameters.AddWithValue("@" + ColTheme, writingEvent.Theme);
            }
            else
            {
                command.Parameters.AddWithValue("@" + ColTheme, DBNull.Value);
            }
            command.Parameters.AddWithValue("@" + ColStartDate, writingEvent.StartDate);
            command.Parameters.AddWithValue("@" + ColEndDate, writingEvent.EndDate);

            writingEvent.Id = (int)command.ExecuteScalar();

            return writingEvent;
        }

        public bool Delete(WritingEvent writingEvent)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqDelete
            };
            command.Parameters.AddWithValue("@" + ColId, writingEvent.Id);
            return command.ExecuteNonQuery() > 0;
        }

        public List<WritingEvent> GetAll()
        {
            var writingEvents = new List<WritingEvent>();
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqGetAll
            };

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                writingEvents.Add(_factory.CreateFromSqlReader(reader));
            }

            return writingEvents;
        }

        public WritingEvent GetWritingEvent(WritingEvent writingEvent)
        {
            using var connection = Database.GetConnection();
            connection.Open();

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = ReqGetById
            };

            command.Parameters.AddWithValue("@" + ColId, writingEvent.Id);

            var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return reader.Read() ? _factory.CreateFromSqlReader(reader) : null;
        }

        public bool Update(WritingEvent writingEvent)
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
            command.Parameters.AddWithValue("@" + ColId, writingEvent.Id);
            command.Parameters.AddWithValue("@" + ColName, writingEvent.Name);
            command.Parameters.AddWithValue("@" + ColDescription, writingEvent.Description);
            command.Parameters.AddWithValue("@" + ColTheme, writingEvent.Theme);
            command.Parameters.AddWithValue("@" + ColStartDate, writingEvent.StartDate);
            command.Parameters.AddWithValue("@" + ColEndDate, writingEvent.EndDate);
            return command.ExecuteNonQuery() > 0;
        }
        private bool WritingEventExists(int id)
        {
            List<WritingEvent> writingEvents = GetAll();

            foreach (WritingEvent w in writingEvents)
            {
                if (w.Id == id)
                {
                    return true; 
                }
            }

            return false; 
        }
    }
}
