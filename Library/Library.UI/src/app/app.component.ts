import { Component, OnInit } from '@angular/core';
import { SignalRMessage } from './_shared/signalR/signalR.message';
import { SignalRService } from './_shared/signalR/signalR.service';
import { MessageNotifierService, Severities } from './_shared/messageNotifier/messageNotifier.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'LibraryUI';

  constructor(private signalRService: SignalRService,
    private messageNotifierService: MessageNotifierService,
    private router: Router){

  }
  
  ngOnInit(): void {

    this.signalRService.StartHub();

    this.signalRService.notificationReceived.subscribe((signalRMessage: SignalRMessage) => {
        let json = JSON.parse(signalRMessage.payload);

        this.messageNotifierService.messageNotify(Severities.INFO, signalRMessage.type, json.Message.Name);
    });
  }
}
