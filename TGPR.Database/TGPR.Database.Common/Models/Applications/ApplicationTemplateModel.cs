using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TGPR.Database.Common.Models.Applications
{
    public class ApplicationTemplateModel
    {
        public int ApplicationTemplateId { get; set; }

        [Required]
        public int ApplicationTypeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public bool Deleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public ApplicationTypeModel Type { get; set; }

        public ICollection<ApplicationCategoryModel> Categories { get; set; }
    }
}
