using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TGPR.Database.Common.Models.Applications;
using TGPR.Database.DataAccess.Entities.Applications;
using TGPR.Database.DataAccess.Operational;
using TGPR.Database.DataAccess.Repositories.Applications;

namespace TGPR.Database.Components.Applications
{
    public interface IApplicationQuestionTypeComponent
    {
        Task<IEnumerable<ApplicationQuestionTypeModel>> GetApplicationQuestionTypesAsync();
    }

    internal class ApplicationQuestionTypeComponent : IApplicationQuestionTypeComponent
    {
        private readonly IRepositoryFactory _repoFactory;
        private readonly IMapper _mapper;

        public ApplicationQuestionTypeComponent(IRepositoryFactory repoFactory, IMapper mapper)
        {
            _repoFactory = repoFactory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationQuestionTypeModel>> GetApplicationQuestionTypesAsync()
        {
            using (var repo = _repoFactory.Create<IApplicationQuestionTypeRepository>())
            {
                IEnumerable<ApplicationQuestionType> questionTypes = await repo.GetAllAsync();

                List<ApplicationQuestionTypeModel> models = questionTypes
                    .Select(_mapper.Map<ApplicationQuestionTypeModel>)
                    .ToList();

                return models;
            }
        }
    }
}
