using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using telegram_captcha_bot.Settings;
using static telegram_captcha_bot.Logging.Logging;

namespace telegram_captcha_bot
{
    namespace Settings
    {
        public static partial class Manage
        {
            public static async Task<bool> TestAsync()
            {
                Log("Testing settings...", "CONF");
                try
                {
                    await LoadAsync(false);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}