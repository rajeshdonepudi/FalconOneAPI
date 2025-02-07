using FalconeOne.BLL.Interfaces;
using FalconOne.Models.Dtos.Mail;
using Microsoft.AspNetCore.Mvc;

namespace FalconOne.API.Controllers.Mail
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpGet("view-mail")]
        public async Task<IActionResult> ViewMail([FromQuery] Guid mailId, CancellationToken cancellationToken)
        {
            var info = await _mailService.GetMailAsync(mailId, cancellationToken);

            return Ok(info);
        }

        [HttpPost("sent-mails")]
        public async Task<IActionResult> GetSentMails(FilterUserEmailsDto model, CancellationToken cancellationToken)
        {
            var result = await _mailService.GetSentMailsAsync(model, cancellationToken);

            return Ok(result);
        }

        [HttpPost("received-mails")]
        public async Task<IActionResult> GetReceivedMails(FilterUserEmailsDto model, CancellationToken cancellationToken)
        {
            var result = await _mailService.GetReceivedMailsAsync(model, cancellationToken);

            return Ok(result);
        }

        [HttpPost("new-mail")]
        public async Task<IActionResult> NewMail([FromBody] NewMailDto model, CancellationToken cancellationToken)
        {
            var result = await _mailService.NewMailAsync(model, cancellationToken);

            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
