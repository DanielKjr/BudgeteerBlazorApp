using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Budgeteer.Core
{
    public enum ExpenseInterval { Monthly = 1, Quarterly = 3, Yearly = 12}
    public class Expense
    {
        public int ExpenseId { get; set; }
        //Avoid null reference
        [Required]
        [StringLength(150)]
        public string ExpenseName { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage ="Cost must be greater than or equal to 0")]
        public int ExpenseCost { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage ="Interval must be Monthly, Quarterly or Yearly")]
        public ExpenseInterval Interval { get; set; }



	}
}