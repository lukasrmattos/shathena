using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Network.Methods.HC
{
    [Method(methodId: (uint)0x81, size: 6, name: "HC_ACCOUNTID", direction: MethodAttribute.packetdirection.pd_out)]
    public class AccountData
    {
        public static byte[] Build(uint accountId)
        {
            using MemoryStream ms = new MemoryStream();
            using BinaryWriter bw = new BinaryWriter(ms);

            bw.Write(0x81);
            bw.Write(8);

            return ms.ToArray();
        }
    }
}
