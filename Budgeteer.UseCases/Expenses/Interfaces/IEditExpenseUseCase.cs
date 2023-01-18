using Budgeteer.Core;

namespace Budgeteer.UseCases
{
	public interface IEditExpenseUseCase
	{
		Task ExecuteAsync(Expense expense);
	}
}