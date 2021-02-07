﻿using System.Threading.Tasks;
using Lms.Core;
using Lms.Rpc;
using Microsoft.Extensions.Hosting;
namespace NormHostDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {

            return Host.CreateDefaultBuilder(args)
                    .RegisterLmsServices<NormDemoModule>()
                    .UseRpcServer()
                    
                ;

        }
    }
}