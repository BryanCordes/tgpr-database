using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TGPR.Database.Common.Enums;
using TGPR.Database.Common.Enums.Applications;
using TGPR.Database.Common.Models.Applications;
using TGPR.Database.Components.Infrastructure;
using TGPR.Database.DataAccess.Operational;

namespace TGPR.Database.Components.Applications.QuestionTypeOptions
{
    internal class MultipleSelectionQuestionTypeOptions : IQuestionTypeOptions
    {
        private readonly IRepositoryFactory _repoFactory;
        private readonly IMapper _mapper;

        public ApplicationQuestionTypeEnum QuestionType => ApplicationQuestionTypeEnum.MultipleSelection;

        public MultipleSelectionQuestionTypeOptions(IRepositoryFactory repoFactory, IMapper mapper)
        {
            _repoFactory = repoFactory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationOptionModel>> GetOptionsAsync(int questionId)
        {
            //var optionA = new ApplicationOption
            //{
            //    ApplicationQuestionId = questionId,
            //    ApplicationOptionStatusId = (int)ApplicationOptionStatusEnum.None,
            //    Text = "Option A",
            //    ApplicationSortOrder = 1,
            //    ReviewerSortOrder = 1
            //};

            //var optionB = new ApplicationOption
            //{
            //    ApplicationQuestionId = questionId,
            //    ApplicationOptionStatusId = (int)ApplicationOptionStatusEnum.None,
            //    Text = "Option B",
            //    ApplicationSortOrder = 2,
            //    ReviewerSortOrder = 2
            //};

            //var optionC = new ApplicationOption
            //{
            //    ApplicationQuestionId = questionId,
            //    ApplicationOptionStatusId = (int)ApplicationOptionStatusEnum.None,
            //    Text = "Option C",
            //    ApplicationSortOrder = 3,
            //    ReviewerSortOrder = 3
            //};

            //using (var repo = _repoFactory.Create<IApplicationOptionRepository>())
            //{
            //    await repo.AddAsync(optionA);
            //    await repo.AddAsync(optionB);
            //    await repo.AddAsync(optionC);
            //}

            //IEnumerable<ApplicationOptionModel> models = new[] {optionA, optionB, optionC}
            //    .Select(_mapper.Map<ApplicationOptionModel>);

            //return models;

            return await Task.Run(() => Enumerable.Empty<ApplicationOptionModel>());
        }
    }
}
