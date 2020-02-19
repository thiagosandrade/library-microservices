import { NgModule } from '@angular/core';
import { HttpClientModule } from "@angular/common/http";

import { ListAuthorRoutingModule } from './list-author-routing.module';
import { ListAuthorComponent } from './list-author.component';
import { SharedModule } from 'src/app/_shared/shared.module';


@NgModule({
  declarations: [
    ListAuthorComponent
  ],
  imports: [
    SharedModule,
    ListAuthorRoutingModule,
    HttpClientModule
  ],
  providers: [], 
  bootstrap: []
})
export class ListAuthorModule { }
