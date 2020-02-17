import { Injectable, EventEmitter, Output } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HttpTransportType } from '@aspnet/signalr';
import { SignalRMessage } from './signalR.message';
import { environment } from '../../../environments/environment';
import { Message } from 'primeng/api';
import * as signalR from '@aspnet/signalr';  

@Injectable()
export class SignalRService {
    @Output() notificationReceived: EventEmitter<SignalRMessage> = new EventEmitter();
    @Output() connectionEstablished: EventEmitter<any> = new EventEmitter();
 
    private _hubConnection: HubConnection;
    private baseUrl = `${environment.apiUrl}/notify`;
    public msgs: Message[] = [];

    constructor() {
    }
 
    public StartHub(){
        this.createConnection();
        this.startConnection();
        this.registerOnServerEvents();
    }

    public StopHub(){
        this._hubConnection.stop();
        console.log('Hub stopped');
        this._hubConnection = null;
    }

    private createConnection() {
        this._hubConnection = new HubConnectionBuilder()
            .configureLogging(signalR.LogLevel.Error)  
            .withUrl(this.baseUrl,
                {
                 transport: HttpTransportType.WebSockets
                }
            )
            .build();
        // console.log('Connection created');
    }
 
    private registerOnServerEvents() {
        this._hubConnection
            .on('BroadcastMessage', (data) => {
                    // console.log(data);
                    this.notificationReceived.emit(data);
                }
            );
        // console.log('Server events registered');
    }
 
    private startConnection(): void {
        this._hubConnection
            .start()
            .then(() => {
                // console.log('Hub started');
                this.connectionEstablished.emit({ severity: 'true', summary: 'empty' });
            }).catch(err => {
                console.log(`Error on connecting to signalR, retrying..`);
                setTimeout(() => this.startConnection, 5000);
            });
    }
}