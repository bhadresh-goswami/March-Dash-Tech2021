using DashTechCMS.Models.SQLObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DashTechCMS.Models;
using DashTechCMS.Models.Admin;

namespace DashTechCMS.Areas.admin.Controllers
{
    [FilterConfig._AuthenticationFilter]
    public class dashboardController : Controller
    {
        // GET: admin/dashboard
        public ActionResult Index()
        {
            TempData["UserRole"] = "Admin";
            InfoBoxModelLogic infoBoxModelLogic = new InfoBoxModelLogic();
            infoBoxModelLogic.GetDetails();

            TempData["currentRevenue"] = infoBoxModelLogic.infoBox.TotalRevenue;
            TempData["todayRevenue"] = infoBoxModelLogic.infoBox.TodaysRevenue;
            TempData["monthRevenue"] = infoBoxModelLogic.infoBox.CurrentMonthRevenue;
            TempData["totalCandidate"] = infoBoxModelLogic.infoBox.TotalCandidate;
            TempData["CurrentMonthPlacement"] = infoBoxModelLogic.infoBox.CurrentMonthPlacement;
            TempData["TodaysPlacements"] = infoBoxModelLogic.infoBox.TodaysPlacements;

            var monthObj = (new MonthsOrderSalesModelLogic());
            monthObj.GetDetails();

            ViewBag.monthlyData = monthObj.list;
            //TempData["totalCandidate"] = infoBoxModelLogic.infoBox.TotalCandidate;

            return View();
        }
    }
}