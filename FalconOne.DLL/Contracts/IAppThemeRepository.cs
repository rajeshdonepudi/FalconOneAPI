using FalconOne.Models.Dtos.Theme;
using FalconOne.Models.Entities;
using FalconOne.Models.EntityContracts;

namespace FalconOne.DAL.Contracts
{
    public interface IAppThemeRepository : IGenericRepository<AppTheme>
    {
        Task<SiteThemeDto> GetTenantPrimarySiteThemeAsync(Guid tenantId, CancellationToken cancellationToken);
        Task<IEnumerable<SiteThemeDto>> GetAllSiteThemesAsync(Guid tenantId, CancellationToken cancellationToken);
        Task<List<AppTheme>> GetAllTenantSiteThemesAsync(Guid tenantId, CancellationToken cancellationToken);
    }
}
