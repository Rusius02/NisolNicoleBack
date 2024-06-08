using Application.UseCases.Books.Dtos;
using Application.UseCases.WritingEvents.dtos;
using Application.Utils;
using Domain;
using Infrastructure.SqlServer.Repository.WritingEvents;

namespace Application.UseCases.WritingEvents
{
    public class UseCaseListWritingEvent
    {
        private readonly IWritingEventRepository  _writingEventRepository;

        public UseCaseListWritingEvent(IWritingEventRepository writingEventRepository)
        {
            _writingEventRepository = writingEventRepository;
        }

        public List<OutputDtoWritingEvent> Execute()
        {
            List<WritingEvent> writingEvents = _writingEventRepository.GetAll();
            return Mapper.GetInstance().Map<List<OutputDtoWritingEvent>>(writingEvents);
        }

        public OutputDtoWritingEvent Execute(InputDtoWritingEvent dto)
        {
            var wEvFromDto = Mapper.GetInstance().Map<WritingEvent>(dto);
            WritingEvent writingEvents = _writingEventRepository.GetWritingEvent(wEvFromDto);
            return Mapper.GetInstance().Map<OutputDtoWritingEvent>(writingEvents);
        }
    }
}
