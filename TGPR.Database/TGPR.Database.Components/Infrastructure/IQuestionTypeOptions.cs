using System.Collections.Generic;
using System.Threading.Tasks;
using TGPR.Database.Common.Enums.Applications;
using TGPR.Database.Common.Models.Applications;

namespace TGPR.Database.Components.Infrastructure
{
    public interface IQuestionTypeOptions
    {
        ApplicationQuestionTypeEnum QuestionType { get; }
        Task<IEnumerable<ApplicationOptionModel>> GetOptionsAsync(int questionId);
    }
}
