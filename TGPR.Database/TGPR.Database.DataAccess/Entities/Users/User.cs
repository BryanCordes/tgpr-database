using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TGPR.Database.DataAccess.Infrastructure;

namespace TGPR.Database.DataAccess.Entities.Users
{
    [Table("User")]
    public class User : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; set; }

        public DateTime? LastLoginOn { get; set; }

        public DateTime? InactiveOn { get; set; }

        public ICollection<UserRole> Roles { get; set; }
    }
}
