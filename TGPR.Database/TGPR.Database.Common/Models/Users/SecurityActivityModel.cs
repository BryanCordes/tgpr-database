using System.ComponentModel.DataAnnotations;

namespace TGPR.Database.Common.Models.Users
{
    public class SecurityActivityModel
    {
        [Required]
        public int SecurityActivityId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
