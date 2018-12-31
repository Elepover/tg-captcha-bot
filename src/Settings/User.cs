using System;
using System.Collections.Generic;

namespace telegram_captcha_bot
{
    namespace Settings
    {
        public class User
        {
            public User()
            {
                UID = -1;
                GUID = Guid.NewGuid();
                Verified = false;
                StartedAt = DateTime.UtcNow;
                Messages = new List<Message>();
            }
            public User(long ID)
            {
                UID = ID;
                GUID = Guid.NewGuid();
                Verified = false;
                StartedAt = DateTime.UtcNow;
                Messages = new List<Message>();
            }
            public long UID { get; set; }
            public Guid GUID { get; set; }
            public bool Verified { get; set; }
            public DateTime StartedAt { get; set; }
            public List<Message> Messages { get; set; }
        }
    }
}