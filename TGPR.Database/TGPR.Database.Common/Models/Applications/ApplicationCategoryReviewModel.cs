namespace TGPR.Database.Common.Models.Applications
{
    public class ApplicationCategoryReviewModel
    {
        public int ApplicationCategoryReviewId { get; set; }
        public int ApplicationCategoryId { get; set; }

        public ApplicationCategoryModel ApplicationCategory { get; set; }
    }
}
