using FalconOne.Models.Entities.Tenants;
using FalconOne.Models.EntityContracts;
using Microsoft.AspNetCore.Identity;

namespace FalconOne.Models.Entities.Users
{
    public class UserToken : IdentityUserToken<Guid>
    {
        public Guid? TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
