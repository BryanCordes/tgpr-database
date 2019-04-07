import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../../environments/environment";
import {ApplicationQuestionModel} from '../../../_models/applications/application-question.model';
import {EditApplicationQuestionModel} from '../../../administration/applications/questions/edit/edit-application-question-dialog.component';

@Injectable()
export class ApplicationQuestionService {

    constructor(private http: HttpClient) { }

    create(applicationQuestion: ApplicationQuestionModel) {
        return this.http.post<ApplicationQuestionModel>(environment.baseUrl + "/api/ApplicationQuestion/create", applicationQuestion);
    }

    update(applicationQuestion: EditApplicationQuestionModel) {
        return this.http.put<ApplicationQuestionModel>(environment.baseUrl + `/api/ApplicationQuestion/${applicationQuestion.ApplicationQuestionId}`, applicationQuestion);
    }

    updateSortOrder(applicationQuestion: ApplicationQuestionModel) {
        return this.http.post(environment.baseUrl + "/api/ApplicationQuestion/updateSortOrder", applicationQuestion);
    }

    delete(applicationQuestion: ApplicationQuestionModel) {
        return this.http.delete(environment.baseUrl + `/api/ApplicationQuestion/${applicationQuestion.ApplicationQuestionId}`);
    }
}
