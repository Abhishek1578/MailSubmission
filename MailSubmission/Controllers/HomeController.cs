using MailSubmission.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MailSubmission.Controllers
{
    public class HomeController : Controller
    {
    private readonly UserDbContext db = new UserDbContext();
        [HttpGet]
        public ActionResult Index()
        {
            var user = db.Users.ToList();
            if(user == null || user.Count==0)
            {
                return HttpNotFound("User not Found");
            }
            return View(user);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Users user)
        {
            if (ModelState.IsValid)
            {

                db.Users.Add(user); 
                db.SaveChanges();
                try
                {
                    SendMail(user.FirstName, user.LastName);
                    ViewBag.Message = "Email Send Successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = $"Error Sending email: {ex.Message}";
                }
            }
            return View(user);
        }
        private void SendMail(string FirstName, string LastName)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            string to = "abhiishekchauhan.2002@gmail.com";
            string from = "abhishekchauhan1578@gmail.com";
            string subject = "User Submission";
            string body = $"First Name:{FirstName}\nLast Name:{LastName}";

            string Appassword = "cupl vptu itwg ncnr";
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port=587,
                UseDefaultCredentials = false,
                Credentials=new NetworkCredential(from,Appassword ),
                EnableSsl=true,
            };

            MailMessage mailMessage = new MailMessage(from, to, subject, body);
            smtpClient.Send(mailMessage);
            Console.WriteLine("Email send Successfully");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}