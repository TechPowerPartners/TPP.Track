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
