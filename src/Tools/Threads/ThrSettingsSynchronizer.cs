using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using telegram_captcha_bot.Logging;
using telegram_captcha_bot.Settings;
using static telegram_captcha_bot.Logging.Logging;

namespace telegram_captcha_bot
{
    namespace Tools
    {
        public static partial class Threads
        {
            public static async Task ThrSettingsSynchronizer()
            {
                Log("Started!", "CONFSYNC");
                while (true)
                {
                    try
                    {
                        Log("Saving settings...", "CONFSYNC");
                        await Manage.SaveAsync(Vars.CurrentSettings);
                    }
                    catch (Exception ex)
                    {
                        Log("Exception synchronizing settings: "+ ex.ToString(), "CONFSYNC", LogLevel.ERROR);
                    }
                    Thread.Sleep(60000);
                }
            }
        }
    }
}