using FalconOne.Helpers.Helpers;

namespace FalconOne.Models.DTOs.Users
{
    public class FilterUserDirectoryDto : PageParams
    {
        public string? SearchTerm { get; set; }
        public List<Guid>? Tenants { get; set; }
    }
}
