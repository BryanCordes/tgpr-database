import {UserRoleDisplayModel, UserRoleListViewModel} from './user-role-list.view-model';
import {Component, Input, OnInit} from '@angular/core';
import {UserModel} from '../../../_models/administration/users/user.model';
import {RoleModel} from '../../../_models/administration/users/role.model';

@Component({
  selector: 'tgpr-user-role-list',
  templateUrl: './user-role-list.component.html',
  providers: [UserRoleListViewModel]
})
export class UserRoleListComponent implements OnInit {

  @Input() user: UserModel;
  @Input() roles: RoleModel[];

  constructor(private _viewModel: UserRoleListViewModel) { }

  public toggle(model: UserRoleDisplayModel) {
    if (model.UserRole) {
      this._viewModel.remove(model);
    } else {
      this._viewModel.add(model);
    }
  }

  ngOnInit(): void {
    this._viewModel.user = this.user;
    this._viewModel.roles = this.roles;

    this._viewModel.load();
  }
}


