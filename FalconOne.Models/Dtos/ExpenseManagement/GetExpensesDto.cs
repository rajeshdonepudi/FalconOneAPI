using FalconOne.Helpers.Helpers;

namespace FalconOne.Models.Dtos.ExpenseManagement
{
    public class GetExpensesDto : PageParams
    {
        public string? SearchParams { get; set; }
    }
}
