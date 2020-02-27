import { NgModule } from '@angular/core';
import { HttpClientModule } from "@angular/common/http";

import { EditAuthorRoutingModule } from './edit-author-routing.module';
import { EditAuthorComponent } from './edit-author.component';
import { SharedModule } from 'src/app/_shared/shared.module';

@NgModule({
  declarations: [
    EditAuthorComponent
  ],
  imports: [
    SharedModule,
    EditAuthorRoutingModule,
    HttpClientModule
  ],
  providers: [], 
  bootstrap: []
})
export class EditAuthorModule { }
