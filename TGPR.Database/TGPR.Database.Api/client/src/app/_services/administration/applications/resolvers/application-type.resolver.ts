import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from "@angular/router";
import {Observable} from "rxjs";
import {ApplicationTemplateService} from "../application-template.service";
import {ApplicationTypeModel} from '../../../../_models/applications/application-type.model';

@Injectable()
export class ApplicationTypeResolver implements Resolve<ApplicationTypeModel[]> {

    constructor(private _service: ApplicationTemplateService){ }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ApplicationTypeModel[]> | Promise<ApplicationTypeModel[]> | ApplicationTypeModel[] {
        return this._service.getApplicationTypes();
    }

}
