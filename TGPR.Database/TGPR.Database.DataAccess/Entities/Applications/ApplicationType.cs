using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGPR.Database.DataAccess.Entities.Applications
{
    [Table("ApplicationType")]
    public class ApplicationType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ApplicationTypeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
