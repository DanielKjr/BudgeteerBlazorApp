using Budgeteer.Core;
using Budgeteer.Plugins.EFDB;

namespace Budgeteer.UseCases
{
	public interface IViewExpensesByUserReference
	{
		Task<IEnumerable<Expense>> ExecuteAsync(User currentUser);
	}
}