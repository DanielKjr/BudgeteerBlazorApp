using Budgeteer.Core;
using Budgeteer.Plugins.EFDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgeteer.UseCases.PluginInterfaces
{
    public interface IExpenseRepository
    {
		Task AddExpenseAsync(Expense expense, User currentUser);
		Task<IEnumerable<Expense>> GetExpensesByNameAsync(string name);

        Task UpdateExpenseAsync(Expense expense);
        Task<Expense> GetExpenseByIdAsync(int expenseId);
        Task<IEnumerable<Expense>> GetExpensesByUserReference(User currentUser);
        Task<Expense> DeleteExpenseByIdAsync(int expenseId);

        Task AddUserAsync(AccountCreation account);
    }
}
