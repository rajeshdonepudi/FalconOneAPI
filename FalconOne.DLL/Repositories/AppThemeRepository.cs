using FalconOne.DAL.Contracts;
using FalconOne.Models.Dtos.Theme;
using FalconOne.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace FalconOne.DAL.Repositories
{
    public class AppThemeRepository : GenericRepository<AppTheme>, IAppThemeRepository
    {
        public AppThemeRepository(FalconOneContext context, IMemoryCache cache) : base(context, cache)
        {
        }

        public async Task<IEnumerable<SiteThemeDto>> GetAllSiteThemesAsync(Guid tenantId, CancellationToken cancellationToken)
        {
            var themes = await _context.AppThemes.Where(x => !x.IsDeleted && x.TenantId == tenantId)
                                                 .OrderByDescending(x => x.IsPrimary).ThenByDescending(x => x.CreatedOn)
                                                 .Select(x => new SiteThemeDto
                                                 {
                                                     Id = x.Id,
                                                     PrimaryColor = x.PrimaryColor,
                                                     SecondaryColor = x.SecondaryColor,
                                                     ThemePreference = x.ThemePreference,
                                                     IsPrimary = x.IsPrimary,
                                                     FontFamily = x.FontFamily
                                                 })                                                
                                                 .ToListAsync(cancellationToken);
            return themes;
        }

        public async Task<SiteThemeDto> GetTenantPrimarySiteThemeAsync(Guid tenantId, CancellationToken cancellationToken)
        {
            var themes = await _context.AppThemes.Where(x => !x.IsDeleted && x.TenantId == tenantId && x.IsPrimary)
                                                 .Select(x => new SiteThemeDto
                                                 {
                                                     Id = x.Id,
                                                     PrimaryColor = x.PrimaryColor,
                                                     SecondaryColor = x.SecondaryColor,
                                                     ThemePreference = x.ThemePreference,
                                                     IsPrimary = x.IsPrimary,
                                                     FontFamily = x.FontFamily 
                                                 })
                                                 .FirstOrDefaultAsync(cancellationToken);
            return themes;
        }

        public async Task<List<AppTheme>> GetAllTenantSiteThemesAsync(Guid tenantId, CancellationToken cancellationToken)
        {
            var themes = await _context.AppThemes.Where(x => !x.IsDeleted && x.TenantId == tenantId)
                                                 .ToListAsync(cancellationToken);

            return themes;
        }
    }
}

