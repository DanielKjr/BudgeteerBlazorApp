using Budgeteer.Core;

namespace Budgeteer.UseCases
{
	public interface IAddExpenseUseCase
	{
		Task ExecuteAsync(Expense expense);
	}
}