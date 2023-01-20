using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Budgeteer.Plugins.EFDB
{
	public class ExpenseContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<ExpenseEntry> Entries { get; set; }

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


	public class User
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int UserID { get; set; }
		[Required]
		public string UserName { get; set;} = string.Empty;
	}


	public class ExpenseEntry
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int EntryId { get; set; }
		[Required]
		public string ExpenseName { get; set; } = string.Empty;
		[Required]
		public int Cost { get; set; }
		public User User { get; set; } = new User();



		//Since theres no CLR property which holds the foreign key
		//for this relationship, a shadow property is created
		public int ExpenseId { get; set; }

	}
}
