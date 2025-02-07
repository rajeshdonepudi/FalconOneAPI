using FalconeOne.BLL.Interfaces;
using FalconOne.API.Attributes;
using FalconOne.Helpers.Helpers;
using FalconOne.Models.Dtos.Security.Roles;
using FalconOne.Models.DTOs.Users;
using FalconOne.Security;
using Microsoft.AspNetCore.Mvc;

namespace FalconOne.API.Controllers.Security
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseSecureController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService securityService)
        {
            _roleService = securityService;
        }

        [HttpGet("view-role-info")]
        [FalconOneAuthorize(PermissionPool.Role.VIEW_ROLE)]
        public async Task<IActionResult> GetRole([FromQuery] Guid roleId, CancellationToken cancellationToken)
        {
            var result = await _roleService.GetRoleAsync(roleId, cancellationToken);

            return Ok(result);
        }

        [HttpPost("add-role")]
        [FalconOneAuthorize(PermissionPool.Role.CREATE_ROLE)]
        public async Task<IActionResult> CreateRole(RoleDto model, CancellationToken cancellationToken)
        {
            var result = await _roleService.CreateRoleAsync(model, cancellationToken);

            if (result) return Ok(result);

            return BadRequest();
        }

        [HttpDelete("delete-role")]
        [FalconOneAuthorize(PermissionPool.Role.DELETE_ROLE)]
        public async Task<IActionResult> DeleteRole([FromQuery] string roleId, CancellationToken cancellationToken)
        {
            var result = await _roleService.DeleteRoleAsync(roleId, cancellationToken);

            if (result) return Ok();

            return BadRequest();
        }

        [HttpGet("view-roles")]
        [FalconOneAuthorize(PermissionPool.Role.VIEW_ROLES)]
        public async Task<IActionResult> GetRoles(CancellationToken cancellationToken)
        {
            var result = await _roleService.GetAllTenantRolesAsync(cancellationToken);

            return Ok(result);
        }

        [HttpPost("view-user-roles")]
        [FalconOneAuthorize(PermissionPool.Role.VIEW_USER_ROLES)]
        public async Task<IActionResult> GetAllRoles(PageParams model, CancellationToken cancellationToken)
        {
            var result = await _roleService.GetAllUserRolesAsync(model, cancellationToken);

            return Ok(result);
        }

        [HttpPost("view-users-in-role")]
        [FalconOneAuthorize(PermissionPool.Role.VIEW_USERS_IN_ROLES)]
        public async Task<IActionResult> GetUsersAssociatedWithRole(GetUsersInRoleDto model, CancellationToken cancellationToken)
        {
            var result = await _roleService.GetAllUsersInRoleAsync(model, cancellationToken);

            return Ok(result);
        }

        [HttpPost("add-user-to-role")]
        [FalconOneAuthorize(PermissionPool.Role.ADD_USER_TO_ROLE)]
        public async Task<IActionResult> AddUserToRole(AddUserToRoleDto model, CancellationToken cancellationToken)
        {
            var result = await _roleService.AddUserToRoleAsync(model, cancellationToken);

            if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpDelete("remove-user-from-role")]
        [FalconOneAuthorize(PermissionPool.Role.REMOVE_USER_FROM_ROLE)]
        public async Task<IActionResult> RemoveUserFromRole(RemoveUserFromRoleDto model, CancellationToken cancellationToken)
        {
            var result = await _roleService.RemoveUserFromRoleAsync(model, cancellationToken);

            if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
