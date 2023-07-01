import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { baseUrl } from 'src/service-proxies/const';

@Component({
  selector: 'app-timer',
  templateUrl: './timer.component.html',
  styleUrls: ['./timer.component.scss'],
})
export class TimerComponent implements OnInit {
  @Input() public canStart: boolean = false;

  @Output() public onStart: EventEmitter<void> = new EventEmitter();
  @Output() public onEnd: EventEmitter<string> = new EventEmitter();

  public timerData: string = '00:00:00';
  public timerStarting: boolean = false;

  private hubConnection!: signalR.HubConnection;

  ngOnInit(): void {
    this.buildSignalR();
    this.openConnect();
  }

  public start(): void {
    this.hubConnection.invoke('StartTimer');
    this.hubConnection.invoke('StartSendingData');
    this.onStart.emit();
    this.timerStarting = true;
  }

  public stop(): void {
    this.hubConnection.invoke('StopSendingData');
    this.hubConnection.invoke('StopTimer');
    this.hubConnection.invoke('ResetTimer');
    this.onEnd.emit(this.timerData);
    this.timerStarting = false;
  }

  private buildSignalR(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Debug)
      .withUrl(baseUrl + 'timerhub', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
      })
      .build();
  }

  private openConnect(): void {
    try {
      this.hubConnection
        .start()
        .then(() => {
          console.log('SignalR Connected!');
          this.hubConnection.on('ReceiveData', (data: string) => {
            this.timerData = data;
          });
        })
        .catch((err) => {
          console.log('ERROR: ' + err.toString());
        });
    } catch (ex) {}
  }
}
