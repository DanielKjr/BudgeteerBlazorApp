using Budgeteer.Core;

namespace Budgeteer.UseCases
{
	public interface ICreateUserUseCase
	{
		Task ExecuteAsync(AccountCreation account);
	}
}