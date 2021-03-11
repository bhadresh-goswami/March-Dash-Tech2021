using DashTechCMS.Models.Messages;
using DashTechCMS.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DashTechCMS.Areas.reports.Controllers
{
    [FilterConfig._AuthenticationFilter]
    public class PlacementsController : Controller
    {
        // GET: reports/Placements
        public ActionResult Index(DateTime? stDate, DateTime? endDate)
        {
            List<PlacementInfoModel> placementInfos = new List<PlacementInfoModel>();
            DateTime sdt = DateTime.Now.Date;
            DateTime edt = DateTime.Now.Date;

            if (stDate != null && endDate != null)
            {
                sdt = (DateTime)stDate;
                edt = (DateTime)endDate;
            }
            ViewBag.sdt = sdt;
            ViewBag.edt = edt;
            try
            {
                placementInfos = new PlacemenInfoLogic().GetDetails(sdt, edt);
                return View(placementInfos);
            }
            catch (Exception ex)
            {
                TempData["alert"] = new AlertBoxModel() { Type = "Error", Message = ex.Message };
                return View(placementInfos);
            }
        }
    }
}