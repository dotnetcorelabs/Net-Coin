using NetCoin.Common;
using NetCoin.Common.Helpers;
using NetCoin.Communication.Socket;
using System;
using System.Net;

namespace ClientChainApp.Wins10
{
    class Program
    {
        const string IP_ARG_NAME = "--ip";
        const string PORT_ARG_NAME = "--port";

        static void Main(string[] args)
        {
            if (!ArgumentsHelpers.TryGetArgument(args, IP_ARG_NAME, out string ipAddress))
            {
                ipAddress = "127.0.0.1";
            }
            if (!ArgumentsHelpers.TryGetArgument(args, PORT_ARG_NAME, out string port))
            {
                port = "8000";
            }

            SocketClient client = new SocketClient(IPAddress.Parse(ipAddress), int.Parse(port));
            client.Initialize();
            client.OnMessageReceived += Client_OnMessageReceived;

            Console.WriteLine("Write an message");
            string message = Console.ReadLine();
            client.Send(message);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static void Client_OnMessageReceived(object sender, MessageEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            Console.WriteLine($"Message received {e.ASCIIMessage}");
        }
    }
}
