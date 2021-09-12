using System.Threading.Tasks;
using GripItemTrade.Core.Domain;

namespace GripItemTrade.Core.Interfaces
{
	public interface IGenericRepository<T, in TId> where T: EntityBase<TId>
	{
		Task<T> CreateAsync(T entity);
		Task<T> GetAsync(TId id);
		Task UpdateAsync(T entity);
		Task DeleteAsync(TId id);
	}
}
