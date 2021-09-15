using GripItemTrade.Domain.Accounts;
using GripItemTrade.Domain.Accounts.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GripItemTrade.Infrastructure.DataAccess.Accounting
{
	public sealed class BalanceEntryRepository : EfGenericRepository, IBalanceEntryRepository
	{
		public BalanceEntryRepository(DataContext dataContext) : base(dataContext)
		{
		}

		public async Task<BalanceEntry> GetByIdAsync(int id)
		{
			var result = await dataContext.BalanceEntries.Include(be => be.Account)
				.Include(be => be.Customer)
				.FirstOrDefaultAsync(be => be.Id == id);
			return result;
		}
	}
}
