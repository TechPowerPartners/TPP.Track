import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-timer',
  templateUrl: './timer.component.html',
  styleUrls: ['./timer.component.scss'],
})
export class TimerComponent implements OnInit {
  private intervalId: any;
  private startTime: Date = new Date();
  public time: string = '';

  ngOnInit(): void {
    this.startTimer();
  }

  startTimer(): void {
    this.intervalId = setInterval(() => {
      const currentTime = new Date();
      const elapsedTime = currentTime.getTime() - this.startTime.getTime();
      this.time = this.formatTime(elapsedTime);
    }, 1000);
  }

  stopTimer(): void {
    clearInterval(this.intervalId);
  }

  formatTime(time: number): string {
    const hours = Math.floor(time / 3600000);
    const minutes = Math.floor((time / 60000) % 60);
    const seconds = Math.floor((time / 1000) % 60);
    return `${hours}:${minutes < 10 ? '0' : ''}${minutes}:${
      seconds < 10 ? '0' : ''
    }${seconds}`;
  }
}
