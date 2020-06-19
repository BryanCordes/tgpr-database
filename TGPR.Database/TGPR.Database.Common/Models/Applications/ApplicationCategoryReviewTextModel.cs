using System;
using TGPR.Database.Common.Models.Users;

namespace TGPR.Database.Common.Models.Applications
{
    public class ApplicationCategoryReviewTextModel
    {
        public int ApplicationCategoryReviewTextId { get; set; }
        public int ApplicationId { get; set; }
        public int ApplicationCategoryReviewId { get; set; }
        public string Text { get; set; }
        public Guid ReviewedById { get; set; }
        public int ApplicationCategoryReviewStatusId { get; set; }
        public DateTime ReviewedOn { get; set; } = DateTime.UtcNow;

        public ApplicationModel Application { get; set; }

        public ApplicationCategoryReviewModel ApplicationCategoryReview { get; set; }

        public UserModel ApplicationUser { get; set; }
    }
}
