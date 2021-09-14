using GripItemTrade.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GripItemTrade.Domain.Accounts.Interfaces
{
	public interface IAccountService
	{
		Task<IResponseContainerWithValue<BalanceEntryTransferResult>> TransferAsync(Account sourceAccount, Account destinationAccount, ICollection<BalanceEntryTransferItem> transferItems);
	}
}
