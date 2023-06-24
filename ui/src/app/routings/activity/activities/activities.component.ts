import { Component, OnInit, OnDestroy } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Subject, catchError, of, takeUntil } from 'rxjs';
import { ActivityVm } from 'src/service-proxies/dto/activity';
import { ActivityServiceProxy } from 'src/service-proxies/services/activity.service';

@Component({
  selector: 'app-activities',
  templateUrl: './activities.component.html',
  styleUrls: ['./activities.component.scss'],
})
export class ActivitiesComponent implements OnInit, OnDestroy {
  public activities: ActivityVm[] = [];

  private subject$: Subject<void> = new Subject<void>();

  constructor(
    private readonly _activityService: ActivityServiceProxy,
    private readonly _toastrService: ToastrService
  ) {}

  ngOnInit(): void {
    this._activityService
      .getAll()
      .pipe(
        catchError((error) => {
          this._toastrService.error(error);
          return of();
        }),
        takeUntil(this.subject$)
      )
      .subscribe((activities: ActivityVm[]) => {
        this.activities = activities;
      });
  }

  ngOnDestroy(): void {
    this.subject$.next();
    this.subject$.complete();
  }

  public create(): void {}

  public update(id: string): void {}

  public delete(id: string): void {}
}
