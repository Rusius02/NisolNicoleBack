using Domain;

namespace Infrastructure.SqlServer.Repository.WritingEvents
{
    public interface IWritingEventRepository
    {
        WritingEvent? Create(WritingEvent writingEvent);

        List<WritingEvent> GetAll();

        WritingEvent GetWritingEvent(WritingEvent writingEvent);

        bool Delete(WritingEvent writingEvent);

        bool Update(WritingEvent writingEvent);
    }
}
