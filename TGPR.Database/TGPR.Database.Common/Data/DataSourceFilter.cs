namespace TGPR.Database.Common.Data
{
    public class DataSourceFilter
    {
        public string Filter { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
