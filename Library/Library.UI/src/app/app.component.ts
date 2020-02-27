import { Component, OnInit } from '@angular/core';
import { SignalRMessage } from './_shared/signalR/signalR.message';
import { SignalRService } from './_shared/signalR/signalR.service';
import { MessageNotifierService, Severities } from './_shared/messageNotifier/messageNotifier.service';
import { ApiLoginService } from './_shared/service/api.login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'LibraryUI';
  
  constructor(private signalRService: SignalRService, private router: Router,
    private messageNotifierService: MessageNotifierService,
    private loginService: ApiLoginService){ }
  
  ngOnInit(): void {

    this.signalRService.StartHub();

    this.signalRService.notificationReceived.subscribe((signalRMessage: SignalRMessage) => {
        let json = JSON.parse(signalRMessage.payload);

        this.messageNotifierService.messageNotify(Severities.INFO, signalRMessage.type, json.Message.Name);
    });
  }

  logout(): void {
    this.loginService.logout();
  }

  login(): void {
    this.router.navigate(['login']);
  }

  get isLoggedIn() {
    var result = this.loginService.isLogged().subscribe(
      data => {
        if(data != null)
          return true;

        return false;
      }
    );

    return result;
  }
}
