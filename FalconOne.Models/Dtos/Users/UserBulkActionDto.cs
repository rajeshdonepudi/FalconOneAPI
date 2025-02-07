using FalconOne.Enumerations.User;

namespace FalconOne.Models.DTOs.Users
{
    public class UserBulkActionDto
    {
        public UserBulkActionDto()
        {
            ResourceAliases = new List<string>();
        }
        public List<string> ResourceAliases { get; set; }
        public UserBulkActionsEnum Action { get; set; }
    }
}
