using System.Collections.Generic;
using System.ComponentModel;
using Silky.Core.Extensions;

namespace Silky.Swagger.Abstraction.SwaggerGen.SwaggerGenerator
{
    public enum ResponsesCode
    {
        [Description("success")] Success = 200,

        [Description("frame exception")] FrameworkException = 500,
        
        [Description("business exception")] BusinessError = 1000,

        [Description("validation exception")] ValidateError = 1001,

        [Description("User friendly class exception")] UserFriendly = 1002,

        [Description("Not logged into the system")] UnAuthentication = 401,

        [Description("unauthorized access")] UnAuthorization = 403,

        // [Description("No routing information found")] NotFound = 404,
        //
        // [Description("checktokenfail")] IssueTokenError = 4011,
        //
        // [Description("Ban external network access")] FuseProtection = 406,

        [Description("request exception")] BadRequest = 400,
    }

    public static class ResponsesCodeHelper
    {
        public static IDictionary<ResponsesCode, string> GetAllCodes()
        {
            var codes = typeof(ResponsesCode).GetEnumSources<ResponsesCode>();
            return codes;
        }
    }
}