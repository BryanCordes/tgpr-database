import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {SharedService} from '../../../layout/shared.service';
import {EditUserViewModel} from './edit-user.view-model';

@Component({
  selector: 'tgpr-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['../../../_ui/ni-card/ni-card.component.scss'],
  providers: [EditUserViewModel]
})
export class EditUserComponent implements OnInit {

  private pageTitle = 'Edit User';

  public breadcrumb: any[] = [
    {
      title: 'Administration',
      link: '/administration'
    }, {
      title: 'User Administration',
      link: '/administration/users'
    }, {
      title: 'Edit User',
      link: ''
    }
  ];

  constructor(public _viewModel: EditUserViewModel, private _route: ActivatedRoute, private _sharedService: SharedService) {
    this._sharedService.emitChange(this.pageTitle);
  }

  save() {
    this._viewModel.save();
  }

  cancel() {
    this._viewModel.cancel();
  }

  ngOnInit(): void {
    this._viewModel.user = this._route.snapshot.data.user;
    this._viewModel.roles = this._route.snapshot.data.roles;

    this._viewModel.load();
  }
}
