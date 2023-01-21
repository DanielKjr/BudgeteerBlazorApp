using Budgeteer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgeteer.UseCases.PluginInterfaces
{
    public interface IExpenseRepository
    {
		Task AddExpenseAsync(Expense expense);
		Task<IEnumerable<Expense>> GetExpensesByNameAsync(string name);

        Task UpdateExpenseAsync(Expense expense);
        Task<Expense> GetExpenseByIdAsync(int expenseId);
        Task<Expense> DeleteExpenseByIdAsync(int expenseId);

        Task AddUserAsync(ExpenseAccount account);
    }
}
