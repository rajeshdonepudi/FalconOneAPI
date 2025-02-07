using FalconOne.Helpers.Helpers;
using FalconOne.Models.Dtos.Security.Permissions;
using FalconOne.Models.Dtos.Security.SecurityGroups;
using FalconOne.Models.DTOs.Users;
using FalconOne.Models.Entities.Security;
using FalconOne.Models.EntityContracts;

namespace FalconOne.DAL.Contracts
{
    public interface ISecurityGroupRepository : IGenericRepository<SecurityGroup>
    {
        Task<PagedList<UserInfoDto>> GetAllUsersBySecurityGroupPaginatedAsync(Guid securityGroupId, PageParams pageParams, CancellationToken cancellationToken);
        Task<bool> DeleteSecurityGroup(Guid securityGroupId, CancellationToken cancellationToken);
        Task<bool> AddSecurityGroup(CreateSecurityGroupDto model, Guid tenantId, CancellationToken cancellationToken);
        Task<PagedList<SecurityGroupsListDto>> GetTenantSecurityGroupsAsync(FilterSecurityGroupsDto model, Guid tenantId, CancellationToken cancellationToken);
        Task<IEnumerable<KeyValuePair<string, Guid>>> GetTenantSecurityGroupsLookupAsync(Guid tenantId, string searchTerm, CancellationToken cancellationToken);
        Task<SecurityGroupInfoDto> GetSecurityGroupInfo(Guid tenantId, Guid securityGroupId, CancellationToken cancellationToken);
    }
}
