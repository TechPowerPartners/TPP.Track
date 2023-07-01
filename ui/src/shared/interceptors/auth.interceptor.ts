import {
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserService } from '@shared/services/user.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private readonly _userService: UserService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    const authToken = this._userService.token;

    const authReq = req.clone({
      headers: req.headers.set('Authorization', 'Bearer' + authToken),
    });

    return next.handle(authReq);
  }
}
