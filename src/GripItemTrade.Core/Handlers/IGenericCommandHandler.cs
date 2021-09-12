using GripItemTrade.Core.Interfaces;
using System.Threading.Tasks;

namespace GripItemTrade.Core.Handlers
{
	public interface IGenericCommandHandler<in TCommand, TResult>
	{
		Task<IResponseContainerWithValue<TResult>> HandleAsync(TCommand command);
	}
}
