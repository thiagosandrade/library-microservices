import { NgModule } from '@angular/core';

import { BookRoutingModule } from './book-routing.module';
import { HttpClientModule } from "@angular/common/http";

import { AuthModule } from '../_guards/auth.module';
import { StoreModule } from '@ngrx/store';
import { bookReducer } from '../store/selectors/book.selector';
import { EffectsModule } from '@ngrx/effects';
import { BookEffects } from '../store/effects/book.effects';

@NgModule({
  declarations: [
  ],
  imports: [
    StoreModule.forFeature('booksFeature', bookReducer),
    EffectsModule.forFeature(
      [ BookEffects ]
    ),
    AuthModule,
    BookRoutingModule,
    HttpClientModule
  ],
  providers: [], 
  bootstrap: []
})
export class BookModule { }
