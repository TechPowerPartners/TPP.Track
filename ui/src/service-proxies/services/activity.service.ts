import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { ActivityVm } from '../dto/activity/activityVm';

@Injectable()
export class ActivityServiceProxy {
  constructor(private readonly _http: HttpClient) {}

  public getAll(): Observable<ActivityVm> {
    return this._http.get<ActivityVm>('https://localhost:7234/api/activity');
  }
}
