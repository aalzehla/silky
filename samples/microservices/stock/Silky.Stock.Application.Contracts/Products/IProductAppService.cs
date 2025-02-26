using System.Threading.Tasks;
using Silky.Stock.Application.Contracts.Products.Dtos;
using Microsoft.AspNetCore.Mvc;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;
using Silky.Rpc.Runtime.Server;
using Silky.Transaction;

namespace Silky.Stock.Application.Contracts.Products
{
    [ServiceRoute]
    public interface IProductAppService
    {
        /// <summary>
        /// Add product
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetProductOutput> Create(CreateProductInput input);

        /// <summary>
        /// update product
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UpdateCachingIntercept("Product:Id:{0}")]
        Task<GetProductOutput> Update(UpdateProductInput input);

        /// <summary>
        /// passIdGet product information
        /// </summary>
        /// <param name="id">productId</param>
        /// <returns></returns>
        [GetCachingIntercept("Product:Id:{0}")]
        [HttpGet("{id:long}")]
        Task<GetProductOutput> Get([CacheKey(0)]long id);

        /// <summary>
        /// passId删除product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [RemoveCachingIntercept("GetProductOutput","Product:Id:{0}")]
        [HttpDelete("{id:long}")]
        Task Delete([CacheKey(0)]long id);

        [Transaction]
        [RemoveCachingIntercept("GetProductOutput","Product:Id:{0}")]
        [Governance(ProhibitExtranet = true)]
        Task<GetProductOutput> DeductStock(DeductStockInput input);
    }
}