using EASendMail;
using Microsoft.AspNetCore.Mvc;
using SenEmail.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace SenEmail.Controllers
{
    public class HomeController : Controller
    {
        //لینک راهنما
        //https://www.emailarchitect.net/easendmail/kb/csharp.aspx?cat=2

        private readonly ILogger<HomeController> _logger;

        private readonly string _email= "belquranteam@gmail.com";
        private readonly string _Password = "zxtxggnxugnadroj";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test1()
        {
            //تست یک روش عادی و مرسوم

            string body = "<head>" +
                            "Here comes some logo" +
                            "</head>" +
                            "<body>" +
                            "<h1>Account confirmation reqest.</h1>" +
                            "</body>";

            try
            {
                using (var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com", 465))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential()
                    {
                        UserName = _email,
                        Password = _Password,// "ihezxteeszmpbcub",
                    };
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.EnableSsl = true;

                    //Oops: from/recipients switched around here...
                    //smtpClient.Send("targetemail@targetdomain.xyz", "myemail@gmail.com", "Account verification", body);
                    smtpClient.Send(_email, "info@belquran.com", "Account verification", body);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }

            return View();
        }


        public IActionResult Test2()
        {
            //استفاده از کتاب خانه کمکی EASendMail
            //https://www.emailarchitect.net/easendmail/kb/csharp.aspx?cat=2
            try
            {
                string body = "<head>" +
                                "ایمیل تست" +
                                "</head>" +
                                "<body>" +
                                "<h1>این ایمیل تست است.</h1>" +
                                "</body>";

                SmtpMail oMail = new SmtpMail("TryIt");

                // Your gmail email address
                oMail.From = _email;
                // Set recipient email address
                oMail.To = "info@belquran.com";

                // Set email subject
                oMail.Subject = "test email from gmail account";
                // Set email body
                //oMail.TextBody = "this is a test email sent from c# project with gmail.";
                oMail.HtmlBody = body;
                // Gmail SMTP server address
                SmtpServer oServer = new SmtpServer("smtp.gmail.com");

                // Gmail user authentication
                // For example: your email is "gmailid@gmail.com", then the user should be the same
                oServer.User = _email;

                // Create app password in Google account
                // https://support.google.com/accounts/answer/185833?hl=en
                oServer.Password = _Password;

                // Set 465 port
                oServer.Port = 465;

                // detect SSL/TLS automatically
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;


                EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
                oSmtp.SendMail(oServer, oMail);

            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}