import { NgModule } from '@angular/core';

import { HttpClientModule } from "@angular/common/http";

import { AuthModule } from '../_guards/auth.module';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { CartRoutingModule } from './cart-routing.module';
import { CartEffects } from '../store/effects/cart.effects';
import { cartReducer } from '../store/selectors/cart.selector';
import { CartComponent } from './cart.component';
import { SharedModule } from '../_shared/shared.module';
import { BookModule } from '../book/book.module';

@NgModule({
  declarations: [
    CartComponent
  ],
  imports: [
    StoreModule.forFeature('cartsFeature', cartReducer),
    EffectsModule.forFeature(
      [ CartEffects ]
    ),
    SharedModule,
    AuthModule,
    AuthModule,
    BookModule,
    CartRoutingModule,
    HttpClientModule
  ],
  exports:[
    CartComponent
  ],
  providers: [], 
  bootstrap: []
})
export class CartModule { }
