import {Injectable} from '@angular/core';
import {UserModel} from '../../../_models/administration/users/user.model';
import {UserRoleModel} from '../../../_models/administration/users/user-role.model';
import {UserAdministrationService} from '../../../_services/administration/users/user-administration.service';
import {Router} from '@angular/router';
import {FormControl, Validators} from '@angular/forms';

@Injectable()
export class CreateUserViewModel {

  public emailFormControl: FormControl;
  public passwordFormControl: FormControl;
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

    this.initializeFormControls();
  }

  private initializeFormControls(){
    this.emailFormControl = new FormControl('', [
      Validators.required,
      Validators.email,
    ]);

    this.passwordFormControl = new FormControl('', [
      Validators.required
    ]);

    this.firstNameFormControl = new FormControl('', [
      Validators.required
    ]);

    this.lastNameFormControl = new FormControl('', [
      Validators.required
    ]);

    this.phoneFormControl = new FormControl('', []);

    this.addressFormControl = new FormControl('', []);
  }

  public save() {
    if (this.emailFormControl.invalid
        || this.passwordFormControl.invalid
        || this.firstNameFormControl.invalid
        || this.lastNameFormControl.invalid) {
      return;
    }

    this.user.Email = this.emailFormControl.value;
    this.user.PasswordHash = this.passwordFormControl.value;
    this.user.FirstName = this.firstNameFormControl.value;
    this.user.LastName = this.lastNameFormControl.value;
    this.user.PhoneNumber = this.phoneFormControl.value;
    this.user.Address = this.addressFormControl.value;

    this._service.create(this.user)
      .subscribe(savedUser => {
        this._router.navigate(['administration', 'users']);
      });
  }

  public cancel() {
    this._router.navigate(['administration', 'users']);
  }
}
