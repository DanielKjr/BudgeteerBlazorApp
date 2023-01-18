using Budgeteer.Core;
using Budgeteer.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgeteer.UseCases
{
	public class AddExpenseUseCase : IAddExpenseUseCase
	{
		private readonly IExpenseRepository expenseRepository;
		public AddExpenseUseCase(IExpenseRepository expenseRepository)
		{
			this.expenseRepository = expenseRepository;
		}

		public async Task ExecuteAsync(Expense expense)
		{
			
			await this.expenseRepository.AddExpenseAsync(expense);
		}
	}
}
