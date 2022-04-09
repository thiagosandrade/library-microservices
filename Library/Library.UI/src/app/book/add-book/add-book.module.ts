import { NgModule } from '@angular/core';
import { HttpClientModule } from "@angular/common/http";

import { AddBookRoutingModule } from './add-book-routing.module';
import { AddBookComponent } from './add-book.component';
import { SharedModule } from 'src/app/_shared/shared.module';

@NgModule({
  declarations: [
    AddBookComponent
  ],
  imports: [
    SharedModule,
    AddBookRoutingModule,
    HttpClientModule
  ],
  providers: [], 
  bootstrap: []
})
export class AddBookModule { }
