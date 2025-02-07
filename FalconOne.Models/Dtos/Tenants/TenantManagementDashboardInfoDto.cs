namespace FalconOne.Models.Dtos.Tenants
{
    public record TenantManagementDashboardInfoDto
    {
        public long TotalTenantsInSystem { get; set; }
        public long TotalUsersInSystem { get; set; }
    }
}
