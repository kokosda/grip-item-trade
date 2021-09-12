using System.Threading.Tasks;

namespace GripItemTrade.Core.Interfaces
{
	public interface ICommonSpecification<in T>
	{
		Task<IResponseContainer> IsSatisfiedBy(T subject);
	}
}
