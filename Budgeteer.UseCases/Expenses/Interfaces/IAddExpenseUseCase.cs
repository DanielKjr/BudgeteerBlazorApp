using Budgeteer.Core;
using Budgeteer.Plugins.EFDB;

namespace Budgeteer.UseCases
{
	public interface IAddExpenseUseCase
	{
		Task ExecuteAsync(Expense expense, User currentUser);
	}
}