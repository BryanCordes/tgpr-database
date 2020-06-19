import {Component} from '@angular/core';
import {ApplicationReviewViewModel} from './application-review.view-model';

@Component({
  selector: 'tgpr-application-review',
  templateUrl: './application-review.component.html',
  providers: [ApplicationReviewViewModel]
})
export class ApplicationReviewComponent {

}
