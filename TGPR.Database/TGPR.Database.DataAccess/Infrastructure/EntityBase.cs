using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using TGPR.Database.Common.Attributes;

namespace TGPR.Database.DataAccess.Infrastructure
{
    public abstract class EntityBase : IEntity
    {
        [NotMapped]
        [JsonIgnore]
        [MapIgnore]
        public ObjectState ObjectState { get; set; } = ObjectState.Unchanged;
    }
}
