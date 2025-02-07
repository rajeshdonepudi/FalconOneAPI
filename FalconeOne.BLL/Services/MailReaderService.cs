using FalconeOne.BLL.Interfaces;
using FalconOne.Integrations;
using FalconOne.Models.Dtos.Mail;

namespace FalconeOne.BLL.Services
{
    public class MailReaderService : IMailReaderService
    {
        private readonly IGmailMessageReader _gmailMessageReader;
        private readonly IAppConfigService _appConfigService;
        private string GMAIL_APP_PASSWORD = string.Empty;

        public MailReaderService(IGmailMessageReader gmailMessageReader, IAppConfigService appConfigService)
        {
            _gmailMessageReader = gmailMessageReader;
            _appConfigService = appConfigService;

            GMAIL_APP_PASSWORD = _appConfigService.GetValueAsync("GmailAppPassword").Result;
        }

        public async Task<MemoryStream> DownloadAttachment(string attachmentId)
        {
            _gmailMessageReader.Login("bheeshma.hasthinapuram@gmail.com", GMAIL_APP_PASSWORD);

            _gmailMessageReader.ChooseBox("INBOX");

            var result = await _gmailMessageReader.DownloadAttachmentByIdAsync(attachmentId);

            _gmailMessageReader.Disconnect();

            return result;
        }

        public async Task<EmailMessageItemDto> GetSingleEmailByUid(ReadMessageRequestDto model, uint uid)
        {
            _gmailMessageReader.Login(model.Email, GMAIL_APP_PASSWORD);
            _gmailMessageReader.ChooseBox(model.MailBox);

            var email = await _gmailMessageReader.FetchSingleEmailAsync(uid);

            _gmailMessageReader.Disconnect();

            if (email == null)
            {
                throw new Exception($"Email with UID {uid} not found.");
            }

            List<string> attachments = new List<string>();
            int numAttachments = email.NumAttachments;

            for (int i = 0; i < numAttachments; i++)
            {
                string attachmentName = email.GetAttachmentFilename(i);
                attachments.Add(attachmentName);
            }

            return new EmailMessageItemDto
            {
                Date = email.EmailDateStr,
                HeaderMessageId = email.GetHeaderField("Message-ID"),
                Subject = email.Subject,
                From = email.FromName,
                Attachments = attachments,
                Body = email.Body,
                MessageId = email.GetImapUid()
            };
        }

        public async Task<IEnumerable<EmailMessageItemDto>> GetEmailMessages(ReadMessageRequestDto model)
        {
            _gmailMessageReader.Login(model.Email, GMAIL_APP_PASSWORD);

            _gmailMessageReader.ChooseBox(model.MailBox);

            var emailBundle = _gmailMessageReader.GetEmailBundle();


            var emails = new List<Chilkat.Email>();

            for (int i = 0; i < emailBundle.MessageCount; i++)
            {
                emails.Add(emailBundle.GetEmail(i));
            }

            var transformedMails = emails.Select(x => new EmailMessageItemDto
            {
                Subject = x.Subject,
                Body = x.Body,
                From = x.FromName,
                Snippet = x.GetPlainTextBody().Length > 100 ? x.GetPlainTextBody().Substring(0, 100) + "..." : x.GetPlainTextBody(),
                MessageId = x.GetImapUid(),
                Flags = _gmailMessageReader.GetEmailFlags(x.GetImapUid()).Result,
                IsRead = _gmailMessageReader.GetEmailFlags(x.GetImapUid()).Result?.Contains(@"\Seen") ?? false
            }).ToList();

            _gmailMessageReader.Disconnect();

            return transformedMails;

        }
    }
}
