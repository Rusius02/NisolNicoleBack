using Application.UseCases.WritingEvents.dtos;
using Application.Utils;
using Domain;
using Infrastructure.SqlServer.Repository.WritingEvents;

namespace Application.UseCases.WritingEvents
{
    public class UseCaseDeleteWritingEvent
    {
        private readonly IWritingEventRepository _writingEventRepository;
        public UseCaseDeleteWritingEvent(IWritingEventRepository writingEventRepository)
        {
            _writingEventRepository = writingEventRepository;
        }
        public bool Execute(InputDtoDeleteWritingEvent dto)
        {
            var wEvFromDto = Mapper.GetInstance().Map<WritingEvent>(dto);
            var wEvFromDB = _writingEventRepository.Delete(wEvFromDto);
            return Mapper.GetInstance().Map<bool>(wEvFromDB);
        }
    }
}
