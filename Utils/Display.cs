using ColoredConsole;
using System;
using System.Globalization;

namespace Shared
{
    public class Display
    {
        public static void Header()
        {
            string header =
                "  #####                                                   \n" +
                " #     # #    #   ##   ##### #    # ###### #    #   ##    \n" +
                " #       #    #  #  #    #   #    # #      ##   #  #  #   \n" +
                "  #####  ###### #    #   #   ###### #####  # #  # #    #  \n" +
                "       # #    # ######   #   #    # #      #  # # ######  \n" +
                " #     # #    # #    #   #   #    # #      #   ## #    #  \n" +
                "  #####  #    # #    #   #   #    # ###### #    # #    #  \n";
                                                         

            Console.WriteLine(header);
        }

        public static void ShowBreakLine()
        {
            Console.WriteLine("---------------------------------------------------------------");
        }

        public static void Show(ShowType show, string msg, ServerType svr = ServerType.ALL)
        {
            ColorToken ct = "".Cyan();

            switch(show)
            {
                case ShowType.Debug:
                    ct = show.ToString().DarkCyan();
                    break;

                case ShowType.Error:
                    ct = show.ToString().Red();
                    break;

                case ShowType.Exception:
                    ct = show.ToString().Magenta();
                    break;

                case ShowType.Info:
                    ct = show.ToString().Gray();
                    break;

                case ShowType.Status:
                    ct = show.ToString().Green();
                    break;

                case ShowType.Warn:
                    ct = show.ToString().Yellow();
                    break;
            }

            switch(svr)
            {
                case ServerType.ALL:
                    ColorConsole.WriteLine("[", ct, "]: ", msg);
                    break;

                case ServerType.CHAR:
                    ColorConsole.WriteLine("[", "Char-Server".Magenta(), "|", ct, "]: ", msg);
                    break;

                case ServerType.LOGIN:
                    ColorConsole.WriteLine("[", "Login-Server".Blue(), "|", ct, "]: ", msg);
                    break;

                case ServerType.MAP:
                    ColorConsole.WriteLine("[", "Map-Server".Yellow(), "|", ct, "]: ", msg);
                    break;
            }
        }
    }
}
