using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UnitedWomenMVC.NavOData;

namespace UnitedWomenMVC.Controllers
{
    public class LoansController : Controller
    {
        public NAV nav = new NAV(new Uri(ConfigurationManager.AppSettings["ODATA_URI"]))
        {
            Credentials =
                new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                    ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"])
        };


        // GET: Loans
        public ActionResult Index()
        {
            var list = nav.LoansReg.Where(r => r.Client_Code == System.Web.HttpContext.Current.User.Identity.Name).ToList();
            return View(list);
        }

        public ActionResult Edit(string id)
        {
            return View();
        }

        public ActionResult DropdownLoans()
        {
            var list = nav.LoansReg.Where(r => r.Client_Code == System.Web.HttpContext.Current.User.Identity.Name).ToList();
            return View(list);
        }

        public ActionResult Loanstatment()
        {
            return View("Reports");
        }
        public ActionResult Reports(int id)
        {
            switch (id)
            {
                case 1:
                    var filenameMs = System.Web.HttpContext.Current.User.Identity.Name.Replace(@"/", @"");
                    try
                    {
                        WebConfig.ObjNav.FnMemberStatement(System.Web.HttpContext.Current.User.Identity.Name, String.Format("MEMBER STATEMENT_{0}.pdf", filenameMs));
                        @ViewBag.src = "/Downloads/" + String.Format("MEMBER STATEMENT_{0}.pdf", filenameMs);
                    }
                    catch (Exception exception)
                    {
                        exception.Data.Clear();
                    }
                    

                    break;

                case 2:
                    var filenameDepo = System.Web.HttpContext.Current.User.Identity.Name.Replace(@"/", @"");
                    try
                    {
                        WebConfig.ObjNav.FnDepositsStatement(System.Web.HttpContext.Current.User.Identity.Name, String.Format("DEPOSITS STATEMENT_{0}.pdf", filenameDepo));
                       
                        @ViewBag.src = "/Downloads/" + String.Format("DEPOSITS STATEMENT_{0}.pdf", filenameDepo);
                    }
                    catch (Exception exception)
                    {
                        exception.Data.Clear();
                    }

                    break;
                case 3:
                    Loanstatment();
                    break;

            }

            return View();
        }

        public ActionResult GetMyLoanStatement(string loanNo)
        {
            var filenameLs = System.Web.HttpContext.Current.User.Identity.Name.Replace(@"/", @"");
            try
            {
                WebConfig.ObjNav.FnGetLoanStatement(loanNo, String.Format("LOAN STATEMENT_{0}.pdf", filenameLs), System.Web.HttpContext.Current.User.Identity.Name);
                @ViewBag.src = "/Downloads/" + String.Format("LOAN STATEMENT_{0}.pdf", filenameLs);
            }
            catch (Exception exception)
            {
                exception.Data.Clear();
            }

            return View("Reports");
        }
      
    }
}