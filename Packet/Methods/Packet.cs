using Network.Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Methods
{
    public abstract class Packet
    {
        public abstract void Parse(Header header, byte[] data);
    }
}
