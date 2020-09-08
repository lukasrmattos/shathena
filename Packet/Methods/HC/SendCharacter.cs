using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Network.Methods.HC
{
    public class SendCharacter
    {
        public static byte[] Build_099d(List<object> characters)
        {
            using MemoryStream ms = new MemoryStream();
            using BinaryWriter bw = new BinaryWriter(ms);

            bw.Write((ushort)0x99d);
            bw.Write((ushort)0 + 4);

            Byte[] array = new Byte[20];

            Array.Clear(array, 0, array.Length);
            bw.Write(array);

            // This is something special Gravity came up with.
            // The client triggers some finalization code only if count is != 3.
            //if (count == 3)
            //{
            //    WFIFOHEAD(fd, 4);
            //    WFIFOW(fd, 0) = 0x99d;
            //    WFIFOW(fd, 2) = 4;
            //    WFIFOSET(fd, 4);
            //}

            return ms.ToArray();
        }

        public static byte[] Build_Slots(uint accountId)
        {
            using MemoryStream ms = new MemoryStream();
            using BinaryWriter bw = new BinaryWriter(ms);

            bw.Write((ushort)0x82d);
            bw.Write((ushort)29);
            bw.Write((byte)15); // min normal slot
            bw.Write((byte)5); // premium slot
            bw.Write((byte)5); // Billing Slot
            bw.Write((byte)10); // producible_slot
            bw.Write((byte)30); // valid_slot

            Byte[] array = new Byte[20];

            Array.Clear(array, 0, array.Length);
            bw.Write(array);

            return ms.ToArray();
        }

        public static byte[] Build_Chars(int max_chars, int min_chars, int chars_vip)
        {
            using MemoryStream ms = new MemoryStream();
            using BinaryWriter bw = new BinaryWriter(ms);

            int j, offset;

            offset = 3;
            j = 24 + offset; // offset

            bw.Write((ushort)0x6b);

            bw.BaseStream.Seek(4, SeekOrigin.Begin);
            bw.Write((byte)max_chars);
            bw.Write((byte)min_chars);
            bw.Write((byte)min_chars + chars_vip);

            bw.BaseStream.Seek(4 + offset, SeekOrigin.Begin);

            Byte[] array = new Byte[20];

            Array.Clear(array, 0, array.Length);
            bw.Write(array);

            bw.Write(j);

            bw.BaseStream.Seek(2, SeekOrigin.Begin);
            bw.Write((ushort)j);

            return ms.ToArray();
        }

        public static byte[] Build_Pages(uint char_slot)
        {
            using MemoryStream ms = new MemoryStream();
            using BinaryWriter bw = new BinaryWriter(ms);

            bw.Write((ushort)0x9a0);
            bw.Write((uint)char_slot > 3 ? char_slot / 3 : 1);

            return ms.ToArray();
        }

        public static byte[] Build_BlockCharacter()
        {
            using MemoryStream ms = new MemoryStream();
            using BinaryWriter bw = new BinaryWriter(ms);

            bw.Write((ushort)0x20d);
            bw.Write((ushort)4);

            return ms.ToArray();
        }

        public static byte[] Build_Pincode(uint account_id, int state)
        {
            using MemoryStream ms = new MemoryStream();
            using BinaryWriter bw = new BinaryWriter(ms);

            var rnd = new Random();

            bw.Write((ushort)0x8b9);
            bw.Write((uint)rnd.Next(0xFFFF));
            bw.Write((uint)account_id);
            bw.Write((ushort)state);

            return ms.ToArray();
        }
    }
}
