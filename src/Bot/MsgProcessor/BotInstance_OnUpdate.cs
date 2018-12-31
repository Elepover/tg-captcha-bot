using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using telegram_captcha_bot.Logging;
using telegram_captcha_bot.Tools;
using static telegram_captcha_bot.Logging.Logging;

namespace telegram_captcha_bot
{
    namespace Bot
    {
        public partial class MsgProcessor
        {
            /// <summary>
            /// Main processor of incoming updates.
            /// </summary>
            /// <param name="Sender">Update sender.</param>
            /// <param name="e">Update event args.</param>
            /// <returns></returns>
            public static async void BotInstance_OnUpdate(object Sender, UpdateEventArgs e)
            {
                try
                {
                    if (e.Update.Message.NewChatMembers != null)
                    {
                        await MsgProcessor.RouteSystemMsg(e);
                        return;
                    }
                    if (e.Update.Message.Type == MessageType.Text && e.Update.Message.Chat.Type == ChatType.Private)
                    {
                        await MsgProcessor.RouteUserMsg(Sender, e);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Log("Error processing incoming update: " + ex.ToString(), "BOT", LogLevel.ERROR);
                }
            }
        }
    }
}