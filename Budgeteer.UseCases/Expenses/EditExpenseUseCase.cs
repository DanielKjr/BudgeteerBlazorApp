using Budgeteer.Core;
using Budgeteer.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgeteer.UseCases
{
	public class EditExpenseUseCase : IEditExpenseUseCase
	{
		private readonly IExpenseRepository expenseRepository;

		public EditExpenseUseCase(IExpenseRepository expenseRepository)
		{
			this.expenseRepository = expenseRepository;
		}
		public async Task ExecuteAsync(Expense expense)
		{
			await this.expenseRepository.UpdateExpenseAsync(expense);
		}

	}
}
