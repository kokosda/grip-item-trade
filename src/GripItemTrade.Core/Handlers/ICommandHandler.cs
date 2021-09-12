using GripItemTrade.Core.Interfaces;
using System.Threading.Tasks;

namespace GripItemTrade.Core.Handlers
{
	public interface ICommandHandler<in T>
	{
		Task<IResponseContainer> HandleAsync(T command);
	}
}
