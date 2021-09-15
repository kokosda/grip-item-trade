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
		protected readonly DataContext dataContext;

		public EfGenericRepository(DataContext dataContext)
		{
			this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
		}
		
		public async Task<T> CreateAsync<T, TId>(T entity) where T: EntityBase<TId>
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			await dataContext.AddAsync(entity);
			return entity;
		}

		/// <remarks>TODO: optimize for references' eager loading.</remarks>
		public async Task<T> GetAsync<T, TId>(TId id) where T : EntityBase<TId>
		{
			var result = await dataContext.FindAsync<T>(id);
			return result;
		}

		public Task UpdateAsync<T, TId>(T entity) where T : EntityBase<TId>
		{
			if (entity is null)
				throw new ArgumentNullException(nameof(entity));

			dataContext.Entry(entity).State = EntityState.Modified;
			return Task.CompletedTask;
		}

		public async Task DeleteAsync<T, TId>(TId id) where T : EntityBase<TId>
		{
			var entity = await GetAsync<T, TId>(id);
			dataContext.Remove(entity);
		}

		public async Task ApplyChangesAsync()
		{
			await dataContext.SaveChangesAsync();
		}
	}
}
