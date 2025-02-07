﻿using FalconOne.Helpers.Helpers;
using FalconOne.Models.Dtos.Security.Permissions;
using FalconOne.Models.Dtos.Security.SecurityGroups;
using FalconOne.Models.Dtos.Tenants;
using FalconOne.Models.DTOs.Users;

namespace FalconeOne.BLL.Interfaces
{
    public interface ISecurityGroupService
    {
        Task<bool> DeleteUserFromSecurityGroup(DeleteUserFromSecurityGroupDto model, CancellationToken cancellationToken);
        Task<PagedList<UserInfoDto>> GetAllSecurityGroupUsersAsync(FilterSecurityGroupUsers model, CancellationToken cancellationToken);
        Task<bool> AddUsersToSecurityGroupAsync(AddUsersToSecurityGroupDto model, CancellationToken cancellationToken);
        Task<SecurityGroupInfoDto> GetSecurityGroupInfoAsync(Guid securityGroupId, CancellationToken cancellationToken);
        Task<bool> DeleteSecurityGroupAsync(Guid securityGroupId, CancellationToken cancellationToken);
        Task<bool> AddSecurityGroupAsync(CreateSecurityGroupDto model, CancellationToken cancellationToken);
        Task<bool> UpdateSecurityGroupAsync(UpdateSecurityGroupDto model, CancellationToken cancellationToken);
        Task<PagedList<SecurityGroupsListDto>> GetTenantSecurityGroupsAsync(FilterSecurityGroupsDto model, CancellationToken cancellationToken);
        Task<IEnumerable<KeyValuePair<string, Guid>>> GetTenantSecurityGroupsLookupAsync(string searchTerm, CancellationToken cancellationToken);
        Task<IEnumerable<GroupPermissionsDto>> GetSecurityGroupPermissionsAsync(Guid securityGroupId, CancellationToken cancellationToken);
    }
}
