using System.Threading.Tasks;
using Silky.Order.Application.Contracts.Orders.Dtos;
using Silky.Rpc.Routing;
using Silky.Transaction;

namespace Silky.Order.Application.Contracts.Orders
{
    [ServiceRoute]
    public interface IOrderAppService
    {
        /// <summary>
        /// Add order interface
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Transaction]
        Task<GetOrderOutput> Create(CreateOrderInput input);
    }
}