using FalconOne.Enumerations.Mail;
using FalconOne.Models.Entities.Mails;
using FalconOne.Models.EntityConfiguration.Mails;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FalconOne.Models.Entities.Users
{
    [EntityTypeConfiguration(typeof(UserMailConfiguration))]
    [Table("UserMails")]
    public class UserMail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid RecipientId { get; set; }
        public virtual User Recipient { get; set; }
        public required MailRecipientTypeEnum RecipientType { get; set; }
        public Guid MailId { get; set; }
        public virtual Mail Mail { get; set; }
    }
}
