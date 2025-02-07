﻿using FalconOne.Models.Entities.Tenants;
using FalconOne.Models.Entities.Users;
using FalconOne.Models.EntityConfiguration.Security;
using FalconOne.Models.EntityContracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FalconOne.Models.Entities.Security
{
    [EntityTypeConfiguration(typeof(ApplicationRoleConfiguration))]
    [Table("Roles")]
    public class ApplicationRole : IdentityRole<Guid>, ITrackableEntity, ISoftDeletable
    {
        public ApplicationRole()
        {

        }

        public Guid? TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
        public Guid? LastUpdatedByUserId { get; set; }
        public User LastUpdatedByUser { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedOn { get; set; }
        public User DeletedByUser { get; set; }
        public Guid? DeletedByUserId { get; set; }
    }
}
