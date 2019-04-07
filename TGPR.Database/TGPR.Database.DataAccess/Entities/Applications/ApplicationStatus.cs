using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGPR.Database.DataAccess.Entities.Applications
{
    [Table("ApplicationStatus")]
    public class ApplicationStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ApplicationStatusId { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
