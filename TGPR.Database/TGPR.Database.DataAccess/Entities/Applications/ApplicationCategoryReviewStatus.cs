using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGPR.Database.DataAccess.Entities.Applications
{
    [Table("ApplicationCategoryReviewStatus")]
    public class ApplicationCategoryReviewStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ApplicationCategoryReviewStatusId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
