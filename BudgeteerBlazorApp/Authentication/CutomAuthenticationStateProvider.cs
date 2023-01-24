using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace BudgeteerBlazorApp.Authentication
{
	public class CustomAuthenticationStateProvider : AuthenticationStateProvider
	{
		private readonly ProtectedSessionStorage sessionStorage;
		private ClaimsPrincipal anonymous = new ClaimsPrincipal(new ClaimsIdentity());

		public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
		{
			this.sessionStorage = sessionStorage;
		}
		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			try
			{
				var userSessionStorageResult = await sessionStorage.GetAsync<UserSession>("UserSession");
				var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;

				if (userSession == null)
					return await Task.FromResult(new AuthenticationState(anonymous));

				var claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(ClaimTypes.Name, userSession.UserName),
				new Claim(ClaimTypes.Role, userSession.Role)
			}, "CustomAuth"));

				return await Task.FromResult(new AuthenticationState(claimPrincipal));
			}
			catch
			{

				return await Task.FromResult(new AuthenticationState(anonymous));
			}

		}

		public async Task UpdateUthenticationState(UserSession userSession)
		{
			ClaimsPrincipal claimsPrincipal;

			if (userSession != null)
			{
				await sessionStorage.SetAsync("UserSession", userSession);
				claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(ClaimTypes.Name, userSession.UserName),
				new Claim(ClaimTypes.Role, userSession.Role)
			}));
			}
			else
			{
				await sessionStorage.DeleteAsync("UserSession");
				claimsPrincipal = anonymous;
			}

			NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
		}
	}
}
