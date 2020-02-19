import { NgModule } from '@angular/core';

import { HttpClientModule } from "@angular/common/http";

import { LoginRoutingModule } from './login-routing.module';
import { AuthModule } from '../_guards/auth.module';
import { LoginComponent } from './login.component';
import { SharedModule } from '../_shared/shared.module';

@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    SharedModule,
    AuthModule,
    LoginRoutingModule,
    HttpClientModule
  ],
  providers: [], 
  bootstrap: []
})
export class LoginModule { }
