using System.Threading.Tasks;
using Silky.Account.Application.Contracts.Accounts.Dtos;
using Microsoft.AspNetCore.Mvc;
using Silky.Rpc.CachingInterceptor;
using Silky.Rpc.Routing;
using Silky.Rpc.Runtime.Server;
using Silky.Rpc.Security;
using Silky.Transaction;

namespace Silky.Account.Application.Contracts.Accounts
{
    /// <summary>
    /// Account service
    /// </summary>
    [ServiceRoute]
    public interface IAccountAppService
    {
        /// <summary>
        /// Add account
        /// </summary>
        /// <param name="input">account information</param>
        /// <returns></returns>
        Task<GetAccountOutput> Create(CreateAccountInput input);
        
        /// <summary>
        /// login interface
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        Task<string> Login(LoginInput input);
        
        [AllowAnonymous]
        [HttpPost("dashboard/login")]
        Task<string> DashboardLogin(DashboardLoginInput input);

        /// <summary>
        /// Get the current logged in user
        /// </summary>
        /// <returns></returns>
        [HttpGet("current/userinfo")]
        [GetCachingIntercept("CurrentUserInfo",OnlyCurrentUserData = true)]
        Task<GetAccountOutput> GetLoginUserInfo();

        /// <summary>
        /// Get account by account name
        /// </summary>
        /// <param name="name">Account Name</param>
        /// <returns></returns>
        [GetCachingIntercept("Account:UserName:{0}")]
        [HttpGet("{name}")]
        Task<GetAccountOutput> GetAccountByName([CacheKey(0)] string name);

        /// <summary>
        /// passId获取account information
        /// </summary>
        /// <param name="id">accountId</param>
        /// <returns></returns>
        [GetCachingIntercept("Account:Id:{0}")]
        [HttpGet("{id:long}")]
        Task<GetAccountOutput> GetAccountById([CacheKey(0)] long id);

        /// <summary>
        /// 更新account information
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UpdateCachingIntercept( "Account:Id:{0}")]
        Task<GetAccountOutput> Update(UpdateAccountInput input);

        /// <summary>
        /// 删除account information
        /// </summary>
        /// <param name="id">accountId</param>
        /// <returns></returns>
        [RemoveCachingIntercept("GetAccountOutput","Account:Id:{0}")]
        [HttpDelete("{id:long}")]
        Task Delete([CacheKey(0)]long id);

        /// <summary>
        /// Order debit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Governance(ProhibitExtranet = true)]
        [RemoveCachingIntercept("GetAccountOutput","Account:Id:{0}")]
        [Transaction]
        Task<long?> DeductBalance(DeductBalanceInput input);
    }
}