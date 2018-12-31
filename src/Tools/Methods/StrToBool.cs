using System;

namespace telegram_captcha_bot
{
    namespace Tools
    {
        public static partial class Methods
        {
            public static bool StrToBool(string Input)
            {
                switch (Input.ToLower())
                {
                    case "true":
                        return true;
                    case "false":
                        return false;
                    default:
                        throw (new ArgumentException("Unable to parse input to any boolean value."));
                }
            }
        }
    }
}