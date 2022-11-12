
using Silky.Caching;

namespace Silky.Account.Application.Contracts.Accounts.Dtos
{
    [CacheName("GetAccountOutput")]
    public class GetAccountOutput
    {
        /// <summary>
        /// accountId
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// account名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// e-mail
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// account余额
        /// </summary>
        public decimal Balance { get; set; }
    }
}