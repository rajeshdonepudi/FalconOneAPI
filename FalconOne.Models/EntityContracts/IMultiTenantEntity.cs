using FalconOne.Models.Entities.Tenants;

namespace FalconOne.Models.EntityContracts
{
    public interface IMultiTenantEntity : ITrackableEntity
    {
        Guid? TenantId { get; set; }
        Tenant Tenant { get; set; }
    }
}
