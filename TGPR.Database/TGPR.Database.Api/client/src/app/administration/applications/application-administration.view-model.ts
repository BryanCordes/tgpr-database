import {Injectable} from '@angular/core';
import {ApplicationTemplateDataSource} from './application-template.data-source';
import {DataSourceFilter} from '../../_models/shared/datasource-filter';
import {ApplicationTypeModel} from '../../_models/applications/application-type.model';
import {ApplicationTemplateService} from '../../_services/administration/applications/application-template.service';
import {DataSourceResponse} from '../../_models/shared/datasource-response';
import {ApplicationTemplateSummaryModel} from '../../_models/administration/applications/application-template-summary.model';
import {MatDialog} from '@angular/material';
import {
  CreateApplicationTemplateDialogComponent,
  CreateApplicationTemplateModel
} from './create/create-application-template-dialog.component';
import {ApplicationTemplateModel} from '../../_models/administration/applications/application-template.model';
import {Router} from '@angular/router';
import {RenameApplicationTemplateDialogComponent} from './rename/rename-application-template-dialog.component';
import {RoleModel} from '../../_models/administration/users/role.model';

@Injectable()
export class ApplicationAdministrationViewModel {

  public dataSource: ApplicationTemplateDataSource;
  public columns: string[];
  public dataSourceFilter: DataSourceFilter;
  public applicationTypes: ApplicationTypeModel[];

  constructor(private _service: ApplicationTemplateService, private _router: Router, private _dialog: MatDialog) {
    this.initializeFilter();
    this.initializeColumns();
  }

  public initializeDataSource(templates: DataSourceResponse<ApplicationTemplateSummaryModel[]>) {
    this.dataSource = new ApplicationTemplateDataSource(this._service);

    this.dataSource.setApplications(templates);

    this.dataSource.loading$
      .subscribe(loading => {
        if(loading) {
          return;
        }

        this.setTypes();
      });
  }

  public updateDataSource(page: number, pageSize: number, sort: string, direction: string, filter: string) {
    this.dataSourceFilter.Page = page;
    this.dataSourceFilter.PageSize = pageSize;
    this.dataSourceFilter.SortColumn = sort;
    this.dataSourceFilter.SortDirection = direction;
    this.dataSourceFilter.Filter = filter;

    this.dataSource.loadApplications(this.dataSourceFilter);
  }

  public create() {
    const dialogRef = this._dialog.open(CreateApplicationTemplateDialogComponent, {
      width: '350px',
      data: this.applicationTypes
    });

    dialogRef.afterClosed()
      .subscribe(result => {
        const modelData: CreateApplicationTemplateModel = result;
        if (!modelData) {
          return;
        }

        let applicationTemplate: ApplicationTemplateModel = {
          Name: modelData.Name,
          ApplicationTypeId: modelData.ApplicationTypeId,
          Type: null,
          Active: true,
          Deleted: false,
          Categories: [],
          CreatedOn: new Date(),
          UpdatedOn: null
        };

        this._service.create(applicationTemplate)
          .subscribe(template => {
            this._router.navigate(["administration", "application", template.ApplicationTemplateId]);
          });
      });
  }

  public rename(template: ApplicationTemplateSummaryModel) {
    const dialogRef = this._dialog.open(RenameApplicationTemplateDialogComponent, {
      width: "350px",
      data: template.Name
    });

    dialogRef.afterClosed()
      .subscribe(result => {
        let modelData: string = result;
        if(!modelData) {
          return;
        }

        let applicationTemplate: ApplicationTemplateModel = {
          ApplicationTemplateId: template.ApplicationTemplateId,
          Name: modelData,
          ApplicationTypeId: template.ApplicationTypeId,
          Type: null,
          Active: true,
          Deleted: false,
          Categories: [],
          CreatedOn: new Date(),
          UpdatedOn: null
        };

        this._service.updateName(applicationTemplate)
          .subscribe(() =>{
            template.Name = modelData;
          });
      });
  }

  public edit(template: ApplicationTemplateSummaryModel) {
    this._router.navigate(["administration", "application", template.ApplicationTemplateId]);
  }

  public setActive(template: ApplicationTemplateSummaryModel) {
    if(template.Active === true) {
      return;
    }

    this._service.setActive(template)
      .subscribe(() =>{
        template.Active = true;
        this.deactivate(template);
      });
  }

  public delete(template: ApplicationTemplateSummaryModel) {
    return this._service.delete(template);
  }

  private initializeFilter() {
    this.dataSourceFilter = {
      Filter: "",
      SortColumn: "Name",
      SortDirection: "asc",
      Page: 0, // 0 based
      PageSize: 25
    };
  }

  private initializeColumns(){
    this.columns = [
      "Name",
      "ApplicationTypeId",
      "CreatedOn",
      "UpdatedOn",
      "Active",
      "delete",
      "test",
      "rename",
      "edit"
    ];
  }

  private setTypes() {
    for(let template of this.dataSource.data) {
      this.setType(template);
    }
  }

  private setType(template: ApplicationTemplateSummaryModel) {
    let templateType: ApplicationTypeModel = this.applicationTypes
      .find(x => x.ApplicationTypeId == template.ApplicationTypeId);

    template.Type = templateType
      ? templateType.Name
      : "";
  }

  private deactivate(template: ApplicationTemplateSummaryModel){
    let templates: ApplicationTemplateSummaryModel[] = this.dataSource.data
      .filter(x => x.ApplicationTypeId === template.ApplicationTypeId
        && x.ApplicationTemplateId !== template.ApplicationTemplateId);

    for(let deactivatedTemplates of templates){
      deactivatedTemplates.Active = false;
    }
  }
}
