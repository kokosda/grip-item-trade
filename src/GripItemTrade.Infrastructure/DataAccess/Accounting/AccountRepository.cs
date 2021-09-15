using GripItemTrade.Domain.Accounts;
using GripItemTrade.Domain.Accounts.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GripItemTrade.Infrastructure.DataAccess.Accounting
{
	public sealed class AccountRepository : EfGenericRepository, IAccountRepository
	{
		public AccountRepository(DataContext dataContext) : base(dataContext)
		{
		}

		public async Task<Account> GetByIdAsync(int id)
		{
			var result = await dataContext.Accounts.Include(a => a.Customer)
				.Include(a => a.BalanceEntries)
				.FirstOrDefaultAsync(a => a.Id == id);
			return result;
		}
	}
}
