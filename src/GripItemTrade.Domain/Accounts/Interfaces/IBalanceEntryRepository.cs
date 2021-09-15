using GripItemTrade.Core.Interfaces;
using System.Threading.Tasks;

namespace GripItemTrade.Domain.Accounts.Interfaces
{
	public interface IBalanceEntryRepository : IGenericRepository
	{
		Task<BalanceEntry> GetByIdAsync(int id);
	}
}
