import {Component, OnInit, ViewChild} from '@angular/core';
import {AuthenticationService} from '../../_services/authentication/authentication.service';
import {TimeoutService} from './timeout.service';
import {Router} from '@angular/router';
import {SwalComponent, SwalPartialTargets} from '@sweetalert2/ngx-sweetalert2';
import Swal from 'sweetalert2';

@Component({
  selector: 'tgpr-timeout',
  templateUrl: './timeout.component.html'
})
export class TimeoutComponent implements OnInit {

  @ViewChild('timeoutSwal') private timeoutSwal: SwalComponent;

  constructor(public swalTargets: SwalPartialTargets, private _timeoutService: TimeoutService, private _authService: AuthenticationService, private _router: Router) { }

  timeoutMilliseconds = (30 * 1000);
  countdown = 30;

  private cancelled = false;
  private closing = false;

  public close(){
    this.cancelled = true;
  }

  ngOnInit(): void {
    this._timeoutService.onIdleStart$
      .subscribe(() => {
        this.cancelled = false;
        this.timeoutSwal.show();
      });

    this._timeoutService.onCountdown$
      .subscribe(countdown => {
        this.countdown = countdown;
      });

    this._timeoutService.onInterrupt$
      .subscribe(() => {
        if(Swal && Swal.isVisible()) {
          Swal.clickCancel();

          // we are still here
          // update the token
          this._authService.refreshToken();
        }
      });

    this._timeoutService.onTimeout$
      .subscribe(() => {
        this._authService.logout(this._router.routerState.snapshot.url);
      });

    this._timeoutService.restart();
  }
}
