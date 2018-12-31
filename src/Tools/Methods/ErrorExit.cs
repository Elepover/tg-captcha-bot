using System;
using static telegram_captcha_bot.Logging.Logging;

namespace telegram_captcha_bot
{
    namespace Tools
    {
        public static partial class Methods
        {
            public static void ErrorExit()
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("fatal: unrecoverable error occurred. Check logs for troubleshooting.");
                Console.ForegroundColor = ConsoleColor.White;
                Environment.Exit(134); // return SIGABRT (128+6)
            }
        }
    }
}