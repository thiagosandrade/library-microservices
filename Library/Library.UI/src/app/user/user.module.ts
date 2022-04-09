import { NgModule } from '@angular/core';

import { UserRoutingModule } from './user-routing.module';
import { HttpClientModule } from "@angular/common/http";

import { AuthModule } from '../_guards/auth.module';
import { StoreModule } from '@ngrx/store';
import { userReducer } from '../store/selectors/user.selector';
import { EffectsModule } from '@ngrx/effects';
import { UserEffects } from '../store/effects/user.effects';

@NgModule({
  declarations: [
  ],
  imports: [
    StoreModule.forFeature('usersFeature', userReducer),
    EffectsModule.forFeature(
      [ UserEffects]
    ),
    AuthModule,
    UserRoutingModule,
    HttpClientModule
  ],
  providers: [], 
  bootstrap: []
})
export class UserModule { }
