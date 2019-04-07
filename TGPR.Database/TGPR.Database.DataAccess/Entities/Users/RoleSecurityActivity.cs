using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TGPR.Database.DataAccess.Infrastructure;

namespace TGPR.Database.DataAccess.Entities.Users
{
    [Table("RoleSecurityActivity")]
    public class RoleSecurityActivity : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RoleSecurityActivityId { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        [Required]
        public int SecurityActivityId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        [ForeignKey("SecurityActivityId")]
        public SecurityActivity SecurityActivity { get; set; }
    }
}
