import {AfterViewInit, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {MatPaginator, MatSort} from '@angular/material';
import {ActivatedRoute} from '@angular/router';
import {SharedService} from '../../layout/shared.service';
import {ApplicationAdministrationViewModel} from './application-administration.view-model';
import {tap} from 'rxjs/operators';
import {merge} from 'rxjs/internal/observable/merge';
import {ApplicationTemplateSummaryModel} from '../../_models/administration/applications/application-template-summary.model';

@Component({
  selector: 'tgpr-application-administration',
  templateUrl: './application-administration.component.html',
  providers: [ApplicationAdministrationViewModel]
})
export class ApplicationAdministrationComponent implements OnInit, AfterViewInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('dataFilter') filterInput: ElementRef;

  private pageTitle = 'Application Administration';

  public breadcrumb: any[] = [
    {
      title: 'Administration',
      link: '/administration'
    },
    {
      title: 'Application Administration',
      link: ''
    }
  ];

  constructor(public _viewModel: ApplicationAdministrationViewModel, private _route: ActivatedRoute, private _sharedService: SharedService) {
    this._sharedService.emitChange(this.pageTitle);
  }

  public create() {
    this._viewModel.create();
  }

  public rename(template: ApplicationTemplateSummaryModel) {
    this._viewModel.rename(template);
  }

  public test(template: ApplicationTemplateSummaryModel) {
    this._viewModel.test(template);
  }

  public edit(template: ApplicationTemplateSummaryModel) {
    this._viewModel.edit(template);
  }

  public delete(template: ApplicationTemplateSummaryModel) {
    this._viewModel.delete(template)
      .subscribe(() => {
        this.updateDataSource();
      });
  }

  public setActive($event, template: ApplicationTemplateSummaryModel) {
    $event.preventDefault();

    this._viewModel.setActive(template);
  }

  private updateDataSource() {
    let page: number = this.paginator.pageIndex;
    let pageSize: number = this.paginator.pageSize;
    let sortColumn: string = this.sort.active;
    let sortDirection: string = this.sort.direction;
    let filter: string = this.filterInput.nativeElement.value;

    this._viewModel.updateDataSource(page, pageSize, sortColumn, sortDirection, filter);
  }

  ngOnInit(): void {
    this._viewModel.applicationTypes = this._route.snapshot.data.applicationTypes;

    const dataSourceResponse = this._route.snapshot.data.dataSourceResponse;
    this._viewModel.initializeDataSource(dataSourceResponse);
  }

  ngAfterViewInit(): void {
    this.sort.sortChange
      .subscribe(() => this.paginator.pageIndex = 0);

    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        tap(() => this.updateDataSource())
      )
      .subscribe();
  }
}
