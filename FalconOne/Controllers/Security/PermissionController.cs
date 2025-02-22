using FalconeOne.BLL.Interfaces;
using FalconOne.API.Attributes;
using FalconOne.Models.Dtos.Security.Permissions;
using FalconOne.Security;
using Microsoft.AspNetCore.Mvc;

namespace FalconOne.API.Controllers.Security
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : BaseSecureController
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet("all")]
        [FalconOneAuthorize("")]
        public async Task<IActionResult> GetPermissions(CancellationToken cancellationToken)
        {
            var result = await _permissionService.GetPermissionsAsync(cancellationToken);

            return Ok(result);
        }

        [HttpGet("tenant-permissions")]
        [FalconOneAuthorize(PermissionPool.Permission_Management.VIEW_PERMISSIONS)]
        public async Task<IActionResult> GetTenantPermissions(CancellationToken cancellationToken)
        {
            var result = await _permissionService.GetTenantPermissionsAsync(cancellationToken);

            return Ok(result);
        }

        

        [HttpPost("manage-permissions")]
        [FalconOneAuthorize(PermissionPool.Permission_Management.MANAGE_PERMISSIONS)]
        public async Task<IActionResult> ManagePermissions(ManagePermissionsDto model, CancellationToken cancellationToken)
        {
            var result = await _permissionService.ManagePermissionsAsync(model, cancellationToken);

            if(result) return Ok();

            return BadRequest();
        }

        [HttpPost("manage-permissions-for-tenant")]
        public async Task<IActionResult> ManagePermissionsForTenant(ManagePermissionsForTenantDto model, CancellationToken cancellationToken)
        {
            var result = await _permissionService.ManagePermissionsForTenantAsync(model, cancellationToken);

            if(result) return Ok();

            return BadRequest();
        }
    }
}
