using System;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using static telegram_captcha_bot.Logging.Logging;

namespace telegram_captcha_bot
{
    namespace Settings
    {
        public static partial class Manage
        {
            /// <summary>
            /// Save settings to local disk.
            /// </summary>
            /// <param name="Source">A Settings object to save.</param>
            /// <returns></returns>
            public static async Task<bool> SaveAsync(Settings Source)
            {
                // do not handle errors, let the calling method do it.
                Log("Saving settings...", "CONF");
                string Text = JsonConvert.SerializeObject(Source, Formatting.Indented);
                await File.WriteAllTextAsync(Vars.SettingsFile, Text);
                return true;
            }
        }
    }
}