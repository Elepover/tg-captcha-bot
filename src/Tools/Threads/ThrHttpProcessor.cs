using System;
using System.IO;
using System.Net;
using System.Web;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using telegram_captcha_bot.Logging;
using telegram_captcha_bot.Settings;
using static telegram_captcha_bot.Logging.Logging;

namespace telegram_captcha_bot
{
    namespace Tools
    {
        public static partial class Threads
        {
            public static async Task ThrHttpProcessor()
            {
                Log("Started!", "HTTP");
                try
                {
                    HttpListener Listener = new HttpListener();
                    Listener.Prefixes.Add(Vars.CurrentSettings.ListenURL);
                    Listener.Start();
                    while (true)
                    {
                        HttpListenerContext Context = await Listener.GetContextAsync();
                        HttpListenerRequest Request = Context.Request;
                        HttpListenerResponse Response = Context.Response;
                        Log("Request received from " + Request.RemoteEndPoint.Address.ToString() + ":" + Request.RemoteEndPoint.Port + ", url: " + Request.Url.ToString() + ", responding...", "HTTP");
                        try
                        {
                            switch (Request.Url.LocalPath)
                            {
                                case "/landing":
                                    string ResponseStr = Vars.LandingPageHtml
                                        .Replace("$1", Vars.CurrentSettings.reCAPTCHAPublic);
                                    byte[] BufferLanding = Encoding.UTF8.GetBytes(ResponseStr);
                                    Response.ContentLength64 = BufferLanding.Length;
                                    await Response.OutputStream.WriteAsync(BufferLanding, 0, BufferLanding.Length);
                                    Response.OutputStream.Close();
                                    // Response.Close(); not presented in documentation
                                    break;
                                case "/validate":
                                    Guid UserID = Guid.Parse(HttpUtility.ParseQueryString(Request.Url.Query).Get("g"));
                                    string Pass = HttpUtility.ParseQueryString(Request.Url.Query).Get("g-recaptcha-response");
                                    Log("Validating user: " + UserID.ToString(), "HTTP");
                                    User ResolvedUser = Tools.DB.GetUserByGUID(UserID);
                                    if (ResolvedUser == null)
                                    {
                                        throw (new ArgumentException("Failed identifying user."));
                                    }
                                    if (ResolvedUser.Verified)
                                    {
                                        throw (new ArgumentException("User already verified."));
                                    }
                                    if (await Tools.Methods.VerifyCaptchaAsync(Vars.CurrentSettings.reCAPTCHASecret, Pass))
                                    {
                                        Log("Verified user: " + UserID.ToString(), "HTTP");
                                        ResolvedUser.Verified = true;
                                    }
                                    else
                                    {
                                        throw (new ArgumentException("Invalid reCAPTCHA response."));
                                    }
                                    byte[] BufferValidate = Encoding.UTF8.GetBytes(Vars.VerifyPageHtml);
                                    Response.ContentLength64 = BufferValidate.Length;
                                    await Response.OutputStream.WriteAsync(BufferValidate, 0, BufferValidate.Length);
                                    Response.OutputStream.Close();
                                    // try to notify user
                                    await Vars.BotInstance.SendTextMessageAsync(ResolvedUser.UID, "🔓 *Verified!*\n\nSuccess! Your account is now verified!");
                                    break;
                                default:
                                    throw (new ArgumentException("Wrong request."));
                            }
                        }
                        catch (Exception ex)
                        {
                            Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            Response.StatusDescription = "Bad request";
                            byte[] Buffer = Encoding.UTF8.GetBytes("Bad request: " + ex.Message);
                            Response.ContentLength64 = Buffer.Length;
                            await Response.OutputStream.WriteAsync(Buffer, 0, Buffer.Length);
                            Response.OutputStream.Close();
                        }
                        
                    }
                }
                catch (HttpListenerException Hex)
                {
                    Log("Critical error in http server: " + Hex.ToString(), "HTTP", LogLevel.ERROR);
                    Tools.Methods.ErrorExit();
                }
                catch (Exception ex)
                {
                    Log("Exception in http processor: "+ ex.ToString(), "HTTP", LogLevel.ERROR);
                }
                Thread.Sleep(60000);
            }
        }
    }
}