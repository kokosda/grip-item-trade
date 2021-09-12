using System.Data;

namespace GripItemTrade.Core.Interfaces
{
	public interface ISqlConnectionFactory
	{
		IDbConnection GetOpenConnection();
	}
}
