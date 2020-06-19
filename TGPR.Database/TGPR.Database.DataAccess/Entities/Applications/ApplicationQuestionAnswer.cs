using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGPR.Database.DataAccess.Entities.Applications
{
    [Table("ApplicationQuestionAnswer")]
    public class ApplicationQuestionAnswer
    {
        [Key]
        public int ApplicationQuestionAnswerId { get; set; }
        public int ApplicationId { get; set; }
        public int ApplicationQuestionId { get; set; }
        public int? ApplicationOptionId { get; set; }
        
        public string Text { get; set; }

        [ForeignKey("ApplicationId")]
        public virtual Application Application { get; set; }

        [ForeignKey("ApplicationQuestionId")]
        public virtual ApplicationQuestion ApplicationQuestion { get; set; }

        [ForeignKey("ApplicationOptionId")]
        public virtual ApplicationOption ApplicationOption { get; set; }
    }
}
