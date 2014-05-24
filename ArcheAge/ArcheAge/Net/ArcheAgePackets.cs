using ArcheAge.Properties;
using LocalCommons.Native.Network;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;

namespace ArcheAge.ArcheAge.Net
{

    public sealed class NP_Undefined : NetPacket
    {
        public NP_Undefined()
            : base(1, 0x1AB)
        {
            ns.Write((int)0x00);
            ns.Write((byte)0x00);
            ns.Write((int)0x00);
        }
    }

    /// <summary>
    /// Undefined Packet.
    /// </summary>
    public sealed class NP_ClientConnected : NetPacket
    {
        public NP_ClientConnected() : base(1, 0x00)
        {
            ns.Write((short)0x00);
            ns.Write((byte)0x00);
            ns.Write((int)0x00); //Undefined IP ??? Web Ip?
            ns.Write((short)0x00); //Undefined Port ??? Web Port ?
            ns.Write((int)0x00); //Undefined
            ns.Write((int)0x00000000); //Undefined
        }
    }
}
