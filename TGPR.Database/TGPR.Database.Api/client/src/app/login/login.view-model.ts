import {Injectable} from '@angular/core';
import {LoginModel} from '../_models/login/login.model';
import {AuthenticationService} from '../_services/authentication/authentication.service';
import {Router} from '@angular/router';
import {FormControl, Validators} from '@angular/forms';

@Injectable()
export class LoginViewModel {
  public loginModel: LoginModel;
  public invalidCredentials = false;

  public emailFormControl: FormControl;
  public passwordFormControl: FormControl;

  constructor(private _authService: AuthenticationService, private _router: Router) {
    this.loginModel = {
      Email: '',
      Password: ''
    };

    this.emailFormControl = new FormControl('', [
      Validators.required,
      Validators.email,
    ]);

    this.passwordFormControl = new FormControl('', [
      Validators.required
    ]);
  }

  public login() {
    if (!this.emailFormControl.valid
       || !this.passwordFormControl.valid) {
      return;
    }

    this.loginModel.Email = this.emailFormControl.value;
    this.loginModel.Password = this.passwordFormControl.value;

    this.invalidCredentials = false;

    this._authService.login(this.loginModel)
      .subscribe(result => {
        if (result === 'invalid_credentials') {
          this.invalidCredentials = true;
        } else if (result === 'unexpected_error') {
          // prompt for unexpected error
        } else {
          this._router.navigate(['']);
        }
      });
  }
}
