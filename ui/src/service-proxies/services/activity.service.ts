import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  IActivityVm,
  ICreateActivityDto,
  IUpdateActivityDto,
} from '../dto/activity';

@Injectable({ providedIn: 'root' })
export class ActivityServiceProxy {
  constructor(private readonly _http: HttpClient) {}

  public getAll(): Observable<IActivityVm[]> {
    return this._http.get<IActivityVm[]>(
      'https://localhost:7234/api/activity/getAll'
    );
  }

  public get(id: string): Observable<IActivityVm> {
    return this._http.get<IActivityVm>(
      `https://localhost:7234/api/activity/${id}`
    );
  }

  public create(dto: ICreateActivityDto): Observable<string> {
    return this._http.post<string>(
      'https://localhost:7234/api/activity/create',
      dto
    );
  }

  public update(dto: IUpdateActivityDto): Observable<void> {
    return this._http.put<void>(
      'https://localhost:7234/api/activity/update',
      dto
    );
  }

  public delete(id: string): Observable<void> {
    return this._http.delete<void>(`https://localhost:7234/api/activity/${id}`);
  }
}
