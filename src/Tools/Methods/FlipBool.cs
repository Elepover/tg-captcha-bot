using System;
using System.Linq;

namespace telegram_captcha_bot
{
    namespace Tools
    {
        public static partial class Methods
        {
            public static bool FlipBool(bool Input)
            {
                if (Input) { return false; } else { return true; }
            }
        }
    }
}