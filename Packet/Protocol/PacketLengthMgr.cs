using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Protocol
{
    class PacketLengthMgr
    {
        public static int GetPacketLengthForMethodId(uint methodId)
        {
            return Methods.Method.GetSize(methodId);
        }
    }
}
