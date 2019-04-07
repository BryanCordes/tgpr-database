import {Injectable} from '@angular/core';
import {RoleDataSource} from './roles.data-source';
import {RoleService} from '../../_services/administration/roles/role.service';
import {RoleModel} from '../../_models/administration/users/role.model';
import {SecurityActivityModel} from '../../_models/administration/users/security-activity.model';
import {Observable} from 'rxjs/internal/Observable';

@Injectable()
export class RoleAdministrationViewModel {
  public dataSource: RoleDataSource;
  public columns: string[];

  public activities: SecurityActivityModel;

  public selectedRole: RoleModel;

  constructor(private _service: RoleService) {
    this.createColumns();
  }

  private createColumns() {
    this.columns = [
      'Name',
      'delete',
      'rename',
      'edit'
    ];
  }

  public initializeDataSource(roles: RoleModel[]): void {
    this.dataSource = new RoleDataSource(this._service);

    this.dataSource.setRoles(roles);

    this.initializeEditRole();
  }

  public create(role: RoleModel): void {
    this._service.create(role)
      .subscribe(data => {
        this.dataSource.addRole(data);
      });
  }

  public edit(role: RoleModel): void {
    this.selectedRole = role;
  }

  public rename(role: RoleModel): Observable<RoleModel> {
    return this._service.edit(role);
  }

  public delete(role: RoleModel): void {
    this._service.delete(role)
      .subscribe(() =>{
        this.dataSource.removeRole(role);
        this.initializeEditRole();
      });
  }

  private initializeEditRole(){
    if (this.dataSource.data && this.dataSource.data.length > 0) {
      this.selectedRole = this.dataSource.data[0];
    } else {
      this.selectedRole = undefined;
    }
  }
}
