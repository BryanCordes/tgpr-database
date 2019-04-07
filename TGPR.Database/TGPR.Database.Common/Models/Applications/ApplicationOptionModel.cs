using System.Collections.Generic;

namespace TGPR.Database.Common.Models.Applications
{
    public class ApplicationOptionModel
    {
        public int ApplicationOptionId { get; set; }
        public int ApplicationQuestionId { get; set; }
        public int ApplicationOptionStatusId { get; set; }
        
        public string Text { get; set; }

        public int ApplicationSortOrder { get; set; }
        public int ReviewerSortOrder { get; set; }

        public bool Deleted { get; set; }

        public virtual ApplicationQuestionModel ApplicationQuestion { get; set; }
        
        public virtual ApplicationOptionStatusModel ApplicationOptionStatus { get; set; }

        public virtual ICollection<ApplicationQuestionModel> ChildQuestions { get; set; }
    }
}
