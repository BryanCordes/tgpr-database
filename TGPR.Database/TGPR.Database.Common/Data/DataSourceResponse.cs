using System.Collections.Generic;

namespace TGPR.Database.Common.Data
{
    public class DataSourceResponse<T>
    {
        public DataSourceFilter DataSourceFilter { get; set; }
        public int TotalRecords { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
