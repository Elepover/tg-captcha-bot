using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using telegram_captcha_bot.Logging;
using static telegram_captcha_bot.Logging.Logging;

namespace telegram_captcha_bot
{
    public class Program
    {
        static void Main(string[] args)
        {
            Log("Starting main worker...", "MAIN-D", LogLevel.INFO);
            Task MainAsyncTask = MainAsync(args);
            MainAsyncTask.Wait();
            Log("Main worker accidentally exited. Stopping...", "MAIN-D", LogLevel.ERROR);
            Environment.Exit(1);
        }
        static async Task MainAsync(string[] args)
        {
            Log("Initializing... Bot version " + Vars.AppVer.ToString());
            try
            {
                Log("Initializing settings system...");
                await Settings.Manage.InitAsync();
                Log("Starting threads...");
                Vars.SettingsSynchronizer = new Thread(async () => await Tools.Threads.ThrSettingsSynchronizer());
                Vars.SettingsSynchronizer.Start();
                Bot.HttpProcessor.StartReceiving();
                Log("Initializing bot instance...");
                Vars.BotInstance = new Telegram.Bot.TelegramBotClient(Vars.CurrentSettings.APIKey);
                await Vars.BotInstance.TestApiAsync();
                Log("API test complete!");
                Log("Starting message receiving...");
                Vars.BotInstance.OnUpdate += Bot.MsgProcessor.BotInstance_OnUpdate;
                Vars.BotInstance.StartReceiving(new []{UpdateType.Message});
                Log("Startup complete!");
                while (true)
                {
                    Thread.Sleep(10000);
                }
            }
            catch (Exception ex)
            {
                Log("Error during startup: "+ ex.ToString(), "CORE", LogLevel.ERROR);
                Tools.Methods.ErrorExit();
            }
        }
    }
}
