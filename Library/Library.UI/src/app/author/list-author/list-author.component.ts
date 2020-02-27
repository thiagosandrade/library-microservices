import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IAuthor } from 'src/app/_shared/model/author.model';
import { SignalRService } from 'src/app/_shared/signalR/signalR.service';
import { ApiLoginService } from 'src/app/_shared/service/api.login.service';
import { Store, select } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { selectAuthorList } from 'src/app/store/selectors/author.selector';
import { GetAll, SetSelected, Delete, EntitiesEnum } from 'src/app/store/actions/app.actions';

@Component({
  selector: 'app-list-author',
  templateUrl: './list-author.component.html',
  styleUrls: ['./list-author.component.scss']
})
export class ListAuthorComponent implements OnInit {

  constructor(private router: Router, private apiLoginService: ApiLoginService,
    private signalRService: SignalRService, private store: Store<IAppState<IAuthor>>) { }

  authors$ = this.store.pipe(select(selectAuthorList));
    
  ngOnInit() {

    if(!this.apiLoginService.isLogged()) {
      this.router.navigate(['login']);
      return;
    }

    this.signalRService.StartHub();
    this.signalRService.notificationReceived.subscribe(() => {
      this.authors$ = this.store.pipe(select(selectAuthorList));
    });

    this.store.dispatch(new GetAll(EntitiesEnum.Author));
  }

  deleteAuthor(author: IAuthor): void {
    this.store.dispatch(new Delete(author, EntitiesEnum.Author));
  };

  editAuthor(user: IAuthor): void {
    this.store.dispatch(new SetSelected(user, EntitiesEnum.Author))
    this.router.navigate(['author','edit-author']);
  };

  addAuthor(): void {
    this.router.navigate(['author','add-author']);
  };
}
