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
    
    public partial class RecurringMaster
    {
        public int RecurringId { get; set; }
        public System.DateTime DueDate { get; set; }
        public Nullable<System.DateTime> PaidDate { get; set; }
        public decimal Amount { get; set; }
        public int RefCandidateId { get; set; }
        public string ReceivedIn { get; set; }
        public Nullable<bool> SendReminderEmail { get; set; }
        public string PaymentStatus { get; set; }
        public string remarks { get; set; }
        public string InstallmentNumber { get; set; }
    
        public virtual CandidateMaster CandidateMaster { get; set; }
    }
}