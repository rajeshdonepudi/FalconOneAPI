using FalconeOne.BLL.Helpers;
using FalconeOne.BLL.Interfaces;
using FalconOne.DAL.Contracts;
using FalconOne.Helpers.Helpers;
using FalconOne.Models.Dtos.ExpenseManagement;
using FalconOne.Models.Entities.ExpenseManagement;
using FalconOne.Models.Entities.Users;
using IdenticonSharp.Identicons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace FalconeOne.BLL.Services
{
    public class ExpenseCategoryService : BaseService, IExpenseCategoryService
    {
        public ExpenseCategoryService(UserManager<User> userManager, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ITenantProvider tenantProvider, IIdenticonProvider identiconProvider) 
            : base(userManager, unitOfWork, httpContextAccessor, configuration, tenantProvider, identiconProvider)
        {
        }

        public async Task<IEnumerable<KeyValuePair<string, Guid>>> GetAllCategoriesForLookupAsync(CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.ExpenseCategoryRepository.GetAllCategoriesLookupAsync(USER_ID, cancellationToken);

            return result;
        }

        public async Task<PagedList<BasicExpenseCategoryDto>> GetAllExpenseCategoriesAsync(GetExpenseCategoriesDto model, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.ExpenseCategoryRepository.GetExpenseCategoriesByUserId(model, USER_ID, cancellationToken);

            return result;
        }

        public async Task<BasicExpenseCategoryDto> AddExpenseCategoryAsync(AddExpenseCategoryDto model, CancellationToken cancellationToken)
        {
            if (!model.IsValidCategory)
            {
                throw new BusinessException(MessageHelper.ExpenseManagementErrors.INVALID_EXPENSE_CATEGORY);
            }

            var category = await _unitOfWork.ExpenseCategoryRepository.AddAsync(new ExpenseCategory
            {
                Name = model.Name,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                CreatedByUserId = USER_ID
            }, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new BasicExpenseCategoryDto(category);
        }

        public async Task DeleteExpenseCategoryAsync(Guid categoryId, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.ExpenseCategoryRepository.GetByIdAsync(categoryId, cancellationToken);

            if(category is null)
            {
                throw new Exception(ErrorMessages.SOMETHING_WENT_WRONG);
            }

            category.DeletedOn = DateTime.UtcNow;
            category.DeletedByUserId = USER_ID;
            category.IsDeleted = true;

            _unitOfWork.ExpenseCategoryRepository.UpdateAsync(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateExpenseCategoryAsync(UpdateExpenseCategoryDto model, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.ExpenseCategoryRepository.GetByIdAsync(model.Id, cancellationToken);

            if (category is null)
            {
                throw new Exception(ErrorMessages.SOMETHING_WENT_WRONG);
            }

            category.Name = model.Name;
            category.Description = model.Description;
            category.ModifiedOn = DateTime.UtcNow;
            category.LastUpdatedByUserId = USER_ID;

            _unitOfWork.ExpenseCategoryRepository.UpdateAsync(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
