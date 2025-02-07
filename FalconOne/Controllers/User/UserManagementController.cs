using FalconeOne.BLL.Interfaces;
using FalconOne.API.Attributes;
using FalconOne.Helpers.Helpers;
using FalconOne.Models.Dtos.Tenants;
using FalconOne.Models.DTOs.Account;
using FalconOne.Models.DTOs.Users;
using FalconOne.Security;
using Microsoft.AspNetCore.Mvc;

namespace FalconOne.API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : BaseSecureController
    {
        private readonly IUserService _userService;

        public UserManagementController(IUserService accountService)
        {
            _userService = accountService;
        }

        [HttpGet("user-management-dashboard-info")]
        [FalconOneAuthorize(PermissionPool.UserManagement.USER_METRIC_BASIC_INFO)]
        public async Task<IActionResult> TenantUserManagementDashboardInfo([FromQuery] string accountAlias, CancellationToken cancellationToken)
        {
            var result = await _userService.GetTenantUserManagementDashboardInfo(accountAlias, cancellationToken);

            return Ok(result);
        }

        [HttpPost("add-user")]
        [FalconOneAuthorize(PermissionPool.UserManagement.CREATE_USER)]
        public async Task<IActionResult> AddUser(UpsertUserDto model)
        {
            var result = await _userService.UpsertUser(model);

            if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }


        [HttpPost("upsert-tenant-user")]
        [FalconOneAuthorize(PermissionPool.UserManagement.UPDATE_USER)]
        public async Task<IActionResult> UpsertTenantUser(UpsertTenantUserDto model, CancellationToken cancellationToken)
        {
            var result = await _userService.UpsertTenantUser(model, cancellationToken);

            if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpDelete("delete-user/{resourceAlias}")]
        [FalconOneAuthorize(PermissionPool.UserManagement.DELETE_USER)]
        public async Task<IActionResult> DeleteUser(string resourceAlias)
        {
            var result = await _userService.DeleteUserAsync(resourceAlias, CancellationToken.None);

            if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("all-users")]
        public async Task<IActionResult> GetAllUsers(PageParams model)
        {
            var response = await _userService.GetAllActiveAsync(model);

            return Ok(response);
        }

        [HttpPost("all-tenant-users")]
        [FalconOneAuthorize(PermissionPool.UserManagement.VIEW_USERS)]
        public async Task<IActionResult> GetAllTenantUsers(FilterTenantUsers model, CancellationToken cancellationToken)
        {
            var response = await _userService.GetAllActiveTenantUsersAsync(model, cancellationToken);

            return Ok(response);
        }

        [HttpGet("get-user")]
        [FalconOneAuthorize(PermissionPool.User.VIEW_USER_INFO)]
        public async Task<IActionResult> GetByUserId([FromQuery] Guid userId, CancellationToken cancellationToken)
        {
            var response = await _userService.GetByIdAsync(userId, cancellationToken);

            return Ok(response);
        }

        [HttpPost("revoke-refresh-token")]
        [FalconOneAuthorize(PermissionPool.UserManagement.REVOKE_ACCESS)]
        public async Task<IActionResult> RevokeRefreshToken(RevokeRefreshTokenRequestDto model)
        {
            var response = await _userService.RevokeAccess(model.RefreshToken);

            if (response)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("bulk-action")]
        [FalconOneAuthorize(PermissionPool.UserManagement.BULK_USER_ACTIONS)]
        public async Task<IActionResult> TakeBulkAction(UserBulkActionDto model, CancellationToken cancellationToken)
        {
            var result = await _userService.UpdateBulkActionsAsync(model, cancellationToken);

            if (result)
            {   
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("user-created-year")]
        
        public async Task<IActionResult> UserCreatedByYear()
        {
            return Ok(await _userService.UserCreatedByYears());
        }

        [HttpPost("filter-user-directory")]
        
        public async Task<IActionResult> GetAllUsersForUserDirectory(FilterUserDirectoryDto model, CancellationToken cancellationToken)
        {
            var result = await _userService.GetAllUsersForUserDirectoryAsync(model, cancellationToken);

            return Ok(result);
        }

        [HttpGet("user-lookup-for-directory")]
        
        public async Task<IActionResult> GetUserLookupForDirectory(CancellationToken cancellationToken)
        {
            var result = await _userService.GetUsersLookupForDirectoryAsync(cancellationToken);

            return Ok(result);
        }
    }
}
