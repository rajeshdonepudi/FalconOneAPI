using FalconOne.Models.Entities.Mails;
using FalconOne.Models.Entities.Users;
using FalconOne.Models.EntityConfiguration.Mails;
using FalconOne.Models.EntityContracts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FalconOne.Models.Entities
{
    [EntityTypeConfiguration(typeof(AttachmentConfiguration))]
    [Table("Attachments")]
    public class Attachment : ITrackableEntity, ICloneable, ISoftDeletable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public required string FileName { get; set; }
        public required byte[] Content { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }

        public Guid MailId { get; set; }
        public virtual Mail Mail { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }
        public Guid? LastUpdatedByUserId { get; set; }
        public virtual User LastUpdatedByUser { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedOn { get; set; }
        public virtual User DeletedByUser { get; set; }
        public Guid? DeletedByUserId { get; set; }

        public object Clone()
        {
            var cloned = new Attachment
            {
                FileName = FileName,
                Content = Content,
            };

            return cloned;
        }
    }
}
