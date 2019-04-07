using System;
using System.ComponentModel.DataAnnotations;

namespace TGPR.Database.Common.Models.Users
{
    public class RoleSecurityActivityModel
    {
        public Guid? RoleSecurityActivityId { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        [Required]
        public int SecurityActivityId { get; set; }
        
        public RoleModel Role { get; set; }
        
        public SecurityActivityModel SecurityActivity { get; set; }
    }
}
