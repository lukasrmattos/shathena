using Network.Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Methods
{
    public interface IMethodIn
    {
        public dynamic Parse(Header header, byte[] data);
    }

    public interface IMethodOut
    {
        void WriteTo(System.IO.BinaryWriter output);
    }
}
