using System;
using System.Collections.Generic;

namespace TGPR.Database.Common.Models.Users
{
    public class UserSummaryModel
    {
        public Guid UserId { get; set; }
        
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? LastLoginOn { get; set; }

        public DateTime? InactiveOn { get; set; }

        public ICollection<UserRoleModel> Roles { get; set; }
    }
}
