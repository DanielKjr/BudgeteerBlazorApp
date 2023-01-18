using Budgeteer.Core;

namespace Budgeteer.UseCases
{
	public interface IViewExpenseByIdUseCase
	{
		Task<Expense> ExecuteAsync(int expenseId);
	}
}