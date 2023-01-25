﻿using Budgeteer.Core;
using Budgeteer.Plugins.EFDB;
using Budgeteer.UseCases.PluginInterfaces;

namespace Budgeteer.Plugins.InMemory
{
	public class ExpensesRepository : IExpenseRepository
	{

		private List<Expense> _expenses;
		private ExpenseContext context;

		public ExpensesRepository()
		{
			_expenses= new List<Expense>();
			context = new ExpenseContext();

		}

		public Task AddExpenseAsync(Expense expense)
		{
			if (_expenses.Any(x => x.ExpenseName.Equals(expense.ExpenseName, StringComparison.OrdinalIgnoreCase)))
				return Task.CompletedTask;

			if (_expenses.Count > 1)
			{
				var maxId = _expenses.Max(x => x.ExpenseId);

				expense.ExpenseId = maxId + 1;
			}
			else
			{
				expense.ExpenseId = 1;
			}
			



			_expenses.Add(expense);
			return Task.CompletedTask;
		}

		public  Task AddUserAsync(AccountCreation account)
		{			
            var existingUserCount = context.Users.Count(x => x.UserName == account.Name);
			if (existingUserCount == 0)
			{
				account.Salt = Encryption.EncryptionHandler.GetRandomSalt();
				User newUser = new User()
				{
					UserName = account.Name,
					HashedPassword = Encryption.EncryptionHandler.HashPassword(account.Password, account.Salt)
				};

				context.Add(newUser);
				context.Add(new UserSalt() { Salt = account.Salt, User = newUser });
				context.SaveChanges();
              
            }

            return Task.CompletedTask;
        }

		public async Task<Expense> DeleteExpenseByIdAsync(int expenseId)
		{
			var expense = _expenses.First(x => x.ExpenseId == expenseId);

			_expenses.Remove(expense);

			return await Task.FromResult(expense);

			
		}

		public async Task<Expense> GetExpenseByIdAsync(int expenseId)
		{
			var expense = _expenses.First(x => x.ExpenseId == expenseId);
			//create new object to avoid it being a reference
			var newExpense = new Expense
			{
				ExpenseId = expense.ExpenseId,
				ExpenseName = expense.ExpenseName,
				ExpenseCost = expense.ExpenseCost,
				Interval = expense.Interval
			};

			return await Task.FromResult(newExpense);

		}

		public async Task<IEnumerable<Expense>> GetExpensesByNameAsync(string name)
		{
			if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_expenses);

			return _expenses.Where(x => x.ExpenseName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
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