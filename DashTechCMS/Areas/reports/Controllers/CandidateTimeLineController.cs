using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DashTechCMS.Models;
using DashTechCMS.Models.Messages;
using DashTechCMS.Models.Reports;
using DashTechCMS.Models.SQLObject;

namespace DashTechCMS.Areas.reports.Controllers
{
    public class CandidateTimeLineController : Controller
    {
        dashrdku_database db = new dashrdku_database();
        // GET: reports/CandidateTimeLine
        public ActionResult Index(DateTime? stDate, DateTime? endDate)
        {
            try
            {

                DateTime sdt = DateTime.Now.Date;
                DateTime edt = DateTime.Now.Date;

                if (stDate != null && endDate != null)
                {
                    sdt = (DateTime)stDate;
                    edt = (DateTime)endDate;
                }

                CandidateTimeLineModelLogic timeLineLogic = new CandidateTimeLineModelLogic();
                ViewBag.sdt = sdt;
                ViewBag.edt = edt;
                return View(timeLineLogic.GetList(sdt, edt));
            }
            catch (Exception ex)
            {
                TempData["alert"] = new AlertBoxModel() { Type = "Error", Message = ex.Message };
                return View(new List<CandidateTimeLineModel>());
            }
        }
    }
}