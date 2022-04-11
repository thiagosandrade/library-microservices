import { NgModule } from '@angular/core';
import { HttpClientModule } from "@angular/common/http";

import { ListBookRoutingModule } from './list-book-routing.module';
import { ListBookComponent } from './list-book.component';
import { SharedModule } from 'src/app/_shared/shared.module';

@NgModule({
  declarations: [
    ListBookComponent
  ],
  imports: [
    SharedModule,
    ListBookRoutingModule,
    HttpClientModule
  ],
  providers: [], 
  bootstrap: []
})
export class ListBookModule { }
