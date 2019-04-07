using System;
using System.ComponentModel.DataAnnotations;

namespace TGPR.Database.Common.Models.Users
{
    public class UserRoleModel
    {
        public Guid UserRoleId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid RoleId { get; set; }
        
        public UserModel User { get; set; }

        public RoleModel Role { get; set; }
    }
}
