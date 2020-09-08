using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Network.Methods.HC
{
    [Method(methodId: (uint)4, size: 4, name: "HC_ACCOUNTID", direction: MethodAttribute.packetdirection.pd_out)]
    public class Char_AccountId
    {
        public static byte[] Build(uint accountId)
        {
            using MemoryStream ms = new MemoryStream();
            using BinaryWriter bw = new BinaryWriter(ms);

            bw.Write(accountId);

            return ms.ToArray();
        }
    }
}
