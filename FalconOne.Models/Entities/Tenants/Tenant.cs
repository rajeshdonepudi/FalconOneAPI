using FalconOne.Models.Entities.Common;
using FalconOne.Models.Entities.Security;
using FalconOne.Models.Entities.Users;
using FalconOne.Models.EntityConfiguration.Tenant;
using FalconOne.Models.EntityContracts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FalconOne.Models.Entities.Tenants
{
    [EntityTypeConfiguration(typeof(TenantConfiguration))]
    [Table("Tenants")]
    public class Tenant : ITrackableEntity, ISoftDeletable
    {
        public Tenant()
        {
            Users = new HashSet<TenantUser>();
            Themes = new HashSet<AppTheme>();
            Settings = new HashSet<Setting>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string AccountAlias { get; private set; }
        public string Host { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedOn { get; set; }
        public Guid? ProfilePictureId { get; set; }

        public virtual Image ProfilePicture { get; set; }

        #region Navigation Properties
        public virtual ICollection<TenantUser> Users { get; set; }
        public virtual ICollection<AppTheme> Themes { get; set; }
        public virtual ICollection<SecurityGroup> SecurityGroups { get; set; }
        public virtual ICollection<TenantPermission> Permissions { get; set; }
        public virtual ICollection<Setting> Settings { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }
        public Guid? LastUpdatedByUserId { get; set; }
        public virtual User LastUpdatedByUser { get; set; }
        public virtual User DeletedByUser { get; set; }
        public Guid? DeletedByUserId { get; set; }

        #endregion
    }
}
