using FalconeOne.BLL.Helpers;
using FalconeOne.BLL.Interfaces;
using FalconOne.DAL.Contracts;
using FalconOne.Helpers.Helpers;
using FalconOne.Models.Dtos.Mail;
using FalconOne.Models.Entities.Users;
using IdenticonSharp.Identicons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace FalconeOne.BLL.Services
{
    public class MailService : BaseService, IMailService
    {
        public MailService(UserManager<User> userManager, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ITenantProvider tenantService, IIdenticonProvider identiconProvider) : base(userManager, unitOfWork, httpContextAccessor, configuration, tenantService, identiconProvider)
        {
        }

        public async Task<ViewMailInfoDto> GetMailAsync(Guid mailId, CancellationToken cancellationToken)
        {
             var result = await _unitOfWork.MailRepository.GetMailInfoAsync(mailId, cancellationToken);

            return result;
        }

        public async Task<PagedList<MailItemDto>> GetSentMailsAsync(FilterUserEmailsDto model, CancellationToken cancellationToken)
        {
            var res =  await _unitOfWork.MailRepository.GetUserSentEmails(TenantId, model, cancellationToken);
            return res;
        }

        public async Task<PagedList<MailItemDto>> GetReceivedMailsAsync(FilterUserEmailsDto model, CancellationToken cancellationToken)
        {
            var res =  await _unitOfWork.MailRepository.GetUserReceivedEmails(TenantId, model, cancellationToken);


            return res;
        }

        public async Task<bool> NewMailAsync(NewMailDto model, CancellationToken cancellationToken)
        {
            var validSender = await IsValidSenderOrReceipient(model.SenderId, cancellationToken);

            if (!validSender)
            {
                throw new Exception(MessageHelper.MailErrors.INVALID_SENDER);
            }

            if (!model.ToRecipients.Any())
            {
                throw new Exception(MessageHelper.MailErrors.NO_RECEIPIENT_SELECTED);
            }

            var toReceipients = await SanitizeRecipients(model.ToRecipients, cancellationToken);
            var ccReceipients = await SanitizeRecipients(model.CcRecipients, cancellationToken);
            var bccReceipients = await SanitizeRecipients(model.BccRecipients, cancellationToken);

            model.ToRecipients = toReceipients;
            model.CcRecipients = ccReceipients;
            model.BccRecipients = bccReceipients;

            var result = await _unitOfWork.MailRepository.NewMailAsync(model, cancellationToken);

            return result;
        }

        #region Private methods
        private async Task<List<Guid>> SanitizeRecipients(List<Guid> receipients, CancellationToken cancellationToken)
        {
            var sanitizedReceipients = new List<Guid>();

            foreach (var receipient in receipients.Distinct())
            {
                var isValidReceipient = await IsValidSenderOrReceipient(receipient, cancellationToken);

                if (isValidReceipient)
                {
                    sanitizedReceipients.Add(receipient);
                }
            }
            return sanitizedReceipients;
        }

        private async Task<bool> IsValidSenderOrReceipient(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, cancellationToken);

            var isValidUser = user.IsActive && !user.IsDeleted && user.EmailConfirmed;

            return isValidUser;
        }

        #endregion
    }
}
