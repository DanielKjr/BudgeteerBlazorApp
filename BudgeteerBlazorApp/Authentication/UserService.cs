using Budgeteer.Core;
using Budgeteer.Plugins.EFDB;
using Budgeteer.Plugins.Encryption;

namespace BudgeteerBlazorApp.Authentication
{
	public class UserService
	{	
		private ExpenseContext context = new ExpenseContext();
		//TODO use this as a way to track current user perhaps
		public User? currentUser { get; private set; }

		public User? GetByUserName(string userName)
		{
			currentUser = context.Users.First(x => x.UserName == EncryptionHandler.Base64Encode(userName));

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
