import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { baseUrl } from 'src/service-proxies/const';
import { ILoginDto, IRegisterDto, IUserInfo } from '../dto/account';

@Injectable({ providedIn: 'root' })
export class AccountService {
  constructor(private readonly _httpClient: HttpClient) {}

  public login(dto: ILoginDto): Observable<any> {
    return this._httpClient.post(baseUrl + 'api/account/login', dto, {
      responseType: 'text',
    });
  }

  public register(dto: IRegisterDto): Observable<any> {
    return this._httpClient.post(baseUrl + 'api/account/register', dto, {
      responseType: 'text',
    });
  }

  public logout(): Observable<void> {
    return this._httpClient.post<void>(baseUrl + 'api/account/logout', {});
  }

  public getUserInfo(): Observable<IUserInfo> {
    return this._httpClient.get<IUserInfo>(baseUrl + 'api/account/info');
  }
}
