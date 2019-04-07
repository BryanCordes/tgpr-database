import {Component, OnInit} from '@angular/core';
import {RoleAdministrationViewModel} from './role-administration.view-model';
import {RoleModel} from '../../_models/administration/users/role.model';
import {ActivatedRoute} from '@angular/router';
import {MatDialog} from '@angular/material';
import {CreateRoleDialogComponent} from './create/create-role-dialog.component';
import {SharedService} from '../../layout/shared.service';
import {EditRoleDialogComponent} from './edit/edit-role-dialog.component';

@Component({
  selector: 'tgpr-role-administration',
  templateUrl: './role-administration.component.html',
  providers: [RoleAdministrationViewModel]
})
export class RoleAdministrationComponent implements OnInit {

  private pageTitle = 'User Role Administration';

  public breadcrumb: any[] = [
    {
      title: 'Administration',
      link: '/administration'
    },
    {
      title: 'User Role Administration',
      link: ''
    }
  ];

  constructor(public _viewModel: RoleAdministrationViewModel, private _sharedService: SharedService, private _route: ActivatedRoute, private _dialog: MatDialog) {
    this._sharedService.emitChange(this.pageTitle);
  }

  public create() {
    const dialogRef = this._dialog.open(CreateRoleDialogComponent, {
      width: '250px'
    });

    dialogRef.afterClosed()
      .subscribe(result => {
        const modelData: RoleModel = result;
        if (!modelData) {
          return;
        }

        this._viewModel.create(modelData);
      });
  }

  public edit(role: RoleModel) {
    this._viewModel.edit(role);
  }

  public rename(role: RoleModel) {
    const roleCopy: RoleModel = JSON.parse(JSON.stringify(role));
    const dialogRef = this._dialog.open(EditRoleDialogComponent, {
      width: '250px',
      data: roleCopy
    });

    dialogRef.afterClosed()
      .subscribe(result => {
        const modelData: RoleModel = result;
        if (!modelData) {
          return;
        }

        this._viewModel.rename(modelData)
          .subscribe(() =>{
            role.Name = modelData.Name;
          })
      })
  }

  public delete(role: RoleModel) {
    this._viewModel.delete(role);
  }

  ngOnInit(): void {
    this._viewModel.activities = this._route.snapshot.data.activities;

    const roles: RoleModel[] = this._route.snapshot.data.roles;

    this._viewModel.initializeDataSource(roles);
  }
}
