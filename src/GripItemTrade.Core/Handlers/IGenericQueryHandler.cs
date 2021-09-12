using GripItemTrade.Core.Interfaces;
using System.Threading.Tasks;

namespace GripItemTrade.Core.Handlers
{
	public interface IGenericQueryHandler<in TQuery, TResult>
	{
		Task<IResponseContainerWithValue<TResult>> HandleAsync(TQuery query);
	}
}
