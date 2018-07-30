using NetCoin.Common.Logger;
using NetCoin.Communication.Socket.EventArgs;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetCoin.Communication.Socket
{
    public abstract class SocketClientBase
    {
        private readonly IPAddress _serverIpAddress;
        private readonly int _serverPort;
        private readonly TcpClient _tcpClient;
        private readonly byte[] _blockBlob = new byte[100];

        public delegate void ASCIIMessageReceived(object sender, MessageEventArgs args);

        public event EventHandler<MessageEventArgs> OnMessageReceived;

        private bool disposedValue = false; // To detect redundant calls

        public SocketClientBase(IPAddress serverIpAddress, int serverPort)
        {
            _serverIpAddress = serverIpAddress;
            _serverPort = serverPort;
            _tcpClient = new TcpClient();
        }

        public void Initialize()
        {
            try
            {
                _tcpClient.Connect(_serverIpAddress, _serverPort);

                Trace.WriteLine($"Connected to {_serverIpAddress}:{_serverPort}");
            }
            catch (Exception ex)
            {
                Trace.TraceException(ex);
            }
        }

        public void Send(string ASCIIContent)
        {
            ASCIIEncoding ascnd = new ASCIIEncoding();
            byte[] ba = ascnd.GetBytes(ASCIIContent);

            Stream stm = _tcpClient.GetStream();

            Trace.TraceInformation($"Sending.. {ASCIIContent}");

            stm.Write(ba, 0, ba.Length);

            string message = ReadMessage(stm);
            OnMessageReceived?.Invoke(this, new MessageEventArgs(message));
        }

        private string ReadMessage(Stream stream)
        {
            int k = stream.Read(_blockBlob, 0, 100);

            StringBuilder sbMessage = new StringBuilder();
            for (int i = 0; i < k; i++)
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
                    if (_tcpClient.Connected)
                        _tcpClient.Close();

                    _tcpClient.Dispose();
                }

                disposedValue = true;
            }
        }

        ~SocketClientBase()
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
