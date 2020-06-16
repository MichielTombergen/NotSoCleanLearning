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
    
    public partial class QuestionResult
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuestionResult()
        {
            this.QuestionResultFeedback = new HashSet<QuestionResultFeedback>();
        }
    
        public int ID { get; set; }
        public int QuestionID { get; set; }
        public int StudentID { get; set; }
        public string AnswerText { get; set; }
        public int AnswerOption_ID { get; set; }
    
        public virtual Question Question { get; set; }
        public virtual Student Student { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionResultFeedback> QuestionResultFeedback { get; set; }
    }
}