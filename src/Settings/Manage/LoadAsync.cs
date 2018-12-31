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
            /// <summary>
            /// Load settings from filesystem.
            /// </summary>
            /// <param name="Apply">To apply settings, choorse true (default)</param>
            /// <returns></returns>
            public static async Task<bool> LoadAsync(bool Apply = true)
            {
                // do not handle errors, let the calling method do it.
                Log("Loading settings...", "CONF");
                string SettingsText = await File.ReadAllTextAsync(Vars.SettingsFile);
                Settings Temp = JsonConvert.DeserializeObject<Settings>(SettingsText);
                if (Apply) {
                    Log("Applying settings...", "CONF");
                    Vars.CurrentSettings = Temp;
                }
                return true;
            }
        }
    }
}