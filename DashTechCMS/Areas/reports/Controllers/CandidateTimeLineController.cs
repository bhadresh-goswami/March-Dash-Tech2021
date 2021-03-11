using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DashTechCMS.Models;
using DashTechCMS.Models.Messages;
using DashTechCMS.Models.Reports;
using DashTechCMS.Models.SQLObject;
using DashTechCMS.Models.User;

namespace DashTechCMS.Areas.reports.Controllers
{
    [FilterConfig._AuthenticationFilter]
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

        public ActionResult GetTimeLine()
        {
            CandidateDetailBoardLogic detailBoardLogic = new CandidateDetailBoardLogic();
            return View(detailBoardLogic.GetDetails());
        }
        public ActionResult GetTimeLine2()
        {
            CandidateDetailBoardLogic detailBoardLogic = new CandidateDetailBoardLogic();
            return View(detailBoardLogic.GetDetails());
        }
        public ActionResult ChangeStatus(string status, int cid, string message)
        {
            try
            {
                dashrdku_database db = new dashrdku_database();
                FollowUpMaster model = new FollowUpMaster();
                UserInfoModel user = Session["userInfo"] as UserInfoModel;
                model.RefCandidateId = cid;
                model.FollowUpBy = user.UserRocketName;
                model.FollowUpDate = DateTime.Now.Date;
                model.FollowUpTime = DateTime.Now.TimeOfDay;
                model.FollowUpMessage = message;
                model.FollowUpStatus = status;
                db.FollowUpMasters.Add(model);
                db.SaveChanges();
                TempData["alert"] = new AlertBoxModel() { Type = "Success", Message = "Follow Up Message Saved!" };

            }
            catch (Exception ex)
            {
                TempData["alert"] = new AlertBoxModel() { Type = "Error", Message = ex.Message };
            }
            return RedirectToAction("GetTimeLine2");
        }

        public ActionResult TimeLine()
        {
            CandidateDetailBoardLogic detailBoardLogic = new CandidateDetailBoardLogic();
            return View(detailBoardLogic.GetDetails());
        }

        public ActionResult JsonTimeLine()
        {
            return View();
        }

        public JsonResult TimelineJsonData()
        {
            CandidateDetailBoardLogic detailBoardLogic = new CandidateDetailBoardLogic();
            
            return Json(detailBoardLogic.GetDetails().Select(a => new
            {
                CandidateId = a.CandidateId,LocationName=a.LocationName,
                CandidateName = a.CandidateName,
                OnBoardingDate = a.OnBoardingDate.ToString("MM-dd-yyyy"),
                SalesAssocaites = a.SalesAssocaites,
                ExpertCVStatus = string.Format("Start={0} to End={1}<br>RUC={2}<br>Total Days={3}", a.ResumeProcessStarted == null ? "N/A" : a.ResumeProcessStarted.ToString(), a.ResumeProcessEnded == null ? "N/A" : a.ResumeProcessEnded.ToString(), a.ResumeUnderstandingDate == null ? "N/A": a.ResumeUnderstandingDate.ToString(), a.TotalResumeDays),
                CurrentStatus = a.CurrentStatus,
                MarketingStatus = string.Format("Start={0} to End={1}<br>Total Days={2}", a.MarketingStartDate == null ? "N/A" : a.MarketingStartDate.ToString(), a.PODate == null ? "N/A" : a.PODate.ToString(), a.TotalMarketingDays),
                TrainingStatus = string.Format("Start={0} to End={1}<br>Total Days={2}", a.TrainingStartDate == null ? "N/A" : a.TrainingStartDate.ToString(), a.TrainingEndDate == null ? "N/A" : a.TrainingEndDate.ToString(), a.TotalTrainingDays),
                PODate = a.PODate != null ? a.PODate.ToString() : "N/A",
                TechnicalStatus = "https://localhost:44372/Content/images/details.png",
                OnboardingStatus = "https://localhost:44372/Content/images/details.png",
                AccountStatus = "https://localhost:44372/Content/images/details.png",
                DisputeStatus = "https://localhost:44372/Content/images/details.png"
            }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSalesPerson()
        {
            return Json(db.UserAccountDetails.Where(a => a.RoleMaster.RoleTitle.Contains("Sales")).Select(b => b.FullName).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult TimeLineReport()
        {

            return View();
        }
    }
}