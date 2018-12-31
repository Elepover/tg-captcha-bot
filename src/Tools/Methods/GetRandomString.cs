using System;
using System.Linq;

namespace telegram_captcha_bot
{
    namespace Tools
    {
        public static partial class Methods
        {
            public static string GetRandomString(int Length = 8)
            {
                const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                return new string(Enumerable.Repeat(Chars, Length).Select(s => s[(new Random()).Next(s.Length)]).ToArray());
            }
        }
    }
}