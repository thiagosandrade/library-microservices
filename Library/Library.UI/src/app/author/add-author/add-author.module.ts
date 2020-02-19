import { NgModule } from '@angular/core';

import { HttpClientModule } from "@angular/common/http";

import { AddAuthorRoutingModule } from './add-author-routing.module';
import { AddAuthorComponent } from './add-author.component';
import { SharedModule } from 'src/app/_shared/shared.module';


@NgModule({
  declarations: [
    AddAuthorComponent
  ],
  imports: [
    SharedModule,
    AddAuthorRoutingModule,
    HttpClientModule
  ],
  providers: [], 
  bootstrap: []
})
export class AddAuthorModule { }
