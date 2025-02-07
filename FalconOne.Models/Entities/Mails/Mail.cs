using FalconOne.Enumerations.Mail;
using FalconOne.Models.Entities.Users;
using FalconOne.Models.EntityConfiguration.Mails;
using FalconOne.Models.EntityConfiguration.Tags;
using FalconOne.Models.EntityContracts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FalconOne.Models.Entities.Mails
{
    [EntityTypeConfiguration(typeof(EntityTagEntityTypeConfiguration))]
    public class EntityTag : ISoftDeletable, ITrackableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid? UserId { get; set; }
        public virtual User User { get; set; }


        public Guid TagId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedOn { get; set; }
        public Guid? DeletedByUserId { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? LastUpdatedByUserId { get; set; }

        public virtual Tag Tag { get; set; }
        public virtual User CreatedByUser { get; set; }
        public virtual User DeletedByUser { get; set; }
        public virtual User LastUpdatedByUser { get; set; }
    }

    [EntityTypeConfiguration(typeof(TagEntityTypeConfiguration))]
    public class Tag : ITrackableEntity, ISoftDeletable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public required string Name { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }
        public Guid? LastUpdatedByUserId { get; set; }
        public virtual User LastUpdatedByUser { get; set; }

        public virtual ICollection<EntityTag> EntityTags { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedOn { get; set; }
        public User DeletedByUser { get; set; }
        public Guid? DeletedByUserId { get; set; }
    }
    
    [EntityTypeConfiguration(typeof(MailConfiguration))]
    [Table("Mails")]
    public class Mail : ISoftDeletable, ITrackableEntity
    {
        public Mail()
        {
            Recipients = new HashSet<UserMail>();
            Attachments = new HashSet<Attachment>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime SentDate { get; set; }
        public MailStatusEnum Status { get; set; }
        public Guid SenderId { get; set; }

        public virtual User Sender { get; set; }
        public virtual ICollection<UserMail> Recipients { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public User DeletedByUser { get; set; }
        public Guid? DeletedByUserId { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? LastUpdatedByUserId { get; set; }
        
        public virtual User CreatedByUser { get; set; }
        public virtual User LastUpdatedByUser { get; set; }
    }
}
