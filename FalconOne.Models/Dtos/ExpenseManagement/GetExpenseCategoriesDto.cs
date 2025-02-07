using FalconOne.Helpers.Helpers;

namespace FalconOne.Models.Dtos.ExpenseManagement
{
    public class GetExpenseCategoriesDto : PageParams
    {
        public string? SearchParams { get; set; }
    }
}
