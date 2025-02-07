using FalconOne.Helpers.Helpers;
using FalconOne.Models.Dtos.ExpenseManagement;
using FalconOne.Models.Entities.ExpenseManagement;
using FalconOne.Models.EntityContracts;

namespace FalconOne.DAL.Contracts
{
    public interface IExpenseCategoryRepository : IGenericRepository<ExpenseCategory>
    {
        Task<IEnumerable<KeyValuePair<string, Guid>>> GetAllCategoriesLookupAsync(Guid userId, CancellationToken cancellationToken);
        Task<PagedList<BasicExpenseCategoryDto>> GetExpenseCategoriesByUserId(GetExpenseCategoriesDto model, Guid userId, CancellationToken cancellationToken);
    }
}
