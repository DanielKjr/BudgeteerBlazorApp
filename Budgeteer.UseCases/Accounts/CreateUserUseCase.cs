using Budgeteer.Core;
using Budgeteer.UseCases.PluginInterfaces;

namespace Budgeteer.UseCases
{
	public class CreateUserUseCase : ICreateUserUseCase
	{
		private readonly IExpenseRepository expenseRepository;

		public CreateUserUseCase(IExpenseRepository expenseRepository)
		{
			this.expenseRepository = expenseRepository;
		}

		public async Task ExecuteAsync(AccountCreation account)
		{
			await this.expenseRepository.AddUserAsync(account);
		}
	}
}
