import { Component, OnInit } from '@angular/core';
import { SignalRMessage } from './_shared/signalR/signalR.message';
import { SignalRService } from './_shared/signalR/signalR.service';
import { MessageNotifierService, Severities } from './_shared/messageNotifier/messageNotifier.service';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { IAppState } from './store/state/app.state';
import { IUser } from './_shared/model/user.model';
import { isUserLogged } from './store/selectors/login.selector';
import { Logout } from './store/actions/login.actions';
import { EntitiesEnum } from './store/actions/app.actions';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'LibraryUI';
  
  constructor(private signalRService: SignalRService, private router: Router,
    private messageNotifierService: MessageNotifierService,
    private store: Store<IAppState<IUser>>){ }
  
  userLogged$ = this.store.select(isUserLogged);

  ngOnInit(): void {

    this.signalRService.StartHub();

    this.signalRService.notificationReceived.subscribe((signalRMessage: SignalRMessage) => {
      console.log(signalRMessage)
        let json = JSON.parse(signalRMessage.payload);

        this.messageNotifierService.messageNotify(Severities.INFO, signalRMessage.type, json.Message);
    });
  }

  logout(): void {
    this.store.dispatch(new Logout(null, EntitiesEnum.Login))
  }

  login(): void {
    this.router.navigate(['login']);
  }

  authors(): void {
    this.router.navigate(['author', 'list-author']);
  }

  books(): void {
    this.router.navigate(['book', 'list-book']);
  }

  users(): void {
    this.router.navigate(['user', 'list-user']);
  }
 
}