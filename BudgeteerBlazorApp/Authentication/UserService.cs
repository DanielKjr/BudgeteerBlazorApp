using Budgeteer.Core;
using Budgeteer.Plugins.EFDB;

namespace BudgeteerBlazorApp.Authentication
{
	public class UserService
	{	
		private ExpenseContext context = new ExpenseContext();
		//TODO use this as a way to track current user perhaps
		private User? currentUser;

		public User? GetByUserName(string userName)
		{
			currentUser = context.Users.First(x => x.UserName == userName);

			if (currentUser != null)
			{
				return currentUser;
			}
			else
			{
				return null;
			}
			
		}

		public UserSalt? GetSaltByUserReference(User user)
		{
			return context.Salts.First(x => x.User == user);
		}
	}
}
