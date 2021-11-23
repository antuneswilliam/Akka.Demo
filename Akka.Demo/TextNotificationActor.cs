using Akka.Actor;
using System;
using System.Threading;

namespace Akka.Demo
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
            if (message.ToString() == "null")
                throw new NullReferenceException();

            if (message.ToString() == "arg")
                throw new ArgumentException();

            if (message.ToString() == "ex")
                throw new Exception();

            Console.WriteLine($"Sending text message: {message}");
        }
    }
}
