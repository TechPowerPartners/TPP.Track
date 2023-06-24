import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SessionsComponent } from './sessions/sessions.component';
import { SessionRoutingModule } from './session-routing.module';
import { SharedModule } from '@shared/shared.module';

@NgModule({
  declarations: [SessionsComponent],
  imports: [CommonModule, SessionRoutingModule, SharedModule],
})
export class SessionModule {}
