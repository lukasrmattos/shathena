
using Configuration;
using Database;
using Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Emulator
{
    public static class Core
    {
        public static Conf Conf;

        public static Server LoginServer;
        public static Thread LoginThread;

        public static Server CharServer;
        public static Thread CharThread;

        public static Server MapServer;
        public static Thread MapThread;

        public static void Init()
        {
            Display.Header();

            #region Configuration
            Conf = new Conf();
            if (!Conf.Ok)
            {
                Display.Show(ShowType.Error, "Failed to load configurations, aborting...");
                Console.ReadKey();
            }

            Display.Show(ShowType.Status, "All configurations loaded!");

            #endregion

            #region Database
            DatabaseRo.Configure(Conf.Inter.Database);
            #endregion

            LoginThread = new Thread(delegate ()
            {
                Display.Show(ShowType.Status, "Login-Server Online!");
                LoginServer = new Server((ushort)6900, 1000, ServerType.LOGIN);
            });

            LoginThread.Start();

            CharThread = new Thread(delegate ()
            {
                Display.Show(ShowType.Status, "Char-Server Online!");
                CharServer = new Server((ushort)6121, 1000, ServerType.CHAR);
            });

            CharThread.Start();

            System.Threading.Tasks.Task.Delay(-1).Wait();
        }
    }
}
