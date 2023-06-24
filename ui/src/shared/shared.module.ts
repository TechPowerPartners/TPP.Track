import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MomentPipe } from './pipes/moment.pipe';

@NgModule({
  declarations: [MomentPipe],
  exports: [MomentPipe],
  imports: [CommonModule],
})
export class SharedModule {}
