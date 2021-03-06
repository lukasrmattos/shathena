﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Protocol
{
    public class Header
    {
        private uint _methodId;
        public uint MethodId
        {
            get { return _methodId; }
            set { _methodId = value; }
        }

        private int _headerSize;
        public int HeaderSize
        {
            get { return _headerSize; }
            set { _headerSize = value; }
        }

        private int _size;
        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public static Header ParseFrom(byte[] data)
        {
            Header header = new Header();
            header.MethodId = (uint)((data[1] << 8) | data[0]);
            header.HeaderSize += 2;

            header.Size = Protocol.PacketLengthMgr.GetPacketLengthForMethodId(header.MethodId);
            if (header.Size == -1)
            {
                header.Size = ((data[3] << 8) | data[2]);
                // workaroung
                if (header.MethodId == 2222)
                    header.Size = ((data[5] << 8) | data[4]);
                header.HeaderSize += 2;
            }

            header.Size -= header.HeaderSize;
            return header;
        }
    }
}
