using Budgeteer.Core;

namespace Budgeteer.UseCases
{
    public interface IViewExpensesByName
    {
        Task<IEnumerable<Expense>> ExecuteAsync(string name = "");
    }
}