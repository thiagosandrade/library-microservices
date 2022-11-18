import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiLoginService } from 'src/app/_shared/service/api.login.service';
import { Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { Delete, EntitiesEnum, Create, Get, GetAll } from 'src/app/store/actions/app.actions';
import { ICart, IProductCart } from 'src/app/_shared/model/cart.model';
import { selectLoggedUser } from 'src/app/store/selectors/login.selector';
import { IUser } from 'src/app/_shared/model/user.model';
import { getSelectedCart } from 'src/app/store/selectors/cart.selector';
import { IBook } from '../_shared/model/book.model';
import { selectBookList } from '../store/selectors/book.selector';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  constructor(private router: Router, private apiLoginService: ApiLoginService,
    private store: Store<IAppState<IUser>>, private cartStore: Store<IAppState<ICart>>, private bookStore: Store<IAppState<IBook>>) { }

  userLogged$ = this.store.select(selectLoggedUser);
  cartItems : IProductCart[] = [];
  booksList : IBook[] = [];

  ngOnInit() {

    if(!this.apiLoginService.isLogged()) {
      this.router.navigate(['login']);
      return;
    }
    
    this.bookStore.dispatch(new GetAll(EntitiesEnum.Book));

    this.userLogged$.subscribe(item => {
      if(item.id != null && item.id != undefined){
        this.store.dispatch(new Get(item, EntitiesEnum.Cart));
      }

      this.bookStore.select(selectBookList).subscribe(books => 
        {
          this.booksList = books
          this.cartStore.select(getSelectedCart).subscribe(cart => {
            if(cart != null && cart != undefined && this.booksList.length > 0)
              this.cartItems = cart.items
      
              this.cartItems.map(item => item.productName = this.booksList !== null ? this.booksList.filter(x => x.id === item.productId)[0].title : '')
          })
        })
    })
  }

  removeItemFromCart(item: IProductCart): void {
    item.quantity = 1;
    item.id = 0;
    this.store.dispatch(new Delete(item, EntitiesEnum.Cart));
  };

  addItemToCart(item: IProductCart): void {
    item.quantity = 1;
    item.id = 0;
    this.store.dispatch(new Create(item, EntitiesEnum.Cart))
  };
}
