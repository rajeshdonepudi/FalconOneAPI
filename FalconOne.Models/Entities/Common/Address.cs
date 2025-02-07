﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FalconOne.Models.Entities.Users;
using FalconOne.Enumerations.Common;
using FalconOne.Models.EntityContracts;
using Microsoft.EntityFrameworkCore;
using FalconOne.Models.EntityConfiguration.User;

namespace FalconOne.Models.Entities.Common
{
    [Table("Addresses")]
    [EntityTypeConfiguration(typeof(AddressEntityTypeConfiguration))]
    public class Address : ITrackableEntity, ISoftDeletable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public required string HouseNo { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }

        public int State { get; set; }
        public AddressTypeEnum AddressType { get; set; } = AddressTypeEnum.NotSpecified;
        public LocationTypeEnum LocationType { get; set; } = LocationTypeEnum.NotSpecified;
        public Guid? UserId { get; set; }
        public Guid? CountryId { get; set; }

        public DateTime? ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        
        public Guid? CreatedByUserId { get; set; }
        public Guid? LastUpdatedByUserId { get; set; }

        public virtual User User { get; set; }
        public virtual Country Country { get; set; }
        public virtual User CreatedByUser { get; set; }
        public virtual User LastUpdatedByUser { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedOn { get; set; }
        public virtual User DeletedByUser { get; set; }
        public Guid? DeletedByUserId { get; set; }
    }
}
