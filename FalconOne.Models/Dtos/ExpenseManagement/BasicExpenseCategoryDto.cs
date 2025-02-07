using FalconOne.Models.Entities.ExpenseManagement;

namespace FalconOne.Models.Dtos.ExpenseManagement
{
    public class BasicExpenseCategoryDto
    {
        public BasicExpenseCategoryDto(ExpenseCategory category)
        {
            Id = category.Id;
            Name = category.Name;
            Description = category.Description;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
