using GripItemTrade.Core.Domain;
using GripItemTrade.Core.Interfaces;
using GripItemTrade.Core.ResponseContainers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

		public async Task<IResponseContainer> ApplyChangesAsync()
		{
			var result = new ResponseContainer();

			try
			{
				await dataContext.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException ex)
			{
				var exceptionEntry = ex.Entries.Single();
				var databaseEntry = exceptionEntry.GetDatabaseValues();

				if (databaseEntry == null)
					result.AddErrorMessage($"Unable to save changes. The {exceptionEntry.GetType().Name} was deleted by another user.");
				else
					result.AddErrorMessage($"The {exceptionEntry.GetType().Name} you attempted to edit was modified in another transaction. Repeat your request later.");
			}

			return result;
		}
	}
}
