using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RioSuaveLib;

namespace RioSuaveAPI.MailingList
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailingListController : ControllerBase
    {
        public MailingListController(ILogger<MailingListController> logger, EmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Email email)
        {
            _logger.LogInformation(email.Address);
            _emailService.Send("keelerwebdev@gmail.com", email.Address, "Mailing List Request For: " + email.Address, email.Address + " would like to join the mailing list.");
        }

        private readonly ILogger<MailingListController> _logger;
        private readonly EmailService _emailService;
    }
}