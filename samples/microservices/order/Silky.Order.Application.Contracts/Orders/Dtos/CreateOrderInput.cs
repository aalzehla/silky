using System.ComponentModel.DataAnnotations;

namespace Silky.Order.Application.Contracts.Orders.Dtos
{
    public class CreateOrderInput
    {
        /// <summary>
        /// accountId
        /// </summary>
        [Required(ErrorMessage = "accountIdEmpty is not allowed")]
        public long AccountId { get; set; }

        /// <summary>
        /// productId
        /// </summary>
        [Required(ErrorMessage = "productIdEmpty is not allowed")]
        public long ProductId { get; set; }

        /// <summary>
        /// Purchase quantity
        /// </summary>
        [Required(ErrorMessage = "product数量Empty is not allowed")]
        public int Quantity { get; set; }
    }
}