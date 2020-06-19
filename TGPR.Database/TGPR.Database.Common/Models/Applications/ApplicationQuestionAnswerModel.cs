namespace TGPR.Database.Common.Models.Applications
{
    public class ApplicationQuestionAnswerModel
    {
        public int ApplicationQuestionAnswerId { get; set; }
        public int ApplicationId { get; set; }
        public int ApplicationQuestionId { get; set; }
        public int? ApplicationOptionId { get; set; }

        public string Text { get; set; }

        public ApplicationModel Application { get; set; }

        public ApplicationQuestionModel ApplicationQuestion { get; set; }

        public ApplicationOptionModel ApplicationOption { get; set; }
    }
}
