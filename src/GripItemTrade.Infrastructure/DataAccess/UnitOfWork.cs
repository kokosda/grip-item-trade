using GripItemTrade.Core.Interfaces;
using GripItemTrade.Infrastructure.DataAccess.Interfaces;
using System;
using System.Threading.Tasks;

namespace GripItemTrade.Infrastructure.DataAccess
{
	public sealed class UnitOfWork : IUnitOfWork
	{
		private readonly IGenericRepository genericRepository;

		public UnitOfWork(IGenericRepository genericRepository)
		{
			this.genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
		}

		public async Task CommitAsync()
		{
			await genericRepository.ApplyChangesAsync();
		}
	}
}
