using Akka.Actor;
using Akka.DI.Core;
using System;

namespace Akka.Worker.Demo
{
    public class NotificationActor : UntypedActor
    {
        private readonly IEmailNotification emailNotification;
        private readonly IActorRef childActor;

        public NotificationActor(IEmailNotification emailNotification)
        {
            this.emailNotification = emailNotification;
            this.childActor = Context.ActorOf(Context.System.DI().Props<TextNotificationActor>());
        }

        protected override void OnReceive(object message)
        {
            Console.WriteLine($"Message received: {message}");
            emailNotification.Send(message?.ToString());
            childActor.Tell(message);
        }

        protected override void PreStart()
        {
            Console.WriteLine("Actor started");
        }

        protected override void PostStop()
        {
            Console.WriteLine("Actor stopped");
        }
    }
}
