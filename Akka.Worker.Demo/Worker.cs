using Akka.Actor;
using Akka.DI.Core;
using Akka.DI.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Akka.Worker.Demo
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider serviceProvider;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var actorSystem = ActorSystem.Create("test-actor-system");

                actorSystem.UseServiceProvider(serviceProvider);

                var actor = actorSystem.ActorOf(actorSystem.DI().Props<NotificationActor>());

                actor.Tell("Hello there!");

                Console.ReadLine();

                actorSystem.Stop(actor);

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
