using GripItemTrade.Domain.Transactions;
using GripItemTrade.Domain.Transactions.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GripItemTrade.Infrastructure.DataAccess.Transactions
{
	public sealed class TransactionalOperationRepository : EfGenericRepository, ITransactionalOperationRepository
	{
		public TransactionalOperationRepository(DataContext dataContext) : base(dataContext)
		{
		}

		public async Task<TransactionalOperation> GetByIdAsync(int id)
		{
			var result = await dataContext.TransactionalOperations
				.Include(to => to.ParentOperation)
				.Include(to => to.Entries)
				.Include(to => to.Account)
				.Include(to => to.DependentOperations)
				.ThenInclude(@do => @do.Account)
				.FirstOrDefaultAsync(to => to.Id == id);

			return result;
		}
	}
}
