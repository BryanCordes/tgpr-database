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
    internal class SingleSelectionQuestionTypeOption : IQuestionTypeOptions
    {
        private readonly IRepositoryFactory _repoFactory;
        private readonly IMapper _mapper;

        public ApplicationQuestionTypeEnum QuestionType => ApplicationQuestionTypeEnum.SingleSelection;

        public SingleSelectionQuestionTypeOption(IRepositoryFactory repoFactory, IMapper mapper)
        {
            _repoFactory = repoFactory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationOptionModel>> GetOptionsAsync(int questionId)
        {
            var optionYes = new ApplicationOption
            {
                ApplicationQuestionId = questionId,
                ApplicationOptionStatusId = (int)ApplicationOptionStatusEnum.None,
                Text = "Yes",
                ApplicationSortOrder = 1,
                ReviewerSortOrder = 1
            };

            var optionNo = new ApplicationOption
            {
                ApplicationQuestionId = questionId,
                ApplicationOptionStatusId = (int)ApplicationOptionStatusEnum.None,
                Text = "No",
                ApplicationSortOrder = 2,
                ReviewerSortOrder = 2
            };

            using (var repo = _repoFactory.Create<IApplicationOptionRepository>())
            {
                await repo.AddAsync(optionYes);
                await repo.AddAsync(optionNo);
            }

            var modelYes = _mapper.Map<ApplicationOptionModel>(optionYes);
            var modelNo = _mapper.Map<ApplicationOptionModel>(optionNo);

            return new[] { modelYes, modelNo };
        }
    }
}
