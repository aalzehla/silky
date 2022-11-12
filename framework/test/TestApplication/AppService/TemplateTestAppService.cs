using System.Collections.Generic;
using System.Threading.Tasks;
using ITestApplication.Test;
using Silky.Core.Runtime.Session;
using Silky.Rpc.Runtime.Client;

namespace TestApplication.AppService;

public class TemplateTestAppService : ITemplateTestAppService
{
    private readonly IInvokeTemplate _invokeTemplate;

    public TemplateTestAppService(IInvokeTemplate invokeTemplate)
    {
        _invokeTemplate = invokeTemplate;
    }

    // public async Task<string> CallDeleteOne()
    // {
    //     var result = await _invokeTemplate.DeleteForObjectAsync<string>("/api/another/{name}", "test");
    //     return result;
    // }

    public async Task<string> CallCreateOne(string name)
    {
        var result = await _invokeTemplate.PostForObjectAsync<string>("api/another/{name}", name);
        return result;
    }

    public async Task<dynamic> CallTest()
    {
        // 1. The parameter order is consistent with the provider
        // var result =
        //     await _invokeTemplate.PostForObjectAsync<dynamic>("api/another/test",
        //         new { Name = "Zhang San", Address = "beijing" });

        // 2. Pass parameters using a dictionary
        var result =
            await _invokeTemplate.PostForObjectAsync<dynamic>("api/another/test", new Dictionary<string, object>()
            {
                { "input", new { Name = NullSession.Instance.UserName, Address = "beijing" } }
            });
        return result;
    }

    // public Task Upload(IFormFile file)
    // {
    //     throw new System.NotImplementedException();
    // }
}