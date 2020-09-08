using Network.Extensions;
using Network.Protocol;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;

namespace Network.Methods.CH
{
    [Method(methodId: (uint)PacketHeader.HEADER_CH_LOGIN, size: 17, name: "CH_LOGIN", direction: MethodAttribute.packetdirection.pd_in)]
    public class Login : IMethodIn
    {
        public dynamic Parse(Header header, byte[] data)
        {
            using (MemoryStream MS = new MemoryStream(data))
            using (BinaryReader BR = new BinaryReader(MS))
            {
                try
                {
                    dynamic o = new ExpandoObject();

                    o.AccountId = BR.ReadUInt32();
                    o.Token1 = BR.ReadUInt32();
                    o.Token2 = BR.ReadUInt32();

                    return o;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return null;
                }
            }
        }
    }
}
