using FalconeOne.BLL.Interfaces;
using FalconOne.Models.Dtos.Mail;
using Microsoft.AspNetCore.Mvc;

namespace FalconOne.API.Controllers.Mail
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailReaderController : ControllerBase
    {
        private readonly IMailReaderService _mailReaderService;

        public MailReaderController(IMailReaderService mailReaderService)
        {
            _mailReaderService = mailReaderService;
        }

        [HttpPost("read-message")]
        public async Task<IActionResult> Get(ReadMessageRequestDto model)
        {
            var result = await _mailReaderService.GetSingleEmailByUid(model, model.MessageId);

            return Ok(result);
        }

        [HttpPost("all/read")]
        public async Task<IActionResult> GetAll(ReadMessageRequestDto model)
        {
            var result = await _mailReaderService.GetEmailMessages(model);

            return Ok(result);
        }

        [HttpGet("download-attachment")]
        public async Task<IActionResult> DownloadAttachment([FromQuery] string attachmentId,[FromQuery] string name)
        {
            try
            {
                var memoryStream = await _mailReaderService.DownloadAttachment(attachmentId);

                return File(memoryStream, "application/octet-stream", name);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
