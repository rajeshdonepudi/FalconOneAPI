using FalconOne.Helpers.Helpers;
using FalconOne.Models.Dtos.Security.Roles;
using FalconOne.Models.Entities.Security;
using FalconOne.Models.EntityContracts;

namespace FalconOne.DAL.Contracts
{
    public interface ISecurityRolesRepository : IGenericRepository<ApplicationRole>
    {
        Task<PagedList<SecurityRoleDto>> GetAllUserRolesAsync(Guid tenantId, PageParams model, CancellationToken cancellationToken);
        Task<SecurityRoleDto> GetTenantRoleInfoAsync(Guid roleId, Guid tenantId, bool isGodUser, CancellationToken cancellationToken);
        Task<IEnumerable<KeyValuePair<Guid, string>>> GetSecurityRolesForLookup(CancellationToken cancellationToken);
        Task<PagedList<UserInRoleDto>> GetUsersInRoleAsync(GetUsersInRoleDto model, CancellationToken cancellationToken);
    }
}
