using FalconeOne.BLL.Interfaces;
using FalconOne.API.Attributes;
using FalconOne.Helpers.Helpers;
using FalconOne.Models.Dtos.Tenants;
using FalconOne.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FalconOne.API.Controllers.Tenant
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : BaseSecureController
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpPost("add-tenant")]
        public async Task<IActionResult> AddTenant(AddTenantDto model, CancellationToken cancellationToken)
        {
            var result = await _tenantService.AddTenantAsync(model,cancellationToken);

            if (result) return Created();

            return BadRequest();
        }

        [HttpPost("all-tenants")]
        
        public async Task<IActionResult> GetAllUsers(PageParams model, CancellationToken cancellationToken)
        {
            var response = await _tenantService.GetAllTenantsAsync(model, cancellationToken);

            return Ok(response);
        }

        [HttpGet("tenant-info")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTenantInfo(CancellationToken cancellationToken)
        {
            var result = await _tenantService.GetTenantInfoAsync(cancellationToken);

            return Ok(result);
        }

        [HttpGet("get-tenant-details")]
        public async Task<IActionResult> GetTenantDetails([FromQuery] string accountId, CancellationToken cancellationToken)
        {
            var result = await _tenantService.GetTenantDetailsAsync(accountId, cancellationToken);

            return Ok(result);
        }

        [HttpGet("tenant-dashboard-info")]
        
        public async Task<IActionResult> GetTenantManagementDashboardData(CancellationToken cancellationToken)
        {
            var result = await _tenantService.GetTenantManagementDashboardInfo(cancellationToken);

            return Ok(result);
        }

        [HttpGet("tenant-lookup-for-directory")]
        
        public async Task<IActionResult> GetTenantsLookupForDirectory([FromQuery] string? searchTerm, CancellationToken cancellationToken)
        {
            var result = await _tenantService.GetTenantsLookupForDirectoryAsync(searchTerm, cancellationToken);

            return Ok(result);
        }
    }
}
