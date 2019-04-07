using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TGPR.Database.DataAccess.Infrastructure;

namespace TGPR.Database.DataAccess.Entities.Applications
{
    [Table("ApplicationQuestion")]
    public class ApplicationQuestion : IApplicationSortableEntity
    {
        public int ApplicationQuestionId { get; set; }
        public int ApplicationQuestionTypeId { get; set; }
        public int ApplicationCategoryId { get; set; }
        public int? ParentApplicationOptionId { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int ApplicationSortOrder { get; set; }

        [Required]
        public int ReviewerSortOrder { get; set; }

        [Required]
        public bool Deleted { get; set; }

        [ForeignKey("ApplicationQuestionTypeId")]
        public virtual ApplicationQuestionType ApplicationQuestionType { get; set; }

        [ForeignKey("ApplicationCategoryId")]
        public virtual ApplicationCategory ApplicationCategory { get; set; }

        [ForeignKey("ParentApplicationOptionId")]
        public virtual ApplicationOption ParentApplicationOption { get; set; }
        
        public virtual ICollection<ApplicationOption> Options { get; set; }

        public virtual ICollection<ApplicationQuestionAnswer> Answers { get; set; }
    }
}
