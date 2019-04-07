import {Component, OnInit} from '@angular/core';
import {CreateUserViewModel} from './create-user.view-model';
import {ActivatedRoute} from '@angular/router';
import {SharedService} from '../../../layout/shared.service';

@Component({
  selector: 'tgpr-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['../../../_ui/ni-card/ni-card.component.scss'],
  providers: [CreateUserViewModel]
})
export class CreateUserComponent implements OnInit {

  private pageTitle = 'Create User';

  public breadcrumb: any[] = [
    {
      title: 'Administration',
      link: '/administration'
    }, {
      title: 'User Administration',
      link: '/administration/users'
    }, {
      title: 'Create User',
      link: ''
    }
  ];

  constructor(public _viewModel: CreateUserViewModel, private _route: ActivatedRoute, private _sharedService: SharedService) {
    this._sharedService.emitChange(this.pageTitle);
  }

  save() {
    this._viewModel.save();
  }

  cancel() {
    this._viewModel.cancel();
  }

  ngOnInit(): void {
    this._viewModel.roles = this._route.snapshot.data.roles;
  }
}
