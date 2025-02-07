using FalconOne.Models.Dtos.Tenants;

namespace FalconOne.Models.Dtos.Security.Roles
{
    public record UserInRoleDto
    {
        public UserInRoleDto()
        {
            TenantInfo = new List<TenantLookupDto>();
        }

        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string ResourceAlias { get; set; }
        public List<TenantLookupDto> TenantInfo { get; set; }
    }
}
