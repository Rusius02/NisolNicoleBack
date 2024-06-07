namespace Application.UseCases.WritingEvents.dtos
{
    public class InputDtoCreateWritingEvent
    {
        public string Name { get; set; }
        public string? Theme { get; set; }
        public string Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
