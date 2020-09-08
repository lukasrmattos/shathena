using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Network.Methods.AC
{
    [Method(methodId: (uint)PacketHeader.HEADER_AC_REFUSE_LOGIN_R2, size: 26 - 4, name: "AC_REFUSE_LOGIN_R2", direction: MethodAttribute.packetdirection.pd_out)]
    public class Login_RefuseLogin
    {
        public static byte[] Build(ushort error)
        {
            using MemoryStream ms = new MemoryStream();
            using BinaryWriter bw = new BinaryWriter(ms);

            bw.BaseStream.Seek(0, SeekOrigin.Begin);
            bw.Write((ushort)2110);

            bw.BaseStream.Seek(2, SeekOrigin.Begin);
            bw.Write(error);

            bw.BaseStream.Seek(26, SeekOrigin.Begin);
            bw.Write(26);

            return ms.ToArray();
        }
    }
}
