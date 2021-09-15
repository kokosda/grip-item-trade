using GripItemTrade.Core.Interfaces;
using System.Threading.Tasks;

namespace GripItemTrade.Domain.Transactions.Interfaces
{
	public interface ITransactionalOperationRepository : IGenericRepository
	{
		Task<TransactionalOperation> GetByIdAsync(int id);
	}
}
