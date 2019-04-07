using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TGPR.Database.DataAccess.Entities.Users;

namespace TGPR.Database.DataAccess.Entities.Applications
{
    [Table("ApplicationCategoryReviewText")]
    public class ApplicationCategoryReviewText
    {
        [Key]
        public int ApplicationCategoryReviewTextId { get; set; }
        public int ApplicationId { get; set; }
        public int ApplicationCategoryReviewId { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public Guid ReviewedById { get; set; }

        public int ApplicationCategoryReviewStatusId { get; set; }

        public DateTime ReviewedOn { get; set; } = DateTime.UtcNow;

        [ForeignKey("ApplicationId")]
        public virtual Application Application { get; set; }

        [ForeignKey("ApplicationCategoryReviewId")]
        public virtual ApplicationCategoryReview ApplicationCategoryReview { get; set; }

        [ForeignKey("ReviewedById")]
        public virtual User ApplicationUser { get; set; }
    }
}
