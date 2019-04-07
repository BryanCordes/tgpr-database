using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TGPR.Database.Common.Models.Users
{
    public class RoleModel
    {
        public Guid? RoleId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<RoleSecurityActivityModel> SecurityActivities { get; set; }
    }
}
