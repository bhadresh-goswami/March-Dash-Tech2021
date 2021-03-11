using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashTechCMS.Models.ExpertCV
{
    public class ExpertCVModel
    {
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string SalesAssociateName { get; set; }
        public string RecurringTitle { get; set; }
        public string ServiceName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public System.DateTime Date { get; set; }
        public string PaymentStatus { get; set; }
        public string CandidateStatus { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> MarketingStartDate { get; set; }
        public string VisaStatus { get; set; }
        public string TechnologyName { get; set; }

    }
    public class ExpertCVModelLogic
    {
        public List<ExpertCVModel> getData()
        {
            dashrdku_database db = new dashrdku_database();

            var data = db.CandidateMasters.ToList();

            List<ExpertCVModel> expertCVModels = new List<ExpertCVModel>();

            foreach (CandidateMaster candidate in data)
            {
                ExpertCVModel model = new ExpertCVModel();
                model.CandidateId = candidate.CandidateId;
                model.CandidateName = candidate.CandidateName;
                model.CandidateStatus = candidate.CandidateStatus;
                model.Date = candidate.Date;
                model.EmailId = candidate.EmailId;
                model.MarketingStartDate = candidate.MarketingStartDate;
                model.MobileNumber = candidate.MobileNumber;
                model.PaidAmount = candidate.PaidAmount;
                model.PaymentStatus = candidate.PaymentStatus;
                model.RecurringTitle = db.RecurringTypes.Find(candidate.RefRecurringTypeId).RecurringTitle;
                model.SalesAssociateName = db.UserAccountDetails.Find(candidate.RefSalesAssociate).RocketName;
                model.ServiceName = db.SalesServiceMasters.Find(candidate.RefServiceId).ServiceName;
                model.Remarks = candidate.Remarks;
                model.TechnologyName = db.TechnologyMasters.Find(candidate.TechnologyId).TechTitle;
                model.TotalAmount = candidate.TotalAmount;
                model.VisaStatus = candidate.VisaStatus;

                expertCVModels.Add(model);
            }
            return expertCVModels;
        }
    }
}