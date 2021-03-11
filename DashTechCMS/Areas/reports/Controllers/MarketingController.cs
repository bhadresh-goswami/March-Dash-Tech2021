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
    public class MarketingController : Controller
    {
        // GET: reports/Marketing
        public ActionResult Index(DateTime? stDate, DateTime? endDate)
        {
            List<CandidateMarketingModel> list = new List<CandidateMarketingModel>();
            DateTime sdt = DateTime.Now.Date, edt = DateTime.Now.Date;

            try
            {
                if (stDate != null && endDate != null)
                {
                    sdt = (DateTime)stDate;
                    edt = (DateTime)endDate;
                }
                CandidateMarketingLogic marketingLogic = new CandidateMarketingLogic();
                list = marketingLogic.getData(sdt, edt);
                TempData["alert"] = new AlertBoxModel() { Type = "Success", Message = string.Format("Total {0} Found!", list.Count) };
            }
            catch (Exception ex)
            {
                TempData["alert"] = new AlertBoxModel() { Type = "Error", Message = ex.Message };
            }
            ViewBag.sdt = sdt;
            ViewBag.edt = edt;
            return View(list);
        }

        public ActionResult DetailsReport(DateTime? stDate, DateTime? endDate)
        {
            List<DailyReportModel> dailyReports = new List<DailyReportModel>();
            try
            {
                DailyReportLogic reportLogic = new DailyReportLogic();
                return View(reportLogic.getData());
            }
            catch (Exception ex)
            {

            }
            return View(dailyReports);
        }
    }
}