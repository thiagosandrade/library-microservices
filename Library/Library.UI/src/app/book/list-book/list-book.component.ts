import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IBook } from 'src/app/_shared/model/book.model';
import { SignalRService } from 'src/app/_shared/signalR/signalR.service';
import { ApiLoginService } from 'src/app/_shared/service/api.login.service';
import { Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { selectBookList } from 'src/app/store/selectors/book.selector';
import { GetAll, SetSelected, Delete, EntitiesEnum, Create, Get } from 'src/app/store/actions/app.actions';
import { Observable } from 'rxjs';
import { ICart, IProductCart } from 'src/app/_shared/model/cart.model';
import { getSelectedCart } from 'src/app/store/selectors/cart.selector';

@Component({
  selector: 'app-list-book',
  templateUrl: './list-book.component.html',
  styleUrls: ['./list-book.component.scss']
})
export class ListBookComponent implements OnInit {

  constructor(private router: Router, private apiLoginService: ApiLoginService,
    private signalRService: SignalRService, private store: Store<IAppState<IBook>>, private cartStore: Store<IAppState<ICart>>) { }

    books$ : Observable<IBook[]> = this.store.select(selectBookList);
    private cartId = 0;

  tableColumns : any[]= [
    {name: 'Id', prop: 'id'},
    {name: 'Image', prop: 'thumbnailUrl'},
    { name: 'ISBN', prop:'isbn'}, 
    { name: 'Title', prop:'title'}, 
    { name: 'ShortDescription', prop: 'shortDescription'}, 
    { name: 'Authors', prop: 'authorsAsString'},
    { name: '', prop: ''}];
    
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

    this.cartStore.select(getSelectedCart).subscribe(item => {
      this.cartId = item.id
    })
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

  addItemToCart(book: IBook): void {
   
      let cartItem : IProductCart = {
        id: 0,
        cartId: this.cartId,
        productId: book.id,
        quantity: 1
      }

      this.cartStore.dispatch(new Create(cartItem, EntitiesEnum.Cart));
  }
}
