import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IUser } from 'src/app/_shared/model/user.model';
import { SignalRService } from 'src/app/_shared/signalR/signalR.service';
import { ApiLoginService } from 'src/app/_shared/service/api.login.service';
import { Store, select } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { selectUserList } from 'src/app/store/selectors/user.selector';
import { GetAll, SetSelected, Delete, EntitiesEnum } from 'src/app/store/actions/app.actions';
import { Observable } from 'rxjs';
import { isUserLoggedAdmin } from 'src/app/store/selectors/login.selector';

@Component({
  selector: 'app-list-user',
  templateUrl: './list-user.component.html',
  styleUrls: ['./list-user.component.scss']
})
export class ListUserComponent implements OnInit {

  constructor(private router: Router, private apiLoginService: ApiLoginService,
    private signalRService: SignalRService, private store: Store<IAppState<IUser>>) { }

  users$ : Observable<IUser[]> = this.store.pipe(select(selectUserList));
  isAdmin$ : Observable<boolean> = this.store.pipe(select(isUserLoggedAdmin));
    
  ngOnInit() {

    if(!this.apiLoginService.isLogged()) {
      this.router.navigate(['login']);
      return;
    }

    this.signalRService.StartHub();
    this.signalRService.notificationReceived.subscribe(() => {
      this.users$ = this.store.pipe(select(selectUserList));
    });
    this.store.dispatch(new GetAll(EntitiesEnum.User));
  }

  deleteUser(user: IUser): void {
    this.store.dispatch(new Delete(user, EntitiesEnum.User));
  };

  editUser(user: IUser): void {
    this.store.dispatch(new SetSelected(user, EntitiesEnum.User))
    this.router.navigate(['user','edit-user']);
  };

  addUser(): void {
    this.router.navigate(['user','add-user']);
  };
}
