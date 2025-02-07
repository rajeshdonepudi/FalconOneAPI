namespace FalconOne.Models.Dtos.Mail
{
    public record EmailMessageItemDto
    {
        public string Date { get; set; }
        public uint MessageId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Flags { get; set; }
        public string From { get; set; }
        public string Snippet { get; set; }
        public bool IsRead { get; set; }
        public string HeaderMessageId { get; set; }
        public List<string> Attachments { get; set; }
    }

    public record ReadMessageRequestDto
    {
        public uint MessageId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MailBox { get; set; }
    }
}
