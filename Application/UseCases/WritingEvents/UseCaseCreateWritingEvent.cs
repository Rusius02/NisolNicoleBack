using Application.UseCases.WritingEvents.dtos;
using Application.Utils;
using Domain;
using Infrastructure.SqlServer.Repository.WritingEvents;

namespace Application.UseCases.WritingEvents
{
    public class UseCaseCreateWritingEvent : IWriting<OutputDtoCreateWritingEvent, InputDtoCreateWritingEvent>
    {
        private readonly IWritingEventRepository _writingEventRepository;
        public UseCaseCreateWritingEvent(IWritingEventRepository writingEventRepository)
        {
            _writingEventRepository = writingEventRepository;
        }

        public OutputDtoCreateWritingEvent Execute(InputDtoCreateWritingEvent dto)
        {
            var wEvFromDto = Mapper.GetInstance().Map<WritingEvent>(dto);

            var wEvFromDb = _writingEventRepository.Create(wEvFromDto);
            return Mapper.GetInstance().Map<OutputDtoCreateWritingEvent>(wEvFromDb);
        }

        public bool Execute(InputDtoUpdateWritingEvent dto)
        {
            var wEvFromDto = Mapper.GetInstance().Map<WritingEvent>(dto);
            var wEvFromDb = _writingEventRepository.Update(wEvFromDto);
            return Mapper.GetInstance().Map<bool>(wEvFromDb);
        }
    }
}
