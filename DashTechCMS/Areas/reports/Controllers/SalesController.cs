using DashTechCMS.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DashTechCMS.Areas.reports.Controllers
{
    [FilterConfig._AuthenticationFilter]

    public class SalesController : Controller
    {
        // GET: reports/Sales
        public ActionResult NewClients(DateTime? stDate, DateTime? endDate)
        {
            if (stDate == null && endDate == null)
            {
                stDate = DateTime.Now.Date;
                endDate = DateTime.Now.Date;
            }

            DateTime sdt = (DateTime)stDate;
            DateTime edt = (DateTime)endDate;

            SalesModelLogic salesModelLogic = new SalesModelLogic();
            List<SalesModel> newClient = salesModelLogic.getNewClients(sdt, edt);
            ViewBag.sdt = sdt;
            ViewBag.edt = edt;

            return View(newClient);
        }
        public ActionResult RefundedMonthly()
        {
            SalesLogic logic = new SalesLogic();

            List<Sales_Monthly_Location_Associates> Sales_Monthly_Location_Associates = new List<Sales_Monthly_Location_Associates>();
            Sales_Monthly_Location_Associates = logic.getDetails_Refund_Monthly();


            return View(Sales_Monthly_Location_Associates);
        }
        public ActionResult SalesMonthly()
        {
            SalesLogic logic = new SalesLogic();

            List<Sales_Monthly> salesMonthly = new List<Sales_Monthly>();
            salesMonthly = logic.getDetails_Sales_Monthly();


            return View(salesMonthly);
        }
        public ActionResult SalesMonthlyLocation()
        {
            SalesLogic logic = new SalesLogic();

            List<Sales_Monthly_Locaiton> salesMonthlyLocation = new List<Sales_Monthly_Locaiton>();
            salesMonthlyLocation = logic.getDetails_Sales_Monthly_Locaiton();

            return View(salesMonthlyLocation);
        }
        public ActionResult SalesMonthlyLocaitonAssociate()
        {
            SalesLogic logic = new SalesLogic();

            List<Sales_Monthly_Location_Associates> salesMonthlyLocationAssociates = new List<Sales_Monthly_Location_Associates>();
            salesMonthlyLocationAssociates = logic.getDetails_Sales_Monthly_Location_Associates();
            return View(salesMonthlyLocationAssociates);
        }
    }
}