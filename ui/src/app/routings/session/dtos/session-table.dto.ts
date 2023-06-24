import { Observable } from 'rxjs';
import { TimeSpan } from '@shared/models/timespan';

export class SessionTableDto {
  id: string = '';
  startTime!: moment.Moment;
  endTime!: moment.Moment;
  duration!: TimeSpan;
  activityName!: Observable<string>;
}
