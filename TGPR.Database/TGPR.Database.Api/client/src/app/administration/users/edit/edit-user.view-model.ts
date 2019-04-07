import {Injectable} from '@angular/core';
import {UserModel} from '../../../_models/administration/users/user.model';
import {UserAdministrationService} from '../../../_services/administration/users/user-administration.service';
import {FormControl, Validators} from '@angular/forms';
import {UserRoleModel} from '../../../_models/administration/users/user-role.model';
import {Router} from '@angular/router';

@Injectable()
export class EditUserViewModel {

  public emailFormControl: FormControl;
  public firstNameFormControl: FormControl;
  public lastNameFormControl: FormControl;
  public phoneFormControl: FormControl;
  public addressFormControl: FormControl;

  public user: UserModel;
  public roles: UserRoleModel[];

  constructor(private _service: UserAdministrationService, private _router: Router) {

    this.user = {
      Email: '',
      PasswordHash: '',
      FirstName: '',
      LastName: '',
      PhoneNumber: '',
      Address: '',
      Roles: []
    };
  }

  public load() {
    this.emailFormControl = new FormControl(this.user.Email, [
      Validators.required,
      Validators.email,
    ]);

    this.firstNameFormControl = new FormControl(this.user.FirstName, [
      Validators.required
    ]);

    this.lastNameFormControl = new FormControl(this.user.LastName, [
      Validators.required
    ]);

    this.phoneFormControl = new FormControl(this.user.PhoneNumber, []);

    this.addressFormControl = new FormControl(this.user.Address, []);
  }

  public save() {
    if (this.emailFormControl.invalid
      || this.firstNameFormControl.invalid
      || this.lastNameFormControl.invalid) {
      return;
    }

    this.user.Email = this.emailFormControl.value;
    this.user.FirstName = this.firstNameFormControl.value;
    this.user.LastName = this.lastNameFormControl.value;
    this.user.PhoneNumber = this.phoneFormControl.value;
    this.user.Address = this.addressFormControl.value;

    this._service.update(this.user)
      .subscribe(() => {
        this._router.navigate(['administration', 'users']);
      });
  }

  public cancel() {
    this._router.navigate(['administration', 'users']);
  }
}
