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
    
    public partial class UserAccountDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserAccountDetail()
        {
            this.CandidateTechnicalExpertDetails = new HashSet<CandidateTechnicalExpertDetail>();
            this.MarketingRevenueDetails = new HashSet<MarketingRevenueDetail>();
            this.PODetails = new HashSet<PODetail>();
            this.CandidateMasters = new HashSet<CandidateMaster>();
            this.CandidateTimeLines = new HashSet<CandidateTimeLine>();
            this.LeadMasters = new HashSet<LeadMaster>();
            this.TeamDetails = new HashSet<TeamDetail>();
            this.TeamDetails1 = new HashSet<TeamDetail>();
            this.TeamDetails2 = new HashSet<TeamDetail>();
            this.RequestMasters = new HashSet<RequestMaster>();
            this.RequestMasters1 = new HashSet<RequestMaster>();
        }
    
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string RocketName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public Nullable<int> RefLocationId { get; set; }
        public Nullable<int> RefRoleId { get; set; }
        public string UserImageUrl { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public string CompanyName { get; set; }
        public Nullable<System.DateTime> JoiningDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateTechnicalExpertDetail> CandidateTechnicalExpertDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MarketingRevenueDetail> MarketingRevenueDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PODetail> PODetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateMaster> CandidateMasters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CandidateTimeLine> CandidateTimeLines { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeadMaster> LeadMasters { get; set; }
        public virtual LocationMaster LocationMaster { get; set; }
        public virtual RoleMaster RoleMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeamDetail> TeamDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeamDetail> TeamDetails1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeamDetail> TeamDetails2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestMaster> RequestMasters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestMaster> RequestMasters1 { get; set; }
    }
}
