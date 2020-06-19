using System.Collections.Generic;

namespace TGPR.Database.Common.Models.Applications
{
    public class ApplicationQuestionModel
    {
        public int ApplicationQuestionId { get; set; }
        public int ApplicationCategoryId { get; set; }
        public int ApplicationQuestionTypeId { get; set; }
        public int? ParentApplicationOptionId { get; set; }
        
        public string Text { get; set; }

        public int ApplicationSortOrder { get; set; }
        public int ReviewerSortOrder { get; set; }

        public int Width { get; set; }

        public bool Deleted { get; set; }

        public virtual ApplicationQuestionTypeModel ApplicationQuestionType { get; set; }

        public virtual ICollection<ApplicationOptionModel> Options { get; set; }
    }
}
