using Akka.Actor;
using Akka.DI.Core;
using Akka.DI.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;

namespace Akka.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<IEmailNotification, EmailNotification>();
            serviceCollection.AddScoped<NotificationActor>();
            serviceCollection.AddScoped<TextNotificationActor>();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            using var actorSystem = ActorSystem.Create("test-actor-system");

            actorSystem.UseServiceProvider(serviceProvider);

            var actor = actorSystem.ActorOf(actorSystem.DI().Props<NotificationActor>());

            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("Type 'exit' to quit");
            Console.WriteLine("Type 'arg' to test ArgumentException and Resume the Actor");
            Console.WriteLine("Type 'null' to test NullReferenceException and Restart the Actor");
            Console.WriteLine("Type 'ex' to test a regular Exception and Stop the Actor");
            Console.WriteLine("Type any other message to test the Actor");
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine();

            while (true)
            {                              
                var message = Console.ReadLine();

                if (message == "exit")
                    break;

                actor.Tell(message);
            }

            actorSystem.Stop(actor);
        }
    }
}
