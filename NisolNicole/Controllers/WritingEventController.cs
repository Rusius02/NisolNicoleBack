using Application.UseCases.WritingEvents;
using Application.UseCases.WritingEvents.dtos;
using Microsoft.AspNetCore.Mvc;

namespace NisolNicole.Controllers
{

    [ApiController]
    [Route("api/WritingEvent")]
    public class WritingEventController : ControllerBase
    {
        private readonly UseCaseCreateWritingEvent _useCaseCreateWritingEvent;
        private readonly UseCaseDeleteWritingEvent _useCaseDeleteWritingEvent;

        public WritingEventController(UseCaseCreateWritingEvent useCaseCreateWritingEvent, UseCaseDeleteWritingEvent useCaseDeleteWritingEvent)
        {
            _useCaseCreateWritingEvent = useCaseCreateWritingEvent;
            _useCaseDeleteWritingEvent = useCaseDeleteWritingEvent;
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult<OutputDtoCreateWritingEvent> Create([FromBody] InputDtoCreateWritingEvent  dtoCreateWritingEvent)
        {
            return StatusCode(201, _useCaseCreateWritingEvent.Execute(dtoCreateWritingEvent));
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [Route("Update")]
        public ActionResult<bool> Update([FromBody] InputDtoUpdateWritingEvent inputDtoUpdateWritingEvent)
        {
            return StatusCode(200, _useCaseCreateWritingEvent.Execute(inputDtoUpdateWritingEvent));
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        public ActionResult<bool> Delete(int id)
        {
            return StatusCode(200, _useCaseDeleteWritingEvent.Execute(new InputDtoDeleteWritingEvent()
            {
                Id = id
            }));
        }

    }
}
