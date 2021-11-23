using System;
using System.Collections.Generic;
using System.Text;

namespace Akka.Demo
{
    public class EmailNotification : IEmailNotification
    {
        public void Send(string message)
        {
            Console.WriteLine($"Sending email with message: {message}");
        }
    }
}
