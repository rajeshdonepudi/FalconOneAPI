using FalconOne.Models.Entities.Tenants;
using FalconOne.Models.EntityConfiguration.Security;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FalconOne.Models.Entities.Security
{
    [EntityTypeConfiguration(typeof(TenantUserSecurityGroupConfiguration))]
    [Table("UserSecurityGroups")]
    public class TenantUserSecurityGroup
    {
        public Guid Id { get; set; }
        public Guid SecurityGroupId { get; set; }
        public virtual SecurityGroup SecurityGroup { get; set; }

        public Guid TenantUserId { get; set; }
        public virtual TenantUser TenantUser { get; set; }
    }
}
