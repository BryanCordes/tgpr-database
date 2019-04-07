import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {SharedService} from '../../../layout/shared.service';
import {EditApplicationTemplateViewModel} from './edit-application-template.view-model';

@Component({
  selector: 'tgpr-edit-application-template',
  templateUrl: './edit-application-template.component.html',
  providers: [EditApplicationTemplateViewModel]
})
export class EditApplicationTemplateComponent implements OnInit {

  private pageTitle = 'Edit Application Template';

  public templateTitle = '';

  public breadcrumb: any[] = [
    {
      title: 'Administration',
      link: '/administration'
    }, {
      title: 'Application Administration',
      link: '/administration/applications'
    }, {
      title: 'Edit Application Template',
      link: ''
    }
  ];

  constructor(public _viewModel: EditApplicationTemplateViewModel, private _route: ActivatedRoute, private _sharedService: SharedService) {
    this._sharedService.emitChange(this.pageTitle);
  }

  private getTitle() {
    let title = `Edit Application Template [${this._viewModel.template.Name}]`;

    return title;
  }

  ngOnInit(): void {
    this._viewModel.template = this._route.snapshot.data.template;
    this._viewModel.questionTypes = this._route.snapshot.data.questionTypes;

    this.templateTitle = this.getTitle();
  }
}
