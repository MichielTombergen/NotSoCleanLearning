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
    
    public partial class QuestionResultFeedback
    {
        public int ID { get; set; }
        public int QuestionResultID { get; set; }
        public int TeacherID { get; set; }
        public string Text { get; set; }
    
        public virtual QuestionResult QuestionResult { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
