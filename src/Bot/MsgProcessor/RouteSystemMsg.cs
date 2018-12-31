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
            public static async Task RouteSystemMsg(UpdateEventArgs e)
            {
                // copy update so easier to call
                Update Upd = e.Update;
                User Self = await Vars.BotInstance.GetMeAsync();
                foreach (User Usr in e.Update.Message.NewChatMembers)
                {
                    Log("Processing incoming update (SysRoute), ID: "+ Upd.Id + ", user: "+ Usr.FirstName + " " + Usr.LastName + ", UID: "+ Usr.Id, "BOT");
                    if (Usr == Self)
                    {
                        goto SkipFor; // skip bot itself
                    }
                    // check user
                    Settings.User DBUser = DB.GetUserByID(Upd.Message.From.Id);
                    if (DBUser == null || DBUser.Verified != true)
                    {
                        // the user is not verified
                        // perform action
                        Log("User not verified, performing action...", "BOT");
                        if (Vars.CurrentSettings.KickUnverified)
                        {
                            await Vars.BotInstance.KickChatMemberAsync(Upd.Message.Chat.Id, Usr.Id, DateTime.UtcNow.AddSeconds(60));
                            Log("Kicked user for 1 minute.", "BOT");
                        }
                        else
                        {
                            await Vars.BotInstance.RestrictChatMemberAsync(Upd.Message.Chat.Id, Usr.Id, DateTime.UtcNow, false, false, false, false);
                            Settings.Message Msg = new Settings.Message();
                            Msg.ChatID = Upd.Message.Chat.Id;
                            Msg.MessageID = Upd.Message.MessageId;
                            DBUser.Messages.Add(Msg);
                            Log("Limited user permanently.", "BOT");
                        }
                    }
                    SkipFor:;
                }
            }
        }
    }
}