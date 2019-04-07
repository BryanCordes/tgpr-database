import {Injectable} from "@angular/core";
import {environment} from "../../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {ApplicationQuestionTypeModel} from '../../../_models/applications/application-question-type.model';

@Injectable()
export class ApplicationQuestionTypeService {

    constructor(private http: HttpClient) { }

    getQuestionTypes() {
        return this.http.get<ApplicationQuestionTypeModel[]>(environment.baseUrl + "/api/ApplicationQuestionType");
    }
}
