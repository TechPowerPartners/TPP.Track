import { Component, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Component({
  selector: 'app-timer',
  templateUrl: './timer.component.html',
  styleUrls: ['./timer.component.scss'],
})
export class TimerComponent implements OnInit {
  private hubConnection!: signalR.HubConnection;
  public timerData: string = '';

  ngOnInit(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Debug)
      .withUrl('https://localhost:7234/timerhub', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
      })
      .build();

    this.hubConnection.start().then(() => {
      console.log('SignalR Connected!');
      this.startListening();
    }).catch((err) => {
      console.log('ERROR: ' + err.toString());
    });
  }

  private startListening(): void {
    this.hubConnection.on('ReceiveData', (data: string) => {
      this.timerData = data;
    });

    this.hubConnection.invoke('StartSendingData');
  }
}