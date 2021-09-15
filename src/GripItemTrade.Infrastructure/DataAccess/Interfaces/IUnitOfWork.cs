using GripItemTrade.Core.Interfaces;
using System.Threading.Tasks;

namespace GripItemTrade.Infrastructure.DataAccess.Interfaces
{
	public interface IUnitOfWork
	{
		Task<IResponseContainer> CommitAsync();
	}
}
