using LocalCommons.Native.Network;
using LocalCommons.Native.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcheAgeLogin.ArcheAge.Network
{
    /// <summary>
    /// Sends Information About That Login Was right and we can continue =)
    /// </summary>
    public sealed class NP_AcceptLogin : NetPacket
    {
        public NP_AcceptLogin() : base(0x00, true)
        {
            ns.Write((short)0x00); //Undefined
            ns.Write((short)0x06); //Undefined
            ns.Write(new byte[6], 0, 6); //undefined
        }
    }

    /// <summary>
    /// Sends Request To Specified Game Server by Entered Information
    /// </summary>
    public sealed class NP_SendGameAuthorization : NetPacket
    {
        public NP_SendGameAuthorization(GameServer server, int m_AccountId) : base(0x0A, true)
        {
            ns.Write((int)m_AccountId);
            ns.WriteASCIIFixed(server.IPAddress, server.IPAddress.Length);
            ns.Write((short)server.Port);
        }
    }

    /// <summary>
    /// Sends Information About Current Servers To Client.
    /// </summary>
    public sealed class NP_ServerList : NetPacket
    {
        public NP_ServerList() : base(0x08, true)
        {
            List<GameServer> m_Current = GameServerController.CurrentGameServers.Values.ToList<GameServer>();

            ns.Write((byte)m_Current.Count);
            foreach (GameServer server in m_Current)
            {
                ns.Write((byte)server.Id);
                ns.WriteASCIIFixed(server.Name, server.Name.Length);

                ns.Write((byte) 0x01); //Always 1
                int status = server.IsOnline() ? (server.CurrentAuthorized.Count >= server.MaxPlayers ? 0x01 : 0x00) : 0x02;
                ns.Write((int)status); //Server Status - 0x00 - Online / 0x01 - Overload / 0x02 - Offline
                ns.Write((int)0x00); //Undefined
                ns.Write((short)0x00); //Undefined
            }
            ns.Write((byte)0x01); //Last Server Id?
            ns.Write((short)0x288C); //Current Users???
            ns.Write((short)0x22); //Undefined
            ns.Write((short)0x174); //Undefined
            ns.Write((short)0x3DEF); //Undefined
            ns.Write((byte)0x00); //Undefined
            
            //String? CharName? Probably Last Character.
            ns.WriteASCIIFixed("Raphael", "Raphael".Length);
            
            //Undefined
            ns.Write((byte)0x01);
            ns.Write((byte)0x02);

            //String? 
            //Undefined //Ten Char String Undefined
            ns.WriteASCIIFixed("Raphael", "Raphael".Length);
            ns.Write((int)0x00); //undefined
            ns.Write((int)0x00); //undefined
        }
    }

    /// <summary>
    /// Sends Information about that Password Were Correct
    /// If Not - Send NP_FailLogin.
    /// </summary>
    public sealed class NP_PasswordCorrect : NetPacket
    {
        public NP_PasswordCorrect(int sessionId) : base(0x03, true)
        {
            ns.Write((int)sessionId);
            string encrypted = "f3d02d5dda564e7bb4320de5b27f5c78";
            ns.WriteASCIIFixed(encrypted, encrypted.Length);
        }
    }

    /// <summary>
    /// Sends Information About Rijndael(AES) Key
    /// </summary>
    public sealed class NP_AESKey : NetPacket
    {
        public NP_AESKey() : base(0x04, true)
        {
            //Rijndael / SHA256
            ns.Write((int)5000); //Undefined? 5000
            //le - string
            ns.WriteASCIIFixed("xnDekI2enmWuAvwL", 16); //Always 16?
            byte[] b = new byte[32];
            ns.Write(b, 0, b.Length);
        }
    }

    /// <summary>
    /// Sends Message Box About That Error Occured While Logging In.
    /// </summary>
    public sealed class NP_FailLogin : NetPacket
    {
        public NP_FailLogin() : base(0x0C, true)
        {
            ns.Write((byte)0x02); // Reason
            ns.Write((short)0x00);//Undefined
            ns.Write((short)0x00);//Undefined
        }
    }
}
