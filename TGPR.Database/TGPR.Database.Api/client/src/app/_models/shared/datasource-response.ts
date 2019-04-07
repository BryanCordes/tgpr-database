import {DataSourceFilter} from "./datasource-filter";

export class DataSourceResponse<T> {
  DataSourceFilter: DataSourceFilter;
  TotalRecords: number;
  Data: T;
}
