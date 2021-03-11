using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashTechCMS.Models.Reports
{
    public class SalesModel
    {

        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string SalesAssociate { get; set; }
        public string RecurringType { get; set; }
        public string Service { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public System.DateTime Date { get; set; }
        public string PaymentStatus { get; set; }
        public string CandidateStatus { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> MarketingStartDate { get; set; }
        public string VisaStatus { get; set; }
        public string Technology { get; set; }

    }
    public class SalesModelLogic
    {
        dashrdku_database db;
        public List<SalesModel> getNewClients(DateTime stDate, DateTime endDate)
        {
            DateTime today = DateTime.Now.Date;
            db = new dashrdku_database();
            List<CandidateMaster> candidates = db.CandidateMasters.Where(c => c.Date >= stDate.Date && c.Date <= endDate.Date).ToList();
            List<SalesModel> newCandidates = new List<SalesModel>();
            foreach (CandidateMaster candidate in candidates)
            {
                newCandidates.Add(new SalesModel()
                {
                    CandidateId = candidate.CandidateId,
                    CandidateName = candidate.CandidateName,
                    CandidateStatus = candidate.CandidateStatus,
                    Date = candidate.Date,
                    EmailId = candidate.EmailId,
                    MarketingStartDate = candidate.MarketingStartDate,
                    MobileNumber = candidate.MobileNumber,
                    PaidAmount = candidate.PaidAmount,
                    PaymentStatus = candidate.PaymentStatus,
                    RecurringType = candidate.RecurringType.RecurringTitle,
                    SalesAssociate = candidate.UserAccountDetail.FullName,
                    Service = candidate.SalesServiceMaster.ServiceName,
                    Remarks = candidate.Remarks,
                    Technology = candidate.TechnologyMaster.TechTitle,
                    TotalAmount = candidate.TotalAmount,
                    VisaStatus = candidate.VisaStatus
                });
            }
            return newCandidates;
        }
    }
}