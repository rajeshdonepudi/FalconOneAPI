﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FalconOne.Models.Entities.Security;

namespace FalconOne.Models.EntityConfiguration.Security
{
    public class TenantUserSecurityGroupConfiguration : IEntityTypeConfiguration<TenantUserSecurityGroup>
    {
        public void Configure(EntityTypeBuilder<TenantUserSecurityGroup> builder)
        {
            builder.HasOne(y => y.SecurityGroup)
                .WithMany(z => z.TenantUserSecurityGroups)
                .HasForeignKey(z => z.SecurityGroupId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.TenantUser)
                   .WithMany(y => y.TenantUserSecurityGroups)
                   .HasForeignKey(x => x.TenantUserId)
                   .HasPrincipalKey(z => z.Id)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
