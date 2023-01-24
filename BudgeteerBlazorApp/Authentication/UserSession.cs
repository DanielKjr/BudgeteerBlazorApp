namespace BudgeteerBlazorApp.Authentication
{
	public class UserSession
	{
		public event Action OnChange;
		private bool isLoggedIn;
		public bool IsLoggedIn
		{ get { return isLoggedIn; }
			set
			{
				if (isLoggedIn != value)
				{
					isLoggedIn = value;
					NotifyStateChanged();
				}
			}
		}
		public string UserName { get; set;} = string.Empty;

		public string Role { get; set;} = string.Empty;

		private void NotifyStateChanged() => OnChange?.Invoke();
	}
}
