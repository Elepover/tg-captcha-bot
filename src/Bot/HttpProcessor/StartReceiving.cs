using System;
using System.Net;
using System.Threading;

namespace telegram_captcha_bot
{
    namespace Bot
    {
        public static partial class HttpProcessor
        {
            public static void StartReceiving()
            {
                Vars.HttpWorker = new Thread(async () => await Tools.Threads.ThrHttpProcessor());
                Vars.HttpWorker.Start();
            }
        }
    }
}