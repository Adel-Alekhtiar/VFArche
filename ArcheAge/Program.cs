using ArcheAge.ArcheAge.Net;
using ArcheAge.ArcheAge.Net.Connections;
using ArcheAge.Properties;
using LocalCommons.Native.Logging;
using LocalCommons.Native.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ArcheAge
{
    /// <summary>
    /// Main Application Enter Point.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "ArcheAge Emu - Game Server";
            Console.CancelKeyPress += Console_CancelKeyPress;
            Stopwatch watch = Stopwatch.StartNew();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            LoadExecutingAssembly(args);
            watch.Stop();
            Logger.Trace("ArcheAge Started In {0} sec.", (watch.ElapsedMilliseconds / 1000).ToString("0.00"));
            watch = null;
            Key_Pressed();
        }

        static void Key_Pressed()
        {
            ConsoleKeyInfo info = Console.ReadKey();
            if (info != null)
            {
                Key_Pressed();
            }
        }

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            if (e.SpecialKey == ConsoleSpecialKey.ControlC)
            {
                Shutdown();
            }
        }

        static void Shutdown()
        {
            //HERE SHUTDOWN.
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Trace("Unhandled Exception - Sender: {0} , Exception - \n{1}", sender.GetType().Name, ((Exception)e.ExceptionObject).ToString());
            Console.WriteLine();
            Console.WriteLine("Press Any Key To Exit");
            Console.ReadKey();
            Environment.Exit(0);
        }

        static void LoadExecutingAssembly(string[] args)
        {
            //----- Initialize Commons --------------------------------
            Logger.Init(); //Load Logger
            LocalCommons.Native.Significant.Main.InitializeStruct(args); //Initializing LocalCommons.dll

            //------ Binary ------------------------------------------
            Logger.Section("Binary Data");

            //------ Network ------------------------------------------
            Logger.Section("Network");
            DelegateList.Initialize();
            InstallLoginServer();
            new AsyncListener(Settings.Default.ArcheAge_IP, Settings.Default.ArcheAge_Port, typeof(ClientConnection)); //Waiting For ArcheAge Connections
        }

        static void InstallLoginServer()
        {
            IPEndPoint point = new IPEndPoint(IPAddress.Parse(Settings.Default.LoginServer_IP), Settings.Default.LoginServer_Port);
            Socket con = new Socket(point.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            con.Connect(point);
            if (con.Connected)
                new LoginConnection(con);
            else
                InstallLoginServer();
        }
    }
}
