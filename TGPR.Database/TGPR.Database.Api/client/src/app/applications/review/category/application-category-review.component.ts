import {Component, Input} from '@angular/core';
import {ApplicationCategoryReviewViewModel} from './application-category-review.view-model';
import {ApplicationCategoryModel} from '../../../_models/applications/application-category.model';
import {ApplicationCategoryReviewModel} from '../../../_models/applications/application-category-review.model';
import {ApplicationCategoryReviewStatusModel} from '../../../_models/applications/application-category-review-status.model';

@Component({
  selector: 'tgpr-application-category-review',
  templateUrl: './application-category-review.component.html',
  providers: [ApplicationCategoryReviewViewModel]
})
export class ApplicationCategoryReviewComponent {

  @Input() category: ApplicationCategoryModel;
  @Input() categoryReview: ApplicationCategoryReviewModel;



}
