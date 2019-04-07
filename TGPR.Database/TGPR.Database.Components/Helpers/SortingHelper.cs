using System.Collections.Generic;
using System.Linq;
using TGPR.Database.DataAccess.Infrastructure;

namespace TGPR.Database.Components.Helpers
{
    public interface ISortingHelper
    {
        IEnumerable<T> ReorderReviewer<T>(IEnumerable<T> entities, int oldValue, int newValue)
            where T : class, IApplicationSortableEntity;

        IEnumerable<T> ReorderDelete<T>(IEnumerable<T> entities, int value)
            where T : class, IApplicationSortableEntity;
    }

    internal class SortingHelper : ISortingHelper
    {
        public IEnumerable<T> ReorderReviewer<T>(IEnumerable<T> entities, int oldValue, int newValue)
            where T : class, IApplicationSortableEntity
        {
            var entityList = entities.ToList();

            IEnumerable<T> below = entityList
                .Where(x => x.ReviewerSortOrder >= newValue && x.ReviewerSortOrder < oldValue);
            foreach (var belowEntity in below)
            {
                belowEntity.ReviewerSortOrder++;

                yield return belowEntity;
            }

            IEnumerable<T> above = entityList
                .Where(x => x.ReviewerSortOrder <= newValue && x.ReviewerSortOrder > oldValue);
            foreach (var aboveEntity in above)
            {
                aboveEntity.ReviewerSortOrder--;

                yield return aboveEntity;
            }
        }

        public IEnumerable<T> ReorderDelete<T>(IEnumerable<T> entities, int value)
            where T : class, IApplicationSortableEntity
        {
            IEnumerable<T> above = entities
                .Where(x => x.ReviewerSortOrder > value);
            foreach (var aboveEntity in above)
            {
                aboveEntity.ReviewerSortOrder--;

                yield return aboveEntity;
            }
        }
    }
}
