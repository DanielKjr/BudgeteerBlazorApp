using Budgeteer.Core;
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
		public DbSet<UserSalt> Salts { get; set; }

		public string DbPath { get; } = string.Empty;

		public ExpenseContext()
		{
			
#if DEBUG
			var folder = Environment.SpecialFolder.LocalApplicationData;
			var path = Environment.GetFolderPath(folder);
			DbPath = System.IO.Path.Join(path, "expenses.db");
#endif
#if RELEASE
			DbPath = System.IO.Path.Combine(Environment.CurrentDirectory, "expenses.db");
#endif
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

		[Required]
		public string HashedPassword { get; set;} = string.Empty;
		public ICollection<ExpenseEntry> Entries { get; set; } = new HashSet<ExpenseEntry>();

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

		[Required]
		public virtual User? User { get; set; }

		[Required]
		public ExpenseInterval Interval { get; set; }





	}

	public class UserSalt
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int SaltId { get; set; }

		[Required]
		public string Salt { get; set; } = string.Empty;

		[Required]
		public User? User { get; set; }

	}
}
