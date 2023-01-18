using Budgeteer.Core;
using Budgeteer.UseCases.PluginInterfaces;

namespace Budgeteer.UseCases
{
	public class DeleteExpenseByIdUseCase : IDeleteExpenseByIdUseCase
	{
		private readonly IExpenseRepository expenseRepository;

		public DeleteExpenseByIdUseCase(IExpenseRepository expenseRepository)
		{
			this.expenseRepository = expenseRepository;
		}

		public async Task ExecuteAsync(Expense expense)
		{
			await this.expenseRepository.DeleteExpenseByIdAsync(expense.ExpenseId);
		}
	}
}
