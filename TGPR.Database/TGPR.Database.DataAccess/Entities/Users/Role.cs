using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TGPR.Database.DataAccess.Infrastructure;

namespace TGPR.Database.DataAccess.Entities.Users
{
    [Table("Role")]
    public class Role : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RoleId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<RoleSecurityActivity> SecurityActivities { get; set; }
    }
}
