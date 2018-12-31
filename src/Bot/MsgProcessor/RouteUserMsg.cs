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
            public static async Task RouteUserMsg(object Sender, UpdateEventArgs e)
            {
                // copy update so easier to call
                Update Upd = e.Update;
                User Usr = e.Update.Message.From;
                Log("Processing incoming update (UserRoute), ID: "+ Upd.Id + ", user: "+ Usr.FirstName + " " + Usr.LastName + ", UID: "+ Usr.Id, "BOT");
                if (Upd.Message.Chat.Type != ChatType.Private) { return; }
                // check if user exists
                Settings.User DBUser = DB.GetUserByID(Upd.Message.From.Id);
                if (DBUser == null)
                {
                    // the user does not exist!
                    // create user
                    Log("Adding unfamiliar user to database...", "BOT");
                    DBUser = new Settings.User(Usr.Id);
                    Vars.CurrentSettings.Users.Add(DBUser);
                    Log("Done, currently we have " + Vars.CurrentSettings.Users.Count + " users on record.");
                    // continue
                }
                if (DBUser.Verified)
                {
                    // user has already passed verification
                    Log("User already passed verification.", "BOT");
                    await Vars.BotInstance.SendTextMessageAsync(Usr.Id, "💤 Your account is already verified!", ParseMode.Markdown);
                    return;
                }
                // user hasn't passed verification yet
                await Vars.BotInstance.SendTextMessageAsync(Usr.Id, "🔒 *Verification*\n\nWelcome!\n\nTo verify that you're not a robot, visit the page below.\n\n"
                                                               + Vars.CurrentSettings.ServerURL + "landing?g=" + DBUser.GUID.ToString(),
                                                            ParseMode.Markdown);
            }
        }
    }
}