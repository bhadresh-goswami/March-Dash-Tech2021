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
    
    public partial class TaskStatusDetail
    {
        public int TSId { get; set; }
        public int RefTMId { get; set; }
        public string CurrentStatus { get; set; }
        public System.TimeSpan StatusTime { get; set; }
        public System.DateTime StatusDate { get; set; }
        public string StatusRemarks { get; set; }
    
        public virtual TaskManageMaster TaskManageMaster { get; set; }
    }
}