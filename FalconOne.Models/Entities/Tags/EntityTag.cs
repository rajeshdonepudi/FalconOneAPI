using FalconOne.Models.Entities.Users;
using FalconOne.Models.EntityConfiguration.Tags;
using FalconOne.Models.EntityContracts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FalconOne.Models.Entities.Tags
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
}
