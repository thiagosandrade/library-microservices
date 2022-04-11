import { NgModule } from '@angular/core';
import { HttpClientModule } from "@angular/common/http";

import { EditBookRoutingModule } from './edit-book-routing.module';
import { EditBookComponent } from './edit-book.component';
import { SharedModule } from 'src/app/_shared/shared.module';

@NgModule({
  declarations: [
    EditBookComponent
  ],
  imports: [
    SharedModule,
    EditBookRoutingModule,
    HttpClientModule
  ],
  providers: [], 
  bootstrap: []
})
export class EditBookModule { }
