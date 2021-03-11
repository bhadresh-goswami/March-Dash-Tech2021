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
    
    public partial class OnBoardingMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OnBoardingMaster()
        {
            this.OnBoardingPayments = new HashSet<OnBoardingPayment>();
        }
    
        public int OBId { get; set; }
        public System.DateTime OBDate { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public int InstallmentDuration { get; set; }
        public bool IsDeleted { get; set; }
        public string InsertedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int RefCandidatePOId { get; set; }
        public decimal TotalAmount { get; set; }
        public Nullable<decimal> UpFrontAmount { get; set; }
        public string Remarks { get; set; }
    
        public virtual PODetail PODetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OnBoardingPayment> OnBoardingPayments { get; set; }
    }
}