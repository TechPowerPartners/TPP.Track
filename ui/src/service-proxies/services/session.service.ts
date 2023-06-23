import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { ICreateSessionDto, IEndSessionDto, ISessionVm } from '../dto/session';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class SessionServiceProxy {
  constructor(private readonly _http: HttpClient) {}

  public getAll(): Observable<ISessionVm[]> {
    return this._http.get<ISessionVm[]>(
      'https://localhost:7234/api/session/getAll'
    );
  }

  public get(id: string): Observable<ISessionVm> {
    return this._http.get<ISessionVm>(
      `https://localhost:7234/api/session/${id}`
    );
  }

  public create(dto: ICreateSessionDto): Observable<string> {
    return this._http.post<string>(
      'https://localhost:7234/api/session/create',
      dto
    );
  }

  public update(dto: IEndSessionDto): Observable<void> {
    return this._http.put<void>(
      'https://localhost:7234/api/session/update',
      dto
    );
  }

  public delete(id: string): Observable<void> {
    return this._http.delete<void>(`https://localhost:7234/api/session/${id}`);
  }
}
