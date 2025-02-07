using System.ComponentModel.DataAnnotations;

namespace FalconOne.Models.DTOs.Users
{
    public class RoleDto
    {
        [Required]
        public required string Name { get; set; }
    }
}
