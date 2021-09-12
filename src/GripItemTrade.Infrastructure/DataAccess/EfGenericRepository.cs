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
	public class EfGenericRepository<T, TId> : IGenericRepository<T, TId> where T: EntityBase<TId>
	{
		private readonly DbContext dbContext;
		private readonly DbSet<T> dbSet;

		public EfGenericRepository(DbContext dbContext)
		{
			this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			dbSet = this.dbContext.Set<T>();
		}
		
		public async Task<T> CreateAsync(T entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			await dbSet.AddAsync(entity);
			await dbContext.SaveChangesAsync();
			return entity;
		}

		/// <remarks>TODO: optimize for references' eager loading.</remarks>
		public async Task<T> GetAsync(TId id)
		{
			var result = await dbSet.FindAsync(id);
			return result;
		}

		public async Task UpdateAsync(T entity)
		{
			if (entity is null)
				throw new ArgumentNullException(nameof(entity));

			dbContext.Entry(entity).State = EntityState.Modified;
			await dbContext.SaveChangesAsync();
		}

		public async Task DeleteAsync(TId id)
		{
			var entity = await GetAsync(id);
			dbSet.Remove(entity);
		}
	}
}
