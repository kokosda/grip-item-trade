using GripItemTrade.Core.Domain;
using GripItemTrade.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GripItemTrade.Infrastructure.DataAccess
{
	/// <summary>
	/// Entity Framework generic repository.
	/// </summary>
	public class EfGenericRepository: IGenericRepository
	{
		private readonly DataContext dbContext;

		public EfGenericRepository(DataContext dataContext)
		{
			this.dbContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
		}
		
		public async Task<T> CreateAsync<T, TId>(T entity) where T: EntityBase<TId>
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			await dbContext.AddAsync(entity);
			return entity;
		}

		/// <remarks>TODO: optimize for references' eager loading.</remarks>
		public async Task<T> GetAsync<T, TId>(TId id) where T : EntityBase<TId>
		{
			var result = await dbContext.FindAsync<T>(id);
			return result;
		}

		public Task UpdateAsync<T, TId>(T entity) where T : EntityBase<TId>
		{
			if (entity is null)
				throw new ArgumentNullException(nameof(entity));

			dbContext.Entry(entity).State = EntityState.Modified;
			return Task.CompletedTask;
		}

		public async Task DeleteAsync<T, TId>(TId id) where T : EntityBase<TId>
		{
			var entity = await GetAsync<T, TId>(id);
			dbContext.Remove(entity);
		}

		public async Task ApplyChangesAsync()
		{
			await dbContext.SaveChangesAsync();
		}
	}
}
