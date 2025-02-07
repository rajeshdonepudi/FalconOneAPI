using FalconOne.Models.Entities.Tenants;
using FalconOne.Models.EntityContracts;
using Microsoft.AspNetCore.Identity;

namespace FalconOne.Models.Entities.Users
{
    public class UserLogin : IdentityUserLogin<Guid>, IMultiTenantEntity
    {
        public Guid? TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
        public Guid? LastUpdatedByUserId { get; set; }
        public User LastUpdatedByUser { get; set; }
    }
}
