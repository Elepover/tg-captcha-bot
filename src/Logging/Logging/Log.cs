using System;

namespace telegram_captcha_bot
{
    namespace Logging
    {
        public static partial class Logging
        {
            public static void Log(string Text,
                                string Module = "CORE",
                                LogLevel Type = LogLevel.INFO)
            {
                string Output = "[" + DateTime.UtcNow.ToShortDateString() + " " + DateTime.UtcNow.ToShortTimeString() + "][" + Module + "]";
                Console.BackgroundColor = ConsoleColor.Black;
                switch (Type)
                {
                    case LogLevel.INFO:
                        Console.ForegroundColor = ConsoleColor.White;
                        Output += "[INFO] "; // Spacebars between prefixes and contents were already added here.
                        break;
                    case LogLevel.WARN:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Output += "[WARN] ";
                        break;
                    case LogLevel.ERROR:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Output += "[ERROR] ";
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Output += "[UKNN] ";
                        break;
                }
                Output += Text;
                Console.WriteLine(Output);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}