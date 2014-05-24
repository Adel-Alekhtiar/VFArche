using LocalCommons.Native.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Timers;

namespace LocalCommons.Native.Network
{
    /// <summary>
    /// Abstract Connection Which You Must Inherit
    /// Author: Raphail
    /// </summary>
    public abstract class IConnection : IDisposable
    {
        protected Socket m_Current;
        private SocketAsyncEventArgs m_AsyncReceive;
        private byte[] m_RecvBuffer;
        private object m_SyncRoot = new object();
        private Queue<NetPacket> m_PacketQueue;
        private bool m_Disposing;
        private DateTime m_NextCheckActivity;
        private string m_Address;
        private static int m_CoalesceSleep = -1;
        private bool m_BlockAllPackets;
        private DateTime m_ConnectedOn;
        private static BufferPool m_RecvBufferPool = new BufferPool("Receive", 2048, 2048);
        protected event EventHandler DisconnectedEvent;
        private bool m_Running;

        //For ArcheAge Connections.
        protected bool m_LittleEndian;

        /// <summary>
        /// Current TCP Client.
        /// </summary>
        public Socket CurrentChannel
        {
            get { return m_Current; }
        }

        /// <summary>
        /// Sleeping On Send.
        /// </summary>
        public static int CoalesceSleep
        {
            get { return m_CoalesceSleep; }
            set { m_CoalesceSleep = value; }
        }

        /// <summary>
        /// Blocking Packets - Not Send Means.
        /// </summary>
        public bool BlockAllPackets
        {
            get { return m_BlockAllPackets; }
            set { m_BlockAllPackets = value; }
        }

        /// <summary>
        /// New Instance Of IConnection or Any Your Connection.
        /// </summary>
        /// <param name="socket">Accepted Socket.</param>
        public IConnection(Socket socket)
        {
            //Console.Beep(392, 100);
            m_Current = socket;
            m_ConnectedOn = DateTime.Now;
            m_RecvBuffer = m_RecvBufferPool.AcquireBuffer();

            //-------------Async Receive ----------------------
            m_AsyncReceive = new SocketAsyncEventArgs();
            m_AsyncReceive.Completed += m_AsyncReceive_Completed;
            m_AsyncReceive.SetBuffer(m_RecvBuffer, 0, m_RecvBuffer.Length);
            //-------------------------------------------------

            m_PacketQueue = new Queue<NetPacket>();
            //-----------------------------------------------

            this.m_Address = ((IPEndPoint)m_Current.RemoteEndPoint).Address.ToString();
            if (m_Current == null)
                return;
            RunReceive();
            m_Running = true;
        }

        /// <summary>
        /// Set TRUE If you want Break Running.
        /// </summary>
        private bool BreakRunProcess;

        /// <summary>
        /// Start Running Receiving Process.
        /// </summary>
        public void RunReceive()
        {
            try
            {
                bool res = false;
                do
                {
                    if (m_AsyncReceive == null) //Disposed
                        break;
                    lock (m_SyncRoot)
                         res = !m_Current.ReceiveAsync(m_AsyncReceive);

                    if (res)
                        ProceedReceiving(m_AsyncReceive);
                }
                while (res);
            }
            catch (Exception e) {
                Logger.Trace(e.ToString());
                if (DisconnectedEvent != null)
                    DisconnectedEvent(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Adds Packet To Queue And After Send It.
        /// </summary>
        /// <param name="packet"></param>
        public virtual void SendAsync(NetPacket packet)
        {
            if (CoalesceSleep != -1)
                Thread.Sleep(CoalesceSleep);
            m_PacketQueue.Enqueue(packet);
            m_AsyncSend_Do();
        }

        /// <summary>
        /// Calls When We Need Send Data.
        /// </summary>
        private void m_AsyncSend_Do()
        {
            try
            {
                if (m_PacketQueue.Count > 0)
                {
                    NetPacket packet = m_PacketQueue.Dequeue();
                    byte[] compiled = packet.Compile();
                    StringBuilder builder = new StringBuilder();
                    foreach (byte b in compiled)
                        builder.AppendFormat("{0:X2} ", b);
                    Console.WriteLine("SEND: " + builder.ToString());
                    m_Current.Send(compiled, compiled.Length, SocketFlags.None);
                }
            }
            catch (Exception e)
            {
                Logger.Trace(e.ToString());
                if (DisconnectedEvent != null)
                    DisconnectedEvent(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Reading Length And Handles Data By [HandleReceived(byte[])] Without Length.
        /// </summary>
        /// <param name="e"></param>
        private void ProceedReceiving(SocketAsyncEventArgs e)
        {
            int transfered = e.BytesTransferred;
            if (e.SocketError != SocketError.Success || transfered <= 0)
            {
                if (DisconnectedEvent != null)
                    DisconnectedEvent(this, EventArgs.Empty);
                return;
            }

#if DEBUG
            //--- Console Hexadecimal 
            StringBuilder builder = new StringBuilder();
            builder.Append("\n");
            for (int i = 0; i < transfered; i++)
                builder.AppendFormat("{0:x2} ".ToUpper(), m_RecvBuffer[i]);
            
            Logger.Trace(builder.ToString());
            //--- Console Hexadecimal
#endif

            PacketReader reader = new PacketReader(m_RecvBuffer, 0);
            short length = m_LittleEndian ? reader.ReadLEInt16() : reader.ReadInt16();

            byte[] data = new byte[length];
            Buffer.BlockCopy(m_RecvBuffer, 2, data, 0, length);
            HandleReceived(data);
            reader = null;
        }

        /// <summary>
        /// Calls When Data Received From Server.
        /// </summary>
        /// <param name="data"></param>
        public abstract void HandleReceived(byte[] data);

        /// <summary>
        /// Returns Address Of Current Connection.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return m_Address;
        }

        /// <summary>
        /// Calls When Receiving Done.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_AsyncReceive_Completed(object sender, SocketAsyncEventArgs e)
        {
            ProceedReceiving(e);
            if (!m_Disposing)
                RunReceive();
        }

        /// <summary>
        /// Fully Dispose Current Connection.
        /// Can Be Overriden.
        /// </summary>
        public virtual void Dispose()
        {
            if (m_Current == null || m_Disposing)
                return;

            m_Disposing = true;

            try { m_Current.Shutdown(SocketShutdown.Both); }
            catch (SocketException ex) { Logger.Trace(ex.ToString()); }


            try { m_Current.Close(); }
            catch (SocketException ex) { Logger.Trace(ex.ToString()); }

            if (m_RecvBuffer != null)
                m_RecvBufferPool.ReleaseBuffer(m_RecvBuffer);
            
            m_Current = null;
            m_RecvBuffer = null;
            m_AsyncReceive = null;
            if (m_PacketQueue.Count <= 0)
                lock (m_PacketQueue)
                    m_PacketQueue.Clear();

            m_PacketQueue = null;
            m_Disposing = false;
            m_Running = false;
        }
    }
}
