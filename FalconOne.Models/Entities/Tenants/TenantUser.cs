using FalconOne.Models.EntityConfiguration.Tenant;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using FalconOne.Models.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;
using FalconOne.Models.Entities.Security;
using FalconOne.Models.EntityContracts;

namespace FalconOne.Models.Entities.Tenants
{
    [EntityTypeConfiguration(typeof(TenantUserConfiguration))]
    [Table("TenantUsers")]
    public class TenantUser : ISoftDeletable
    {
        public TenantUser()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid TenantId { get; set; }

        public Guid UserId { get; set; }

        public DateTime DeletedOn { get; set; }

        public bool IsDeleted { get; set; }

        #region Navigation Properties
        public virtual Tenant Tenant { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<TenantUserSecurityGroup> TenantUserSecurityGroups { get; set; }
        public virtual User DeletedByUser { get; set; }
        public Guid? DeletedByUserId { get; set; }
        #endregion
    }
}
