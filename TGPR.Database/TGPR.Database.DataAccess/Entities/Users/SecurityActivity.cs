using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGPR.Database.DataAccess.Entities.Users
{
    [Table("SecurityActivity")]
    public class SecurityActivity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SecurityActivityId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
