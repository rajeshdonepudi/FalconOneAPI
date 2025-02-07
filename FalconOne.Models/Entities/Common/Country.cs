using FalconOne.Models.Entities.Users;
using FalconOne.Models.EntityConfiguration.User;
using FalconOne.Models.EntityContracts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FalconOne.Models.Entities.Common
{
    [Table("Countries")]
    [EntityTypeConfiguration(typeof(CountryEntityTypeConfiguration))]
    public class Country : ITrackableEntity, ISoftDeletable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string TwoCharCountryCode { get; set; }
        public required string ThreeCharCountryCode { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }

        public Guid? CreatedByUserId { get; set; }
        public Guid? LastUpdatedByUserId { get; set; }

        public virtual User CreatedByUser { get; set; }
        public virtual User LastUpdatedByUser { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedOn { get; set; }
        public virtual User DeletedByUser { get; set; }
        public Guid? DeletedByUserId { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
