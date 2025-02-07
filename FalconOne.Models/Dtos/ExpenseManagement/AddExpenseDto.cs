using FalconOne.Enumerations.ExpenseManagement;

namespace FalconOne.Models.Dtos.ExpenseManagement
{
    public class AddExpenseCategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsValidCategory => !string.IsNullOrWhiteSpace(Name);
    }

    public class UpdateExpenseCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateExpenseDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Amount { get; set; }
        public required string Description { get; set; }
        public ExpenseTypeEnum Type { get; set; }
    }

    public class AddExpenseDto
    {
        public decimal Amount { get; set; }
        public required string Description { get; set; }
        public ExpenseTypeEnum Type { get; set; } = ExpenseTypeEnum.Miscellaneous;
        public Guid CategoryId { get; set; }
        public bool IsValidExpense => Amount != default && !string.IsNullOrWhiteSpace(Description);
    }
}
