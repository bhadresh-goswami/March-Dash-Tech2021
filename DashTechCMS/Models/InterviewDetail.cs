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
    
    public partial class InterviewDetail
    {
        public int InteviewId { get; set; }
        public Nullable<int> RefSumissionId { get; set; }
        public System.DateTime DateOfInterview { get; set; }
        public System.TimeSpan TimeOfInterview { get; set; }
        public int RefModeOfInterview { get; set; }
        public string RoundOfInterv { get; set; }
        public Nullable<bool> InterviewSupport { get; set; }
        public string InterviewSupportName { get; set; }
        public string Feedback { get; set; }
        public string JobDescription { get; set; }
        public string Status { get; set; }
        public string InterviewDetails { get; set; }
        public string InterviewJoiningDetails { get; set; }
        public string InterviewSupportFeedback { get; set; }
        public Nullable<int> PreviousInterview { get; set; }
    
        public virtual SubmissionDetail SubmissionDetail { get; set; }
    }
}
