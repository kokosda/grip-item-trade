using System.Threading.Tasks;

namespace GripItemTrade.Infrastructure.DataAccess.Interfaces
{
	public interface IUnitOfWork
	{
		Task CommitAsync();
	}
}
