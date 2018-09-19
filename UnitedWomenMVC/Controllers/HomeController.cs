using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using UnitedWomenMVC.NavOData;
using Members = UnitedWomenMVC.Models.Members;

namespace UnitedWomenMVC.Controllers
{
    public class HomeController : Controller
    {
        public readonly string StrSqlConn = @"Server=" + ConfigurationManager.AppSettings["DB_INSTANCE"] + ";Database=" +
                                            ConfigurationManager.AppSettings["DB_NAME"] + "; User ID=" +
                                            ConfigurationManager.AppSettings["DB_USER"] + "; Password=" +
                                            ConfigurationManager.AppSettings["DB_PWD"] +
                                            "; MultipleActiveResultSets=true";

        public string CompanyName = "United Women Sacco Ltd";

        public NAV Nav = new NAV(new Uri(ConfigurationManager.AppSettings["ODATA_URI"]))
        {
            Credentials =
                new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                    ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"])
        };

        [AllowAnonymous]
        public ActionResult Defaults()
        {
            return View();
        }

        [Authorize]
        public ActionResult Dashboard()
        {
           return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Defaults(United_Women_Sacco_Ltd_Members_Register user)
        {
            var usersEntities = new UWSEntities();
            var userId = usersEntities.ValidateUser(user.No_, user.Password).FirstOrDefault();

            string message;
            switch (userId)
            {
                case "invalid":
                    message = "Username and/or password is incorrect.";
                    break;
                case "deactivated":
                    message = "Account has not been activated.";
                    break;

                default:
                    FormsAuthentication.SetAuthCookie(user.No_, true);
                    ReturnMember(user.No_.Trim());
                    return RedirectToAction("Dashboard");
            }

            ViewBag.Message = message;
            return View(user);
        }

        protected Members ReturnMember(string memberNo)
        {
            return new Members(memberNo);
        }

        [ValidateAntiForgeryToken]
        protected void UserLoggedIn()
        {
            if (!User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
        }
        
        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Defaults");
        }
    }
}