using FalconeOne.BLL.Interfaces;
using FalconOne.API.Attributes;
using FalconOne.Models.Dtos.Security.Permissions;
using FalconOne.Models.DTOs.Users;
using FalconOne.Security;
using Microsoft.AspNetCore.Mvc;

namespace FalconOne.API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseSecureController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get-user-profile-info")]
        [FalconOneAuthorize(PermissionPool.User.VIEW_USER_PROFILE_INFO)]
        public async Task<IActionResult> GetProfileInfo([FromQuery] Guid userId, CancellationToken cancellationToken)
        {
            var result = await _userService.GetUserProfileInfoAsync(userId, cancellationToken);

            return Ok(result);
        }

        [HttpGet("get-user-info")]
        [FalconOneAuthorize(PermissionPool.User.VIEW_USER_INFO)]
        public async Task<IActionResult> GetUserInfo([FromQuery] string resourceId, CancellationToken cancellationToken)
        {
            var result = await _userService.GetUserByResourceId(resourceId, cancellationToken);

            return Ok(result);
        }

        [HttpGet("user-dashboard-info")]
        [FalconOneAuthorize(PermissionPool.User.USER_DASHBOARD_METRIC_INFO)]
        public async Task<IActionResult> TenantUserManagementDashboardInfo(CancellationToken cancellationToken)
        {
            var result = await _userService.GetUserDashboardInfoAsync(cancellationToken);

            return Ok(result);
        }

        [HttpGet("get-user-roles")]
        [FalconOneAuthorize(PermissionPool.User.VIEW_USER_ROLES)]
        public async Task<IActionResult> GetUserRoles([FromQuery] string resourceId, CancellationToken cancellationToken)
        {
            var roles = await _userService.GetUserRolesByResourceId(resourceId, cancellationToken);

            return Ok(roles);
        }

        [HttpPost("remove-user-permission")]
        public async Task<IActionResult> RemoveUserPermission(RemoveUserPermission model, CancellationToken cancellationToken)
        {
            var result = await _userService.RemoveUserPermission(model, cancellationToken);

            if (result) return Ok();

            return BadRequest();
        }

        [HttpGet("get-user-permissions")]
        [FalconOneAuthorize(PermissionPool.User.VIEW_USER_PERMISSIONS)]
        public async Task<IActionResult> GetUserPermissions(string resourceId, CancellationToken cancellationToken)
        {
            var permissions = await _userService.GetUserPermissions(resourceId, cancellationToken);

            return Ok(permissions);
        }

        [HttpPost("upload-profile-picture")]
        [FalconOneAuthorize(PermissionPool.User.CHANGE_PROFILE_PICTURE)]
        public async Task<IActionResult> UpdateProfilePicture(UpdateProfilePictureDto model, CancellationToken cancellationToken)
        {
            var result = await _userService.UpdateProfilePictureAsync(model, cancellationToken);

            if(result) return Accepted();

            return BadRequest(model);
        }
    }
}
