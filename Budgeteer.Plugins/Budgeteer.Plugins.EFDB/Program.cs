

using Budgeteer.Plugins.EFDB;
using Budgeteer.Plugins.Encryption;
using Microsoft.EntityFrameworkCore.Sqlite;

using var db = new ExpenseContext();

#region dbTestLines
//Console.WriteLine($"Database path: {db.DbPath}.");

//Console.WriteLine("Inserting new expense");


//db.Add(new User() { UserName = "Daniel" });
//User user = db.Users.First(x => x.UserName == "Daniel");


//user.Entries.Add(new ExpenseEntry() { ExpenseName = "Rent", Cost = 4000 });
//db.SaveChanges();





//post = db.Expenses.OrderBy(x => x.ExpenseId).First();

//db.SaveChanges();

//Console.WriteLine("Querying for an expensepost");
//var entry = db.Entries.OrderBy(x => x.ExpenseId).Last();

//var post = db.Expenses.OrderBy(x => x.ExpenseId).First();

//Console.WriteLine("ID: " + post.ExpenseId + "\nName: " + post.ExpenseName);
#endregion

#region EncryptionTests
string password;
string salt;

Console.WriteLine("Write a password");
password = Console.ReadLine();
salt = EncryptionHandler.GetRandomSalt();

Console.WriteLine($"The salt: {salt}\n");

string hashPassword = EncryptionHandler.HashPassword(password, salt);

Console.WriteLine($"Password: {password}\nSalt: {salt}\nHashedPassword: {hashPassword}");

#endregion

Console.ReadLine();
