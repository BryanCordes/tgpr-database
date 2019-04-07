using System.Collections.Generic;

namespace TGPR.Database.Common.Models.Applications
{
    public class ApplicationCategoryModel
    {
        public int ApplicationCategoryId { get; set; }
        public int ApplicationTemplateId { get; set; }
        public string Name { get; set; }
        public int ApplicationSortOrder { get; set; }
        public int ReviewerSortOrder { get; set; }
        public bool Deleted { get; set; }
        public bool HasReview { get; set; }

        public IEnumerable<ApplicationQuestionModel> Questions { get; set; }
    }
}
