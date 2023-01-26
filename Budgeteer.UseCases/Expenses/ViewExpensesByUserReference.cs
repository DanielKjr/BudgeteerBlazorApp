using Budgeteer.Core;
using Budgeteer.Plugins.EFDB;
using Budgeteer.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgeteer.UseCases.Expenses
{
	public class ViewExpensesByUserReference : IViewExpensesByUserReference
	{
		private readonly IExpenseRepository expenseRepository;

		public ViewExpensesByUserReference(IExpenseRepository expenseRepository)
		{
			this.expenseRepository = expenseRepository;
		}

		public async Task<IEnumerable<Expense>> ExecuteAsync(User currentUser)
		{
			return await expenseRepository.GetExpensesByUserReference(currentUser);
		}
	}
}
