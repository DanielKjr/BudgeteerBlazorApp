using Budgeteer.Core;

namespace Budgeteer.UseCases
{
	public interface IDeleteExpenseByIdUseCase
	{
		Task ExecuteAsync(Expense expense);
	}
}