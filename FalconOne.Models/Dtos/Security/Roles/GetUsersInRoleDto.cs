using FalconOne.Helpers.Helpers;

namespace FalconOne.Models.Dtos.Security.Roles
{
    public class GetUsersInRoleDto : PageParams
    {
        public Guid RoleId { get; set; }
    }
}
