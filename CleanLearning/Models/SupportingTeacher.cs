//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CleanLearning.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SupportingTeacher
    {
        public int ID { get; set; }
        public int ModuleID { get; set; }
        public int TeacherID { get; set; }
    
        public virtual Module Module { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
