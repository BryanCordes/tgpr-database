using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TGPR.Database.DataAccess.Entities.Users;

namespace TGPR.Database.DataAccess.Entities.Applications
{
    [Table("ApplicationTemplate")]
    public class ApplicationTemplate
    {
        [Key]
        public int ApplicationTemplateId { get; set; }
        public int ApplicationTypeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public bool Deleted { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; set; }
        public Guid CreatedById { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedById { get; set; }

        [ForeignKey("ApplicationTypeId")]
        public virtual ApplicationType Type { get; set; }

        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }

        [ForeignKey("UpdatedById")]
        public virtual User UpdatedBy { get; set; }

        public virtual ICollection<ApplicationCategory> Categories { get; set; }
    }
}
