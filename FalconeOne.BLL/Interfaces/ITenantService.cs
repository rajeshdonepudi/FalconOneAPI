using FalconOne.Helpers.Helpers;
using FalconOne.Models.Dtos.Tenants;

namespace FalconeOne.BLL.Interfaces
{
    public interface ITenantService
    {
        Task<TenantDetailsDto> GetTenantDetailsAsync(string resoureAlias, CancellationToken cancellationToken);
        Task<bool> AddTenantAsync(AddTenantDto model, CancellationToken cancellationToken);
        Task<PagedList<TenantDetailsDto>> GetAllTenantsAsync(PageParams model, CancellationToken cancellationToken);
        Task<TenantLookupDto> GetTenantInfoAsync(CancellationToken cancellationToken);
        Task<TenantManagementDashboardInfoDto> GetTenantManagementDashboardInfo(CancellationToken cancellationToken);
        Task<IEnumerable<KeyValuePair<string, Guid>>> GetTenantsLookupForDirectoryAsync(string? searchTerm, CancellationToken cancellationToken);
    }
}
