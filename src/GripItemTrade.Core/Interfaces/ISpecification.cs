using GripItemTrade.Core.Domain;

namespace GripItemTrade.Core.Interfaces
{
	public interface ISpecification<in T, TId> where T : EntityBase<TId>
	{
		IResponseContainer IsSatisfiedBy(T entity);
	}
}