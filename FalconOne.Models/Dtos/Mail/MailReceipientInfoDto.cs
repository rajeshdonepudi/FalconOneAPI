namespace FalconOne.Models.Dtos.Mail
{
    public record MailReceipientInfoDto
    {
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string FullName { get; set; }
    }
}
