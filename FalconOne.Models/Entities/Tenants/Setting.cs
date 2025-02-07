using FalconOne.Enumerations.Settings;
using FalconOne.Models.Entities.Users;
using FalconOne.Models.EntityConfiguration.Security;
using FalconOne.Models.EntityContracts;
using Microsoft.EntityFrameworkCore;

namespace FalconOne.Models.Entities.Tenants
{
    [EntityTypeConfiguration(typeof(SettingEntityTypeConfiguration))]
    public class Setting : ITrackableEntity, ISoftDeletable
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Value { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public SettingTypeEnum SettingType { get; set; }

        public Guid TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }
        public Guid? LastUpdatedByUserId { get; set; }
        public virtual User LastUpdatedByUser { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedOn { get; set; }
        public virtual User DeletedByUser { get; set; }
        public Guid? DeletedByUserId { get; set; }
    }
}
