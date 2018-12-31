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
            public static async Task<bool> InitAsync()
            {
                // do not handle errors, let the calling method do it
                // do not log when calling sub-functions, let them do it
                Log("Init stage started.", "CONF");
                // step 0, load default to memory
                Vars.CurrentSettings = new Settings();
                // step 1, detect file existence
                if (!File.Exists(Vars.SettingsFile))
                {
                    await CreateAsync();
                    return true;
                }
                // file exists
                // step 2, detect permissions
                if (!DetectPermission())
                {
                    throw (new System.UnauthorizedAccessException("Application has no access to settings file."));
                }
                // step 3, detect integrity
                if (!await TestAsync())
                {
                    await CreateAsync();
                    return true;
                }
                // step 4, everything is ok, load to memory
                // will override what we did at L17
                await LoadAsync();
                return true;
            }
        }
    }
}