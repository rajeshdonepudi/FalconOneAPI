namespace FalconOne.Models.Dtos.Mail
{
    public record ViewMailInfoDto
    {
        public ViewMailInfoDto()
        {
            ToRecipients = new List<MailReceipientInfoDto>();
            BccRecipients = new List<MailReceipientInfoDto>();
            CcRecipients = new List<MailReceipientInfoDto>();
        }

        public Guid MailId { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
        public MailSenderInfoDto SenderInfo { get; set; }

        public List<MailReceipientInfoDto> ToRecipients { get; set; }
        public List<MailReceipientInfoDto> BccRecipients { get; set; }
        public List<MailReceipientInfoDto> CcRecipients { get; set; }
    }
}
