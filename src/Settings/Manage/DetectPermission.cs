using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using telegram_captcha_bot.Settings;
using telegram_captcha_bot.Tools;
using static telegram_captcha_bot.Logging.Logging;

namespace telegram_captcha_bot
{
    namespace Settings
    {
        public static partial class Manage
        {
            /// <summary>
            /// Detect application's access to settings file.
            /// </summary>
            /// <returns><br />true: File is writable<br />false: File is readonly.</returns>
            public static bool DetectPermission()
            {
                // do not handle errors, let the calling method do it.
                Log("Detecting permissions...", "CONF");
                return Methods.FlipBool((new FileInfo(Vars.SettingsFile)).IsReadOnly);
            }
        }
    }
}