
using FalconOne.Models.DTOs.Users;

namespace FalconeOne.BLL.Interfaces
{
    public interface IUserLookupService
    {
        Task<IEnumerable<UserLookupDto>> GetAllUsersLookup(string searchTerm, CancellationToken cancellationToken);
        Task<IEnumerable<UserLookupDto>> GetAllUsersLookupByTenantId(Guid tenantId, string searchTerm, CancellationToken cancellationToken);
    }
}
