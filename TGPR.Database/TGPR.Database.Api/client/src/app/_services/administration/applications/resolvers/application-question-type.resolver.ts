import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from "@angular/router";
import {Observable} from "rxjs";
import {ApplicationQuestionTypeService} from "../application-question-type.service";
import {ApplicationQuestionTypeModel} from '../../../../_models/applications/application-question-type.model';

@Injectable()
export class ApplicationQuestionTypeResolver implements Resolve<ApplicationQuestionTypeModel[]> {

    constructor(private _service: ApplicationQuestionTypeService){ }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ApplicationQuestionTypeModel[]> | Promise<ApplicationQuestionTypeModel[]> | ApplicationQuestionTypeModel[] {
        return this._service.getQuestionTypes();
    }

}
