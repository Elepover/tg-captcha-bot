using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using static telegram_captcha_bot.Logging.Logging;

namespace telegram_captcha_bot
{
    namespace Settings
    {
        public class Settings
        {
            public Settings()
            {
                APIKey = "";
                AdminId = -1;
                reCAPTCHASecret = "";
                reCAPTCHAPublic = "";
                ListenURL = "http://verify.example.com:8080/";
                ServerURL = "https://verify.example.com/";
                VerifyTimeWindow = 60;
                KickUnverified = false;
                Users = new List<User>();
            }
            public string APIKey { get; set; }
            public long AdminId { get; set; }
            public string reCAPTCHASecret { get; set; }
            public string reCAPTCHAPublic { get; set; }
            public string ListenURL { get; set; }
            public string ServerURL { get; set; }
            public int VerifyTimeWindow { get; set; }
            public bool KickUnverified { get; set; }
            public List<User> Users { get; set; }
        }
    }
}