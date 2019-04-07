using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGPR.Database.DataAccess.Entities.Applications
{
    [Table("ApplicationQuestionType")]
    public class ApplicationQuestionType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ApplicationQuestionTypeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
