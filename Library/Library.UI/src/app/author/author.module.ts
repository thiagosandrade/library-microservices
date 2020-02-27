import { NgModule } from '@angular/core';

import { AuthorRoutingModule } from './author-routing.module';
import { HttpClientModule } from "@angular/common/http";

import { AuthModule } from '../_guards/auth.module';
import { StoreModule } from '@ngrx/store';
import { authorReducer } from '../store/selectors/author.selector';
import { EffectsModule } from '@ngrx/effects';
import { AuthorEffects } from '../store/effects/author.effects';

@NgModule({
  declarations: [
  ],
  imports: [
    StoreModule.forFeature('authorsFeature', authorReducer),
    EffectsModule.forFeature(
      [ AuthorEffects ]
    ),
    AuthModule,
    AuthorRoutingModule,
    HttpClientModule
  ],
  providers: [], 
  bootstrap: []
})
export class AuthorModule { }
