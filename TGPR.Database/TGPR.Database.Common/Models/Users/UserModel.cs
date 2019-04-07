using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TGPR.Database.Common.Models.Users
{
    public class UserModel
    {
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
        
        public DateTime CreatedOn { get; set; }

        public DateTime? LastLoginOn { get; set; }

        public DateTime? InactiveOn { get; set; }

        public ICollection<UserRoleModel> Roles { get; set; }
    }
}
