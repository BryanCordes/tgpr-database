using System.Collections.Generic;

namespace TGPR.Database.Common.Models.Applications
{
    public class ApplicationModel
    {
        public int ApplicationId { get; set; }
        public int ApplicationTemplateId { get; set; }

        public ApplicationTemplateModel ApplicationTemplate { get; set; }

        public IEnumerable<ApplicationCategoryModel> Categories { get; set; }
    }
}
