//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DashTechCMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SubmissionMaster
    {
        public int SubmissionId { get; set; }
        public string SubmissionBy { get; set; }
        public System.DateTime SubmissionDate { get; set; }
        public string CandidateName { get; set; }
        public string JobTitle { get; set; }
        public decimal Rate { get; set; }
        public string ClientName { get; set; }
        public string VendorName { get; set; }
        public string VendorCompanyName { get; set; }
        public string VendorContactNumber { get; set; }
        public string JobPortal { get; set; }
        public string JobDescription { get; set; }
        public Nullable<System.DateTime> InterviewDate { get; set; }
        public string SupportAssignedTo { get; set; }
        public string InterviewStatus { get; set; }
        public string InterviewFeedback { get; set; }
        public Nullable<int> CandidateId { get; set; }
        public System.TimeSpan InterviewTime { get; set; }
    }
}
