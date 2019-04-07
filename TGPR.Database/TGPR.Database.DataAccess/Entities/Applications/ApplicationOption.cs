using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TGPR.Database.DataAccess.Infrastructure;

namespace TGPR.Database.DataAccess.Entities.Applications
{
    [Table("ApplicationOption")]
    public class ApplicationOption : IApplicationSortableEntity
    {
        public int ApplicationOptionId { get; set; }
        public int ApplicationQuestionId { get; set; }
        public int ApplicationOptionStatusId { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int ApplicationSortOrder { get; set; }

        [Required]
        public int ReviewerSortOrder { get; set; }

        [Required]
        public bool Deleted { get; set; }

        [ForeignKey("ApplicationQuestionId")]
        public virtual ApplicationQuestion ApplicationQuestion { get; set; }

        [ForeignKey("ApplicationOptionStatusId")]
        public virtual ApplicationOptionStatus ApplicationOptionStatus { get; set; }

        [InverseProperty("ParentApplicationOption")]
        public virtual ICollection<ApplicationQuestion> ChildQuestions { get; set; }
    }
}
