using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGPR.Database.DataAccess.Entities.Applications
{
    [Table("ApplicationCategoryReview")]
    public class ApplicationCategoryReview
    {
        [Key]
        public int ApplicationCategoryReviewId { get; set; }
        public int ApplicationCategoryId { get; set; }

        [ForeignKey("ApplicationCategoryId")]
        public virtual ApplicationCategory ApplicationCategory { get; set; }
    }
}
