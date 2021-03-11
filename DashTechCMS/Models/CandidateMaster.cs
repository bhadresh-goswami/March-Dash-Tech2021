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
    
    public partial class CandidateMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CandidateMaster()
        {
            this.CandidateTechnicalExpertDetails = new HashSet<CandidateTechnicalExpertDetail>();
            this.MarketingRevenueDetails = new HashSet<MarketingRevenueDetail>();
            this.RequestMasters = new HashSet<RequestMaster>();
            this.CandidateMarketingDetails = new HashSet<CandidateMarketingDetail>();
            this.CandidateTimeLines = new HashSet<CandidateTimeLine>();
            this.CommentDetails = new HashSet<CommentDetail>();
            this.FollowUpMasters = new HashSet<FollowUpMaster>();
            this.RecurringMasters = new HashSet<RecurringMaster>();
        }
    
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public Nullable<int> RefSalesAssociate { get; set; }
        public int RefRecurringTypeId { get; set; }
        public int RefServiceId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public System.DateTime Date { get; set; }
        public string PaymentStatus { get; set; }
        public string CandidateStatus { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> MarketingStartDate { get; set; }
        public string VisaStatus { get; set; }
        public int TechnologyId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateTechnicalExpertDetail> CandidateTechnicalExpertDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MarketingRevenueDetail> MarketingRevenueDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestMaster> RequestMasters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateMarketingDetail> CandidateMarketingDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateTimeLine> CandidateTimeLines { get; set; }
        public virtual RecurringType RecurringType { get; set; }
        public virtual UserAccountDetail UserAccountDetail { get; set; }
        public virtual SalesServiceMaster SalesServiceMaster { get; set; }
        public virtual TechnologyMaster TechnologyMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommentDetail> CommentDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FollowUpMaster> FollowUpMasters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecurringMaster> RecurringMasters { get; set; }
    }
}
