using Network;
using Network.Methods;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emulator
{
    public class FileData
    {
        private string _ipPort;
        private long _accountId;
        private ClientState _clientState;
        private Network.FdBuffer _buffer = new Network.FdBuffer();

        public FdBuffer Buffer { get => _buffer; }
        public ulong AccountId { get; set; }

        public FileData(string ipPort)
        {
            _ipPort = ipPort;
        }

        public override string ToString()
        {
            return _ipPort;
        }
    }
}
