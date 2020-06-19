import {AfterViewInit, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {CompanionApplicationsViewModel} from './companion-applications.view-model';
import {ActivatedRoute} from '@angular/router';
import {SharedService} from '../../layout/shared.service';
import {MatPaginator, MatSort, Sort} from '@angular/material';
import {merge} from 'rxjs/internal/observable/merge';
import {debounceTime, distinctUntilChanged, tap} from 'rxjs/operators';
import {ApplicationModel} from '../../_models/applications/application.model';
import {fromEvent} from 'rxjs/internal/observable/fromEvent';

@Component({
  selector: 'tgpr-companion-applications',
  templateUrl: './companion-applications.component.html',
  providers: [CompanionApplicationsViewModel]
})
export class CompanionApplicationsComponent implements OnInit, AfterViewInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('dataFilter') filterInput: ElementRef;

  private pageTitle = "Companion Applications";

  public breadcrumb: any[] = [
    {
      title: 'Applications',
      link: '/applications'
    },
    {
      title: 'Companion Applications',
      link: ''
    }
  ];

  constructor(public _viewModel: CompanionApplicationsViewModel, private _route: ActivatedRoute, private _sharedService: SharedService) {
    this._sharedService.emitChange(this.pageTitle);
  }

  public edit(application: ApplicationModel) {

  }

  public delete(application: ApplicationModel) {

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
    const dataSourceResponse = this._route.snapshot.data.dataSourceResponse;
    this._viewModel.initializeDataSource(dataSourceResponse);
  }

  ngAfterViewInit(): void {
    const sortState: Sort = { active: "ApplicationId", direction: "asc"};
    this.sort.active = sortState.active;
    this.sort.direction = sortState.direction;

    this.sort.sortChange
      .subscribe(() => this.paginator.pageIndex = 0);

    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
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
