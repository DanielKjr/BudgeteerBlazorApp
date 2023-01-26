using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgeteer.Core;
using Budgeteer.Plugins.EFDB;
using Budgeteer.UseCases;
using Budgeteer.UseCases.PluginInterfaces;

namespace Budgeteer.UseCases
{
    public class ViewExpensesByName : IViewExpensesByName
    {
        private readonly IExpenseRepository expenseRepository;

        public ViewExpensesByName(IExpenseRepository expenseRepository)
        {
            this.expenseRepository = expenseRepository;
        }

        public async Task<IEnumerable<Expense>> ExecuteAsync(string name = "")
        {
            return await expenseRepository.GetExpensesByNameAsync(name);
        }
    }

	
}
