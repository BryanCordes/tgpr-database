import { Component } from '@angular/core';
import {LoginViewModel} from './login.view-model';

@Component({
  selector: 'tgpr-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  providers: [LoginViewModel]
})
export class LoginComponent {

  constructor(public _viewModel: LoginViewModel) {}

}
