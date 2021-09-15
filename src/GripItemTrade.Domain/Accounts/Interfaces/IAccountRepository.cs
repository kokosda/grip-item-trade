using GripItemTrade.Core.Interfaces;
using System.Threading.Tasks;

namespace GripItemTrade.Domain.Accounts.Interfaces
{
	public interface IAccountRepository : IGenericRepository
	{
		Task<Account> GetByIdAsync(int id);
	}
}
