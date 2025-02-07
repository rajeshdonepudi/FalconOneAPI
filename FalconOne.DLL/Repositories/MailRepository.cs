using FalconOne.DAL.Contracts;
using FalconOne.Enumerations.Mail;
using FalconOne.Extensions.EntityFramework;
using FalconOne.Helpers.Helpers;
using FalconOne.Models.Dtos.Mail;
using FalconOne.Models.Entities.Mails;
using FalconOne.Models.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace FalconOne.DAL.Repositories
{
    public class MailRepository : GenericRepository<Mail>, IMailRepository
    {
        public MailRepository(FalconOneContext context, IMemoryCache cache) : base(context, cache)
        {
        }

        public async Task<PagedList<MailItemDto>> GetUserReceivedEmails(Guid tenantId, FilterUserEmailsDto model, CancellationToken cancellationToken)
        {
            var query = _context.Users.Where(x => x.Id == model.UserId && x.IsActive && !x.IsDeleted && x.Tenants.Any(x => x.TenantId == tenantId))
                                            .SelectMany(x => x.ReceivedMails)
                                            .OrderByDescending(x => x.Mail.SentDate)
                                            .Select(x => new MailItemDto
                                            {
                                                Id = x.Mail.Id,
                                                Body = x.Mail.Body,
                                                Subject = x.Mail.Subject,
                                                SenderInfo = new MailSenderInfoDto
                                                {
                                                    Avatar = x.Mail.Sender.ProfilePicture.Base64,
                                                    Email = x.Mail.Sender.Email
                                                }
                                            });

            if (!string.IsNullOrEmpty(model.SearchTerm))
            {
                query = query.Where(x => x.Body.Contains(model.SearchTerm) || x.Subject.Contains(model.SearchTerm));
            }

            return await query.ToPagedListAsync(model, cancellationToken);
        }

        public async Task<PagedList<MailItemDto>> GetUserReceivedEmails(FilterUserEmailsDto model, CancellationToken cancellationToken)
        {
            var query = _context.Users.Where(x => x.Id == model.UserId && x.IsActive && !x.IsDeleted)
                                            .SelectMany(x => x.ReceivedMails)
                                            .OrderByDescending(x => x.Mail.SentDate)
                                            .Select(x => new MailItemDto
                                            {
                                                Id = x.Mail.Id,
                                                Body = x.Mail.Body,
                                                Subject = x.Mail.Subject,
                                                SenderInfo = new MailSenderInfoDto
                                                {
                                                    Avatar = x.Mail.Sender.ProfilePicture.Base64,
                                                    Email = x.Mail.Sender.Email
                                                }
                                            });

            if (!string.IsNullOrEmpty(model.SearchTerm))
            {
                query = query.Where(x => x.Body.Contains(model.SearchTerm) || x.Subject.Contains(model.SearchTerm));
            }

            return await query.ToPagedListAsync(model, cancellationToken);
        }


        public async Task<PagedList<MailItemDto>> GetUserSentEmails(Guid tenantId, FilterUserEmailsDto model, CancellationToken cancellationToken)
        {
            var query = _context.Users.Where(x => x.Id == model.UserId && x.IsActive && !x.IsDeleted)
                                            .SelectMany(x => x.SentMails)
                                            .OrderByDescending(x => x.SentDate)
                                            .Select(x => new MailItemDto
                                            {
                                                Id = x.Id,
                                                Body = x.Body,
                                                Subject = x.Subject,
                                                SenderInfo = new MailSenderInfoDto
                                                {
                                                    Avatar = x.Sender.ProfilePicture.Base64,
                                                    Email = x.Sender.Email
                                                }
                                            });

            if (!string.IsNullOrEmpty(model.SearchTerm))
            {
                query = query.Where(x => x.Body.Contains(model.SearchTerm) || x.Subject.Contains(model.SearchTerm));
            }

            return await query.ToPagedListAsync(model, cancellationToken);
        }

        public async Task<bool> NewMailAsync(NewMailDto model, CancellationToken cancellationToken)
        {
            var mail = new Mail
            {
                Subject = model.Subject,
                Body = model.Body,
                SenderId = model.SenderId,
                SentDate = DateTime.UtcNow,
                Status = MailStatusEnum.Sent
            };

            var recipients = new List<UserMail>();

            foreach (var item in model.ToRecipients)
            {
                recipients.Add(new UserMail
                {
                    RecipientId = item,
                    RecipientType = MailRecipientTypeEnum.To,
                });
            }

            foreach (var item in model.BccRecipients)
            {
                recipients.Add(new UserMail
                {
                    RecipientId = item,
                    RecipientType = MailRecipientTypeEnum.BCC,
                });
            }

            foreach (var item in model.CcRecipients)
            {
                recipients.Add(new UserMail
                {
                    RecipientId = item,
                    RecipientType = MailRecipientTypeEnum.CC,
                });
            }

            mail.Recipients = recipients;

            await _context.AddAsync(mail, cancellationToken);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result > 0;
        }

        public async Task<ViewMailInfoDto> GetMailInfoAsync(Guid mailId, CancellationToken cancellationToken)
        {
            var result = await _context.Mails.Where(x => x.Id == mailId)
                                             .Select(y => new ViewMailInfoDto
                                             {
                                                 MailId = y.Id,
                                                 SenderInfo = new MailSenderInfoDto
                                                 {
                                                     Email = y.Sender.Email,
                                                     Avatar = y.Sender.ProfilePicture != null ? y.Sender.ProfilePicture.Base64 : "",
                                                 },
                                                 Subject = y.Subject,
                                                 Body = y.Body,
                                                 ToRecipients = y.Recipients.Where(t => t.RecipientType == MailRecipientTypeEnum.To).Select(r => new MailReceipientInfoDto { FullName = r.Recipient.FirstName + " " + r.Recipient.LastName, Email =  r.Recipient.Email, Avatar = r.Recipient.ProfilePicture.Base64 }).ToList(),
                                                 CcRecipients = y.Recipients.Where(t => t.RecipientType == MailRecipientTypeEnum.CC).Select(r => new MailReceipientInfoDto { FullName = r.Recipient.FirstName + " " + r.Recipient.LastName, Email = r.Recipient.Email, Avatar = r.Recipient.ProfilePicture.Base64 }).ToList(),
                                                 BccRecipients = y.Recipients.Where(t => t.RecipientType == MailRecipientTypeEnum.BCC).Select(r => new MailReceipientInfoDto { FullName = r.Recipient.FirstName + " " + r.Recipient.LastName, Email = r.Recipient.Email, Avatar = r.Recipient.ProfilePicture.Base64 }).ToList(),
                                             }).FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}

