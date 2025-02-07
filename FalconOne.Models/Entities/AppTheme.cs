using FalconOne.Enumerations.Themes;
using FalconOne.Models.Entities.Tenants;
using FalconOne.Models.Entities.Users;
using FalconOne.Models.EntityContracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FalconOne.Models.Entities
{
    [Table("Themes")]
    public class AppTheme : ITrackableEntity, ISoftDeletable, IMultiTenantEntity, ICloneable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public bool IsPrimary { get; set; }
        public required string PrimaryColor { get; set; }
        public required string SecondaryColor { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedOn { get; set; }
        public string? FontFamily { get; set; }
        public ThemePreferenceEnum ThemePreference { get; set; } = ThemePreferenceEnum.SystemDefault;

        public Guid? TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
        public User DeletedByUser { get; set; }
        public Guid? DeletedByUserId { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
        public Guid? LastUpdatedByUserId { get; set; }
        public User LastUpdatedByUser { get; set; }

        public object Clone()
        {
            var theme = new AppTheme
            {
                PrimaryColor = PrimaryColor,
                SecondaryColor = SecondaryColor,
                IsPrimary = false,
                IsDeleted = false,
            };

            return theme;
        }
    }
}
