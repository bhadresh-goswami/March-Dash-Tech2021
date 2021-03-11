using DashTechCMS.Models;
using DashTechCMS.Models.ExpertCV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DashTechCMS.Models.Email;

namespace DashTechCMS.Areas.expertcvcoach.Controllers
{
    public class dashboardController : Controller
    {
        dashrdku_database db = new dashrdku_database();
        // GET: expertcvcoach/dashboard
        public ActionResult Index()
        {
            ExpertCVModelLogic expertCVModelLogic = new ExpertCVModelLogic();
            return View(expertCVModelLogic.getData());
        }
        public ActionResult ChangeStatus(int id, string status)
        {
            var data = db.CandidateMasters.Find(id);
            data.CandidateStatus = status;
            db.SaveChanges();

            string toEmail = data.UserAccountDetail.EmailId + ",himanshu@expertcvcoach.com";
            string ccEmail = data.UserAccountDetail.RefLocationId == 1 ? "abhishek@dashtechinc.com,darshan@dashtechinc.com,prabhuk @dashtechinc.com,devansh@dashtechinc.com,bhadresh@dashtechinc.com,jatin@dashtechinc.com" : "akash.s@dashtechinc.com,maaz@dashtechinc.com,drashya@dashtechinc.com,bhadresh@dashtechinc.com,kush@dashtechinc.com,abhishek.b@dashtechinc.com,jatin@dashtechinc.com";

            string body = string.Format(@"Hi Team, <br>Current Candidate Status is,<br><br>
<table border='1'>
<tr>
    <td>CandidateName</td><td>{0}</td>
</tr>
<tr>
    <td>Registration Date</td><td>{1}</td>
</tr>
<tr>
    <td>Total Paid</td><td>$ {2}</td>
</tr>
<tr>
    <td>Total Days</td><td>{3}</td>
</tr>
<tr>
    <td>Status</td><td>{4}</td>
</tr>
</table>
", data.CandidateName, data.Date.ToString("MM/dd/yyyy"), data.PaidAmount, DateTime.Now.Date.Subtract(data.Date), status);

            SMTPEmailSendingModel.Send(toEmail, body, "(" + data.CandidateName + ") Changed:" + status, ccEmail, "bhadresh@dashtechinc.com");

            var timeline = new CandidateTimeLine();
            timeline.ChangedBy = (Session["userInfo"] as DashTechCMS.Models.User.UserInfoModel).UserId;
            timeline.Date = DateTime.Now.Date;
            timeline.RefCandidateId = id;
            timeline.Status = status;
            db.CandidateTimeLines.Add(timeline);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}