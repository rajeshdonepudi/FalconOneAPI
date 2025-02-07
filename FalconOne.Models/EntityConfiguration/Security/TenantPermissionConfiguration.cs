using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FalconOne.Models.Entities.Tenants;
using FalconOne.Models.Entities.Security;

namespace FalconOne.Models.EntityConfiguration.Security
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasIndex(p => new { p.Type, p.Value, p.PermissionGroupId })
               .IsUnique();
        }
    }
    public class TenantPermissionConfiguration : IEntityTypeConfiguration<TenantPermission>
    {
        public void Configure(EntityTypeBuilder<TenantPermission> builder)
        {
            builder.HasKey(t => new { t.PermissionId, t.TenantId });

            builder.HasOne(t => t.Tenant)
                   .WithMany(y => y.Permissions)
                   .HasForeignKey(f => f.TenantId)
                   .IsRequired(true);

            builder.HasOne(t => t.Permission)
                   .WithMany(x => x.AssociatedTenants)
                   .HasForeignKey(x => x.PermissionId)
                   .IsRequired(true);
        }
    }
}
