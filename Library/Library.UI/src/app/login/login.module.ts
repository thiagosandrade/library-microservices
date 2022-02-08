import { NgModule } from '@angular/core';

import { HttpClientModule } from "@angular/common/http";

import { LoginRoutingModule } from './login-routing.module';
import { AuthModule } from '../_guards/auth.module';
import { LoginComponent } from './login.component';
import { SharedModule } from '../_shared/shared.module';
import { LoginEffects } from '../store/effects/login.effects';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { loginReducer } from '../store/selectors/login.selector';

@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    StoreModule.forFeature('loginFeature', loginReducer),
    EffectsModule.forFeature(
      [ LoginEffects ]
    ),
    SharedModule,
    AuthModule,
    LoginRoutingModule,
    HttpClientModule
  ],
  providers: [], 
  bootstrap: []
})
export class LoginModule { }
