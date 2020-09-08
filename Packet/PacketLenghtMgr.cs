using Network.Methods;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network
{
    class PacketLenghtMgr
    {
        public static int GetPacketLengthForMethodId(uint methodId)
        {
            return Method.GetSize(methodId); ;
        }
    }
}
