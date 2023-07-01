import { Injectable } from '@angular/core';
import { IUserInfo } from 'src/service-proxies/dto/account';

const USER_KEY = 'auth-user';
const TOKEN_KEY = 'token';

@Injectable({ providedIn: 'root' })
export class UserService {
  public get isLoggedIn(): boolean {
    return !!window.sessionStorage.getItem(USER_KEY);
  }

  public get token(): string | null {
    return window.sessionStorage.getItem(TOKEN_KEY);
  }

  public saveUser(user: IUserInfo): void {
    window.sessionStorage.removeItem(USER_KEY);
    window.sessionStorage.setItem(USER_KEY, JSON.stringify(user));
  }

  public getUser(): IUserInfo {
    const user = window.sessionStorage.getItem(USER_KEY);

    return user ? JSON.parse(user) : {};
  }

  public saveToken(token: string): void {
    window.sessionStorage.removeItem(TOKEN_KEY);
    window.sessionStorage.setItem(TOKEN_KEY, token);
  }

  public clear(): void {
    window.sessionStorage.clear();
  }
}
