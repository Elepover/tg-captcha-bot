using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using telegram_captcha_bot.Logging;
using static telegram_captcha_bot.Logging.Logging;

namespace telegram_captcha_bot
{
    namespace Tools
    {
        public static partial class Methods
        {
            public static async Task<bool> VerifyCaptchaAsync(string Secret, string Response)
            {
                try
                {
                    Log("Verifying reCAPTCHA...", "CAPTCHA");
                    using (HttpClient Client = new HttpClient()) {
                        Dictionary<string, string> Values = new Dictionary<string, string>
                        {
                            { "secret", Secret },
                            { "response", Response }
                        };
                        FormUrlEncodedContent Content = new FormUrlEncodedContent(Values);
                        HttpResponseMessage ServerResponse = await Client.PostAsync("https://www.google.com/recaptcha/api/siteverify", Content);
                        string ResponseString = await ServerResponse.Content.ReadAsStringAsync();
                        JObject JObj = JObject.Parse(ResponseString);
                        bool Success = Methods.StrToBool(JObj["success"].ToString());
                        Log("Verification complete, result: " + Success, "CAPTCHA");
                        return Success;
                    }
                }
                catch (Exception ex)
                {
                    Log("Error verifying reCAPTCHA: " + ex.ToString(), "CAPTCHA");
                    return false;
                }
            }
        }
    }
}