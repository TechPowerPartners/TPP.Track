import { ModalModule } from 'ngx-bootstrap/modal';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ActivityRoutingModule } from './activity-routing.module';
import { ActivitiesComponent } from './activities/activities.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    ActivitiesComponent
  ],
  imports: [
    CommonModule,
    ActivityRoutingModule,
    ReactiveFormsModule,
    ModalModule.forRoot(),
  ]
})
export class ActivityModule { }
