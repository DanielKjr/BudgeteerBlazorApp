using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Budgeteer.Plugins.EFDB
{
	public class ExpenseContext : DbContext
	{
		public DbSet<ExpensePost> Expenses { get; set; }
		public DbSet<Entry> Entrys { get; set; }

		public string DbPath { get; } = string.Empty;

		public ExpenseContext()
		{
			var folder = Environment.SpecialFolder.LocalApplicationData;
			var path = Environment.GetFolderPath(folder);
			DbPath = System.IO.Path.Join(path, "expenses.db");
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		=> options.UseSqlite($"Data Source={DbPath}");

	}


	public class ExpensePost
	{
		[Key]
		public int ExpenseId { get; set; }
		public string ExpenseName { get; set;} = string.Empty;
	}


	public class Entry
	{
		public int EntryId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;

		public int ExpenseId { get; set; }
		public ExpensePost Post { get; set; } = new ExpensePost();
		
	}
}
