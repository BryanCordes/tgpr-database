using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TGPR.Database.Common.Enums.Applications;

namespace TGPR.Database.DataAccess.Entities.Applications
{
    [Table("Application")]
    public class Application
    {
        [Key]
        public int ApplicationId { get; set; }
        public int ApplicationTemplateId { get; set; }
        public int ApplicationStatusId { get; set; } = (int)ApplicationStatusEnum.New;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; set; }

        [ForeignKey("ApplicationTemplateId")]
        public virtual ApplicationTemplate ApplicationTemplate { get; set; }

        [ForeignKey("ApplicationStatusId")]
        public virtual ApplicationStatus ApplicationStatus { get; set; }

        public virtual ICollection<ApplicationQuestionAnswer> QuestionAnswers { get; set; }

        public virtual ICollection<ApplicationCategoryReviewText> ApplicationCategoryReviews { get; set; }
    }
}
