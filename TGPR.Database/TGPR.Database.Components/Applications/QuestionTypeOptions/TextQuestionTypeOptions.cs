using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TGPR.Database.Common.Enums.Applications;
using TGPR.Database.Common.Models.Applications;
using TGPR.Database.Components.Infrastructure;
using TGPR.Database.DataAccess.Entities.Applications;
using TGPR.Database.DataAccess.Operational;
using TGPR.Database.DataAccess.Repositories.Applications;

namespace TGPR.Database.Components.Applications.QuestionTypeOptions
{
    internal class TextQuestionTypeOptions : IQuestionTypeOptions
    {
        private readonly IRepositoryFactory _repoFactory;
        private readonly IMapper _mapper;

        public ApplicationQuestionTypeEnum QuestionType => ApplicationQuestionTypeEnum.Text;

        public TextQuestionTypeOptions(IRepositoryFactory repoFactory, IMapper mapper)
        {
            _repoFactory = repoFactory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationOptionModel>> GetOptionsAsync(int questionId)
        {
            var option = new ApplicationOption
            {
                ApplicationQuestionId = questionId,
                ApplicationOptionStatusId = (int) ApplicationOptionStatusEnum.None,
                Text = string.Empty,
                ApplicationSortOrder = 1,
                ReviewerSortOrder = 1
            };

            using (var repo = _repoFactory.Create<IApplicationOptionRepository>())
            {
                await repo.AddAsync(option);
            }

            var model = _mapper.Map<ApplicationOptionModel>(option);

            return new[] { model };
        }
    }
}
