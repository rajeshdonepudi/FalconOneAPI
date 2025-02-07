using FalconOne.Helpers.Helpers;
using FalconOne.Models.Dtos.Mail;
using FalconOne.Models.Entities.Mails;
using FalconOne.Models.EntityContracts;

namespace FalconOne.DAL.Contracts
{
    public interface IMailRepository : IGenericRepository<Mail>
    {
        Task<PagedList<MailItemDto>> GetUserReceivedEmails(Guid tenantId, FilterUserEmailsDto model, CancellationToken cancellationToken);
        Task<PagedList<MailItemDto>> GetUserSentEmails(Guid tenantId, FilterUserEmailsDto model, CancellationToken cancellationToken);
        Task<bool> NewMailAsync(NewMailDto model, CancellationToken cancellationToken);
        Task<ViewMailInfoDto> GetMailInfoAsync(Guid mailId, CancellationToken cancellationToken);
        Task<PagedList<MailItemDto>> GetUserReceivedEmails(FilterUserEmailsDto model, CancellationToken cancellationToken);
    }
}
