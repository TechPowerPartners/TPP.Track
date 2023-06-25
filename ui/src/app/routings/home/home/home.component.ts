import { Component } from '@angular/core';
import * as moment from 'moment';
import { ActivityServiceProxy } from 'src/service-proxies/services/activity.service';
import { SessionServiceProxy } from 'src/service-proxies/services/session.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  public canTimeStart: boolean = true;

  constructor(
    private readonly _sessionService: SessionServiceProxy,
    private readonly _activitySession: ActivityServiceProxy
  ) {}

  public onTimerStart(): void {
    this._sessionService.create({
      startTime: moment(),
      activityId: '',
    });
  }

  public onTimerEnd(duration: string): void {}
}
