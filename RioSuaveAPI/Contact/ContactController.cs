using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RioSuaveLib;

namespace RioSuaveAPI.Contact
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        public ContactController(ILogger<ContactController> logger, EmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]ContactForm contactForm)
        {
            _logger.LogInformation(contactForm.Name + ' ' + contactForm.Email + ' ' + contactForm.Comments);
            _emailService.Send("keelerwebdev@gmail.com", contactForm.Email, "New Message From: " + contactForm.Email, contactForm.Comments);
        }

        private readonly ILogger<ContactController> _logger;
        private readonly EmailService _emailService;
    }
}