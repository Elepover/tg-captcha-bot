using System;

namespace telegram_captcha_bot
{
    namespace Settings
    {
        public class Message
        {
            public Message()
            {
                MessageID = -1;
                ChatID = -1;
            }
            public long MessageID { get; set; }
            public long ChatID { get; set; }
        }
    }
}