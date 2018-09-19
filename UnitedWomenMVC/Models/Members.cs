using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using UnitedWomenMVC.NavOData;

namespace UnitedWomenMVC.Models
{
    public class Members
    {
        public int ID { get; set; }
        public static string No { get; set; }
        public static string Name { get; set; }
        public static string PersonalNumber { get; set; }
        public static string Accountcategory { get; set; }
        public static string Idnumber { get; set; }
        public static string MobileNo { get; set; }
        public static string BankAccount { get; set; }
        public static string Email { get; set; }
        public static string Password { get; set; }
        public static string Share { get; set; }
        public static Decimal Share_d { get; set; }
        public static string currentshare { get; set; }
        public static Decimal currentshare_d { get; set; }
        public static string currentsavings { get; set; }
        public static string Benevolent { get; set; }
        public static string Dividend { get; set; }
        public static string Oustandandingbal { get; set; }
        public static Decimal Oustandandingbal_d { get; set; }
        public static string FOSAbal { get; set; }
        public static string interest { get; set; }
        public static string FosaACNO { get; set; }
        public static string TotalRepayments { get; set; }
        public static string GroupACNO { get; set; }
        public int NoOfLoansGuaranteed { get; set; }
        public static string currentshares12 { get; set; }
        public static string currentshares13 { get; set; }
        public static string currentshares15 { get; set; }
        public static string currentshares14 { get; set; }
        public static string currentshares16 { get; set; }
        public static string currentshares17 { get; set; }
        public static Decimal totaldeposits12 { get; set; }
        public static Decimal totaldeposits13 { get; set; }
        public static Decimal totaldeposits14 { get; set; }
        public static Decimal totaldeposits15 { get; set; }
        public static Decimal totaldeposits16 { get; set; }
        public static string totaldeposits17 { get; set; }
        public static string Monthlycontribution { get; set; }
        public static string fosabbal { get; set; }
        public static string CurrrentLoan { get; set; }
        public static string totalloansoutstanding { get; set; }
        public static string FreeShares { get; set; }

        public  string LoanNo { get; set; }
        public  string LoanProductType { get; set; }
        public  decimal RequestedAmount { get; set; }
        public  decimal ApprovedAmount { get; set; }
        public  string LoanStatus { get; set; }
        public  string RepaymentMethod { get; set; }
        public  decimal OutstandingBalance { get; set; }


        public NAV nav = new NAV(new Uri(ConfigurationManager.AppSettings["ODATA_URI"]))
        {
            Credentials =
              new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                  ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"])
        };

        public Members(string memberNumber)
        {
            string year12 = Convert.ToString(2012).ToString();
            string year13 = Convert.ToString(2013).ToString();
            string year14 = Convert.ToString(2014).ToString();
            string year15 = Convert.ToString(2015).ToString();
            string year16 = Convert.ToString(2016).ToString();
            string year17 = Convert.ToString(2017).ToString();

            var objMembers = nav.MemberList.Where(r => r.No == memberNumber);
            foreach (var objMember in objMembers)
            {
                No = objMember.No;
                Name = objMember.Name;
                PersonalNumber = objMember.Personal_No;
                Accountcategory = objMember.Account_Category;
                Idnumber = objMember.ID_No;
                MobileNo = objMember.Phone_No;
                BankAccount = objMember.Bank_Account_No;
                CurrrentLoan = Convert.ToDecimal(objMember.Current_Loan).ToString("N");
                Email = objMember.E_Mail;
                Password = objMember.Password;

                FreeShares = Convert.ToDecimal(objMember.Free_Shares).ToString("N2");
                Share = Convert.ToDecimal(objMember.Shares_Retained).ToString("N");
                if (Share.StartsWith("-"))
                {
                    Share = (-1 * Convert.ToDecimal(objMember.Shares_Retained)).ToString("N2");
                    Share_d = (-1 * Convert.ToDecimal(objMember.Shares_Retained));
                }
                currentshare = (-1 * Convert.ToDecimal(objMember.Current_Shares)).ToString("N");
                if (currentshare.StartsWith("-"))
                {
                    currentshare = (-1 * Convert.ToDecimal(objMember.Current_Shares)).ToString("N");
                }
                currentshare_d = Convert.ToDecimal(objMember.Current_Shares);
                currentsavings = Convert.ToDecimal(objMember.Current_Savings).ToString("N2");
                if (currentsavings.StartsWith("-"))
                {
                    currentsavings = (-1 * Convert.ToDecimal(objMember.Current_Savings)).ToString("N2");
                }
                // Monthlycontribution = Convert.ToDecimal(objMember.Monthly_Contribution).ToString("N");
                FOSAbal = (-1 * Convert.ToDecimal(objMember.FOSA_Account_Bal)).ToString("N");

                Oustandandingbal = Convert.ToDecimal(objMember.Outstanding_Balance).ToString("N");
                Oustandandingbal_d = Convert.ToDecimal(objMember.Outstanding_Balance);
                interest = Convert.ToDecimal(objMember.Outstanding_Interest).ToString("n");
                if (interest.StartsWith("-"))
                {
                    interest = (-1 * Convert.ToDecimal(objMember.Outstanding_Interest)).ToString("n");
                }
                totalloansoutstanding = Convert.ToDecimal(objMember.Total_Loans_Outstanding).ToString("N");
                if (totalloansoutstanding.StartsWith("-"))
                {
                    totalloansoutstanding = (-1 * Convert.ToDecimal(objMember.Total_Loans_Outstanding)).ToString("N");
                }


                //   currentshares17 = (WSConfig.ObjNav.FnCurrent_shares(user, year17)).ToString();


                //totaldeposits17 = WSConfig.ObjNav.FnTotalDeposits(user, year17).ToString("N2");

         
                var objmyloans =
                    nav.LoansReg.Where(
                        r => r.Client_Code == HttpContext.Current.User.Identity.Name && r.Outstanding_Balance > 0).ToList();
                foreach (var objl in objmyloans)
                {
                    LoanNo = objl.Loan_No;
                    LoanProductType = objl.Loan_Product_Type;
                    RequestedAmount = Convert.ToDecimal(objl.Requested_Amount);
                    ApprovedAmount = Convert.ToDecimal(objl.Approved_Amount);
                    LoanStatus = objl.Loan_Status;
                    RepaymentMethod = objl.Repayment_Method;
                    OutstandingBalance = Convert.ToDecimal(objl.Outstanding_Balance);
                }
            }

        }
        
    }
}