using FalconOne.Enumerations.Tags;
using FalconOne.Models.Entities.Users;
using FalconOne.Models.EntityConfiguration.Tags;
using FalconOne.Models.EntityContracts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FalconOne.Models.Entities.Tags
{
    [EntityTypeConfiguration(typeof(TagEntityTypeConfiguration))]
    public class Tag : ITrackableEntity, ISoftDeletable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string NormalizedName { get; set; } = string.Empty;
        public required string Name { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }
        public Guid? LastUpdatedByUserId { get; set; }
        public virtual User LastUpdatedByUser { get; set; }
        public TagSourceEnum CreatedFrom { get; set; }

        public virtual ICollection<EntityTag> EntityTags { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedOn { get; set; }
        public virtual User DeletedByUser { get; set; }
        public Guid? DeletedByUserId { get; set; }
    }
}
