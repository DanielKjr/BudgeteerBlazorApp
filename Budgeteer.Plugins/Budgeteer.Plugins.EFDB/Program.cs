

using Budgeteer.Plugins.EFDB;
using Microsoft.EntityFrameworkCore.Sqlite;

using var db = new ExpenseContext();

Console.WriteLine($"Database path: {db.DbPath}.");

Console.WriteLine("Inserting new expense");

db.Add(new ExpensePost { ExpenseName = "Rent" });
db.SaveChanges();

Console.WriteLine("Querying for an expensepost");

var post = db.Expenses.OrderBy(x => x.ExpenseId).First();

Console.WriteLine("ID: " + post.ExpenseId + "\nName: " + post.ExpenseName);


Console.ReadLine();
