using FalconeOne.BLL.Interfaces;
using FalconOne.API.Attributes;
using FalconOne.Models.Dtos.ExpenseManagement;
using FalconOne.Security;
using Microsoft.AspNetCore.Mvc;

namespace FalconOne.API.Controllers.ExpenseManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : BaseSecureController
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet("dashboard-info")]
        [FalconOneAuthorize(PermissionPool.ExpenseManagement.VIEW_EXPENSES_DASHBOARD)]
        public async Task<IActionResult> GetDashboardInfo(CancellationToken cancellationToken)
        {
            var result = await _expenseService.GetDashboardInfoAsync(cancellationToken);

            return Ok(result);
        }

        [HttpGet("{expenseId}/details")]
        [FalconOneAuthorize(PermissionPool.ExpenseManagement.VIEW_EXPENSE_INFO)]
        public async Task<IActionResult> GetExpenseDetails(Guid expenseId,CancellationToken cancellationToken)
        {
            var result = await _expenseService.GetExpenseDetailsAsync(expenseId, cancellationToken);

            return Ok(result);
        }

        [HttpGet("types")]
        [FalconOneAuthorize(PermissionPool.ExpenseManagement.VIEW_ALL_EXPENSE_TYPES_LOOKUP)]
        public async Task<IActionResult> GetAllExpenseTypesForLookup()
        {
            var result = await _expenseService.GetAllExpenseTypesAsync();

            return Ok(result);
        }

        [HttpPost("get-all-expenses")]
        [FalconOneAuthorize(PermissionPool.ExpenseManagement.VIEW_ALL_EXPENSES)]
        public async Task<IActionResult> GetAllExpenses(GetExpensesDto model, CancellationToken cancellationToken)
        {
            var result = await _expenseService.GetAllExpensesAsync(model, cancellationToken);

            return Ok(result);
        }

        [HttpPost("add-expense")]
        [FalconOneAuthorize(PermissionPool.ExpenseManagement.ADD_EXPENSE)]
        public async Task<IActionResult> AddExpense(AddExpenseDto model, CancellationToken cancellationToken)
        {
            var result = await _expenseService.AddExpenseAsync(model, cancellationToken);

            return Created(GetRequestURI(), result);
        }

        [HttpPost("add-expenses-bulk")]
        [FalconOneAuthorize(PermissionPool.ExpenseManagement.ADD_EXPENSES_BULK)]
        public async Task<IActionResult> AddExpenses(List<AddExpenseDto> model, CancellationToken cancellationToken)
        {
            var result = await _expenseService.AddExpensesAsync(model, cancellationToken);

            return Created(GetRequestURI(), result);
        }

        [HttpPatch("update")]
        [FalconOneAuthorize(PermissionPool.ExpenseManagement.UPDATE_EXPENSE)]
        public async Task<IActionResult> UpdateExpense(UpdateExpenseDto model, CancellationToken cancellationToken)
        {
            await _expenseService.UpdateExpenseAsync(model, cancellationToken);

            return Accepted();
        }

        [HttpDelete("{expenseId}/delete")]
        [FalconOneAuthorize(PermissionPool.ExpenseManagement.DELETE_EXPENSE)]
        public async Task<IActionResult> DeleteExpense(Guid expenseId, CancellationToken cancellationToken)
        {
            await _expenseService.DeleteExpenseAsync(expenseId, cancellationToken);

            return NoContent();
        }
    }
}
