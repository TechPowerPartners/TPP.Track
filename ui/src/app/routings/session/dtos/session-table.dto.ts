import { Observable } from 'rxjs';

export class SessionTableDto {
  id: string = '';
  startTime!: moment.Moment;
  endTime!: moment.Moment;
  duration!: string;
  activityName!: Observable<string>;
}
