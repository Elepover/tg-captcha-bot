using System;
using telegram_captcha_bot.Settings;
using static telegram_captcha_bot.Logging.Logging;

namespace telegram_captcha_bot
{
    namespace Tools
    {
        public static partial class DB
        {
            public static User GetUserByID(long Input)
            {
                foreach (User Source in Vars.CurrentSettings.Users)
                {
                    if (Source.UID == Input)
                    {
                        Log("Query complete (HIT)", "DB");
                        return Source;
                    }
                }
                Log("Query complete (MISS)", "DB");
                return null;
            }
        }
    }
}