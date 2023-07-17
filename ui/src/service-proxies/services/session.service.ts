import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { ICreateSessionDto, IEndSessionDto, ISessionVm } from '../dto/session';
import { HttpClient } from '@angular/common/http';
import { baseUrl } from '../const';

@Injectable({ providedIn: 'root' })
export class SessionServiceProxy {
  constructor(private readonly _http: HttpClient) {}

  public getAll(): Observable<ISessionVm[]> {
    return this._http.get<ISessionVm[]>(baseUrl + 'api/session/getAll');
  }

  public get(id: string): Observable<ISessionVm> {
    return this._http.get<ISessionVm>(baseUrl + `api/session/${id}`);
  }

  public create(dto: ICreateSessionDto): Observable<string> {
    return this._http.post<string>(baseUrl + 'api/session/create', dto);
  }

  public end(dto: IEndSessionDto): Observable<void> {
    return this._http.put<void>(baseUrl + 'api/session/end', dto);
  }

  public delete(id: string): Observable<void> {
    return this._http.delete<void>(baseUrl + `api/session/${id}`);
  }
}
