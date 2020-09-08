using Network.Extensions;
using Network.Protocol;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;

namespace Network.Methods.CA
{
    [Method(methodId: (uint)PacketHeader.HEADER_CA_LOGIN, size: MethodAttribute.packet_length_dynamic, name: "CA_LOGIN", direction: MethodAttribute.packetdirection.pd_in)]
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

                    var Id = BR.ReadInt16();

                    BR.BaseStream.Seek(9 - 4, SeekOrigin.Begin);
                    o.Username = BR.ReadBytes(24).NullByteTerminatedString();

                    BR.BaseStream.Seek(92 - 4, SeekOrigin.Begin);
                    o.Password = BR.ReadBytes(24).NullByteTerminatedString();

                    BR.BaseStream.Seek(54 - 4, SeekOrigin.Begin);
                    o.ClientType = BR.ReadSByte();

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
