using FalconOne.Helpers.Helpers;
using FalconOne.Models.Dtos.Mail;

namespace FalconeOne.BLL.Interfaces
{
    public interface IMailService
    {
        Task<PagedList<MailItemDto>> GetReceivedMailsAsync(FilterUserEmailsDto model, CancellationToken cancellationToken);
        Task<PagedList<MailItemDto>> GetSentMailsAsync(FilterUserEmailsDto model, CancellationToken cancellationToken);
        Task<bool> NewMailAsync(NewMailDto model, CancellationToken cancellationToken);
        Task<ViewMailInfoDto> GetMailAsync(Guid mailId, CancellationToken cancellationToken);
    }
}
