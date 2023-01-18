using Budgeteer.Core;
using Budgeteer.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgeteer.UseCases.Expenses
{
	public class ViewExpenseByIdUseCase : IViewExpenseByIdUseCase
	{
		private readonly IExpenseRepository expenseRepository;

		public ViewExpenseByIdUseCase(IExpenseRepository expenseRepository)
		{
			this.expenseRepository = expenseRepository;
		}

		public async Task<Expense> ExecuteAsync(int expenseId)
		{
			return await this.expenseRepository.GetExpenseByIdAsync(expenseId);
		}

	}
}
