import * as moment from 'moment';

export interface ISessionVm {
  id: string;
  startTime: moment.Moment;
  endTime: moment.Moment;
  duration: string;
  activityId: string;
}

export class SessionVm implements ISessionVm {
  id: string = '';
  startTime!: moment.Moment;
  endTime!: moment.Moment;
  duration!: string;
  activityId!: string;

  constructor(obj: Partial<ISessionVm>) {
    Object.assign(this, obj);
  }
}

export interface ICreateSessionDto {
  startTime: moment.Moment;
  activityId: string;
}

export class CreateSessionDto implements ICreateSessionDto {
  startTime!: moment.Moment;
  activityId!: string;

  constructor(obj: Partial<ICreateSessionDto>) {
    Object.assign(this, obj);
  }
}

export interface IEndSessionDto {
  id: string;
  duration: string;
}

export class EndSessionDto implements IEndSessionDto {
  id!: string;
  duration!: string;

  constructor(obj: Partial<IEndSessionDto>) {
    Object.assign(this, obj);
  }
}
