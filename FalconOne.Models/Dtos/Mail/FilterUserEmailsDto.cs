using FalconOne.Helpers.Helpers;

namespace FalconOne.Models.Dtos.Mail
{
    public class FilterUserEmailsDto : PageParams
    {
        public Guid UserId { get; set; }
        public string? SearchTerm { get; set; }
    }
}
