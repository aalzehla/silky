using System.ComponentModel;

namespace Silky.Core.Exceptions
{
    public enum StatusCode
    {
        [Description("success")] Success = 200,

        [Description("frame exception")] FrameworkException = 500,

        [Description("Route parsing exception")] RouteParseError = 503,

        [Description("Service route not found")] NotFindServiceRoute = 404,

        [Description("No usable service addresses found")] NotFindServiceRouteAddress = 504,

        [Description("非frame exception")] NonSilkyException = 505,

        [Description("Communication exception")] CommunicatonError = 506,

        [Description("Local service entry not found")] NotFindLocalServiceEntry = 507,

        [Description("Failed callback implementation class not found")] NotFindFallbackInstance = 508,

        [Description("Failed to execute callback method failed")] FallbackExecFail = 509,

        [Description("Distributed transaction exception")] TransactionError = 511,

        [Description("TccDistributed transaction exception")] TccTransactionError = 512,

        [Description("Service entry does not exist")] NotFindServiceEntry = 513,

        [Description("Exceeded maximum concurrency")] OverflowMaxRequest = 514,

        [Description("Exceeded maximum concurrency")] OverflowMaxServerHandle = 515,

        [Description("UnServiceKeyImplementation")]
        UnServiceKeyImplementation = 516,

        [Description("rpcCommunication authentication failed")] RpcUnAuthentication = 517,

        [Description("There is an exception in cache interception")] CachingInterceptError = 518,

        [Description("execution timeout")] Timeout = 519,

        [Description("Server exception")] ServerError = 520,

        [Description("No content")] NoContent = 521,

        [Description("business exception")] [IsBusinessException]
        BusinessError = 1000,

        [Description("validation exception")] [IsUserFriendlyException]
        ValidateError = 1001,

        [Description("User friendly class exception")] [IsUserFriendlyException]
        UserFriendly = 1002,

        [Description("not certified")] [IsUnAuthorizedException]
        UnAuthentication = 401,

        [Description("unauthorized")] [IsUnAuthorizedException]
        UnAuthorization = 403,

        [Description("No routing information found")] NotFound = 404,

        [Description("checktokenfail")] [IsUnAuthorizedException]
        IssueTokenError = 4011,

        [Description("Ban external network access")] FuseProtection = 406,
        
        [Description("request exception")] BadRequest = 400,
        
        [Description("Request timed out")] DeadlineExceeded = 408,
    }
}