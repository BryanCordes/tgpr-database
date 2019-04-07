import {Injectable} from '@angular/core';
import {DEFAULT_INTERRUPTSOURCES, Idle} from '@ng-idle/core';
import {Keepalive} from '@ng-idle/keepalive';
import {Observable} from 'rxjs/internal/Observable';
import {environment} from '../../../environments/environment';

@Injectable()
export class TimeoutService {

  public onIdleStart$: Observable<any>;
  public onCountdown$: Observable<number>;
  public onInterrupt$: Observable<any>;
  public onTimeout$: Observable<any>;
  public onKeepalive$: Observable<any>;

  constructor(private _idle: Idle, private _keepalive: Keepalive) {

    _idle.setIdle(environment.timeoutSeconds - 30);
    _idle.setTimeout(30);

    _idle.setInterrupts(DEFAULT_INTERRUPTSOURCES);

    this.onIdleStart$ = _idle.onIdleStart;
    this.onCountdown$ = _idle.onTimeoutWarning;
    this.onInterrupt$ = _idle.onIdleEnd;
    this.onTimeout$ = _idle.onTimeout;

    _keepalive.interval(environment.timeoutSeconds / 2);

    this.onKeepalive$ = _keepalive.onPing;

    this.restart();
  }

  public restart() {
    this._idle.watch();
  }
}
