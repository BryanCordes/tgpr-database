import {Component, Input, OnInit} from '@angular/core';
import {RoleActivityAdministrationViewModel, RoleActivityDisplayModel} from './role-activity-administration.view-model';
import {RoleModel} from '../../../_models/administration/users/role.model';
import {SecurityActivityModel} from '../../../_models/administration/users/security-activity.model';

@Component({
  selector: 'tgpr-role-activity-administration',
  templateUrl: './role-activity-administration.component.html',
  providers: [RoleActivityAdministrationViewModel]
})
export class RoleActivityAdministrationComponent implements OnInit {

  @Input() set activities(activityModels: SecurityActivityModel[]) {
    this._viewModel.activities = activityModels;
  }

  @Input() set role(roleModel: RoleModel) {
    this._viewModel.role = roleModel;
    this._viewModel.load();
  }

  constructor(public _viewModel: RoleActivityAdministrationViewModel) { }

  public toggle(model: RoleActivityDisplayModel) {
    if (model.RoleActivity) {
      this._viewModel.remove(model);
    } else {
      this._viewModel.add(model);
    }
  }

  ngOnInit(): void {

  }
}
