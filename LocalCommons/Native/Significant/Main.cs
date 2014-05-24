using LocalCommons.Native.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace LocalCommons.Native.Significant
{
    /// <summary>
    /// Initializing Class
    /// Author: Raphail
    /// </summary>
    public class Main
    {
        public static readonly bool Is64Bit = Environment.Is64BitProcess;
        private static AutoResetEvent signal = new AutoResetEvent(true);

        public static void Set() { signal.Set(); }

        public static void InitializeStruct(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Logger.Section("Assembly References");

            Version ver = Assembly.GetExecutingAssembly().GetName().Version;

            Logger.Trace("ArcheAge Emu - Version {0}.{1}, Build {2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision);
            Logger.Trace("Main: Running On .NET Framework (C#) Version {0}.{1}.{2}", Environment.Version.Major, Environment.Version.Minor, Environment.Version.Build);
            Logger.Trace("For Stop Server With Data Saving Please Press Ctrl + C");
            int platform = (int)Environment.OSVersion.Platform;
            if (platform == 4 || platform == 128)
            {
                Logger.Trace("Main: Unix Platform Detected");
            }
            Console.ResetColor();
        }
    }
}
