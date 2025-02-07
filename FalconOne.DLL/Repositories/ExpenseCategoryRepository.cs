using FalconOne.DAL.Contracts;
using FalconOne.Extensions.EntityFramework;
using FalconOne.Helpers.Helpers;
using FalconOne.Models.Dtos.ExpenseManagement;
using FalconOne.Models.Entities.ExpenseManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace FalconOne.DAL.Repositories
{
    public class ExpenseCategoryRepository : GenericRepository<ExpenseCategory>, IExpenseCategoryRepository
    {
        public ExpenseCategoryRepository(FalconOneContext context, IMemoryCache cache) : base(context, cache)
        {
        }

        public async Task<IEnumerable<KeyValuePair<string, Guid>>> GetAllCategoriesLookupAsync(Guid userId, CancellationToken cancellationToken)
        {
            var query = _context.ExpenseCategories.Where(x => !x.IsDeleted &&
                                                              x.CreatedByUserId.HasValue &&
                                                              x.CreatedByUserId.Value == userId);

            var result = await query.Select(x => new KeyValuePair<string, Guid>(x.Name, x.Id)).ToListAsync(cancellationToken);

            return result;
        }

        public async Task<PagedList<BasicExpenseCategoryDto>> GetExpenseCategoriesByUserId(GetExpenseCategoriesDto model, Guid userId, CancellationToken cancellationToken)
        {
            var query = _context.ExpenseCategories.Where(x => !x.IsDeleted &&
                                                              x.CreatedByUserId.HasValue &&
                                                              x.CreatedByUserId.Value == userId);

            if (!string.IsNullOrEmpty(model.SearchParams))
            {
                query = query.Where(x => x.Name.ToLower() == model.SearchParams.ToLower());
            }

            return await query.Select(x => new BasicExpenseCategoryDto(x))
                              .ToPagedListAsync(model, cancellationToken);
        }
    }
}

