using System;
using System.Net.Sockets;
using System.Threading;

namespace WebServiceConsole
{
    internal class Program
    {
        private static string ListeningOn = "http://localhost:82/";

        private static void Main(string[] args)
        {
            var appHost = new AppHost();
            appHost.Init();
            appHost.Start(ListeningOn);

            Console.WriteLine("AppHost Created at {0}, listening on {1}", DateTime.Now, ListeningOn);

            Thread.Sleep(Timeout.Infinite);
            Console.WriteLine("ReadLine()");
            Console.ReadLine();
        }
    }
}