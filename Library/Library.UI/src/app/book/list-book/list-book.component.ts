import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IBook } from 'src/app/_shared/model/book.model';
import { SignalRService } from 'src/app/_shared/signalR/signalR.service';
import { ApiLoginService } from 'src/app/_shared/service/api.login.service';
import { Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { selectBookList } from 'src/app/store/selectors/book.selector';
import { GetAll, SetSelected, Delete, EntitiesEnum } from 'src/app/store/actions/app.actions';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-list-book',
  templateUrl: './list-book.component.html',
  styleUrls: ['./list-book.component.scss']
})
export class ListBookComponent implements OnInit {

  constructor(private router: Router, private apiLoginService: ApiLoginService,
    private signalRService: SignalRService, private store: Store<IAppState<IBook>>) { }

  books$ : Observable<IBook[]> = this.store.select(selectBookList);
    
  ngOnInit() {

    if(!this.apiLoginService.isLogged()) {
      this.router.navigate(['login']);
      return;
    }

    this.signalRService.StartHub();
    this.signalRService.notificationReceived.subscribe(() => {
      this.books$ = this.store.select(selectBookList);
    });

    this.store.dispatch(new GetAll(EntitiesEnum.Book));
  }

  deleteBook(book: IBook): void {
    this.store.dispatch(new Delete(book, EntitiesEnum.Book));
  };

  editBook(book: IBook): void {
    this.store.dispatch(new SetSelected(book, EntitiesEnum.Book));
    this.router.navigate(['book','edit-book']);
  };

  addBook(): void {
    this.router.navigate(['book','add-book']);
  };
}
