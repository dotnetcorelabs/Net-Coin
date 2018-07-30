using NetCoin.Common.Logger;
using NetCoin.Communication.Socket.EventArgs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetCoin.Communication.Socket
{
    public abstract class SocketServerBase : IDisposable
    {
        private readonly IPAddress _ipAddress;
        private readonly int _port;
        private readonly TcpListener _tcpListener;
        private readonly byte[] _blockBlob = new byte[100];

        public delegate void ASCIIMessageReceived(object sender, MessageEventArgs args);

        public event EventHandler<MessageEventArgs> OnMessageReceived;
        private System.Net.Sockets.Socket _socket;

        private bool disposedValue = false; // To detect redundant calls

        public SocketServerBase(IPAddress ipAddress, int port)
        {
            _port = port;
            _ipAddress = ipAddress;

            _tcpListener = new TcpListener(ipAddress, port);
        }

        public void Initialize()
        {
            try
            {
                _tcpListener.Start();
                Trace.WriteLine($"Server is running on address {_ipAddress}:{_port}");
                Trace.WriteLine("Local endpoint:" + _tcpListener.LocalEndpoint);
            }
            catch (Exception ex)
            {
                Trace.TraceException(ex);
            }
        }

        public void Send(string ASCIIContent)
        {
            ASCIIEncoding asencd = new ASCIIEncoding();
            _socket.Send(asencd.GetBytes(ASCIIContent));
            Trace.TraceVerbose("\nAutomatic Message is Sent");
        }

        public void Listen()
        {
            Trace.WriteLine("Waiting for Connections...");

            _socket = _tcpListener.AcceptSocket();

            string message = ReceiveMessage();
            OnMessageReceived?.Invoke(this, new MessageEventArgs(message));
        }

        protected string ReceiveMessage()
        {
            int size = _socket.Receive(_blockBlob);

            Trace.WriteLine("Recieved..");
            StringBuilder sbMessage = new StringBuilder();

            for (int i = 0; i < size; i++)
            {
                sbMessage.Append(Convert.ToChar(_blockBlob[i]));
            }

            return sbMessage.ToString();
        }

        #region IDisposable Support
        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_socket != null)
                    {
                        if (_socket.Connected)
                        {
                            _socket.Close();
                        }
                        _socket.Dispose();
                    }

                    _tcpListener.Stop();
                }

                disposedValue = true;
            }
        }

        ~SocketServerBase()
        {
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
