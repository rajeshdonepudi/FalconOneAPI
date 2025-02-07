using FalconOne.Helpers.Helpers;

namespace FalconOne.Models.Dtos.Security.SecurityGroups
{
    public class FilterSecurityGroupsDto : PageParams
    {
        public string? SearchTerm { get; set; }
    }
}
