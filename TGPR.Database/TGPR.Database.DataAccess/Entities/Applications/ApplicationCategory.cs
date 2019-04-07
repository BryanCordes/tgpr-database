using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TGPR.Database.DataAccess.Infrastructure;

namespace TGPR.Database.DataAccess.Entities.Applications
{
    [Table("ApplicationCategory")]
    public class ApplicationCategory : IApplicationSortableEntity
    {
        [Key]
        public int ApplicationCategoryId { get; set; }
        public int ApplicationTemplateId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int ApplicationSortOrder { get; set; }

        [Required]
        public int ReviewerSortOrder { get; set; }

        [Required]
        public bool Deleted { get; set; }

        [Required]
        public bool HasReview { get; set; }

        [ForeignKey("ApplicationTemplateId")]
        public virtual ApplicationTemplate ApplicationTemplate { get; set; }

        public virtual ICollection<ApplicationQuestion> Questions { get; set; }
    }
}
