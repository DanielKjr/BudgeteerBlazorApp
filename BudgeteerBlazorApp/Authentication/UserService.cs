using Budgeteer.Core;
using Budgeteer.Plugins.EFDB;

namespace BudgeteerBlazorApp.Authentication
{
	public class UserService
	{	
		private ExpenseContext context = new ExpenseContext();


		public User? GetByUserName(string userName)
		{
			return context.Users.First(x => x.UserName == userName);
		}

		public UserSalt? GetSaltByUserReference(User user)
		{
			return context.Salts.First(x => x.User == user);
		}
	}
}
