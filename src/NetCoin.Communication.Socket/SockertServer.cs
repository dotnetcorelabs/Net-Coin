using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetCoin.Communication.Socket
{
    public class SockertServer : SocketServerBase
    {
        public SockertServer(IPAddress ipAddress, int port)
            : base(ipAddress, port)
        {
        }
    }
}
