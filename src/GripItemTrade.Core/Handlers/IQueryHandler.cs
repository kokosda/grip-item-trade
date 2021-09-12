using System.Threading.Tasks;
using GripItemTrade.Core.Interfaces;

namespace GripItemTrade.Core.Handlers
{
    public interface IQueryHandler<in T>
    {
        Task<IResponseContainer> HandleAsync(T query);
    }
}