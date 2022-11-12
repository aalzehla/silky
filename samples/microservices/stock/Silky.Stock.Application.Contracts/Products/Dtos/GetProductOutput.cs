
using Silky.Caching;

namespace Silky.Stock.Application.Contracts.Products.Dtos
{
    [CacheName("GetProductOutput")]
    public class GetProductOutput : ProductDtoBase
    {
        /// <summary>
        /// productId
        /// </summary>
        public long Id { get; set; }
    }
}