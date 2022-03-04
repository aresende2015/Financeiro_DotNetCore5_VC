import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { User } from '@app/models/identity/User';
import { UserService } from '@app/services/user.service';
import { setTheme } from 'ngx-bootstrap/utils';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private userService: UserService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let currentUser: User;

    this.userService.currentUser$.pipe(take(1)).subscribe(user => {
      currentUser = user

      if (currentUser) {
        request = request.clone(
          {
            setHeaders: {
              Authorization: `Bearer ${currentUser.token}`
            }
          }
        );
      }
    });

    return next.handle(request);
  }
}
