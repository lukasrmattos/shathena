using Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using YamlDotNet.Core.Tokens;

namespace Network.Methods.AC
{
    [Method(methodId: (uint)PacketHeader.HEADER_AC_ACCEPT_LOGIN3, size: 160, name: "AC_ACCEPT_LOGIN3", direction: MethodAttribute.packetdirection.pd_out)]
    public class Char_AcceptLogin
    {
        public static byte[] Build(uint accountId, string ip, ushort port, Tuple<uint, uint> tokens)
        {
            using MemoryStream ms = new MemoryStream();
            using BinaryWriter bw = new BinaryWriter(ms);

            int cmd = 2756;
            int header = 64;
            int size = 160;

            int server_num = 1;
            int n = 0;

            Display.Show(ShowType.Debug, string.Format("Gerado Tokens {0} / {1}", tokens.Item1, tokens.Item2), ServerType.LOGIN);

            bw.Write((ushort)cmd); // 0
            bw.Write((ushort)(header + size * server_num)); // 2
            bw.Write((uint)tokens.Item1); // 4
            bw.Write((uint)accountId); // 8
            bw.Write((uint)tokens.Item2); // 12
            bw.Write((uint)0); // 16
            bw.Write(new byte[24]); // 20
            bw.Write((ushort)0); // 44
            bw.Write((sbyte)0); // Sex - 46
            bw.Write(new byte[17]); // 47
            bw.Write((uint)BitConverter.ToInt32(IPAddress.Parse(ip).GetAddressBytes(), 0)); // 64
            bw.Write((ushort)port); // 68
            bw.Write((char[])"Test".ToCharArray()); // 70
            bw.BaseStream.Seek(bw.BaseStream.Position + (20 - "Test".Length), SeekOrigin.Begin);

            bw.Write((ushort)0); // fake user count
            bw.Write((ushort)1);
            bw.Write((ushort)1);

            bw.Write(new byte[128]);

            return ms.ToArray();
        }
    }
}
