using Application.UseCases.Contact.dtos;
using Infrastructure.Services.Contact;

namespace Application.UseCases.NewFolder
{
    public class UseCaseContactAuthorByMail
    {
        private readonly IEmailService _emailService;

        public UseCaseContactAuthorByMail(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Execute(InputContactFormDto input)
        {
            var to = "adrien.stievenart@live.be"; 
            var subject = $"Contact from {input.Name}";
            var body = $"Name: {input.Name}\nEmail: {input.Email}\nMessage:\n{input.Message}";

            await _emailService.SendEmailAsync(to, subject, body);
        }
    }
}
