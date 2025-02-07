namespace FalconOne.Models.Dtos.Mail
{
    public record NewMailDto
    {
        public NewMailDto()
        {
            ToRecipients = new List<Guid>();
            CcRecipients = new List<Guid>();
            BccRecipients = new List<Guid>();
        }

        public Guid SenderId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<Guid> ToRecipients { get; set; }
        public List<Guid> CcRecipients { get; set; }
        public List<Guid> BccRecipients { get; set; }
    }
}
