using Akka.Actor;
using System;

namespace Akka.Worker.Demo
{
    public class TextNotificationActor : UntypedActor
    {
        protected override void PreStart()
        {
            Console.WriteLine("TextNotification child started!");
        }

        protected override void PostStop()
        {
            Console.WriteLine("TextNotification child stopped!");
        }

        protected override void OnReceive(object message)
        {
            Console.WriteLine($"Sending text message: {message}");
        }
    }
}
