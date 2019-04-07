import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from "@angular/router";
import {Observable} from "rxjs";
import {ApplicationTemplateService} from "../application-template.service";
import {ApplicationTemplateModel} from '../../../../_models/administration/applications/application-template.model';

@Injectable()
export class ApplicationTemplateResolver implements Resolve<ApplicationTemplateModel> {
    constructor(private _service: ApplicationTemplateService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ApplicationTemplateModel> | Promise<ApplicationTemplateModel> | ApplicationTemplateModel {
        let templateId: number = parseInt(route.paramMap.get('id'));

        return this._service.getApplication(templateId);
    }
}
