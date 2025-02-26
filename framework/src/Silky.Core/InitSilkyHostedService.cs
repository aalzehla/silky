using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Silky.Core.Modularity;
using Silky.Core.Utils;

namespace Silky.Core
{
    public class InitSilkyHostedService : IHostedService
    {
        private readonly IModuleManager _moduleManager;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly ILogger<InitSilkyHostedService> _logger;

        public InitSilkyHostedService(IServiceProvider serviceProvider,
            IModuleManager moduleManager,
            IHostApplicationLifetime hostApplicationLifetime,
            ILogger<InitSilkyHostedService> logger)
        {
            if (EngineContext.Current is SilkyEngine)
            {
                EngineContext.Current.ServiceProvider = serviceProvider;
            }
            
            _moduleManager = moduleManager;
            _hostApplicationLifetime = hostApplicationLifetime;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine(@"                                              
   _____  _  _  _           
  / ____|(_)| || |          
 | (___   _ | || | __ _   _ 
  \___ \ | || || |/ /| | | |
  ____) || || ||   < | |_| |
 |_____/ |_||_||_|\_\ \__, |
                       __/ |
                      |___/
            ");

            Console.WriteLine($" :: Silky ::        {VersionHelper.GetSilkyVersion()}");
            Console.WriteLine($" :: Docs ::         https://docs.silky-fk.com\n");
            
            await _moduleManager.PreInitializeModules();
            _hostApplicationLifetime.ApplicationStarted.Register(async () =>
            {
                await _moduleManager.InitializeModules();
                await _moduleManager.PostInitializeModules();
                _logger.LogInformation("Initialized all Silky modules.");
            });
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _hostApplicationLifetime.ApplicationStopping.Register(async () =>
            {
                await _moduleManager.ShutdownModules();
                _logger.LogInformation("Shutdown all Silky modules.");
            });
        }
    }
}