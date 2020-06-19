using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TGPR.Database.Common.Enums.Applications;
using TGPR.Database.DataAccess.Entities.Users;

namespace TGPR.Database.DataAccess.Entities.Applications
{
    [Table("Application")]
    public class Application
    {
        [Key]
        public int ApplicationId { get; set; }

        [Required]
        public int ApplicationTemplateId { get; set; }

        [Required]
        public int ApplicationStatusId { get; set; } = (int)ApplicationStatusEnum.New;

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(150)]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(300)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string Address2 { get; set; }

        [Required]
        [MaxLength(300)]
        public string City { get; set; }

        [Required]
        [MaxLength(2)]
        public string State { get; set; }

        [Required]
        [MaxLength(10)]
        public string Zip { get; set; }

        [Required]
        [MaxLength(15)]
        public string Phone { get; set; }

        [Required]
        public bool IsTest { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedById { get; set; }

        [ForeignKey("ApplicationTemplateId")]
        public virtual ApplicationTemplate ApplicationTemplate { get; set; }

        [ForeignKey("ApplicationStatusId")]
        public virtual ApplicationStatus ApplicationStatus { get; set; }

        [ForeignKey("UpdatedById")]
        public virtual User UpdatedBy { get; set; }

        public virtual ICollection<ApplicationQuestionAnswer> QuestionAnswers { get; set; }

        public virtual ICollection<ApplicationCategoryReviewText> ApplicationCategoryReviews { get; set; }
    }
}
