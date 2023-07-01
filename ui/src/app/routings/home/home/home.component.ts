import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { ActivityVm } from 'src/service-proxies/dto/activity';
import { ActivityServiceProxy } from 'src/service-proxies/services/activity.service';
import { SessionServiceProxy } from 'src/service-proxies/services/session.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  public sessionId: string | null = null;
  public canTimeStart: boolean = false;
  public activities: ActivityVm[] = [];
  public selectedActivityId: string = '';

  public get activitySelectDisabled(): boolean {
    return !!(this.sessionId && this.selectedActivityId);
  }

  constructor(
    private readonly _sessionService: SessionServiceProxy,
    private readonly _activitySession: ActivityServiceProxy
  ) {}

  ngOnInit(): void {
    this.sessionId = localStorage.getItem('session');
    this.loadActivities();
  }

  public onTimerStart(): void {
    this._sessionService
      .create({
        startTime: moment(),
        activityId: this.selectedActivityId,
      })
      .subscribe((sessionId: string) => {
        this.sessionId = sessionId;
        localStorage.setItem('session', sessionId);
        localStorage.setItem('activity', this.selectedActivityId);
      });
  }

  public onTimerEnd(duration: string): void {
    this._sessionService
      .end({
        id: this.sessionId!,
        duration: duration,
      })
      .subscribe(() => {
        this.sessionId = null;
        localStorage.removeItem('session');
        localStorage.removeItem('activity');
      });
  }

  public loadActivities(): void {
    this._activitySession.getAll().subscribe((activities: ActivityVm[]) => {
      this.activities = activities;

      this.selectedActivityId = activities[0].id;
    });
  }

  public onActivitySelect(activity: ActivityVm): void {
    this.canTimeStart = !!activity;
  }
}
