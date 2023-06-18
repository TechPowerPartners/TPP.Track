import { Injectable } from '@angular/core';  
import { HttpClient, HttpHeaders } from '@angular/common/http';  
import { Observable, throwError, of } from 'rxjs';  
import { catchError, map } from 'rxjs/operators';  
import * as signalR from '@microsoft/signalr';
  
export class HubService {  
  private baseUrl = 'https://localhost:7234/';
  private hubName = 'timerhub';  
  
  private hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chat", { transport: signalR.HttpTransportType.WebSockets })
    .configureLogging(signalR.LogLevel.Information)
    .build();
}   