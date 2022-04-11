import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IAuthor } from 'src/app/_shared/model/author.model';
import { SignalRService } from 'src/app/_shared/signalR/signalR.service';
import { ApiLoginService } from 'src/app/_shared/service/api.login.service';
import { Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { selectAuthorList } from 'src/app/store/selectors/author.selector';
import { GetAll, SetSelected, Delete, EntitiesEnum } from 'src/app/store/actions/app.actions';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-list-author',
  templateUrl: './list-author.component.html',
  styleUrls: ['./list-author.component.scss']
})
export class ListAuthorComponent implements OnInit {

  constructor(private router: Router, private apiLoginService: ApiLoginService,
    private signalRService: SignalRService, private store: Store<IAppState<IAuthor>>) { }

  authors$ : Observable<IAuthor[]> = this.store.select(selectAuthorList);
  tableColumns : any[]= [
    {name: 'Id', prop: 'id'},
    { name: 'Name', prop:'name'}, 
    { name: 'Surname', prop:'surname'}, 
    { name: 'Birth', prop: 'birth'}, 
    { name: 'Age', prop: 'age'}];

  ngOnInit() {

    if(!this.apiLoginService.isLogged()) {
      this.router.navigate(['login']);
      return;
    }

    this.signalRService.StartHub();
    this.signalRService.notificationReceived.subscribe(() => {
      this.authors$ = this.store.select(selectAuthorList);
    });

    this.store.dispatch(new GetAll(EntitiesEnum.Author));
  }

  deleteAuthor(author: IAuthor): void {
    this.store.dispatch(new Delete(author, EntitiesEnum.Author));
  };

  editAuthor(author: IAuthor): void {
    this.store.dispatch(new SetSelected(author, EntitiesEnum.Author))
    this.router.navigate(['author','edit-author']);
  };

  addAuthor(): void {
    this.router.navigate(['author','add-author']);
  };
}
