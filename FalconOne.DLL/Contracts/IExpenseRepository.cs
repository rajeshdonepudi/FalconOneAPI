using FalconOne.Helpers.Helpers;
using FalconOne.Models.Dtos.ExpenseManagement;
using FalconOne.Models.Entities.ExpenseManagement;
using FalconOne.Models.EntityContracts;

namespace FalconOne.DAL.Contracts
{
    public interface IExpenseRepository : IGenericRepository<Expense>
    {
        Task<ExpenseDashboardInfoDto> GetExpenseDashboardInfo(Guid userId, CancellationToken cancellationToken);
        Task<PagedList<BasicExpenseDetailDto>> GetExpensesByUserIdAsync(GetExpensesDto model, Guid userId, CancellationToken cancellationToken);
    }
}
