using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoin.Common
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(string ASCIIMessage)
        {
            this.ASCIIMessage = ASCIIMessage;
        }

        public string ASCIIMessage { get; }
    }
}
