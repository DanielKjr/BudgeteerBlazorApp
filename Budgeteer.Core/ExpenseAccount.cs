using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgeteer.Core
{
	public class ExpenseAccount
	{
		[Required]
		public string Name { get; set; } = string.Empty;

		[Required]
		public int Id { get; set; }

		[Required]
		public string Salt { get; set; } = string.Empty;


	}
}
