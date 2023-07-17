export interface IActivityVm {
  id: string;
  name: string;
  description?: string;
}

export class ActivityVm implements IActivityVm {
  id: string = '';
  name: string = '';
  description?: string = '';

  constructor(obj: Partial<IActivityVm>) {
    Object.assign(this, obj);
  }
}

export interface ICreateActivityDto {
  name: string;
  description?: string;
}

export class CreateActivityDto implements ICreateActivityDto {
  name: string = '';
  description?: string = '';

  constructor(obj: Partial<ICreateActivityDto>) {
    Object.assign(this, obj);
  }
}

export interface IUpdateActivityDto {
  id: string;
  name: string;
  description?: string;
}

export class UpdateActivityDto implements IUpdateActivityDto {
  id: string = '';
  name: string = '';
  description?: string = '';

  constructor(obj: Partial<IUpdateActivityDto>) {
    Object.assign(this, obj);
  }
}
