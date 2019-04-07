import {AfterViewInit, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {UserAdministrationViewModel} from './user-administration.view-model';
import {MatPaginator, MatSort} from '@angular/material';
import {ActivatedRoute} from '@angular/router';
import {DataSourceResponse} from '../../_models/shared/datasource-response';
import {debounceTime, distinctUntilChanged, tap} from 'rxjs/operators';
import {merge} from 'rxjs'
import {fromEvent} from 'rxjs/internal/observable/fromEvent';
import {SharedService} from '../../layout/shared.service';
import {UserSummaryModel} from '../../_models/administration/users/user-summary.model';

@Component({
  selector: 'tgpr-user-administration',
  templateUrl: './user-administration.component.html',
  providers: [UserAdministrationViewModel]
})
export class UserAdministrationComponent implements OnInit, AfterViewInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('dataFilter') filterInput: ElementRef;

  private pageTitle = 'User Administration';

  public breadcrumb: any[] = [
    {
      title: 'Administration',
      link: '/administration'
    },
    {
      title: 'User Administration',
      link: ''
    }
  ];

  constructor(public _viewModel: UserAdministrationViewModel, private _route: ActivatedRoute, private _sharedService: SharedService) {
    this._sharedService.emitChange(this.pageTitle);
  }

  ngOnInit(): void {
    const dataSourceResponse: DataSourceResponse<UserSummaryModel[]> = this._route.snapshot.data.dataSourceResponse;

    this._viewModel.initializeDataSource(dataSourceResponse);
  }

  public create() {
    this._viewModel.create();
  }

  public edit(user: UserSummaryModel) {
    this._viewModel.edit(user);
  }

  public setActive(user: UserSummaryModel) {
    this._viewModel.setActive(user);
  }

  public setInactive(user: UserSummaryModel) {
    this._viewModel.setInactive(user);
  }

  private updateDataSource(): void {
    let page: number = this.paginator.pageIndex;
    let pageSize: number = this.paginator.pageSize;
    let sortColumn: string = this.sort.active;
    let sortDirection: string = this.sort.direction;
    let filter: string = this.filterInput.nativeElement.value;

    this._viewModel.updateDataSource(page, pageSize, sortColumn, sortDirection, filter);
  }

  ngAfterViewInit(): void {
    this.sort.sortChange
      .subscribe(() => this.paginator.pageIndex = 0);

    merge(this.sort.sortChange, this.paginator.page)
      .pipe (
        tap(() => this.updateDataSource())
      )
      .subscribe();

    this.registerFilterInput();
  }

  private registerFilterInput(): void {
    fromEvent(this.filterInput.nativeElement, 'keyup')
      .pipe(
        debounceTime(250),
        distinctUntilChanged(),
        tap(() => {
          this.paginator.pageIndex = 0;
          this.updateDataSource();
        })
      )
      .subscribe();
  }
}
