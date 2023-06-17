import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-timer',
  templateUrl: './timer.component.html',
  styleUrls: ['./timer.component.scss'],
})
export class TimerComponent implements OnInit {
  private hubConnection!: signalR.HubConnection;
  public timerData: string = '';

  ngOnInit(): void {
    this.startTimer();
  }

    this.hubConnection.start().then(() => {
      console.log('SignalR Connected!');
      this.startListening();
    }).catch((err) => {
      console.log('ERROR: ' + err.toString());
    });
  }

  stopTimer(): void {
    clearInterval(this.intervalId);
  }

    this.hubConnection.invoke('StartSendingData');
  }
}