﻿using ArcheAge.ArcheAge.Structuring;
using LocalCommons.Native.Logging;
using LocalCommons.Native.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
namespace ArcheAge.ArcheAge.Net.Connections
{

    /// <summary>
    /// Connection That Used For ArcheAge Client( Game Side )
    /// </summary>
    public class ClientConnection : IConnection
    {
        //----- Static
        private static Dictionary<int, Account> m_CurrentAccounts = new Dictionary<int, Account>();
        private byte m_Random;
        public static Dictionary<int, Account> CurrentAccounts
        {
            get { return m_CurrentAccounts; }
        }

        //----- Static

        private Account m_CurrentAccount;
        
        public Account CurrentAccount
        {
            get { return m_CurrentAccount; }
            set { m_CurrentAccount = value; }
        }

        public ClientConnection(Socket socket) : base(socket) {
            DisconnectedEvent += ClientConnection_DisconnectedEvent;
            m_LittleEndian = true;
            Logger.Trace("Client {0}: Connected", this);
        }

        public override void SendAsync(NetPacket packet)
        {
            packet.IsArcheAgePacket = true;
            base.SendAsync(packet);
        }

        void ClientConnection_DisconnectedEvent(object sender, EventArgs e)
        {
            Dispose();
            Logger.Trace("Client {0} : Disconnected", this);
        }

        public override void HandleReceived(byte[] data)
        {
            PacketReader reader = new PacketReader(data, 0);
            reader.Offset += 1; //Undefined Random Byte
            byte level = reader.ReadByte(); //Packet Level
            short opcode = reader.ReadLEInt16(); //Packet Opcode

            if (!DelegateList.ClientHandlers.ContainsKey(level))
            {
                Logger.Trace("Received Undefined Level {0} - Opcode 0x{1:X2}", level, opcode);
                return;
            }

            PacketHandler<ClientConnection> handler = DelegateList.ClientHandlers[level][opcode];
            if (handler != null)
                handler.OnReceive(this, reader);
            else
                Logger.Trace("Received Undefined Packet Level - {0} Op - 0x{1:X2}", level, opcode);
        }
    }
}
