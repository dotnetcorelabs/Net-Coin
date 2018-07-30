using NetCoin.Common.Helpers;
using NetCoin.Communication.Socket;
using NetCoin.Communication.Socket.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerChainApp.Wins10
{
    public class Program
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

            SockertServer client = new SockertServer(IPAddress.Parse(ipAddress), int.Parse(port));
            client.Initialize();
            client.OnMessageReceived += Client_OnMessageReceived;
            client.Listen();

            Console.WriteLine("Write an message");
            string message = Console.ReadLine();
            client.Send(message);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            Console.ReadLine();
        }

        private static void Client_OnMessageReceived(object sender, MessageEventArgs e)
        {
            Console.WriteLine($"Message received {e.ASCIIMessage}");
        }
    }
}
