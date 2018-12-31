using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using Telegram.Bot;

namespace telegram_captcha_bot
{
    public class Vars
    {
        public readonly static Version AppVer = new Version("1.0.0.0");
        public readonly static string AppExecutable = Assembly.GetExecutingAssembly().Location;
        public readonly static string AppDirectory = (new FileInfo(AppExecutable)).DirectoryName;
        public readonly static string SettingsFile = Path.Combine(AppDirectory, "tg-captcha-bot.json");
        public readonly static string LandingPageHtml = "<!doctype html><html><head><meta charset=\"utf-8\"><title>Verification</title><script src=\"https://www.google.com/recaptcha/api.js\"></script></head><body><form action=\"validate\" method=\"get\" id=\"form\"><input id=\"g\" name=\"g\" value=\"\" style=\"display: none\"><div class=\"g-recaptcha\" data-sitekey=\"$1\" data-callback=\"dataCallback\"></div></form><script type=\"application/javascript\">function GetQueryString(name){     var reg = new RegExp(\"(^|&)\"+ name +\"=([^&]*)(&|$)\");var r = window.location.search.substr(1).match(reg);if(r!=null)return  unescape(r[2]); return null;}function dataCallback(){document.getElementById(\"form\").submit();}var g = GetQueryString(\"g\");if(g ==null || g.toString().length<1){window.location.href=\"about:blank\";}else{document.getElementById(\"g\").setAttribute(\"value\",g);;}</script></body></html>";
        public readonly static string VerifyPageHtml = "<!doctype html><html><head><meta charset=\"utf-8\"></head><body><script type=\"application/javascript\">window.location.href=\"about:blank\";</script></body></html>";

        public static Settings.Settings CurrentSettings;
        public static HttpListener HttpServer;
        public static TelegramBotClient BotInstance;

        public static Thread SettingsSynchronizer;
        public static Thread HttpWorker;
    }
}