import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { TimerComponent } from '@components/timer/timer.component';
import { HomeRoutingModule } from './home-routing.module';

@NgModule({
  declarations: [HomeComponent, TimerComponent],
  imports: [CommonModule, HomeRoutingModule],
})
export class HomeModule {}
