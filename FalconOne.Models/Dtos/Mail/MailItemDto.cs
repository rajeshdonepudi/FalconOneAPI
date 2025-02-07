namespace FalconOne.Models.Dtos.Mail
{
    public record MailItemDto
    {
        public Guid Id { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
        public MailSenderInfoDto SenderInfo { get; set; }
    }
}
