import { NgModule } from '@angular/core';

import { AuthorRoutingModule } from './author-routing.module';
import { HttpClientModule } from "@angular/common/http";

import { AuthModule } from '../_guards/auth.module';

@NgModule({
  declarations: [
  ],
  imports: [
    AuthModule,
    AuthorRoutingModule,
    HttpClientModule
  ],
  providers: [], 
  bootstrap: []
})
export class AuthorModule { }
