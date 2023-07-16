import { map } from 'rxjs/operators';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Subject, catchError, of, takeUntil, Observable } from 'rxjs';
import { ActivityVm } from 'src/service-proxies/dto/activity';
import { SessionVm } from 'src/service-proxies/dto/session';
import { ActivityServiceProxy } from 'src/service-proxies/services/activity.service';
import { SessionServiceProxy } from 'src/service-proxies/services/session.service';
import { SessionTableDto } from '../dtos/session-table.dto';

@Component({
  selector: 'app-sessions',
  templateUrl: './sessions.component.html',
  styleUrls: ['./sessions.component.scss'],
})
export class SessionsComponent implements OnInit, OnDestroy {
  public sessions: SessionTableDto[] = [];

  private subject$: Subject<void> = new Subject<void>();

  constructor(
    private readonly _sessionService: SessionServiceProxy,
    private readonly _activityService: ActivityServiceProxy,
    private readonly _toastrService: ToastrService
  ) {}

  ngOnInit(): void {
    this._sessionService
      .getAll()
      .pipe(
        catchError((error) => {
          this._toastrService.error(error);
          return of();
        }),
        takeUntil(this.subject$)
      )
      .subscribe((sessions: SessionVm[]) => {
        this.sessions = sessions.map((session) => {
          return {
            ...session,
            activityName: this.loadActivity(session.activityId),
          };
        });
      });
  }

  ngOnDestroy(): void {
    this.subject$.next();
    this.subject$.complete();
  }

  public create(): void {}

  public update(id: string): void {}

  public delete(id: string): void {}

  private loadActivity(id: string): Observable<string> {
    return this._activityService.get(id).pipe(
      map((activity) => {
        return activity.name;
      })
    );
  }
}
