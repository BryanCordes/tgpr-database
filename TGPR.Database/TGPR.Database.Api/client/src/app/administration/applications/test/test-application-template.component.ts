import {Component, OnInit} from '@angular/core';
import {TestApplicationTemplateViewModel} from './test-application-template.view-model';
import {ActivatedRoute} from '@angular/router';
import {SharedService} from '../../../layout/shared.service';
import {FormGroup} from '@angular/forms';



@Component({
  selector: 'tgpr-application-administration-test',
  templateUrl: './test-application-template.component.html',
  styleUrls: ['../../../_ui/ni-card/ni-card.component.scss'],
  providers: [TestApplicationTemplateViewModel]
})
export class TestApplicationTemplateComponent implements OnInit {

  private pageTitle = 'Test Application Template';

  public templateTitle = '';
  public formGroup: FormGroup;

  public breadcrumb: any[] = [
    {
      title: 'Administration',
      link: '/administration'
    }, {
      title: 'Application Administration',
      link: '/administration/applications'
    }, {
      title: 'Test Application Template',
      link: ''
    }
  ];

  constructor(public _viewModel: TestApplicationTemplateViewModel, private _route: ActivatedRoute, private _sharedService: SharedService) {
    this._sharedService.emitChange(this.pageTitle);

    // let the questions dynamically load their own controls
    this.formGroup = this._viewModel.createFormGroup();
  }

  public submit() {
    this._viewModel.save();
  }

  public isValid() {
    return this.formGroup.valid;
  }

  public cancel() {
    this._viewModel.cancel();
  }

  private getTitle() {
    let title = `Test Application Template [${this._viewModel.template.Name}]`;

    return title;
  }

  ngOnInit(): void {
    this._viewModel.template = this._route.snapshot.data.template;
    this._viewModel.formGroup = this.formGroup;

    this._viewModel.initializeQuestions();

    this.templateTitle = this.getTitle();
  }
}
