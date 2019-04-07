import {Injectable} from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpInterceptor,
  HttpHeaderResponse, HttpProgressEvent, HttpResponse, HttpUserEvent, HttpSentEvent
} from '@angular/common/http';
import {Observable} from 'rxjs/internal/Observable';


@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpSentEvent | HttpHeaderResponse | HttpProgressEvent | HttpResponse<any> | HttpUserEvent<any>> {
    request = this.addToken(request);

    return next.handle(request);
  }

  addToken(request: HttpRequest<any>) {
    // add authorization header with jwt token if available
    const jwt = localStorage.getItem('auth_token');
    if (jwt) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${jwt}`
        }
      });
    }

    return request;
  }
}
