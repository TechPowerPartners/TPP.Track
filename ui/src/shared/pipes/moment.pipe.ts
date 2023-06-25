import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({
  name: 'momentPipe',
})
export class MomentPipe implements PipeTransform {
  transform(value: Date | moment.Moment, ...args: any[]): any {
    let [format] = args;
    return moment(value).format(format);
  }
}
