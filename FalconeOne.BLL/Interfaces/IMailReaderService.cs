using Chilkat;
using FalconOne.Models.Dtos.Mail;

namespace FalconeOne.BLL.Interfaces
{
    public interface IMailReaderService
    {
        Task<MemoryStream> DownloadAttachment(string attachmentId);
        Task<IEnumerable<EmailMessageItemDto>> GetEmailMessages(ReadMessageRequestDto model);
        Task<EmailMessageItemDto> GetSingleEmailByUid(ReadMessageRequestDto model, uint uid);
    }
}