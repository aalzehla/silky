using Silky.Order.Domain.Shared.Orders;

namespace Silky.Order.Application.Contracts.Orders.Dtos
{
    public class GetOrderOutput
    {
        /// <summary>
        /// OrderId
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// accountId
        /// </summary>
        public long AccountId { get; set; }
        
        /// <summary>
        /// productId
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Purchase quantity
        /// </summary>
        public int Quantity { get; set; }
        
        /// <summary>
        /// Order金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Order状态
        /// </summary>
        public OrderStatus Status { get; set; }
    }
}