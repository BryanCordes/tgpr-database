using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGPR.Database.DataAccess.Entities.Users
{
    public class UserRefreshToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserRefreshTokenId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string RefreshToken { get; set; }

        [Required]
        public string SecurityToken { get; set; }

        [Required]
        public string Client { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; set; }
    }
}
