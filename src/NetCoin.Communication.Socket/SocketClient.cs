using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetCoin.Communication.Socket
{
    public sealed class SocketClient : SocketClientBase
    {
        public SocketClient(IPAddress ipAddress, int port)
            : base(ipAddress, port)
        {
        }
    }
}
