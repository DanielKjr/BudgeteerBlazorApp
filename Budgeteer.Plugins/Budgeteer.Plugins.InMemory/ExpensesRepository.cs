using Budgeteer.Core;
using Budgeteer.Plugins.EFDB;
using Budgeteer.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Budgeteer.Plugins.InMemory
{
	public class ExpensesRepository : IExpenseRepository
	{

		private List<Expense> _expenses;
		private ExpenseContext context;

		public ExpensesRepository()
		{
			_expenses = new List<Expense>();
			context = new ExpenseContext();

		}


		public async Task AddExpenseAsync(Expense expense, User currentUser)
		{
			//This alongside the other comments somehow fixes the problem.
			context.ChangeTracker.Clear();

			var doesEntryExist = context.Entries.Any(x => x.ExpenseName == expense.ExpenseName && x.User.UserID == currentUser.UserID);
			if (!doesEntryExist)
			{

				ExpenseEntry entry = new ExpenseEntry()
				{
					ExpenseName = Encryption.EncryptionHandler.Base64Encode(expense.ExpenseName),
					Cost = expense.ExpenseCost,
					Interval = expense.Interval,
					User = currentUser
				};

				//I have no idea 
				//https://stackoverflow.com/questions/43500403/ef-core-sqlite-one-to-many-relationship-failing-on-unique-index-constraint			
				context.ChangeTracker.TrackGraph(entry, node => node.Entry.State =
				!node.Entry.IsKeySet ? EntityState.Added : EntityState.Unchanged);

				//completely clueless
				//	https://stackoverflow.com/questions/58340818/entity-framework-core-insert-without-changetracking
				var changedEntriesCopy = context.ChangeTracker.Entries()
			   .Where(e => e.State == EntityState.Added ||
			   e.State == EntityState.Modified ||
			   e.State == EntityState.Deleted).ToList();

				await context.SaveChangesAsync();

				//shouldn't be necessary but works?!
				foreach (var item in changedEntriesCopy)
				{
					item.State = EntityState.Detached;
				}
			}
			else
			{
				await Task.FromException(new Exception("An entry with that name already exists"));
			}





		}

		public async Task AddUserAsync(AccountCreation account)
		{
			//Check if username is taken
			var doesUserExist = context.Users.Any(x => x.UserName == account.Name);
			if (!doesUserExist)
			{
				account.Salt = Encryption.EncryptionHandler.GetRandomSalt();
				User newUser = new User()
				{
					UserName = Encryption.EncryptionHandler.Base64Encode(account.Name),
					HashedPassword = Encryption.EncryptionHandler.HashPassword(account.Password, account.Salt)
				};

				await context.AddAsync(newUser);
				await context.AddAsync(new UserSalt() { Salt = account.Salt, User = newUser });
				await context.SaveChangesAsync();

			}
			else
			{
				await Task.FromException(new Exception("Username is taken."));
			}

		}

		public async Task DeleteExpenseByIdAsync(int expenseId)
		{
			var expense = context.Entries.First(x => x.EntryId == expenseId);
			context.Entries.Remove(expense);
			await context.SaveChangesAsync();

		}

		public async Task<Expense> GetExpenseByIdAsync(int expenseId)
		{
			var expense = context.Entries.First(x => x.EntryId == expenseId);
			//create new object to avoid it being a reference
			var newExpense = new Expense
			{
				ExpenseId = expense.EntryId,
				ExpenseName = expense.ExpenseName,
				ExpenseCost = expense.Cost,
				Interval = expense.Interval
			};

			return await Task.FromResult(newExpense);

		}

		public async Task<IEnumerable<Expense>> GetExpensesByNameAsync(string name)
		{
			if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_expenses);

			return _expenses.Where(x => x.ExpenseName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
		}

		public async Task<IEnumerable<Expense>> GetExpensesByUserReference(User currentUser)
		{
			var doesUserExist = context.Users.Any(x => x.UserID == currentUser.UserID);
			_expenses.Clear();
			if (doesUserExist)
			{

				User user = context.Users.First(x => x.UserID == currentUser.UserID);
				user.Entries = context.Entries.Where(e => e.User == user).ToList();

				if (user.Entries.Count != 0)
				{
					foreach (ExpenseEntry entry in user.Entries)
					{
						_expenses.Add(new Expense
						{
							ExpenseId = entry.EntryId,
							ExpenseName =Encryption.EncryptionHandler.Base64Decode(entry.ExpenseName),
							ExpenseCost = entry.Cost,
							Interval = entry.Interval
						});
					}
				}

				return await Task.FromResult(_expenses);
			}

			return _expenses;

		}

		public Task UpdateExpenseAsync(Expense expense)
		{
			//make sure there's no duplicate names
			if (_expenses.Any(x => x.ExpenseId != expense.ExpenseId && x.ExpenseName.Equals(expense.ExpenseName, StringComparison.OrdinalIgnoreCase)))
				return Task.CompletedTask;

			var exp = _expenses.FirstOrDefault(x => x.ExpenseId == expense.ExpenseId);
			if (exp != null)
			{
				exp.ExpenseName = expense.ExpenseName;
				exp.ExpenseCost = expense.ExpenseCost;
				exp.Interval = expense.Interval;
			}
			return Task.CompletedTask;
		}
	}
}