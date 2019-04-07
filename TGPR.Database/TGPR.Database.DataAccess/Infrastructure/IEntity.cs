namespace TGPR.Database.DataAccess.Infrastructure
{
    public interface IEntity
    {
        ObjectState ObjectState { get; set; }
    }

    public interface IApplicationSortableEntity
    {
        int ApplicationSortOrder { get; set; }
        int ReviewerSortOrder { get; set; }
    }
}
