using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGPR.Database.DataAccess.Entities.Applications
{
    [Table("ApplicationOptionStatus")]
    public class ApplicationOptionStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ApplicationOptionStatusId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
