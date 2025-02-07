using FalconeOne.BLL.Interfaces;
using FalconOne.DAL.Contracts;
using FalconOne.Models.Dtos.Security.Permissions;
using FalconOne.Models.Entities.Users;
using IdenticonSharp.Identicons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace FalconeOne.BLL.Services
{
    public class PermissionService : BaseService, IPermissionService
    {
        public PermissionService(UserManager<User> userManager, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ITenantProvider tenantService, IIdenticonProvider identiconProvider) : base(userManager, unitOfWork, httpContextAccessor, configuration, tenantService, identiconProvider)
        {
        }

        public async Task<IEnumerable<SecurityGroupDto>> GetTenantPermissionsAsync(CancellationToken cancellationToken)
        {
            var permissions = await _unitOfWork.PermissionRepository.GetTenantPermissionsAsync(TenantId, cancellationToken);

            return permissions;
        }

        

        public async Task<IEnumerable<SecurityGroupDto>> GetPermissionsAsync(CancellationToken cancellationToken)
        {
            var permissions = await _unitOfWork.PermissionRepository.GetPermissionsAsync(cancellationToken);

            return permissions;
        }

        public async Task<bool> ManagePermissionsAsync(ManagePermissionsDto permission, CancellationToken cancellationToken)
        {
            return await _unitOfWork.PermissionRepository.ManagePermissionsAsync(TenantId, permission, cancellationToken);
        }

        public async Task<bool> ManagePermissionsForTenantAsync(ManagePermissionsForTenantDto model, CancellationToken cancellationToken)
        {
            return await _unitOfWork.PermissionRepository.ManagePermissionsForTenantAsync(model, cancellationToken);
        }
    }
}
