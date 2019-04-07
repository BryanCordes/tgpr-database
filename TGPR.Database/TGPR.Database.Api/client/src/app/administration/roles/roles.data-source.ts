import {CollectionViewer, DataSource} from '@angular/cdk/collections';
import {RoleModel} from '../../_models/administration/users/role.model';
import {BehaviorSubject} from 'rxjs/internal/BehaviorSubject';
import {Observable} from 'rxjs/internal/Observable';
import {RoleService} from '../../_services/administration/roles/role.service';

export class RoleDataSource extends DataSource<RoleModel> {
  private roles = new BehaviorSubject<RoleModel[]>([]);

  private loading = new BehaviorSubject<boolean>(false);

  public data: RoleModel[] = [];

  public loading$ = this.loading.asObservable();

  constructor(private _service: RoleService) {
    super();
  }

  connect(collectionViewer: CollectionViewer): Observable<RoleModel[]> {
    return this.roles.asObservable();
  }

  disconnect(collectionViewer: CollectionViewer): void {
    this.roles.complete();
    this.loading.complete();
  }

  addRole(role: RoleModel) {
    this.data.push(role);

    this.roles.next(this.data);
  }

  removeRole(role: RoleModel) {
    const index: number = this.data.indexOf(role);

    this.data.splice(index, 1);

    this.roles.next(this.data);
  }

  setRoles(roles: RoleModel[]) {
    this.data = roles;

    this.roles.next(roles);
  }
}
